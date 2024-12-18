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
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Crownwood.DotNetMagic.Win32;
using Crownwood.DotNetMagic.Common;

namespace Crownwood.DotNetMagic.Controls
{
	/// <summary>
	/// Display a single line of tooltip text.
	/// </summary>
	public class PopupTooltipSingle : PopupBase
	{
		// Class fields
		private static int PADDING = 3;

		// Instance fields
		private Size _size;
		private int _textHeight;
		private string _toolText;

		/// <summary>
		/// Initialize a new instance of the PopupTooltipSingle class.
		/// </summary>
		public PopupTooltipSingle()
			: base(VisualStyle.Office2003)
		{
			InternalConstruct(new Font(SystemInformation.MenuFont, FontStyle.Regular));
		}

		/// <summary>
		/// Initialize a new instance of the PopupTooltip class.
		/// </summary>
		/// <param name="font">Font for drawing text.</param>
		public PopupTooltipSingle(Font font)
			: base(VisualStyle.Office2003)
		{
			InternalConstruct(font);
		}

		/// <summary>
		/// Initialize a new instance of the PopupTooltip class.
		/// </summary>
		/// <param name="style">Required visual style.</param>
		public PopupTooltipSingle(VisualStyle style)
			: base(style)
		{
			InternalConstruct(new Font(SystemInformation.MenuFont, FontStyle.Regular));
		}

		/// <summary>
		/// Initialize a new instance of the PopupTooltip class.
		/// </summary>
		/// <param name="style">Required visual style.</param>
		/// <param name="font">Font for drawing text.</param>
		public PopupTooltipSingle(VisualStyle style, Font font)
			: base(style)
		{
			InternalConstruct(font);
		}

		/// <summary>
		/// Gets and sets the tool text to be shown.
		/// </summary>
		public string ToolText
		{
			get { return _toolText; }
			
			set 
			{ 
				// Remove any hot key prefix
				value = value.Replace("&", "");
			
				// Remember new text
				_toolText = value; 

				// Recalculate the size and position of tooltip to match text
				CalculateSizePosition(Location);

				// Force repaint of contents
				Refresh();
			}
		}

		/// <summary>
		/// Gets and sets the text height.
		/// </summary>
		public int TextHeight
		{
			get { return _textHeight; }
			set { _textHeight = value; }
		}

		/// <summary>
		/// Make the popup visible but without taking the focus
		/// </summary>
		public virtual void ShowWithoutFocus(Point screenPos)
		{
			// Calculate window size and position
			CalculateSizePosition(screenPos);

			// Make sure the tooltip is visible
			ShowWithoutFocus();

			// Force repaint of contents
			Refresh();
		}

		/// <summary>
		/// Raises the Paint event.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			// Get size of the total client area
			Rectangle clientRect = this.ClientRectangle;

			// Shrink on right and bottom to draw correctly
			clientRect.Width--;
			clientRect.Height--;

			// Draw border around whole control
			e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, clientRect);

			using(StringFormat drawFormat = new StringFormat())
			{
				drawFormat.Alignment = StringAlignment.Near;
				drawFormat.LineAlignment = StringAlignment.Center;

				// Draw the tool tip text in remaining space
				using(SolidBrush infoBrush = new SolidBrush(ForeColor))
					e.Graphics.DrawString(ToolText, 
										  Font, 
										  infoBrush,
										  new RectangleF(PADDING, 0, (float)(Width * 1.25), Height),
										  drawFormat);
			}
		}

		/// <summary>
		/// Processes Windows messages.
		/// </summary>
		/// <param name="m">The Windows Message to process. </param>
		protected override void WndProc(ref Message m)
		{
			// We are a transparent window, let messages flow to whatever
			// is underneath us. Needed especially for the TreeControl because
			// we want clicking the mouse to select the node under the tooltip.
			if (m.Msg == (int)Win32.Msgs.WM_NCHITTEST)
			{
				// Allow actions to occur to window beneath us
				m.Result = (IntPtr)Win32.HitTest.HTTRANSPARENT;
			}
			else
				base.WndProc(ref m);
		}

		private void InternalConstruct(Font font)
		{
			// Define our background/foreground colors as tooltip ones
			BackColor = SystemColors.Info;
			ForeColor = SystemColors.InfoText;
			Font = font;

			// By default we autocalculate the text height
			_textHeight = -1;
		}

		private void CalculateSizePosition(Point screenPos)
		{
			// Calculate size required to draw the tool text
			using(Graphics g = CreateGraphics())
			{
				// Get accurate size of the tooltext
				SizeF rawSize = g.MeasureString(ToolText, Font);

				// Convert from floating point to integer
				_size = new Size((int)rawSize.Width, (int)rawSize.Height);
				
				// Do we override the height?
				if (_textHeight != -1)
				{
					// Just exact height provided
					_size.Height = _textHeight;
				}
				else
				{
					// Use height as the text plus some padding
					_size.Height += PADDING * 2;
				}

				// Add borders around the text area to get the total size
				Size total = new Size(_size.Width + PADDING * 3, _size.Height);

				// Default to no space required for shadows
				int shadow = 0;
				
				// If a shadow is showing then gets its size
				if (PopupShadow != null)
					shadow = PopupShadow.ShadowLength;

				// Check that the position allows it to be shown
				if ((screenPos.X + total.Width + shadow) > Screen.GetWorkingArea(this).Width)
					screenPos.X = Screen.GetWorkingArea(this).Width - total.Width - shadow;

				if ((screenPos.Y + total.Height + shadow) > Screen.GetWorkingArea(this).Height)
					screenPos.Y = Screen.GetWorkingArea(this).Height - total.Height - shadow;

				// Move the window without activating it (i.e. do not take focus)
				User32.SetWindowPos(this.Handle, 
									IntPtr.Zero, 
									screenPos.X, screenPos.Y, 
									total.Width, total.Height, 
									(int)Win32.SetWindowPosFlags.SWP_NOZORDER + 
									(int)Win32.SetWindowPosFlags.SWP_NOOWNERZORDER + 
									(int)Win32.SetWindowPosFlags.SWP_NOACTIVATE);
			}
		}
	}
}
