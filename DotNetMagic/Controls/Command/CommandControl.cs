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
using System.Drawing.Design;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using Crownwood.DotNetMagic.Win32;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls;

namespace Crownwood.DotNetMagic.Controls.Command
{
	/// <summary>
	/// Base class for controls that expose commands.
	/// </summary>
	[ToolboxItem(false)]
	public class CommandControl : UserControl, ISupportInitialize
	{
        // Class constants
        private static readonly int WM_RELAYOUT = 0x0401;

		// Instance fields - Exposed Properties
		private Padding _padding;
		private VisualStyle _style;
		private LayoutDirection _direction;
		private TextEdge _textEdge;
		private ImageAndText _imageAndText;
        private bool _topLevel;
		private bool _enableAutoSize;
		private bool _equalButtonVert;
        private bool _equalButtonHorz;
        private bool _office2003GradBack;
		private bool _ide2005GradBack;
		private bool _onlyHorizontalText;
		private CommandBaseCollection _externals;
		private CommandBaseCollection _internals;

		// Instance fields - Internal state
		private int _initCount;
        private bool _mouseOver;
        private bool _layoutRequired;
		private Timer _hoverTimer;
		private Timer _updateTimer;
		private Point _hoverPoint;
		private CommandState _tooltipCmdState;
		private PopupTooltipSingle _popupTooltip;
		private ILayoutEngine _engine;
		private CommandDetails _details;
		private CommandStateCollection _states;
		
		// Instance fields - Current command/state/mouse
		private bool _mouseCapture;
		private MouseButtons _mouseDownButton;
		private CommandState _currentCmdState;

		/// <summary>
		/// Initializes a new instance of the CommandControl class.
		/// </summary>
		public CommandControl()
		{
			// We need double buffering for smooth drawing
			SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.AllPaintingInWmPaint |
					 ControlStyles.DoubleBuffer |
					 ControlStyles.UserPaint |
					 ControlStyles.ResizeRedraw, true);
					 
			// User cannot select (set focus to)
			SetStyle(ControlStyles.Selectable, false);

			// Create objects, define initial internal state
			InternalConstruct();

			// Generated code for attaching to our own events
			InitializeComponent();
			
			// Default exposed properties
			BeginInit();
			ResetStyle();
			ResetDirection();
			ResetTextEdge();
			ResetImageAndText();
			ResetDock();
			ResetPadding();
            ResetTopLevel();
            ResetEqualButtonVert();
            ResetEqualButtonHorz();
			ResetHoverTimeout();
			ResetUpdateTimeout();
			ResetEnableAutoUpdate();
			ResetEnableAutoSize();
			ResetOffice2003GradBack();
			ResetIDE2005GradBack();
			ResetOnlyHorizontalText();
			EndInit();
		}

		private void InternalConstruct()
		{
			// Create internal state objects
			_details = new CommandDetails(this);
			_engine = new SingleLayoutEngine();
			_states = new CommandStateCollection();
			_padding = new Padding();
			
			// Define state
			_initCount = 0;
			_layoutRequired = false;
			_mouseDownButton = MouseButtons.None;
			_mouseCapture = false;
			_currentCmdState = null;
			_tooltipCmdState = null;

			// Hook into padding changed events
			_padding.PaddingChanged += new EventHandler(OnPaddingChanged);

			// Create exposed/internal collections of commands
			_externals = new CommandBaseCollection();
			_internals = new CommandBaseCollection();

			// Hook into command collection modifications
			_externals.Clearing += new CollectionClear(OnCommandsClearing);
            _externals.Cleared += new CollectionClear(OnCommandsCleared);
            _externals.Inserted += new CollectionChange(OnCommandInserted);
			_externals.Removed += new CollectionChange(OnCommandRemoved);
			_internals.Clearing += new CollectionClear(OnCommandsClearing);
            _internals.Cleared += new CollectionClear(OnCommandsCleared);
            _internals.Inserted += new CollectionChange(OnCommandInserted);
			_internals.Removed += new CollectionChange(OnCommandRemoved);
			
			// Need a timer so that when the mouse hovers we can show tooltips
			_hoverTimer = new Timer();
			_hoverTimer.Tick += new EventHandler(OnMouseHoverTick);
		
			// Need a timer so that we can send updates to top level commands
			_updateTimer = new Timer();
			_updateTimer.Tick += new EventHandler(OnUpdateTick);
		}

