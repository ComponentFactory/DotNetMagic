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

namespace SampleSlidingTitleBar
{
	/// <summary>
	/// Summary description for SampleSlidingTitleBar.
	/// </summary>
	public class SampleSlidingTitleBar : System.Windows.Forms.Form
	{
		private Image _image;
		private bool _picture;
		private PictureBox _pictureBox;
		private ExampleUserControl _buttons;
	
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Crownwood.DotNetMagic.Controls.SlidingTitleBar slidingTitleBar1;
		private System.Windows.Forms.GroupBox groupBoxEdge;
		private System.Windows.Forms.RadioButton radioTop;
		private System.Windows.Forms.RadioButton radioBottom;
		private System.Windows.Forms.RadioButton radioLeft;
		private System.Windows.Forms.RadioButton radioRight;
		private System.Windows.Forms.GroupBox groupSliding;
		private System.Windows.Forms.NumericUpDown upSteps;
		private System.Windows.Forms.Label labelSteps;
		private System.Windows.Forms.Label labelDuration;
		private System.Windows.Forms.NumericUpDown upDuration;
		private System.Windows.Forms.Label labelDelay;
		private System.Windows.Forms.CheckBox checkBoxOnHover;
		private System.Windows.Forms.NumericUpDown upDelay;
		private System.Windows.Forms.GroupBox groupBoxDisplay;
		private System.Windows.Forms.CheckBox checkBoxOpen;
		private System.Windows.Forms.CheckBox checkBoxArrows;
		private System.Windows.Forms.GroupBox groupBoxContent;
		private System.Windows.Forms.RadioButton radioButtonPicture;
		private System.Windows.Forms.RadioButton radioButtonButtons;
		private System.Windows.Forms.GroupBox groupBoxTitleBar;
		private System.Windows.Forms.RadioButton radioButtonCustom1;
		private System.Windows.Forms.RadioButton radioButtonDef;
		private System.Windows.Forms.RadioButton radioButtonCustom2;
		private System.Windows.Forms.RadioButton radioButtonCustom3;
		private System.ComponentModel.Container components = null;

