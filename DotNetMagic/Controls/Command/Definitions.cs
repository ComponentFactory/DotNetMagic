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

namespace Crownwood.DotNetMagic.Controls.Command
{
	/// <summary>
	/// Specifies the position of a command.
	/// </summary>
	public enum Position
	{
		/// <summary>
		/// Specified a command be placed in the normal position.
		/// </summary>
		Near,

		/// <summary>
		/// Specifies a command be placed opposite the normal position.
		/// </summary>
		Far,

		/// <summary>
		/// Specified a command be placed with system commands.
		/// </summary>
		System
	}

	/// <summary>
	/// Specifies if the image and text should be displayed.
	/// </summary>
	public enum ImageAndText
	{
		/// <summary>
		/// Specifies that only the image should be shown.
		/// </summary>
		ImageOnly,

		/// <summary>
		/// Specifies that only the text should be shown.
		/// </summary>
		TextOnly,

		/// <summary>
		/// Specifies that the image and text should be shown.
		/// </summary>
		Both
	}
    
	/// <summary>
	/// Interface required when positioning or drawing a command.
	/// </summary>
	public interface ICommandDetails
	{
		/// <summary>
		/// Drawing font.
		/// </summary>
		Font Font { get; }

		/// <summary>
		/// Drawing visual style.
		/// </summary>
		VisualStyle Style { get; }

		/// <summary>
		/// Flow direction.
		/// </summary>
		LayoutDirection Direction { get; }

		/// <summary>
		/// Image and Text should be shown.
		/// </summary>
		ImageAndText ImageAndText { get; }

		/// <summary>
		/// Should text be shown horizontal always.
		/// </summary>
		bool OnlyHorizontalText { get; }

		/// <summary>
		/// Drawing edge for text.
		/// </summary>
		TextEdge TextEdge { get; }

        /// <summary>
        /// Gets a value indicating if all buttons have equal height of tallest button.
        /// </summary>
        bool EqualButtonVert { get; }

        /// <summary>
        /// Gets a value indicating if all buttons have equal width of widest button.
        /// </summary>
        bool EqualButtonHorz { get; }
        
		/// <summary>
		/// Bounding rectangle for drawing.
		/// </summary>
		Rectangle LayoutRect { get; }
		
		/// <summary>
		/// Control hosting commands.
		/// </summary>
		Control HostControl { get; }

		/// <summary>
		/// Text color used for drawing any text strings.
		/// </summary>
		Color TextColor { get; }

		/// <summary>
		/// Base color used for drawing background related items.
		/// </summary>
		Color BaseColor { get; }

		/// <summary>
		/// Base color used for drawing hot tracking items.
		/// </summary>
		Color TrackBaseColor1 { get; }

		/// <summary>
		/// Base color used for drawing hot tracking items.
		/// </summary>
		Color TrackBaseColor2 { get; }

		/// <summary>
		/// Top Lighter color used for drawing hot tracking items.
		/// </summary>
		Color TrackLightColor1 { get; }

		/// <summary>
		/// Bottom Lighter color used for drawing hot tracking items.
		/// </summary>
		Color TrackLightColor2 { get; }

		/// <summary>
		/// Top Very light color used for drawing hot tracking items.
		/// </summary>
		Color TrackLightLightColor1 { get; }

		/// <summary>
		/// Bottom Very light color used for drawing hot tracking items.
		/// </summary>
		Color TrackLightLightColor2 { get; }

		/// <summary>
		/// Darker color used for drawing hot tracking items.
		/// </summary>
		Color TrackDarkColor { get; }

		/// <summary>
		/// Dark color used for separators.
		/// </summary>
		Color SepDarkColor { get; }

		/// <summary>
		/// Light color used for separators.
		/// </summary>
		Color SepLightColor { get; }

		/// <summary>
		/// Top Base color used for drawing open menu items.
		/// </summary>
		Color OpenBaseColor1 { get; }

		/// <summary>
		/// Bottom Base color used for drawing open menu items.
		/// </summary>
		Color OpenBaseColor2 { get; }

		/// <summary>
		/// Color used for drawing open borders.
		/// </summary>
		Color OpenBorderColor { get; }
	}

	/// <summary>
	/// Interface implemented by commands that can open.
	/// </summary>
	public interface ICommandOpen
	{
		/// <summary>
		/// Gets a value indicating of command can currently be opened.
		/// </summary>
		/// <returns></returns>
		bool CanOpen();
		
		/// <summary>
		/// Command is requested to open itself.
		/// </summary>
		/// <param name="screenPt"></param>
		void Open(Point screenPt);

		/// <summary>
		/// Command is requested to close itself.
		/// </summary>
		void Close();
	}
	
	/// <summary>
	/// Interface implemented by commands that are buttons.
	/// </summary>
	public interface ICommandIsButton
	{
	}

	/// <summary>
	/// Interface implemented by any engine used to layout commands.
	/// </summary>
	public interface ILayoutEngine
	{
		/// <summary>
		/// Perform sizing and positioning of commands.
		/// </summary>
		Rectangle LayoutCommands(CommandStateCollection states,
								 ICommandDetails details);

		/// <summary>
		/// Find the ideal size to fit all the commands inside.
		/// </summary>
		Rectangle FindIdealSize(CommandStateCollection states,
							    ICommandDetails details);
}
}
