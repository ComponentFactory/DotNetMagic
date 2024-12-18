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
using System.Drawing.Imaging;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

namespace Crownwood.DotNetMagic.Menus
{
    /// <summary>
    /// Represents the method that will handle an event associated with a MenuCommand.
    /// </summary>
    public delegate void CommandHandler(MenuCommand item);

    /// <summary>
    /// Specifies the animation setting.
    /// </summary>
    public enum Animate
    {
		/// <summary>
		/// No animation is required.
		/// </summary>
        No,
		/// <summary>
		/// Animation is always required.
		/// </summary>
        Yes,
		/// <summary>
		/// Use system defined setting for using animation.
		/// </summary>
        System
    }
    
    /// <summary>
    /// Specifies the animation style.
    /// </summary>
    public enum Animation
    {
		/// <summary>
		/// Use system defined setting for animation style.
		/// </summary>
        System                  = 0x00100000,
		/// <summary>
		/// Alpha blend the window into view.
		/// </summary>
        Blend                   = 0x00080000,
		/// <summary>
		/// Slide the window into view starting from the center.
		/// </summary>
        SlideCenter             = 0x00040010,
		/// <summary>
		/// Slide the window into view from top to bottom.
		/// </summary>
        SlideHorVerPositive     = 0x00040005,
		/// <summary>
		/// Slide the window into view from bottom to top.
		/// </summary>
        SlideHorVerNegative     = 0x0004000A,
		/// <summary>
		/// Slide the window into view from left to right.
		/// </summary>
        SlideHorPosVerNegative  = 0x00040009,
		/// <summary>
		/// Slide the window into view from right to left.
		/// </summary>
        SlideHorNegVerPositive  = 0x00040006
    }

	/// <summary>
	/// Implements information required to represent a menu option.
	/// </summary>
    [ToolboxItem(false)]
    [DefaultProperty("Text")]
    [DefaultEvent("Click")]
    public class MenuCommand : Component, ICloneable	
    {
        /// <summary>
        /// Specifies the property that has changed.
        /// </summary>
        public enum Property
        {
			/// <summary>
			/// Text property has been changed.
			/// </summary>
            Text,
			/// <summary>
			/// Enabled property has been changed.
			/// </summary>
            Enabled,
			/// <summary>
			/// ImageIndex property has been changed.
			/// </summary>
            ImageIndex,
			/// <summary>
			/// ImageList property has been changed.
			/// </summary>
            ImageList,
			/// <summary>
			/// Image property has been changed.
			/// </summary>
            Image,
			/// <summary>
			/// Shortcut property has been changed.
			/// </summary>
            Shortcut,
			/// <summary>
			/// Checked property has been changed.
			/// </summary>
            Checked,
			/// <summary>
			/// RadioCheck property has been changed.
			/// </summary>
            RadioCheck,
			/// <summary>
			/// Break property has been changed.
			/// </summary>
            Break,
			/// <summary>
			/// Infrequent property has been changed.
			/// </summary>
            Infrequent,
			/// <summary>
			/// Visible property has been changed.
			/// </summary>
            Visible,
			/// <summary>
			/// Description property has been changed.
			/// </summary>
            Description
        }

        /// <summary>
        /// Represents the method that will handle a change in a property value.
        /// </summary>
        public delegate void PropChangeHandler(MenuCommand item, Property prop);

        // Instance fields
        private bool _visible;
        private bool _break;
        private string _text;
        private string _description;
        private bool _enabled;
        private bool _checked;
        private int _imageIndex;
        private bool _infrequent;
        private object _tag;
        private bool _radioCheck;
        private Shortcut _shortcut;
        private ImageList _imageList;
        private Image _image;
        private MenuCommandCollection _menuItems;
		private MenuCommand _original;

        /// <summary>
        /// Occurs when the command has been invoked.
        /// </summary>
        public event EventHandler Click;

		/// <summary>
		/// Occurs when the command needs its state updating.
		/// </summary>
        public event EventHandler Update;

		/// <summary>
		/// Occurs before a PopupMenu of the child commands it to be shown.
		/// </summary>
        public event CommandHandler PopupStart;

		/// <summary>
		/// Occurs after the PopupMenu of the child commands has been dismissed.
		/// </summary>
        public event CommandHandler PopupEnd;

