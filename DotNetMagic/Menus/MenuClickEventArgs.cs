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

namespace Crownwood.DotNetMagic.Menus
{
	/// <summary>
	/// Provides information about the source of the Click event for a MenuCommand.
	/// </summary>
    public class MenuClickEventArgs : EventArgs
    {
		/// <summary>
		/// Specifies the source of a Click event.
		/// </summary>
		public enum ClickSource
		{
			/// <summary>
			/// Specifies the Mouse value.
			/// </summary>
			Mouse,
			/// <summary>
			/// Specifies the Return value.
			/// </summary>
			Return,
			/// <summary>
			/// Specifies the Mnemonic value.
			/// </summary>
			Mnemonic,
			/// <summary>
			/// Specifies the Shortcut value.
			/// </summary>
			Shortcut
		}

        // Instance fields
		private ClickSource _source;

		/// <summary>
		/// Initialize a new instance of the MenuClickEventArgs class.
		/// </summary>
        public MenuClickEventArgs(ClickSource source)
        {
			// Remember source
			_source = source;
        }

		/// <summary>
		/// Gets the source of the event.
		/// </summary>
		public ClickSource Source
		{
			get { return _source; }
		}
    }
}
