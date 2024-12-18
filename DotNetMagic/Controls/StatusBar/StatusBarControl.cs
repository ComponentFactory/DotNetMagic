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
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.ComponentModel;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls.Command;

namespace Crownwood.DotNetMagic.Controls
{
	/// <summary>
	/// Configurable title bar for presenting user with header information.
	/// </summary>
    [ToolboxBitmap(typeof(StatusBarControl))]
    public class StatusBarControl : Panel
    {
		// Static fields
		private static int _sizingGripWidth = 16;
		private static int _sizingGripHeight = 16;

		// Instance fields
		private int _padLeft;
		private int _padRight;
		private int _padTop;
		private int _padBottom;
		private bool _sizingGrip;
		private bool _defaultBackColor;
		private bool _office2003GradBack;
		private bool _ide2005GradBack;
		private VisualStyle _style;
		private StatusPanelCollection _panels;
		private ColorDetails _colorDetails;

		/// <summary>
		/// Initializes a new instance of the StatusBarControl class.
		/// </summary>
        public StatusBarControl()
        {
			// NAG processing
			NAG.NAG_Start();

            // Prevent drawing flicker by blitting from memory in WM_PAINT
            SetStyle(ControlStyles.DoubleBuffer | 
				     ControlStyles.AllPaintingInWmPaint |
				     ControlStyles.ResizeRedraw |
					 ControlStyles.UserPaint, true);

			// Should not be allowed to select this control
			SetStyle(ControlStyles.Selectable, false);

			// Create collection to hold panels
			_panels = new StatusPanelCollection();

			// Hook into collection changes
			_panels.Inserted += new CollectionChange(OnPanelInserted);
			_panels.Removed += new CollectionChange(OnPanelRemoved);
			_panels.Clearing += new CollectionClear(OnPanelsClearing);
			_panels.Cleared += new CollectionClear(OnPanelsCleared);
			
			// Create helper for drawing themes
			_colorDetails = new ColorDetails();
			
			// Background color is defaulted
			_defaultBackColor = true;

			ResetBackColor();
			ResetStyle();
			ResetSizingGrip();
			ResetDock();
			ResetPadLeft();
			ResetPadRight();
			ResetPadTop();
			ResetPadBottom();
			ResetOffice2003GradBack();
			ResetIDE2005GradBack();
			
			// Set default height of control to be the font plus 4 pixels for panel
			// border control and also the padding at top and bottom
			this.Height = Font.Height + PadTop + PadBottom + 4;
		}

		/// <summary>
		/// Dispose of instance resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Color details has resources that need releasing
				_colorDetails.Dispose();
			}
			
