// *****************************************************************************
// 
//  (c) Crownwood Software Ltd 2004-2005. All rights reserved. 
//	The software and associated documentation supplied hereunder are the 
//	proprietary information of Crownwood Software Ltd, Bracknell, 
//	Berkshire, England and are supplied subject to licence terms.
// 
//  Version 3.0.3.0 	www.dotnetmagic.com
// *****************************************************************************

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls;
using Crownwood.DotNetMagic.Controls.Command;

namespace SampleToolControl
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class SampleToolControl : System.Windows.Forms.Form
	{
		// Instance fields - Images
		private Image _image1;
		private Image _image2;		
		private Image _image3;
		private Image _image4;
		
		// Instance fields - Commands
		private ButtonCommand _top;		
		private ButtonCommand _left;		
		private ButtonCommand _bottom;		
		private ButtonCommand _right;		
		private ButtonCommand _textTop;
		private ButtonCommand _textLeft;
		private ButtonCommand _textBottom;
		private ButtonCommand _textRight;
		private ButtonCommand _office;
		private ButtonCommand _ide2005;
		private ButtonCommand _ide;
		private ButtonCommand _plain;
		
		// Instance fields - Designer generated
		private Crownwood.DotNetMagic.Controls.ToolControl toolControl1;
		private Crownwood.DotNetMagic.Controls.ToolControl toolControl2;
		private System.Windows.Forms.TextBox textBox1;
		private Crownwood.DotNetMagic.Controls.StatusBarControl statusBarControl1;
		private Crownwood.DotNetMagic.Controls.StatusPanel statusPanelText;
		private Crownwood.DotNetMagic.Controls.StatusPanel statusPanelDate;
		private System.Windows.Forms.Timer timer;
		private System.ComponentModel.IContainer components;

		public SampleToolControl()
		{
			// Required for Windows Form Designer support
			InitializeComponent();

			// Load images
			_image1 = ResourceHelper.LoadBitmap(this.GetType(), "SampleToolControl.Image1.bmp", Point.Empty);
			_image2 = ResourceHelper.LoadBitmap(this.GetType(), "SampleToolControl.Image2.bmp", Point.Empty);
			_image3 = ResourceHelper.LoadBitmap(this.GetType(), "SampleToolControl.Image3.bmp", Point.Empty);
			_image4 = ResourceHelper.LoadBitmap(this.GetType(), "SampleToolControl.Image4.bmp", Point.Empty);
			
			SetupToolControl1Commands();
			SetupToolControl2Commands();
			
			timer_Tick(this, EventArgs.Empty);
		}

		private void SetupToolControl1Commands()
		{
			// Create command object
			_top = new ButtonCommand(null, "Top", null, new EventHandler(OnTop));
			_left = new ButtonCommand(null, "Left", null, new EventHandler(OnLeft));
			_bottom = new ButtonCommand(null, "Bottom", null, new EventHandler(OnBottom));
			_right = new ButtonCommand(null, "Right", null, new EventHandler(OnRight));
			SeparatorCommand sep1 = new SeparatorCommand();
			ButtonCommand image1 = new ButtonCommand(_image1, "", null, null);
			ButtonCommand image2 = new ButtonCommand(_image2, "", null, null);
			ButtonCommand image3 = new ButtonCommand(_image3, "", null, null);
			ButtonCommand image4 = new ButtonCommand(_image4, "", null, null);
			SeparatorCommand sep2 = new SeparatorCommand();
			ButtonCommand image5 = new ButtonCommand(_image1, "Save", null, null);
			ButtonCommand image6 = new ButtonCommand(_image2, "Delete", null, null);
			
			// Assign tooltip text
			_top.Tooltip = "Dock against top edge";
			_left.Tooltip = "Dock against left edge";
			_bottom.Tooltip = "Dock against bottom edge";
			_right.Tooltip = "Dock against right edge";
			image1.Tooltip = "Dummy command with image";
			image2.Tooltip = "Dummy command with image";
			image3.Tooltip = "Dummy command with image";
			image4.Tooltip = "Dummy command with image";
			image5.Tooltip = "Dummy command with image and text";
			image6.Tooltip = "Dummy command with image and text";
			
			// Set button state for those to be part of a group
			_top.ButtonStyle = ButtonStyle.ToggleButton;
			_left.ButtonStyle = ButtonStyle.ToggleButton;
			_bottom.ButtonStyle = ButtonStyle.ToggleButton;
			_right.ButtonStyle = ButtonStyle.ToggleButton;
			
			// This bar is docked against top by default
			_top.Pushed = true;
			
			// Add commands to the control
			toolControl1.Commands.AddRange(new CommandBase[]{_top, _left, _bottom, _right, sep1,
															 image1, image2, image3, image4, sep2,
															 image5, image6});
		}

		private void SetupToolControl2Commands()
		{
			// Create command object
			_textTop = new ButtonCommand(null, "TTop", null, new EventHandler(OnTextTop));
			_textLeft = new ButtonCommand(null, "TLeft", null, new EventHandler(OnTextLeft));
			_textBottom = new ButtonCommand(null, "TBottom", null, new EventHandler(OnTextBottom));
			_textRight = new ButtonCommand(null, "TRight", null, new EventHandler(OnTextRight));
			SeparatorCommand sep1 = new SeparatorCommand();
			_office = new ButtonCommand(null, "Office2003", null, new EventHandler(OnOffice2003));
			_ide2005 = new ButtonCommand(null, "IDE2005", null, new EventHandler(OnIDE2005));
			_ide = new ButtonCommand(null, "IDE", null, new EventHandler(OnIDE));
			_plain = new ButtonCommand(null, "Plain", null, new EventHandler(OnPlain));
			SeparatorCommand sep2 = new SeparatorCommand();
			ButtonCommand image1 = new ButtonCommand(_image1, "Save", null, null);
			ButtonCommand image2 = new ButtonCommand(_image2, "Delete", null, null);
			ButtonCommand image3 = new ButtonCommand(_image4, "Paste", null, null);
			
			// Assign tooltip text
			_textTop.Tooltip = "Set text at top";
			_textLeft.Tooltip = "Set text at left";
			_textBottom.Tooltip = "Set text at bottom";
			_textRight.Tooltip = "Set text at right";
			_office.Tooltip = "Use the Office2003 style";
			_plain.Tooltip = "Use the Plain style";
			_ide2005.Tooltip = "Use the IDE2005 style";
			_ide.Tooltip = "Use the IDE style";
			image1.Tooltip = "Example to show text positioning";
			image2.Tooltip = "Example to show text positioning";
			image3.Tooltip = "Example to show text positioning";
			
			// Set button state for those to be part of a group
			_textTop.ButtonStyle = ButtonStyle.ToggleButton;
			_textLeft.ButtonStyle = ButtonStyle.ToggleButton;
			_textBottom.ButtonStyle = ButtonStyle.ToggleButton;
			_textRight.ButtonStyle = ButtonStyle.ToggleButton;
			_office.ButtonStyle = ButtonStyle.ToggleButton;
			_ide2005.ButtonStyle = ButtonStyle.ToggleButton;
			_ide.ButtonStyle = ButtonStyle.ToggleButton;
			_plain.ButtonStyle = ButtonStyle.ToggleButton;
			
			// This bar is docked against top by default
			_textRight.Pushed = true;
			
			// Default to using Office2003 style
			_office.Pushed = true;
			
			// Add commands to the control
			toolControl2.Commands.AddRange(new CommandBase[]{_textTop, _textLeft, _textBottom, _textRight, sep1,
															 _office, _ide2005, _ide, _plain, sep2, image1, image2, image3});
		}

		private void OnTop(object sender, EventArgs e)
		{
			toolControl1.Dock = DockStyle.Top;	
			_top.Pushed = true;
			_left.Pushed = false;
			_bottom.Pushed = false;
			_right.Pushed = false;
		}

		private void OnLeft(object sender, EventArgs e)
		{
			toolControl1.Dock = DockStyle.Left;	
			_top.Pushed = false;
			_left.Pushed = true;
			_bottom.Pushed = false;
			_right.Pushed = false;
		}

		private void OnBottom(object sender, EventArgs e)
		{
			toolControl1.Dock = DockStyle.Bottom;	
			_top.Pushed = false;
			_left.Pushed = false;
			_bottom.Pushed = true;
			_right.Pushed = false;
		}
		
		private void OnRight(object sender, EventArgs e)
		{
			toolControl1.Dock = DockStyle.Right;	
			_top.Pushed = false;
			_left.Pushed = false;
			_bottom.Pushed = false;
			_right.Pushed = true;
		}
		
		private void OnTextTop(object sender, EventArgs e)
		{
			toolControl2.TextEdge = TextEdge.Top;
			_textTop.Pushed = true;
			_textLeft.Pushed = false;
			_textBottom.Pushed = false;
			_textRight.Pushed = false;
		}

		private void OnTextLeft(object sender, EventArgs e)
		{
			toolControl2.TextEdge = TextEdge.Left;	
			_textTop.Pushed = false;
			_textLeft.Pushed = true;
			_textBottom.Pushed = false;
			_textRight.Pushed = false;
		}

		private void OnTextBottom(object sender, EventArgs e)
		{
			toolControl2.TextEdge = TextEdge.Bottom;
			_textTop.Pushed = false;
			_textLeft.Pushed = false;
			_textBottom.Pushed = true;
			_textRight.Pushed = false;
		}
		
		private void OnTextRight(object sender, EventArgs e)
		{
			toolControl2.TextEdge = TextEdge.Right;
			_textTop.Pushed = false;
			_textLeft.Pushed = false;
			_textBottom.Pushed = false;
			_textRight.Pushed = true;
		}
		
		private void OnOffice2003(object sender, EventArgs e)
		{
			toolControl1.Style = VisualStyle.Office2003;
			toolControl2.Style = VisualStyle.Office2003;
			statusBarControl1.Style = VisualStyle.Office2003;
			_office.Pushed = true;
			_ide2005.Pushed = false;
			_ide.Pushed = false;
			_plain.Pushed = false;
		}

		private void OnIDE2005(object sender, EventArgs e)
		{
			toolControl1.Style = VisualStyle.IDE2005;
			toolControl2.Style = VisualStyle.IDE2005;
			statusBarControl1.Style = VisualStyle.IDE2005;
			_office.Pushed = false;
			_ide2005.Pushed = true;
			_ide.Pushed = false;
			_plain.Pushed = false;
		}

		private void OnIDE(object sender, EventArgs e)
		{
			toolControl1.Style = VisualStyle.IDE;
			toolControl2.Style = VisualStyle.IDE;
			statusBarControl1.Style = VisualStyle.IDE;
			_office.Pushed = false;
			_ide2005.Pushed = false;
			_ide.Pushed = true;
			_plain.Pushed = false;
		}
		
		private void OnPlain(object sender, EventArgs e)
		{
			toolControl1.Style = VisualStyle.Plain;
			toolControl2.Style = VisualStyle.Plain;
			statusBarControl1.Style = VisualStyle.Plain;
			_office.Pushed = false;
			_ide2005.Pushed = false;
			_ide.Pushed = false;
			_plain.Pushed = true;
		}

		private void timer_Tick(object sender, System.EventArgs e)
		{
			// Update the date and time section of the status bar
			statusPanelDate.Text = DateTime.Now.ToShortDateString();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SampleToolControl));
			this.toolControl1 = new Crownwood.DotNetMagic.Controls.ToolControl();
			this.toolControl2 = new Crownwood.DotNetMagic.Controls.ToolControl();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.statusBarControl1 = new Crownwood.DotNetMagic.Controls.StatusBarControl();
			this.statusPanelText = new Crownwood.DotNetMagic.Controls.StatusPanel();
			this.statusPanelDate = new Crownwood.DotNetMagic.Controls.StatusPanel();
			this.timer = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.toolControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolControl2)).BeginInit();
			this.SuspendLayout();
			// 
			// toolControl1
			// 
			this.toolControl1.Location = new System.Drawing.Point(0, 0);
			this.toolControl1.Name = "toolControl1";
			this.toolControl1.Size = new System.Drawing.Size(442, 4);
			this.toolControl1.TabIndex = 2;
			// 
			// toolControl2
			// 
			this.toolControl2.Location = new System.Drawing.Point(0, 4);
			this.toolControl2.Name = "toolControl2";
			this.toolControl2.Size = new System.Drawing.Size(442, 4);
			this.toolControl2.TabIndex = 3;
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(0, 8);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(442, 274);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "";
			// 
			// statusBarControl1
			// 
			this.statusBarControl1.Location = new System.Drawing.Point(0, 282);
			this.statusBarControl1.Name = "statusBarControl1";
			this.statusBarControl1.PadTop = 3;
			this.statusBarControl1.Size = new System.Drawing.Size(442, 24);
			this.statusBarControl1.StatusPanels.AddRange(new Crownwood.DotNetMagic.Controls.StatusPanel[] {
																											  this.statusPanelText,
																											  this.statusPanelDate});
			this.statusBarControl1.TabIndex = 5;
			// 
			// statusPanelText
			// 
			this.statusPanelText.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.statusPanelText.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.statusPanelText.AutoSizing = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusPanelText.Location = new System.Drawing.Point(2, 2);
			this.statusPanelText.Name = "statusPanelText";
			this.statusPanelText.Size = new System.Drawing.Size(408, 16);
			this.statusPanelText.TabIndex = 0;
			// 
			// statusPanelDate
			// 
			this.statusPanelDate.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.statusPanelDate.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.statusPanelDate.AutoSizing = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statusPanelDate.Location = new System.Drawing.Point(2, 2);
			this.statusPanelDate.Name = "statusPanelDate";
			this.statusPanelDate.Size = new System.Drawing.Size(0, 16);
			this.statusPanelDate.TabIndex = 0;
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 900;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// SampleToolControl
			// 
			this.ClientSize = new System.Drawing.Size(442, 306);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.toolControl2);
			this.Controls.Add(this.toolControl1);
			this.Controls.Add(this.statusBarControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SampleToolControl";
			this.Text = "DotNetMagic - SampleToolControl";
			((System.ComponentModel.ISupportInitialize)(this.toolControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolControl2)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new SampleToolControl());
		}
	}
}