		/// <summary>
		/// Signals the object that initialization is starting.
		/// </summary>
		public void BeginInit()
		{
			// Use counter in case nested called to init are made
			_initCount++;
		}

		/// <summary>
		/// Signals the object that initialization is complete.
		/// </summary>
		public void EndInit()
		{
			// Use counter in case nested called to init are made
			_initCount--;

			// Finish initializing?
			if (_initCount == 0)
				LayoutRequired();
		}
		
		/// <summary>
		/// Gets or sets the background color for the control.
		/// </summary>
		public new Color BackColor
		{
			get { return base.BackColor; }
			
			set
			{
				// Tell base class to store the new colour
				base.BackColor = value;
				
				// Tell the details handler the background color is not defaulted
				_details.DefineBaseColors(base.BackColor);
				_details.DefaultBaseColor = false;
			}
		}
		
		/// <summary>
		/// Resets the Direction property to its default value.
		/// </summary>
		public new void ResetBackColor()
		{
			// Define the new background color
			base.ResetBackColor();
			
			// Tell detail handler that the background is defaulted
			_details.DefineBaseColors(base.BackColor);
			_details.DefaultBaseColor = true;
		}

		/// <summary>
		/// Gets or sets the visual style of the control.
		/// </summary>
		[Category("Appearance")]
		[Description("Indicates which visual style to draw with.")]
		[DefaultValue(typeof(VisualStyle), "Office2003")]
		public VisualStyle Style
		{
			get { return _style; }

			set
			{
				if (_style != value)
				{
					_style = value;
					LayoutRequired();
				}
			}
		}

		/// <summary>
		/// Resets the Style property to its default value.
		/// </summary>
		public void ResetStyle()
		{
			Style = VisualStyle.Office2003;
		}

		/// <summary>
		/// Gets or sets the direction of commands.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(typeof(LayoutDirection), "Horizontal")]
		public LayoutDirection Direction
		{
			get { return _direction; }

			set
			{
				if (_direction != value)
				{
					_direction = value;
					LayoutRequired();
				}
			}
		}

		/// <summary>
		/// Resets the Direction property to its default value.
		/// </summary>
		public void ResetDirection()
		{
			Direction = LayoutDirection.Horizontal;
		}

		/// <summary>
		/// Gets or sets the text edge used for commands displaying text.
		/// </summary>
		[Category("Appearance")]
		[Description("Determines where text is drawn relative to any image.")]
		[DefaultValue(typeof(TextEdge), "Right")]
		public TextEdge TextEdge
		{
			get { return _textEdge; }

			set
			{
				if (_textEdge != value)
				{
					_textEdge = value;
					LayoutRequired();
				}
			}
		}

		/// <summary>
		/// Resets the TextEdge property to its default value.
		/// </summary>
		public void ResetTextEdge()
		{
			TextEdge = TextEdge.Right;
		}
		
		/// <summary>
		/// Gets or sets the showing of image and text.
		/// </summary>
		[Category("Appearance")]
		[Description("Determines if the image and text are drawn.")]
		[DefaultValue(typeof(ImageAndText), "Both")]
		public ImageAndText ImageAndText
		{
			get { return _imageAndText; }

			set
			{
				if (_imageAndText != value)
				{
					_imageAndText = value;
					LayoutRequired();
				}
			}
		}

		/// <summary>
		/// Resets the ImageAndText property to its default value.
		/// </summary>
		public void ResetImageAndText()
		{
			ImageAndText = ImageAndText.Both;
		}