			base.Dispose (disposing);
		}

		/// <summary>
		/// Gets or sets the background color for the control.
		/// </summary>
		public override Color BackColor
		{
			get { return base.BackColor; }

			set
			{
				if (value != base.BackColor)
				{
					_defaultBackColor = (value == SystemColors.Control);
					base.BackColor = value;
					Invalidate();
				}
			}
		}

		private bool ShouldSerializeBackColor()
		{
			return this.BackColor != SystemColors.Control;
		}
		
		/// <summary>
		/// Resets the BackColor property to its default value.
		/// </summary>
		public new void ResetBackColor()
		{
			BackColor = SystemColors.Control;
		}

		/// <summary>
		/// Gets and sets the docking edge for the status bar.
		/// </summary>
		[DefaultValue(typeof(DockStyle), "Bottom")]
		public new DockStyle Dock
		{
			get { return base.Dock; }
			set { base.Dock = value; }
		}

		/// <summary>
		/// Resets the Dock property to its default value.
		/// </summary>
		public void ResetDock()
		{
			Dock = DockStyle.Bottom;
		}

		/// <summary>
		/// Gets and sets how the panels are drawn.
		/// </summary>
		[Category("Appearance")]
		[Description("Determine how the panels are drawn.")]
		[DefaultValue(typeof(VisualStyle), "Office2003")]
		public VisualStyle Style
		{
			get { return _style; }
			
			set 
			{ 
				if (_style != value)
				{
					_style = value;
					_colorDetails.Style = value;

					// Get style specific panel border setting
					PanelBorder pb = (_style != VisualStyle.Plain) ? PanelBorder.Solid : PanelBorder.Sunken;

					// Set the panel style of each panel to default
					foreach(StatusPanelBorder host in Controls)
					{
						// Those that are defaulted, update
						if (host.StatusPanel.DefaultPanelBorder)
							host.StatusPanel.PanelBorder = pb;
							
						// Must force a redraw
						host.Invalidate();
						host.StatusPanel.Invalidate();
					}

					Invalidate();
				}
			}
		}

		/// <summary>
		/// Resets the Dock property to its default value.
		/// </summary>
		public void ResetStyle()
		{
			Style = VisualStyle.Office2003;
		}

		/// <summary>
		/// Gets and sets a value indicating if the background should be drawn with a gradient when using Office2003 style.
		/// </summary>
		[Category("Appearance")]
		[Description("Should the background be drawn with a gradient when using Office2003 style.")]
		[DefaultValue(false)]
		public bool Office2003GradBack
		{
			get { return _office2003GradBack; }
			
			set 
			{ 
				if (_office2003GradBack != value)
				{
					_office2003GradBack = value; 
					Invalidate();
				}
			}
		}

		/// <summary>
		/// Resets the Office2003GradBack property to its default value.
		/// </summary>
		public void ResetOffice2003GradBack()
		{
			Office2003GradBack = false;
		}

		/// <summary>
		/// Gets and sets a value indicating if the background should be drawn with a gradient when using IDE2005 style.
		/// </summary>
		[Category("Appearance")]
		[Description("Should the background be drawn with a gradient when using IDE2005 style.")]
		[DefaultValue(true)]
		public bool IDE2005GradBack
		{
			get { return _ide2005GradBack; }
			
			set 
			{ 
				if (_ide2005GradBack != value)
				{
					_ide2005GradBack = value; 
					Invalidate();
				}
			}
		}

		/// <summary>
		/// Resets the IDE2005GradBack property to its default value.
		/// </summary>
		public void ResetIDE2005GradBack()
		{
			IDE2005GradBack = true;
		}

		/// <summary>
		/// Gets and sets a value indicating if a sizing grip should be shown.
		/// </summary>
		[Category("Appearance")]
		[Description("Should a sizing grip be shown and operated.")]
		[DefaultValue(true)]
		public bool SizingGrip
		{
			get { return _sizingGrip; }
			
			set 
			{ 
				if (_sizingGrip != value)
				{
					_sizingGrip = value;
					OnRepositionPanels(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Resets the SizingGrip property to its default value.
		/// </summary>
		public void ResetSizingGrip()
		{
			SizingGrip = true;
		}

		/// <summary>
		/// Gets the collection of StatusPanel instances to be displayed.
		/// </summary>
		[Category("Appearance")]
		[Description("Collection of panels to display.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StatusPanelCollection StatusPanels
		{
			get { return _panels; }
		}
		
		/// <summary>
		/// Gets and sets the starting offset for positioning panels.
		/// </summary>
		[Category("Appearance")]
		[Description("Define starting offset for positioning panels.")]
		[DefaultValue(1)]		
		public int PadLeft
		{
			get { return _padLeft; }
			
			set
			{
				if (_padLeft != value)
				{
					_padLeft = value;
					OnRepositionPanels(this, EventArgs.Empty);
				}
			}
		}
		
		/// <summary>
		/// Resets the PadLeft property to its default value.
		/// </summary>
		public void ResetPadLeft()
		{
			PadLeft = 1;
		}

		/// <summary>
		/// Gets and sets the ending offset when positioning with spring.
		/// </summary>
		[Category("Appearance")]
		[Description("Define ending offset when positioning with spring.")]
		[DefaultValue(1)]		
		public int PadRight
		{
			get { return _padRight; }
			
			set
			{
				if (_padRight != value)
				{
					_padRight = value;
					OnRepositionPanels(this, EventArgs.Empty);
				}
			}
		}
		
		/// <summary>
		/// Resets the PadRight property to its default value.
		/// </summary>
		public void ResetPadRight()
		{
			PadRight = 1;
		}

		/// <summary>
		/// Gets and sets the starting offset from top when positioning panels.
		/// </summary>
		[Category("Appearance")]
		[Description("Define starting offset from top when positioning panels.")]
		[DefaultValue(1)]		
		public int PadTop
		{
			get { return _padTop; }
			
			set
			{
				if (_padTop != value)
				{
					_padTop = value;
					OnRepositionPanels(this, EventArgs.Empty);
				}
			}
		}
		
		/// <summary>
		/// Resets the PadTop property to its default value.
		/// </summary>
		public void ResetPadTop()
		{
			PadTop = 1;
		}

		/// <summary>
		/// Gets and sets the offset from bottom when positioning panels.
		/// </summary>
		[Category("Appearance")]
		[Description("Define offset from bottom when positioning panels.")]
		[DefaultValue(1)]		
		public int PadBottom
		{
			get { return _padBottom; }
			
			set
			{
				if (_padBottom != value)
				{
					_padBottom = value;
					OnRepositionPanels(this, EventArgs.Empty);
				}
			}
		}
		
		/// <summary>
		/// Resets the PadBottom property to its default value.
		/// </summary>
		public void ResetPadBottom()
		{
			PadBottom = 1;
		}

		/// <summary>
		/// Raises the Resize event.
		/// </summary>
		/// <param name="e">An EventArgs structure containing event data.</param>
		protected override void OnResize(EventArgs e)
		{
			// Must reposition the controls when size has changed
			PositionPanels();
			Invalidate();

			base.OnResize(e);
		}

		/// <summary>
		/// Raises the PaintBackground event.
		/// </summary>
		/// <param name="pevent">A PaintEventArgs that contains the event data.</param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			if ((_style == VisualStyle.Office2003) && _defaultBackColor)
			{
				CommandDraw.DrawGradientBackground(pevent.Graphics, 
												   this, 
												   _colorDetails.BaseColor,
												   _colorDetails.BaseColor1, 
												   _colorDetails.BaseColor2,
												   _office2003GradBack);
			}
			else if ((_style == VisualStyle.IDE2005) && _defaultBackColor)
			{
				CommandDraw.DrawGradientBackground(pevent.Graphics, 
												   this, 
												   _colorDetails.BaseColor,
												   _colorDetails.BaseColor1, 
												   _colorDetails.BaseColor2,
												   _ide2005GradBack);
			}
			else
				base.OnPaintBackground(pevent);
		}


		/// <summary>
		/// Raises the Paint event.
		/// </summary>
		/// <param name="e">A PaintEventArgs structure contained event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			// Do we need to paing a sizing grip?
			if (SizingGrip)
			{
				// Find the form we are placed on
				Form parentForm = this.FindForm();
				
				// Just in case the impossible happens
				if (parentForm != null)
				{
					// No point drawing if we are minimized or maximized
					if (parentForm.WindowState == FormWindowState.Normal)
					{
						Color darkColor;
						Color lightColor;
						
						// Should we get the background color from the color details object?
						if ((_style == VisualStyle.Office2003) && _defaultBackColor)
						{
							darkColor = _colorDetails.MenuSeparatorColor;
							lightColor = _colorDetails.MenuBackColor;
						}
						else if ((_style == VisualStyle.IDE2005) && _defaultBackColor)
						{
							darkColor = SystemColors.ControlDark;
							lightColor = _colorDetails.MenuBackColor;
						}
						else
						{
							darkColor = ControlPaint.Dark(BackColor);
							lightColor = ControlPaint.Light(BackColor);
						}
						
						// Create the two drawing pens
						using(Pen darkPen = new Pen(darkColor),
								  lightPen = new Pen(lightColor))
						{
							int right = Width - 2;
							int bottom = Height - 2;

							// Draw smallest diagonal stripe
							e.Graphics.DrawLine(darkPen, new Point(right, bottom - 1), new Point(right - 1, bottom));
							e.Graphics.DrawLine(darkPen, new Point(right, bottom - 2), new Point(right - 2, bottom));
							e.Graphics.DrawLine(lightPen, new Point(right, bottom - 3), new Point(right - 3, bottom));

							// Draw middle diagonal stripe
							e.Graphics.DrawLine(darkPen, new Point(right, bottom - 5), new Point(right - 5, bottom));
							e.Graphics.DrawLine(darkPen, new Point(right, bottom - 6), new Point(right - 6, bottom));
							e.Graphics.DrawLine(lightPen, new Point(right, bottom - 7), new Point(right - 7, bottom));

							// Draw longest diagonal stripe
							e.Graphics.DrawLine(darkPen, new Point(right, bottom - 9), new Point(right - 9, bottom));
							e.Graphics.DrawLine(darkPen, new Point(right, bottom - 10), new Point(right - 10, bottom));
							e.Graphics.DrawLine(lightPen, new Point(right, bottom - 11), new Point(right - 11, bottom));
						}
					}
				}
			}
			
			// Let base class do the standard stuff first
			base.OnPaint(e);
		}

		/// <summary>
		/// Process individual windows messages.
		/// </summary>
		/// <param name="m">Message to snoop.</param>
		protected override void WndProc(ref Message m)
		{
			// We are only interested in changing hit testing
			if (m.Msg == (int)Win32.Msgs.WM_NCHITTEST)
			{
				// Find the form we are placed on
				Form parentForm = this.FindForm();
				
				// Just in case the impossible happens
				if (parentForm != null)
				{
					// No point having a sizing handle if minimized or maximized
					if (parentForm.WindowState == FormWindowState.Normal)
					{			
						// Is there a sizing grip to hit test against?
						if (SizingGrip)
						{
							// Extract the screen based mouse position
							uint x = ((uint)m.LParam & 0x0000FFFFU);
							uint y = (((uint)m.LParam & 0xFFFF0000U) >> 16);

							// Convert to client point
							Point clientPt = PointToClient(new Point((int)x, (int)y));

							// Is this over the sizing grip area
							if ((clientPt.X > (Width - _sizingGripWidth)) &&
								(clientPt.Y > (Height - _sizingGripHeight)))
							{
								// Let the user resize the application window
								m.Result = (IntPtr)Win32.HitTest.HTBOTTOMRIGHT;

								// Exit as we do not want default processing to occur
								return;
							}
						}
					}
				}
			}

			base.WndProc(ref m);
		}

		/// <summary>
		/// Raises the HandleCreated event.
		/// </summary>
		/// <param name="e">An EventArgs structure contained event data.</param>
		protected override void OnHandleCreated(EventArgs e)
		{
			int index = 0;

			// Create all the panels only when we have been created
			foreach(StatusPanel panel in StatusPanels)
				InitializePanel(index++, panel);

			// Ask for all panels to be repositioned
			OnRepositionPanels(this, EventArgs.Empty);

			base.OnHandleCreated(e);
		}
		
		internal ColorDetails ColorDetails
		{
			get { return _colorDetails; }
		}

		private void OnPanelInserted(int index, object value)
		{
			// Do not create child controls until we are created
			if (IsHandleCreated && !IsDisposed)
			{
				// Cast to correct type
				StatusPanel panel = value as StatusPanel;

				// Create hosting border control and add as child
				InitializePanel(index, panel);

				// Ask for all panels to be repositioned
				OnRepositionPanels(this, EventArgs.Empty);
			}
		}

		private void OnPanelRemoved(int index, object value)
		{
			// Do nothing until we are created
			if (IsHandleCreated && !IsDisposed)
			{
				// Cast to correct type
				StatusPanel panel = value as StatusPanel;

				// Unhook from panel events
				panel.RequestedWidthChanged -= new EventHandler(OnRepositionPanels);
				panel.PanelBorderChanged -= new EventHandler(OnRepositionPanels);
				panel.AlignmentChanged -= new EventHandler(OnRepositionPanels);
				panel.TextChanged -= new EventHandler(OnRepositionPanels);
				panel.ImageChanged -= new EventHandler(OnRepositionPanels);
				panel.VisibleChanged -= new EventHandler(OnRepositionPanels);
				panel.AutoSizingChanged -= new EventHandler(OnRepositionPanels);
				panel.PaintBackground -= new PaintEventHandler(OnPanelPaintBackground);

				// Get hosting control from indexed position
				StatusPanelBorder host = Controls[index] as StatusPanelBorder;
				host.PaintBackground -= new PaintEventHandler(OnPanelPaintBackground);

				// Remove it from the collection of children
				Controls.Remove(host);

				// Ask for all panels to be repositioned
				OnRepositionPanels(this, EventArgs.Empty);
			}
		}

		private void OnPanelsClearing()
		{
			// Do nothing until we are created
			if (IsHandleCreated && !IsDisposed)
			{
				int count = _panels.Count;

				// Unhook from all items
				for(int i=0; i<count; i++)
				{
					_panels[i].RequestedWidthChanged -= new EventHandler(OnRepositionPanels);
					_panels[i].PanelBorderChanged -= new EventHandler(OnRepositionPanels);
					_panels[i].AlignmentChanged -= new EventHandler(OnRepositionPanels);
					_panels[i].TextChanged -= new EventHandler(OnRepositionPanels);
					_panels[i].ImageChanged -= new EventHandler(OnRepositionPanels);
					_panels[i].VisibleChanged -= new EventHandler(OnRepositionPanels);
					_panels[i].AutoSizingChanged -= new EventHandler(OnRepositionPanels);
					_panels[i].PaintBackground -= new PaintEventHandler(OnPanelPaintBackground);
					
					// Get hosting control from indexed position
					StatusPanelBorder host = Controls[i] as StatusPanelBorder;
					host.PaintBackground += new PaintEventHandler(OnPanelPaintBackground);
				}
			}
		}

		private void OnPanelsCleared()
		{
			// Do not try and remove child controls until we have been created
			if (IsHandleCreated && !IsDisposed)
			{
				int count = _panels.Count;

				// Remove all the children
				Controls.Clear();

				// Ask for all panels to be repositioned
				OnRepositionPanels(this, EventArgs.Empty);
			}
		}

		private void InitializePanel(int index, StatusPanel panel)
		{
			// Create a hosting panel for the new panel
			StatusPanelBorder host = new StatusPanelBorder(this);
			host.PaintBackground += new PaintEventHandler(OnPanelPaintBackground);

			// Assign across the panel
			host.StatusPanel = panel;

			// Set correct default border panel (for those defauled)
			if (host.StatusPanel.DefaultPanelBorder)
				host.StatusPanel.PanelBorder = (_style != VisualStyle.Plain) ? PanelBorder.Solid : PanelBorder.Sunken;

			// Add host to the collection of control
			Controls.Add(host);

			// Move to correct index
			Controls.SetChildIndex(host, index);

			// Hook into panel events
			panel.RequestedWidthChanged += new EventHandler(OnRepositionPanels);
			panel.PanelBorderChanged += new EventHandler(OnRepositionPanels);
			panel.AlignmentChanged += new EventHandler(OnRepositionPanels);
			panel.TextChanged += new EventHandler(OnRepositionPanels);
			panel.ImageChanged += new EventHandler(OnRepositionPanels);
			panel.VisibleChanged += new EventHandler(OnRepositionPanels);
			panel.AutoSizingChanged += new EventHandler(OnRepositionPanels);
			panel.PaintBackground += new PaintEventHandler(OnPanelPaintBackground);
		}

		private void OnPanelPaintBackground(object sender, PaintEventArgs pevent)
		{
			if ((_style == VisualStyle.Office2003) && _defaultBackColor)
			{
				CommandDraw.DrawGradientBackground(pevent.Graphics, 
												   sender as Control, 
												   _colorDetails.BaseColor,
												   _colorDetails.BaseColor1, 
												   _colorDetails.BaseColor2,
												   _office2003GradBack);
			}
			else if ((_style == VisualStyle.IDE2005) && _defaultBackColor)
			{
				CommandDraw.DrawGradientBackground(pevent.Graphics, 
												   sender as Control, 
												   _colorDetails.BaseColor,
												   _colorDetails.BaseColor1, 
												   _colorDetails.BaseColor2,
												   _ide2005GradBack);
			}

		}
		
		private void OnRepositionPanels(object sender, EventArgs e)
		{
			// Do nothing until we have been created
			if (IsHandleCreated && !IsDisposed)
			{
				// Ask for all panels to be repositioned
				PositionPanels();

				// Show drawing changes straight away
				Invalidate();

				// Redraw all the child panels and panel border as well
				foreach(Control child in Controls)
				{
					child.Invalidate();

					foreach(Control child2 in child.Controls)
						child2.Invalidate();
				}
			}
		}

		private void PositionPanels()
		{
			// Do nothing until we have been created
			if (IsHandleCreated && !IsDisposed)
			{
				// Scan list to find total width of non-spring panels
				int totalRequested = 0;

				// Count number of visible panels to position
				int panels = 0;

				// Count number of those with spring setting
				int springs = 0;

				// Find required width for sizing grip
				int sizingGripWidth = 0;
				
				// Find the form we are placed on
				Form parentForm = this.FindForm();
				
				// Just in case the impossible happens
				if (parentForm != null)
				{
					// No point having a sizing handle if minimized or maximized
					if (parentForm.WindowState == FormWindowState.Normal)
						sizingGripWidth = SizingGrip ? _sizingGripWidth : 0;
				}

				// Find width of each panel in turn
				foreach(StatusPanelBorder host in Controls)
				{
					// Only position the panel if it is visible
					if (host.StatusPanel.Visible)
					{
						switch(host.StatusPanel.AutoSizing)
						{
							case StatusBarPanelAutoSize.None:
								// Use its requested size plus 4 pixel border
								totalRequested += 4 + host.StatusPanel.RequestedWidth;
								break;
							case StatusBarPanelAutoSize.Contents:
								// Find width needed to draw contents plus 4 pixel border
								totalRequested += 4 + host.StatusPanel.ContentsWidth;
								break;
							case StatusBarPanelAutoSize.Spring:
								// Count total number needing spring
								springs++;
								break;
						}

						// Count number of visible panels to be positioned
						panels++;
					}
				}

				// Add the spacing gaps between panels (if there are any)
				totalRequested += panels > 0 ? (panels - 1) * 2 : 0;

				// Find remaining space left over for springs
				int springSpace = (Width - PadLeft - PadRight - sizingGripWidth) - totalRequested;

				// Cannot allocate less than zero for size of springs
				if (springSpace < 0)
					springSpace = 0;

				int eachSpring = 0;

				// Are there any springs at all?
				if (springs > 0)
				{
					// Limit check, cannot allocate negative space
					if (springSpace > 0)
					{
						// Divide remainder evenly between springs
						eachSpring = springSpace / springs;
					}
				}

				int x = PadLeft;
				int top = PadTop;
				int height = Height - PadTop - PadBottom;
				
				// Position each hosting control in turn
				foreach(StatusPanelBorder host in Controls)
				{
					// Reflect the panel visibility in the hosting panel
					host.Visible = host.StatusPanel.Visible;
				
					// Only position the panel if it is visible
					if (host.StatusPanel.Visible)
					{
						int panelWidth = 0;

						// Find width of this panel
						switch(host.StatusPanel.AutoSizing)
						{
							case StatusBarPanelAutoSize.None:
								// Use its requested size
								panelWidth = host.StatusPanel.RequestedWidth + 4;
								break;
							case StatusBarPanelAutoSize.Contents:
								// Find width needed to draw contents
								panelWidth = host.StatusPanel.ContentsWidth + 4;
								break;
							case StatusBarPanelAutoSize.Spring:
								// Decrement the number of springs processed
								springs--;

								// Reduce total allocated to strings
								springSpace -= eachSpring;

								// If the last spring to be positioned
								if (springs == 0)
								{
									// Make sure last spring gets any remainders from division earlier
									panelWidth = eachSpring + springSpace;
								}
								else
								{
									// Use the spring value
									panelWidth = eachSpring;
								}
								break;
						}

						// Position this panel
						host.SetBounds(x, top, panelWidth, height);

						// Move across starting position
						x += panelWidth + 2;
					}
				}
			}
		}
	}
}
