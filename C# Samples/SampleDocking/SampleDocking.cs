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
using System.Xml;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Menus;
using Crownwood.DotNetMagic.Docking;
using Crownwood.DotNetMagic.Controls;
using Crownwood.DotNetMagic.Controls.Command;

namespace SampleDocking
{
	/// <summary>
	/// Sample form showing how to use the docking system.
	/// </summary>
	public class SampleDockingForm : System.Windows.Forms.Form
	{
		// Instance fields
		private int count = 0;
		private ImageList imageList;
		private DockingManager dockingManager;
		private byte[] slot1;
		private byte[] slot2;
		private bool allowContextMenu;
		private bool customContextMenu;
		private bool captionBars;
		private bool closeButtons;
		private int ignoreClose;
		private ArrayList _treeControls;
		private ArrayList _exampleForms;
		
		// Designer generated fields
		private Crownwood.DotNetMagic.Menus.MenuControl menuControl1;
		private Crownwood.DotNetMagic.Controls.ToolControl toolControl1;
		private Crownwood.DotNetMagic.Controls.StatusBarControl statusBarControl1;
		private Crownwood.DotNetMagic.Controls.StatusPanel statusPanelText;
		private Crownwood.DotNetMagic.Controls.StatusPanel statusPanelDate;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuStyles;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuManage;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuOffice2003;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuIDE;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuPlain;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator1;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuExit;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCreate;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCreateExampleForm;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCreateTreeControl;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCreateTextBox;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator2;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCreate3Row;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCreate3Column;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCreate3Window;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCreate3AutoHidden;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator3;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCreate3Floating;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuHideAll;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuShowAll;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator4;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuAllowRedocking;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuAllowFloating;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator5;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuDeleteAll;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSaveConfig1;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSaveConfig2;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator6;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuLoadConfig1;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuLoadConfig2;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator7;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSaveArray1;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSaveArray2;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator8;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuLoadArray1;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuLoadArray2;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuPersistence;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSettings;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuAllowContext;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuCustomizeContext;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator9;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuResizeDefault;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuResize1;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuResize5;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuResize7;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator10;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuShowCaptions;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuShowClose;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator11;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuAllowAllClose;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuIgnoreTreeControlClose;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuIgnoreTextBoxClose;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuIgnoreExampleFormClose;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuIgnore;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator12;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuMinMax;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuIDE2005;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuFeedback;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuFocusRectangles;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSemiBlocks;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuDiamonds;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSquares;
		private Crownwood.DotNetMagic.Controls.TabControl tabControl1;
		private Crownwood.DotNetMagic.Controls.TabPage tabPageFirst;
		private Crownwood.DotNetMagic.Controls.TabPage tabPageSecond;
		private Crownwood.DotNetMagic.Controls.TabPage tabPageThird;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Timer timer;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand toolOffice2003;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand toolIDE;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand toolPlain;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand toolShowAll;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand toolHideAll;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand toolIDE2005;
		private Crownwood.DotNetMagic.Controls.Command.SeparatorCommand separatorCommand1;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new SampleDockingForm());
		}

		public SampleDockingForm()
		{
			// Reduce the amount of flicker that occurs when windows are redocked within
			// the container. As this prevents unsightly backcolors being drawn in the
			// WM_ERASEBACKGROUND that seems to occur.
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			// Initialize instance fields
			allowContextMenu = true;
			customContextMenu = false;
			captionBars = true;
			closeButtons = true;
			ignoreClose = 0;

			// Required for Windows Form Designer support
			InitializeComponent();
			
			// Update the date and time in status bar immediately
			timer_Tick(this, EventArgs.Empty);
			
			// Setup the docking manager instance
			dockingManager = new DockingManager(this, VisualStyle.Office2003);

			// Hook into the events generated by the docking manager
			dockingManager.ContextMenu += new DockingManager.ContextMenuHandler(OnContextMenu);
			dockingManager.ContentShown += new DockingManager.ContentHandler(OnContentShown);
			dockingManager.ContentHidden += new DockingManager.ContentHandler(OnContentHidden);
			dockingManager.ContentHiding += new DockingManager.ContentHidingHandler(OnContentHiding);

			// Ensure correct inner and outer values for correct window positioning
			dockingManager.InnerControl = tabControl1;
			dockingManager.OuterControl = toolControl1;

			// Setup custom config handling
			dockingManager.SaveCustomConfig += new DockingManager.SaveCustomConfigHandler(OnSaveConfig);
			dockingManager.LoadCustomConfig += new DockingManager.LoadCustomConfigHandler(OnLoadConfig);

			// Create the initial starting docking windows			
			menuCreateTreeControl_Click(this, EventArgs.Empty);
			menuCreateExampleForm_Click(this, EventArgs.Empty);
			menuCreate3AutoHidden_Click(this, EventArgs.Empty);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SampleDockingForm));
			this.menuControl1 = new Crownwood.DotNetMagic.Menus.MenuControl();
			this.menuStyles = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuOffice2003 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuIDE2005 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuIDE = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuPlain = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator1 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuExit = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuCreate = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuCreateTreeControl = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuCreateTextBox = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuCreateExampleForm = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator2 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuCreate3Row = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuCreate3Column = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuCreate3Window = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuCreate3AutoHidden = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator3 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuCreate3Floating = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuFeedback = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuFocusRectangles = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSemiBlocks = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuDiamonds = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSquares = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuManage = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuHideAll = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuShowAll = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator4 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuAllowRedocking = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuAllowFloating = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator5 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuDeleteAll = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuPersistence = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSaveConfig1 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSaveConfig2 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator6 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuLoadConfig1 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuLoadConfig2 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator7 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSaveArray1 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSaveArray2 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator8 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuLoadArray1 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuLoadArray2 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSettings = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuAllowContext = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuCustomizeContext = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator9 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuResizeDefault = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuResize1 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuResize5 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuResize7 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator10 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuShowCaptions = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuShowClose = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator11 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuAllowAllClose = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuIgnoreTreeControlClose = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuIgnoreTextBoxClose = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuIgnoreExampleFormClose = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuIgnore = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator12 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuMinMax = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.toolControl1 = new Crownwood.DotNetMagic.Controls.ToolControl();
			this.toolOffice2003 = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.toolIDE2005 = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.toolIDE = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.toolPlain = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.separatorCommand1 = new Crownwood.DotNetMagic.Controls.Command.SeparatorCommand();
			this.toolShowAll = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.toolHideAll = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.statusBarControl1 = new Crownwood.DotNetMagic.Controls.StatusBarControl();
			this.statusPanelText = new Crownwood.DotNetMagic.Controls.StatusPanel();
			this.statusPanelDate = new Crownwood.DotNetMagic.Controls.StatusPanel();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.tabControl1 = new Crownwood.DotNetMagic.Controls.TabControl();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.tabPageFirst = new Crownwood.DotNetMagic.Controls.TabPage();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.tabPageSecond = new Crownwood.DotNetMagic.Controls.TabPage();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.tabPageThird = new Crownwood.DotNetMagic.Controls.TabPage();
			this.textBox3 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.toolControl1)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPageFirst.SuspendLayout();
			this.tabPageSecond.SuspendLayout();
			this.tabPageThird.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuControl1
			// 
			this.menuControl1.AllowLayered = true;
			this.menuControl1.AnimateStyle = Crownwood.DotNetMagic.Menus.Animation.System;
			this.menuControl1.AnimateTime = 100;
			this.menuControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.menuControl1.Direction = Crownwood.DotNetMagic.Common.LayoutDirection.Horizontal;
			this.menuControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.menuControl1.HighlightTextColor = System.Drawing.SystemColors.MenuText;
			this.menuControl1.IgnoreF10 = false;
			this.menuControl1.Location = new System.Drawing.Point(0, 0);
			this.menuControl1.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																									  this.menuStyles,
																									  this.menuCreate,
																									  this.menuFeedback,
																									  this.menuManage,
																									  this.menuPersistence,
																									  this.menuSettings});
			this.menuControl1.Name = "menuControl1";
			this.menuControl1.Size = new System.Drawing.Size(492, 25);
			this.menuControl1.Style = Crownwood.DotNetMagic.Common.VisualStyle.Office2003;
			this.menuControl1.TabIndex = 0;
			this.menuControl1.TabStop = false;
			this.menuControl1.Text = "menuControl1";
			this.menuControl1.Selected += new Crownwood.DotNetMagic.Menus.CommandHandler(this.menuControl1_Selected);
			this.menuControl1.Deselected += new Crownwood.DotNetMagic.Menus.CommandHandler(this.menuControl1_Deselected);
			// 
			// menuStyles
			// 
			this.menuStyles.Description = "Select visual style";
			this.menuStyles.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																									this.menuOffice2003,
																									this.menuIDE2005,
																									this.menuIDE,
																									this.menuPlain,
																									this.menuSeparator1,
																									this.menuExit});
			this.menuStyles.Text = "Styles";
			// 
			// menuOffice2003
			// 
			this.menuOffice2003.Description = "Use the Office2003 visual style";
			this.menuOffice2003.Text = "Office2003";
			this.menuOffice2003.Click += new System.EventHandler(this.menuOffice2003_Click);
			this.menuOffice2003.Update += new System.EventHandler(this.menuOffice2003_Update);
			// 
			// menuIDE2005
			// 
			this.menuIDE2005.Description = "Use the IDE2005 visual style";
			this.menuIDE2005.Text = "IDE2005";
			this.menuIDE2005.Click += new System.EventHandler(this.menuIDE2005_Click);
			this.menuIDE2005.Update += new System.EventHandler(this.menuIDE2005_Update);
			// 
			// menuIDE
			// 
			this.menuIDE.Description = "Use the IDE visual style";
			this.menuIDE.Text = "IDE";
			this.menuIDE.Click += new System.EventHandler(this.menuIDE_Click);
			this.menuIDE.Update += new System.EventHandler(this.menuIDE_Update);
			// 
			// menuPlain
			// 
			this.menuPlain.Description = "Use the Plain visual style";
			this.menuPlain.Text = "Plain";
			this.menuPlain.Click += new System.EventHandler(this.menuPlain_Click);
			this.menuPlain.Update += new System.EventHandler(this.menuPlain_Update);
			// 
			// menuSeparator1
			// 
			this.menuSeparator1.Description = "stylesSeparator";
			this.menuSeparator1.Text = "-";
			// 
			// menuExit
			// 
			this.menuExit.Description = "Exit the application";
			this.menuExit.Text = "Exit";
			this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
			// 
			// menuCreate
			// 
			this.menuCreate.Description = "Create new docking windows";
			this.menuCreate.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																									this.menuCreateTreeControl,
																									this.menuCreateTextBox,
																									this.menuCreateExampleForm,
																									this.menuSeparator2,
																									this.menuCreate3Row,
																									this.menuCreate3Column,
																									this.menuCreate3Window,
																									this.menuCreate3AutoHidden,
																									this.menuSeparator3,
																									this.menuCreate3Floating});
			this.menuCreate.Text = "Create";
			// 
			// menuCreateTreeControl
			// 
			this.menuCreateTreeControl.Description = "Create a docking window on the left edge with a TreeControl";
			this.menuCreateTreeControl.Text = "Create TreeControl";
			this.menuCreateTreeControl.Click += new System.EventHandler(this.menuCreateTreeControl_Click);
			// 
			// menuCreateTextBox
			// 
			this.menuCreateTextBox.Description = "Create a docking window on the right edge with a TextBox";
			this.menuCreateTextBox.Text = "Create TextBox";
			this.menuCreateTextBox.Click += new System.EventHandler(this.menuCreateTextBox_Click);
			// 
			// menuCreateExampleForm
			// 
			this.menuCreateExampleForm.Description = "Create a docking window at the bottom edge with an example Form";
			this.menuCreateExampleForm.Text = "Create ExampleForm";
			this.menuCreateExampleForm.Click += new System.EventHandler(this.menuCreateExampleForm_Click);
			// 
			// menuSeparator2
			// 
			this.menuSeparator2.Description = "Menu";
			this.menuSeparator2.Text = "-";
			// 
			// menuCreate3Row
			// 
			this.menuCreate3Row.Description = "Create three docking windows in the same row";
			this.menuCreate3Row.Text = "Create 3 in Row";
			this.menuCreate3Row.Click += new System.EventHandler(this.menuCreate3Row_Click);
			// 
			// menuCreate3Column
			// 
			this.menuCreate3Column.Description = "Create three docking windows in the same column";
			this.menuCreate3Column.Text = "Create 3 in Column";
			this.menuCreate3Column.Click += new System.EventHandler(this.menuCreate3Column_Click);
			// 
			// menuCreate3Window
			// 
			this.menuCreate3Window.Description = "Create three items in the same docking window";
			this.menuCreate3Window.Text = "Create 3 in Window";
			this.menuCreate3Window.Click += new System.EventHandler(this.menuCreate3Window_Click);
			// 
			// menuCreate3AutoHidden
			// 
			this.menuCreate3AutoHidden.Description = "Create three items in the same docking window wit AutoHidden state";
			this.menuCreate3AutoHidden.Text = "Create 3 AutoHidden";
			this.menuCreate3AutoHidden.Click += new System.EventHandler(this.menuCreate3AutoHidden_Click);
			// 
			// menuSeparator3
			// 
			this.menuSeparator3.Description = "Menu";
			this.menuSeparator3.Text = "-";
			// 
			// menuCreate3Floating
			// 
			this.menuCreate3Floating.Description = "Create three items in the same floating window";
			this.menuCreate3Floating.Text = "Create 3 Floating";
			this.menuCreate3Floating.Click += new System.EventHandler(this.menuCreate3Floating_Click);
			// 
			// menuFeedback
			// 
			this.menuFeedback.Description = "Select drag feedback indications";
			this.menuFeedback.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																									  this.menuFocusRectangles,
																									  this.menuSemiBlocks,
																									  this.menuDiamonds,
																									  this.menuSquares});
			this.menuFeedback.Text = "Feedback";
			// 
			// menuFocusRectangles
			// 
			this.menuFocusRectangles.Description = "Focus rectangles";
			this.menuFocusRectangles.Text = "Focus rectangles";
			this.menuFocusRectangles.Click += new System.EventHandler(this.menuFocusRectangles_Click);
			this.menuFocusRectangles.Update += new System.EventHandler(this.menuFocusRectangles_Update);
			// 
			// menuSemiBlocks
			// 
			this.menuSemiBlocks.Description = "Semi-transparent blocks";
			this.menuSemiBlocks.Text = "Semi-transparent blocks";
			this.menuSemiBlocks.Click += new System.EventHandler(this.menuSemiBlocks_Click);
			this.menuSemiBlocks.Update += new System.EventHandler(this.menuSemiBlocks_Update);
			// 
			// menuDiamonds
			// 
			this.menuDiamonds.Description = "Diamond indicators";
			this.menuDiamonds.Text = "Diamond indicators";
			this.menuDiamonds.Click += new System.EventHandler(this.menuDiamonds_Click);
			this.menuDiamonds.Update += new System.EventHandler(this.menuDiamonds_Update);
			// 
			// menuSquares
			// 
			this.menuSquares.Description = "Square indicators";
			this.menuSquares.Text = "Square indicators";
			this.menuSquares.Click += new System.EventHandler(this.menuSquares_Click);
			this.menuSquares.Update += new System.EventHandler(this.menuSquares_Update);
			// 
			// menuManage
			// 
			this.menuManage.Description = "Manage the collection of docking windows";
			this.menuManage.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																									this.menuHideAll,
																									this.menuShowAll,
																									this.menuSeparator4,
																									this.menuAllowRedocking,
																									this.menuAllowFloating,
																									this.menuSeparator5,
																									this.menuDeleteAll});
			this.menuManage.Text = "Manage";
			// 
			// menuHideAll
			// 
			this.menuHideAll.Description = "Hide all showing docking windows";
			this.menuHideAll.Text = "Hide All";
			this.menuHideAll.Click += new System.EventHandler(this.menuHideAll_Click);
			// 
			// menuShowAll
			// 
			this.menuShowAll.Description = "Show all hidden docking windows";
			this.menuShowAll.Text = "Show All";
			this.menuShowAll.Click += new System.EventHandler(this.menuShowAll_Click);
			// 
			// menuSeparator4
			// 
			this.menuSeparator4.Description = "Menu";
			this.menuSeparator4.Text = "-";
			// 
			// menuAllowRedocking
			// 
			this.menuAllowRedocking.Description = "Should the user be allowed to redock windows";
			this.menuAllowRedocking.Text = "Allow Redocking";
			this.menuAllowRedocking.Click += new System.EventHandler(this.menuAllowRedocking_Click);
			this.menuAllowRedocking.Update += new System.EventHandler(this.menuAllowRedocking_Update);
			// 
			// menuAllowFloating
			// 
			this.menuAllowFloating.Description = "Should user be allowed to make a window floating";
			this.menuAllowFloating.Text = "Allow Floating";
			this.menuAllowFloating.Click += new System.EventHandler(this.menuAllowFloating_Click);
			this.menuAllowFloating.Update += new System.EventHandler(this.menuAllowFloating_Update);
			// 
			// menuSeparator5
			// 
			this.menuSeparator5.Description = "Menu";
			this.menuSeparator5.Text = "-";
			// 
			// menuDeleteAll
			// 
			this.menuDeleteAll.Description = "Delete all the docking windows";
			this.menuDeleteAll.Text = "Delete All";
			this.menuDeleteAll.Click += new System.EventHandler(this.menuDeleteAll_Click);
			// 
			// menuPersistence
			// 
			this.menuPersistence.Description = "Save and load layout";
			this.menuPersistence.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																										 this.menuSaveConfig1,
																										 this.menuSaveConfig2,
																										 this.menuSeparator6,
																										 this.menuLoadConfig1,
																										 this.menuLoadConfig2,
																										 this.menuSeparator7,
																										 this.menuSaveArray1,
																										 this.menuSaveArray2,
																										 this.menuSeparator8,
																										 this.menuLoadArray1,
																										 this.menuLoadArray2});
			this.menuPersistence.Text = "Persistence";
			// 
			// menuSaveConfig1
			// 
			this.menuSaveConfig1.Description = "Save layout information to file Config1.xml";
			this.menuSaveConfig1.Text = "Save as Config1.xml";
			this.menuSaveConfig1.Click += new System.EventHandler(this.menuSaveConfig1_Click);
			// 
			// menuSaveConfig2
			// 
			this.menuSaveConfig2.Description = "Save layout information to file Config2.xml";
			this.menuSaveConfig2.Text = "Save as Config2.xml";
			this.menuSaveConfig2.Click += new System.EventHandler(this.menuSaveConfig2_Click);
			// 
			// menuSeparator6
			// 
			this.menuSeparator6.Description = "Menu";
			this.menuSeparator6.Text = "-";
			// 
			// menuLoadConfig1
			// 
			this.menuLoadConfig1.Description = "Load layout information from file Config1.xml";
			this.menuLoadConfig1.Text = "Load from Config1.xml";
			this.menuLoadConfig1.Click += new System.EventHandler(this.menuLoadConfig1_Click);
			// 
			// menuLoadConfig2
			// 
			this.menuLoadConfig2.Description = "Load layout information from file Config2.xml";
			this.menuLoadConfig2.Text = "Load from Config2.xml";
			this.menuLoadConfig2.Click += new System.EventHandler(this.menuLoadConfig2_Click);
			// 
			// menuSeparator7
			// 
			this.menuSeparator7.Description = "Menu";
			this.menuSeparator7.Text = "-";
			// 
			// menuSaveArray1
			// 
			this.menuSaveArray1.Description = "Save layout information into a byte array";
			this.menuSaveArray1.Text = "Save to byte array1";
			this.menuSaveArray1.Click += new System.EventHandler(this.menuSaveArray1_Click);
			// 
			// menuSaveArray2
			// 
			this.menuSaveArray2.Description = "Save layout information into a byte array";
			this.menuSaveArray2.Text = "Save to byte array2";
			this.menuSaveArray2.Click += new System.EventHandler(this.menuSaveArray2_Click);
			// 
			// menuSeparator8
			// 
			this.menuSeparator8.Description = "Menu";
			this.menuSeparator8.Text = "-";
			// 
			// menuLoadArray1
			// 
			this.menuLoadArray1.Description = "Load layout information from byte array";
			this.menuLoadArray1.Text = "Load from byte array1";
			this.menuLoadArray1.Click += new System.EventHandler(this.menuLoadArray1_Click);
			// 
			// menuLoadArray2
			// 
			this.menuLoadArray2.Description = "Load layout information from byte array";
			this.menuLoadArray2.Text = "Load from byte array2";
			this.menuLoadArray2.Click += new System.EventHandler(this.menuLoadArray2_Click);
			// 
			// menuSettings
			// 
			this.menuSettings.Description = "Docking manager settings";
			this.menuSettings.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																									  this.menuAllowContext,
																									  this.menuCustomizeContext,
																									  this.menuSeparator9,
																									  this.menuResizeDefault,
																									  this.menuResize1,
																									  this.menuResize5,
																									  this.menuResize7,
																									  this.menuSeparator10,
																									  this.menuShowCaptions,
																									  this.menuShowClose,
																									  this.menuSeparator11,
																									  this.menuAllowAllClose,
																									  this.menuIgnoreTreeControlClose,
																									  this.menuIgnoreTextBoxClose,
																									  this.menuIgnoreExampleFormClose,
																									  this.menuIgnore,
																									  this.menuSeparator12,
																									  this.menuMinMax});
			this.menuSettings.Text = "Settings";
			// 
			// menuAllowContext
			// 
			this.menuAllowContext.Description = "Should the user be allowed to access a context menu";
			this.menuAllowContext.Text = "Allow Context Menu";
			this.menuAllowContext.Click += new System.EventHandler(this.menuAllowContext_Click);
			this.menuAllowContext.Update += new System.EventHandler(this.menuAllowContext_Update);
			// 
			// menuCustomizeContext
			// 
			this.menuCustomizeContext.Description = "Demonstrate customizing the context menu";
			this.menuCustomizeContext.Text = "Customize Context Menu";
			this.menuCustomizeContext.Click += new System.EventHandler(this.menuCustomizeContext_Click);
			this.menuCustomizeContext.Update += new System.EventHandler(this.menuCustomizeContext_Update);
			// 
			// menuSeparator9
			// 
			this.menuSeparator9.Description = "Menu";
			this.menuSeparator9.Text = "-";
			// 
			// menuResizeDefault
			// 
			this.menuResizeDefault.Description = "Set all the resize bars to be the default size";
			this.menuResizeDefault.Text = "Default ResizeBar";
			this.menuResizeDefault.Click += new System.EventHandler(this.menuResizeDefault_Click);
			this.menuResizeDefault.Update += new System.EventHandler(this.menuResizeDefault_Update);
			// 
			// menuResize1
			// 
			this.menuResize1.Description = "Set each resize bar to have a size of 1 pixel";
			this.menuResize1.Text = "1 Pixel ResizeBar";
			this.menuResize1.Click += new System.EventHandler(this.menuResize1_Click);
			this.menuResize1.Update += new System.EventHandler(this.menuResize1_Update);
			// 
			// menuResize5
			// 
			this.menuResize5.Description = "Set each resize bar to have a size of 5 pixels";
			this.menuResize5.Text = "5 Pixel ResizeBar";
			this.menuResize5.Click += new System.EventHandler(this.menuResize5_Click);
			this.menuResize5.Update += new System.EventHandler(this.menuResize5_Update);
			// 
			// menuResize7
			// 
			this.menuResize7.Description = "Set each resize bar to have a size of 7 pixels";
			this.menuResize7.Text = "7 Pixel ResizeBar";
			this.menuResize7.Click += new System.EventHandler(this.menuResize7_Click);
			this.menuResize7.Update += new System.EventHandler(this.menuResize7_Update);
			// 
			// menuSeparator10
			// 
			this.menuSeparator10.Description = "Menu";
			this.menuSeparator10.Text = "-";
			// 
			// menuShowCaptions
			// 
			this.menuShowCaptions.Description = "Should the docking windows have caption bars";
			this.menuShowCaptions.Shortcut = System.Windows.Forms.Shortcut.AltF10;
			this.menuShowCaptions.Text = "Show caption bars";
			this.menuShowCaptions.Click += new System.EventHandler(this.menuShowCaptions_Click);
			this.menuShowCaptions.Update += new System.EventHandler(this.menuShowCaptions_Update);
			// 
			// menuShowClose
			// 
			this.menuShowClose.Description = "Should the docking windows have close buttons";
			this.menuShowClose.Text = "Show close buttons";
			this.menuShowClose.Click += new System.EventHandler(this.menuShowClose_Click);
			this.menuShowClose.Update += new System.EventHandler(this.menuShowClose_Update);
			// 
			// menuSeparator11
			// 
			this.menuSeparator11.Description = "Menu";
			this.menuSeparator11.Text = "-";
			// 
			// menuAllowAllClose
			// 
			this.menuAllowAllClose.Description = "Allow close buttons to hide a docking window";
			this.menuAllowAllClose.Text = "Allow all close buttons";
			this.menuAllowAllClose.Click += new System.EventHandler(this.menuAllowAllClose_Click);
			this.menuAllowAllClose.Update += new System.EventHandler(this.menuAllowAllClose_Update);
			// 
			// menuIgnoreTreeControlClose
			// 
			this.menuIgnoreTreeControlClose.Description = "Ignore the close button for TreeControl docking windows";
			this.menuIgnoreTreeControlClose.Text = "Ignore TreeControl close buttons";
			this.menuIgnoreTreeControlClose.Click += new System.EventHandler(this.menuIgnoreTreeControlClose_Click);
			this.menuIgnoreTreeControlClose.Update += new System.EventHandler(this.menuIgnoreTreeControlClose_Update);
			// 
			// menuIgnoreTextBoxClose
			// 
			this.menuIgnoreTextBoxClose.Description = "Ignore the close button for TextBox docking windows";
			this.menuIgnoreTextBoxClose.Text = "Ignore TextBox close buttons";
			this.menuIgnoreTextBoxClose.Click += new System.EventHandler(this.menuIgnoreTextBoxClose_Click);
			this.menuIgnoreTextBoxClose.Update += new System.EventHandler(this.menuIgnoreTextBoxClose_Update);
			// 
			// menuIgnoreExampleFormClose
			// 
			this.menuIgnoreExampleFormClose.Description = "Ignore the close button for ExampleForm docking windows";
			this.menuIgnoreExampleFormClose.Text = "Ignore ExampleForm close buttons";
			this.menuIgnoreExampleFormClose.Click += new System.EventHandler(this.menuIgnoreExampleFormClose_Click);
			this.menuIgnoreExampleFormClose.Update += new System.EventHandler(this.menuIgnoreExampleFormClose_Update);
			// 
			// menuIgnore
			// 
			this.menuIgnore.Description = "Ignore the close button for all docking windows";
			this.menuIgnore.Text = "Ignore all close buttons";
			this.menuIgnore.Click += new System.EventHandler(this.menuIgnore_Click);
			this.menuIgnore.Update += new System.EventHandler(this.menuIgnore_Update);
			// 
			// menuSeparator12
			// 
			this.menuSeparator12.Description = "Menu";
			this.menuSeparator12.Text = "-";
			// 
			// menuMinMax
			// 
			this.menuMinMax.Description = "Should docking windows in same row/column be allowed to min/max windows";
			this.menuMinMax.Text = "Allow Min/Max functionality";
			this.menuMinMax.Click += new System.EventHandler(this.menuMinMax_Click);
			this.menuMinMax.Update += new System.EventHandler(this.menuMinMax_Update);
			// 
			// toolControl1
			// 
			this.toolControl1.Commands.AddRange(new Crownwood.DotNetMagic.Controls.Command.CommandBase[] {
																											 this.toolOffice2003,
																											 this.toolIDE2005,
																											 this.toolIDE,
																											 this.toolPlain,
																											 this.separatorCommand1,
																											 this.toolShowAll,
																											 this.toolHideAll});
			this.toolControl1.Location = new System.Drawing.Point(0, 25);
			this.toolControl1.Name = "toolControl1";
			this.toolControl1.Padding.Bottom = 4;
			this.toolControl1.Padding.Top = 1;
			this.toolControl1.Size = new System.Drawing.Size(492, 23);
			this.toolControl1.TabIndex = 1;
			this.toolControl1.UpdateTimeout = 100;
			// 
			// toolOffice2003
			// 
			this.toolOffice2003.Tag = null;
			this.toolOffice2003.Text = "Office2003";
			this.toolOffice2003.Tooltip = "Use the Office2003 visual style";
			this.toolOffice2003.Update += new System.EventHandler(this.toolOffice2003_Update);
			this.toolOffice2003.Click += new System.EventHandler(this.toolOffice2003_Click);
			// 
			// toolIDE2005
			// 
			this.toolIDE2005.Tag = null;
			this.toolIDE2005.Text = "IDE2005";
			this.toolIDE2005.Tooltip = "Use the IDE2005 visual style";
			this.toolIDE2005.Update += new System.EventHandler(this.toolIDE2005_Update);
			this.toolIDE2005.Click += new System.EventHandler(this.toolIDE2005_Click);
			// 
			// toolIDE
			// 
			this.toolIDE.Tag = null;
			this.toolIDE.Text = "IDE";
			this.toolIDE.Tooltip = "Use the IDE visual style";
			this.toolIDE.Update += new System.EventHandler(this.toolIDE_Update);
			this.toolIDE.Click += new System.EventHandler(this.toolIDE_Click);
			// 
			// toolPlain
			// 
			this.toolPlain.Tag = null;
			this.toolPlain.Text = "Plain";
			this.toolPlain.Tooltip = "Use the Plain visual style";
			this.toolPlain.Update += new System.EventHandler(this.toolPlain_Update);
			this.toolPlain.Click += new System.EventHandler(this.toolPlain_Click);
			// 
			// separatorCommand1
			// 
			this.separatorCommand1.Tag = null;
			// 
			// toolShowAll
			// 
			this.toolShowAll.Tag = null;
			this.toolShowAll.Text = "Show All";
			this.toolShowAll.Tooltip = "Show all hidden docking windows";
			this.toolShowAll.Click += new System.EventHandler(this.toolShowAll_Click);
			// 
			// toolHideAll
			// 
			this.toolHideAll.Tag = null;
			this.toolHideAll.Text = "Hide All";
			this.toolHideAll.Tooltip = "Hide all showing docking windows";
			this.toolHideAll.Click += new System.EventHandler(this.toolHideAll_Click);
			// 
			// statusBarControl1
			// 
			this.statusBarControl1.Location = new System.Drawing.Point(0, 342);
			this.statusBarControl1.Name = "statusBarControl1";
			this.statusBarControl1.PadTop = 3;
			this.statusBarControl1.Size = new System.Drawing.Size(492, 24);
			this.statusBarControl1.StatusPanels.AddRange(new Crownwood.DotNetMagic.Controls.StatusPanel[] {
																											  this.statusPanelText,
																											  this.statusPanelDate});
			this.statusBarControl1.TabIndex = 2;
			// 
			// statusPanelText
			// 
			this.statusPanelText.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.statusPanelText.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.statusPanelText.AutoSizing = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusPanelText.Location = new System.Drawing.Point(2, 2);
			this.statusPanelText.Name = "statusPanelText";
			this.statusPanelText.Size = new System.Drawing.Size(464, 16);
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
			// tabControl1
			// 
			this.tabControl1.Appearance = Crownwood.DotNetMagic.Controls.VisualAppearance.MultiDocument;
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.IDEPixelBorder = true;
			this.tabControl1.ImageList = this.imageList;
			this.tabControl1.Location = new System.Drawing.Point(0, 48);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.OfficeDockSides = false;
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.ShowClose = false;
			this.tabControl1.ShowDropSelect = false;
			this.tabControl1.Size = new System.Drawing.Size(492, 294);
			this.tabControl1.TabIndex = 3;
			this.tabControl1.TabPages.AddRange(new Crownwood.DotNetMagic.Controls.TabPage[] {
																								this.tabPageFirst,
																								this.tabPageSecond,
																								this.tabPageThird});
			this.tabControl1.TextTips = true;
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// tabPageFirst
			// 
			this.tabPageFirst.Controls.Add(this.textBox1);
			this.tabPageFirst.ImageIndex = 6;
			this.tabPageFirst.InactiveBackColor = System.Drawing.Color.Empty;
			this.tabPageFirst.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tabPageFirst.InactiveTextColor = System.Drawing.Color.Empty;
			this.tabPageFirst.Location = new System.Drawing.Point(1, 24);
			this.tabPageFirst.Name = "tabPageFirst";
			this.tabPageFirst.SelectBackColor = System.Drawing.Color.Empty;
			this.tabPageFirst.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tabPageFirst.SelectTextColor = System.Drawing.Color.Empty;
			this.tabPageFirst.Size = new System.Drawing.Size(490, 269);
			this.tabPageFirst.TabIndex = 3;
			this.tabPageFirst.Title = "One";
			this.tabPageFirst.ToolTip = "First Page";
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(490, 269);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// tabPageSecond
			// 
			this.tabPageSecond.Controls.Add(this.textBox2);
			this.tabPageSecond.ImageIndex = 6;
			this.tabPageSecond.InactiveBackColor = System.Drawing.Color.Empty;
			this.tabPageSecond.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tabPageSecond.InactiveTextColor = System.Drawing.Color.Empty;
			this.tabPageSecond.Location = new System.Drawing.Point(1, 24);
			this.tabPageSecond.Name = "tabPageSecond";
			this.tabPageSecond.SelectBackColor = System.Drawing.Color.Empty;
			this.tabPageSecond.Selected = false;
			this.tabPageSecond.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tabPageSecond.SelectTextColor = System.Drawing.Color.Empty;
			this.tabPageSecond.Size = new System.Drawing.Size(490, 269);
			this.tabPageSecond.TabIndex = 4;
			this.tabPageSecond.Title = "Two";
			this.tabPageSecond.ToolTip = "Second Page";
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox2.Location = new System.Drawing.Point(0, 0);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(490, 269);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "";
			// 
			// tabPageThird
			// 
			this.tabPageThird.Controls.Add(this.textBox3);
			this.tabPageThird.ImageIndex = 6;
			this.tabPageThird.InactiveBackColor = System.Drawing.Color.Empty;
			this.tabPageThird.InactiveTextBackColor = System.Drawing.Color.Empty;
			this.tabPageThird.InactiveTextColor = System.Drawing.Color.Empty;
			this.tabPageThird.Location = new System.Drawing.Point(1, 24);
			this.tabPageThird.Name = "tabPageThird";
			this.tabPageThird.SelectBackColor = System.Drawing.Color.Empty;
			this.tabPageThird.Selected = false;
			this.tabPageThird.SelectTextBackColor = System.Drawing.Color.Empty;
			this.tabPageThird.SelectTextColor = System.Drawing.Color.Empty;
			this.tabPageThird.Size = new System.Drawing.Size(490, 269);
			this.tabPageThird.TabIndex = 5;
			this.tabPageThird.Title = "Three";
			this.tabPageThird.ToolTip = "Third Page";
			// 
			// textBox3
			// 
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox3.Location = new System.Drawing.Point(0, 0);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(490, 269);
			this.textBox3.TabIndex = 1;
			this.textBox3.Text = "";
			// 
			// SampleDockingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(492, 366);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.toolControl1);
			this.Controls.Add(this.menuControl1);
			this.Controls.Add(this.statusBarControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SampleDockingForm";
			this.Text = "DotNetMagic - SampleDocking";
			((System.ComponentModel.ISupportInitialize)(this.toolControl1)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPageFirst.ResumeLayout(false);
			this.tabPageSecond.ResumeLayout(false);
			this.tabPageThird.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void menuControl1_Selected(Crownwood.DotNetMagic.Menus.MenuCommand item)
		{
			// Place description for selected menu item in the status bar
			statusPanelText.Text = item.Description;
		}

		private void menuControl1_Deselected(Crownwood.DotNetMagic.Menus.MenuCommand item)
		{
			// Clear down the status bar description
			statusPanelText.Text = string.Empty;
		}

		private void menuOffice2003_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.Office2003);
		}

		private void menuOffice2003_Update(object sender, System.EventArgs e)
		{
			MenuCommand mc = sender as MenuCommand;	
			mc.Checked = (menuControl1.Style == VisualStyle.Office2003);
			mc.RadioCheck = (menuControl1.Style == VisualStyle.Office2003);
		}

		private void menuIDE2005_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.IDE2005);
		}

		private void menuIDE2005_Update(object sender, System.EventArgs e)
		{
			MenuCommand mc = sender as MenuCommand;	
			mc.Checked = (menuControl1.Style == VisualStyle.IDE2005);
			mc.RadioCheck = (menuControl1.Style == VisualStyle.IDE2005);
		}

		private void menuIDE_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.IDE);
		}

		private void menuIDE_Update(object sender, System.EventArgs e)
		{
			MenuCommand mc = sender as MenuCommand;		
			mc.Checked = (menuControl1.Style == VisualStyle.IDE);
			mc.RadioCheck = (menuControl1.Style == VisualStyle.IDE);
		}

		private void menuPlain_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.Plain);
		}

		private void menuPlain_Update(object sender, System.EventArgs e)
		{
			MenuCommand mc = sender as MenuCommand;		
			mc.Checked = (menuControl1.Style == VisualStyle.Plain);
			mc.RadioCheck = (menuControl1.Style == VisualStyle.Plain);
		}

		private void menuExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}
		
		private void toolOffice2003_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.Office2003);
		}

		private void toolOffice2003_Update(object sender, System.EventArgs e)
		{
			ButtonCommand bc = sender as ButtonCommand;
			bc.ButtonStyle = ButtonStyle.ToggleButton;
			bc.Pushed = (menuControl1.Style == VisualStyle.Office2003);
		}

		private void toolIDE_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.IDE);
		}

		private void toolIDE_Update(object sender, System.EventArgs e)
		{
			ButtonCommand bc = sender as ButtonCommand;
			bc.ButtonStyle = ButtonStyle.ToggleButton;
			bc.Pushed = (menuControl1.Style == VisualStyle.IDE);		
		}

		private void toolIDE2005_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.IDE2005);
		}

		private void toolIDE2005_Update(object sender, System.EventArgs e)
		{
			ButtonCommand bc = sender as ButtonCommand;
			bc.ButtonStyle = ButtonStyle.ToggleButton;
			bc.Pushed = (menuControl1.Style == VisualStyle.IDE2005);		
		}

		private void toolPlain_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.Plain);
		}

		private void toolPlain_Update(object sender, System.EventArgs e)
		{
			ButtonCommand bc = sender as ButtonCommand;
			bc.ButtonStyle = ButtonStyle.ToggleButton;
			bc.Pushed = (menuControl1.Style == VisualStyle.Plain);
		}

		private void ChangeStyle(VisualStyle style)
		{
			// Update each of the contained elements
			menuControl1.Style = style;
			toolControl1.Style = style;
			statusBarControl1.Style = style;
			dockingManager.Style = style;
			tabControl1.Style = style;

			if (style == VisualStyle.IDE2005)
			{
				tabControl1.ShowArrows = false;
				tabControl1.ShowDropSelect = true;
			}
			else
			{
				tabControl1.ShowArrows = true;
				tabControl1.ShowDropSelect = false;
			}
			
			// Change appearance of any created tree controls
			if (_treeControls != null)
			{
				foreach(TreeControl tc in _treeControls)
				{
					if (!tc.IsDisposed)
					{
						if (style == VisualStyle.Office2003)
							tc.GroupColoring = GroupColoring.Office2003Light;
						else
							tc.GroupColoring = GroupColoring.ControlProperties;
					}
				}
			}
		}

		private void timer_Tick(object sender, System.EventArgs e)
		{
			// Update the date and time section of the status bar
			statusPanelDate.Text = DateTime.Now.ToShortDateString();
		}
		
		private Content CreateTextBoxContent()
		{
			// Create the TextBox for use in the new docking window
			TextBox tb = new TextBox();
			tb.BorderStyle = BorderStyle.None;
			tb.Multiline = true;
		
			// Create a new docking Content instance with our new TextBox
			return dockingManager.Contents.Add(tb, "TextBox " + count, imageList, count++ % 6);
		}

		private Content CreateTreeControlContent()
		{
			// Create the TreeControl for use in the new docking window
			TreeControl tc = new TreeControl();
			tc.SetTreeControlStyle(TreeControlStyles.GroupOfficeLight);
			tc.BorderStyle = TreeBorderStyle.None;
			tc.Indicators = Indicators.AtGroup;
			
			if (dockingManager.Style == VisualStyle.Office2003)
				tc.GroupColoring = GroupColoring.Office2003Light;
			else
				tc.GroupColoring = GroupColoring.ControlProperties;
			
			// Create top level groups
			for(int i=0; i<3; i++)
			{
				Node group = new Node("Group " + i);
				group.Expanded = true;
				tc.Nodes.Add(group); 

				// Create some dummy entries in teh group
				for(int j=0; j<3; j++)
				{
					// Create a new child node
					Node child = new Node("Node " + (i*3+j));
					
					// Give an image and indicator different for each child node
					child.Image = imageList.Images[(i*3+j) % 7];
					child.Indicator = (Indicator)(i*5)+j;
					
					// Give some groups checkbox/radio buttons
					if (i == 1)
						child.CheckStates = NodeCheckStates.Radio;
					else if (i == 2)
						child.CheckStates = NodeCheckStates.TwoStateCheck;

					group.Nodes.Add(child); 
				}
			}
			
			// Maintain a list of all the tree controls created			
			if (_treeControls == null)
				_treeControls = new ArrayList();
			
			// Need to remember so we can switch visual styles	
			_treeControls.Add(tc);
			
			// Create a new docking Content instance with our new TreeControl
			return dockingManager.Contents.Add(tc, "TreeControl " + count, imageList, count++ % 6);
		}

		private Content CreateExampleFormContent()
		{
			// Create the ExampleForm for use in the new docking window
			ExampleForm ef = new ExampleForm();
			
			// Maintain a list of all the example forms created			
			if (_exampleForms == null)
				_exampleForms = new ArrayList();
			
			// Need to remember so we can switch visual styles	
			_exampleForms.Add(ef);
		
			// Create a new docking Content instance with our new ExampleForm
			return dockingManager.Contents.Add(ef, "ExampleForm " + count, imageList, count++ % 6);
		}
				
		private void menuCreateTextBox_Click(object sender, System.EventArgs e)
		{
			// Create a TextBox content instance
			Content c = CreateTextBoxContent();
        
			// Setup initial state to match menu selections
			InitializeContent(c);
        
			// Request a new docking window be created for the above Content on the right edge
			dockingManager.AddContentWithState(c, Crownwood.DotNetMagic.Docking.State.DockRight);
		}

		private void menuCreateTreeControl_Click(object sender, System.EventArgs e)
		{
			// Create a TreeControl content instance
			Content c = CreateTreeControlContent();
        
			// Setup initial state to match menu selections
			InitializeContent(c);
        
			// Request a new docking window be created for the above Content on the left edge
			dockingManager.AddContentWithState(c, Crownwood.DotNetMagic.Docking.State.DockLeft);
		}

		private void menuCreateExampleForm_Click(object sender, System.EventArgs e)
		{
			// Create an ExampleForm content instance
			Content c = CreateExampleFormContent();
        
			// Setup initial state to match menu selections
			InitializeContent(c);
        
			// Request a new docking window be created for the above Content on the bottom edge
			dockingManager.AddContentWithState(c, Crownwood.DotNetMagic.Docking.State.DockBottom);
		}
		
		private void menuCreate3Row_Click(object sender, System.EventArgs e)
		{
			// Create three different content instances
			Content cA = CreateTextBoxContent();
			Content cB = CreateTreeControlContent();
			Content cC = CreateExampleFormContent();
		
			// Setup initial state to match menu selections
			InitializeContent(cA);
			InitializeContent(cB);
			InitializeContent(cC);

			// Request a new Docking window be created for the first content on the bottom edge
			WindowContent wc = dockingManager.AddContentWithState(cA, Crownwood.DotNetMagic.Docking.State.DockBottom) as WindowContent;
        
			// Add two other content into the same Zone
			dockingManager.AddContentToZone(cB, wc.ParentZone, 1);
			dockingManager.AddContentToZone(cC, wc.ParentZone, 2);
		}

		private void menuCreate3Column_Click(object sender, System.EventArgs e)
		{
			// Create three different content instances
			Content cA = CreateTextBoxContent();
			Content cB = CreateTreeControlContent();
			Content cC = CreateExampleFormContent();
		
			// Setup initial state to match menu selections
			InitializeContent(cA);
			InitializeContent(cB);
			InitializeContent(cC);

			// Request a new Docking window be created for the first content on the left edge
			WindowContent wc = dockingManager.AddContentWithState(cA, Crownwood.DotNetMagic.Docking.State.DockLeft) as WindowContent;
        
			// Add two other content into the same Zone
			dockingManager.AddContentToZone(cB, wc.ParentZone, 1);
			dockingManager.AddContentToZone(cC, wc.ParentZone, 2);
		}

		private void menuCreate3Window_Click(object sender, System.EventArgs e)
		{
			// Create three different content instances
			Content cA = CreateTextBoxContent();
			Content cB = CreateTreeControlContent();
			Content cC = CreateExampleFormContent();
		
			// Setup initial state to match menu selections
			InitializeContent(cA);
			InitializeContent(cB);
			InitializeContent(cC);

			// Request a new Docking window be created for the first content on the right edge
			WindowContent wc = dockingManager.AddContentWithState(cA, Crownwood.DotNetMagic.Docking.State.DockRight) as WindowContent;
        
			// Add two other content into the same Zone
			dockingManager.AddContentToWindowContent(cB, wc);
			dockingManager.AddContentToWindowContent(cC, wc);
		}

		private void menuCreate3AutoHidden_Click(object sender, System.EventArgs e)
		{
			// Create three different content instances
			Content cA = CreateTextBoxContent();
			Content cB = CreateTreeControlContent();
			Content cC = CreateExampleFormContent();
		
			// Setup initial state to match menu selections
			InitializeContent(cA);
			InitializeContent(cB);
			InitializeContent(cC);

			// Prevent flicker where the contents are added and display and then a fraction of a 
			// second later they become auto hidden. By suspending and then resuming the layout this
			// small flicker can be avoided.
			this.SuspendLayout();

			// Request a new Docking window be created for the first content on the right edge
			WindowContent wc = dockingManager.AddContentWithState(cA, Crownwood.DotNetMagic.Docking.State.DockRight) as WindowContent;
        
			// Add two other content into the same WindowContent
			dockingManager.AddContentToWindowContent(cB, wc);
			dockingManager.AddContentToWindowContent(cC, wc);
		
			// Move all contents in the same window as cA into autohide mode
			dockingManager.ToggleContentAutoHide(cA);
		
			this.ResumeLayout();
		}

		private void menuCreate3Floating_Click(object sender, System.EventArgs e)
		{
			// Create three different content instances
			Content cA = CreateTextBoxContent();
			Content cB = CreateTreeControlContent();
			Content cC = CreateExampleFormContent();
		
			// Make the floating window larger than the default
			cA.FloatingSize = new Size(250, 350);
		
			// Setup initial state to match menu selections
			InitializeContent(cA);
			InitializeContent(cB);
			InitializeContent(cC);

			// Request a new Docking window be created for the first content as floating
			WindowContent wc = dockingManager.AddContentWithState(cA, Crownwood.DotNetMagic.Docking.State.Floating) as WindowContent;
        
			// Add second content into the same Window
			dockingManager.AddContentToWindowContent(cB, wc);
        
			// Add third into same Zone
			dockingManager.AddContentToZone(cC, wc.ParentZone, 1);
		}

		private void menuHideAll_Click(object sender, System.EventArgs e)
		{
			dockingManager.HideAllContents();
		}

		private void toolHideAll_Click(object sender, System.EventArgs e)
		{
			dockingManager.HideAllContents();
		}

		private void menuShowAll_Click(object sender, System.EventArgs e)
		{
			dockingManager.ShowAllContents();
		}

		private void toolShowAll_Click(object sender, System.EventArgs e)
		{
			dockingManager.ShowAllContents();
		}

		private void menuAllowRedocking_Click(object sender, System.EventArgs e)
		{
			dockingManager.AllowRedocking = !dockingManager.AllowRedocking;
		}

		private void menuAllowRedocking_Update(object sender, System.EventArgs e)
		{
			MenuCommand mc = sender as MenuCommand;		
			mc.Checked = dockingManager.AllowRedocking;
		}

		private void menuAllowFloating_Click(object sender, System.EventArgs e)
		{
			dockingManager.AllowFloating = !dockingManager.AllowFloating;
		}

		private void menuAllowFloating_Update(object sender, System.EventArgs e)
		{
			MenuCommand mc = sender as MenuCommand;		
			mc.Checked = dockingManager.AllowFloating;
		}

		private void menuDeleteAll_Click(object sender, System.EventArgs e)
		{
			dockingManager.Contents.Clear();
		}

		private void menuSaveConfig1_Click(object sender, System.EventArgs e)
		{
			dockingManager.SaveConfigToFile("config1.xml");
		}

		private void menuSaveConfig2_Click(object sender, System.EventArgs e)
		{
			dockingManager.SaveConfigToFile("config2.xml");
		}

		private void menuLoadConfig1_Click(object sender, System.EventArgs e)
		{
			dockingManager.LoadConfigFromFile("config1.xml");
		}

		private void menuLoadConfig2_Click(object sender, System.EventArgs e)
		{
			dockingManager.LoadConfigFromFile("config2.xml");
		}

		private void menuSaveArray1_Click(object sender, System.EventArgs e)
		{
			slot1 = dockingManager.SaveConfigToArray();
		}

		private void menuSaveArray2_Click(object sender, System.EventArgs e)
		{
			slot2 = dockingManager.SaveConfigToArray();
		}

		private void menuLoadArray1_Click(object sender, System.EventArgs e)
		{
			if (slot1 != null)
				dockingManager.LoadConfigFromArray(slot1);
		}

		private void menuLoadArray2_Click(object sender, System.EventArgs e)
		{
			if (slot2 != null)
				dockingManager.LoadConfigFromArray(slot2);
		}

		private void menuAllowContext_Click(object sender, System.EventArgs e)
		{
			// Toggle the display of the docking windows context menu
			allowContextMenu = (allowContextMenu == false);
		}

		private void menuAllowContext_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = allowContextMenu;
		}

		private void menuCustomizeContext_Click(object sender, System.EventArgs e)
		{
			// Toggle the customization of the displayed context menu
			customContextMenu = (customContextMenu == false);
		}

		private void menuCustomizeContext_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = customContextMenu;
		}
		
		private void menuResizeDefault_Click(object sender, System.EventArgs e)
		{
			dockingManager.ResizeBarVector = -1;
		}

		private void menuResizeDefault_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (dockingManager.ResizeBarVector == -1);
			command.RadioCheck = (dockingManager.ResizeBarVector == -1);
		}

		private void menuResize1_Click(object sender, System.EventArgs e)
		{
			dockingManager.ResizeBarVector = 1;
		}

		private void menuResize1_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (dockingManager.ResizeBarVector == 1);
			command.RadioCheck = (dockingManager.ResizeBarVector == 1);
		}

		private void menuResize5_Click(object sender, System.EventArgs e)
		{
			dockingManager.ResizeBarVector = 5;
		}		

		private void menuResize5_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (dockingManager.ResizeBarVector == 5);
			command.RadioCheck = (dockingManager.ResizeBarVector == 5);
		}

		private void menuResize7_Click(object sender, System.EventArgs e)
		{
			dockingManager.ResizeBarVector = 7;
		}

		private void menuResize7_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (dockingManager.ResizeBarVector == 7);
			command.RadioCheck = (dockingManager.ResizeBarVector == 7);
		}

		private void menuShowCaptions_Click(object sender, System.EventArgs e)
		{
			captionBars = (captionBars == false);
        
			foreach(Content c in dockingManager.Contents)
				c.CaptionBar = captionBars;
		}

		private void menuShowCaptions_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = captionBars;
		}

		private void menuShowClose_Click(object sender, System.EventArgs e)
		{
			closeButtons = (closeButtons == false);
        
			foreach(Content c in dockingManager.Contents)
				c.CloseButton = closeButtons;
		}

		private void menuShowClose_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = closeButtons;
		}

		private void menuAllowAllClose_Click(object sender, System.EventArgs e)
		{
			ignoreClose	= 0;
		}

		private void menuAllowAllClose_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (ignoreClose == 0);
			command.RadioCheck = (ignoreClose == 0);
		}

		private void menuIgnoreTreeControlClose_Click(object sender, System.EventArgs e)
		{
			ignoreClose	= 1;
		}

		private void menuIgnoreTreeControlClose_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (ignoreClose == 1);
			command.RadioCheck = (ignoreClose == 1);
		}

		private void menuIgnoreTextBoxClose_Click(object sender, System.EventArgs e)
		{
			ignoreClose	= 2;
		}

		private void menuIgnoreTextBoxClose_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (ignoreClose == 2);
			command.RadioCheck = (ignoreClose == 2);
		}

		private void menuIgnoreExampleFormClose_Click(object sender, System.EventArgs e)
		{
			ignoreClose	= 3;
		}

		private void menuIgnoreExampleFormClose_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (ignoreClose == 3);
			command.RadioCheck = (ignoreClose == 3);
		}

		private void menuIgnore_Click(object sender, System.EventArgs e)
		{
			ignoreClose	= 4;
		}

		private void menuIgnore_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (ignoreClose == 4);
			command.RadioCheck = (ignoreClose == 4);
		}

		private void menuMinMax_Click(object sender, System.EventArgs e)
		{
			dockingManager.ZoneMinMax = (dockingManager.ZoneMinMax == false);
		}

		private void menuMinMax_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = dockingManager.ZoneMinMax;
		}

		private void menuFocusRectangles_Click(object sender, System.EventArgs e)
		{
			dockingManager.FeedbackStyle = DragFeedbackStyle.Outline;		
		}

		private void menuFocusRectangles_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (dockingManager.FeedbackStyle == DragFeedbackStyle.Outline);	
		}

		private void menuSemiBlocks_Click(object sender, System.EventArgs e)
		{
			dockingManager.FeedbackStyle = DragFeedbackStyle.Solid;		
		}

		private void menuSemiBlocks_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (dockingManager.FeedbackStyle == DragFeedbackStyle.Solid);	
		}

		private void menuDiamonds_Click(object sender, System.EventArgs e)
		{
			dockingManager.FeedbackStyle = DragFeedbackStyle.Diamonds;		
		}

		private void menuDiamonds_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (dockingManager.FeedbackStyle == DragFeedbackStyle.Diamonds);	
		}

		private void menuSquares_Click(object sender, System.EventArgs e)
		{
			dockingManager.FeedbackStyle = DragFeedbackStyle.Squares;		
		}

		private void menuSquares_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (dockingManager.FeedbackStyle == DragFeedbackStyle.Squares);	
		}

		private void OnSaveConfig(XmlTextWriter xmlOut)
		{
			// Add an extra node into the config to store some useless information
			xmlOut.WriteStartElement("MyCustomElement");
			xmlOut.WriteAttributeString("UselessData1", "Hello");
			xmlOut.WriteAttributeString("UselessData2", "World!");
			xmlOut.WriteEndElement();
		}

		private void OnLoadConfig(XmlTextReader xmlIn)
		{
			// We are expecting our custom element to be the current one
			if (xmlIn.Name == "MyCustomElement")
			{
				// Read in both the expected attributes
				string attr1 = xmlIn.GetAttribute(0);
				string attr2 = xmlIn.GetAttribute(1);
            
				// Must move past our element
				xmlIn.Read();
			}
		}

		protected void OnContentShown(Content c, EventArgs cea)
		{
			Console.WriteLine("OnContentShown {0}", c.Title);
		}

		protected void OnContentHidden(Content c, EventArgs cea)
		{
			Console.WriteLine("OnContentHidden {0}", c.Title);
		}
    
		protected void OnContentHiding(Content c, CancelEventArgs cea)
		{
			Console.WriteLine("OnContentHiding {0}", c.Title);
    
			switch(ignoreClose)
			{
				case 0:
					// Allow all, do nothing
					break;
				case 1:
					// Ignore TreeControl
					cea.Cancel = c.Control is TreeControl;
					break;
				case 2:
					// Ignore TextBox
					cea.Cancel = c.Control is TextBox;
					break;
				case 3:
					// Ignore ExampleForm
					cea.Cancel = c.Control is ExampleForm;
					break;
				case 4:
					// Ignore all, cancel
					cea.Cancel = true;
					break;
			}
		}

		private void OnContextMenu(PopupMenu pm, CancelEventArgs cea)
		{
			// Show the PopupMenu be cancelled and not shown?
			if (!allowContextMenu)
				cea.Cancel = true;
			else
			{        
				if (customContextMenu)
				{
					// Remove the Show All and Hide All commands
					pm.MenuCommands.Remove(pm.MenuCommands["Show All"]);
					pm.MenuCommands.Remove(pm.MenuCommands["Hide All"]);
                
					// Add a custom item at the start
					pm.MenuCommands.Insert(0, (new MenuCommand("Custom 1")));
					pm.MenuCommands.Insert(1, (new MenuCommand("-")));
                
					// Add a couple of custom commands at the end
					pm.MenuCommands.Add(new MenuCommand("Custom 2"));
					pm.MenuCommands.Add(new MenuCommand("Custom 3"));
				}
			}
		}

		private void InitializeContent(Content c)
		{
			c.CaptionBar = captionBars;
			c.CloseButton = closeButtons;
		}
	}
}
