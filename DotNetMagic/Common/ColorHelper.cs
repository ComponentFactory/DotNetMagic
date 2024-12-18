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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Crownwood.DotNetMagic.Win32;

namespace Crownwood.DotNetMagic.Common
{
	/// <summary>
	/// Collection of static helper methods for processing colors.
	/// </summary>
    public sealed class ColorHelper
    {
		// Prevent instance from being created.
		private ColorHelper() {}

		/// <summary>
		/// Calculate the background color for the TabControl from a base color.
		/// </summary>
		/// <param name="backColor">Base color used to derive background color.</param>
		/// <returns>Color to use as background.</returns>
		public static Color TabBackgroundFromBaseColor(Color backColor)
		{
			Color backIDE;

			// Check for the 'Classic' control color
			if ((backColor.R == 212) &&
				(backColor.G == 208) &&
				(backColor.B == 200))
			{
				// Use the exact background for this color
				backIDE = Color.FromArgb(247, 243, 233);
			}
			else
			{
				// Check for the 'XP' control color
				if ((backColor.R == 236) &&
					(backColor.G == 233) &&
					(backColor.B == 216))
				{
					// Use the exact background for this color
					backIDE = Color.FromArgb(255, 251, 233);
				}
				else
				{
					// Calculate the IDE background color as only half as dark as the control color
					int red = 255 - ((255 - backColor.R) / 2);
					int green = 255 - ((255 - backColor.G) / 2);
					int blue = 255 - ((255 - backColor.B) / 2);
					backIDE = Color.FromArgb(red, green, blue);
				}
			}
                        
			return backIDE;
		}

		/// <summary>
		/// Calculate an opaque color that is the equivalent of an alpha blended color.
		/// </summary>
		/// <param name="front">Foreground color.</param>
		/// <param name="back">Background color.</param>
		/// <param name="alpha">Level of alpha blending to apply between foreground and background.</param>
		/// <returns>New alpha blended opaque color.</returns>
		public static Color CalculateColor(Color front, Color back, int alpha)
		{
			// Use alpha blending to brigthen the colors but don't use it
			// directly. Instead derive an opaque color that we can use.
			Color frontColor = Color.FromArgb(255, front);
			Color backColor = Color.FromArgb(255, back);

			float frontRed = frontColor.R;
			float frontGreen = frontColor.G;
			float frontBlue = frontColor.B;
			float backRed = backColor.R;
			float backGreen = backColor.G;
			float backBlue = backColor.B;

			float fRed = (frontRed * alpha / 255) + backRed * ((float)(255-alpha)/255);
			float fGreen = (frontGreen * alpha / 255) + backGreen * ((float)(255-alpha)/255);
			float fBlue = (frontBlue * alpha / 255) + backBlue * ((float)(255-alpha)/255);

			byte newRed = (byte)fRed;
			byte newGreen = (byte)fGreen;
			byte newBlue = (byte)fBlue;

			return Color.FromArgb(255, newRed, newGreen, newBlue);
		}

		/// <summary>
		/// Get the number of bits used to define the color depth of the display.
		/// </summary>
		/// <returns>Number of bits in color depth.</returns>
		public static int ColorDepth()
		{
			// Get access to the desktop DC
			IntPtr desktopDC = User32.GetDC(IntPtr.Zero);

			// Find raw values that define the color depth
			int planes = Gdi32.GetDeviceCaps(desktopDC, (int)DeviceCapValues.PLANES);
			int bitsPerPixel = Gdi32.GetDeviceCaps(desktopDC, (int)DeviceCapValues.BITSPIXEL);

			// Must remember to release it!
			User32.ReleaseDC(IntPtr.Zero, desktopDC);

			return planes * bitsPerPixel;
		}

		/// <summary>
		/// Merge two colors together using the given relative percentages.
		/// </summary>
		/// <param name="color1">First color.</param>
		/// <param name="percent1">Percentage of first color to use.</param>
		/// <param name="color2">Second color.</param>
		/// <param name="percent2">Percentage of second color to use.</param>
		/// <returns>Merged color.</returns>
		public static Color MergeColors(Color color1, float percent1, 
										Color color2, float percent2)
		{
			// Use existing three color merge
			return ColorHelper.MergeColors(color1, percent1, 
										   color2, percent2, 
										   Color.Empty, 0f);
		}

		/// <summary>
		/// Merge three colors together using the given relative percentages.
		/// </summary>
		/// <param name="color1">First color.</param>
		/// <param name="percent1">Percentage of first color to use.</param>
		/// <param name="color2">Second color.</param>
		/// <param name="percent2">Percentage of second color to use.</param>
		/// <param name="color3">Third color.</param>
		/// <param name="percent3">Percentage of third color to use.</param>
		/// <returns>Merged color.</returns>
		public static Color MergeColors(Color color1, float percent1, 
										Color color2, float percent2, 
										Color color3, float percent3)
		{
			// Find how much to use of each primary color
			int red = (int) ((color1.R * percent1) + (color2.R * percent2) + (color3.R * percent3));
			int green = (int) ((color1.G * percent1) + (color2.G * percent2) + (color3.G * percent3));
			int blue = (int) ((color1.B * percent1) + (color2.B * percent2) + (color3.B * percent3));

			// Limit check against individual component
			if (red < 0)	 red = 0;
			if (red > 255)	 red = 255;
			if (green < 0)	 green = 0;
			if (green > 255) green = 255;
			if (blue < 0)	 blue = 0;
			if (blue > 255)	 blue = 255;
	
			// Return the merged color
			return Color.FromArgb(red, green, blue);
		}
	}
}


