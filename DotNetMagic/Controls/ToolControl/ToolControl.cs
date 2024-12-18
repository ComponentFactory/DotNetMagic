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
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls.Command;

namespace Crownwood.DotNetMagic.Controls
{
	/// <summary>
	/// Toolbar specific specialization of the base command control
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(ToolControl))]
	public class ToolControl : CommandControl
	{
		/// <summary>
		/// Initialize a new instance of the ToolControl class.
		/// </summary>
		public ToolControl()
		{
			// NAG processing
			NAG.NAG_Start();
		}
	}
}

