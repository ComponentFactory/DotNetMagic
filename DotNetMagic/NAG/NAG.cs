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
	/// Internal class used to decide if a NAG screen should be shown.
	/// </summary>
	internal class NAG
	{
		internal static bool _started = true;

		/// <summary>
		/// Called by every DotNetMagic control constructor to ensure NAG shown.
		/// </summary>
		public static void NAG_Start()
		{
			// Only should NAG first time around
			if (!_started)
			{
				NAGScreen nag = new NAGScreen();
				nag.ShowDialog();
				
				_started = true;
			}
		}
	}
}
