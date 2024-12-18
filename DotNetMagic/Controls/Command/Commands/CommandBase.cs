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
using Crownwood.DotNetMagic.Common;

namespace Crownwood.DotNetMagic.Controls.Command
{
	/// <summary>
	/// Implements the basic functionality common to command classes.
	/// </summary>
	public abstract class CommandBase : Component, ICloneable
	{
		// Instance fields
		private object _tag;				// User data
		private bool _enabled;				// Is this command enabled for interaction
		private bool _visible;				// Is this command to be displayed
        private bool _showVert;             // Is this commmand visible when in vertical
        private bool _showHorz;             // Is this commmand visible when in horizontal
        private bool _fixedVert;			// Is height fixed when showing vertically
        private bool _fixedHorz;			// Is height fixed when showing horizontally
		private string _tooltip;			// Tooltip text for the command
        private Position _position;			// Relative position of the command
        private CommandBase _original;		// Original copy of a cloned instance

		/// <summary>
		/// Occurs when the value of the Tag property changes.
		/// </summary>
		public event EventHandler TagChanged;
		
		/// <summary>
		/// Occurs when the value of the Enabled property changes.
		/// </summary>
		public event EventHandler EnabledChanged;

		/// <summary>
		/// Occurs when the value of the Visible property changes.
		/// </summary>
		public event EventHandler VisibleChanged;

        /// <summary>
        /// Occurs when the value of the ShowVert property changes.
        /// </summary>
        public event EventHandler ShowVertChanged;

        /// <summary>
        /// Occurs when the value of the ShowHorz property changes.
        /// </summary>
        public event EventHandler ShowHorzChanged;
        
		/// <summary>
		/// Occurs when the value of the Tooltip property changes.
		/// </summary>
		public event EventHandler TooltipChanged;
		
		/// <summary>
		/// Occurs when the value of the Position property changes.
		/// </summary>
		public event EventHandler PositionChanged;

		/// <summary>
		/// Occurs when the value of property that effects display changes.
		/// </summary>
		public event EventHandler StatusChanged;

		/// <summary>
		/// Occurs when the command needs updating.
		/// </summary>
		public event EventHandler Update;

		/// <summary>
		/// Initializes a new instance of the CommandBase class.
		/// </summary>
		public CommandBase()
		{
			// Set defaults for exposed fields
			InternalConstruct(null, true, true, true, true, Position.Near);
		}

		/// <summary>
		/// Initializes a new instance of the CommandBase class.
		/// </summary>
		/// <param name="fixedVert">Is vertical size fixed.</param>
		/// <param name="fixedHorz">Is horizontal size fixed.</param>
		public CommandBase(bool fixedVert, bool fixedHorz)
		{
			// Set defaults for exposed fields
			InternalConstruct(null, true, true, fixedVert, fixedHorz, Position.Near);
		}

		private void InternalConstruct(object tag, 
									   bool enabled,
									   bool visible,
									   bool fixedVert,
									   bool fixedHorz,
									   Position position)
		{
			// Default internal state
			_tag = tag;
			_fixedVert = fixedVert;
			_fixedHorz = fixedHorz;
			_original = null;

			// Use reset functions for exposed properties
			ResetEnabled();
			ResetVisible();
            ResetShowVert();
            ResetShowHorz();
			ResetTooltip();
			ResetPosition();
		}

		/// <summary>
		/// Gets or sets the object that contains data about the command.
		/// </summary>
		[Browsable(false)]
		public object Tag
		{
			get { return _tag; }
			
			set 
			{ 
				if (_tag != value)
				{
					_tag = value; 
					OnTagChanged();
				}
			}
		}

		/// <summary>
		/// Gets or sets the enabled state of the command.
		/// </summary>
		[Category("Behavior")]
		[Description("Indicates whether command is enabled.")]
		[DefaultValue(true)]
		public bool Enabled
		{
			get { return _enabled; }
			
			set 
			{ 
				if (_enabled != value)
				{
					_enabled = value; 
					OnEnabledChanged();
				}
			}
		}