		/// <summary>
		/// Occurs when a property value has been changed.
		/// </summary>
        public event PropChangeHandler PropertyChanged;

		/// <summary>
		/// Initialize a new instance of the MenuCommand class.
		/// </summary>
        public MenuCommand()
        {
            InternalConstruct("MenuItem", null, -1, Shortcut.None, null);
        }

		/// <summary>
		/// Initialize a new instance of the MenuCommand class.
		/// </summary>
		/// <param name="text">Display text for command.</param>
        public MenuCommand(string text)
        {
            InternalConstruct(text, null, -1, Shortcut.None, null);
        }

		/// <summary>
		/// Initialize a new instance of the MenuCommand class.
		/// </summary>
		/// <param name="text">Display text for command.</param>
		/// <param name="clickHandler">Handler for processing click event.</param>
        public MenuCommand(string text, EventHandler clickHandler)
        {
            InternalConstruct(text, null, -1, Shortcut.None, clickHandler);
        }

		/// <summary>
		/// Initialize a new instance of the MenuCommand class.
		/// </summary>
		/// <param name="text">Display text for command.</param>
		/// <param name="shortcut">Keyboard combination to click command.</param>
        public MenuCommand(string text, Shortcut shortcut)
        {
            InternalConstruct(text, null, -1, shortcut, null);
        }

		/// <summary>
		/// Initialize a new instance of the MenuCommand class.
		/// </summary>
		/// <param name="text">Display text for command.</param>
		/// <param name="shortcut">Keyboard combination to click command.</param>
		/// <param name="clickHandler">Handler for processing click event.</param>
        public MenuCommand(string text, Shortcut shortcut, EventHandler clickHandler)
        {
            InternalConstruct(text, null, -1, shortcut, clickHandler);
        }

		/// <summary>
		/// Initialize a new instance of the MenuCommand class.
		/// </summary>
		/// <param name="text">Display text for command.</param>
		/// <param name="imageList">Source for accessing command image.</param>
		/// <param name="imageIndex">Index into source ImageList.</param>
        public MenuCommand(string text, ImageList imageList, int imageIndex)
        {
            InternalConstruct(text, imageList, imageIndex, Shortcut.None, null);
        }

		/// <summary>
		/// Initialize a new instance of the MenuCommand class.
		/// </summary>
		/// <param name="text">Display text for command.</param>
		/// <param name="imageList">Source for accessing command image.</param>
		/// <param name="imageIndex">Index into source ImageList.</param>
		/// <param name="shortcut">Keyboard combination to click command.</param>
        public MenuCommand(string text, ImageList imageList, int imageIndex, Shortcut shortcut)
        {
            InternalConstruct(text, imageList, imageIndex, shortcut, null);
        }

		/// <summary>
		/// Initialize a new instance of the MenuCommand class.
		/// </summary>
		/// <param name="text">Display text for command.</param>
		/// <param name="imageList">Source for accessing command image.</param>
		/// <param name="imageIndex">Index into source ImageList.</param>
		/// <param name="clickHandler">Handler for processing click event.</param>
        public MenuCommand(string text, ImageList imageList, int imageIndex, EventHandler clickHandler)
        {
            InternalConstruct(text, imageList, imageIndex, Shortcut.None, clickHandler);
        }

		/// <summary>
		/// Initialize a new instance of the MenuCommand class.
		/// </summary>
		/// <param name="text">Display text for command.</param>
		/// <param name="imageList">Source for accessing command image.</param>
		/// <param name="imageIndex">Index into source ImageList.</param>
		/// <param name="shortcut">Keyboard combination to click command.</param>
		/// <param name="clickHandler">Handler for processing click event.</param>
        public MenuCommand(string text, 
                           ImageList imageList, 
                           int imageIndex, 
                           Shortcut shortcut, 
                           EventHandler clickHandler)
        {
            InternalConstruct(text, imageList, imageIndex, shortcut, clickHandler);
        }

		/// <summary>
		/// Dispose of resources.
		/// </summary>
		/// <param name="disposing">Disposing.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Must get rid of collections as they cache information
				if (_menuItems != null)
				{
					_menuItems.Dispose();
					_menuItems = null;
				}

