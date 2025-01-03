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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls;

namespace SampleStatusBarControl
{
	/// <summary>
	/// Summary description for SampleStatusBarControl.
	/// </summary>
	public class SampleStatusBarControl : System.Windows.Forms.Form
	{
		// Instance fields
		private ImageList imageList;
	
		// Designer generated
		private Crownwood.DotNetMagic.Controls.StatusBarControl statusBarControl1;
		private System.Windows.Forms.NumericUpDown numericWidth;
		private System.Windows.Forms.Label labelWidth;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.Label labelText;
		private System.Windows.Forms.Label labelIcon;
		private System.Windows.Forms.ComboBox comboBoxIcon;
		private System.Windows.Forms.Label labelSize;
		private System.Windows.Forms.ComboBox comboBoxSize;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.GroupBox groupBoxAdd;
		private System.Windows.Forms.Button buttonRemoveAll;
		private System.Windows.Forms.Button buttonRemoveFirst;
		private System.Windows.Forms.Button buttonAddTime;
		private System.Windows.Forms.Label labelBorder;
		private System.Windows.Forms.ComboBox comboBoxBorder;
		private System.Windows.Forms.Label labelAlignment;
		private System.Windows.Forms.ComboBox comboBoxAlignment;
		private System.Windows.Forms.Timer timerForPanels;
		private System.Windows.Forms.Button buttonAP;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioOffice2003;
		private System.Windows.Forms.RadioButton radioIDE;
		private System.Windows.Forms.RadioButton radioPlain;
		private System.Windows.Forms.RadioButton radioIDE2005;
		private System.ComponentModel.IContainer components;