		/// <summary>
		/// Resets the Enabled property.
		/// </summary>
		public void ResetEnabled()
		{
			Enabled = true;
		}

		/// <summary>
		/// Gets or sets the enabled state of the command.
		/// </summary>
		[Category("Behavior")]
		[Description("Determines whether command is visible or hidden.")]
		[DefaultValue(true)]
		public bool Visible
		{
			get { return _visible; }
			
			set 
			{ 
				if (_visible != value)
				{
					_visible = value; 
					OnVisibleChanged();
				}
			}
		}

		/// <summary>
		/// Resets the Visible property.
		/// </summary>
		public void ResetVisible()
		{
			Visible = true;
		}

        /// <summary>
        /// Gets or sets a value indicating if command is shown when in vertical direction.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines whether command is shown when in vertical direction.")]
        [DefaultValue(true)]
        public bool ShowVert
        {
            get { return _showVert; }
			
            set 
            { 
                if (_showVert != value)
                {
                    _showVert = value; 
                    OnShowVertChanged();
                }
            }
        }

        /// <summary>
        /// Resets the ShowVert property.
        /// </summary>
        public void ResetShowVert()
        {
            ShowVert = true;
        }
        
        /// <summary>
        /// Gets or sets a value indicating if command is shown when in horizontal direction.
        /// </summary>
        [Category("Behavior")]
        [Description("Determines whether command is shown when in horizontal direction.")]
        [DefaultValue(true)]
        public bool ShowHorz
        {
            get { return _showHorz; }
			
            set 
            { 
                if (_showHorz != value)
                {
                    _showHorz = value; 
                    OnShowVertChanged();
                }
            }
        }

        /// <summary>
        /// Resets the ShowHorz property.
        /// </summary>
        public void ResetShowHorz()
        {
            ShowHorz = true;
        }

		/// <summary>
		/// Gets or sets the tooltip text.
		/// </summary>
		[Category("Appearance")]
		[Description("Tooltip text for display when mouse hovers over command.")]
		[DefaultValue("")]
		public string Tooltip
		{
			get { return _tooltip; }
			
			set 
			{ 
				if (_tooltip != value)
				{
					_tooltip = value; 
					OnTooltipChanged();
				}
			}
		}
		
		/// <summary>
		/// Resets the Tooltip property.
		/// </summary>
		public void ResetTooltip()
		{
			Tooltip = string.Empty;
		}
		
		/// <summary>
		/// Gets or sets the position of command.
		/// </summary>
		[Category("Appearance")]
		[Description("Determines how to position command inside control.")]
		[DefaultValue(typeof(Position), "Near")]
		public Position Position
		{
			get { return _position; }
			
			set 
			{ 
				if (_position != value)
				{
					_position = value; 
					OnPositionChanged();
				}
			}
		}
		
		/// <summary>
		/// Resets the Position property.
		/// </summary>
		public void ResetPosition()
		{
			Position = Position.Near;
		}
		
		/// <summary>
		/// Request the command update its state.
		/// </summary>
		public void PerformUpdate()
		{
			// Just fire the update command
			OnUpdate();
		}

        /// <summary>
        /// Gets a value indicating if command is visible in given direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsDirectionVisible(LayoutDirection direction)
        {
            // Must be visible and allowed to show in given direction
            if (direction == LayoutDirection.Horizontal)
                return (Visible && ShowHorz);
            else
                return (Visible && ShowVert);
        }

		/// <summary>
		/// Determine if the size of the command is fixed in provided direction.
		/// </summary>
		/// <param name="dir">Direction to be tested.</param>
		/// <returns>true if fixed in requested direction; otherwise false</returns>
		public bool FixedByDirection(LayoutDirection dir)
		{
			if (dir == LayoutDirection.Vertical)
				return _fixedVert;
			else
				return _fixedHorz;
		}
		