				if (_original != null)
				{
					_original.Dispose();
					_original = null;
				}
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Generic construction of new MenuCommand instance.
		/// </summary>
		/// <param name="text">Display text for command.</param>
		/// <param name="imageList">Source for accessing command image.</param>
		/// <param name="imageIndex">Index into source ImageList.</param>
		/// <param name="shortcut">Keyboard combination to click command.</param>
		/// <param name="clickHandler">Handler for processing click event.</param>
        protected void InternalConstruct(string text, 
                                         ImageList imageList, 
                                         int imageIndex, 
                                         Shortcut shortcut, 
                                         EventHandler clickHandler)
        {
            // Save parameters
            _text = text;
            _imageList = imageList;
            _imageIndex = imageIndex;
            _shortcut = shortcut;
            _description = text;

			// This is not a clone
			_original = null;

            if (clickHandler != null)
                Click += clickHandler;
		
            // Define defaults for others
            _enabled = true;
            _checked = false;
            _radioCheck = false;
            _break = false;
            _tag = null;
            _visible = true;
            _infrequent = false;
            _image = null;

            // Create the collection of embedded menu commands
            _menuItems = new MenuCommandCollection();
        }

		/// <summary>
		/// Gets the collection of child MenuCommand instances.
		/// </summary>
		[Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MenuCommandCollection MenuCommands
        {
            get { return _menuItems; }
        }

		/// <summary>
		/// Gets and sets the display text.
		/// </summary>
        [DefaultValue("MenuItem")]
        [Localizable(true)]
        public string Text
        {
            get { return _text; }
			
            set 
            { 
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(Property.Text);
                } 
            }
        }