		public SampleSlidingTitleBar()
		{
			// Required for Windows Form Designer support
			InitializeComponent();

			// Load the example picture used in the default slider content			
			_image = ResourceHelper.LoadBitmap(this.GetType(), "SampleSlidingTitleBar.ExamplePicture.jpg");
			
			// Create the picture box and button examples
			_pictureBox = new PictureBox();
			_pictureBox.Image = _image;
			_pictureBox.Dock = DockStyle.Fill;
			_buttons = new ExampleUserControl();
			_buttons.Bar = slidingTitleBar1;
			_buttons.Dock = DockStyle.Fill;

			// Always start with the picture example
			slidingTitleBar1.Panel.Controls.Add(_pictureBox);
			_picture = true;
			
			UpdateControls();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SampleSlidingTitleBar));
			this.slidingTitleBar1 = new Crownwood.DotNetMagic.Controls.SlidingTitleBar();
			this.groupBoxEdge = new System.Windows.Forms.GroupBox();
			this.radioRight = new System.Windows.Forms.RadioButton();
			this.radioLeft = new System.Windows.Forms.RadioButton();
			this.radioBottom = new System.Windows.Forms.RadioButton();
			this.radioTop = new System.Windows.Forms.RadioButton();
			this.groupSliding = new System.Windows.Forms.GroupBox();
			this.checkBoxOnHover = new System.Windows.Forms.CheckBox();
			this.upDelay = new System.Windows.Forms.NumericUpDown();
			this.labelDelay = new System.Windows.Forms.Label();
			this.upDuration = new System.Windows.Forms.NumericUpDown();
			this.labelDuration = new System.Windows.Forms.Label();
			this.labelSteps = new System.Windows.Forms.Label();
			this.upSteps = new System.Windows.Forms.NumericUpDown();
			this.groupBoxDisplay = new System.Windows.Forms.GroupBox();
			this.checkBoxArrows = new System.Windows.Forms.CheckBox();
			this.checkBoxOpen = new System.Windows.Forms.CheckBox();
			this.groupBoxContent = new System.Windows.Forms.GroupBox();
			this.radioButtonButtons = new System.Windows.Forms.RadioButton();
			this.radioButtonPicture = new System.Windows.Forms.RadioButton();
			this.groupBoxTitleBar = new System.Windows.Forms.GroupBox();
			this.radioButtonCustom3 = new System.Windows.Forms.RadioButton();
			this.radioButtonCustom2 = new System.Windows.Forms.RadioButton();
			this.radioButtonCustom1 = new System.Windows.Forms.RadioButton();
			this.radioButtonDef = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.slidingTitleBar1)).BeginInit();
			this.groupBoxEdge.SuspendLayout();
			this.groupSliding.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.upDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.upDuration)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.upSteps)).BeginInit();
			this.groupBoxDisplay.SuspendLayout();
			this.groupBoxContent.SuspendLayout();
			this.groupBoxTitleBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// slidingTitleBar1
			// 
			this.slidingTitleBar1.GradientDirection = Crownwood.DotNetMagic.Controls.GradientDirection.TopToBottom;
			this.slidingTitleBar1.Location = new System.Drawing.Point(216, 24);
			this.slidingTitleBar1.MouseOverColor = System.Drawing.Color.Empty;
			this.slidingTitleBar1.Name = "slidingTitleBar1";
			// 
			// slidingTitleBar1.Panel
			// 
			this.slidingTitleBar1.Panel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.slidingTitleBar1.Panel.Location = new System.Drawing.Point(0, 26);
			this.slidingTitleBar1.Panel.Name = "";
			this.slidingTitleBar1.Panel.Size = new System.Drawing.Size(264, 238);
			this.slidingTitleBar1.Panel.TabIndex = 1;
			this.slidingTitleBar1.Size = new System.Drawing.Size(266, 266);
			this.slidingTitleBar1.TabIndex = 0;
			this.slidingTitleBar1.Text = "SlidingTitleBar Example";
			this.slidingTitleBar1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.slidingTitleBar1.OpenChanged += new System.EventHandler(this.checkBoxOpen_OpenChanged);
			// 
			// groupBoxEdge
			// 
			this.groupBoxEdge.Controls.Add(this.radioRight);
			this.groupBoxEdge.Controls.Add(this.radioLeft);
			this.groupBoxEdge.Controls.Add(this.radioBottom);
			this.groupBoxEdge.Controls.Add(this.radioTop);
			this.groupBoxEdge.Location = new System.Drawing.Point(16, 272);
			this.groupBoxEdge.Name = "groupBoxEdge";
			this.groupBoxEdge.Size = new System.Drawing.Size(144, 128);
			this.groupBoxEdge.TabIndex = 1;
			this.groupBoxEdge.TabStop = false;
			this.groupBoxEdge.Text = "TitleBar Edge";
			// 
			// radioRight
			// 
			this.radioRight.Location = new System.Drawing.Point(24, 96);
			this.radioRight.Name = "radioRight";
			this.radioRight.TabIndex = 3;
			this.radioRight.Text = "Right";
			this.radioRight.CheckedChanged += new System.EventHandler(this.radioRight_CheckedChanged);
			// 
			// radioLeft
			// 
			this.radioLeft.Location = new System.Drawing.Point(24, 72);
			this.radioLeft.Name = "radioLeft";
			this.radioLeft.TabIndex = 2;
			this.radioLeft.Text = "Left";
			this.radioLeft.CheckedChanged += new System.EventHandler(this.radioLeft_CheckedChanged);
			// 
			// radioBottom
			// 
			this.radioBottom.Location = new System.Drawing.Point(24, 48);
			this.radioBottom.Name = "radioBottom";
			this.radioBottom.TabIndex = 1;
			this.radioBottom.Text = "Bottom";
			this.radioBottom.CheckedChanged += new System.EventHandler(this.radioBottom_CheckedChanged);
			// 
			// radioTop
			// 
			this.radioTop.Location = new System.Drawing.Point(24, 24);
			this.radioTop.Name = "radioTop";
			this.radioTop.TabIndex = 0;
			this.radioTop.Text = "Top";
			this.radioTop.CheckedChanged += new System.EventHandler(this.radioTop_CheckedChanged);
			// 
			// groupSliding
			// 
			this.groupSliding.Controls.Add(this.checkBoxOnHover);
			this.groupSliding.Controls.Add(this.upDelay);
			this.groupSliding.Controls.Add(this.labelDelay);
			this.groupSliding.Controls.Add(this.upDuration);
			this.groupSliding.Controls.Add(this.labelDuration);
			this.groupSliding.Controls.Add(this.labelSteps);
			this.groupSliding.Controls.Add(this.upSteps);
			this.groupSliding.Location = new System.Drawing.Point(16, 16);
			this.groupSliding.Name = "groupSliding";
			this.groupSliding.Size = new System.Drawing.Size(144, 160);
			this.groupSliding.TabIndex = 2;
			this.groupSliding.TabStop = false;
			this.groupSliding.Text = "Sliding";
			// 
			// checkBoxOnHover
			// 
			this.checkBoxOnHover.Location = new System.Drawing.Point(32, 120);
			this.checkBoxOnHover.Name = "checkBoxOnHover";
			this.checkBoxOnHover.TabIndex = 6;
			this.checkBoxOnHover.Text = "Slide on Hover";
			this.checkBoxOnHover.CheckedChanged += new System.EventHandler(this.checkBoxOnHover_CheckedChanged);
			// 
			// upDelay
			// 
			this.upDelay.Increment = new System.Decimal(new int[] {
																	  50,
																	  0,
																	  0,
																	  0});
			this.upDelay.Location = new System.Drawing.Point(72, 88);
			this.upDelay.Maximum = new System.Decimal(new int[] {
																	9999,
																	0,
																	0,
																	0});
			this.upDelay.Minimum = new System.Decimal(new int[] {
																	1,
																	0,
																	0,
																	0});
			this.upDelay.Name = "upDelay";
			this.upDelay.Size = new System.Drawing.Size(56, 20);
			this.upDelay.TabIndex = 5;
			this.upDelay.Value = new System.Decimal(new int[] {
																  333,
																  0,
																  0,
																  0});
			this.upDelay.ValueChanged += new System.EventHandler(this.upDelay_ValueChanged);
			// 
			// labelDelay
			// 
			this.labelDelay.Location = new System.Drawing.Point(16, 88);
			this.labelDelay.Name = "labelDelay";
			this.labelDelay.Size = new System.Drawing.Size(48, 23);
			this.labelDelay.TabIndex = 4;
			this.labelDelay.Text = "Delay";
			this.labelDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// upDuration
			// 
			this.upDuration.Increment = new System.Decimal(new int[] {
																		 150,
																		 0,
																		 0,
																		 0});
			this.upDuration.Location = new System.Drawing.Point(72, 56);
			this.upDuration.Maximum = new System.Decimal(new int[] {
																	   99999,
																	   0,
																	   0,
																	   0});
			this.upDuration.Minimum = new System.Decimal(new int[] {
																	   1,
																	   0,
																	   0,
																	   0});
			this.upDuration.Name = "upDuration";
			this.upDuration.Size = new System.Drawing.Size(56, 20);
			this.upDuration.TabIndex = 3;
			this.upDuration.Value = new System.Decimal(new int[] {
																	 150,
																	 0,
																	 0,
																	 0});
			this.upDuration.ValueChanged += new System.EventHandler(this.upDuration_ValueChanged);
			// 
			// labelDuration
			// 
			this.labelDuration.Location = new System.Drawing.Point(16, 56);
			this.labelDuration.Name = "labelDuration";
			this.labelDuration.Size = new System.Drawing.Size(48, 23);
			this.labelDuration.TabIndex = 2;
			this.labelDuration.Text = "Duration";
			this.labelDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelSteps
			// 
			this.labelSteps.Location = new System.Drawing.Point(16, 24);
			this.labelSteps.Name = "labelSteps";
			this.labelSteps.Size = new System.Drawing.Size(48, 23);
			this.labelSteps.TabIndex = 1;
			this.labelSteps.Text = "Steps";
			this.labelSteps.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// upSteps
			// 
			this.upSteps.Location = new System.Drawing.Point(72, 24);
			this.upSteps.Maximum = new System.Decimal(new int[] {
																	9999,
																	0,
																	0,
																	0});
			this.upSteps.Minimum = new System.Decimal(new int[] {
																	1,
																	0,
																	0,
																	0});
			this.upSteps.Name = "upSteps";
			this.upSteps.Size = new System.Drawing.Size(56, 20);
			this.upSteps.TabIndex = 0;
			this.upSteps.Value = new System.Decimal(new int[] {
																  4,
																  0,
																  0,
																  0});
			this.upSteps.ValueChanged += new System.EventHandler(this.upSteps_ValueChanged);
			// 
			// groupBoxDisplay
			// 
			this.groupBoxDisplay.Controls.Add(this.checkBoxArrows);
			this.groupBoxDisplay.Controls.Add(this.checkBoxOpen);
			this.groupBoxDisplay.Location = new System.Drawing.Point(16, 184);
			this.groupBoxDisplay.Name = "groupBoxDisplay";
			this.groupBoxDisplay.Size = new System.Drawing.Size(144, 80);
			this.groupBoxDisplay.TabIndex = 3;
			this.groupBoxDisplay.TabStop = false;
			this.groupBoxDisplay.Text = "Display";
			// 
			// checkBoxArrows
			// 
			this.checkBoxArrows.Location = new System.Drawing.Point(24, 48);
			this.checkBoxArrows.Name = "checkBoxArrows";
			this.checkBoxArrows.TabIndex = 1;
			this.checkBoxArrows.Text = "Arrows";
			this.checkBoxArrows.CheckedChanged += new System.EventHandler(this.checkBoxArrows_CheckedChanged);
			// 
			// checkBoxOpen
			// 
			this.checkBoxOpen.Location = new System.Drawing.Point(24, 24);
			this.checkBoxOpen.Name = "checkBoxOpen";
			this.checkBoxOpen.TabIndex = 0;
			this.checkBoxOpen.Text = "Open";
			this.checkBoxOpen.CheckedChanged += new System.EventHandler(this.checkBoxOpen_CheckedChanged);
			// 
			// groupBoxContent
			// 
			this.groupBoxContent.Controls.Add(this.radioButtonButtons);
			this.groupBoxContent.Controls.Add(this.radioButtonPicture);
			this.groupBoxContent.Location = new System.Drawing.Point(176, 312);
			this.groupBoxContent.Name = "groupBoxContent";
			this.groupBoxContent.Size = new System.Drawing.Size(120, 88);
			this.groupBoxContent.TabIndex = 4;
			this.groupBoxContent.TabStop = false;
			this.groupBoxContent.Text = "Example Content";
			// 
			// radioButtonButtons
			// 
			this.radioButtonButtons.Location = new System.Drawing.Point(8, 48);
			this.radioButtonButtons.Name = "radioButtonButtons";
			this.radioButtonButtons.Size = new System.Drawing.Size(72, 24);
			this.radioButtonButtons.TabIndex = 5;
			this.radioButtonButtons.Text = "Buttons";
			this.radioButtonButtons.CheckedChanged += new System.EventHandler(this.radioButtonButtons_CheckedChanged);
			// 
			// radioButtonPicture
			// 
			this.radioButtonPicture.Location = new System.Drawing.Point(8, 24);
			this.radioButtonPicture.Name = "radioButtonPicture";
			this.radioButtonPicture.Size = new System.Drawing.Size(72, 24);
			this.radioButtonPicture.TabIndex = 4;
			this.radioButtonPicture.Text = "Picture";
			this.radioButtonPicture.CheckedChanged += new System.EventHandler(this.radioButtonPicture_CheckedChanged);
			// 
			// groupBoxTitleBar
			// 
			this.groupBoxTitleBar.Controls.Add(this.radioButtonCustom3);
			this.groupBoxTitleBar.Controls.Add(this.radioButtonCustom2);
			this.groupBoxTitleBar.Controls.Add(this.radioButtonCustom1);
			this.groupBoxTitleBar.Controls.Add(this.radioButtonDef);
			this.groupBoxTitleBar.Location = new System.Drawing.Point(312, 312);
			this.groupBoxTitleBar.Name = "groupBoxTitleBar";
			this.groupBoxTitleBar.Size = new System.Drawing.Size(216, 88);
			this.groupBoxTitleBar.TabIndex = 5;
			this.groupBoxTitleBar.TabStop = false;
			this.groupBoxTitleBar.Text = "Coloring";
			// 
			// radioButtonCustom3
			// 
			this.radioButtonCustom3.Location = new System.Drawing.Point(112, 56);
			this.radioButtonCustom3.Name = "radioButtonCustom3";
			this.radioButtonCustom3.Size = new System.Drawing.Size(96, 24);
			this.radioButtonCustom3.TabIndex = 15;
			this.radioButtonCustom3.Text = "Custom 3";
			this.radioButtonCustom3.CheckedChanged += new System.EventHandler(this.radioButtonCustom3_CheckedChanged);
			// 
			// radioButtonCustom2
			// 
			this.radioButtonCustom2.Location = new System.Drawing.Point(16, 56);
			this.radioButtonCustom2.Name = "radioButtonCustom2";
			this.radioButtonCustom2.Size = new System.Drawing.Size(96, 24);
			this.radioButtonCustom2.TabIndex = 14;
			this.radioButtonCustom2.Text = "Custom 2";
			this.radioButtonCustom2.CheckedChanged += new System.EventHandler(this.radioButtonCustom2_CheckedChanged);
			// 
			// radioButtonCustom1
			// 
			this.radioButtonCustom1.Location = new System.Drawing.Point(112, 32);
			this.radioButtonCustom1.Name = "radioButtonCustom1";
			this.radioButtonCustom1.Size = new System.Drawing.Size(96, 24);
			this.radioButtonCustom1.TabIndex = 13;
			this.radioButtonCustom1.Text = "Custom 1";
			this.radioButtonCustom1.CheckedChanged += new System.EventHandler(this.radioButtonCustom1_CheckedChanged);
			// 
			// radioButtonDef
			// 
			this.radioButtonDef.Checked = true;
			this.radioButtonDef.Location = new System.Drawing.Point(16, 32);
			this.radioButtonDef.Name = "radioButtonDef";
			this.radioButtonDef.Size = new System.Drawing.Size(96, 24);
			this.radioButtonDef.TabIndex = 12;
			this.radioButtonDef.TabStop = true;
			this.radioButtonDef.Text = "Default Colors";
			this.radioButtonDef.CheckedChanged += new System.EventHandler(this.radioButtonDef_CheckedChanged);
			// 
			// SampleSlidingTitleBar
			// 
			this.ClientSize = new System.Drawing.Size(544, 414);
			this.Controls.Add(this.groupBoxTitleBar);
			this.Controls.Add(this.groupBoxContent);
			this.Controls.Add(this.groupBoxDisplay);
			this.Controls.Add(this.groupSliding);
			this.Controls.Add(this.groupBoxEdge);
			this.Controls.Add(this.slidingTitleBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SampleSlidingTitleBar";
			this.Text = "DotNetMagic - SlidingTitleBar";
			((System.ComponentModel.ISupportInitialize)(this.slidingTitleBar1)).EndInit();
			this.groupBoxEdge.ResumeLayout(false);
			this.groupSliding.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.upDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.upDuration)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.upSteps)).EndInit();
			this.groupBoxDisplay.ResumeLayout(false);
			this.groupBoxContent.ResumeLayout(false);
			this.groupBoxTitleBar.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new SampleSlidingTitleBar());
		}

		private void UpdateControls()
		{
			radioTop.Checked = (slidingTitleBar1.Edge == TitleEdge.Top);
			radioBottom.Checked = (slidingTitleBar1.Edge == TitleEdge.Bottom);
			radioLeft.Checked = (slidingTitleBar1.Edge == TitleEdge.Left);
			radioRight.Checked = (slidingTitleBar1.Edge == TitleEdge.Right);
			upSteps.Value = slidingTitleBar1.SlideSteps;
			upDuration.Value = slidingTitleBar1.SlideDuration;
			checkBoxOnHover.Checked = slidingTitleBar1.SlideOnHover;
			upDelay.Value = slidingTitleBar1.HoverDelay;
			checkBoxOpen.Checked = slidingTitleBar1.Open;
			checkBoxArrows.Checked = slidingTitleBar1.Arrows;
			radioButtonPicture.Checked = _picture;
			radioButtonButtons.Checked = !_picture;
		}
		
		private void radioTop_CheckedChanged(object sender, System.EventArgs e)
		{
			if (radioTop.Checked)
			{
				slidingTitleBar1.Edge = TitleEdge.Top;
				UpdateControls();
			}
		}

		private void radioBottom_CheckedChanged(object sender, System.EventArgs e)
		{
			if (radioBottom.Checked)
			{
				slidingTitleBar1.Edge = TitleEdge.Bottom;
				UpdateControls();
			}
		}

		private void radioLeft_CheckedChanged(object sender, System.EventArgs e)
		{
			if (radioLeft.Checked)
			{
				slidingTitleBar1.Edge = TitleEdge.Left;
				UpdateControls();
			}
		}

		private void radioRight_CheckedChanged(object sender, System.EventArgs e)
		{
			if (radioRight.Checked)
			{
				slidingTitleBar1.Edge = TitleEdge.Right;
				UpdateControls();
			}
		}

		private void upSteps_ValueChanged(object sender, System.EventArgs e)
		{
			slidingTitleBar1.SlideSteps = (int)upSteps.Value;
		}

		private void upDuration_ValueChanged(object sender, System.EventArgs e)
		{
			slidingTitleBar1.SlideDuration = (int)upDuration.Value;
		}

		private void upDelay_ValueChanged(object sender, System.EventArgs e)
		{
			slidingTitleBar1.HoverDelay = (int)upDelay.Value;
		}

		private void checkBoxOnHover_CheckedChanged(object sender, System.EventArgs e)
		{
			slidingTitleBar1.SlideOnHover = checkBoxOnHover.Checked;
		}

		private void checkBoxOpen_OpenChanged(object sender, System.EventArgs e)
		{
			 checkBoxOpen.Checked = slidingTitleBar1.Open;
		}

		private void checkBoxOpen_CheckedChanged(object sender, System.EventArgs e)
		{
			slidingTitleBar1.Open = checkBoxOpen.Checked;
		}

		private void checkBoxArrows_CheckedChanged(object sender, System.EventArgs e)
		{
			slidingTitleBar1.Arrows = checkBoxArrows.Checked;
		}

		private void radioButtonPicture_CheckedChanged(object sender, System.EventArgs e)
		{
			if (radioButtonPicture.Checked)
			{
				slidingTitleBar1.Panel.Controls.Clear();
				slidingTitleBar1.Panel.Controls.Add(_pictureBox);
				_picture = true;
			}
		}

		private void radioButtonButtons_CheckedChanged(object sender, System.EventArgs e)
		{
			if (radioButtonButtons.Checked)
			{
				slidingTitleBar1.Panel.Controls.Clear();
				slidingTitleBar1.Panel.Controls.Add(_buttons);
				_picture = false;
			}
		}

		private void radioButtonDef_CheckedChanged(object sender, System.EventArgs e)
		{
			if (radioButtonDef.Checked)
			{
				slidingTitleBar1.ResetTitleBackColor();
				slidingTitleBar1.ResetTitleForeColor();
				slidingTitleBar1.ResetInactiveBackColor();
				slidingTitleBar1.ResetInactiveForeColor();
				slidingTitleBar1.ResetGradientActiveColor();
				slidingTitleBar1.ResetGradientInactiveColor();
			}
		}

		private void radioButtonCustom1_CheckedChanged(object sender, System.EventArgs e)
		{
			if (radioButtonCustom1.Checked)
			{
				slidingTitleBar1.TitleBackColor = Color.DarkRed;
				slidingTitleBar1.TitleForeColor = Color.White;
				slidingTitleBar1.InactiveBackColor = ControlPaint.Light(Color.DarkRed);
				slidingTitleBar1.InactiveForeColor = Color.Gray;
				slidingTitleBar1.GradientActiveColor = Color.Pink;
				slidingTitleBar1.GradientInactiveColor = ControlPaint.Light(Color.Pink);
			}
		}

		private void radioButtonCustom2_CheckedChanged(object sender, System.EventArgs e)
		{
			if (radioButtonCustom2.Checked)
			{
				slidingTitleBar1.TitleBackColor = Color.Moccasin;
				slidingTitleBar1.TitleForeColor = Color.Black;
				slidingTitleBar1.InactiveBackColor = ControlPaint.Light(Color.Moccasin);
				slidingTitleBar1.InactiveForeColor = Color.Gray;
				slidingTitleBar1.GradientActiveColor = Color.Tan;
				slidingTitleBar1.GradientInactiveColor = ControlPaint.Light(Color.Tan);
			}		
		}

		private void radioButtonCustom3_CheckedChanged(object sender, System.EventArgs e)
		{
			if (radioButtonCustom3.Checked)
			{
				slidingTitleBar1.TitleBackColor = Color.Blue;
				slidingTitleBar1.TitleForeColor = Color.White;
				slidingTitleBar1.InactiveBackColor = ControlPaint.Light(Color.Blue);
				slidingTitleBar1.InactiveForeColor = Color.Black;
				slidingTitleBar1.GradientActiveColor = Color.BlueViolet;
				slidingTitleBar1.GradientInactiveColor = ControlPaint.Light(Color.BlueViolet);
			}
		}
		
		private int GradientToInt(GradientDirection dir)
		{
			switch(dir)
			{
				case GradientDirection.None:
				default:
					return 0;
				case GradientDirection.LeftToRight:
					return 1;
				case GradientDirection.TopLeftToBottomRight:
					return 2;
				case GradientDirection.TopToBottom:
					return 3;
				case GradientDirection.TopRightToBottomLeft:
					return 4;
				case GradientDirection.RightToLeft:
					return 5;
				case GradientDirection.BottomRightToTopLeft:
					return 6;
				case GradientDirection.BottomToTop:
					return 7;
				case GradientDirection.BottomLeftToTopRight:
					return 8;
			}
		}

		private GradientDirection IntToGradient(int val)
		{
			switch(val)
			{
				case 0:
				default:
					return GradientDirection.None;
				case 1:
					return GradientDirection.LeftToRight;
				case 2:
					return GradientDirection.TopLeftToBottomRight;
				case 3:
					return GradientDirection.TopToBottom;
				case 4:
					return GradientDirection.TopRightToBottomLeft;
				case 5:
					return GradientDirection.RightToLeft;
				case 6:
					return GradientDirection.BottomRightToTopLeft;
				case 7:
					return GradientDirection.BottomToTop;
				case 8:
					return GradientDirection.BottomLeftToTopRight;
			}
		}
	}
}
