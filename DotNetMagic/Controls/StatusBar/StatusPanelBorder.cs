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
using System.IO;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using Crownwood.DotNetMagic.Common;

namespace Crownwood.DotNetMagic.Controls
{
	/// <summary>
	/// Represents a single StatusPanelBorder inside the StatusBarControl.
	/// </summary>
    [ToolboxItem(false)]
    public class StatusPanelBorder : Panel
    {
		// Instance fields
		private StatusBarControl _parent;
		private StatusPanel _statusPanel;

		/// <summary>
		/// Occurs when the AutoSize property changes.
		/// </summary>
		public event PaintEventHandler PaintBackground;

		/// <summary>
		/// Initializes a new instance of the StatusPanel class.
		/// </summary>
		/// <param name="parent">Back reference to recover drawing information.</param>
        public StatusPanelBorder(StatusBarControl parent)
        {
            // Prevent drawing flicker by blitting from memory in WM_PAINT
            SetStyle(ControlStyles.DoubleBuffer | 
				     ControlStyles.AllPaintingInWmPaint |
					 ControlStyles.UserPaint, true);

			// Remember back pointer
			_parent = parent;

			// Not hosting a panel to start with
			_statusPanel = null;
		}

		/// <summary>
		/// Gets and sets the hosted StatusPanel
		/// </summary>
		public StatusPanel StatusPanel
		{
			get { return _statusPanel; }

			set
			{
				if (_statusPanel != value)
				{
					// Remove any existing control
					if (_statusPanel != null)
						Controls.Remove(_statusPanel);

					// Assign across new reference
					_statusPanel = value;

					// Add new control
					if (_statusPanel != null)
					{
						// Make sure the control is positioned exactly and not using dock style
						_statusPanel.Dock = DockStyle.None;

						Controls.Add(_statusPanel);
					}

					// Set correct position for the child control
					PositionChild();
				}
			}
		}

		/// <summary>
		/// Raises the Resize event.
		/// </summary>
		/// <param name="e">An EventArgs structure containing event data.</param>
		protected override void OnResize(EventArgs e)
		{
			// Make sure child control is positioned correctly
			PositionChild();
			Invalidate();
			base.OnResize(e);
		}

		/// <summary>
		/// Raises the Paint event.
		/// </summary>
		/// <param name="pevent">A PaintEventArgs structure contained event data.</param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			base.OnPaintBackground(pevent);

			// Raise any required events for painting			
			if (PaintBackground != null)
				PaintBackground(this, pevent);
		}

		/// <summary>
		/// Raises the Paint event.
		/// </summary>
		/// <param name="e">A PaintEventArgs structure contained event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			// Let base class do the standard stuff first
			base.OnPaint(e);

			if (_statusPanel != null)
			{
				PanelBorder border = _statusPanel.PanelBorder;

				// Paint border according to requested style
				switch(border)
				{
					case PanelBorder.Sunken:
						if ((_parent.Style == VisualStyle.Office2003) || (_parent.Style == VisualStyle.IDE2005))
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _parent.ColorDetails.MenuSeparatorColor, ButtonBorderStyle.Inset);
						else
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ControlPaint.Light(BackColor), ButtonBorderStyle.Inset);
						break;
					case PanelBorder.Raised:
						if ((_parent.Style == VisualStyle.Office2003) || (_parent.Style == VisualStyle.IDE2005))
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _parent.ColorDetails.MenuSeparatorColor, ButtonBorderStyle.Outset);
						else
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ControlPaint.Light(BackColor), ButtonBorderStyle.Outset);
						break;
					case PanelBorder.Dotted:
						if (_parent.Style == VisualStyle.IDE)
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ControlPaint.Light(ControlPaint.Dark(BackColor)), ButtonBorderStyle.Dotted);
						else if ((_parent.Style == VisualStyle.Office2003) || (_parent.Style == VisualStyle.IDE2005))
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _parent.ColorDetails.MenuSeparatorColor, ButtonBorderStyle.Dotted);
						else
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ControlPaint.Dark(BackColor), ButtonBorderStyle.Dotted);
						break;
					case PanelBorder.Dashed:
						if (_parent.Style == VisualStyle.IDE)
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ControlPaint.Light(ControlPaint.Dark(BackColor)), ButtonBorderStyle.Dashed);
						else if ((_parent.Style == VisualStyle.Office2003) || (_parent.Style == VisualStyle.IDE2005))
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _parent.ColorDetails.MenuSeparatorColor, ButtonBorderStyle.Dashed);
						else
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ControlPaint.Dark(BackColor), ButtonBorderStyle.Dashed);
						break;
					case PanelBorder.Solid:
						if (_parent.Style == VisualStyle.IDE)
						{
							using(Pen borderPen = new Pen(ControlPaint.Light(ControlPaint.Dark(BackColor))))
								e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
						}
						else if ((_parent.Style == VisualStyle.Office2003) || (_parent.Style == VisualStyle.IDE2005))
						{
							using(Pen borderPen = new Pen(_parent.ColorDetails.MenuSeparatorColor))
								e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
						}
						else
							ControlPaint.DrawBorder(e.Graphics, ClientRectangle, ControlPaint.Dark(BackColor), ButtonBorderStyle.Solid);
						break;
				}
			}
		}

		private void PositionChild()
		{
			if (_statusPanel != null)
				_statusPanel.SetBounds(2, 2, Width - 4, Height - 4);
		}
    }
}
