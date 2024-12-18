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
	/// Implements a button style command.
	/// </summary>
	[ToolboxItem(false)]
	public class ButtonCommand : CommandBaseCommon, ICommandIsButton
	{
		// Instance fields
		private bool _pushed;
		private ButtonStyle _buttonStyle;
		
		/// <summary>
		/// Occurs when the value of the ButtonStyle property changes.
		/// </summary>
		public event EventHandler ButtonStyleChanged;

		/// <summary>
		/// Occurs when the value of the Pushed property changes.
		/// </summary>
		public event EventHandler PushedChanged;

		/// <summary>
		/// Initialize a new instance of the ButtonCommand class.
		/// </summary>
		public ButtonCommand()
		{
			InternalConstruct();
		}

		/// <summary>
		/// Initialize a new instance of the ButtonCommand class.
		/// </summary>
		/// <param name="image">Initial image to assign.</param>
		/// <param name="update">Update delegate to hookup.</param>
		/// <param name="click">Click delegate to hookup.</param>
		public ButtonCommand(Image image, EventHandler update, EventHandler click)
		{
			// Set the image to use
			Image = image;

			// Hook up the delegates
			if (update != null)	Update += update;
			if (click != null)	Click += click;

			InternalConstruct();
		}

		/// <summary>
		/// Initialize a new instance of the ButtonCommand class.
		/// </summary>
		/// <param name="image">Initial image to assign.</param>
		/// <param name="text">Initial text to assign.</param>
		/// <param name="update">Update delegate to hookup.</param>
		/// <param name="click">Click delegate to hookup.</param>
		public ButtonCommand(Image image, string text, EventHandler update, EventHandler click)
		{
			// Set the image/text to used
			Image = image;
			Text = text;

			// Hook up the delegates
			if (update != null)	Update += update;
			if (click != null)	Click += click;

			InternalConstruct();
		}
		
		/// <summary>
		/// Initializes a new instance of the ButtonCommand class.
		/// </summary>
		/// <param name="fixedVert">Is vertical size fixed.</param>
		/// <param name="fixedHorz">Is horizontal size fixed.</param>
		public ButtonCommand(bool fixedVert, bool fixedHorz)
			: base(fixedVert, fixedHorz)
		{
			InternalConstruct();
		}
		
		private void InternalConstruct()
		{
			// Default the properties
			ResetButtonStyle();
			ResetPushed();
		}
		
		/// <summary>
		/// Gets and sets the button style.
		/// </summary>
		[Category("Behavior")]
		[Description("Determine how the button operates.")]
		[DefaultValue(typeof(ButtonStyle), "PushButton")]
		public ButtonStyle ButtonStyle
		{
			get { return _buttonStyle; }
			
			set
			{
				if (_buttonStyle != value)
				{
					_buttonStyle = value;
					OnButtonStyleChanged();
				}
			}
		}
		
		/// <summary>
		/// Resets the ButtonStyle property.
		/// </summary>
		public void ResetButtonStyle()
		{
			ButtonStyle = ButtonStyle.PushButton;
		}
		
		/// <summary>
		/// Gets and sets the pushed state of the button.
		/// </summary>
		[Category("Behavior")]
		[Description("Indicates if the button is pushed or not.")]
		[DefaultValue(false)]
		public bool Pushed
		{
			get { return _pushed; }
			
			set
			{
				if (_pushed != value)
				{
					_pushed = value;
					OnPushedChanged();
				}
			}
		}
		
		/// <summary>
		/// Resets the Pushed property.
		/// </summary>
		public void ResetPushed()
		{
			Pushed = false;
		}
		
		/// <summary>
		/// Perform appropriate action for when command is pressed.
		/// </summary>
		public override void Pressed()
		{
			// By default we just generate a click.
			PerformClick();
		}		

		/// <summary>
		/// Draw the command as a Button.
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
			string drawText;
			CommandImage drawImage;

			// Decide if the Text of the button should be drawn
			if (details.ImageAndText == ImageAndText.ImageOnly)
				drawText = string.Empty;
			else 
				drawText = Text;

			// Decide if the Image of the button should be drawn
			if (details.ImageAndText == ImageAndText.TextOnly)
				drawImage = CommandImage.Empty;
			else 
				drawImage = CommandImage;

			// Use the provided layout direction by default
			LayoutDirection direction = details.Direction;

			// Do we need to override for drawing?
			if (details.OnlyHorizontalText)
				direction = LayoutDirection.Horizontal;

			// Use a helper method to draw command appropriately
			CommandDraw.DrawButtonCommand(g, details.Style,
										  direction, drawRect, state, Enabled,
										  details.TextEdge, details.Font, details.TextColor,
										  details.BaseColor, drawText, drawImage, null,
										  details.TrackBaseColor1, details.TrackBaseColor2,
										  details.TrackLightColor1, details.TrackLightColor2,
										  details.TrackLightLightColor1, details.TrackLightLightColor2,
										  details.TrackDarkColor, _buttonStyle, _pushed, false, false);
		}

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		public override object Clone()
		{
			// Create a new instance of this class
			ButtonCommand copy =  new ButtonCommand();
			
			// Make a note of the original we are cloned from
			copy.Original = this;
			
			// Copy across the members values
			copy.UpdateInstance(this);
			
			return copy;
		}

		/// <summary>
		/// Update internal fields from source Node.
		/// </summary>
		/// <param name="source">Source Node to update from.</param>
		public override void UpdateInstance(CommandBase source)
		{
			// Update base members
			base.UpdateInstance(source);
			
			// Cast to correct type
			ButtonCommand original = source as ButtonCommand;

			// Copy class members
			_pushed = original._pushed;	
			_buttonStyle = original._buttonStyle;	
		}

		/// <summary>
		/// Raises the ButtonStyle event.
		/// </summary>
		protected virtual void OnButtonStyleChanged()
		{
			if (ButtonStyleChanged != null)
				ButtonStyleChanged(this, EventArgs.Empty);

			// Must also raise event to indicate status has changed
			OnStatusChanged();
		}

		/// <summary>
		/// Raises the Pushed event.
		/// </summary>
		protected virtual void OnPushedChanged()
		{
			if (PushedChanged != null)
				PushedChanged(this, EventArgs.Empty);

			// Must also raise event to indicate status has changed
			OnStatusChanged();
		}
		
		/// <summary>
		/// Raises the Click event.
		/// </summary>
		protected override void OnClick()
		{
			// If we are a toggle button
			if (_buttonStyle == ButtonStyle.ToggleButton)
			{
				// Then invert our current pushed state
				Pushed = !Pushed;
			}
		
			// Let base class fire events
			base.OnClick();
		}
	}
}
