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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Crownwood.DotNetMagic.Menus
{
	/// <summary>
	/// Provide designer support for the MenuControl.
	/// </summary>
    public class MenuControlDesigner :  System.Windows.Forms.Design.ParentControlDesigner
    {
		/// <summary>
		/// Gets the collection of components associated with the component managed by the designer.
		/// </summary>
        public override ICollection AssociatedComponents
        {
            get 
            {
                if (base.Control is Crownwood.DotNetMagic.Menus.MenuControl)
                    return ((Crownwood.DotNetMagic.Menus.MenuControl)base.Control).MenuCommands;
                else
                    return base.AssociatedComponents;
            }
        }

		/// <summary>
		/// Gets or sets a value indicating whether a grid should be drawn on the control for this designer.
		/// </summary>
        protected override bool DrawGrid
        {
            get { return false; }
        }
    }
}
