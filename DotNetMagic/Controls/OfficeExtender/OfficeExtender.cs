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
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using Microsoft.Win32;
using Crownwood.DotNetMagic.Common;

namespace Crownwood.DotNetMagic.Controls
{
	/// <summary>
	/// Provide extra property to controls to automatically update colors based on Office2003.
	/// </summary>
	[ToolboxBitmap(typeof(OfficeExtender))]
	[ProvideProperty("Office2003BackColor", typeof(Control))]
	public class OfficeExtender : Component, IExtenderProvider
	{
		// Instance field
		private Hashtable _controls;
		private ColorDetails _colorDetails;
	
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initialize a new instance of the OfficeExtender class.
		/// </summary>
		public OfficeExtender()
		{
			// List of controls being managed
			_controls = new Hashtable();
			
			// Helper for Office2003 colors
			_colorDetails = new ColorDetails();
			
			// Need to know when system colors change
			Microsoft.Win32.SystemEvents.UserPreferenceChanged += 
				new UserPreferenceChangedEventHandler(OnPreferenceChanged);

			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();
		}

		/// <summary>
		/// Initialize a new instance of the OfficeExtender class.
		/// </summary>
		/// <param name="container">Parent container.</param>
		public OfficeExtender(System.ComponentModel.IContainer container)
		{
			// List of controls being managed
			_controls = new Hashtable();

			// Helper for Office2003 colors
			_colorDetails = new ColorDetails();

			// Need to know when system colors change
			Microsoft.Win32.SystemEvents.UserPreferenceChanged += 
				new UserPreferenceChangedEventHandler(OnPreferenceChanged);

			// Required for Windows.Forms Class Composition Designer support
			container.Add(this);
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				// Clear down the table of referenced controls
				_controls.Clear();
				
				// Must unhook from events to ensure garbage collection
				Microsoft.Win32.SystemEvents.UserPreferenceChanged -= 
					new UserPreferenceChangedEventHandler(OnPreferenceChanged);

				// Color details has resources that need releasing
				_colorDetails.Dispose();

				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		/// <summary>
		/// Specifies whether this object can provide its extender properties to the specified object.
		/// </summary>
		/// <param name="extendee">The Object to receive the extender properties. </param>
		/// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
		public bool CanExtend(object extendee)
		{
			// We cannot extend ourself
			if (extendee.GetType() == this.GetType())
				return false;
				
			// We can extend anything that has a BackColor property, which
			// is anything that derives from Control
			return extendee is Control;
		}

		/// <summary>
		/// Gets a value indicating if the given control is to automatically get the Office2003 back color.
		/// </summary>
		/// <param name="control">Target control.</param>
		/// <returns>true if back color auto changed; otherwise false.</returns>
		[DefaultValue(false)]
		public Office2003Color GetOffice2003BackColor(Control control)
		{
			// If already a control we know about
			if (_controls.Contains(control))
			{
				// Then return the saved state
				return (Office2003Color)_controls[control];
			}
			else
			{
				// Otherwise add to list with default value
				_controls.Add(control, Office2003Color.Disable);
				return Office2003Color.Disable;
			}
		}
		
		/// <summary>
		/// Gets a value indicating if the given control is to automatically get the Office2003 back color.
		/// </summary>
		/// <param name="control">Target control.</param>
		/// <param name="color">How to color the background.</param>
		public void SetOffice2003BackColor(Control control, Office2003Color color)
		{
			bool wasFound = false;
		
			// Is the control already in the lookup?
			if (_controls.Contains(control))
			{
				wasFound = true;
				
				// Then remove it
				_controls.Remove(control);
			}
			
			// Add back again with the new value
			_controls.Add(control, color);
			
			switch(color)
			{
				case Office2003Color.Disable:
					if (wasFound)
						control.ResetBackColor();
					break;
				case Office2003Color.Base:
					control.BackColor = _colorDetails.BaseColor;
					break;
				case Office2003Color.Light:
					control.BackColor = _colorDetails.BaseColor1;
					break;
				case Office2003Color.Dark:
					control.BackColor = _colorDetails.BaseColor2;
					break;
				case Office2003Color.Enhanced:
					control.BackColor = _colorDetails.TrackLightColor2;
					break;
			}
		}
		
		/// <summary>
		/// Remove the specified control from the managed collection.
		/// </summary>
		/// <param name="c">Control instance to remove.</param>
		public void RemoveControl(Control c)
		{
			// Only remove if in the collection
			if (_controls.Contains(c))
				_controls.Remove(c);
		}
		
		private void OnPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			// Need to get the latest theme
			_colorDetails.Reset();
		
			// Update each control with latest color
			foreach(Control c in _controls.Keys)
			{
				Office2003Color color = (Office2003Color)_controls[c];
				
				switch(color)
				{
					case Office2003Color.Base:
						c.BackColor = _colorDetails.BaseColor;
						break;
					case Office2003Color.Light:
						c.BackColor = _colorDetails.BaseColor1;
						break;
					case Office2003Color.Dark:
						c.BackColor = _colorDetails.BaseColor2;
						break;
					case Office2003Color.Enhanced:
						c.BackColor = _colorDetails.TrackLightColor2;
						break;
				}
			}
		}
	}
	
	/// <summary>
	/// Specifies the color to use on the background.
	/// </summary>
	public enum Office2003Color
	{
		/// <summary>
		/// Specifies the extender should not change the property.
		/// </summary>
		Disable, 

		/// <summary>
		/// Specifies a base color.
		/// </summary>
		Base, 

		/// <summary>
		/// Specifies a light color.
		/// </summary>
		Light, 

		/// <summary>
		/// Specifies a dark color.
		/// </summary>
		Dark,
	
		/// <summary>
		/// Specifies a selected/enhanced color.
		/// </summary>
		Enhanced
	}
}