		/// <summary>
		/// Gets and sets a value indicating the enabled state of command.
		/// </summary>
        [DefaultValue(true)]
        public bool Enabled
        {
            get { return _enabled; }

            set 
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnPropertyChanged(Property.Enabled);
                }
            }
        }

		/// <summary>
		/// Gets and sets the index of the required display image in the ImageList property.
		/// </summary>
        [DefaultValue(-1)]
        public int ImageIndex
        {
            get { return _imageIndex; }

            set 
            { 
                if (_imageIndex != value)
                {
                    _imageIndex = value;
                    OnPropertyChanged(Property.ImageIndex);
                } 
            }
        }

		/// <summary>
		/// Gets and sets the source to use for acessing a display image.
		/// </summary>
        [DefaultValue(null)]
        public ImageList ImageList
        {
            get { return _imageList; }

            set 
            { 
                if (_imageList != value)
                {
                    _imageList = value;
                    OnPropertyChanged(Property.ImageList);
                }
            }
        }

		/// <summary>
		/// Gets and sets the Image to be displayed.
		/// </summary>
        [DefaultValue(null)]
        public Image Image
        {
            get { return _image; }
            
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(Property.Image);
                }
            }
        }

		/// <summary>
		/// Gets and sets the keyboard combination used to invoke command.
		/// </summary>
        [DefaultValue(typeof(Shortcut), "None")]
        public Shortcut Shortcut
        {
            get { return _shortcut; }

            set 
            { 
                if (_shortcut != value)
                {
                    _shortcut = value;
                    OnPropertyChanged(Property.Shortcut);
                }
            }
        }

		/// <summary>
		/// Gets and sets a value indicating if the command should be displayed with a check mark.
		/// </summary>
        [DefaultValue(false)]
        public bool Checked
        {
            get { return _checked; }

            set 
            { 
                if (_checked != value)
                {
                    _checked = value;
                    OnPropertyChanged(Property.Checked);
                }
            }
        }

		/// <summary>
		/// Gets and sets a value indicating if the check mark should be drawn in the radio style.
		/// </summary>
        [DefaultValue(false)]
        public bool RadioCheck
        {
            get { return _radioCheck; }

            set 
            { 
                if (_radioCheck != value)
                {
                    _radioCheck = value;
                    OnPropertyChanged(Property.RadioCheck);
                }
            }
        }

		/// <summary>
		/// Gets and sets a value indicating if a page should occur after this command.
		/// </summary>
        [DefaultValue(false)]
        public bool Break
        {
            get { return _break; }
			
            set 
            { 
                if (_break != value)
                {
                    _break = value;
                    OnPropertyChanged(Property.Break);
                }
            }
        }

		/// <summary>
		/// Gets and sets a value indicating if the command should be display infrequently.
		/// </summary>
        [DefaultValue(false)]
        public bool Infrequent
        {
            get { return _infrequent; }
			
            set 
            {	
                if (_infrequent != value)
                {
                    _infrequent = value;
                    OnPropertyChanged(Property.Infrequent);
                }
            }
        }

		/// <summary>
		/// Gets and sets a value indicating if the command should be displayed.
		/// </summary>
        [DefaultValue(true)]
        public bool Visible
        {
            get { return _visible; }

            set 
            { 
                if (_visible != value)
                {
                    _visible = value;
                    OnPropertyChanged(Property.Visible);
                }
            }
        }

		/// <summary>
		/// Gets a value indicating if the menu has is a parent of child commands.
		/// </summary>
        [Browsable(false)]
        public bool IsParent
        {
            get { return (_menuItems.Count > 0); }
        }

		/// <summary>
		/// Gets and sets a description used to describe the purpose of the command.
		/// </summary>
        [DefaultValue("")]
        [Localizable(true)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

		/// <summary>
		/// Gets and sets a user specified object.
		/// </summary>
        [DefaultValue(null)]
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

		/// <summary>
		/// Raises the PropertyChanged event.
		/// </summary>
		/// <param name="prop">Property that has been changed.</param>
        public virtual void OnPropertyChanged(Property prop)
        {
            // Any attached event handlers?
            if (PropertyChanged != null)
                PropertyChanged(this, prop);
        }

		/// <summary>
		/// Generates a Click event for the MenuCommand, simulating a click by a user.
		/// </summary>
        public void PerformClick()
        {
            // Update command with correct state
            OnUpdate(EventArgs.Empty);
            
            // Notify event handlers of click event
            OnClick(EventArgs.Empty);
        }
  
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		public object Clone()
		{
			// Create a new MenuCommand instance
			MenuCommand copy = new MenuCommand();
			
			// Copy across member values
			copy._visible = _visible;
			copy._break = _break;
			copy._text = _text;
			copy._description = _description;
			copy._enabled = _enabled;
			copy._checked = _checked;
			copy._imageIndex = _imageIndex;
			copy._infrequent = _infrequent;
			copy._tag = _tag;
			copy._radioCheck = _radioCheck;
			copy._shortcut = _shortcut;
			copy._imageList = _imageList;
			copy._image = _image;

			// Make the copy point back at the original
			copy._original = this;
			
			// Make deep copy of children
			foreach(MenuCommand child in MenuCommands)
				copy.MenuCommands.Add((MenuCommand)child.Clone());			
			
			return copy;
		}
		
		/// <summary>
		/// Update internal fields from source Node.
		/// </summary>
		/// <param name="source">Source Node to update from.</param>
		public void UpdateInstance(MenuCommand source)
		{
			_visible = source._visible;
			_break = source._break;
			_text = source._text;
			_description = source._description;
			_enabled = source._enabled;
			_checked = source._checked;
			_imageIndex = source._imageIndex;
			_infrequent = source._infrequent;
			_tag = source._tag;
			_radioCheck = source._radioCheck;
			_shortcut = source._shortcut;
			_imageList = source._imageList;
			_image = source._image;
		}		

		/// <summary>
		/// Raises the Click event.
		/// </summary>
		/// <param name="e">An EventArgs structure containing event data.</param>
        public virtual void OnClick(EventArgs e)
        {
            // Any attached event handlers?
            if (Click != null)
                Click(this, e);
        }

		/// <summary>
		/// Raises the Update event.
		/// </summary>
		/// <param name="e">An EventArgs structure containing event data.</param>
        public virtual void OnUpdate(EventArgs e)
        {
            // Any attached event handlers?
            if (Update != null)
                Update(this, e);
        }

		/// <summary>
		/// Raises the PopupStart event.
		/// </summary>
        public virtual void OnPopupStart()
        {
            // Any attached event handlers?
            if (PopupStart != null)
                PopupStart(this);
        }
            
		/// <summary>
		/// Raises the PopupEnd event.
		/// </summary>
        public virtual void OnPopupEnd()
        {
            // Any attached event handlers?
            if (PopupEnd != null)
                PopupEnd(this);
		}

		internal MenuCommand Original
		{
			get { return _original; }
			set { _original = value; }
		}
	}
}
