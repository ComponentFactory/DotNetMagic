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
	/// Implementation that exposes the command details needed by command implementations.
	/// </summary>
	public class CommandDetails : ColorDetails, ICommandDetails
	{
		// Instance fields
		private CommandControl _control;
		
		/// <summary>
		/// Initializes a new instance of the CommandDetails class.
		/// </summary>
		/// <param name="control">Parent control used as source of information.</param>
		public CommandDetails(CommandControl control)
		{
			// Remember state
			_control = control;
		}

		/// <summary>
		/// Drawing font.
		/// </summary>
		public Font Font 
		{ 
			get { return _control.Font; } 
		}

		/// <summary>
		/// Drawing visual style.
		/// </summary>
		public override VisualStyle Style 
		{ 
			get { return _control.Style; }
		}

		/// <summary>
		/// Flow direction.
		/// </summary>
		public LayoutDirection Direction 
		{ 
			get { return _control.Direction; }
		}

		/// <summary>
		/// Image and Text should be shown.
		/// </summary>
		public ImageAndText ImageAndText 
		{ 
			get { return _control.ImageAndText; }
		}

		/// <summary>
		/// Should text be shown horizontal always.
		/// </summary>
		public bool OnlyHorizontalText 
		{ 
			get { return _control.OnlyHorizontalText; }
		}

		/// <summary>
		/// Drawing edge for text.
		/// </summary>
		public TextEdge TextEdge 
		{ 
			get { return _control.TextEdge; }
		}

        /// <summary>
        /// Gets a value indicating if all buttons have equal height of tallest button.
        /// </summary>
        public bool EqualButtonVert 
        { 
            get { return _control.EqualButtonVert; }
        }

        /// <summary>
        /// Gets a value indicating if all buttons have equal width of widest button.
        /// </summary>
        public bool EqualButtonHorz 
        { 
            get { return _control.EqualButtonHorz; }
        }

		/// <summary>
		/// Bounding rectangle for drawing.
		/// </summary>
		public Rectangle LayoutRect 
		{ 
			get 
			{ 
				// Get the client rectangle of hosting control
				Rectangle rect = _control.ClientRectangle;

				// Remove the padding border layout area.
				if (Direction == LayoutDirection.Horizontal)
				{
					rect.X += _control.Padding.Left;
					rect.Y += _control.Padding.Top;
				}
				else
				{
					rect.X += _control.Padding.Top;
					rect.Y += _control.Padding.Left;
				}
				
				rect.Width -= _control.Padding.Width;
				rect.Height -= _control.Padding.Height;

				return rect; 
			}
		}
		
		/// <summary>
		/// Control hosting commands.
		/// </summary>
		public Control HostControl 
		{ 
			get { return _control; }
		}

		/// <summary>
		/// Text color used for drawing any text strings.
		/// </summary>
		public Color TextColor
		{ 
			get { return _control.ForeColor; }
		}
	}
}
