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
using System.Drawing.Design;
using System.Reflection;
using System.Collections;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Microsoft.Win32;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls;
using Crownwood.DotNetMagic.Controls.Command;
using Crownwood.DotNetMagic.Win32;

namespace Crownwood.DotNetMagic.Menus
{
	/// <summary>
	/// Manage a collection MenuCommands.
	/// </summary>
    [ToolboxBitmap(typeof(MenuControl))]
    [DefaultProperty("MenuCommands")]
    [DefaultEvent("Selected")]
    [Designer(typeof(Crownwood.DotNetMagic.Menus.MenuControlDesigner))]
    public class MenuControl : ContainerControl, IMessageFilter
    {
        private class MdiClientSubclass : NativeWindow
        {
            protected override void WndProc(ref Message m)
            {
                switch(m.Msg)
                {
                    case (int)Win32.Msgs.WM_MDISETMENU:
                    case (int)Win32.Msgs.WM_MDIREFRESHMENU:
                        return;
                }

                base.WndProc(ref m);
            }			
        }

        // Class constants
        private const int _lengthGap = 3;
        private const int _boxExpandUpper = 1;
        private const int _boxExpandSides = 2;
        private const int _shadowGap = 4;
        private const int _shadowYOffset = 4;
        private const int _separatorWidth = 15;
        private const int _subMenuBorderAdjust = 2;
        private const int _minIndex = 0;
        private const int _restoreIndex = 1;
        private const int _closeIndex = 2;
        private const int _chevronIndex = 3;
        private const int _buttonLength = 16;
        private const int _chevronLength = 12;
        private const int _pendantLength = 48;
        private const int _pendantOffset = 3;

        // Class constant is marked as 'readonly' to allow non constant initialization
        private readonly int WM_OPERATEMENU = (int)Win32.Msgs.WM_USER + 1;

        // Class fields
        private static ImageList _menuImages = null;
        private static bool _supportsLayered = false;

        // Instance fields
        private int _rowWidth;
        private int _rowHeight;
        private int _trackItem;
        private int _breadthGap;
        private int _animateTime;
        private IntPtr _oldFocus;
        private Pen _controlLPen;
        private bool _animateFirst;
        private bool _selected;
        private bool _multiLine;
        private bool _mouseOver;
        private bool _manualFocus;
        private bool _drawUpwards;
		private bool _defaultFont;
        private bool _defaultBackColor;
        private bool _defaultTextColor;
        private bool _defaultHighlightBackColor;
        private bool _defaultHighlightTextColor;
        private bool _defaultSelectedBackColor;
        private bool _defaultSelectedTextColor;
        private bool _defaultPlainSelectedTextColor;
        private bool _plainAsBlock;
        private bool _dismissTransfer;
        private bool _ignoreMouseMove;
        private bool _expandAllTogether;
        private bool _rememberExpansion;
        private bool _deselectReset;
        private bool _highlightInfrequent;
		private bool _exitLoop;
		private bool _allowLayered;
		private bool _256Colors;
		private bool _allowPendant;
		private bool _office2003GradBack;
		private bool _ide2005GradBack;
		private bool _ignoreF10;
		private Color _textColor;
        private Color _highlightBackColor;
        private Color _highlightTextColor;
        private Color _highlightBackDark;
        private Color _highlightBackLight;
        private Color _selectedBackColor;
		private Color _selectedDarkBackColor;
		private Color _selectedTextColor;
        private Color _plainSelectedTextColor;
        private Form _activeChild;
        private Form _mdiContainer;
		private VisualStyle _style;
        private LayoutDirection _direction;
        private PopupMenu _popupMenu;
        private ArrayList _drawCommands;
        private SolidBrush _controlLBrush;
        private Animate _animate;
        private Animation _animateStyle;
        private MdiClientSubclass _clientSubclass;
        private MenuCommand _chevronStartCommand;
        private MenuCommandCollection _menuCommands;
		private MenuClickEventArgs.ClickSource _clickSource;
		private ColorDetails _colorDetails;

        // Instance fields - pendant buttons
        private ButtonWithStyle _minButton;
        private ButtonWithStyle _restoreButton;
        private ButtonWithStyle _closeButton;

        /// <summary>
        /// Occurs when the user tracks into a menu item.
        /// </summary>
        public event CommandHandler Selected;

		/// <summary>
		/// Occurs when the user tracks away from a menu item.
		/// </summary>
        public event CommandHandler Deselected;
        
		/// <summary>
		/// Occurs when the user clicks the Mdi Minimize button.
		/// </summary>
		public event CancelEventHandler MdiMinimize;	

		/// <summary>
		/// Occurs when the user clicks the Mdi Restore button.
		/// </summary>
		public event CancelEventHandler MdiRestore;	

		/// <summary>
		/// Occurs when the user clicks the Mdi Close button.
		/// </summary>
		public event CancelEventHandler MdiClose;	
		
        static MenuControl()
        {
            // Create a strip of images by loading an embedded bitmap resource
            _menuImages = ResourceHelper.LoadBitmapStrip(Type.GetType("Crownwood.DotNetMagic.Menus.MenuControl"),
                                                         "Crownwood.DotNetMagic.Resources.ImagesMenuControl.bmp",
                                                         new Size(_buttonLength, _buttonLength),
                                                         new Point(0,0));

			// We need to know if the OS supports layered windows
            _supportsLayered = (OSFeature.Feature.GetVersionPresent(OSFeature.LayeredWindows) != null);

			if (_supportsLayered)
			{
				try
				{
					// Look for registry setting that overrides layered setting
					RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Crownwood\DotNetMagic");

					// If the key exists...
					if (key != null)
					{
						// Grab the boolean string 'True' or 'False'
						object val = key.GetValue("AllowLayered", true);

						// Convert to boolean and use as setting
						_supportsLayered = bool.Parse(val.ToString());
					}
				}
				catch
				{}
			}
        }

		/// <summary>
		/// Initialize a new instance of the MenuControl class.
		/// </summary>
        public MenuControl()
        {
            // Set default values
            _trackItem = -1;
            _oldFocus = IntPtr.Zero;
            _minButton = null;
            _popupMenu = null;
            _activeChild = null;
            _closeButton = null;
            _controlLPen = null;
            _mdiContainer = null;
            _restoreButton = null;
            _controlLBrush = null;
            _chevronStartCommand = null;
            _animateFirst = true;
			_exitLoop = false;
            _selected = false;
            _multiLine = false;
            _mouseOver = false;
			_defaultFont = true;
            _manualFocus = false;
            _drawUpwards = false;
            _plainAsBlock = false;
            _clientSubclass = null;
            _ignoreMouseMove = false;
            _ignoreF10 = false;
            _deselectReset = true;
            _expandAllTogether = true;
            _rememberExpansion = true;
            _highlightInfrequent = true;
            _office2003GradBack = true;
            _ide2005GradBack = true;
            _dismissTransfer = false;
			_allowLayered = true;
			_allowPendant = true;
            _style = VisualStyle.Office2003;
            _direction = LayoutDirection.Horizontal;
            _menuCommands = new MenuCommandCollection();
			_256Colors = (ColorHelper.ColorDepth() == 8);
			this.Dock = DockStyle.Top;
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			_colorDetails = new ColorDetails();
			_colorDetails.Style = _style;
			
            // Animation details
            _animateTime = 100;
            _animate = Animate.System;
            _animateStyle = Animation.System;

            // Prevent flicker with double buffering and all painting inside WM_PAINT
			SetStyle(ControlStyles.DoubleBuffer | 
					 ControlStyles.AllPaintingInWmPaint |
					 ControlStyles.UserPaint, true);
			
            // Should not be allowed to select this control
            SetStyle(ControlStyles.Selectable, false);

            // Hookup to collection events
            _menuCommands.Cleared += new CollectionClear(OnCollectionCleared);
            _menuCommands.Inserted += new CollectionChange(OnCollectionInserted);
            _menuCommands.Removed += new CollectionChange(OnCollectionRemoved);

			// Need notification when the MenuFont is changed
			Microsoft.Win32.SystemEvents.UserPreferenceChanged += 
			    new UserPreferenceChangedEventHandler(OnPreferenceChanged);

            DefineColors();
            
            // Set the starting Font
            DefineFont((Font)SystemInformation.MenuFont.Clone());

            // Do not allow tab key to select this control
            this.TabStop = false;

            // Default to one line of items
            this.Height = _rowHeight;

            // Add ourself to the application filtering list
            Application.AddMessageFilter(this);
        }

		/// <summary>
		/// Releases all resources used by the Control.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if( disposing )
            {
				// Remove any menu collections
				if (_menuCommands != null)
				{
					_menuCommands.Dispose();
					_menuCommands = null;
				}

				if (_chevronStartCommand != null)
				{
					_chevronStartCommand.Dispose();
					_chevronStartCommand = null; 
				}

				// Reverse previous additional to allow memory cleanup
				Application.RemoveMessageFilter(this);
            
                // Remove notifications
                Microsoft.Win32.SystemEvents.UserPreferenceChanged -= 
                    new UserPreferenceChangedEventHandler(OnPreferenceChanged);

				// Color details has resources that need releasing
				_colorDetails.Dispose();
			}
            
            base.Dispose( disposing );
        }

		/// <summary>
		/// Gets the collection of child MenuCommand instances.
		/// </summary>
        [Category("Behaviour")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("Crownwood.DotNetMagic.Menus.MenuCollectionEditor", typeof(UITypeEditor))]
		public MenuCommandCollection MenuCommands
        {
            get { return _menuCommands; }
        } 

		/// <summary>
		/// Gets or sets the visual style of the control.
		/// </summary>
        [Category("Appearance")]
        public VisualStyle Style
        {
            get { return _style; }
			
            set
            {
                if (_style != value)
                {
                    _style = value;
                    _colorDetails.Style = value;

                    Recalculate();
                    Invalidate();
                }
            }
        }

		/// <summary>
		/// Gets and sets a value indicating if layered windows are allowed for popups.
		/// </summary>
		[Category("Appearance")]
		public bool AllowLayered
		{
			get { return _allowLayered; }
			set { _allowLayered = value; }
		}

