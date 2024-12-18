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
using System.ComponentModel;
using System.Diagnostics;
using Crownwood.DotNetMagic.Common;

namespace Crownwood.DotNetMagic.Controls.Command
{
	/// <summary>
	/// Implements a separator style command.
	/// </summary>
	[ToolboxItem(false)]
	public class SeparatorCommand : CommandBase
	{
		// Class constants
		private static readonly int SEPARATOR_CONSTANT_IDE = 5;
		private static readonly int SEPARATOR_CONSTANT_PLAIN = 4;
		private static readonly int SEPARATOR_CONSTANT_IDE2005 = 5;
		private static readonly int SEPARATOR_CONSTANT_OFFICE2003 = 6;

		/// <summary>
		/// Initializes a new instance of the SeparatorCommand class.
		/// </summary>
		public SeparatorCommand()
			: base(false, false)	// Separator always stretchs in both directions
		{
		}

		/// <summary>
		/// Perform appropriate action for when command is pressed.
		/// </summary>
		public override void Pressed()
		{
			// Separator cannot be pressed, so do nothing
		}		

		/// <summary>
		/// Calculate required drawing size for separator.
		/// </summary>
		/// <param name="g">Graphics reference used in calculations.</param>
		/// <param name="details">Source details needed to perform calculation.</param>
        /// <param name="topLevel">Drawing as a top level item.</param>
        /// <returns>Size of required area for command.</returns>
		public override Size CalculateDrawSize(Graphics g, ICommandDetails details, bool topLevel)
		{
			// Separator is the same size in both directions and should stretch to match the 
			// actual required height (for horizontal direction) or width (for vertical direction).
			switch(details.Style)
			{
				case VisualStyle.Plain:
					return new Size(SEPARATOR_CONSTANT_PLAIN, SEPARATOR_CONSTANT_PLAIN);
				case VisualStyle.IDE:
					return new Size(SEPARATOR_CONSTANT_IDE, SEPARATOR_CONSTANT_IDE);
				case VisualStyle.IDE2005:
					return new Size(SEPARATOR_CONSTANT_IDE2005, SEPARATOR_CONSTANT_IDE2005);
				case VisualStyle.Office2003:
					return new Size(SEPARATOR_CONSTANT_OFFICE2003, SEPARATOR_CONSTANT_OFFICE2003);
				default:
					Debug.Assert(false);
					return Size.Empty;
			}
		}

		/// <summary>
		/// Draw the command as a Separator.
		/// </summary>
		/// <param name="g">Graphics reference used in calculations.</param>
		/// <param name="details">Source details needed to perform calculation.</param>
		/// <param name="drawRect">Bounding rectangle for drawing command.</param>
		/// <param name="state">State of the command to be drawn.</param>
        /// <param name="topLevel">Drawing as a top level item.</param>
        public override void Draw(Graphics g, 
								  ICommandDetails details, 
								  Rectangle drawRect, 
								  ItemState state,
                                  bool topLevel)
		{
			// Use a helper method for perform drawing
			CommandDraw.DrawSeparatorCommand(g, 
											 details.Style, 
											 details.Direction,
											 drawRect,
											 details.SepDarkColor,
											 details.SepLightColor);
		}
		
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		public override object Clone()
		{
			// Create a new instance of this class
			SeparatorCommand copy =  new SeparatorCommand();
			
			// Make a note of the original we are cloned from
			Original = this;
			
			// Copy across the members values
			copy.UpdateInstance(this);
			
			return copy;
		}
	}
}