		/// <summary>
		/// Gets the collection of commands.
		/// </summary>
		[Category("Behavior")]
		[Description("Collection of commands to manage.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("Crownwood.DotNetMagic.Controls.Command.CommandCollectionEditor", typeof(UITypeEditor))]
		[Localizable(true)]
		public CommandBaseCollection Commands
		{
			get { return _externals; }
		}

		/// <summary>
		/// Gets or sets which edge of the parent container a control is docked to.
		/// </summary>
		[DefaultValue(typeof(DockStyle), "Top")]
		public new DockStyle Dock
		{
			get { return base.Dock; }

			set
			{
				if (base.Dock != value)
				{
					base.Dock = value;

					// Update the direction to reflect docking position
					switch(value)
					{
						case DockStyle.Top:
						case DockStyle.Bottom:
							_direction = LayoutDirection.Horizontal;
							break;
						case DockStyle.Left:
						case DockStyle.Right:
							_direction = LayoutDirection.Vertical;
							break;
						case DockStyle.None:
							_direction = LayoutDirection.Horizontal;
							break;
					}

					LayoutRequired();
				}
			}
		}

		/// <summary>
		/// Resets the Dock property to its default value.
		/// </summary>
		public void ResetDock()
		{
			Dock = DockStyle.Top;
		}

		/// <summary>
		/// Gets and sets the padding around the drawing area.
		/// </summary>
		[Category("Appearance")]
		[Description("Determines size of border around commmands area.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Padding Padding
		{
			get { return _padding; }
		}

		/// <summary>
		/// Resets the Padding property to its default value.
		/// </summary>
		public void ResetPadding()
		{
			_padding.ResetTop();
			_padding.ResetBottom();
			_padding.ResetLeft();
			_padding.ResetRight();
		}
		
        /// <summary>
        /// Gets and sets a value indicating if all buttons have equal height of tallest button.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if all buttons have equal height of tallest button.")]
        [DefaultValue(true)]
        public bool EqualButtonVert
        {
            get { return _equalButtonVert; }

            set
            {
                if (_equalButtonVert != value)
                {
                    _equalButtonVert = value;
                    LayoutRequired();
                }
            }
        }

        /// <summary>
        /// Resets the EqualButtonVert property to its default value.
        /// </summary>
        public void ResetEqualButtonVert()
        {
            EqualButtonVert = true;
        }

        /// <summary>
        /// Gets and sets a value indicating if all buttons have equal width of widest button.
        /// </summary>
        [Category("Behavior")]
        [Description("Indicates if all buttons have equal width of widest button.")]
        [DefaultValue(false)]
        public bool EqualButtonHorz
        {
            get { return _equalButtonHorz; }

            set
            {
                if (_equalButtonHorz != value)
                {
                    _equalButtonHorz = value;
                    LayoutRequired();
                }
            }
        }

        /// <summary>
        /// Resets the EqualButtonVert property to its default value.
        /// </summary>
        public void ResetEqualButtonHorz()
        {
            EqualButtonHorz = false;
        }

		/// <summary>
		/// Gets and sets the timeout in milliseconds for showing tooltips.
		/// </summary>
		[Category("Behavior")]
		[Description("Number of milliseconds between mouse hovering and showing tooltips.")]
		[DefaultValue(500)]
		public int HoverTimeout
		{
			get { return _hoverTimer.Interval; }
			set { _hoverTimer.Interval = value; }
		}

		/// <summary>
		/// Resets the HoverTimeout property to its default value.
		/// </summary>
		public void ResetHoverTimeout()
		{
			HoverTimeout = 500;
		}

		/// <summary>
		/// Gets and sets the milliseconds between update handlers for top level commands being fired.
		/// </summary>
		[Category("Behavior")]
		[Description("Number of milliseconds between update handlers for top level commands being fired.")]
		[DefaultValue(500)]
		public int UpdateTimeout
		{
			get { return _updateTimer.Interval; }
			set { _updateTimer.Interval = value; }
		}

		/// <summary>
		/// Resets the UpdateTimeout property to its default value.
		/// </summary>
		public void ResetUpdateTimeout()
		{
			UpdateTimeout = 500;
		}

		/// <summary>
		/// Gets and sets a value indicating if updates be fired automatically for top level commands.
		/// </summary>
		[Category("Behavior")]
		[Description("Should updates be fired automatically for top level commands.")]
		[DefaultValue(true)]
		public bool EnableAutoUpdate
		{
			get { return _updateTimer.Enabled; }
			set { _updateTimer.Enabled = value; }
		}
	
		/// <summary>
		/// Resets the EnableAutoUpdate property to its default value.
		/// </summary>
		public void ResetEnableAutoUpdate()
		{
			EnableAutoUpdate = true;
		}

		/// <summary>
		/// Gets and sets a value indicating if the control should be sized automatically to match contents.
		/// </summary>
		[Category("Behavior")]
		[Description("Should the control be sized automatically to match contents.")]
		[DefaultValue(true)]
		public bool EnableAutoSize
		{
			get { return _enableAutoSize; }
			set { _enableAutoSize = value; }
		}
	
		/// <summary>
		/// Resets the EnableAutoSize property to its default value.
		/// </summary>
		public void ResetEnableAutoSize()
		{
			EnableAutoSize = true;
		}
		
		/// <summary>
		/// Gets and sets a value indicating if the background should be drawn with a gradient when using Office2003 style.
		/// </summary>
		[Category("Appearance")]
		[Description("Should the background be drawn with a gradient when using Office2003 style.")]
		[DefaultValue(true)]
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
			Office2003GradBack = true;
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
		/// Gets and sets a value indicating if the text should be drawn horizontal even when showing in vertical orientation..
		/// </summary>
		[Category("Appearance")]
		[Description("Should the text be drawn horizontal even when showing in vertical orientation.")]
		[DefaultValue(false)]
		public bool OnlyHorizontalText
		{
			get { return _onlyHorizontalText; }
			
			set 
			{ 
				if (_onlyHorizontalText != value)
				{
					_onlyHorizontalText = value; 
                    LayoutRequired();
				}
			}
		}

		/// <summary>
		/// Resets the OnlyHorizontalText property to its default value.
		/// </summary>
		public void ResetOnlyHorizontalText()
		{
			OnlyHorizontalText = false;
		}

		/// <summary>
		/// Calculate the ideal size needed to show exactly the commands.
		/// </summary>
		/// <returns>Ideal size unless still initializing control, in which case Size.Empty</returns>
		public Size CalculateIdealSize()
		{
			// If we are not initializing then...
			if (_initCount == 0)
			{
				// Need to calculate the rectangle that exactly fits the commands
				Rectangle idealRectangle = _engine.FindIdealSize(_states, _details);

				// Increase by padding area
				if (Direction == LayoutDirection.Horizontal)
				{
					idealRectangle.Width += Padding.Width;
					idealRectangle.Height += Padding.Height;
				}
				else
				{
					idealRectangle.Width += Padding.Height;
					idealRectangle.Height += Padding.Width;
				}
				
				// Now we need to relayout again using available size, as the FindIdealSize
				// will have updated internal sizing of commands which needs to be undone.
				LayoutControl();
				
				return idealRectangle.Size;
			}
			else
				return Size.Empty;
		}

		/// <summary>
		/// Returns the display rectangle of the provided command instance.
		/// </summary>
		/// <param name="c">Command instance.</param>
		/// <returns>Display rectangle if found; otherwise Rectangle.Empty.</returns>
		public Rectangle RectFromCommand(CommandBase c)
		{
			// Default the returned rectangle
			Rectangle ret = Rectangle.Empty;

			// Does the command exist in our command collection?
			if (Commands.Contains(c))
			{
				// Find the index of the command
				int index = Commands.IndexOf(c);

				// Get the corresponding command state
				CommandState state = _states[index];

				// Only interested if the command is currently displayed
				if (state.Displayed)
				{
					// Use the stored display rectangle
					ret = state.DrawRect;
				}
			}

			return ret;
		}

		/// <summary>
		/// Update all the commands.
		/// </summary>
		public void UpdateAll()
		{
			// Get each external command to update its state
			foreach(CommandBase cmd in _externals)
				cmd.PerformUpdate();
		}

        /// <summary>
        /// Gets and sets a value indicating if commands should be drawn as if at top level.
        /// </summary>
        [Browsable(false)]
        [DefaultValue(true)]
        internal protected bool TopLevel
        {
            get { return _topLevel; }

            set
            {
                if (_topLevel != value)
                {
                    _topLevel = value;
                    LayoutRequired();
                }
            }
        }

        /// <summary>
        /// Resets the Padding property to its default value.
        /// </summary>
        internal protected void ResetTopLevel()
        {
            TopLevel = true;
        }

        /// <summary>
		/// Raises the Resize event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnResize(EventArgs e)
		{
			// Remember to call the base class
			base.OnResize(e);

			// Reapply the layout algorithm for new control size
			LayoutRequired();
		}

		/// <summary>
		/// Releases the unmanaged resources used by the Container and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false otherwise.</param>
		protected override void Dispose(bool disposing)
		{
			// Stop the timers
			_updateTimer.Stop();
			_hoverTimer.Stop();

			// Shutdown the timers
			_updateTimer.Dispose();
			_hoverTimer.Dispose();

			base.Dispose(disposing);
		}
        
		/// <summary>
		/// Raises the PaintBackground event.
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data.</param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			// Do nothing, it all happens in the paint.
		}

		/// <summary>
		/// Raises the Paint event.
		/// </summary>
		/// <param name="e">A PaintEventArgs that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			// Do we need to layout controls again?
			if (_layoutRequired)
				LayoutControl();
		
			if (_details.DefaultBaseColor && (_style == VisualStyle.Office2003))
			{
				CommandDraw.DrawGradientBackground(e.Graphics, 
												   this, 
												   _details.BaseColor, 
												   _details.BaseColor1, 
												   _details.BaseColor2,
												   _office2003GradBack);
			}
			else if (_details.DefaultBaseColor && (_style == VisualStyle.IDE2005))
			{
				CommandDraw.DrawGradientBackground(e.Graphics, 
												   this, 
												   _details.BaseColor, 
												   _details.BaseColor1, 
												   _details.BaseColor2,
												   _ide2005GradBack);
			}
			else
			{
				// Draw the entire clipping rectangle in require base color
				using(SolidBrush backBrush = new SolidBrush(_details.BaseColor))
					e.Graphics.FillRectangle(backBrush, e.ClipRectangle);
			}

			// Ask each command to draw itself
			foreach(CommandState state in _states)
				state.Draw(e.Graphics, e.ClipRectangle);
		}

		/// <summary>
		/// All commands are being removed.
		/// </summary>
		protected virtual void OnCommandsClearing()
		{
			// Unhook events from all commands
			foreach(CommandBase cmd in _externals)
				cmd.StatusChanged -= new EventHandler(OnCommandStatusChanged);	

			// Mimic change into the state collection
			_states.Clear();
		}

        /// <summary>
        /// All commands have been removed
        /// </summary>
        protected virtual void OnCommandsCleared()
        {
            // Reflect change in presentation immediately
            LayoutRequired();
        }
	
        /// <summary>
		/// A new command has been inserted.
		/// </summary>
		/// <param name="index">Index position of new command.</param>
		/// <param name="value">Command that has been inserted.</param>
		protected virtual void OnCommandInserted(int index, object value)
		{
			// Cast to correct type
			CommandBase cmd = value as CommandBase;
		
			// Mimic change into the state collection
			_states.Insert(index, new CommandState(_details, cmd));
			
			// Need to know when the state of the command changes
			cmd.StatusChanged += new EventHandler(OnCommandStatusChanged);	
        
            // Reflect change in presentation immediately
            LayoutRequired();
        }

		/// <summary>
		/// An existing command has been removed.
		/// </summary>
		/// <param name="index">Index position of removed command.</param>
		/// <param name="value">Command that has been removed.</param>
		protected virtual void OnCommandRemoved(int index, object value)
		{
			// Cast to correct type
			CommandBase cmd = value as CommandBase;

			// Unhook from command events
			cmd.StatusChanged -= new EventHandler(OnCommandStatusChanged);	

			// Mimic change into the state collection
			_states.RemoveAt(index);

            // Reflect change in presentation immediately
            LayoutRequired();
        }

		/// <summary>
		/// Status change has occured in a command.
		/// </summary>
		/// <param name="sender">Source command.</param>
		/// <param name="e">Data associated with event.</param>
		protected void OnCommandStatusChanged(object sender, EventArgs e)
		{
			// Change in command status, so must layout control again
			LayoutRequired();
		}

		/// <summary>
		/// Note that commands need to be layed out again.
		/// </summary>
		protected void LayoutRequired()
		{
			if (!this.IsDisposed)
			{
				// Remember that layout is needed before painting
				_layoutRequired = true;

				// Have we been created yet?
				if (this.Handle != IntPtr.Zero)
				{
					// Using invalidate will not cause a repaint, so post a message instead
					User32.PostMessage(this.Handle, WM_RELAYOUT, 0, 0);
				}     
	           
				// Cause display to be repainted, this might cause the layout before the message
				// but in cases where the client area is zero sized then the post message acts as
				// a backup and ensures that new commands increase the client size.
				Invalidate();
			}
		}

		/// <summary>
		/// Layout the collection of commands.
		/// </summary>
		protected void LayoutControl()
		{
			// If we are not initializing then...
			if (_initCount == 0)
			{
				// Need to recalculate the layout of commands using defined layout engine
				Rectangle usedRectangle = _engine.LayoutCommands(_states, _details);

				// Do we automatically resize to match size of contents?
				if (EnableAutoSize)
				{
					// Increase actual control size by padding area
					if (Direction == LayoutDirection.Horizontal)
					{
						usedRectangle.Width += Padding.Width;
						usedRectangle.Height += Padding.Height;
					}
					else
					{
						usedRectangle.Width += Padding.Height;
						usedRectangle.Height += Padding.Width;
					}

					// Apply the needed size according to docking style
					switch(this.Dock)
					{
						case DockStyle.Top:
						case DockStyle.Bottom:
							this.Height = usedRectangle.Height;
							break;
						case DockStyle.Left:
						case DockStyle.Right:
							this.Width = usedRectangle.Width;
							break;
						case DockStyle.None:
							this.Height = usedRectangle.Height;
							this.Width = usedRectangle.Width;
							break;
					}
				}
				
				// Reset need to layout commands
				_layoutRequired = false;
			}
		}
		
		/// <summary>
		/// Raises the MouseEnter event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnMouseEnter(EventArgs e)
		{
            // Mouse is currently over the control
            _mouseOver = true;
		}

		/// <summary>
		/// Raises the MouseMove event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			// Handle mouse movement and tooltips
			TooltipMove();

            // Find the command the mouse is over
            CommandState commandOver = CommandStateFromPoint(new Point(e.X, e.Y));

			// If mouse is not captured then hot track whatever item is under mouse
            if (!_mouseCapture)
            {
				// Cannot hot track if out application is not active
				Form parentForm = FindForm();
				if ((parentForm != null) && !parentForm.ContainsFocus)
					commandOver = null;
            
				// Change state to hot tracking the new command
				MakeStateHotTrack(commandOver);
            }
            else
            {
	            // Make sure the captured command matches the mouse position
		        MakeCapturedMatchMouse(commandOver);
            }
		}
		
		/// <summary>
		/// Raises the MouseDown event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
            Point pt = new Point(e.X, e.Y);
			
			// Do we currently have a command open?
			if (IsCurrentCommandOpen)
			{
				// Then need to close it up 
				RevertCurrentCommandState();
			}			

			// If we have not captured the mouse
			if (!_mouseCapture)
			{
				// Remove any tooltip
				KillTooltip();

				// We can only open or press a command with the left mouse
				if (e.Button == MouseButtons.Left)
				{
					// Find the command the mouse is over
					CommandState commandDown = CommandStateFromPoint(pt);

					// If mouse down occurs over a command
					if (commandDown != null)
					{
						// Can the target command be opened?
						if (CanCommandOpen(commandDown))
						{
							// Yes, so ask command to open itself
							MakeStateOpen(commandDown, pt);
						}
						else
						{
							// No, so change state to pressed
							MakeStatePressed(commandDown, pt);

							// Remember which button caused the capture
							_mouseDownButton = e.Button;

							// Remember we have captured the mouse
							_mouseCapture = true;
						}
					}
				}
			}
		}
		
