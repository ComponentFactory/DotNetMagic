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
using System.Windows.Forms;

namespace Crownwood.DotNetMagic.Common
{
	/// <summary>
	/// Collection of static methods used to remove child controls and forms.
	/// </summary>
	/// <remarks>
	/// At the time of writing this class there exists a bug that is exposed when
	/// you remove a window that has the focus. In this case window is removed but
	/// the main application window will no longer close. To get around this a
	/// temporary window is created and takes the focus whilst the required window
	/// is actually removed.
	/// </remarks>
    public sealed class ControlHelper
	{
		// Prevent instance from being created.
		private ControlHelper() {}
	
		/// <summary>
		/// Remove all child controls from provided control.
		/// </summary>
		/// <param name="control">Parent control from which to remove all children.</param>
		public static void RemoveAll(Control control)
		{
			if ((control != null) && (control.Controls.Count > 0))
			{
  				// Remove all entries from target
				control.Controls.Clear();
			}
		}

		/// <summary>
		/// Remove specified child item from the provided control collection.
		/// </summary>
		/// <param name="coll">Control collection that child item needs to be removed from.</param>
		/// <param name="item">Child control to remove from collection.</param>
		public static void Remove(Control.ControlCollection coll, Control item)
		{
			if ((coll != null) && (item != null))
			{
				// Remove our target control
				coll.Remove(item);
			}
		}

		/// <summary>
		/// Remove indexed child item from the provided control collection.
		/// </summary>
		/// <param name="coll">Control collection that child item needs to be removed from.</param>
		/// <param name="index">Index into collection that needs removing.</param>
		public static void RemoveAt(Control.ControlCollection coll, int index)
		{
			if (coll != null)
			{
				if ((index >= 0) && (index < coll.Count))
					Remove(coll, coll[index]);
			}
		}
    
		/// <summary>
		/// Remove specified form as a child.
		/// </summary>
		/// <param name="source">Source control from same application as form.</param>
		/// <param name="form">Form to be removed as child.</param>
        public static void RemoveForm(Control source, Form form)
        {
            // Remove Form parent
            form.Parent = null;
        }
    }
}
