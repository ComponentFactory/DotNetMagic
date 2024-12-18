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
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls;

namespace SampleTreeControl
{
	/// <summary>
	/// Summary description for SampleTreeControl.
	/// </summary>
	public class SampleTreeControl : System.Windows.Forms.Form
	{
		// Instance fields
		private int _count = 1;
		private Random _random;
		private ImageList _imageList;
	
		// Instance fields - Designer generated
		private Crownwood.DotNetMagic.Controls.TreeControl treeControl1;
		private System.Windows.Forms.GroupBox groupBoxStyles;
		private System.Windows.Forms.Button buttonStdTheme;
		private System.Windows.Forms.Button buttonStdPlain;
		private System.Windows.Forms.Button buttonNav;
		private System.Windows.Forms.Button buttonGroups;
		private System.Windows.Forms.Button buttonList;
		private System.Windows.Forms.Button buttonExplorer;
		private System.Windows.Forms.ContextMenu contextNode;
		private System.Windows.Forms.ContextMenu contextClient;
		private System.Windows.Forms.MenuItem menuDeleteNode;
		private System.Windows.Forms.MenuItem menuRenameNode;
		private System.Windows.Forms.MenuItem menuHideNode;
		private System.Windows.Forms.MenuItem menuAddRootNode;
		private System.Windows.Forms.MenuItem menuClearRootNodes;
		private System.Windows.Forms.MenuItem menuExpandAll;
		private System.Windows.Forms.MenuItem menuCollapseAll;
		private System.Windows.Forms.MenuItem menuSep1;
		private System.Windows.Forms.MenuItem menuSep2;
		private System.Windows.Forms.MenuItem menuHideAll;
		private System.Windows.Forms.MenuItem menuShowAll;
		private System.Windows.Forms.MenuItem menuSep3;
		private System.Windows.Forms.MenuItem menuAddChildNode;
		private System.Windows.Forms.MenuItem menuAddSiblingNode;
		private System.Windows.Forms.GroupBox groupBoxCustom;
		private System.Windows.Forms.Button buttonCustom1;
		private System.Windows.Forms.Button buttonCustom2;
		private System.Windows.Forms.Button buttonClearAll;
		private System.Windows.Forms.Button buttonAddTestNodes;
		private System.Windows.Forms.Button buttonAddRandom50;
		private System.Windows.Forms.Button buttonAddRandom500;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.Button buttonExpandAllNodes;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Button buttonCustom3;
		private System.Windows.Forms.Button buttonOffice2003Light;
		private System.Windows.Forms.Button buttonOffice2003Dark;
		private System.ComponentModel.IContainer components;