		public SampleStatusBarControl()
		{
			// Required for Windows Form Designer support
			InitializeComponent();
			
			// Load the strip of simple images
			imageList = ResourceHelper.LoadBitmapStrip(typeof(SampleStatusBarControl), 
													   "SampleStatusBarControl.SampleImages.bmp", 
													   new Size(16, 16), 
													   Point.Empty);
			
			comboBoxIcon.SelectedIndex = 0;
			comboBoxSize.SelectedIndex = 0;
			comboBoxAlignment.SelectedIndex = 0;
			comboBoxBorder.SelectedIndex = 5;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SampleStatusBarControl));
			this.statusBarControl1 = new Crownwood.DotNetMagic.Controls.StatusBarControl();
			this.numericWidth = new System.Windows.Forms.NumericUpDown();
			this.labelWidth = new System.Windows.Forms.Label();
			this.textBox = new System.Windows.Forms.TextBox();
			this.labelText = new System.Windows.Forms.Label();
			this.labelIcon = new System.Windows.Forms.Label();
			this.comboBoxIcon = new System.Windows.Forms.ComboBox();
			this.labelSize = new System.Windows.Forms.Label();
			this.comboBoxSize = new System.Windows.Forms.ComboBox();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.groupBoxAdd = new System.Windows.Forms.GroupBox();
			this.labelBorder = new System.Windows.Forms.Label();
			this.comboBoxBorder = new System.Windows.Forms.ComboBox();
			this.labelAlignment = new System.Windows.Forms.Label();
			this.comboBoxAlignment = new System.Windows.Forms.ComboBox();
			this.buttonRemoveAll = new System.Windows.Forms.Button();
			this.buttonRemoveFirst = new System.Windows.Forms.Button();
			this.buttonAddTime = new System.Windows.Forms.Button();
			this.timerForPanels = new System.Windows.Forms.Timer(this.components);
			this.buttonAP = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioPlain = new System.Windows.Forms.RadioButton();
			this.radioIDE = new System.Windows.Forms.RadioButton();
			this.radioOffice2003 = new System.Windows.Forms.RadioButton();
			this.radioIDE2005 = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.numericWidth)).BeginInit();
			this.groupBoxAdd.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusBarControl1
			// 
			this.statusBarControl1.Location = new System.Drawing.Point(0, 240);
			this.statusBarControl1.Name = "statusBarControl1";
			this.statusBarControl1.Size = new System.Drawing.Size(512, 22);
			this.statusBarControl1.TabIndex = 0;
			// 
			// numericWidth
			// 
			this.numericWidth.Location = new System.Drawing.Point(16, 56);
			this.numericWidth.Maximum = new System.Decimal(new int[] {
																		 200,
																		 0,
																		 0,
																		 0});
			this.numericWidth.Name = "numericWidth";
			this.numericWidth.Size = new System.Drawing.Size(48, 20);
			this.numericWidth.TabIndex = 2;
			this.numericWidth.Value = new System.Decimal(new int[] {
																	   85,
																	   0,
																	   0,
																	   0});
			// 
			// labelWidth
			// 
			this.labelWidth.Location = new System.Drawing.Point(16, 32);
			this.labelWidth.Name = "labelWidth";
			this.labelWidth.Size = new System.Drawing.Size(40, 16);
			this.labelWidth.TabIndex = 3;
			this.labelWidth.Text = "Width";
			this.labelWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(80, 56);
			this.textBox.Name = "textBox";
			this.textBox.TabIndex = 4;
			this.textBox.Text = "Example";
			// 
			// labelText
			// 
			this.labelText.Location = new System.Drawing.Point(80, 32);
			this.labelText.Name = "labelText";
			this.labelText.Size = new System.Drawing.Size(40, 16);
			this.labelText.TabIndex = 5;
			this.labelText.Text = "Text";
			this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelIcon
			// 
			this.labelIcon.Location = new System.Drawing.Point(16, 88);
			this.labelIcon.Name = "labelIcon";
			this.labelIcon.Size = new System.Drawing.Size(40, 16);
			this.labelIcon.TabIndex = 6;
			this.labelIcon.Text = "Icon";
			this.labelIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxIcon
			// 
			this.comboBoxIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxIcon.Items.AddRange(new object[] {
															  "None",
															  "Example1",
															  "Example2",
															  "Example3"});
			this.comboBoxIcon.Location = new System.Drawing.Point(16, 112);
			this.comboBoxIcon.Name = "comboBoxIcon";
			this.comboBoxIcon.Size = new System.Drawing.Size(104, 21);
			this.comboBoxIcon.TabIndex = 7;
			// 
			// labelSize
			// 
			this.labelSize.Location = new System.Drawing.Point(136, 88);
			this.labelSize.Name = "labelSize";
			this.labelSize.Size = new System.Drawing.Size(56, 16);
			this.labelSize.TabIndex = 8;
			this.labelSize.Text = "AutoSize";
			this.labelSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxSize
			// 
			this.comboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSize.Items.AddRange(new object[] {
															  "None",
															  "Contents",
															  "Spring"});
			this.comboBoxSize.Location = new System.Drawing.Point(136, 112);
			this.comboBoxSize.Name = "comboBoxSize";
			this.comboBoxSize.Size = new System.Drawing.Size(104, 21);
			this.comboBoxSize.TabIndex = 9;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Location = new System.Drawing.Point(400, 24);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(96, 23);
			this.buttonAdd.TabIndex = 10;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// groupBoxAdd
			// 
			this.groupBoxAdd.Controls.Add(this.labelBorder);
			this.groupBoxAdd.Controls.Add(this.comboBoxBorder);
			this.groupBoxAdd.Controls.Add(this.labelAlignment);
			this.groupBoxAdd.Controls.Add(this.comboBoxAlignment);
			this.groupBoxAdd.Controls.Add(this.textBox);
			this.groupBoxAdd.Controls.Add(this.comboBoxSize);
			this.groupBoxAdd.Controls.Add(this.labelWidth);
			this.groupBoxAdd.Controls.Add(this.comboBoxIcon);
			this.groupBoxAdd.Controls.Add(this.labelIcon);
			this.groupBoxAdd.Controls.Add(this.numericWidth);
			this.groupBoxAdd.Controls.Add(this.labelSize);
			this.groupBoxAdd.Controls.Add(this.labelText);
			this.groupBoxAdd.Location = new System.Drawing.Point(128, 16);
			this.groupBoxAdd.Name = "groupBoxAdd";
			this.groupBoxAdd.Size = new System.Drawing.Size(256, 208);
			this.groupBoxAdd.TabIndex = 11;
			this.groupBoxAdd.TabStop = false;
			this.groupBoxAdd.Text = "Add Values";
			// 
			// labelBorder
			// 
			this.labelBorder.Location = new System.Drawing.Point(136, 144);
			this.labelBorder.Name = "labelBorder";
			this.labelBorder.Size = new System.Drawing.Size(104, 16);
			this.labelBorder.TabIndex = 13;
			this.labelBorder.Text = "Border";
			this.labelBorder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxBorder
			// 
			this.comboBoxBorder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxBorder.Items.AddRange(new object[] {
																"None",
																"Sunken",
																"Raised",
																"Dotted",
																"Dashed",
																"Solid"});
			this.comboBoxBorder.Location = new System.Drawing.Point(136, 168);
			this.comboBoxBorder.Name = "comboBoxBorder";
			this.comboBoxBorder.Size = new System.Drawing.Size(104, 21);
			this.comboBoxBorder.TabIndex = 12;
			// 
			// labelAlignment
			// 
			this.labelAlignment.Location = new System.Drawing.Point(16, 144);
			this.labelAlignment.Name = "labelAlignment";
			this.labelAlignment.Size = new System.Drawing.Size(104, 16);
			this.labelAlignment.TabIndex = 11;
			this.labelAlignment.Text = "Alignment";
			this.labelAlignment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboBoxAlignment
			// 
			this.comboBoxAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAlignment.Items.AddRange(new object[] {
																   "Near",
																   "Center",
																   "Far"});
			this.comboBoxAlignment.Location = new System.Drawing.Point(16, 168);
			this.comboBoxAlignment.Name = "comboBoxAlignment";
			this.comboBoxAlignment.Size = new System.Drawing.Size(104, 21);
			this.comboBoxAlignment.TabIndex = 10;
			// 
			// buttonRemoveAll
			// 
			this.buttonRemoveAll.Location = new System.Drawing.Point(400, 200);
			this.buttonRemoveAll.Name = "buttonRemoveAll";
			this.buttonRemoveAll.Size = new System.Drawing.Size(96, 23);
			this.buttonRemoveAll.TabIndex = 12;
			this.buttonRemoveAll.Text = "RemoveAll";
			this.buttonRemoveAll.Click += new System.EventHandler(this.buttonRemoveAll_Click);
			// 
			// buttonRemoveFirst
			// 
			this.buttonRemoveFirst.Location = new System.Drawing.Point(400, 168);
			this.buttonRemoveFirst.Name = "buttonRemoveFirst";
			this.buttonRemoveFirst.Size = new System.Drawing.Size(96, 23);
			this.buttonRemoveFirst.TabIndex = 13;
			this.buttonRemoveFirst.Text = "RemoveFirst";
			this.buttonRemoveFirst.Click += new System.EventHandler(this.buttonRemoveFirst_Click);
			// 
			// buttonAddTime
			// 
			this.buttonAddTime.Location = new System.Drawing.Point(400, 56);
			this.buttonAddTime.Name = "buttonAddTime";
			this.buttonAddTime.Size = new System.Drawing.Size(96, 23);
			this.buttonAddTime.TabIndex = 14;
			this.buttonAddTime.Text = "Add Time";
			this.buttonAddTime.Click += new System.EventHandler(this.buttonAddTime_Click);
			// 
			// timerForPanels
			// 
			this.timerForPanels.Enabled = true;
			this.timerForPanels.Interval = 300;
			this.timerForPanels.Tick += new System.EventHandler(this.OnTick);
			// 
			// buttonAP
			// 
			this.buttonAP.Location = new System.Drawing.Point(400, 88);
			this.buttonAP.Name = "buttonAP";
			this.buttonAP.Size = new System.Drawing.Size(96, 23);
			this.buttonAP.TabIndex = 15;
			this.buttonAP.Text = "Add Progress";
			this.buttonAP.Click += new System.EventHandler(this.buttonAP_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioIDE2005);
			this.groupBox1.Controls.Add(this.radioPlain);
			this.groupBox1.Controls.Add(this.radioIDE);
			this.groupBox1.Controls.Add(this.radioOffice2003);
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(112, 208);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Visual Style";
			// 
			// radioPlain
			// 
			this.radioPlain.Location = new System.Drawing.Point(16, 104);
			this.radioPlain.Name = "radioPlain";
			this.radioPlain.Size = new System.Drawing.Size(88, 24);
			this.radioPlain.TabIndex = 2;
			this.radioPlain.Text = "Plain";
			this.radioPlain.CheckedChanged += new System.EventHandler(this.radioStyle_CheckedChanged);
			// 
			// radioIDE
			// 
			this.radioIDE.Location = new System.Drawing.Point(16, 80);
			this.radioIDE.Name = "radioIDE";
			this.radioIDE.Size = new System.Drawing.Size(88, 24);
			this.radioIDE.TabIndex = 1;
			this.radioIDE.Text = "IDE";
			this.radioIDE.CheckedChanged += new System.EventHandler(this.radioStyle_CheckedChanged);
			// 
			// radioOffice2003
			// 
			this.radioOffice2003.Checked = true;
			this.radioOffice2003.Location = new System.Drawing.Point(16, 32);
			this.radioOffice2003.Name = "radioOffice2003";
			this.radioOffice2003.Size = new System.Drawing.Size(88, 24);
			this.radioOffice2003.TabIndex = 0;
			this.radioOffice2003.TabStop = true;
			this.radioOffice2003.Text = "Office2003";
			this.radioOffice2003.CheckedChanged += new System.EventHandler(this.radioStyle_CheckedChanged);
			// 
			// radioIDE2005
			// 
			this.radioIDE2005.Location = new System.Drawing.Point(16, 56);
			this.radioIDE2005.Name = "radioIDE2005";
			this.radioIDE2005.Size = new System.Drawing.Size(88, 24);
			this.radioIDE2005.TabIndex = 3;
			this.radioIDE2005.Text = "IDE2005";
			this.radioIDE2005.CheckedChanged += new System.EventHandler(this.radioStyle_CheckedChanged);
			// 
			// SampleStatusBarControl
			// 
			this.ClientSize = new System.Drawing.Size(512, 262);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonAP);
			this.Controls.Add(this.buttonAddTime);
			this.Controls.Add(this.buttonRemoveFirst);
			this.Controls.Add(this.buttonRemoveAll);
			this.Controls.Add(this.groupBoxAdd);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.statusBarControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(520, 296);
			this.Name = "SampleStatusBarControl";
			this.Text = "DotNetMagic - SampleStatusBarControl";
			((System.ComponentModel.ISupportInitialize)(this.numericWidth)).EndInit();
			this.groupBoxAdd.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new SampleStatusBarControl());
		}

		private void buttonAdd_Click(object sender, System.EventArgs e)
		{
			Add(0);
		}

		private void buttonAddTime_Click(object sender, System.EventArgs e)
		{
			Add(1);
		}
		
		private void buttonAP_Click(object sender, System.EventArgs e)
		{
			Add(2);
		}
		
		private void Add(int tag)
		{
			StatusPanel panel = new StatusPanel();
			panel.Text = textBox.Text;
			panel.RequestedWidth = (int)numericWidth.Value;
			
			switch(comboBoxSize.SelectedIndex)
			{
				case 0:
					panel.AutoSizing = StatusBarPanelAutoSize.None;
					break;
				case 1:
					panel.AutoSizing = StatusBarPanelAutoSize.Contents;
					break;
				case 2:
					panel.AutoSizing = StatusBarPanelAutoSize.Spring;
					break;
			}
			
			if (comboBoxIcon.SelectedIndex > 0)
				panel.Image = imageList.Images[comboBoxIcon.SelectedIndex];
			
			switch(comboBoxAlignment.SelectedIndex)
			{
				case 0:
					panel.Alignment = StringAlignment.Near;
					break;
				case 1:
					panel.Alignment = StringAlignment.Center;
					break;
				case 2:
					panel.Alignment = StringAlignment.Far;
					break;
			}
			
			switch(comboBoxBorder.SelectedIndex)
			{
				case 0:
					panel.PanelBorder = PanelBorder.None;
					break;
				case 1:
					panel.PanelBorder = PanelBorder.Sunken;
					break;
				case 2:
					panel.PanelBorder = PanelBorder.Raised;
					break;
				case 3:
					panel.PanelBorder = PanelBorder.Dotted;
					break;
				case 4:
					panel.PanelBorder = PanelBorder.Dashed;
					break;
				case 5:
					panel.PanelBorder = PanelBorder.Solid;
					break;
			}
			
			// Progress control overrides some values
			if (tag == 2)
			{
				panel.AutoSizing = StatusBarPanelAutoSize.None;
				
				ProgressBar bar = new ProgressBar();
				bar.Minimum = 0;
				bar.Maximum = 30;
				bar.Dock = DockStyle.Fill;
				panel.Controls.Add(bar);
			}
			
			panel.Tag = tag;
			
			statusBarControl1.StatusPanels.Add(panel);	
		}

		private void buttonRemoveFirst_Click(object sender, System.EventArgs e)
		{
			if (statusBarControl1.StatusPanels.Count > 0)
				statusBarControl1.StatusPanels.RemoveAt(0);	
		}

		private void buttonRemoveAll_Click(object sender, System.EventArgs e)
		{
			statusBarControl1.StatusPanels.Clear();
		}

		private void OnTick(object sender, System.EventArgs e)
		{
			// Scan each of the status panels
			foreach(StatusPanel panel in statusBarControl1.StatusPanels)
			{
				// Is it designated for time information?
				if ((int)panel.Tag == 1)
					panel.Text = DateTime.Now.ToLongTimeString();

				// Is it designated for progrss information?
				if ((int)panel.Tag == 2)
				{
					ProgressBar bar = panel.Controls[0] as ProgressBar;
					bar.Value++;
					if (bar.Value >= bar.Maximum)
						bar.Value = bar.Minimum;
				}
			}
		}

		private void radioStyle_CheckedChanged(object sender, System.EventArgs e)
		{
			VisualStyle style;
			
			if (radioOffice2003.Checked)
				style = VisualStyle.Office2003;
			else if (radioIDE2005.Checked)
				style = VisualStyle.IDE2005;
			else if (radioIDE.Checked)
				style = VisualStyle.IDE;
			else
				style = VisualStyle.Plain;
				
			statusBarControl1.Style = style;
		}
	}
}
