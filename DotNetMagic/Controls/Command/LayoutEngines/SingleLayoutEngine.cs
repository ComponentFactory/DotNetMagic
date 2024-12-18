// *****************************************************************************
// 
//  (c) Crownwood Software Ltd 2004-2005. All rights reserved. 
//	The software and associated documentation supplied hereunder are the 
//	proprietary information of Crownwood Software Ltd, Bracknell, 
//	Berkshire, England and are supplied subject to licence terms.
// 
//  Version 3.0.3.0 	www.crownwood.net
// *****************************************************************************

using System;
using System.Drawing;
using System.Windows.Forms;
using Crownwood.DotNetMagic.Common;

namespace Crownwood.DotNetMagic.Controls.Command
{
	/// <summary>
	/// Position commands on a single line.
	/// </summary>
	public class SingleLayoutEngine : ILayoutEngine
	{
		// Constants
		private const int MAXIMUM = int.MaxValue;
	
		// Instance fields
		private int _freeSpace;
		private int _leftSpace;
		private int _maximumTangent;
		private bool _finished;
		private Rectangle _layoutRect;
		private CommandStateCollection _states;
		private ICommandDetails _details;

		/// <summary>
		/// Perform sizing and positioning of commands.
		/// </summary>
		/// <param name="states">Collection of additional state for commands.</param>
		/// <param name="details">Lookup interface for calculations.</param>
		/// <returns>Bounding rectangle covering all commands.</returns>
		public Rectangle LayoutCommands(CommandStateCollection states,
										ICommandDetails details)
		{			
			// Peform layout of commands
			Rectangle rectLayout = InternalLayoutCommands(states, details, false);

			// Clear out any temporary working values
			ClearDown();

			return rectLayout;
		}

		/// <summary>
		/// Find the ideal size to fit all the commands inside.
		/// </summary>
		/// <param name="states">Collection of additional state for commands.</param>
		/// <param name="details">Lookup interface for calculations.</param>
		/// <returns>Bounding rectangle covering all commands.</returns>
		public Rectangle FindIdealSize(CommandStateCollection states,
									   ICommandDetails details)
		{			
			// Peform layout of commands
			Rectangle rectLayout = InternalLayoutCommands(states, details, true);

			// Track total width or height required
			int total = 0;
			
			// Process all commands
			for(int i=0; i<_states.Count; i++)
			{
				CommandState state = _states[i];

				// Add the size it needs for drawing in direction
				total += WithFlowLength(state.DrawRect.Size);
			}
			
			// Modify the return rectangle with calculated total
			if (_details.Direction == LayoutDirection.Horizontal)
				rectLayout.Width = total;
			else
				rectLayout.Height = total;

			// Clear out any temporary working values
			ClearDown();

			// Peform layout of commands
			return rectLayout;
		}

        private Rectangle InternalLayoutCommands(CommandStateCollection states,
												 ICommandDetails details,
												 bool findIdealSize)
		{			
            Rectangle returnRect = Rectangle.Empty;

            using(Graphics g = details.HostControl.CreateGraphics())
            {
                // Prepare state for processing
                Prepare(g, states, details, findIdealSize);

                // Position the most important 'System' commands first, followed by
                // the less important 'Near' commands and lastly the 'Far' commands
                // which have the lowest priority of all.
                CalculateByPosition(g, Position.System, false);
                CalculateByPosition(g, Position.Near, true);
                CalculateByPosition(g, Position.Far, false);

                // Calculate the final positions depending on maximum values encountered
                CalculateFinalPositions();

                returnRect = _layoutRect;

                // Calculate the actual area we have decided to use
                if (_details.Direction == LayoutDirection.Horizontal)
                    returnRect.Height = _maximumTangent;
                else
                    returnRect.Width = _maximumTangent;
            }

            return returnRect;
        }