		/// <summary>
		/// Gets or sets the font of the text displayed by the control.
		/// </summary>
        public override Font Font
        {
            get { return base.Font; }
			
            set
            {
				if (value != base.Font)
				{
					_defaultFont = value.Equals(SystemInformation.MenuFont);
					DefineFont(value);
					Recalculate();
					Invalidate();
				}
            }
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
                    
					Recalculate();
					Invalidate();
				}
			}
		}

        private bool ShouldSerializeBackColor()
        {
            return this.BackColor != SystemColors.Control;
        }

		/// <summary>
		/// Text color used for drawing any text strings.
		/// </summary>
        [Category("Appearance")]
        public Color TextColor
        {
            get { return _textColor; }

            set
            {
                if (value != _textColor)
                {
                    _textColor = value;
                    _defaultTextColor = (value == SystemColors.MenuText);

                    Recalculate();
                    Invalidate();
                }
            }
        }

        private bool ShouldSerializeTextColor()
        {
            return _textColor != SystemColors.MenuText;
        }

		/// <summary>
		/// Background color used for drawing highlighted items.
		/// </summary>
        [Category("Appearance")]
        public Color HighlightBackColor
        {
            get { return _highlightBackColor; }

            set
            {
                if (value != _highlightBackColor)
                {
                    _defaultHighlightBackColor = (value == SystemColors.Highlight);
                    DefineHighlightBackColors(value);                    

                    Recalculate();
                    Invalidate();
                }
            }
        }

        private bool ShouldSerializeHighlightBackColor()
        {
            return _highlightBackColor != SystemColors.Highlight;
        }

		/// <summary>
		/// Text color used for drawing highlighted items.
		/// </summary>
        [Category("Appearance")]
        public Color HighlightTextColor
        {
            get { return _highlightTextColor; }

            set
            {
                if (value != _highlightTextColor)
                {
                    _highlightTextColor = value;
                    _defaultHighlightTextColor = (value == SystemColors.MenuText);

                    Recalculate();
                    Invalidate();
                }
            }
        }

        private bool ShouldSerializeHighlightTextColor()
        {
            return _highlightTextColor != SystemColors.HighlightText;
        }

		/// <summary>
		/// Background color used for drawing selected items.
		/// </summary>
        [Category("Appearance")]
        public Color SelectedBackColor
        {
            get { return _selectedBackColor; }

            set
            {
                if (value != _selectedBackColor)
                {
					_defaultSelectedBackColor = (value == SystemColors.Control);
					DefineSelectedBackColors(value);

                    Recalculate();
                    Invalidate();
                }
            }
        }

        private bool ShouldSerializeSelectedBackColor()
        {
            return _selectedBackColor != SystemColors.Control;
        }

		/// <summary>
		/// Text color used for drawing selected items.
		/// </summary>
        [Category("Appearance")]
        public Color SelectedTextColor
        {
            get { return _selectedTextColor; }

            set
            {
                if (value != _selectedTextColor)
                {
                    _selectedTextColor = value;
                    _defaultSelectedTextColor = (value == SystemColors.MenuText);

                    Recalculate();
                    Invalidate();
                }
            }
        }

        private bool ShouldSerializeSelectedTextColor()
        {
            return _selectedTextColor != SystemColors.MenuText;
        }

		/// <summary>
		/// Text color used for drawing selected items in plain appearance.
		/// </summary>
        [Category("Appearance")]
        public Color PlainSelectedTextColor
        {
            get { return _plainSelectedTextColor; }

            set
            {
                if (value != _plainSelectedTextColor)
                {
                    _plainSelectedTextColor = value;
                    _defaultPlainSelectedTextColor = (value == SystemColors.ActiveCaptionText);

                    Recalculate();
                    Invalidate();
                }
            }
        }

        private bool ShouldSerializePlainSelectedTextColor()
        {
            return _plainSelectedTextColor != SystemColors.ActiveCaptionText;
        }

		/// <summary>
		/// Gets and sets a value indicating if plain appearance should show background as a block of color.
		/// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool PlainAsBlock
        {
            get { return _plainAsBlock; }

            set
            {
                if (_plainAsBlock != value)
                {
	                _plainAsBlock = value;

                    Recalculate();
                    Invalidate();
                }
            }
        }

		/// <summary>
		/// Gets and sets a value indicating if multiple lines are allowed for showing menu items.
		/// </summary>
        [Category("Appearance")]
        [DefaultValue(false)]
        public bool MultiLine
        {
            get { return _multiLine; }

            set
            {
                if (_multiLine != value)
                {
                    _multiLine = value;

                    Recalculate();
                    Invalidate();
                }
            }
        }

		/// <summary>
		/// Gets and sets a value indicating which direction to position menu items.
		/// </summary>
        [Category("Appearance")]
        public LayoutDirection Direction
        {
            get { return _direction; }

            set
            {
                if (_direction != value)
                {
                    _direction = value;

                    Recalculate();
                    Invalidate();
                }
            }
        }

		/// <summary>
		/// Gets and sets a value indicating if expanded popup menus should be remembered.
		/// </summary>
        [Category("Behaviour")]
        [DefaultValue(true)]
        public bool RememberExpansion
        {
            get { return _rememberExpansion; }
            set { _rememberExpansion = value; }
        }

		/// <summary>
		/// Gets and sets a value indicating if expanding one popup menu should expand them all.
		/// </summary>
        [Category("Behaviour")]
        [DefaultValue(true)]
        public bool ExpandAllTogether
        {
            get { return _expandAllTogether; }
            set { _expandAllTogether = value; }
        }
        
		/// <summary>
		/// Gets and sets a value indicating if deselecting from control should reset expansion state.
		/// </summary>
        [Category("Behaviour")]
        [DefaultValue(true)]
        public bool DeselectReset
        {
            get { return _deselectReset; }
            set { _deselectReset = value; }
        }
        
		/// <summary>
		/// Gets and sets a value indicating if menu items marked as infrequent should be drawn differently.
		/// </summary>
        [Category("Behaviour")]
        [DefaultValue(true)]
        public bool HighlightInfrequent
        {
            get { return _highlightInfrequent; }
            set { _highlightInfrequent = value; }
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
		/// Gets or sets which edge of the parent container a control is docked to.
		/// </summary>
        public override DockStyle Dock
        {
            get { return base.Dock; }

            set
            {
                base.Dock = value;

                switch(value)
                {
                    case DockStyle.None:
                        _direction = LayoutDirection.Horizontal;
                        break;
                    case DockStyle.Top:
                    case DockStyle.Bottom:
                        this.Height = 0;
                        _direction = LayoutDirection.Horizontal;
                        break;
                    case DockStyle.Left:
                    case DockStyle.Right:
                        this.Width = 0;
                        _direction = LayoutDirection.Vertical;
                        break;
                }

                Recalculate();
                Invalidate();
            }
        }

		/// <summary>
		/// Gets and sets the need for using animation.
		/// </summary>
        [Category("Animate")]
        [DefaultValue(typeof(Animate), "System")]
        public Animate Animate
        {
            get { return _animate; }
            set { _animate = value; }
        }

		/// <summary>
		/// Gets and sets the time used to perform animation.
		/// </summary>
        [Category("AnimateTime")]
        public int AnimateTime
        {
            get { return _animateTime; }
            set { _animateTime = value; }
        }

		/// <summary>
		/// Gets and sets the style of animation to use.
		/// </summary>
        [Category("AnimateStyle")]
        public Animation AnimateStyle
        {
            get { return _animateStyle; }
            set { _animateStyle = value; }
        }

		/// <summary>
		/// Gets and sets the Form the is the MDI container.
		/// </summary>
		[Category("Behaviour")]
        [DefaultValue(null)]
        public Form MdiContainer
        {
            get { return _mdiContainer; }
			
            set
            {
                if (_mdiContainer != value)
                {
                    if (_mdiContainer != null)
                    {
                        // Unsubclass from MdiClient and then remove object reference
                        _clientSubclass.ReleaseHandle();
                        _clientSubclass = null;

                        // Remove registered events
                        _mdiContainer.MdiChildActivate -= new EventHandler(OnMdiChildActivate);

                        RemovePendantButtons();
                    }

                    _mdiContainer = value;

                    if (_mdiContainer != null)
                    {
                        CreatePendantButtons();

                        foreach(Control c in _mdiContainer.Controls)
                        {
                            MdiClient client = c as MdiClient;

                            if (client != null)
                            {
                                // We need to subclass the MdiClient to prevent any attempt
                                // to change the menu for the container Form. This prevents
                                // the system from automatically adding the pendant.
                                _clientSubclass = new MdiClientSubclass();
                                _clientSubclass.AssignHandle(client.Handle);
                            }
                        }

                        // Need to be informed of the active child window
                        _mdiContainer.MdiChildActivate += new EventHandler(OnMdiChildActivate);
                    }
                }
            }
        }

		/// <summary>
		/// Gets and sets a value indicating if pendant buttons are allowed.
		/// </summary>
		[Category("Behvior")]
		[DefaultValue(true)]
		[Description("Should pendant buttons but allowed.")]
        public bool AllowPendant
        {
			get { return _allowPendant; }
			set { _allowPendant = value; }
        }
        
        /// <summary>
        /// Resets value of the AllowPendant property.
        /// </summary>
        public void ResetAllowPendant()
        {
			AllowPendant = true;
        }

		/// <summary>
		/// Default the value of the colors.
		/// </summary>
        protected void DefineColors()
        {
            // Define starting text colors
            _defaultTextColor = true;
            _defaultHighlightTextColor = true;
            _defaultSelectedTextColor = true;
            _defaultPlainSelectedTextColor = true;
            _textColor = SystemColors.MenuText;
            _highlightTextColor = SystemColors.MenuText;
            _selectedTextColor = SystemColors.MenuText;
			_plainSelectedTextColor = SystemColors.ActiveCaptionText;

            // Define starting back colors
            _defaultBackColor = true;
            _defaultHighlightBackColor = true;
            _defaultSelectedBackColor = true;
            base.BackColor = SystemColors.Control;
            DefineHighlightBackColors(SystemColors.Highlight);
            DefineSelectedBackColors(SystemColors.Control);
        }
        
		/// <summary>
		/// Reset the colors back to defaults.
		/// </summary>
        public void ResetColors()
        {
            this.BackColor = SystemColors.Control;
            this.TextColor = SystemColors.MenuText;
            this.HighlightBackColor = SystemColors.Highlight;            
            this.HighlightTextColor = SystemColors.MenuText;
            this.SelectedBackColor = SystemColors.Control;
            this.SelectedTextColor = SystemColors.MenuText;
        }

		/// <summary>
		/// Gets and sets a value indicating if F10 should be ignored.
		/// </summary>
		public bool IgnoreF10
		{
			get { return _ignoreF10; }
			set { _ignoreF10 = value; }
		}

		/// <summary>
		/// Raises the Selected event.
		/// </summary>
		/// <param name="mc">MenuCommand instance that has been selected.</param>
		public virtual void OnSelected(MenuCommand mc)
		{
			// Any attached event handlers?
			if (Selected != null)
				Selected(mc);
		}

		/// <summary>
		/// Raises the Deselected event.
		/// </summary>
		/// <param name="mc">MenuCommand instance that has been deselected.</param>
		public virtual void OnDeselected(MenuCommand mc)
		{
			// Any attached event handlers?
			if (Deselected != null)
				Deselected(mc);
		}

		/// <summary>
		/// Cache helper values that are relative to the newly defined Font.
		/// </summary>
		/// <param name="newFont"></param>
		protected void DefineFont(Font newFont)
		{
			base.Font = newFont;

			_breadthGap = (this.Font.Height / 3) + 1;

            // Calculate the initial height/width of the control
            _rowWidth = _rowHeight = this.Font.Height + _breadthGap * 2 + 1;
		}

		/// <summary>
		/// Calculate the correct selected drawing colors relative to a base color.
		/// </summary>
		/// <param name="baseColor">Starting color for calculations.</param>
        protected void DefineSelectedBackColors(Color baseColor)
        {
			// If user wants default colors and we are working in reduced color set
			if (_defaultSelectedBackColor && _256Colors)
			{
				_selectedBackColor = baseColor;
				_selectedDarkBackColor = SystemColors.ControlDark;
				_controlLPen = new Pen(baseColor);
				_controlLBrush = new SolidBrush(baseColor);
			}
			else
			{
				Color lightColor = ColorHelper.CalculateColor(baseColor, Color.White, 200);

				_selectedBackColor = baseColor;
				_selectedDarkBackColor = ControlPaint.Dark(baseColor);
				_controlLPen = new Pen(lightColor);
				_controlLBrush = new SolidBrush(lightColor);
			}
        }

		/// <summary>
		/// Calculate the correct highlight drawing colors relative to a base color.
		/// </summary>
		/// <param name="baseColor">Starting color for calculations.</param>
        protected void DefineHighlightBackColors(Color baseColor)
        {
			_highlightBackColor = baseColor;
        
            if (_defaultHighlightBackColor)
            {
				_highlightBackDark = SystemColors.Highlight;

				// If only in reduced color mode...
				if (_256Colors)
				{
					// Then use alternate system colors for everything
					_highlightBackLight = SystemColors.Window;
				}
				else
				{
					// Otherwise its safe to use 24bit coloring
					_highlightBackLight = ColorHelper.CalculateColor(SystemColors.Highlight, Color.White, 70);
				}
            }
            else
            {
				_highlightBackDark = ControlPaint.Dark(baseColor);
                _highlightBackLight = baseColor;
            }
		}

		/// <summary>
		/// Occurs when the collection of commands has been cleared.
		/// </summary>
        protected void OnCollectionCleared()
        {
            // Reset state ready for a recalculation
            Deselect();
            RemoveItemTracking();

            Recalculate();
            Invalidate();
        }

		/// <summary>
		/// Occurs when a new menu item has been added.
		/// </summary>
		/// <param name="index">Index position of new item.</param>
		/// <param name="value">Reference to new item instance.</param>
        protected void OnCollectionInserted(int index, object value)
        {
            MenuCommand mc = value as MenuCommand;

            // We need notification whenever the properties of this command change
            mc.PropertyChanged += new MenuCommand.PropChangeHandler(OnCommandChanged);
				
            // Reset state ready for a recalculation
            Deselect();
            RemoveItemTracking();

            Recalculate();
            Invalidate();
        }

		/// <summary>
		/// Occurs when an existing menu item has been removed.
		/// </summary>
		/// <param name="index">Index position of removed item.</param>
		/// <param name="value">Reference to old item instance.</param>
        protected void OnCollectionRemoved(int index, object value)
        {
            // Reset state ready for a recalculation
            Deselect();
            RemoveItemTracking();

            Recalculate();
            Invalidate();
        }

		/// <summary>
		/// Occurs when a property of a child command has been changed.
		/// </summary>
		/// <param name="item">MenuCommand instance that has changed.</param>
		/// <param name="prop">Which property of instance has changed.</param>
        protected void OnCommandChanged(MenuCommand item, MenuCommand.Property prop)
        {
            Recalculate();
            Invalidate();
        }

		/// <summary>
		/// Occurs when the active MDI child has changed.
		/// </summary>
		/// <param name="sender">Source that fired the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected void OnMdiChildActivate(object sender, EventArgs e)
        {
            // Unhook from event
            if (_activeChild != null)
                _activeChild.SizeChanged -= new EventHandler(OnMdiChildSizeChanged);

            // Remember the currently active child form
            _activeChild = _mdiContainer.ActiveMdiChild;

            // Need to know when window becomes maximized
            if (_activeChild != null)
                _activeChild.SizeChanged += new EventHandler(OnMdiChildSizeChanged);

            // Might be a change in pendant requirements
            Recalculate();
            Invalidate();
        }

		/// <summary>
		/// Occurs when the size of an MDI child has changed.
		/// </summary>
		/// <param name="sender">Source that fired the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected void OnMdiChildSizeChanged(object sender, EventArgs e)
        {
            // Has window changed to become maximized?
            if (_activeChild.WindowState == FormWindowState.Maximized)
            {
                // Reflect change in menu
                Recalculate();
                Invalidate();
            }
        }

		/// <summary>
		/// Occurs when the MDI child has been minimized.
		/// </summary>
		/// <param name="sender">Source that fired the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected void OnMdiMin(object sender, EventArgs e)
        {
            if (_activeChild != null)
            {
				// Create structure allowing user to cancel the change
				CancelEventArgs ce = new CancelEventArgs();
				
				// Any attached event handlers?
				if (MdiMinimize != null)
					MdiMinimize(this, ce);
				
				// Allowed to continue with change?
				if (!ce.Cancel)
				{
					_activeChild.WindowState = FormWindowState.Minimized;

					// Reflect change in menu
					Recalculate();
					Invalidate();
				}
            }
        }

		/// <summary>
		/// Occurs when the MDI child has been restored.
		/// </summary>
		/// <param name="sender">Source that fired the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected void OnMdiRestore(object sender, EventArgs e)
        {
            if (_activeChild != null)
            {
				// Create structure allowing user to cancel the change
				CancelEventArgs ce = new CancelEventArgs();
				
				// Any attached event handlers?
				if (MdiRestore != null)
					MdiRestore(this, ce);

				// Allowed to continue with change?
				if (!ce.Cancel)
				{
					_activeChild.WindowState = FormWindowState.Normal;

					// Reflect change in menu
					Recalculate();
					Invalidate();
				}
            }
        }

		/// <summary>
		/// Occurs when the MDI child is being closed.
		/// </summary>
		/// <param name="sender">Source that fired the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected void OnMdiClose(object sender, EventArgs e)
        {
            if (_activeChild != null)
            {
				// Create structure allowing user to cancel the change
				CancelEventArgs ce = new CancelEventArgs();
				
				// Any attached event handlers?
				if (MdiClose != null)
					MdiClose(this, ce);
				
				// Allowed to continue with change?
				if (!ce.Cancel)
				{
					_activeChild.Close();

					// Reflect change in menu
					Recalculate();
					Invalidate();
				}
            }
        }

		/// <summary>
		/// Create the set of buttons used in the pendant.
		/// </summary>
        protected void CreatePendantButtons()
        {
            // Create the objects
            _minButton = new ButtonWithStyle();
            _restoreButton = new ButtonWithStyle();
            _closeButton = new ButtonWithStyle();

			// Define images to draw
			_minButton.Image = _menuImages.Images[_minIndex];
			_restoreButton.Image = _menuImages.Images[_restoreIndex];
			_closeButton.Image = _menuImages.Images[_closeIndex];
			
			// Do not draw the button raised for IDE style
			_minButton.StaticIDE = true;
			_restoreButton.StaticIDE = true;
			_closeButton.StaticIDE = true;

            // Define the constant sizes
            _minButton.Size = new Size(_buttonLength, _buttonLength);
            _restoreButton.Size = new Size(_buttonLength, _buttonLength);
            _closeButton.Size = new Size(_buttonLength, _buttonLength);

            // Default their position so they are not visible
            _minButton.Location = new Point(-_buttonLength, -_buttonLength);
            _restoreButton.Location = new Point(-_buttonLength, -_buttonLength);
            _closeButton.Location = new Point(-_buttonLength, -_buttonLength);

            // Hookup event handlers
            _minButton.Click += new EventHandler(OnMdiMin);
            _restoreButton.Click += new EventHandler(OnMdiRestore);
            _closeButton.Click += new EventHandler(OnMdiClose);

            // Add to display
            this.Controls.AddRange(new Control[]{_minButton, _restoreButton, _closeButton});
        }

		/// <summary>
		/// Destory the set of buttons used in the pendant.
		/// </summary>
        protected void RemovePendantButtons()
        {
            // Unhook event handlers
            _minButton.Click -= new EventHandler(OnMdiMin);
            _restoreButton.Click -= new EventHandler(OnMdiRestore);
            _closeButton.Click -= new EventHandler(OnMdiClose);

            // Remove from display

			// Use helper method to circumvent form Close bug
			ControlHelper.Remove(this.Controls, _minButton);
			ControlHelper.Remove(this.Controls, _restoreButton);
			ControlHelper.Remove(this.Controls, _closeButton);

            // Release resources
            _minButton.Dispose();
            _restoreButton.Dispose();
            _closeButton.Dispose();

            // Remove references
            _minButton = null;
            _restoreButton = null;
            _closeButton = null;
        }
        
		/// <summary>
		/// Occurs when the enabled state of the control changes.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            // Have we become disabled?
            if (!this.Enabled)
            {
                // Is an item selected?
                if (_selected)
                {
                    // Is a popupMenu showing?
                    if (_popupMenu != null)
                    {
                        // Dismiss the submenu
                        _popupMenu.Dismiss();

                        // No reference needed
						_popupMenu.Release(false);
                        _popupMenu = null;
                    }

                    // Reset state
                    Deselect();
                    _drawUpwards = false;

                    SimulateReturnFocus();
                }
            }

            // Do not draw any item as being tracked
            RemoveItemTracking();

            // Change in state changes the way items are drawn
            Invalidate();
        }

        internal void OnWM_MOUSEDOWN(Win32.POINT screenPos)
        {
            // Convert the mouse position to screen coordinates
            User32.ScreenToClient(this.Handle, ref screenPos);

            OnProcessMouseDown(screenPos.x, screenPos.y);
        }

		/// <summary>
		/// Raises the MouseDown event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            OnProcessMouseDown(e.X, e.Y);

            base.OnMouseDown(e);
        }

		/// <summary>
		/// Handle processing of a MouseDown situation.
		/// </summary>
		/// <param name="xPos">X position in client coordinates.</param>
		/// <param name="yPos">Y position in client coordinates.</param>
        protected void OnProcessMouseDown(int xPos, int yPos)
        {
            Point pos = new Point(xPos, yPos);

            for(int i=0; i<_drawCommands.Count; i++)
            {
                DrawCommand dc = _drawCommands[i] as DrawCommand;

                // Find the DrawCommand this is over
                if (dc.SelectRect.Contains(pos) && dc.Enabled)
                {
                    // Is an item already selected?
                    if (_selected)
                    {
                        // Is it this item that is already selected?
                        if (_trackItem == i)
                        {
                            // Is a popupMenu showing
                            if (_popupMenu != null)
                            {
                                // Dismiss the submenu
                                _popupMenu.Dismiss();

                                // No reference needed
								_popupMenu.Release(false);
                                _popupMenu = null;
                            }
                        }
                    }
                    else
                    {
                        // Select the tracked item
                        _selected = true;
                        _drawUpwards = false;
								
                        // Is there a change in tracking?
                        if (_trackItem != i)
                        {
                            // Modify the display of the two items 
                            _trackItem = SwitchTrackingItem(_trackItem, i);
                        }
                        else
                        {
                            // Update display to show as selected
                            DrawCommand(_trackItem, true);
                        }

                        // Is there a submenu to show?
                        if (dc.Chevron || (dc.MenuCommand.MenuCommands.Count > 0))
                            User32.PostMessage(this.Handle, WM_OPERATEMENU, 1, 0);
						else
						{
							// Has no children to fire click event for menu
							dc.MenuCommand.OnClick(new MenuClickEventArgs(MenuClickEventArgs.ClickSource.Mouse));
						}
                    }

                    break;
                }
            }
        }

		/// <summary>
		/// Raises the MouseUp event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // Is an item currently being tracked?
            if (_trackItem != -1)
            {
                // Is it also selected?
                if (_selected == true)
                {
                    // Is it also showing a submenu
                    if (_popupMenu == null)
                    {
                        // Deselect the item
                        Deselect();
                        _drawUpwards = false;

                        DrawCommand(_trackItem, true);

                        SimulateReturnFocus();
                    }
                }
            }

            base.OnMouseUp(e);
        }

        internal void OnWM_MOUSEMOVE(Win32.POINT screenPos)
        {
            // Convert the mouse position to screen coordinates
            User32.ScreenToClient(this.Handle, ref screenPos);
            
            OnProcessMouseMove(screenPos.x, screenPos.y);
        }

		/// <summary>
		/// Raises the MouseMove event.
		/// </summary>
		/// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Sometimes we need to ignore this message
            if (_ignoreMouseMove)
                _ignoreMouseMove = false;
            else
                OnProcessMouseMove(e.X, e.Y);

            base.OnMouseMove(e);
        }

		/// <summary>
		/// Handle processing of a MouseMove situation.
		/// </summary>
		/// <param name="xPos">X position in client coordinates.</param>
		/// <param name="yPos">Y position in client coordinates.</param>
        protected void OnProcessMouseMove(int xPos, int yPos)
        {
            // Sometimes we need to ignore this message
            if (_ignoreMouseMove)
                _ignoreMouseMove = false;
            else
            {
                // Is the first time we have noticed a mouse movement over our window
                if (!_mouseOver)
                {
                    // Create the structure needed for User32 call
                    Win32.TRACKMOUSEEVENTS tme = new Win32.TRACKMOUSEEVENTS();

                    // Fill in the structure
                    tme.cbSize = 16;									
                    tme.dwFlags = (uint)Win32.TrackerEventFlags.TME_LEAVE;
                    tme.hWnd = this.Handle;								
                    tme.dwHoverTime = 0;								

                    // Request that a message gets sent when mouse leaves this window
                    User32.TrackMouseEvent(ref tme);

                    // Yes, we know the mouse is over window
                    _mouseOver = true;
                }

                Form parentForm = this.FindForm();

                // Only hot track if this Form is active
                if ((parentForm != null) && parentForm.ContainsFocus)
                {
                    Point pos = new Point(xPos, yPos);

                    int i = 0;

                    for(i=0; i<_drawCommands.Count; i++)
                    {
                        DrawCommand dc = _drawCommands[i] as DrawCommand;

                        // Find the DrawCommand this is over
                        if (dc.SelectRect.Contains(pos) && dc.Enabled)
                        {
                            // Is there a change in selected item?
                            if (_trackItem != i)
                            {
                                // We are currently selecting an item
                                if (_selected)
                                {
                                    if (_popupMenu != null)
                                    {
                                        // Note that we are dismissing the submenu but not removing
                                        // the selection of items, this flag is tested by the routine
                                        // that will return because the submenu has been finished
                                        _dismissTransfer = true;

                                        // Dismiss the submenu
                                        _popupMenu.Dismiss();
		
                                        // Default to downward drawing
                                        _drawUpwards = false;
                                    }

                                    // Modify the display of the two items 
                                    _trackItem = SwitchTrackingItem(_trackItem, i);

                                    // Does the newly selected item have a submenu?
                                    if (dc.Chevron || (dc.MenuCommand.MenuCommands.Count > 0))	
                                        User32.PostMessage(this.Handle, WM_OPERATEMENU, 1, 0);
                                }
                                else
                                {
                                    // Modify the display of the two items 
                                    _trackItem = SwitchTrackingItem(_trackItem, i);
                                }
                            }

                            break;
                        }
                    }

                    // If not in selected mode
                    if (!_selected)
                    {
                        // None of the commands match?
                        if (i == _drawCommands.Count)
                        {
                            // If we have the focus then do not change the tracked item
                            if (!_manualFocus)
                            {
                                // Modify the display of the two items 
                                _trackItem = SwitchTrackingItem(_trackItem, -1);
                            }
                        }
                    }
                }
            }
        }

		/// <summary>
		/// Raises the MouseLeave event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseOver = false;

            // If we manually grabbed focus then do not switch
            // selection when the mouse leaves the control area
            if (!_manualFocus)
            {
                if (_trackItem != -1)
                {
                    // If an item is selected then do not change tracking item when the 
                    // mouse leaves the control area, as a popup menu might be showing and 
                    // so keep the tracking and selection indication visible
                    if (_selected == false)
                        _trackItem = SwitchTrackingItem(_trackItem, -1);
                }
            }

            base.OnMouseLeave(e);
        }

		/// <summary>
		/// Raises the Resize event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            Recalculate();

            // Any resize of control should redraw all of it otherwise when you 
            // stretch to the right it will not paint correctly as we have a one
            // pixel gap between text and min button which is not honoured otherwise
            this.Invalidate();

            base.OnResize(e);
        }

        internal void DrawSelectionUpwards()
        {
            // Double check the state is correct for this method to be called
            if ((_trackItem != -1) && (_selected))
            {
                // This flag is tested in the DrawCommand method
                _drawUpwards = true;

                // Force immediate redraw of the item
                DrawCommand(_trackItem, true);
            }
        }

		/// <summary>
		/// Process the menu control being deselected.
		/// </summary>
        protected void Deselect()
        {
            // The next submenu should be animated
            _animateFirst = true;

            // Remove selection state
            _selected = false;
            
            // Should expansion items be reset on deselection?
            if (_deselectReset)
            {
                // Set everything to expanded
                SetAllCommandsExpansion(_menuCommands, false);
            }
        }

		/// <summary>
		/// Perform reprocessing of child commands.
		/// </summary>
        protected void Recalculate()
        {
            int length;

            if (_direction == LayoutDirection.Horizontal)
                length = this.Width;
            else 
                length = this.Height;

            // Is there space for any commands?
            if (length > 0)
            {
                // Count the number of rows needed
                int rows = 0;

                // Number of items on this row
                int columns = 0;

                // Create a collection of drawing objects
                _drawCommands = new ArrayList();

                // Minimum length is a gap on either side of the text
                int cellMinLength = _lengthGap * 2;

                // Each cell is as broad as the whole control
                int cellBreadth = this.Height;
				
                // Accumulate starting position of each cell
                int lengthStart = 0;

				// Allow enough space to draw a chevron
                length -= (cellMinLength + _chevronLength);

                bool showPendant = (_allowPendant && (rows == 0) && (_activeChild != null));

				// If we are showing on a single line but the active child is not 
				// currently maximized then we can show a menu item in pendant space
				if (showPendant && !_multiLine && (_activeChild.WindowState != FormWindowState.Maximized))
					showPendant = false;

                // Pendant positioning information
                int xPos = 0; 
                int yPos = 0;
                int xInc = 0;
                int yInc = 0;

                // First line needs extra space for pendant
                if (showPendant)
                {
                    length -= (_pendantLength + _pendantOffset + _shadowGap);

                    bool popupStyle = (_style == VisualStyle.IDE) || 
						              (_style == VisualStyle.IDE2005) ||
						              (_style == VisualStyle.Office2003);
                    int borderWidth = popupStyle ? 1 : 2;

                    // Define the correct visual style
                    _minButton.Style = _style;
                    _restoreButton.Style = _style;
                    _closeButton.Style = _style;

					// Make sure the gradiant back drawing matches
					_minButton.IDE2005GradBack = IDE2005GradBack;
					_restoreButton.IDE2005GradBack = IDE2005GradBack;
					_closeButton.IDE2005GradBack = IDE2005GradBack;
					_minButton.Office2003GradBack = Office2003GradBack;
					_restoreButton.Office2003GradBack = Office2003GradBack;
					_closeButton.Office2003GradBack = Office2003GradBack;

                    if (_direction == LayoutDirection.Horizontal)
                    {
                        xPos = this.Width - _pendantOffset - _buttonLength;
                        yPos = _pendantOffset;
                        xInc = -_buttonLength;
                    }
                    else
                    {
                        xPos = _pendantOffset;
                        yPos = this.Height - _pendantOffset - _buttonLength;
                        yInc = -_buttonLength;
                    }
                }

                // Assume chevron is not needed by default
                _chevronStartCommand = null;

                using(Graphics g = this.CreateGraphics())
                {
                    // Count the item we are processing
                    int index = 0;

                    foreach(MenuCommand command in _menuCommands)
                    {
                        // Give the command a chance to update its state
                        command.OnUpdate(EventArgs.Empty);

                        // Ignore items that are marked as hidden
                        if (!command.Visible)
                            continue;

                        int cellLength = 0;

                        // Is this a separator?
                        if (command.Text == "-")
                            cellLength = _separatorWidth;
                        else
                        {
                            // Calculate the text width of the cell
                            SizeF dimension = g.MeasureString(command.Text, this.Font);

                            // Always add 1 to ensure that rounding is up and not down
                            cellLength = cellMinLength + (int)dimension.Width + 1;
                        }

                        Rectangle cellRect;

                        // Create a new position rectangle
                        if (_direction == LayoutDirection.Horizontal)
                            cellRect = new Rectangle(lengthStart, _rowHeight * rows, cellLength, _rowHeight);
                        else
                            cellRect = new Rectangle(_rowWidth * rows, lengthStart, _rowWidth, cellLength);

                        lengthStart += cellLength;
                        columns++;

                        // If this item is overlapping the control edge and it is not the first
                        // item on the line then we should wrap around to the next row.
                        if ((lengthStart > length) && (columns > 1))
                        {
                            if (_multiLine)
                            {
                                // Move to next row
                                rows++;

                                // Reset number of items on this column
                                columns = 1;

                                // Reset starting position of next item
                                lengthStart = cellLength;

                                // Reset position of this item
                                if (_direction == LayoutDirection.Horizontal)
                                {
                                    cellRect.X = 0;
                                    cellRect.Y += _rowHeight;
                                }
                                else
                                {
                                    cellRect.X += _rowWidth;
                                    cellRect.Y = 0;
                                }

                                // Only the first line needs extra space for pendant
                                if (showPendant && (rows == 1))
                                    length += (_pendantLength + _pendantOffset);
                            }
                            else
                            {
                                // Is a tracked item being make invisible
                                if (index <= _trackItem)
                                {
                                    // Need to remove tracking of this item
                                    RemoveItemTracking();
                                }

                                // Remember which item is first for the chevron submenu
                                _chevronStartCommand = command;

                                if (_direction == LayoutDirection.Horizontal)
                                {
                                    cellRect.Y = 0;
                                    cellRect.Width = cellMinLength + _chevronLength;
                                    cellRect.X = this.Width - cellRect.Width;
                                    cellRect.Height = _rowHeight;
                                    xPos -= cellRect.Width;
                                }
                                else
                                {
                                    cellRect.X = 0;
                                    cellRect.Height = cellMinLength + _chevronLength;
                                    cellRect.Y = this.Height - (cellMinLength + _chevronLength);
                                    cellRect.Width = _rowWidth;
                                    yPos -= cellRect.Height;
                                }

                                // Create a draw command for this chevron
                                _drawCommands.Add(new DrawCommand(cellRect));

                                // Exit, do not add the current item or any afterwards
                                break;
                            }
                        }

						Rectangle selectRect = cellRect;

						// Selection rectangle differs from drawing rectangle with IDE, because pressing the
						// mouse down causes the menu to appear and because the popup menu appears drawn slightly
						// over the drawing area the mouse up might select the first item in the popup. 
						if ((_style == VisualStyle.IDE) || 
							(_style == VisualStyle.IDE2005) ||
							(_style == VisualStyle.Office2003))
						{
							// Modify depending on orientation
							if (_direction == LayoutDirection.Horizontal)
								selectRect.Height -= (_lengthGap + 2);
							else
								selectRect.Width -= _breadthGap;
						}

                        // Create a drawing object
                        _drawCommands.Add(new DrawCommand(command, cellRect, selectRect));

                        index++;
                    }
                }

                // Position the pendant buttons
                if (showPendant)
                {
                    if (_activeChild.WindowState == FormWindowState.Maximized)
                    {
                        // Window maximzied, must show the buttons
                        if (!_minButton.Visible)
                        {
                            _minButton.Show();
                            _restoreButton.Show();
                            _closeButton.Show();
                        }
	
                        // Only enabled minimize box if child is allowed to be minimized
                        _minButton.Enabled = _activeChild.MinimizeBox;

                        _closeButton.Location = new Point(xPos, yPos);

                        xPos += xInc; yPos += yInc;
                        _restoreButton.Location = new Point(xPos, yPos);

                        xPos += xInc; yPos += yInc;
                        _minButton.Location = new Point(xPos, yPos);
                    }
                    else
                    {
                        // No window is maximized, so hide the buttons
                        if (_minButton.Visible)
                        {
                            _minButton.Hide();
                            _restoreButton.Hide();
                            _closeButton.Hide();
                        }
                    }
                }
                else
                {
                    // No window is maximized, so hide the buttons
                    if ((_minButton != null) && _minButton.Visible)
                    {
                        _minButton.Hide();
                        _restoreButton.Hide();
                        _closeButton.Hide();
                    }
                }

                if (_direction == LayoutDirection.Horizontal)
                {
                    int controlHeight = (rows + 1) * _rowHeight;

                    // Ensure the control is the correct height
                    if (this.Height != controlHeight)
                        this.Height = controlHeight;
                }
                else
                {
                    int controlWidth = (rows + 1) * _rowWidth;

                    // Ensure the control is the correct width
                    if (this.Width != controlWidth)
                        this.Width = controlWidth;
                }				
            }
        }

		/// <summary>
		/// Handle drawing a single command.
		/// </summary>
		/// <param name="drawItem">Index of command to be drawn.</param>
		/// <param name="tracked">Show the command be shown as hot tracked.</param>
        protected void DrawCommand(int drawItem, bool tracked)
        {
            // Create a graphics object for drawing
            using(Graphics g = this.CreateGraphics())
                DrawSingleCommand(g, _drawCommands[drawItem] as DrawCommand, tracked);
        }

        internal void DrawSingleCommand(Graphics g, DrawCommand dc, bool tracked)
        {
            Rectangle drawRect = dc.DrawRect;
            MenuCommand mc = dc.MenuCommand;

            // Copy the rectangle used for drawing cell
            Rectangle shadowRect = drawRect;

            // Expand to right and bottom to cover the area used to draw shadows
            shadowRect.Width += _shadowGap;
            shadowRect.Height += _shadowGap;

            if (!dc.Separator)
            {
                Rectangle textRect;

                // Text rectangle size depends on type of draw command we are drawing
                if (dc.Chevron)
                {
                    // Create chevron drawing rectangle
                    textRect = new Rectangle(drawRect.Left + _lengthGap, drawRect.Top + _boxExpandUpper,
                                             drawRect.Width - _lengthGap * 2, drawRect.Height - (_boxExpandUpper * 2));
                }
                else
                {
                    // Create text drawing rectangle
                    textRect = new Rectangle(drawRect.Left + _lengthGap, drawRect.Top + _lengthGap,
                                             drawRect.Width - _lengthGap * 2, drawRect.Height - _lengthGap * 2);
                }

                if (dc.Enabled)
                {
                    // Draw selection 
                    if (tracked)
                    {
                        Rectangle boxRect;

                        // Create the rectangle for box around the text
                        if (_direction == LayoutDirection.Horizontal)
                        {
                            boxRect = new Rectangle(textRect.Left - _boxExpandSides,
                                                    textRect.Top - _boxExpandUpper,
                                                    textRect.Width + _boxExpandSides * 2,
                                                    textRect.Height + _boxExpandUpper);
                        }
                        else
                        {					
                            if (!dc.Chevron)
                            {
                                boxRect = new Rectangle(textRect.Left,
                                                        textRect.Top - _boxExpandSides,
                                                        textRect.Width - _boxExpandSides,
                                                        textRect.Height + _boxExpandSides * 2);
                            }
                            else
                                boxRect = textRect;
                        }

                        switch(_style)
                        {
                            case VisualStyle.IDE:
                                if (_selected)
                                {
                                    // Fill the entire inside
                                    g.FillRectangle(Brushes.White, boxRect);
                                    g.FillRectangle(_controlLBrush, boxRect);
								
                                    Color extraColor = Color.FromArgb(64, 0, 0, 0);
                                    Color darkColor = Color.FromArgb(48, 0, 0, 0);
                                    Color lightColor = Color.FromArgb(0, 0, 0, 0);
                
                                    int rightLeft = boxRect.Right + 1;
                                    int rightBottom = boxRect.Bottom;

									if (_drawUpwards && (_direction == LayoutDirection.Horizontal))                                    
									{
                                        // Draw the box around the selection area
                                        using(Pen dark = new Pen(_selectedDarkBackColor))
                                            g.DrawRectangle(dark, boxRect);

										if (dc.SubMenu)
										{
											// Right shadow
											int rightTop = boxRect.Top;
											int leftLeft = boxRect.Left + _shadowGap;

											// Bottom shadow
											int top = boxRect.Bottom + 1;
											int left = boxRect.Left + _shadowGap;
											int width = boxRect.Width + 1;
											int height = _shadowGap;

											Brush rightShadow;
											Brush bottomLeftShadow;
											Brush bottomShadow;
											Brush bottomRightShadow;

											// Decide if we need to use an alpha brush
											if (_supportsLayered && !_256Colors && _allowLayered)
											{
												// Create brushes
												rightShadow = new LinearGradientBrush(new Point(rightLeft, 9999),
																					  new Point(rightLeft + _shadowGap, 9999),
																					  darkColor, lightColor);

												bottomLeftShadow = new LinearGradientBrush(new Point(left + _shadowGap, top - _shadowGap),
																						   new Point(left, top + height),
																						   extraColor, lightColor);

												bottomRightShadow = new LinearGradientBrush(new Point(left + width - _shadowGap - 2, top - _shadowGap - 2),
																							new Point(left + width, top + height),
																							extraColor, lightColor);

												bottomShadow = new LinearGradientBrush(new Point(9999, top),
																					   new Point(9999, top + height),
																					   darkColor, lightColor);
											}
											else
											{
												rightShadow = new SolidBrush(SystemColors.ControlDark);
												bottomLeftShadow = rightShadow;
												bottomShadow = rightShadow;
												bottomRightShadow = rightShadow;
											}

											// Draw each part of the shadow area
											g.FillRectangle(rightShadow, new Rectangle(rightLeft, rightTop, _shadowGap,  rightBottom - rightTop + 1));
											g.FillRectangle(bottomLeftShadow, left, top, _shadowGap, height);
											g.FillRectangle(bottomRightShadow, left + width - _shadowGap, top, _shadowGap, height);
											g.FillRectangle(bottomShadow, left + _shadowGap, top, width - _shadowGap * 2, height);

											// Dispose of brush objects		
											if (_supportsLayered && !_256Colors && _allowLayered)
											{
												rightShadow.Dispose();
												bottomLeftShadow.Dispose();
												bottomShadow.Dispose();
												bottomRightShadow.Dispose();
											}
											else
												rightShadow.Dispose();
										}
                                    }
                                    else
                                    {
                                        // Draw the box around the selection area
                                        using(Pen dark = new Pen(_selectedDarkBackColor))
                                            g.DrawRectangle(dark, boxRect);

										if (dc.SubMenu)
										{
											if (_direction == LayoutDirection.Horizontal)
											{
												// Remove the bottom line of the selection area
												g.DrawLine(Pens.White, boxRect.Left, boxRect.Bottom, boxRect.Right, boxRect.Bottom);
												g.DrawLine(_controlLPen, boxRect.Left, boxRect.Bottom, boxRect.Right, boxRect.Bottom);

												int rightTop = boxRect.Top + _shadowYOffset;

												Brush shadowBrush;

												// Decide if we need to use an alpha brush
												if (_allowLayered && !_256Colors && _supportsLayered && (_style == VisualStyle.IDE))
												{
													using(LinearGradientBrush topBrush = new LinearGradientBrush(new Point(rightLeft - _shadowGap, rightTop + _shadowGap), 
																												 new Point(rightLeft + _shadowGap, rightTop),
																												 extraColor, lightColor))
													{
														g.FillRectangle(topBrush, new Rectangle(rightLeft, rightTop, _shadowGap, _shadowGap));
                        
														rightTop += _shadowGap;
													}

													shadowBrush = new LinearGradientBrush(new Point(rightLeft, 9999), 
																						  new Point(rightLeft + _shadowGap, 9999),
																						  darkColor, lightColor);
												}
												else
													shadowBrush = new SolidBrush(SystemColors.ControlDark);

												g.FillRectangle(shadowBrush, new Rectangle(rightLeft, rightTop, _shadowGap, rightBottom - rightTop));

												shadowBrush.Dispose();
											}
											else
											{
												// Remove the right line of the selection area
												g.DrawLine(Pens.White, boxRect.Right, boxRect.Top, boxRect.Right, boxRect.Bottom);
												g.DrawLine(_controlLPen, boxRect.Right, boxRect.Top, boxRect.Right, boxRect.Bottom);

												int leftLeft = boxRect.Left + _shadowYOffset;

												Brush shadowBrush;

												// Decide if we need to use an alpha brush
												if (_allowLayered && !_256Colors && _supportsLayered && ((_style == VisualStyle.IDE) || (_style == VisualStyle.IDE)))
												{
													using(LinearGradientBrush topBrush = new LinearGradientBrush(new Point(leftLeft + _shadowGap, rightBottom + 1 - _shadowGap), 
																												 new Point(leftLeft, rightBottom + 1 + _shadowGap),
																												 extraColor, lightColor))
													{
														g.FillRectangle(topBrush, new Rectangle(leftLeft, rightBottom + 1, _shadowGap, _shadowGap));
                        
														leftLeft += _shadowGap;
													}

													shadowBrush = new LinearGradientBrush(new Point(9999, rightBottom + 1), 
																						  new Point(9999, rightBottom + 1 + _shadowGap),
																						  darkColor, lightColor);
												}
												else
													shadowBrush = new SolidBrush(SystemColors.ControlDark);

												g.FillRectangle(shadowBrush, new Rectangle(leftLeft, rightBottom + 1, rightBottom - leftLeft - _shadowGap, _shadowGap));

												shadowBrush.Dispose();
											}
										}
                                    }
                                }
                                else
                                {
                                    using (Pen selectPen = new Pen(_highlightBackDark))
                                    {
                                        // Draw the selection area in white so can alpha draw over the top
                                        g.FillRectangle(Brushes.White, boxRect);

                                        using (SolidBrush selectBrush = new SolidBrush(_highlightBackLight))
                                        {
                                            // Draw the selection area
                                            g.FillRectangle(selectBrush, boxRect);

                                            // Draw a border around the selection area
                                            g.DrawRectangle(selectPen, boxRect);
                                        }
                                    }
                                }
                                break;
                            case VisualStyle.Office2003:
                            case VisualStyle.IDE2005:
                                if (_selected)
                                {
									// Fill the entire inside
									if (((_style == VisualStyle.Office2003) ||
										 (_style == VisualStyle.IDE2005)) && _defaultHighlightBackColor)
									{
										using(Brush openBrush = new LinearGradientBrush(boxRect, _colorDetails.OpenBaseColor1, _colorDetails.OpenBaseColor2, 90))
											g.FillRectangle(openBrush, boxRect);
									}
									else
									{
										g.FillRectangle(Brushes.White, boxRect);
										g.FillRectangle(_controlLBrush, boxRect);
									}
								
                                    Color extraColor = Color.FromArgb(64, 0, 0, 0);
                                    Color darkColor = Color.FromArgb(48, 0, 0, 0);
                                    Color lightColor = Color.FromArgb(0, 0, 0, 0);
                
                                    int rightLeft = boxRect.Right + 1;
                                    int rightBottom = boxRect.Bottom;

									if (_drawUpwards && (_direction == LayoutDirection.Horizontal))                                    
									{
										// Draw the box around the selection area
										if (((_style == VisualStyle.Office2003) ||
											 (_style == VisualStyle.IDE2005)) && _defaultHighlightBackColor)
										{
											using(Pen dark = new Pen(_colorDetails.OpenBorderColor))
												g.DrawRectangle(dark, boxRect);
										}
										else
										{
											using(Pen dark = new Pen(_selectedDarkBackColor))
												g.DrawRectangle(dark, boxRect);
										}

										if (dc.SubMenu)
										{
											// Right shadow
											int rightTop = boxRect.Top;
											int leftLeft = boxRect.Left + _shadowGap;

											// Bottom shadow
											int top = boxRect.Bottom + 1;
											int left = boxRect.Left + _shadowGap;
											int width = boxRect.Width + 1;
											int height = _shadowGap;

											Brush rightShadow;
											Brush bottomLeftShadow;
											Brush bottomShadow;
											Brush bottomRightShadow;

											// Decide if we need to use an alpha brush
											if (_supportsLayered && !_256Colors && _allowLayered)
											{
												// Create brushes
												rightShadow = new LinearGradientBrush(new Point(rightLeft, 9999),
																					  new Point(rightLeft + _shadowGap, 9999),
																					  darkColor, lightColor);

												bottomLeftShadow = new LinearGradientBrush(new Point(left + _shadowGap, top - _shadowGap),
																						   new Point(left, top + height),
																						   extraColor, lightColor);

												bottomRightShadow = new LinearGradientBrush(new Point(left + width - _shadowGap - 2, top - _shadowGap - 2),
																							new Point(left + width, top + height),
																							extraColor, lightColor);

												bottomShadow = new LinearGradientBrush(new Point(9999, top),
																					   new Point(9999, top + height),
																					   darkColor, lightColor);
											}
											else
											{
												rightShadow = new SolidBrush(SystemColors.ControlDark);
												bottomLeftShadow = rightShadow;
												bottomShadow = rightShadow;
												bottomRightShadow = rightShadow;
											}

											// Draw each part of the shadow area
											g.FillRectangle(rightShadow, new Rectangle(rightLeft, rightTop, _shadowGap,  rightBottom - rightTop + 1));
											g.FillRectangle(bottomLeftShadow, left, top, _shadowGap, height);
											g.FillRectangle(bottomRightShadow, left + width - _shadowGap, top, _shadowGap, height);
											g.FillRectangle(bottomShadow, left + _shadowGap, top, width - _shadowGap * 2, height);

											// Dispose of brush objects		
											if (_supportsLayered && !_256Colors && _allowLayered)
											{
												rightShadow.Dispose();
												bottomLeftShadow.Dispose();
												bottomShadow.Dispose();
												bottomRightShadow.Dispose();
											}
											else
												rightShadow.Dispose();
										}
                                    }
                                    else
                                    {
										// Draw the box around the selection area
										if (((_style == VisualStyle.Office2003) ||
											 (_style == VisualStyle.IDE2005) && _defaultHighlightBackColor))
										{
											using(Pen dark = new Pen(_colorDetails.OpenBorderColor))
												g.DrawRectangle(dark, boxRect);
										}
										else
										{
											using(Pen dark = new Pen(_selectedDarkBackColor))
												g.DrawRectangle(dark, boxRect);
										}

										if (dc.SubMenu)
										{
											if (_direction == LayoutDirection.Horizontal)
											{
												// Remove the bottom line of the selection area
												g.DrawLine(Pens.White, boxRect.Left, boxRect.Bottom, boxRect.Right, boxRect.Bottom);
												g.DrawLine(_controlLPen, boxRect.Left, boxRect.Bottom, boxRect.Right, boxRect.Bottom);

												int rightTop = boxRect.Top + _shadowYOffset;

												Brush shadowBrush;

												// Decide if we need to use an alpha brush
												if (_allowLayered && !_256Colors && _supportsLayered && ((_style == VisualStyle.IDE) || 
																										 (_style == VisualStyle.IDE2005) ||
																										 (_style == VisualStyle.Office2003)))
												{
													using(LinearGradientBrush topBrush = new LinearGradientBrush(new Point(rightLeft - _shadowGap, rightTop + _shadowGap), 
																												 new Point(rightLeft + _shadowGap, rightTop),
																												 extraColor, lightColor))
													{
														g.FillRectangle(topBrush, new Rectangle(rightLeft, rightTop, _shadowGap, _shadowGap));
                        
														rightTop += _shadowGap;
													}

													shadowBrush = new LinearGradientBrush(new Point(rightLeft, 9999), 
																						  new Point(rightLeft + _shadowGap, 9999),
																						  darkColor, lightColor);
												}
												else
													shadowBrush = new SolidBrush(SystemColors.ControlDark);

												g.FillRectangle(shadowBrush, new Rectangle(rightLeft, rightTop, _shadowGap, rightBottom - rightTop));

												shadowBrush.Dispose();
											}
											else
											{
												// Remove the right line of the selection area
												g.DrawLine(Pens.White, boxRect.Right, boxRect.Top, boxRect.Right, boxRect.Bottom);
												g.DrawLine(_controlLPen, boxRect.Right, boxRect.Top, boxRect.Right, boxRect.Bottom);

												int leftLeft = boxRect.Left + _shadowYOffset;

												Brush shadowBrush;

												// Decide if we need to use an alpha brush
												if (_allowLayered && !_256Colors && _supportsLayered && (_style == VisualStyle.IDE))
												{
													using(LinearGradientBrush topBrush = new LinearGradientBrush(new Point(leftLeft + _shadowGap, rightBottom + 1 - _shadowGap), 
																												 new Point(leftLeft, rightBottom + 1 + _shadowGap),
																												 extraColor, lightColor))
													{
														g.FillRectangle(topBrush, new Rectangle(leftLeft, rightBottom + 1, _shadowGap, _shadowGap));
                        
														leftLeft += _shadowGap;
													}

													shadowBrush = new LinearGradientBrush(new Point(9999, rightBottom + 1), 
																						  new Point(9999, rightBottom + 1 + _shadowGap),
																						  darkColor, lightColor);
												}
												else
													shadowBrush = new SolidBrush(SystemColors.ControlDark);

												g.FillRectangle(shadowBrush, new Rectangle(leftLeft, rightBottom + 1, rightBottom - leftLeft - _shadowGap, _shadowGap));

												shadowBrush.Dispose();
											}
										}
                                    }
                                }
                                else
                                {
									if (((_style == VisualStyle.Office2003) ||
										 (_style == VisualStyle.IDE2005) && _defaultHighlightBackColor))
									{
										// Fill the area in the gradient hot tracking colours
										using(Brush trackBrush = new LinearGradientBrush(boxRect, _colorDetails.TrackLightColor1, _colorDetails.TrackLightColor2, 90))
											g.FillRectangle(trackBrush, boxRect);
											
										// Now draw the border around the area
										using(Pen trackPen = new Pen(_colorDetails.TrackDarkColor))
											g.DrawRectangle(trackPen, boxRect);
									}
									else
									{
										// Draw the selection area in white so can alpha draw over the top
										g.FillRectangle(Brushes.White, boxRect);

										// Draw the selection area
										using (SolidBrush selectBrush = new SolidBrush(_highlightBackLight))
											g.FillRectangle(selectBrush, boxRect);

										// Draw a border around the selection area
										using (Pen selectPen = new Pen(_highlightBackDark))
											g.DrawRectangle(selectPen, boxRect);
									}
                                }
                                break;
                            case VisualStyle.Plain:
                                if (_plainAsBlock)
                                {
                                    using (SolidBrush selectBrush = new SolidBrush(_highlightBackDark))
                                        g.FillRectangle(selectBrush, drawRect);
                                }
                                else
                                {
                                    if (_selected)
                                    {
                                        using(Pen lighlight = new Pen(ControlPaint.LightLight(this.BackColor)),
                                                  dark = new Pen(ControlPaint.DarkDark(this.BackColor)))
                                        {                                            
                                            g.DrawLine(dark, boxRect.Left, boxRect.Bottom, boxRect.Left, boxRect.Top);
                                            g.DrawLine(dark, boxRect.Left, boxRect.Top, boxRect.Right, boxRect.Top);
                                            g.DrawLine(lighlight, boxRect.Right, boxRect.Top, boxRect.Right, boxRect.Bottom);
                                            g.DrawLine(lighlight, boxRect.Right, boxRect.Bottom, boxRect.Left, boxRect.Bottom);
                                        }
                                    }
                                    else
                                    {
                                        using(Pen lighlight = new Pen(ControlPaint.LightLight(this.BackColor)),
                                                  dark = new Pen(ControlPaint.DarkDark(this.BackColor)))
                                        {
                                            g.DrawLine(lighlight, boxRect.Left, boxRect.Bottom, boxRect.Left, boxRect.Top);
                                            g.DrawLine(lighlight, boxRect.Left, boxRect.Top, boxRect.Right, boxRect.Top);
                                            g.DrawLine(dark, boxRect.Right, boxRect.Top, boxRect.Right, boxRect.Bottom);
                                            g.DrawLine(dark, boxRect.Right, boxRect.Bottom, boxRect.Left, boxRect.Bottom);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }

                if (dc.Chevron)
                {
                    // Draw the chevron image in the centre of the text area
                    int yPos = drawRect.Top;
                    int xPos = drawRect.X + ((drawRect.Width - _chevronLength) / 2);

                    // When selected...
                    if (_selected)
                    {
                        // ...offset down and to the right
                        xPos += 1;
                        yPos += 1;
                    }

					Image drawImage = _menuImages.Images[_chevronIndex];
                    g.DrawImage(drawImage, xPos, yPos);
					drawImage.Dispose();
                }
                else
                {	
                    // Left align the text drawing on a single line centered vertically
                    // and process the & character to be shown as an underscore on next character
                    StringFormat format = new StringFormat();
                    format.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap;
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    format.HotkeyPrefix = HotkeyPrefix.Show;

                    if (_direction == LayoutDirection.Vertical)
                        format.FormatFlags |= StringFormatFlags.DirectionVertical;

                    if (dc.Enabled && this.Enabled)
                    {
                        if (tracked && (_style == VisualStyle.Plain) && _plainAsBlock)
                        {
                            // Is the item selected as well as tracked?
                            if (_selected)
                            {
                                // Offset to show it is selected
                                textRect.X += 2;
                                textRect.Y += 2;
                            }

                            using (SolidBrush textBrush = new SolidBrush(_plainSelectedTextColor))
                                g.DrawString(mc.Text, this.Font, textBrush, textRect, format);
                        }
                        else
						{
                            if (_selected && tracked)
                            {
                                using (SolidBrush textBrush = new SolidBrush(_selectedTextColor))
                                    g.DrawString(mc.Text, this.Font, textBrush, textRect, format);
                            }
                            else
                            {
                                if (tracked)
                                {
									using (SolidBrush textBrush = new SolidBrush(_highlightTextColor))
										g.DrawString(mc.Text, this.Font, textBrush, textRect, format);
                                }
                                else
                                {
                                    using (SolidBrush textBrush = new SolidBrush(_textColor))
                                        g.DrawString(mc.Text, this.Font, textBrush, textRect, format);
                                }
                            }
						}
                    }
                    else 
                    {
                        // Helper values used when drawing grayed text in plain style
                        Rectangle rectDownRight = textRect;
                        rectDownRight.Offset(1,1);

                        // Draw the text offset down and right
                        g.DrawString(mc.Text, this.Font, Brushes.White, rectDownRight, format);

                        // Draw then text offset up and left
                        using (SolidBrush grayBrush = new SolidBrush(SystemColors.GrayText))
                            g.DrawString(mc.Text, this.Font, grayBrush, textRect, format);
                    }

					format.Dispose();
                }
            }
        }

		/// <summary>
		/// Handle drawing all the child commands.
		/// </summary>
		/// <param name="g">Graphics object to use in drawing.</param>
        protected void DrawAllCommands(Graphics g)
        {
            for(int i=0; i<_drawCommands.Count; i++)
            {
                // Grab some commonly used values				
                DrawCommand dc = _drawCommands[i] as DrawCommand;

                // Draw this command only
                DrawSingleCommand(g, dc, (i == _trackItem));
            }
        }

		/// <summary>
		/// Switch hot tracking from old to new item.
		/// </summary>
		/// <param name="oldItem">Index of old hot tracking item.</param>
		/// <param name="newItem">Index of new hot tracking item.</param>
		/// <returns>Index of new hot tracking item.</returns>
        protected int SwitchTrackingItem(int oldItem, int newItem)
        {
            // Create a graphics object for drawinh
            using(Graphics g = this.CreateGraphics())
            {
                // Deselect the old draw command
                if (oldItem != -1)
                {
                    DrawCommand dc = _drawCommands[oldItem] as DrawCommand;

                    // Draw old item not selected
                    DrawSingleCommand(g, _drawCommands[oldItem] as DrawCommand, false);

                    // Generate an unselect event
                    if (dc.MenuCommand != null)
                        OnDeselected(dc.MenuCommand);
                }

                _trackItem = newItem;

                // Select the new draw command
                if (_trackItem != -1)
                {
                    DrawCommand dc = _drawCommands[_trackItem] as DrawCommand;

                    // Draw new item selected
                    DrawSingleCommand(g, _drawCommands[_trackItem] as DrawCommand, true);
					
                    // Generate an select event
                    if (dc.MenuCommand != null)
                        OnSelected(dc.MenuCommand);
                }

                // Request redraw of all items to prevent strange bug where some items
                // never get redrawn correctly and so leave blank spaces when using the
                // mouse/keyboard to shift between popup menus
                Invalidate();
            }

            return _trackItem;
        }

		/// <summary>
		/// Remove tracking of current item.
		/// </summary>
        protected void RemoveItemTracking()
        {
            if (_trackItem != -1)
            {
                DrawCommand dc = _drawCommands[_trackItem] as DrawCommand;

                // Generate an unselect event
                if (dc.MenuCommand != null)
                    OnDeselected(dc.MenuCommand);

                // Remove tracking value
                _trackItem = -1;
            }		
        }

        internal void OperateSubMenu(DrawCommand dc, bool selectFirst, bool trackRemove)
        {
            if (this.IsDisposed)
                return;
                
            Rectangle drawRect = dc.DrawRect;

            // Find screen positions for popup menu
            Point screenPos;
			
            if ((_style == VisualStyle.IDE) || 
				(_style == VisualStyle.IDE2005) ||
				(_style == VisualStyle.Office2003))
            {
                if (_direction == LayoutDirection.Horizontal)
                    screenPos = PointToScreen(new Point(dc.DrawRect.Left + 1, drawRect.Bottom - _lengthGap - 2));
                else
                    screenPos = PointToScreen(new Point(dc.DrawRect.Right - _breadthGap, drawRect.Top + _boxExpandSides - 1));
            }
            else
            {
                if (_direction == LayoutDirection.Horizontal)
                    screenPos = PointToScreen(new Point(dc.DrawRect.Left + 1, drawRect.Bottom));
                else
                    screenPos = PointToScreen(new Point(dc.DrawRect.Right, drawRect.Top));
            }

            Point aboveScreenPos;
			
            if ((_style == VisualStyle.IDE) || 
				(_style == VisualStyle.IDE2005) ||
				(_style == VisualStyle.Office2003))
            {
                if (_direction == LayoutDirection.Horizontal)
                    aboveScreenPos = PointToScreen(new Point(dc.DrawRect.Left + 1, drawRect.Top + _breadthGap + _lengthGap - 1));
                else
                    aboveScreenPos = PointToScreen(new Point(dc.DrawRect.Right - _breadthGap, drawRect.Bottom + _lengthGap + 1));
            }
            else
            {
                if (_direction == LayoutDirection.Horizontal)
                    aboveScreenPos = PointToScreen(new Point(dc.DrawRect.Left + 1, drawRect.Top));
                else
                    aboveScreenPos = PointToScreen(new Point(dc.DrawRect.Right, drawRect.Bottom));
            }

            int borderGap;

            // Calculate the missing gap in the PopupMenu border
            if (_direction == LayoutDirection.Horizontal)
                borderGap = dc.DrawRect.Width - _subMenuBorderAdjust;
            else
                borderGap = dc.DrawRect.Height - _subMenuBorderAdjust;		
	
            _popupMenu = new PopupMenu();

			// Pass on our layered setting
			_popupMenu.AllowLayered = _allowLayered;

            // Define the correct visual style based on ours
            _popupMenu.Style = this.Style;

            // Key direction when keys cause dismissal
            int returnDir = 0;

            // Command selected by the PopupMenu
            MenuCommand returnCommand = null;

            // Should the PopupMenu tell the collection to remember expansion state
            _popupMenu.RememberExpansion = _rememberExpansion;

            // Propogate our highlight setting
            _popupMenu.HighlightInfrequent = _highlightInfrequent;

            // Might need to define custom colors
            if (!_defaultSelectedBackColor)
                _popupMenu.BackColor = _selectedBackColor;
            
            if (!_defaultSelectedTextColor)
                _popupMenu.TextColor = _selectedTextColor;

            if (!_defaultHighlightTextColor)
                _popupMenu.HighlightTextColor = _highlightTextColor;

            if (!_defaultHighlightBackColor)
                _popupMenu.HighlightColor = _highlightBackColor;

			if (!_defaultFont)
				_popupMenu.Font = (Font)base.Font.Clone();

            // Pass on the animation values
            _popupMenu.Animate = _animate;
            _popupMenu.AnimateStyle = _animateStyle;
            _popupMenu.AnimateTime = _animateTime;
    
            if (dc.Chevron)
            {
                MenuCommandCollection mcc = new MenuCommandCollection();

                bool addCommands = false;

                // Generate a collection of menu commands for those not visible
                foreach(MenuCommand command in _menuCommands)
                {
                    if (!addCommands && (command == _chevronStartCommand))
                        addCommands = true;

                    if (addCommands)
                        mcc.Add(command);
                }

                // Track the popup using provided menu item collection
                returnCommand = _popupMenu.TrackPopup(screenPos, 
                                                      aboveScreenPos,
                                                      _direction,
                                                      null,
                                                      mcc, 
                                                      borderGap,
                                                      selectFirst, 
                                                      this,
                                                      _animateFirst,
													  ref returnDir,
													  ref _clickSource);
            }
            else
            {
                // Generate event so that caller has chance to modify MenuCommand contents
                dc.MenuCommand.OnPopupStart();
                
                // Honour the collections request for showing infrequent items
                _popupMenu.ShowInfrequent = dc.MenuCommand.MenuCommands.ShowInfrequent;

                // Track the popup using provided menu item collection
                returnCommand = _popupMenu.TrackPopup(screenPos, 
                                                      aboveScreenPos,
                                                      _direction,
                                                      dc.MenuCommand,
                                                      dc.MenuCommand.MenuCommands, 
                                                      borderGap,
                                                      selectFirst,
                                                      this,
                                                      _animateFirst,
                                                      ref returnDir,
													  ref _clickSource);
            }
            
            // No more animation till simulation ends
            _animateFirst = false;

            // If we are supposed to expand all items at the same time
            if (_expandAllTogether)
            {   
                // Is anything we have shown now in the expanded state
                if (AnythingExpanded(_menuCommands))
                {
                    // Set everything to expanded
                    SetAllCommandsExpansion(_menuCommands, true);
                }
            }
            
            // Was arrow key not used to dismiss the submenu?
            if (returnDir == 0)
            {
                // The submenu may have eaten the mouse leave event
                _mouseOver = false;

                // Only if the submenu was dismissed at the request of the submenu
                // should the selection mode be cancelled, otherwise keep selection mode
                if (!_dismissTransfer)
                {
                    // This item is no longer selected
                    Deselect();
                    _drawUpwards = false;

                    if (!this.IsDisposed)
                    {
                        // Should we stop tracking this item
                        if (trackRemove)
                        {
			                // Unselect the current item
                            _trackItem = SwitchTrackingItem(_trackItem, -1);
                        }
                        else
                        {
                            if (_trackItem != -1)
                            {
                                // Repaint the item
                                DrawCommand(_trackItem, true);
                            }
                        }
                    }
                }
                else
                {
                    // Do not change _selected status
                    _dismissTransfer = false;
                }
            }

            if (!dc.Chevron)
            {
                // Generate event so that caller has chance to modify MenuCommand contents
                dc.MenuCommand.OnPopupEnd();
            }

            // Spin the message loop so the messages dealing with destroying
            // the PopupMenu window are processed and cause it to disappear from
            // view before events are generated
            Application.DoEvents();

            // Remove unwanted object
			if (_popupMenu != null)
			{
				_popupMenu.Release(false);
				_popupMenu = null;
			}

            // Was arrow key used to dismiss the submenu?
            if (returnDir != 0)
            {
                if (returnDir < 0)
                {
                    // Shift selection left one
                    ProcessMoveLeft(true);
                }
                else
                {
                    // Shift selection right one
                    ProcessMoveRight(true);
                }

                // A WM_MOUSEMOVE is generated when we open up the new submenu for 
                // display, ignore this as it causes the selection to move
                _ignoreMouseMove = true;
            }
            else
            {
                // Was a MenuCommand returned?
                if (returnCommand != null)
                {
					// Remove 

					// Are we simulating having the focus?
					if (_manualFocus)
					{
						// Always return focus to original when a selection is made
						SimulateReturnFocus();
					}

                    // Pulse the selected event for the command
                    returnCommand.OnClick(new MenuClickEventArgs(_clickSource));
                }
            }
        }

		/// <summary>
		/// Return a value indicating if any submenus are expanded.
		/// </summary>
		/// <param name="mcc">Collection of commands to test.</param>
		/// <returns>true if a submenu is expanded; otherwise false.</returns>
        protected bool AnythingExpanded(MenuCommandCollection mcc)
        {
            foreach(MenuCommand mc in mcc)
            {
                if (mc.MenuCommands.ShowInfrequent)
                    return true;
                    
                if (AnythingExpanded(mc.MenuCommands))
                    return true;
            }
            
            return false;
        }
        
		/// <summary>
		/// Recurse to all submenus and set expansion state.
		/// </summary>
		/// <param name="mcc">Collection of commands to process.</param>
		/// <param name="show">New expansion state to set.</param>
        protected void SetAllCommandsExpansion(MenuCommandCollection mcc, bool show)
        {
            foreach(MenuCommand mc in mcc)
            {
                // Set the correct value for this collection
                mc.MenuCommands.ShowInfrequent = show;
                    
                // Recurse into all lower level collections
                SetAllCommandsExpansion(mc.MenuCommands, show);
            }
        }

		/// <summary>
		/// Simulate the appear of taking the focus but without actually doing so.
		/// </summary>
        protected void SimulateGrabFocus()
        {	
			if (!_manualFocus)
			{
				_manualFocus = true;
				_animateFirst = true;

				Form parentForm = this.FindForm();

				if (parentForm != null)
				{
					// Want notification when user selects a different Form
					parentForm.Deactivate += new EventHandler(OnParentDeactivate);
				}

				// Must hide caret so user thinks focus has changed
				bool hideCaret = User32.HideCaret(IntPtr.Zero);

				// Create an object for storing windows message information
				Win32.MSG msg = new Win32.MSG();

				_exitLoop = false;

				// Process messages until exit condition recognised
				while(!_exitLoop)
				{
					// Suspend thread until a windows message has arrived
					if (User32.WaitMessage())
					{
						// Take a peek at the message details without removing from queue
						while(!_exitLoop && User32.PeekMessage(ref msg, 0, 0, 0, (int)Win32.PeekMessageFlags.PM_NOREMOVE))
						{
//							Console.WriteLine("Loop {0} {1}", this.Handle, ((Win32.Msgs)msg.message).ToString());
    
	                        if (User32.GetMessage(ref msg, 0, 0, 0))
                            {
								// Should this method be dispatched?
								if (!ProcessInterceptedMessage(ref msg))
								{
									User32.TranslateMessage(ref msg);
									User32.DispatchMessage(ref msg);
								}
                            }
						}
					}
				}

				if (parentForm != null)
				{
					// Remove notification when user selects a different Form
					parentForm.Deactivate -= new EventHandler(OnParentDeactivate);
				}
				
				// If caret was hidden then show it again now
				if (hideCaret)
					User32.ShowCaret(IntPtr.Zero);

				// We lost the focus
				_manualFocus = false;
			}
        }

		/// <summary>
		/// Simulate returning focus without actually doing so.
		/// </summary>
        protected void SimulateReturnFocus()
        {
			if (_manualFocus)
				_exitLoop = true;

			// Remove any item being tracked
			if (_trackItem != -1)
			{
				// Unselect the current item
				_trackItem = SwitchTrackingItem(_trackItem, -1);
			}
        }

		/// <summary>
		/// Handle the parent Form becoming deactivated.
		/// </summary>
		/// <param name="sender">Source that fired the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected void OnParentDeactivate(object sender, EventArgs e)
		{
			SimulateReturnFocus();
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
		/// <param name="e">A PaintEventArgs that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            DrawAllCommands(e.Graphics);
            base.OnPaint(e);
        }

		/// <summary>
		/// Process attempt to move left one item.
		/// </summary>
		/// <param name="select">Should new item be selected.</param>
        protected void ProcessMoveLeft(bool select)
        {
            if (_popupMenu == null)
            {
                int newItem = _trackItem;
                int startItem = newItem;

                for(int i=0; i<_drawCommands.Count; i++)
                {
                    // Move to previous item
                    newItem--;

                    // Have we looped all the way around all choices
                    if (newItem == startItem)
                        return;

                    // Check limits
                    if (newItem < 0)
                        newItem = _drawCommands.Count - 1;

                    DrawCommand dc = _drawCommands[newItem] as DrawCommand;

                    // Can we select this item?
                    if (!dc.Separator && (dc.Chevron || dc.MenuCommand.Enabled))
                    {
                        // If a change has occured
                        if (newItem != _trackItem)
                        {
                            // Modify the display of the two items 
                            _trackItem = SwitchTrackingItem(_trackItem, newItem);
							
                            if (_selected)
                            {
                                if (dc.Chevron || (dc.MenuCommand.MenuCommands.Count > 0))
                                    User32.PostMessage(this.Handle, WM_OPERATEMENU, 0, 1);
                            }

                            break;
                        }
                    }
                }
            }
        }

		/// <summary>
		/// Process attempt to move right one item.
		/// </summary>
		/// <param name="select">Should new item be selected.</param>
        protected void ProcessMoveRight(bool select)
        {
            if (_popupMenu == null)
            {
                int newItem = _trackItem;
                int startItem = newItem;

                for(int i=0; i<_drawCommands.Count; i++)
                {
                    // Move to previous item
                    newItem++;

                    // Check limits
                    if (newItem >= _drawCommands.Count)
                        newItem = 0;

                    DrawCommand dc = _drawCommands[newItem] as DrawCommand;

                    // Can we select this item?
                    if (!dc.Separator && (dc.Chevron || dc.MenuCommand.Enabled))
                    {
                        // If a change has occured
                        if (newItem != _trackItem)
                        {
                            // Modify the display of the two items 
                            _trackItem = SwitchTrackingItem(_trackItem, newItem);

                            if (_selected)
                            {
                                if (dc.Chevron || (dc.MenuCommand.MenuCommands.Count > 0))
                                    User32.PostMessage(this.Handle, WM_OPERATEMENU, 0, 1);
                            }

                            break;
                        }
                    }
                }
            }
        }

		/// <summary>
		/// Process attempt to enter current command.
		/// </summary>
		protected void ProcessEnter()
		{
            if (_popupMenu == null)
            {
				// Are we tracking an item?
				if (_trackItem != -1)
				{
					// The item must not already be selected
					if (!_selected)
					{
						DrawCommand dc = _drawCommands[_trackItem] as DrawCommand;

						// Is there a submenu to show?
						if (dc.Chevron || (dc.MenuCommand.MenuCommands.Count >= 0))
						{
							// Select the tracked item
							_selected = true;
							_drawUpwards = false;
										
							// Update display to show as selected
							DrawCommand(_trackItem, true);

							// Show the submenu

							if (dc.Chevron || (dc.MenuCommand.MenuCommands.Count > 0))
								User32.PostMessage(this.Handle, WM_OPERATEMENU, 0, 1);
						}
						else
						{
                            // No, pulse the Click event for the command
                            dc.MenuCommand.OnClick(new MenuClickEventArgs(MenuClickEventArgs.ClickSource.Return));

							int item = _trackItem;

							// Not tracking anymore
							RemoveItemTracking();

							// Update display to show as not selected
							DrawCommand(item, false);

							// Finished, so return focus to origin
							SimulateReturnFocus();
						}
					}
					else
					{
                        // Must be showing a submenu less item as selected
						DrawCommand dc = _drawCommands[_trackItem] as DrawCommand;

						// Pulse the event
                        dc.MenuCommand.OnClick(new MenuClickEventArgs(MenuClickEventArgs.ClickSource.Return));

						int item = _trackItem;

						RemoveItemTracking();

						// Not selected anymore
                        Deselect();
                        
						// Update display to show as not selected
						DrawCommand(item, false);

						// Finished, so return focus to origin
						SimulateReturnFocus();
					}
				}
			}
		}

		/// <summary>
		/// Process attempt to move down one item.
		/// </summary>
        protected void ProcessMoveDown()
        {
            if (_popupMenu == null)
            {
                // Are we tracking an item?
                if (_trackItem != -1)
                {
                    // The item must not already be selected
                    if (!_selected)
                    {
                        DrawCommand dc = _drawCommands[_trackItem] as DrawCommand;

                        // Is there a submenu to show?
                        if (dc.Chevron || (dc.MenuCommand.MenuCommands.Count >= 0))
                        {
                            // Select the tracked item
                            _selected = true;
                            _drawUpwards = false;
										
                            // Update display to show as selected
                            DrawCommand(_trackItem, true);

                            // Show the submenu
	                        if (dc.Chevron || (dc.MenuCommand.MenuCommands.Count > 0))
		                        User32.PostMessage(this.Handle, WM_OPERATEMENU, 0, 1);
                        }
                    }
                }
            }
        }

		/// <summary>
		/// Process a key by checking if it represents a mnemonic character.
		/// </summary>
		/// <param name="key">Key to test against.</param>
		/// <returns>true if a submenu should be displayed; otherwise false.</returns>
        protected bool ProcessMnemonicKey(char key)
        {
            // No current selection
            if (!_selected)
            {
                // Search for an item that matches
                for(int i=0; i<_drawCommands.Count; i++)
                {
                    DrawCommand dc = _drawCommands[i] as DrawCommand;

                    // Only interested in enabled items
                    if ((dc.MenuCommand != null) && dc.MenuCommand.Enabled && dc.MenuCommand.Visible)
                    {
                        // Does the character match?
                        if (key == dc.Mnemonic)
                        {
                            // Select the tracked item
                            _selected = true;
                            _drawUpwards = false;
										
                            // Is there a change in tracking?
                            if (_trackItem != i)
                            {
                                // Modify the display of the two items 
                                _trackItem = SwitchTrackingItem(_trackItem, i);
                            }
                            else
                            {
                                // Update display to show as selected
                                DrawCommand(_trackItem, true);
                            }

                            // Is there a submenu to show?
                            if (dc.Chevron || (dc.MenuCommand.MenuCommands.Count >= 0))
							{
	                            if (dc.Chevron || (dc.MenuCommand.MenuCommands.Count > 0))
		                            User32.PostMessage(this.Handle, WM_OPERATEMENU, 0, 1);

	                            return true;
							}
                            else
                            {			
                                // No, pulse the Click event for the command
                                dc.MenuCommand.OnClick(new MenuClickEventArgs(MenuClickEventArgs.ClickSource.Mnemonic));
	
								int item = _trackItem;

								RemoveItemTracking();

								// No longer seleted
                                Deselect();
                                
                                // Update display to show as not selected
                                DrawCommand(item, false);

								// Finished, so return focus to origin
								SimulateReturnFocus();

								return false;
                            }
                        }
                    }
                }
            }

            return false;
        }

		/// <summary>
		/// Handle the user preferences being changed on the local machine.
		/// </summary>
		/// <param name="sender">Source that fired the event.</param>
		/// <param name="e">A UserPreferenceChangedEventArgs containing the event data.</param>
		protected void OnPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			// Are we using the default menu or a user defined value?
			if (_defaultFont)
			{
				DefineFont((Font)SystemInformation.MenuFont.Clone());
				Recalculate();
				Invalidate();
			}
		}

		/// <summary>
		/// Handle the user changing the system colors.
		/// </summary>
		/// <param name="e">An EventArgs containing the event data.</param>
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			if (_defaultBackColor)
				this.BackColor = SystemColors.Control;
			
            if (_defaultHighlightBackColor)
                this.HighlightBackColor = SystemColors.Highlight;

            if (_defaultSelectedBackColor)
                this.SelectedBackColor = SystemColors.Control;
            
            if (_defaultTextColor)
			    _textColor = SystemColors.MenuText;

            if (_defaultHighlightTextColor)
                _highlightTextColor = SystemColors.MenuText;

            if (_defaultSelectedTextColor)
                _selectedTextColor = SystemColors.MenuText;
            
            Recalculate();
            Invalidate();
    
            base.OnSystemColorsChanged(e);
		}
        
		/// <summary>
		/// Process keyboard windows messages before standard control processing.
		/// </summary>
		/// <param name="msg">Windows message information.</param>
		/// <returns>true if message has been processed; otherwise false.</returns>
		public bool PreFilterMessage(ref Message msg)
        {
            Form parentForm = this.FindForm();

			// Should we snoop the incoming message? (default to not)
			bool snoop = false;

			// If the Form containing this MenuControl is the active one and 
			// it has the focus then we always want to snoop its messages
			if ((parentForm != null) && (parentForm == Form.ActiveForm) && parentForm.ContainsFocus)
				snoop = true;
			else
			{
				// If the active form is a floating window that contains docking contents
				if (Form.ActiveForm is Crownwood.DotNetMagic.Docking.FloatingForm)
					snoop = true;
			}

            // Are we allowed to snoop the message?
            if (snoop)
            {		
                switch(msg.Msg)
                {
                    case (int)Win32.Msgs.WM_KEYDOWN:
                        // Ignore keyboard input if the control is disabled
                        if (this.Enabled)
						{
							// We ignore use of the Alt Gr key
							if (((int)msg.LParam & 0x20000000) == 0)
							{
								// Find up/down state of shift and control keys
								ushort shiftKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_SHIFT);
								ushort controlKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_CONTROL);
								ushort altKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_MENU);

								// Basic code we are looking for is the key pressed...
								int code = (int)msg.WParam;

								// ...plus the modifier for SHIFT...
								if (((int)shiftKey & 0x00008000) != 0)
									code += 0x00010000;

								// ...plus the modifier for CONTROL...
								if (((int)controlKey & 0x00008000) != 0)
									code += 0x00020000;

								// ...plus the modifier for ALT
								if (((int)altKey & 0x00008000) != 0)
									code += 0x00040000;

								// Construct shortcut from keystate and keychar
								Shortcut sc = (Shortcut)(code);

								// Search for a matching command
								return GenerateShortcut(sc, _menuCommands);
							}
                        }
                        break;
                    case (int)Win32.Msgs.WM_SYSKEYUP:
                        // Ignore keyboard input if the control is disabled
                        if (this.Enabled)
                        {
							// If this the ALT or F10 key?
                            if (((int)msg.WParam == (int)Win32.VirtualKeys.VK_MENU) ||
							     (((int)msg.WParam == (int)Win32.VirtualKeys.VK_F10) && !_ignoreF10))
                            {
								// If the F10 key...
								if ((int)msg.WParam == (int)Win32.VirtualKeys.VK_F10)
								{
									// Find up/down state of shift and control keys
									ushort shiftKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_SHIFT);
									ushort controlKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_CONTROL);
									ushort altKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_MENU);

									// Basic code we are looking for is the key pressed...
									int code = (int)msg.WParam;

									// ...plus the modifier for SHIFT...
									if (((int)shiftKey & 0x00008000) != 0)
										code += 0x00010000;

									// ...plus the modifier for CONTROL...
									if (((int)controlKey & 0x00008000) != 0)
										code += 0x00020000;

									// ...plus the modifier for ALT
									if (((int)altKey & 0x00008000) != 0)
										code += 0x00040000;

									// Construct shortcut from keystate and keychar
									Shortcut sc = (Shortcut)(code);
									
									// ...then do nothing if it represents a shortcut on its own
									if (IsShortcut(sc, _menuCommands))
										return true;
								}

                                // Are there any menu commands?
                                if (_drawCommands.Count > 0)
                                {
                                    // If no item is currently tracked then...
                                    if (_trackItem == -1)
                                    {
                                        // ...start tracking the first valid command
                                        for(int i=0; i<_drawCommands.Count; i++)
                                        {
                                            DrawCommand dc = _drawCommands[i] as DrawCommand;
											
                                            if (!dc.Separator && (dc.Chevron || dc.MenuCommand.Enabled))
                                            {
                                                _trackItem = SwitchTrackingItem(-1, i);
                                                break;
                                            }
                                        }
                                    }
											
									// Grab the focus for key events						
                                    SimulateGrabFocus();							
                                }
								
								return true;
                            }
                        }
                        break;
                    case (int)Win32.Msgs.WM_SYSKEYDOWN:
                        // Ignore keyboard input if the control is disabled
						if (this.Enabled)
                        {
                            if ((int)msg.WParam != (int)Win32.VirtualKeys.VK_MENU)
                            {
								// Find up/down state of shift and control keys
								ushort shiftKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_SHIFT);
								ushort controlKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_CONTROL);
								ushort altKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_MENU);

								// Basic code we are looking for is the key pressed...
								int code = (int)msg.WParam;

								// ...plus the modifier for SHIFT...
								if (((int)shiftKey & 0x00008000) != 0)
									code += 0x00010000;

								// ...plus the modifier for CONTROL...
								if (((int)controlKey & 0x00008000) != 0)
									code += 0x00020000;

								// ...plus the modifier for ALT
								if (((int)altKey & 0x00008000) != 0)
									code += 0x00040000;

								// Construct shortcut from keystate and keychar
								Shortcut sc = (Shortcut)(code);

								if (GenerateShortcut(sc, _menuCommands))
									return true;
								
                                // Last resort is treat as a potential mnemonic
                                if (ProcessMnemonicKey((char)msg.WParam))
                                {
									if (!_manualFocus)
										SimulateGrabFocus();

									return true;
                                }
							}
                        }
                        break;
                    default:
                        break;
                }
            }

            return false;
        }

		internal bool ProcessInterceptedMessage(ref Win32.MSG msg)
		{
			bool eat = false;
        
			switch(msg.message)
			{
				case (int)Win32.Msgs.WM_LBUTTONDOWN:
				case (int)Win32.Msgs.WM_MBUTTONDOWN:
				case (int)Win32.Msgs.WM_RBUTTONDOWN:
				case (int)Win32.Msgs.WM_XBUTTONDOWN:
				case (int)Win32.Msgs.WM_NCLBUTTONDOWN:
				case (int)Win32.Msgs.WM_NCMBUTTONDOWN:
				case (int)Win32.Msgs.WM_NCRBUTTONDOWN:
					// Mouse clicks cause the end of simulated focus unless they are
					// inside the client area of the menu control itself
					Point pt = new Point( (int)((uint)msg.lParam & 0x0000FFFFU), 
										  (int)(((uint)msg.lParam & 0xFFFF0000U) >> 16));
				
					if (!this.ClientRectangle.Contains(pt))	
						SimulateReturnFocus();
					break;
				case (int)Win32.Msgs.WM_KEYDOWN:
					// Find up/down state of shift and control keys
					ushort shiftKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_SHIFT);
					ushort controlKey = User32.GetKeyState((int)Win32.VirtualKeys.VK_CONTROL);

					// Basic code we are looking for is the key pressed...
					int basecode = (int)msg.wParam;
					int code = basecode;

					// ...plus the modifier for SHIFT...
					if (((int)shiftKey & 0x00008000) != 0)
						code += 0x00010000;

					// ...plus the modifier for CONTROL
					if (((int)controlKey & 0x00008000) != 0)
						code += 0x00020000;

					if (code == (int)Win32.VirtualKeys.VK_ESCAPE)
					{
						// Is an item being tracked
						if (_trackItem != -1)
						{
							// Is it also showing a submenu
							if (_popupMenu == null)
							{
								// Unselect the current item
								_trackItem = SwitchTrackingItem(_trackItem, -1);

							}
						}

						SimulateReturnFocus();

						// Prevent intended destination getting message
						eat = true;
					}
					else if (code == (int)Win32.VirtualKeys.VK_LEFT)
					{
						if (_direction == LayoutDirection.Horizontal)
							ProcessMoveLeft(false);

						if (_selected)
							_ignoreMouseMove = true;

						// Prevent intended destination getting message
						eat = true;
					}
					else if (code == (int)Win32.VirtualKeys.VK_RIGHT)
					{
						if (_direction == LayoutDirection.Horizontal)
							ProcessMoveRight(false);
						else
							ProcessMoveDown();

						if (_selected)
							_ignoreMouseMove = true;

						// Prevent intended destination getting message
						eat = true;
					}
					else if (code == (int)Win32.VirtualKeys.VK_RETURN)
					{
						ProcessEnter();

						// Prevent intended destination getting message
						eat = true;
					}
					else if (code == (int)Win32.VirtualKeys.VK_DOWN)
					{
						if (_direction == LayoutDirection.Horizontal)
							ProcessMoveDown();
						else
							ProcessMoveRight(false);

						// Prevent intended destination getting message
						eat = true;
					}
					else if (code == (int)Win32.VirtualKeys.VK_UP)
					{
						ProcessMoveLeft(false);

						// Prevent intended destination getting message
						eat = true;
					}
					else
					{
						// Construct shortcut from keystate and keychar
						Shortcut sc = (Shortcut)(code);

						// Search for a matching command
						if (!GenerateShortcut(sc, _menuCommands))
						{
							// Last resort is treat as a potential mnemonic
							ProcessMnemonicKey((char)msg.wParam);

							if (_selected)
								_ignoreMouseMove = true;
						}
						else
						{
							SimulateReturnFocus();
						}

						// Always eat keyboard message in simulated focus
						eat = true;
					}
					break;
				case (int)Win32.Msgs.WM_KEYUP:
					eat = true;
					break;
				case (int)Win32.Msgs.WM_SYSKEYUP:
					// Ignore keyboard input if the control is disabled
					if (((int)msg.wParam == (int)Win32.VirtualKeys.VK_MENU)  ||
				        (((int)msg.wParam == (int)Win32.VirtualKeys.VK_F10) && !_ignoreF10))
					{
						bool ignore = false;

						// If the F10 key...
						if ((int)msg.wParam == (int)Win32.VirtualKeys.VK_F10)
						{
							// Find up/down state of shift and control keys
							ushort shiftKeyU = User32.GetKeyState((int)Win32.VirtualKeys.VK_SHIFT);
							ushort controlKeyU = User32.GetKeyState((int)Win32.VirtualKeys.VK_CONTROL);
							ushort altKeyU = User32.GetKeyState((int)Win32.VirtualKeys.VK_MENU);

							// Basic code we are looking for is the key pressed...
							int codeU = (int)msg.wParam;

							// ...plus the modifier for SHIFT...
							if (((int)shiftKeyU & 0x00008000) != 0)
								codeU += 0x00010000;

							// ...plus the modifier for CONTROL...
							if (((int)controlKeyU & 0x00008000) != 0)
								codeU += 0x00020000;

							// ...plus the modifier for ALT
							if (((int)altKeyU & 0x00008000) != 0)
								codeU += 0x00040000;

							// Construct shortcut from keystate and keychar
							Shortcut sc = (Shortcut)(codeU);
									
							// ...then do nothing if it represents a shortcut on its own
							if (IsShortcut(sc, _menuCommands))
								ignore = true;
						}
							
						if (!ignore)
						{
							if (_trackItem != -1)
							{
								// Is it also showing a submenu
								if (_popupMenu == null)
								{
									// Unselect the current item
									_trackItem = SwitchTrackingItem(_trackItem, -1);
								}
							}

							SimulateReturnFocus();
						}

						// Always eat keyboard message in simulated focus
						eat = true;
					}
					break;
				case (int)Win32.Msgs.WM_SYSKEYDOWN:
					if ((int)msg.wParam != (int)Win32.VirtualKeys.VK_MENU)
					{
						// Find up/down state of shift and control keys
						ushort shiftKeyD = User32.GetKeyState((int)Win32.VirtualKeys.VK_SHIFT);
						ushort controlKeyD = User32.GetKeyState((int)Win32.VirtualKeys.VK_CONTROL);
						ushort altKeyD = User32.GetKeyState((int)Win32.VirtualKeys.VK_MENU);

						// Basic code we are looking for is the key pressed...
						int codeD = (int)msg.wParam;

						// ...plus the modifier for SHIFT...
						if (((int)shiftKeyD & 0x00008000) != 0)
							codeD += 0x00010000;

						// ...plus the modifier for CONTROL...
						if (((int)controlKeyD & 0x00008000) != 0)
							codeD += 0x00020000;

						// ...plus the modifier for ALT
						if (((int)altKeyD & 0x00008000) != 0)
							codeD += 0x00040000;

						// Construct shortcut from keystate and keychar
						Shortcut sc = (Shortcut)(codeD);

						// Search for a matching command
						if (!GenerateShortcut(sc, _menuCommands))
						{
							// Last resort is treat as a potential mnemonic
							ProcessMnemonicKey((char)msg.wParam);

							if (_selected)
								_ignoreMouseMove = true;
						}
						else
						{
							SimulateReturnFocus();
						}

						// Always eat keyboard message in simulated focus
						eat = true;
					}
					break;
				default:
					break;
			}

			return eat;
		}

		/// <summary>
		/// Handle a possible shortcut combination by checking all child commands.
		/// </summary>
		/// <param name="sc">Shortcut to be tested for.</param>
		/// <param name="mcc">Collection of commands to test against.</param>
		/// <returns>true if match found; otherwise false.</returns>
        protected virtual bool GenerateShortcut(Shortcut sc, MenuCommandCollection mcc)
        {
            foreach(MenuCommand mc in mcc)
            {
				// Update the state of the menu
				mc.OnUpdate(EventArgs.Empty);

                // Does the command match?
                if (mc.Enabled && (mc.Shortcut == sc))
                {
					// Generate event for command
                    mc.OnClick(new MenuClickEventArgs(MenuClickEventArgs.ClickSource.Shortcut));

                    return true;
                }
                else
                {
                    // Any child items to test?
                    if (mc.MenuCommands.Count > 0)
                    {
                        // Recursive descent of all collections
                        if (GenerateShortcut(sc, mc.MenuCommands))
                            return true;
                    }
                }
            }

            return false;
        }

		/// <summary>
		/// Find if the shortcut combination is currently being used.
		/// </summary>
		/// <param name="sc">Shortcut to be tested for.</param>
		/// <param name="mcc">Collection of commands to test against.</param>
		/// <returns>true if match found; otherwise false.</returns>
		protected virtual bool IsShortcut(Shortcut sc, MenuCommandCollection mcc)
		{
			foreach(MenuCommand mc in mcc)
			{
				// Update the state of the menu
				mc.OnUpdate(EventArgs.Empty);

				// Does the command match?
				if (mc.Enabled && (mc.Shortcut == sc))
					return true;
				else
				{
					// Any child items to test?
					if (mc.MenuCommands.Count > 0)
					{
						// Recursive descent of all collections
						if (IsShortcut(sc, mc.MenuCommands))
							return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Handle the WM_OPERATEMENU windows message by showing a new submenu.
		/// </summary>
		/// <param name="m">Windows message details.</param>
        protected void OnWM_OPERATEMENU(ref Message m)
        {
            // Is there a valid item being tracted?
            if (_trackItem != -1)
            {
                DrawCommand dc = _drawCommands[_trackItem] as DrawCommand;

                OperateSubMenu(dc, (m.LParam != IntPtr.Zero), (m.WParam != IntPtr.Zero));
            }
        }

		/// <summary>
		/// Handle the WM_GETDLGCODE windows message by asking for all keyboard input.
		/// </summary>
		/// <param name="m">Windows message details.</param>
        protected void OnWM_GETDLGCODE(ref Message m)
        {
            // We want to the Form to provide all keyboard input to us
            m.Result = (IntPtr)Win32.DialogCodes.DLGC_WANTALLKEYS;
        }

		/// <summary>
		/// Processes Windows messages.
		/// </summary>
		/// <param name="m">The Windows Message to process.</param>
        protected override void WndProc(ref Message m)
        {
            // WM_OPERATEMENU is not a constant and so cannot be in a switch
            if (m.Msg == WM_OPERATEMENU)
                OnWM_OPERATEMENU(ref m);
            else
            {
                switch(m.Msg)
                {
                    case (int)Win32.Msgs.WM_GETDLGCODE:
                        OnWM_GETDLGCODE(ref m);
                        return;
                }
            }

            base.WndProc(ref m);
        }
    }
}