		public SampleTreeControl()
		{
			// Create the random number generator
			_random = new Random();
		
			// Required for Windows Form Designer support
			InitializeComponent();

			// Load the pictures to use for the root nodes
			_imageList = ResourceHelper.LoadBitmapStrip(typeof(SampleTreeControl), "SampleTreeControl.bitmaps.bmp", new Size(22, 22), Point.Empty);

			// Create initial batch of nodes
			AddTestNodes();

			// Assign the tree to the property grid
			propertyGrid1.SelectedObject = treeControl1;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SampleTreeControl));
			this.treeControl1 = new Crownwood.DotNetMagic.Controls.TreeControl();
			this.contextNode = new System.Windows.Forms.ContextMenu();
			this.menuAddChildNode = new System.Windows.Forms.MenuItem();
			this.menuAddSiblingNode = new System.Windows.Forms.MenuItem();
			this.menuSep3 = new System.Windows.Forms.MenuItem();
			this.menuDeleteNode = new System.Windows.Forms.MenuItem();
			this.menuRenameNode = new System.Windows.Forms.MenuItem();
			this.menuHideNode = new System.Windows.Forms.MenuItem();
			this.contextClient = new System.Windows.Forms.ContextMenu();
			this.menuAddRootNode = new System.Windows.Forms.MenuItem();
			this.menuClearRootNodes = new System.Windows.Forms.MenuItem();
			this.menuSep1 = new System.Windows.Forms.MenuItem();
			this.menuExpandAll = new System.Windows.Forms.MenuItem();
			this.menuCollapseAll = new System.Windows.Forms.MenuItem();
			this.menuSep2 = new System.Windows.Forms.MenuItem();
			this.menuHideAll = new System.Windows.Forms.MenuItem();
			this.menuShowAll = new System.Windows.Forms.MenuItem();
			this.buttonStdTheme = new System.Windows.Forms.Button();
			this.groupBoxStyles = new System.Windows.Forms.GroupBox();
			this.buttonOffice2003Dark = new System.Windows.Forms.Button();
			this.buttonOffice2003Light = new System.Windows.Forms.Button();
			this.buttonExplorer = new System.Windows.Forms.Button();
			this.buttonList = new System.Windows.Forms.Button();
			this.buttonGroups = new System.Windows.Forms.Button();
			this.buttonNav = new System.Windows.Forms.Button();
			this.buttonStdPlain = new System.Windows.Forms.Button();
			this.groupBoxCustom = new System.Windows.Forms.GroupBox();
			this.buttonCustom3 = new System.Windows.Forms.Button();
			this.buttonCustom2 = new System.Windows.Forms.Button();
			this.buttonCustom1 = new System.Windows.Forms.Button();
			this.buttonClearAll = new System.Windows.Forms.Button();
			this.buttonAddTestNodes = new System.Windows.Forms.Button();
			this.buttonAddRandom50 = new System.Windows.Forms.Button();
			this.buttonAddRandom500 = new System.Windows.Forms.Button();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.buttonExpandAllNodes = new System.Windows.Forms.Button();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.groupBoxStyles.SuspendLayout();
			this.groupBoxCustom.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeControl1
			// 
			this.treeControl1.AllowDrop = true;
			this.treeControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.treeControl1.ContextMenuNode = this.contextNode;
			this.treeControl1.ContextMenuSpace = this.contextClient;
			this.treeControl1.FocusNode = null;
			this.treeControl1.HotBackColor = System.Drawing.Color.Empty;
			this.treeControl1.HotForeColor = System.Drawing.Color.Empty;
			this.treeControl1.Location = new System.Drawing.Point(16, 16);
			this.treeControl1.Name = "treeControl1";
			this.treeControl1.SelectedNode = null;
			this.treeControl1.SelectedNoFocusBackColor = System.Drawing.SystemColors.Control;
			this.treeControl1.Size = new System.Drawing.Size(242, 512);
			this.treeControl1.TabIndex = 0;
			// 
			// contextNode
			// 
			this.contextNode.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.menuAddChildNode,
																						this.menuAddSiblingNode,
																						this.menuSep3,
																						this.menuDeleteNode,
																						this.menuRenameNode,
																						this.menuHideNode});
			// 
			// menuAddChildNode
			// 
			this.menuAddChildNode.Index = 0;
			this.menuAddChildNode.Text = "Add Child";
			this.menuAddChildNode.Click += new System.EventHandler(this.menuAddChildNodeClick);
			// 
			// menuAddSiblingNode
			// 
			this.menuAddSiblingNode.Index = 1;
			this.menuAddSiblingNode.Text = "Add Sibling";
			this.menuAddSiblingNode.Click += new System.EventHandler(this.menuAddSiblingNodeClick);
			// 
			// menuSep3
			// 
			this.menuSep3.Index = 2;
			this.menuSep3.Text = "-";
			// 
			// menuDeleteNode
			// 
			this.menuDeleteNode.Index = 3;
			this.menuDeleteNode.Text = "Delete";
			this.menuDeleteNode.Click += new System.EventHandler(this.menuDeleteNodeClick);
			// 
			// menuRenameNode
			// 
			this.menuRenameNode.Index = 4;
			this.menuRenameNode.Text = "Rename";
			this.menuRenameNode.Click += new System.EventHandler(this.menuRenameNodeClick);
			// 
			// menuHideNode
			// 
			this.menuHideNode.Index = 5;
			this.menuHideNode.Text = "Hide";
			this.menuHideNode.Click += new System.EventHandler(this.menuHideNodeClick);
			// 
			// contextClient
			// 
			this.contextClient.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.menuAddRootNode,
																						  this.menuClearRootNodes,
																						  this.menuSep1,
																						  this.menuExpandAll,
																						  this.menuCollapseAll,
																						  this.menuSep2,
																						  this.menuHideAll,
																						  this.menuShowAll});
			// 
			// menuAddRootNode
			// 
			this.menuAddRootNode.Index = 0;
			this.menuAddRootNode.Text = "Add Root Node";
			this.menuAddRootNode.Click += new System.EventHandler(this.menuAddRootNodeClick);
			// 
			// menuClearRootNodes
			// 
			this.menuClearRootNodes.Index = 1;
			this.menuClearRootNodes.Text = "Clear Root Nodes";
			this.menuClearRootNodes.Click += new System.EventHandler(this.menuClearRootNodesClick);
			// 
			// menuSep1
			// 
			this.menuSep1.Index = 2;
			this.menuSep1.Text = "-";
			// 
			// menuExpandAll
			// 
			this.menuExpandAll.Index = 3;
			this.menuExpandAll.Text = "Expand All";
			this.menuExpandAll.Click += new System.EventHandler(this.menuExpandAllClick);
			// 
			// menuCollapseAll
			// 
			this.menuCollapseAll.Index = 4;
			this.menuCollapseAll.Text = "Collapse All";
			this.menuCollapseAll.Click += new System.EventHandler(this.menuCollapseAllClick);
			// 
			// menuSep2
			// 
			this.menuSep2.Index = 5;
			this.menuSep2.Text = "-";
			// 
			// menuHideAll
			// 
			this.menuHideAll.Index = 6;
			this.menuHideAll.Text = "Hide All";
			this.menuHideAll.Click += new System.EventHandler(this.menuHideAllClick);
			// 
			// menuShowAll
			// 
			this.menuShowAll.Index = 7;
			this.menuShowAll.Text = "Show All";
			this.menuShowAll.Click += new System.EventHandler(this.menuShowAllClick);
			// 
			// buttonStdTheme
			// 
			this.buttonStdTheme.Location = new System.Drawing.Point(16, 24);
			this.buttonStdTheme.Name = "buttonStdTheme";
			this.buttonStdTheme.Size = new System.Drawing.Size(120, 23);
			this.buttonStdTheme.TabIndex = 1;
			this.buttonStdTheme.Text = "Standard - Themed";
			this.buttonStdTheme.Click += new System.EventHandler(this.buttonStdTheme_Click);
			// 
			// groupBoxStyles
			// 
			this.groupBoxStyles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxStyles.Controls.Add(this.buttonOffice2003Dark);
			this.groupBoxStyles.Controls.Add(this.buttonOffice2003Light);
			this.groupBoxStyles.Controls.Add(this.buttonExplorer);
			this.groupBoxStyles.Controls.Add(this.buttonList);
			this.groupBoxStyles.Controls.Add(this.buttonGroups);
			this.groupBoxStyles.Controls.Add(this.buttonNav);
			this.groupBoxStyles.Controls.Add(this.buttonStdPlain);
			this.groupBoxStyles.Controls.Add(this.buttonStdTheme);
			this.groupBoxStyles.Location = new System.Drawing.Point(274, 16);
			this.groupBoxStyles.Name = "groupBoxStyles";
			this.groupBoxStyles.Size = new System.Drawing.Size(288, 152);
			this.groupBoxStyles.TabIndex = 2;
			this.groupBoxStyles.TabStop = false;
			this.groupBoxStyles.Text = "Predefined Styles";
			// 
			// buttonOffice2003Dark
			// 
			this.buttonOffice2003Dark.Location = new System.Drawing.Point(152, 88);
			this.buttonOffice2003Dark.Name = "buttonOffice2003Dark";
			this.buttonOffice2003Dark.Size = new System.Drawing.Size(120, 23);
			this.buttonOffice2003Dark.TabIndex = 8;
			this.buttonOffice2003Dark.Text = "Office2003 Dark";
			this.buttonOffice2003Dark.Click += new System.EventHandler(this.buttonOffice2003Dark_Click);
			// 
			// buttonOffice2003Light
			// 
			this.buttonOffice2003Light.Location = new System.Drawing.Point(152, 56);
			this.buttonOffice2003Light.Name = "buttonOffice2003Light";
			this.buttonOffice2003Light.Size = new System.Drawing.Size(120, 23);
			this.buttonOffice2003Light.TabIndex = 7;
			this.buttonOffice2003Light.Text = "Office2003 Light";
			this.buttonOffice2003Light.Click += new System.EventHandler(this.buttonOffice2003Light_Click);
			// 
			// buttonExplorer
			// 
			this.buttonExplorer.Location = new System.Drawing.Point(16, 88);
			this.buttonExplorer.Name = "buttonExplorer";
			this.buttonExplorer.Size = new System.Drawing.Size(120, 23);
			this.buttonExplorer.TabIndex = 6;
			this.buttonExplorer.Text = "Explorer";
			this.buttonExplorer.Click += new System.EventHandler(this.buttonStdExplor_Click);
			// 
			// buttonList
			// 
			this.buttonList.Location = new System.Drawing.Point(152, 120);
			this.buttonList.Name = "buttonList";
			this.buttonList.Size = new System.Drawing.Size(120, 23);
			this.buttonList.TabIndex = 5;
			this.buttonList.Text = "List";
			this.buttonList.Click += new System.EventHandler(this.buttonList_Click);
			// 
			// buttonGroups
			// 
			this.buttonGroups.Location = new System.Drawing.Point(152, 24);
			this.buttonGroups.Name = "buttonGroups";
			this.buttonGroups.Size = new System.Drawing.Size(120, 23);
			this.buttonGroups.TabIndex = 4;
			this.buttonGroups.Text = "Groups";
			this.buttonGroups.Click += new System.EventHandler(this.buttonGroups_Click);
			// 
			// buttonNav
			// 
			this.buttonNav.Location = new System.Drawing.Point(16, 120);
			this.buttonNav.Name = "buttonNav";
			this.buttonNav.Size = new System.Drawing.Size(120, 23);
			this.buttonNav.TabIndex = 3;
			this.buttonNav.Text = "Navigator";
			this.buttonNav.Click += new System.EventHandler(this.buttonNav_Click);
			// 
			// buttonStdPlain
			// 
			this.buttonStdPlain.Location = new System.Drawing.Point(16, 56);
			this.buttonStdPlain.Name = "buttonStdPlain";
			this.buttonStdPlain.Size = new System.Drawing.Size(120, 23);
			this.buttonStdPlain.TabIndex = 2;
			this.buttonStdPlain.Text = "Standard - Plain";
			this.buttonStdPlain.Click += new System.EventHandler(this.buttonStdPlain_Click);
			// 
			// groupBoxCustom
			// 
			this.groupBoxCustom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxCustom.Controls.Add(this.buttonCustom3);
			this.groupBoxCustom.Controls.Add(this.buttonCustom2);
			this.groupBoxCustom.Controls.Add(this.buttonCustom1);
			this.groupBoxCustom.Location = new System.Drawing.Point(570, 16);
			this.groupBoxCustom.Name = "groupBoxCustom";
			this.groupBoxCustom.Size = new System.Drawing.Size(152, 152);
			this.groupBoxCustom.TabIndex = 14;
			this.groupBoxCustom.TabStop = false;
			this.groupBoxCustom.Text = "Custom Styles";
			// 
			// buttonCustom3
			// 
			this.buttonCustom3.Location = new System.Drawing.Point(16, 88);
			this.buttonCustom3.Name = "buttonCustom3";
			this.buttonCustom3.Size = new System.Drawing.Size(120, 23);
			this.buttonCustom3.TabIndex = 5;
			this.buttonCustom3.Text = "Custom - Draw";
			this.buttonCustom3.Click += new System.EventHandler(this.buttonCustom3_Click);
			// 
			// buttonCustom2
			// 
			this.buttonCustom2.Location = new System.Drawing.Point(16, 56);
			this.buttonCustom2.Name = "buttonCustom2";
			this.buttonCustom2.Size = new System.Drawing.Size(120, 23);
			this.buttonCustom2.TabIndex = 4;
			this.buttonCustom2.Text = "Custom - Navigator";
			this.buttonCustom2.Click += new System.EventHandler(this.buttonCustom2_Click);
			// 
			// buttonCustom1
			// 
			this.buttonCustom1.Location = new System.Drawing.Point(16, 24);
			this.buttonCustom1.Name = "buttonCustom1";
			this.buttonCustom1.Size = new System.Drawing.Size(120, 23);
			this.buttonCustom1.TabIndex = 3;
			this.buttonCustom1.Text = "Custom - Red";
			this.buttonCustom1.Click += new System.EventHandler(this.buttonCustom1Click);
			// 
			// buttonClearAll
			// 
			this.buttonClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClearAll.Location = new System.Drawing.Point(16, 544);
			this.buttonClearAll.Name = "buttonClearAll";
			this.buttonClearAll.Size = new System.Drawing.Size(112, 23);
			this.buttonClearAll.TabIndex = 19;
			this.buttonClearAll.Text = "Clear All Nodes";
			this.buttonClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
			// 
			// buttonAddTestNodes
			// 
			this.buttonAddTestNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddTestNodes.Location = new System.Drawing.Point(144, 544);
			this.buttonAddTestNodes.Name = "buttonAddTestNodes";
			this.buttonAddTestNodes.Size = new System.Drawing.Size(112, 23);
			this.buttonAddTestNodes.TabIndex = 20;
			this.buttonAddTestNodes.Text = "Add Test Nodes";
			this.buttonAddTestNodes.Click += new System.EventHandler(this.buttonAddTestNodes_Click);
			// 
			// buttonAddRandom50
			// 
			this.buttonAddRandom50.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddRandom50.Location = new System.Drawing.Point(272, 544);
			this.buttonAddRandom50.Name = "buttonAddRandom50";
			this.buttonAddRandom50.Size = new System.Drawing.Size(112, 23);
			this.buttonAddRandom50.TabIndex = 21;
			this.buttonAddRandom50.Text = "Add Random x 50";
			this.buttonAddRandom50.Click += new System.EventHandler(this.buttonAddRandom50_Click);
			// 
			// buttonAddRandom500
			// 
			this.buttonAddRandom500.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonAddRandom500.Location = new System.Drawing.Point(400, 544);
			this.buttonAddRandom500.Name = "buttonAddRandom500";
			this.buttonAddRandom500.Size = new System.Drawing.Size(112, 23);
			this.buttonAddRandom500.TabIndex = 22;
			this.buttonAddRandom500.Text = "Add Random x 500";
			this.buttonAddRandom500.Click += new System.EventHandler(this.buttonAddRandom500_Click);
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.HelpVisible = false;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(274, 176);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(448, 352);
			this.propertyGrid1.TabIndex = 29;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ToolbarVisible = false;
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// buttonExpandAllNodes
			// 
			this.buttonExpandAllNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonExpandAllNodes.Location = new System.Drawing.Point(528, 544);
			this.buttonExpandAllNodes.Name = "buttonExpandAllNodes";
			this.buttonExpandAllNodes.Size = new System.Drawing.Size(112, 23);
			this.buttonExpandAllNodes.TabIndex = 30;
			this.buttonExpandAllNodes.Text = "Expand All Nodes";
			this.buttonExpandAllNodes.Click += new System.EventHandler(this.buttonExpandAllNodes_Click);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList.ImageSize = new System.Drawing.Size(22, 22);
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// SampleTreeControl
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 580);
			this.Controls.Add(this.buttonExpandAllNodes);
			this.Controls.Add(this.propertyGrid1);
			this.Controls.Add(this.groupBoxCustom);
			this.Controls.Add(this.treeControl1);
			this.Controls.Add(this.groupBoxStyles);
			this.Controls.Add(this.buttonClearAll);
			this.Controls.Add(this.buttonAddTestNodes);
			this.Controls.Add(this.buttonAddRandom50);
			this.Controls.Add(this.buttonAddRandom500);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(664, 328);
			this.Name = "SampleTreeControl";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "DotNetMagic - SampleTreeControl";
			this.groupBoxStyles.ResumeLayout(false);
			this.groupBoxCustom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new SampleTreeControl());
		}

		private void AddTestNodes()
		{
			Node t1 = new Node("Font");
			Node t2 = new Node("Color");
			Node t3 = new Node("Checkbox");
			Node t4 = new Node("Indicator");
			treeControl1.Nodes.AddRange(new Node[]{t1,t2,t3,t4});
			t1.Image = _imageList.Images[0];
			t2.Image = _imageList.Images[1];
			t3.Image = _imageList.Images[2];
			t4.Image = _imageList.Images[3];
			t1.Tooltip = "A selection of fonts";
			t2.Tooltip = "Examples of different colors";
			t3.Tooltip = "Individual definition of checkboxes";
			t4.Tooltip = "A range of different indicators";

			// Fonts
			Node f1a = new Node("Arial, 9pt");
			Node f1b = new Node("Arial, 9pt, Bold");
			Node f1c = new Node("Arial, 9pt, Italic");
			Node f2a = new Node("Sans Serif, 12pt");
			Node f2b = new Node("Sans Serif, 12pt, Bold");
			Node f2c = new Node("Sans Serif, 12pt, Italic");
			f1a.NodeFont = new Font("Arial", 9f);
			f1b.NodeFont = new Font("Arial", 9f, FontStyle.Bold);
			f1c.NodeFont = new Font("Arial", 9f, FontStyle.Italic);
			f2a.NodeFont = new Font("Sans Serif", 12f);
			f2b.NodeFont = new Font("Sans Serif", 12f, FontStyle.Bold);
			f2c.NodeFont = new Font("Sans Serif", 12f, FontStyle.Italic);
			t1.Nodes.AddRange(new Node[]{f1a,f1b,f1c,f2a,f2b,f2c});

			// Colors
			Node c1 = new Node("Red");
			Node c2 = new Node("Blue");
			Node c3 = new Node("Green");
			Node c4 = new Node("Red & Yellow");
			Node c5 = new Node("Two Blues");
			Node c6 = new Node("Two Greens");
			t2.Nodes.AddRange(new Node[]{c1,c2,c3,c4,c5,c6});
			c1.ForeColor = Color.Red;
			c2.ForeColor = Color.Blue;
			c3.ForeColor = Color.Green;
			c4.ForeColor = Color.Yellow;
			c4.BackColor = Color.Red;
			c5.ForeColor = Color.DarkBlue;
			c5.BackColor = Color.LightBlue;
			c6.ForeColor = Color.DarkGreen;
			c6.BackColor = Color.LightGreen;

			// Checkboxes
			Node b1 = new Node("None");
			Node b2 = new Node("3 State");
			Node b3 = new Node("2 State");
			Node b4 = new Node("Radio 1");
			Node b5 = new Node("Radio 2");
			Node b6 = new Node("Radio 3");
			t3.Nodes.AddRange(new Node[]{b1,b2,b3,b4,b5,b6});
			b2.CheckStates = NodeCheckStates.ThreeStateCheck;
			b3.CheckStates = NodeCheckStates.TwoStateCheck;
			b4.CheckStates = NodeCheckStates.Radio;
			b5.CheckStates = NodeCheckStates.Radio;
			b6.CheckStates = NodeCheckStates.Radio;

			// Indicators
			Node i1 = new Node("Flag");
			Node i1a = new Node("Blue Flag");
			Node i1b = new Node("Green Flag");
			Node i1c = new Node("Orange Flag");
			Node i1d = new Node("Red Flag");
			Node i2 = new Node("Box");
			Node i2a = new Node("Blue Box");
			Node i2b = new Node("Green Box");
			Node i2c = new Node("Orange Box");
			Node i2d = new Node("Red Box");
			Node i3 = new Node("Arrow");
			Node i3a = new Node("Blue Arrow");
			Node i3b = new Node("Green Arrow");
			Node i3c = new Node("Orange Arrow");
			Node i3d = new Node("Red Arrow");
			Node i4 = new Node("Others");
			Node i4a = new Node("Error");
			Node i4b = new Node("Exclamation");
			Node i4c = new Node("Paperclip");
			Node i4d = new Node("Graph");
			Node i4e = new Node("Lock");
			Node i5 = new Node("Tick");
			Node i5a = new Node("Black Tick");
			Node i5b = new Node("Blue Tick");
			Node i5c = new Node("Brown Tick");
			Node i5d = new Node("Green Tick");
			Node i5e = new Node("Red Tick");
			Node i6 = new Node("Question");
			Node i6a = new Node("Black Question");
			Node i6b = new Node("Blue Question");
			Node i6c = new Node("Brown Question");
			Node i6d = new Node("Green Question");
			Node i6e = new Node("Red Question");
			i1.Indicator = Indicator.FlagGray;
			i1a.Indicator = Indicator.FlagBlue;
			i1b.Indicator = Indicator.FlagGreen;
			i1c.Indicator = Indicator.FlagOrange;
			i1d.Indicator = Indicator.FlagRed;
			i1.Nodes.AddRange(new Node[]{i1a,i1b,i1c,i1d});
			i2.Indicator = Indicator.BoxGray;
			i2a.Indicator = Indicator.BoxBlue;
			i2b.Indicator = Indicator.BoxGreen;
			i2c.Indicator = Indicator.BoxOrange;
			i2d.Indicator = Indicator.BoxRed;
			i2.Nodes.AddRange(new Node[]{i2a,i2b,i2c,i2d});
			i3.Indicator = Indicator.ArrowGray;
			i3a.Indicator = Indicator.ArrowBlue;
			i3b.Indicator = Indicator.ArrowGreen;
			i3c.Indicator = Indicator.ArrowOrange;
			i3d.Indicator = Indicator.ArrowRed;
			i3.Nodes.AddRange(new Node[]{i3a,i3b,i3c,i3d});
			i4.Indicator = Indicator.Lightning;
			i4a.Indicator = Indicator.Error;
			i4b.Indicator = Indicator.Exclamation;
			i4c.Indicator = Indicator.Paperclip;
			i4d.Indicator = Indicator.Graph;
			i4e.Indicator = Indicator.Lock;
			i4.Nodes.AddRange(new Node[]{i4a,i4b,i4c,i4d,i4e});
			i5a.Indicator = Indicator.TickBlack;
			i5b.Indicator = Indicator.TickBlue;
			i5c.Indicator = Indicator.TickBrown;
			i5d.Indicator = Indicator.TickGreen;
			i5e.Indicator = Indicator.TickRed;
			i5.Nodes.AddRange(new Node[]{i5a,i5b,i5c,i5d,i5e});
			i6a.Indicator = Indicator.QuestionBlack;
			i6b.Indicator = Indicator.QuestionBlue;
			i6c.Indicator = Indicator.QuestionBrown;
			i6d.Indicator = Indicator.QuestionGreen;
			i6e.Indicator = Indicator.QuestionRed;
			i6.Nodes.AddRange(new Node[]{i6a,i6b,i6c,i6d,i6e});
			t4.Nodes.AddRange(new Node[]{i1,i2,i3,i5,i6,i4});
			i1.Tooltip = "Different color flags";
			i2.Tooltip = "Different color boxes";
		}

		private void buttonStdTheme_Click(object sender, System.EventArgs e)
		{
			treeControl1.SetTreeControlStyle(TreeControlStyles.StandardThemed);
		}

		private void buttonStdPlain_Click(object sender, System.EventArgs e)
		{
			treeControl1.SetTreeControlStyle(TreeControlStyles.StandardPlain);
		}

		private void buttonStdExplor_Click(object sender, System.EventArgs e)
		{
			treeControl1.SetTreeControlStyle(TreeControlStyles.Explorer);
		}

		private void buttonNav_Click(object sender, System.EventArgs e)
		{
			treeControl1.SetTreeControlStyle(TreeControlStyles.Navigator);
			treeControl1.GroupImageBoxWidth = 28;
		}

		private void buttonGroups_Click(object sender, System.EventArgs e)
		{
			treeControl1.SetTreeControlStyle(TreeControlStyles.Group);
		}

		private void buttonOffice2003Light_Click(object sender, System.EventArgs e)
		{
			treeControl1.SetTreeControlStyle(TreeControlStyles.GroupOfficeLight);
		}

		private void buttonOffice2003Dark_Click(object sender, System.EventArgs e)
		{
			treeControl1.SetTreeControlStyle(TreeControlStyles.GroupOfficeDark);
		}

		private void buttonList_Click(object sender, System.EventArgs e)
		{
			treeControl1.SetTreeControlStyle(TreeControlStyles.List);
		}

		private void menuAddRootNodeClick(object sender, System.EventArgs e)
		{
			treeControl1.Nodes.Add(new Node("New Node " + _count++));
		}

		private void menuClearRootNodesClick(object sender, System.EventArgs e)
		{
			treeControl1.Nodes.Clear();
		}

		private void menuExpandAllClick(object sender, System.EventArgs e)
		{
			treeControl1.ExpandAll();
		}

		private void menuCollapseAllClick(object sender, System.EventArgs e)
		{
			treeControl1.CollapseAll();
		}

		private void menuShowAllClick(object sender, System.EventArgs e)
		{
			foreach(Node n in treeControl1.Nodes)
				ShowAll(n);
		}
		
		private void menuHideAllClick(object sender, System.EventArgs e)
		{
			foreach(Node n in treeControl1.Nodes)
				HideAll(n);
		}

		private void ShowAll(Node n)
		{
			n.Show();
			
			foreach(Node child in n.Nodes)
				ShowAll(child);
		}

		private void HideAll(Node n)
		{
			n.Hide();
			
			foreach(Node child in n.Nodes)
				HideAll(child);
		}

		private void menuDeleteNodeClick(object sender, System.EventArgs e)
		{
			if (treeControl1.SelectedNode != null)
				treeControl1.SelectedNode.ParentNodes.Remove(treeControl1.SelectedNode);		
		}

		private void menuRenameNodeClick(object sender, System.EventArgs e)
		{
			if (treeControl1.SelectedNode != null)
				treeControl1.SelectedNode.BeginEdit();		
		}

		private void menuHideNodeClick(object sender, System.EventArgs e)
		{
			if (treeControl1.SelectedNode != null)
				treeControl1.SelectedNode.Hide();
		}

		private void menuAddChildNodeClick(object sender, System.EventArgs e)
		{
			if (treeControl1.SelectedNode != null)
				treeControl1.SelectedNode.Nodes.Add(new Node("New Node " + _count++));
		}

		private void menuAddSiblingNodeClick(object sender, System.EventArgs e)
		{
			if (treeControl1.SelectedNode != null)
				treeControl1.SelectedNode.ParentNodes.Insert(treeControl1.SelectedNode.ParentNodes.IndexOf(treeControl1.SelectedNode) + 1, new Node("New Node " + _count++));
		}

		private void buttonCustom1Click(object sender, System.EventArgs e)
		{
			treeControl1.Nodes.Clear();
			AddTestNodes();
			treeControl1.Nodes[2].Expand();
			
			// Start with the plain settings
			treeControl1.SetTreeControlStyle(TreeControlStyles.StandardPlain);
			
			treeControl1.BoxBorderColor = Color.Black;
			treeControl1.BoxInsideColor = Color.Red;
			treeControl1.BoxSignColor = Color.White;
			treeControl1.BoxDrawStyle = DrawStyle.Gradient;
			treeControl1.LineColor = Color.DarkRed;
			treeControl1.LineDashStyle = LineDashStyle.Solid;
			treeControl1.BorderColor = Color.DarkRed;
			treeControl1.CheckBorderColor = Color.Black;
			treeControl1.CheckInsideColor = Color.Red;
			treeControl1.CheckInsideHotColor = Color.White;
			treeControl1.CheckDrawStyle = DrawStyle.Gradient;
			treeControl1.CheckTickColor = Color.White;
			treeControl1.CheckTickHotColor = Color.Red;
			treeControl1.CheckMixedColor = Color.White;
			treeControl1.CheckMixedHotColor = Color.Red;
			treeControl1.CheckLength = 17;
			treeControl1.ColumnWidth = 23;
			treeControl1.SelectedBackColor = Color.DarkRed;
			treeControl1.HotBackColor = Color.Wheat;
			treeControl1.ExtendToRight = true;
			treeControl1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold);
			treeControl1.GroupFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
		}

		private void buttonCustom2_Click(object sender, System.EventArgs e)
		{
			treeControl1.Nodes.Clear();
			AddTestNodes();
			treeControl1.Nodes[2].Expand();

			// Start with the navigator settings
			treeControl1.SetTreeControlStyle(TreeControlStyles.Navigator);

			treeControl1.GroupImageBoxWidth = 4;
			treeControl1.GroupImageBoxBackColor = Color.Black;
			treeControl1.GroupImageBoxSelectedBackColor = Color.Black;
			treeControl1.GroupImageBoxColumn = false;
			treeControl1.GroupBackColor = Color.LightGray;
			treeControl1.GroupForeColor = Color.Black;
			treeControl1.GroupSelectedBackColor = Color.DarkSlateGray;
			treeControl1.GroupSelectedNoFocusBackColor = Color.DarkSlateGray;
			treeControl1.GroupSelectedForeColor = Color.White;
			treeControl1.GroupHotBackColor = Color.Gray;
			treeControl1.GroupHotForeColor = Color.Black;
			treeControl1.BorderStyle = TreeBorderStyle.Solid;
			treeControl1.BoxInsideColor = Color.Gray;
			treeControl1.BoxDrawStyle = DrawStyle.Gradient;
			treeControl1.CheckDrawStyle = DrawStyle.Gradient;
			treeControl1.CheckInsideColor = Color.LightGray;
			treeControl1.CheckInsideHotColor = Color.Gray;
			treeControl1.SelectedBackColor = Color.DarkSlateGray;
			treeControl1.SelectedForeColor = Color.White;
			treeControl1.HotBackColor = Color.LightGray;
			treeControl1.HotForeColor = Color.Black;
			treeControl1.Font = new Font("Arial", 9f);
			treeControl1.GroupFont = new Font("Arial", 12f, FontStyle.Bold);
			
			// Remove images from all the root nodes
			foreach(Node n in treeControl1.Nodes)
				n.Image = null;
		}


		private void buttonCustom3_Click(object sender, System.EventArgs e)
		{
			treeControl1.Nodes.Clear();
			AddTestNodes();
			treeControl1.Nodes[3].Expand();
			treeControl1.Nodes[3].Nodes[2].Expand();
			treeControl1.Nodes[3].Nodes[5].Expand();

			// Start with the navigator settings
			treeControl1.SetTreeControlStyle(TreeControlStyles.StandardPlain);
			
			// Create the custom VC's
			CustomNodeVC customNodeVC = new CustomNodeVC();
			CustomCollectionVC customCollectionVC = new CustomCollectionVC();
		
			// Use custom node only for a few nodes
			treeControl1.Nodes[3].Nodes[0].VC = customNodeVC;
			treeControl1.Nodes[3].Nodes[1].VC = customNodeVC;
				
			// Use custom collection for all collection handling
			treeControl1.CollectionVC = customCollectionVC;
		}

		private void buttonClearAll_Click(object sender, System.EventArgs e)
		{
			treeControl1.Nodes.Clear();
		}

		private void buttonAddTestNodes_Click(object sender, System.EventArgs e)
		{
			AddTestNodes();
		}

		private void buttonAddRandom50_Click(object sender, System.EventArgs e)
		{
			for(int i=0; i<50; i++)
				AddRandomNode(treeControl1.Nodes);
				
			treeControl1.ExpandAll();
		}

		private void buttonAddRandom500_Click(object sender, System.EventArgs e)
		{
			for(int i=0; i<500; i++)
				AddRandomNode(treeControl1.Nodes);

			treeControl1.ExpandAll();
		}
		
		private void AddRandomNode(NodeCollection nodes)
		{
			// If there are no nodes
			if (nodes.Count == 0)
			{
				// Then must just add a node
				nodes.Add(new Node("Random " + _random.Next(99999)));
			}

			// 50/50 chance of inserting over doing down a level
			if (_random.Next(2) == 1)
			{
				// Insert at a random position in the collection of nodes
				nodes.Insert(_random.Next(nodes.Count), new Node("Random " + _random.Next(99999)));
			}
			else
			{
				int index = _random.Next(nodes.Count);
				
				// Otherwise, traverse down a level
				AddRandomNode(nodes[index].Nodes);
				
				// Remember to expand to show change
				nodes[index].Expand();
			}
		}

		private void buttonExpandAllNodes_Click(object sender, System.EventArgs e)
		{
			treeControl1.ExpandAll();
		}
	}
	
	public class CustomNodeVC : DefaultNodeVC
	{
		public override Size MeasureSize(TreeControl tc, Node n, Graphics g)
		{
			bool sizeDirty = n.IsSizeDirty;
		
			// Let base class calculate basic sizing values
			Size newSize = base.MeasureSize (tc, n, g);

			// Do we need recalculate the node size?
			if (sizeDirty)
			{
				// We add a border all around
				newSize.Width += 6;
				newSize.Height += 6;

				// Create font we will use to draw extra text
				Font extraFont = new Font("Arial", 8f, FontStyle.Bold);			

				// Measure the extra text we want to draw
				SizeF extraText = g.MeasureString("Custom Node", extraFont);

				// Add on the space for the extra text plus a spacing gap
				newSize.Width += (int)(extraText.Width + 1) + 5;
			}
			
			return newSize;
		}
		
		public override void DrawText(TreeControl tc, Node n, Graphics g, Rectangle rect, int hotLeft)
		{
			// Create rectangle indented by a pixel inside the node size
			Rectangle wholeBounds = new Rectangle(rect.X + 1, 
													rect.Y + 1, 
													rect.Width - 3, 
													rect.Height - 3);

			// Use Gradient brush to fill node area
			using(Brush drawBrush = new LinearGradientBrush(wholeBounds, 
															Color.Red, 
															Color.White, 
															0f))
			{
				g.FillRectangle(drawBrush, wholeBounds);
			}
				
			// Draw a pixel width border around node area
			g.DrawRectangle(Pens.Gray, wholeBounds);

			// Create fixed font for extra text
			Font extraFont = new Font("Arial", 8f, FontStyle.Bold);			

			// Measure the width of the fixed text
			SizeF extraText = g.MeasureString("Custom Node", extraFont);
			int extraWidth = (int)(extraText.Width + 1) + 5;

			// Calculate rectangle for drawing string into
			Rectangle stringRect = new Rectangle(wholeBounds.Right - extraWidth, 
												 wholeBounds.Top, 
												 extraWidth, 
												 wholeBounds.Height);

			// Create a StringFormat so text is centered vertically
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			// Draw the custom string at right hand side of node			
			g.DrawString("Custom Node", extraFont, Brushes.Black, stringRect, format);

			// Reduce available width to remove the extra text
			rect.Width -= (int)(extraText.Width + 1) + 5;

			// Reduce left over size
			rect.Width -= 6;
			rect.Height -= 6;
			rect.X += 3;
			rect.Y += 3;

			// Let base class finish the drawing
			base.DrawText(tc, n, g, rect, hotLeft);
		}
	}
	
	public class CustomCollectionVC : DefaultCollectionVC
	{
		public override Edges MeasureEdges(TreeControl tc, NodeCollection nc, Graphics g)
		{
			// Get the requirement from the base class first
			Edges edges = base.MeasureEdges (tc, nc, g);
			
			// We only modify nested collections
			if (nc != tc.Nodes)
			{
				// We add on our space for all edges to create a border
				edges.Left += 10;
				edges.Right += 10;
				edges.Top += 10;
				edges.Bottom += 10;

				// Create font we will use for custom painting
				Font drawFont = new Font("Arial", 8f, FontStyle.Bold);				
				
				// Add space to draw this text
				edges.Top += (int)(drawFont.Height * 1.25);
			}
						
			return edges;
		}
		
		public override void AdjustBeforeDrawing(TreeControl tc, NodeCollection nc, ref Rectangle ncBounds)
		{
			// We only modify nested collections
			if (nc != tc.Nodes)
			{
				// Shrink width so the expand/collapse column is moved away from border we create
				ncBounds.X += 10;
				ncBounds.Width -= 20;
			}
			else
				base.AdjustBeforeDrawing(tc, nc, ref ncBounds);
			
		}
		
		public override void Draw(TreeControl tc, NodeCollection nc, Graphics g, Rectangle clipRectangle, bool preDraw)
		{
			// Do not post draw as that would overwrite the node drawing
			if (preDraw)
			{
				// We only modify nested collections
				if (nc != tc.Nodes)
				{
					int depth = 0;
					
					// Find depth of this collection
					Node parent = nc.ParentNode;
					while(parent != null)
					{
						parent = parent.Parent;
						depth++;
					}
					
					Color backColor;
					
					// We use just three unique colors for background
					depth = depth % 2;
					
					if (depth == 0)
						backColor = Color.Silver;
					else if (depth == 1)
						backColor = Color.Bisque;
					else
						backColor = Color.LightGreen;
					
					Rectangle ncBounds = nc.Bounds;
					
					// Create font we will use for custom painting
					Font drawFont = new Font("Arial", 8f, FontStyle.Bold);				
				
					// Add space to draw this text
					int textHeight = (int)(drawFont.Height * 1.25);
					
					// Draw the text first
					g.DrawString("Custom Collection", drawFont, Brushes.Black, ncBounds.Left + 2, ncBounds.Top + 1);
					
					// We only want to draw slightly inside the collection
					Rectangle wholeBounds = new Rectangle(ncBounds.Left, ncBounds.Top + 2 + textHeight, ncBounds.Width, ncBounds.Height - 5 - textHeight);
					
					// Create an appropriate gradient brush
					using(Brush drawBrush = new LinearGradientBrush(wholeBounds, backColor, Color.White, 0f))
					{
						// Use Gradient Fill for entire area
						g.FillRectangle(drawBrush, wholeBounds);
						
						// Draw an outline around whole border					
						g.DrawRectangle(Pens.Gray, wholeBounds);
					}
				}
			}
					
			base.Draw(tc, nc, g, clipRectangle, preDraw);
		}
	}
}