        /// <summary>
		/// Derived classes implement pressed semantics.
		/// </summary>
		public abstract void Pressed();

		/// <summary>
		/// Derived classes implement this to return required drawing size for command.
		/// </summary>
		/// <param name="g">Graphics reference used in calculations.</param>
		/// <param name="details">Source details needed to perform calculation.</param>
        /// <param name="topLevel">Drawing as a top level item.</param>
        /// <returns>Size of required area for command.</returns>
		public abstract Size CalculateDrawSize(Graphics g, ICommandDetails details, bool topLevel);

		/// <summary>
		/// Derived classes implement this to draw the command.
		/// </summary>
		/// <param name="g">Graphics reference used in calculations.</param>
		/// <param name="details">Source details needed to perform calculation.</param>
		/// <param name="drawRect">Bounding rectangle for drawing command.</param>
		/// <param name="state">State of the command to be drawn.</param>
        /// <param name="topLevel">Drawing as a top level item.</param>
        public abstract void Draw(Graphics g, 
								  ICommandDetails details, 
								  Rectangle drawRect, 
								  ItemState state,
                                  bool topLevel);


		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		public abstract object Clone();

		/// <summary>
		/// Update internal fields from source Node.
		/// </summary>
		/// <param name="source">Source Node to update from.</param>
		public virtual void UpdateInstance(CommandBase source)
		{
			// Copy class members
			_tag = source._tag;	
			_enabled = source._enabled;
			_visible = source._visible;
			_showVert = source._showVert;
			_showHorz = source._showHorz;
			_fixedVert = source._fixedVert;
			_fixedHorz = source._fixedHorz;
			_tooltip = source._tooltip;
			_position = source._position;
		}

		/// <summary>
		/// Raises the TagChanged event.
		/// </summary>
		protected virtual void OnTagChanged()
		{
			if (TagChanged != null)
				TagChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the Visible event.
		/// </summary>
		protected virtual void OnVisibleChanged()
		{
			if (VisibleChanged != null)
				VisibleChanged(this, EventArgs.Empty);

			// Must also raise event to indicate status has changed
			OnStatusChanged();
		}

        /// <summary>
        /// Raises the ShowVertChanged event.
        /// </summary>
        protected virtual void OnShowVertChanged()
        {
            if (ShowVertChanged != null)
                ShowVertChanged(this, EventArgs.Empty);

            // Must also raise event to indicate status has changed
            OnStatusChanged();
        }
        
        /// <summary>
        /// Raises the ShowHorzChanged event.
        /// </summary>
        protected virtual void OnShowHorzChanged()
        {
            if (ShowHorzChanged != null)
                ShowHorzChanged(this, EventArgs.Empty);

            // Must also raise event to indicate status has changed
            OnStatusChanged();
        }

        /// <summary>
		/// Raises the EnabledChanged event.
		/// </summary>
		protected virtual void OnEnabledChanged()
		{
			if (EnabledChanged != null)
				EnabledChanged(this, EventArgs.Empty);

			// Must also raise event to indicate status has changed
			OnStatusChanged();
		}

		/// <summary>
		/// Raises the TooltipChanged event.
		/// </summary>
		protected virtual void OnTooltipChanged()
		{
			if (TooltipChanged != null)
				TooltipChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the PositionChanged event.
		/// </summary>
		protected virtual void OnPositionChanged()
		{
			if (PositionChanged != null)
				PositionChanged(this, EventArgs.Empty);

			// Must also raise event to indicate status has changed
			OnStatusChanged();
		}

		/// <summary>
		/// Raises the StatusChanged event.
		/// </summary>
		protected virtual void OnStatusChanged()
		{
			if (StatusChanged != null)
				StatusChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the Update event.
		/// </summary>
		protected virtual void OnUpdate()
		{
			if (Update != null)
				Update(this, EventArgs.Empty);
		}

		internal CommandBase Original
		{
			get { return _original; }
			set { _original = value; }
		}
	}
}