        private void Prepare(Graphics g, 
                             CommandStateCollection states, 
                             ICommandDetails details,
                             bool findIdealSize)
        {
            // Cache useful information
            _states = states;
            _details = details;

            // Initialize simple working values
            _maximumTangent = 0;
            _finished = false;
            
            // If try to find ideal size then give it unlimited space
            if (findIdealSize)
				_layoutRect = new Rectangle(0, 0, MAXIMUM, MAXIMUM);
            else
				_layoutRect = _details.LayoutRect;

            // Starting position depends on orientation
            if (_details.Direction == LayoutDirection.Horizontal)
                _leftSpace = _layoutRect.Left;
            else
                _leftSpace = _layoutRect.Top;

            // Amount of free space to use depends on orientation
            _freeSpace = WithFlowLength(_layoutRect);

            // Ask each command for its requested raw size
            CalculateRawCommandSize(g);

            // Adjust size of button commands to reflect detail properties
            AdjustButtonSizes();
        }

        private void CalculateRawCommandSize(Graphics g)
        {
            // Cache count of commands
            int commandCount = _states.Count;

            // Ask each command in turn to calculate its size
            for(int i=0; i<commandCount; i++)
            {
                CommandState state = _states[i];
                CommandBase command = state.Command;

                // Get and cache the minimum drawing size of the command
                state.DrawRect = new Rectangle(Point.Empty, command.CalculateDrawSize(g, _details, true));
            }
        }

        private void AdjustButtonSizes()
        {
            // Do we need to calculate the largest button size?
            if (_details.EqualButtonVert || _details.EqualButtonHorz)
            {
                // Cache count of commands
                int commandCount = _states.Count;

                // Cache largest values found so far
                Size largest = Size.Empty;

                // Check each command for those that are buttons
                for(int i=0; i<commandCount; i++)
                {
                    CommandState state = _states[i];
                    CommandBase command = state.Command;

                    // Is this treated as a button?
                    if (command is ICommandIsButton)
                    {
                        Size button = state.DrawRect.Size;

                        // Remember the largest values found
                        if (button.Width > largest.Width)   largest.Width = button.Width;
                        if (button.Height > largest.Height) largest.Height = button.Height;
                    }
                }

				bool equalVert = _details.EqualButtonVert;
				bool equalHorz = _details.EqualButtonHorz;
			
				// If showing in vertical alignment then need to swap around
				if (_details.Direction == LayoutDirection.Vertical)
				{
					// Swap values around
					bool temp = equalVert;
					equalVert = equalHorz;
					equalHorz = equalVert;
				}
					
				// Now process each button again, this time applying largest values
                for(int i=0; i<commandCount; i++)
                {
                    CommandState state = _states[i];
                    CommandBase command = state.Command;

                    // Is this treated as a button?
                    if (command is ICommandIsButton)
                    {

                        if (equalVert)
                        {
                            if (equalHorz)
                            {
                                // Equal in both directions, just set to largest value
                                state.DrawRect = new Rectangle(state.DrawRect.Location, largest);
                            }
                            else
                            {
                                // Update just the vertical value
                                state.DrawRect = new Rectangle(state.DrawRect.Location, 
                                                               new Size(state.DrawRect.Width, largest.Height));
                            }
                        }
                        else
                        {
                            // Update just the horizontal value
                            state.DrawRect = new Rectangle(state.DrawRect.Location, 
                                                           new Size(largest.Width, state.DrawRect.Height));
                        }
                    }
                }
            }
		}

		private void ClearDown()
		{
			_states = null;
			_details = null;
		}

		private void CalculateByPosition(Graphics g, 
                                         Position position, 
                                         bool posiflow)
		{
            // Cache number of commands
			int commandCount = _states.Count;

			// Process all commands
			for(int i=0; i<commandCount; i++)
			{
				CommandState state = _states[i];
				CommandBase command = state.Command;

				// Only interested in those of the correct type
				if (command.Position == position)
				{
					// Has positioning of commands finished or 
					// command requested to not be shown
					if (_finished || !command.IsDirectionVisible(_details.Direction))
					{
						// Need to inform command it is not currently displayed
						state.Displayed = false;
					}
					else
					{
						// Get drawing size of the command
						Size drawSize = state.DrawRect.Size;

						// Find the size in the direction we are flowing in
						int flowLength = WithFlowLength(drawSize);
						int tangentLength = AgainstFlowLength(drawSize);

						// Is there enough room for it to be placed?
						if (_freeSpace >= flowLength)
						{
							// Yes, this command is being displayed
							state.Displayed = true;

							// Position the command in direction of flow
							state.DrawRect = CalculateInitialDrawRect(drawSize, posiflow);

							// Record maximum length in opposite direction
							if (tangentLength > _maximumTangent)
								_maximumTangent = tangentLength;
						}
						else
						{
							// No, this command is not being displayed
							state.Displayed = false;

							// No more room for any more commands
							_finished = true;
						}
					}
				}
			}
		}

