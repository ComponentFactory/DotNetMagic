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
using System.Windows.Forms;
using System.ComponentModel;
using Crownwood.DotNetMagic.Common;

namespace Crownwood.DotNetMagic.Controls.Command
{
	/// <summary>
	/// Determines the padding for the CommandControl.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class Padding
	{
		// Instance fields
		private Edges _edges;

		/// <summary>
		/// Occures when any padding value changes.
		/// </summary>
		public event EventHandler PaddingChanged;

		/// <summary>
		/// Initialize a new instance of the Padding class.
		/// </summary>
		public Padding()
		{
			// Create the actual storage
			_edges = new Edges();

			// Default all the edges
			ResetLeft();
			ResetTop();
			ResetRight();
			ResetBottom();
		}

		/// <summary>
		/// Gets and sets the padding for the left edge.
		/// </summary>
		[Description("Number of pixels to padding the left edge.")]
		[DefaultValue(1)]
		public int Left
		{
			get { return _edges.Left; }
			
			set 
			{ 
				// Cannot set a negative edge indent
				if (value >= 0)
				{
					if (_edges.Left != value)
					{
						_edges.Left = value; 
						OnPaddingChanged();
					}
				}
			}
		}

		/// <summary>
		/// Resets the Left property to its default value.
		/// </summary>
		public void ResetLeft()
		{
			Left = 1;
		}

		/// <summary>
		/// Gets and sets the padding for the top edge.
		/// </summary>
		[Description("Number of pixels to padding the top edge.")]
		[DefaultValue(2)]
		public int Top
		{
			get { return _edges.Top; }
			
			set 
			{ 
				// Cannot set a negative edge indent
				if (value >= 0)
				{
					if (_edges.Top != value)
					{
						_edges.Top = value; 
						OnPaddingChanged();
					}
				}
			}
		}

		/// <summary>
		/// Resets the Top property to its default value.
		/// </summary>
		public void ResetTop()
		{
			Top = 2;
		}

		/// <summary>
		/// Gets and sets the padding for the right edge.
		/// </summary>
		[Description("Number of pixels to padding the right edge.")]
		[DefaultValue(1)]
		public int Right
		{
			get { return _edges.Right; }
			
			set 
			{ 
				// Cannot set a negative edge indent
				if (value >= 0)
				{
					if (_edges.Right != value)
					{
						_edges.Right = value; 
						OnPaddingChanged();
					}
				}
			}
		}

		/// <summary>
		/// Resets the Right property to its default value.
		/// </summary>
		public void ResetRight()
		{
			Right = 1;
		}

		/// <summary>
		/// Gets and sets the padding for the bottom edge.
		/// </summary>
		[Description("Number of pixels to padding the bottom edge.")]
		[DefaultValue(2)]
		public int Bottom
		{
			get { return _edges.Bottom; }
			
			set 
			{ 
				// Cannot set a negative edge indent
				if (value >= 0)
				{
					if (_edges.Bottom != value)
					{
						_edges.Bottom = value; 
						OnPaddingChanged();
					}
				}
			}
		}

		/// <summary>
		/// Resets the Bottom property to its default value.
		/// </summary>
		public void ResetBottom()
		{
			Bottom = 2;
		}

		/// <summary>
		/// Returns a String that represents the current IndentPaddingEdges.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			// Return nothing so it appears as blank in property window
			return string.Empty;
		}
		
		/// <summary>
		/// Gets the total width represented by the padding.
		/// </summary>
		[Browsable(false)]
		public int Width
		{
			get { return Left + Right; }
		}

		/// <summary>
		/// Gets the total height represented by the padding.
		/// </summary>
		[Browsable(false)]
		public int Height
		{
			get { return Top + Bottom; }
		}

		/// <summary>
		/// Raises the PaddingChanged event.
		/// </summary>
		protected virtual void OnPaddingChanged()
		{
			if (PaddingChanged != null)
				PaddingChanged(this, EventArgs.Empty);
		}
	}
}
