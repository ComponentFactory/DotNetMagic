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
using System.Windows.Forms;
using Crownwood.DotNetMagic.Common;

namespace Crownwood.DotNetMagic.Menus
{
	/// <summary>
	/// Provides a collection container for MenuCommand instances.
	/// </summary>
    public class MenuCommandCollection : CollectionWithEvents, IDisposable
    {
        // Instance fields
        private string _extraText;
        private Font _extraFont;
        private Color _extraTextColor;
        private Brush _extraTextBrush;
        private Color _extraBackColor;
        private Brush _extraBackBrush;
        private bool _showInfrequent;

		/// <summary>
		/// Initializes a new instance of the MenuCommandCollection class.
		/// </summary>
        public MenuCommandCollection()
        {
            // Define defaults for internal state
            _extraText = "";
            _extraFont = null;
            _extraTextColor = SystemColors.ActiveCaptionText;
            _extraTextBrush = null;
            _extraBackColor = SystemColors.ActiveCaption;
            _extraBackBrush = null;
            _showInfrequent = false;
        }

		/// <summary>
		/// Dispose of resources.
		/// </summary>
		public void Dispose()
		{
			// Dispose of system resource
			if (_extraFont != null)
			{
				_extraFont.Dispose();
				_extraFont = null;
			}
		}

		/// <summary>
		/// Adds the specified MenuCommand object to the collection.
		/// </summary>
		/// <param name="value">The MenuCommand object to add to the collection.</param>
		/// <returns>The MenuCommand object added to the collection.</returns>
        public MenuCommand Add(MenuCommand value)
        {
            // Use base class to process actual collection operation
            base.List.Add(value as object);

            return value;
        }

		/// <summary>
		/// Adds an array of MenuCommand objects to the collection.
		/// </summary>
		/// <param name="values">An array of MenuCommand objects to add to the collection.</param>
        public void AddRange(MenuCommand[] values)
        {
            // Use existing method to add each array entry
            foreach(MenuCommand page in values)
                Add(page);
        }

		/// <summary>
		/// Removes a MenuCommand from the collection.
		/// </summary>
		/// <param name="value">A MenuCommand to remove from the collection.</param>
        public void Remove(MenuCommand value)
        {
            // Use base class to process actual collection operation
            base.List.Remove(value as object);
        }

		/// <summary>
		/// Inserts a MenuCommand instance into the collection at the specified location.
		/// </summary>
		/// <param name="index">The location in the collection where you want to add the MenuCommand.</param>
		/// <param name="value">The MenuCommand object to insert.</param>
        public void Insert(int index, MenuCommand value)
        {
            // Use base class to process actual collection operation
            base.List.Insert(index, value as object);
        }

		/// <summary>
		/// Determines whether a MenuCommand is in the collection.
		/// </summary>
		/// <param name="value">The MenuCommand to locate in the collection.</param>
		/// <returns>true if item is found in the collection; otherwise, false.</returns>
        public bool Contains(MenuCommand value)
        {
            // Use base class to process actual collection operation
            return base.List.Contains(value as object);
        }

		/// <summary>
		/// Gets the MenuCommand at the specified index.
		/// </summary>
        public MenuCommand this[int index]
        {
            // Use base class to process actual collection operation
            get { return (base.List[index] as MenuCommand); }
        }

		/// <summary>
		/// Gets the MenuCommand with the specified text.
		/// </summary>
        public MenuCommand this[string text]
        {
            get 
            {
                // Search for a MenuCommand with a matching title
                foreach(MenuCommand mc in base.List)
                    if (mc.Text == text)
                        return mc;

                return null;
            }
        }

		/// <summary>
		/// Returns the index of the first occurrence of the given MenuCommand.
		/// </summary>
		/// <param name="value">The MenuCommand to locate.</param>
		/// <returns>Index of object; otherwise -1</returns>
        public int IndexOf(MenuCommand value)
        {
            // Find the 0 based index of the requested entry
            return base.List.IndexOf(value);
        }

		/// <summary>
		/// Determine if there are any visible items in the collection.
		/// </summary>
		/// <returns>true if at least one item is visible; otherwise false</returns>
        public bool VisibleItems()
        {
            foreach(MenuCommand mc in base.List)
            {
                // Is the item visible?
                if (mc.Visible)
                {
                    // And its not a separator...
                    if (mc.Text != "-")
                    {
                        // Then should return 'true' except when we are a sub menu item ourself
                        // in which case there might not be any visible children which means that
                        // this item would not be visible either.
                        if ((mc.MenuCommands.Count > 0) && (!mc.MenuCommands.VisibleItems()))
                            continue;

                        return true;
                    }
                }
            }

            return false;
        }

		/// <summary>
		/// Gets or sets the ExtraText associated with this MenuCommandCollection.
		/// </summary>
        public string ExtraText
        {
            get { return _extraText; }
            set { _extraText = value; }
        }

		/// <summary>
		/// Gets or sets the ExtraFont associated with this MenuCommandCollection.
		/// </summary>
        public Font ExtraFont
        {
            get 
			{ 
				if (_extraFont == null)
		            _extraFont = SystemInformation.MenuFont;

				return _extraFont; 
			}

            set { _extraFont = value; }
        }

		/// <summary>
		/// Gets or sets the ExtraTextColor associated with this MenuCommandCollection.
		/// </summary>
        public Color ExtraTextColor
        {
            get { return _extraTextColor; }
            set { _extraTextColor = value; }
        }

		/// <summary>
		/// Gets or sets the ExtraTextBrush associated with this MenuCommandCollection.
		/// </summary>
        public Brush ExtraTextBrush
        {
            get { return _extraTextBrush; }
            set { _extraTextBrush = value; }
        }

		/// <summary>
		/// Gets or sets the ExtraBackColor associated with this MenuCommandCollection.
		/// </summary>
        public Color ExtraBackColor
        {
            get { return _extraBackColor; }
            set { _extraBackColor = value; }
        }

		/// <summary>
		/// Gets or sets the ExtraBackBrush associated with this MenuCommandCollection.
		/// </summary>
        public Brush ExtraBackBrush
        {
            get { return _extraBackBrush; }
            set { _extraBackBrush = value; }
        }

		/// <summary>
		/// Gets or sets the ShowInfrequent associated with this MenuCommandCollection.
		/// </summary>
        public bool ShowInfrequent
        {
            get { return _showInfrequent; }
            set { _showInfrequent = value; }
		}
	}
}