		/// <summary>
		/// Raises the MouseUp event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			// Do have logical capture of mouse?
			if (_mouseCapture)
			{
				// Are we waiting for this button to go up?
				if (e.Button == _mouseDownButton)
				{
					// We have no longer captured the mouse
					_mouseCapture = false;			
				
					// Remove capture manually as we are not calling base class to do it for us
					Capture = false;

					// Revert the current command state and call 'Pressed' on command
					RevertCurrentMouseUp();

					// If the mouse is still over the control
					if (_mouseOver)
					{
						// Find the command the mouse is over
						CommandState commandOver = CommandStateFromPoint(new Point(e.X, e.Y));

						// Change state to hot tracking the new command
						MakeStateHotTrack(commandOver);
					}
				}
				else
				{
					// We don't want to lose capture of mouse
					// (this can happen if we open a command, we ignore the up but keep capture)
					Capture = true;
				}
			}
		}

		/// <summary>
		/// Raises the MouseLeave event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			// Remove any tooltip showing
			KillTooltip();
			
			// Mouse leaves control, so remove any hot tracking or close open command
			RevertCurrentCommandState();
		
            // Mouse is not currently over the control
            _mouseOver = false;

			// If leaving then we cannot be capturing th mouse
			_mouseCapture = false;
		}

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows Message to process. </param>
        protected override void WndProc(ref Message m)
        {
            // Is this an internal request to layout control again?
            if (m.Msg == WM_RELAYOUT)
            {
                // Still need to perform a layout?
                if (_layoutRequired)
                {
                    // Perform layout now
                    LayoutControl();

                    // Must invalidate as it might cause zero sized client to become sized
                    Invalidate();
                }
            }

            // Let base class handle other messages
            base.WndProc(ref m);
        }

		private void InitializeComponent()
		{
			// 
			// CommandControl
			// 
			this.Name = "CommandControl";
		}
		
		private void OnPaddingChanged(object sender, EventArgs e)
		{
			// Relayout to show change in padding
			LayoutRequired();
		}

		private void MakeStateHotTrack(CommandState commandOver)
		{
			// Is there a change in state?
			if (commandOver != _currentCmdState)
			{
				// Make any existing command revert back to normal
				RevertCurrentCommandState();
				
				// Make this the new command
				_currentCmdState = commandOver;
				
				// Do we have a valid command to hot track?
				if (_currentCmdState != null)
				{
					// Switch to the hot tracking mode
					_currentCmdState.State = ItemState.HotTrack;
					
					// Make sure it is repainted
					Invalidate(_currentCmdState.DrawRect);
				}
			}
		}

		private void MakeStateOpen(CommandState commandDown, Point pt)
		{
			// Remove any tooltip
			KillTooltip();

			// If current command is not the new one, then revert it
			RevertCurrentCommandState(commandDown);
			
			// Make this the new command
			_currentCmdState = commandDown;
			
			// Do we have a valid command to open?
			if (_currentCmdState != null)
			{
				// Switch to the hot tracking mode
				_currentCmdState.State = ItemState.Open;
				
				// Make sure it is repainted
				Invalidate(_currentCmdState.DrawRect);
				
				// Cast to expected interface
				ICommandOpen commandOpen = commandDown.Command as ICommandOpen;
				
				// Ask the command to open itself now
				commandOpen.Open(pt);
			}
		}

		private void MakeStatePressed(CommandState commandDown, Point pt)
		{
			// Remove any tooltip
			KillTooltip();

			// If current command is not the new one, then revert it
			RevertCurrentCommandState(commandDown);
			
			// Make this the new command
			_currentCmdState = commandDown;
			
			// Do we have a valid command to hot track?
			if (_currentCmdState != null)
			{
				// Switch to the hot tracking mode
				_currentCmdState.State = ItemState.Pressed;
				
				// Make sure it is repainted
				Invalidate(_currentCmdState.DrawRect);
			}
		}
		
		private void MakeCapturedMatchMouse(CommandState commandOver)			
		{
			// Mouse is being pressed, if over the current command
			if (commandOver == _currentCmdState)
			{
				if (_currentCmdState != null)
				{
					// Then state should be pressed
					if (_currentCmdState.State != ItemState.Pressed)
					{
						// Place it into the pressed state
						_currentCmdState.State = ItemState.Pressed;	
						
						// Repaint the new state
						Invalidate(_currentCmdState.DrawRect);
					}
				}
			}
			else
			{
				if (_currentCmdState != null)
				{
					// Keep command pressed as hot tracked
					if (_currentCmdState.State != ItemState.HotTrack)
					{
						// Place it into the pressed state
						_currentCmdState.State = ItemState.HotTrack;	
						
						// Repaint the new state
						Invalidate(_currentCmdState.DrawRect);
					}
				}
			}
		}
		
		private void RevertCurrentMouseUp()
		{
			// Is there a current state to revert back?
			if (_currentCmdState != null)
			{
				// If the current command is pressed
				if (_currentCmdState.State == ItemState.Pressed)
				{
					// Tell the command it has been pressed
					_currentCmdState.Command.Pressed();
				}

				// Now revert back the current command
				RevertCurrentCommandState();
			}			
		}
		
		private void RevertCurrentCommandState(CommandState commandNew)
		{
			// If the current command is not the new one already
			if (_currentCmdState != commandNew)
				RevertCurrentCommandState();
		}
		
		private void RevertCurrentCommandState()
		{
			// Is there a current state to revert back?
			if (_currentCmdState != null)
			{
				// Is the command currently open?
				if (_currentCmdState.State == ItemState.Open)
				{
					// Cast to expected interface
					ICommandOpen commandOpen = _currentCmdState.Command as ICommandOpen;
				
					// Ask the command to close itself now
					commandOpen.Close();
					
					// No longer have logical capture of mouse
					_mouseCapture = false;
				}
			
				// Revert it back to normal state
				_currentCmdState.State = ItemState.Normal;
				
				// Make sure it is repainted
				Invalidate(_currentCmdState.DrawRect);
				
				// Remove command from being current
				_currentCmdState = null;
			}
		}
		
		private CommandState CommandStateFromPoint(Point clientPt)
		{
			// Be default we only want commands that are enabled
			return CommandStateFromPoint(clientPt, true);
		}
		
		private CommandState CommandStateFromPoint(Point clientPt, bool onlyEnabled)
		{
			// Test against each command state in turn
			foreach(CommandState cmdState in _states)
			{
				// Only interested in visible and enabled commands (if interested in enabled bugs)
				if (cmdState.Displayed && (!onlyEnabled || (onlyEnabled && cmdState.Command.Enabled)))
					if (cmdState.Contains(clientPt))
						return cmdState;
			}
			
			// No match found
			return null;
		}

		private bool IsCurrentCommandOpen
		{
			get { return ((_currentCmdState != null) && (_currentCmdState.State == ItemState.Open)); }
		}
		
		private bool CanCommandOpen(CommandState state)
		{
			// Cast the embedded command to relevant interface
			ICommandOpen commandOpen = state.Command as ICommandOpen;
					
			// Does the target have correct interface and request to be opened?
			if ((commandOpen != null) && commandOpen.CanOpen())
				return true;
				
			// Failed, cannot open
			return false;
		}
	
		private void TooltipMove()
		{
			// If there is no tooltip present
			if (_popupTooltip == null)
			{
				Form f = FindForm();

				// Only allow tooltip if the form we are inside is active
				if ((f == null) || ((f != null) && f.ContainsFocus))
				{
					// Restart the hover timer
					_hoverTimer.Stop();
					_hoverTimer.Start();
					
					// Remember the point when mouse move started
					_hoverPoint = Control.MousePosition;
				}
			}
			else
			{
				// Yes there is a tooltip, are we still over something?
				CommandState commandOver = CommandStateFromPoint(PointToClient(Control.MousePosition), false);

				// Over a command?
				if (commandOver != null)
				{
					// Yes, changed to a different command?
					if (commandOver != _tooltipCmdState)
					{
						// Does the new command have a useful tooltip?
						if (commandOver.Command.Tooltip.Length > 0)
						{
							// Cache which command the popup is for
							_tooltipCmdState = commandOver;

							// Update with the new text
							_popupTooltip.ToolText = commandOver.Command.Tooltip;

							// Make it visible but without taking the foucs
							_popupTooltip.ShowWithoutFocus(new Point(Control.MousePosition.X, Control.MousePosition.Y + 24));
						}
						else
							KillTooltip();
					}
				}
				else
					KillTooltip();
			}
		}

		private void KillTooltip()
		{
			// Do not want timer to fire again
			_hoverTimer.Stop();

			// Check if there is anything to kill
			if (_popupTooltip != null)
			{
				// Remove the tooltip
				_popupTooltip.Close();
				_popupTooltip.Dispose();
				_popupTooltip = null;

				// No longer cache associated command
				_tooltipCmdState = null;
			}
		}

		private void OnMouseHoverTick(object sender, EventArgs e)
		{
			// Do not want timer to fire again
			_hoverTimer.Stop();

			// Only interested if no button is being pressed
			if (Control.MouseButtons == MouseButtons.None)
			{
				// Only interested if mouse has not moved
				if (Control.MousePosition == _hoverPoint)
				{
					// Find any command the mouse is over
					CommandState commandOver = CommandStateFromPoint(PointToClient(Control.MousePosition), false);

					// Was a command found?
					if (commandOver != null)
					{
						// Only interesed if the tooltip has a interesting value
						if (commandOver.Command.Tooltip.Length > 0)
						{
							// Cache which command the popup is for
							_tooltipCmdState = commandOver;

							// Create a tooltip control using same style as our own
							_popupTooltip = new PopupTooltipSingle(this.Style);
								
							// Define string for display
							_popupTooltip.ToolText = commandOver.Command.Tooltip;

							// Make it visible but without taking the foucs
							_popupTooltip.ShowWithoutFocus(new Point(Control.MousePosition.X, Control.MousePosition.Y + 24));
						}
					}
				}
			}
		}

		private void OnUpdateTick(object sender, EventArgs e)
		{
			// Pulse each of the top-level update handlers
			UpdateAll();
		}
	}
}