		private Rectangle CalculateInitialDrawRect(Size drawSize, bool posiflow)
		{
			Rectangle returnRect = Rectangle.Empty;

			// Get the length in the flowing direction
			int flowLength = WithFlowLength(drawSize);
			int tangentLength = AgainstFlowLength(drawSize);

			if (posiflow)
			{
				if (_details.Direction == LayoutDirection.Horizontal)
					returnRect = new Rectangle(_leftSpace, 0, flowLength,  tangentLength);
				else
					returnRect = new Rectangle(0, _leftSpace, tangentLength, flowLength);

				// Move the start of free space by the amount allocated
				_leftSpace += flowLength;

				// Reduce the total space available
				_freeSpace -= flowLength;
			}
			else
			{
				// Find right hand side of free space area
				int rightSpace = _leftSpace + _freeSpace;

				if (_details.Direction == LayoutDirection.Horizontal)
					returnRect = new Rectangle(rightSpace - flowLength, 0, flowLength,  tangentLength);
				else
					returnRect = new Rectangle(0, rightSpace - flowLength, tangentLength, flowLength);

				// Subtract flowing length from end of free space by jsut reducing
				// the total amount of free space that is now left
				_freeSpace -= flowLength;
			}

			return returnRect;
		}

		private void CalculateFinalPositions()
		{
			// Cache common values
			int commandCount = _states.Count;

			// Process all commands
			for(int i=0; i<commandCount; i++)
			{
				CommandState state = _states[i];
				CommandBase command = state.Command;

				// Only interested in those that are displayed
				if (state.Displayed)
				{
					Rectangle drawRect = state.DrawRect;

					// Is the command a fixed size in tangent direction
					if (command.FixedByDirection(_details.Direction))
					{
						if (_details.Direction == LayoutDirection.Horizontal)
						{
							// Find offset
							int offset = (_maximumTangent - state.DrawRect.Height) / 2;

							// Position halfway down the available space
							state.DrawRect = new Rectangle(drawRect.Left, _layoutRect.Top + offset, drawRect.Width, drawRect.Height);
						}
						else
						{
							// Find offset
							int offset = (_maximumTangent - state.DrawRect.Width) / 2;

							// Position halfway down the available space
							state.DrawRect = new Rectangle(_layoutRect.Left + offset, drawRect.Top, drawRect.Width, drawRect.Height);
						}
					}
					else
					{
						// Not fixed length, so make it stretch the entire maximum
						if (_details.Direction == LayoutDirection.Horizontal)
							state.DrawRect = new Rectangle(drawRect.Left, _layoutRect.Top, drawRect.Width, _maximumTangent);
						else
							state.DrawRect = new Rectangle(_layoutRect.Left, drawRect.Top, _maximumTangent, drawRect.Height);
					}
				}
			}
		}

		private int WithFlowLength(Rectangle rawRect)
		{
			return WithFlowLength(new Size(rawRect.Width, rawRect.Height));
		}

		private int WithFlowLength(Size rawSize)
		{
			if (_details.Direction == LayoutDirection.Horizontal)
				return rawSize.Width;
			else
				return rawSize.Height;
		}

		private int AgainstFlowLength(Size rawSize)
		{
			if (_details.Direction != LayoutDirection.Horizontal)
				return rawSize.Width;
			else
				return rawSize.Height;
		}
	}
}
