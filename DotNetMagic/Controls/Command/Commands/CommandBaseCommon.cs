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
	/// Implements some common functionality to many different command classes.
	/// </summary>
	public abstract class CommandBaseCommon : CommandBase
	{
		// Class constants
		private static readonly int BORDER_WIDTH = 1;
		private static readonly int BORDER_HEIGHT = 1;
		private static readonly int SPACE_WIDTH = 1;
		private static readonly int SPACE_HEIGHT = 1;
		private static readonly int SPACE_GAP_WIDTH = 1;
		private static readonly int SPACE_LEFT_EXTRA = 1;

		// Instance fields
		private string _text;			// Text used for display
		private CommandImage _image;	// Image displayed with command

		/// <summary>
		/// Occurs when the value of the Text property changes.
		/// </summary>
		public event EventHandler TextChanged;
		
		/// <summary>
		/// Occurs when the value of the Image property changes.
		/// </summary>
		public event EventHandler ImageChanged;

		/// <summary>
		/// Occurs when the command is clicked.
		/// </summary>
		public event EventHandler Click;

		/// <summary>
		/// Initializes a new instance of the CommonCommandBase class.
		/// </summary>
		public CommandBaseCommon()
		{
			InternalConstruct();
		}
		
		/// <summary>
		/// Initializes a new instance of the CommandBaseCommon class.
		/// </summary>
		public CommandBaseCommon(bool fixedVert, bool fixedHorz)
			: base(fixedVert, fixedHorz)
		{
			InternalConstruct();
		}
		
		private void InternalConstruct()
		{
			// Set internal state
			_image = new CommandImage();

			// Default exposed properties
			ResetText();
			ResetImage();
		}

		/// <summary>
		/// Gets or sets the display text of command.
		/// </summary>
		[Category("Appearance")]
		[Description("The text contained in the command.")]
		[DefaultValue("Text")]
		[Localizable(true)]
		public string Text
		{
			get { return _text; }
			
			set
			{
				if (_text != value)
				{
					_text = value;
					OnTextChanged();
				}
			}
		}
		
		/// <summary>
		/// Resets the Text property.
		/// </summary>
		public void ResetText()
		{
			Text = "Text";
		}

		/// <summary>
		/// Gets or sets the display image of command.
		/// </summary>
		[Category("Appearance")]
		[Description("The image contained in the command.")]
		[DefaultValue(null)]
		[Localizable(true)]
		public Image Image
		{
			get { return _image.Image; }

			set
			{
				if (_image.Image != value)
				{
					_image.Image = value;
					OnImageChanged();
				}
			}
		}
		
		/// <summary>
		/// Resets the Image property.
		/// </summary>
		public void ResetImage()
		{
			Image = null;
		}

		/// <summary>
		/// Generates a Click event for a button style command.
		/// </summary>
		public void PerformClick()
		{
			OnClick();
		}
		/// <summary>
		/// Calculate required drawing size for push button.
		/// </summary>
		/// <param name="g">Graphics reference used in calculations.</param>
		/// <param name="details">Source details needed to perform calculation.</param>
        /// <param name="topLevel">Drawing as a top level item.</param>
        /// <returns>Size of required area for command.</returns>
		public override Size CalculateDrawSize(Graphics g, ICommandDetails details, bool topLevel)
		{
			int width = 0;
			int height = 0;

			// The size of the button area is calculated as....
			//
			// Size needed for image	(the image is not rotated when shown vertically)
			// Size needed for the text (the text is rotated when shown vertically)
			//

			// Do we have an image that needs positioning?
			if ((Image != null) && (details.ImageAndText != ImageAndText.TextOnly))
			{
				Size imageSize = ImageSpace(details.Style);

				width = imageSize.Width;
				height = imageSize.Height;

				// With an image we need an extra pixel gap to the left (IDE and Office2003 only)
				if ((details.Style == VisualStyle.IDE) || 
					(details.Style == VisualStyle.IDE2005) ||
					(details.Style == VisualStyle.Office2003))
					width += SPACE_LEFT_EXTRA;
			}

			// Do we have any text to position?
			if ((Text.Length > 0)  && (details.ImageAndText != ImageAndText.ImageOnly))
			{
				// Get regular size of the text (horizontally)
				Size textSize = CommandDraw.TextSize(g, details.Font, Text);

				// Are we drawing the contents as if horizontal
				if ((details.Direction == LayoutDirection.Horizontal) || details.OnlyHorizontalText)
				{
					// Position the text drawn horizontal
					switch(details.TextEdge)
					{
						case TextEdge.Left:
						case TextEdge.Right:
							// Increase width by text width
							width += textSize.Width;

							// If we have an image as well then need a spacing gap
							if (Image != null)
								width += SPACE_GAP_WIDTH;

							// Make sure height is enough for text
							if (height < textSize.Height)
								height = textSize.Height;
							break;
						case TextEdge.Top:
						case TextEdge.Bottom:
							// Increase height by text height
							height += SPACE_HEIGHT + textSize.Height;

							// If we have an image as well then need a spacing gap
							if (Image != null)
								height += SPACE_HEIGHT;

							// Make sure width is enough for text
							if (width < textSize.Width)
								width = textSize.Width;
							break;
					}
				}
				else
				{
					// Position the text drawn vertical
					switch(details.TextEdge)
					{
						case TextEdge.Left:
						case TextEdge.Right:
							// Increase height text height
							height += textSize.Width;

							// If we have an image as well then need a spacing gap
							if (Image != null)
								height += SPACE_GAP_WIDTH;

							// Make sure width is enough for text
							if (width < textSize.Height)
								width = textSize.Height;
							break;
						case TextEdge.Top:
						case TextEdge.Bottom:
							// Increase height by text height
							width += textSize.Height;

							// If we have an image as well then need a spacing gap
							if (Image != null)
								width += SPACE_HEIGHT;

							// Make sure width is enough for text
							if (height < textSize.Width)
								height = textSize.Width;
							break;
					}
				}
			}

			// Calculate total area using borders and spacing gaps around inner size
			width += (BORDER_WIDTH * 2) + (SPACE_WIDTH * 2);
			height += (BORDER_HEIGHT * 2) + (SPACE_HEIGHT * 2);

			// The width and height are always the same no matter which direction we are in
			return new Size(width,height);
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
			CommandBaseCommon original = source as CommandBaseCommon;

			// Copy class members
			_text = original._text;	
			_image = original._image;
		}

		/// <summary>
		/// Gets access to image management object.
		/// </summary>
		protected CommandImage CommandImage
		{
			get { return _image; }
		}

		/// <summary>
		/// Get the size of the image.
		/// </summary>
		protected Size ImageSize
		{
			get { return _image.ImageSize; }
		}

		/// <summary>
		/// Get the space required for the image.
		/// </summary>
		protected Size ImageSpace(VisualStyle style)
		{
			return _image.ImageSpace(style);
		}

		/// <summary>
		/// Raises the TextChanged event.
		/// </summary>
		protected virtual void OnTextChanged()
		{
			if (TextChanged != null)
				TextChanged(this, EventArgs.Empty);

			// Must also raise event to indicate status has changed
			OnStatusChanged();
		}

		/// <summary>
		/// Raises the ImageChanged event.
		/// </summary>
		protected virtual void OnImageChanged()
		{
			if (ImageChanged != null)
				ImageChanged(this, EventArgs.Empty);

			// Must also raise event to indicate status has changed
			OnStatusChanged();
		}

		/// <summary>
		/// Raises the Click event.
		/// </summary>
		protected virtual void OnClick()
		{
			if (Click != null)
				Click(this, EventArgs.Empty);
		}
	}
}
