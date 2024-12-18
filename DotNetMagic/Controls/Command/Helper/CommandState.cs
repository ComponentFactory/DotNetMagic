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
using Crownwood.DotNetMagic.Common;

namespace Crownwood.DotNetMagic.Controls.Command
{
	/// <summary>
	/// Maintains additional state information for a command.
	/// </summary>
	public class CommandState
	{
		// Instance fields
		private ItemState _state;
		private bool _displayed;
		private Rectangle _drawRect;
		private CommandBase _command;
		private ICommandDetails _details;

		/// <summary>
		/// Initializes a new instance of the CommandState class.
		/// </summary>
		/// <param name="details">Source used for obtaining command details.</param>
		/// <param name="command">Command to handle state for.</param>
		public CommandState(ICommandDetails details, CommandBase command)
		{
			// Default state
			_state = ItemState.Normal;
			_displayed = false;
			_drawRect = Rectangle.Empty;
			_command = command;
			_details = details;
		}

		/// <summary>
		/// Gets or sets the state to draw command in.
		/// </summary>
		public ItemState State
		{
			get { return _state; }
            set { _state = value; }
		}

		/// <summary>
		/// Gets or sets the displayed state of the command.
		/// </summary>
		public bool Displayed
		{
			get { return _displayed; }
			set { _displayed = value; }
		}

		/// <summary>
		/// Gets or sets the rectangle used to draw the command.
		/// </summary>
		public Rectangle DrawRect
		{
			get { return _drawRect; }
			set { _drawRect = value; }
		}

		/// <summary>
		/// Gets and sets the command this state object handles.
		/// </summary>
		public CommandBase Command
		{
			get { return _command; }
			set { _command = value; }
		}

		/// <summary>
		/// Decide if the point is inside the command display rectangle
		/// </summary>
		/// <param name="pt">Point to test against.</param>
		/// <returns>true if drawing rectangle contains point; otherwise false.</returns>
		public bool Contains(Point pt)
		{
			return DrawRect.Contains(pt);
		}

		/// <summary>
		/// Draw the command in the current state.
		/// </summary>
		/// <param name="g">Graphics to use when drawing.</param>
		public void Draw(Graphics g)
		{
			// Cannot draw if we are not actually visible
			if (Displayed)
				InternalDraw(g, _details, DrawRect);
		}

		/// <summary>
		/// Draw the command in the current state.
		/// </summary>
		/// <param name="g">Graphics to use when drawing.</param>
		/// <param name="clipRect">Current clipping rectangle.</param>
		public void Draw(Graphics g, Rectangle clipRect)
		{
			// Cannot draw if we are not actually visible
			if (Displayed)
			{
				// Only draw if the clipping rectangle contains our draw rectangle
				if (clipRect.IntersectsWith(_drawRect))
					InternalDraw(g, _details, DrawRect);
			}
		}

		private void InternalDraw(Graphics g,
								  ICommandDetails details,
								  Rectangle drawRect)
		{
			// Tell the control to draw itself in the correct state
			_command.Draw(g, details, drawRect, _state, true);
		}
	}
}
