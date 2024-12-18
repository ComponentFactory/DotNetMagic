/// *****************************************************************************
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
using Crownwood.DotNetMagic.Menus;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls;
using Crownwood.DotNetMagic.Controls.Command;
using Crownwood.DotNetMagic.Docking;

namespace SampleTabbedGroups
{
	/// <summary>
	/// Summary description for SampleTabbedGroups.
	/// </summary>
	public class SampleTabbedGroups : System.Windows.Forms.Form
	{
		// Internal state
	    private int _count = 1;
        private int _image = -1;
		private System.Windows.Forms.ImageList groupTabs;
		private Crownwood.DotNetMagic.Controls.TabbedGroups tabbedGroups1;
		private Crownwood.DotNetMagic.Controls.StatusBarControl statusBarControl1;
		private Crownwood.DotNetMagic.Controls.StatusPanel statusPanelText;
		private Crownwood.DotNetMagic.Controls.StatusPanel statusPanelDate;
		private Crownwood.DotNetMagic.Menus.MenuControl menuControl1;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuStyles;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuOffice2003;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuIDE;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuPlain;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator1;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuExit;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSetup;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuAddPage;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuRemovePage;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator2;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuInitSimple;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuInitMedium;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuInitComplex;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuHeader;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuHideAll;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuShowAll;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuShowActive;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuShowOver;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuShowActiveOver;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuActions;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuRebalance;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator3;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuProminent;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuProminentOnly;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator4;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuResizeThin;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuResizeMedium;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuResizeThick;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSeparator5;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuResizeLock;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuLayoutLock;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuFeedback;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuFocusRectangles;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSemiBlocks;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuDiamonds;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuSquares;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuIDE2005;
		private Crownwood.DotNetMagic.Menus.MenuCommand menuShowMultiple;
		private Crownwood.DotNetMagic.Controls.ToolControl toolControl1;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand buttonOffice2003;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand buttonIDE;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand buttonPlain;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand buttonAdd;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand buttonRemove;
		private Crownwood.DotNetMagic.Controls.Command.ButtonCommand buttonIDE2005;
		private Crownwood.DotNetMagic.Controls.Command.SeparatorCommand separatorCommand1;
		private System.Windows.Forms.Timer timer;
		private System.ComponentModel.IContainer components;

		public SampleTabbedGroups()
		{
			// Required for Windows Form Designer support
			InitializeComponent();
			
			// Create the initial setup			
			menuInitMedium_Click(this, EventArgs.Empty);

			// Update the date and time in status bar immediately
			timer_Tick(this, EventArgs.Empty);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SampleTabbedGroups));
			this.menuControl1 = new Crownwood.DotNetMagic.Menus.MenuControl();
			this.menuStyles = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuOffice2003 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuIDE2005 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuIDE = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuPlain = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator1 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuExit = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSetup = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuAddPage = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuRemovePage = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator2 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuInitSimple = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuInitMedium = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuInitComplex = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuHeader = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuHideAll = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuShowAll = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuShowActive = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuShowOver = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuShowActiveOver = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuShowMultiple = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuActions = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuRebalance = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator3 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuProminent = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuProminentOnly = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator4 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuResizeThin = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuResizeMedium = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuResizeThick = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSeparator5 = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuResizeLock = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuLayoutLock = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuFeedback = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuFocusRectangles = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSemiBlocks = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuDiamonds = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.menuSquares = new Crownwood.DotNetMagic.Menus.MenuCommand();
			this.groupTabs = new System.Windows.Forms.ImageList(this.components);
			this.tabbedGroups1 = new Crownwood.DotNetMagic.Controls.TabbedGroups();
			this.statusBarControl1 = new Crownwood.DotNetMagic.Controls.StatusBarControl();
			this.statusPanelText = new Crownwood.DotNetMagic.Controls.StatusPanel();
			this.statusPanelDate = new Crownwood.DotNetMagic.Controls.StatusPanel();
			this.toolControl1 = new Crownwood.DotNetMagic.Controls.ToolControl();
			this.buttonOffice2003 = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.buttonIDE2005 = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.buttonIDE = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.buttonPlain = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.separatorCommand1 = new Crownwood.DotNetMagic.Controls.Command.SeparatorCommand();
			this.buttonAdd = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.buttonRemove = new Crownwood.DotNetMagic.Controls.Command.ButtonCommand();
			this.timer = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.tabbedGroups1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolControl1)).BeginInit();
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
			this.menuControl1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.menuControl1.HighlightTextColor = System.Drawing.SystemColors.MenuText;
			this.menuControl1.IgnoreF10 = false;
			this.menuControl1.Location = new System.Drawing.Point(0, 0);
			this.menuControl1.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																									  this.menuStyles,
																									  this.menuSetup,
																									  this.menuHeader,
																									  this.menuFeedback,																								  
																									  this.menuActions});
			this.menuControl1.Name = "menuControl1";
			this.menuControl1.Size = new System.Drawing.Size(520, 25);
			this.menuControl1.Style = Crownwood.DotNetMagic.Common.VisualStyle.Office2003;
			this.menuControl1.TabIndex = 0;
			this.menuControl1.TabStop = false;
			this.menuControl1.Text = "menuControl1";
			this.menuControl1.Selected += new Crownwood.DotNetMagic.Menus.CommandHandler(this.menuControl1_Selected);
			this.menuControl1.Deselected += new Crownwood.DotNetMagic.Menus.CommandHandler(this.menuControl1_Deselected);
			// 
			// menuStyles
			// 
			this.menuStyles.Description = "Define the visual style";
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
			this.menuSeparator1.Description = "Menu";
			this.menuSeparator1.Text = "-";
			// 
			// menuExit
			// 
			this.menuExit.Description = "Use the Office2003 visual style";
			this.menuExit.Text = "Exit";
			this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
			// 
			// menuSetup
			// 
			this.menuSetup.Description = "Change the arrangement of pages";
			this.menuSetup.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																								   this.menuAddPage,
																								   this.menuRemovePage,
																								   this.menuSeparator2,
																								   this.menuInitSimple,
																								   this.menuInitMedium,
																								   this.menuInitComplex});
			this.menuSetup.Text = "Setup";
			// 
			// menuAddPage
			// 
			this.menuAddPage.Description = "Add a new page to the currently active position";
			this.menuAddPage.Text = "Add Page";
			this.menuAddPage.Click += new System.EventHandler(this.menuAddPage_Click);
			// 
			// menuRemovePage
			// 
			this.menuRemovePage.Description = "Remove the current active page";
			this.menuRemovePage.Text = "Remove Page";
			this.menuRemovePage.Click += new System.EventHandler(this.menuRemovePage_Click);
			this.menuRemovePage.Update += new System.EventHandler(this.menuRemovePage_Update);
			// 
			// menuSeparator2
			// 
			this.menuSeparator2.Description = "Menu";
			this.menuSeparator2.Text = "-";
			// 
			// menuInitSimple
			// 
			this.menuInitSimple.Description = "Reset the display to a simple arrangement";
			this.menuInitSimple.Text = "Initialize Simple";
			this.menuInitSimple.Click += new System.EventHandler(this.menuInitSimple_Click);
			// 
			// menuInitMedium
			// 
			this.menuInitMedium.Description = "Reset the display to a medium complexity arrangement";
			this.menuInitMedium.Text = "Initialize Medium";
			this.menuInitMedium.Click += new System.EventHandler(this.menuInitMedium_Click);
			// 
			// menuInitComplex
			// 
			this.menuInitComplex.Description = "Reset the display to a complex arrangement";
			this.menuInitComplex.Text = "Initialize Complex";
			this.menuInitComplex.Click += new System.EventHandler(this.menuInitComplex_Click);
			// 
			// menuHeader
			// 
			this.menuHeader.Description = "Define the Tab headers mode";
			this.menuHeader.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																									this.menuHideAll,
																									this.menuShowAll,
																									this.menuShowActive,
																									this.menuShowOver,
																									this.menuShowActiveOver,
																									this.menuShowMultiple});
			this.menuHeader.Text = "HeaderMode";
			// 
			// menuHideAll
			// 
			this.menuHideAll.Description = "Hide all the tab headers from view";
			this.menuHideAll.Text = "Hide All";
			this.menuHideAll.Click += new System.EventHandler(this.menuHideAll_Click);
			this.menuHideAll.Update += new System.EventHandler(this.menuHideAll_Update);
			// 
			// menuShowAll
			// 
			this.menuShowAll.Description = "Show all the tab headers";
			this.menuShowAll.Text = "Show All";
			this.menuShowAll.Click += new System.EventHandler(this.menuShowAll_Click);
			this.menuShowAll.Update += new System.EventHandler(this.menuShowAll_Update);
			// 
			// menuShowActive
			// 
			this.menuShowActive.Description = "Only show the tab header for the active group";
			this.menuShowActive.Text = "Show Active Leaf";
			this.menuShowActive.Click += new System.EventHandler(this.menuShowActive_Click);
			this.menuShowActive.Update += new System.EventHandler(this.menuShowActive_Update);
			// 
			// menuShowOver
			// 
			this.menuShowOver.Description = "Only show the tab header when the mouse is over the group";
			this.menuShowOver.Text = "Show Mouse Over";
			this.menuShowOver.Click += new System.EventHandler(this.menuShowOver_Click);
			this.menuShowOver.Update += new System.EventHandler(this.menuShowOver_Update);
			// 
			// menuShowActiveOver
			// 
			this.menuShowActiveOver.Description = "Show tab headers for the active group and any group with the mouse over";
			this.menuShowActiveOver.Text = "Show Active and Mouse Over";
			this.menuShowActiveOver.Click += new System.EventHandler(this.menuShowActiveOver_Click);
			this.menuShowActiveOver.Update += new System.EventHandler(this.menuShowActiveOver_Update);
			// 
			// menuShowMultiple
			// 
			this.menuShowMultiple.Description = "Show tab headers only when there is more than one tab in the group";
			this.menuShowMultiple.Text = "Show Only Multiple Tabs";
			this.menuShowMultiple.Click += new System.EventHandler(this.menuShowMultiple_Click);
			this.menuShowMultiple.Update += new System.EventHandler(this.menuShowMultiple_Update);
			// 
			// menuActions
			// 
			this.menuActions.Description = "Modify a selection of control properties";
			this.menuActions.MenuCommands.AddRange(new Crownwood.DotNetMagic.Menus.MenuCommand[] {
																									 this.menuRebalance,
																									 this.menuSeparator3,
																									 this.menuProminent,
																									 this.menuProminentOnly,
																									 this.menuSeparator4,
																									 this.menuResizeThin,
																									 this.menuResizeMedium,
																									 this.menuResizeThick,
																									 this.menuSeparator5,
																									 this.menuResizeLock,
																									 this.menuLayoutLock});
			this.menuActions.Text = "Actions";
			// 
			// menuRebalance
			// 
			this.menuRebalance.Description = "Alter the relative spacing of groups to being balanced";
			this.menuRebalance.Text = "Rebalance";
			this.menuRebalance.Click += new System.EventHandler(this.menuRebalance_Click);
			// 
			// menuSeparator3
			// 
			this.menuSeparator3.Description = "Menu";
			this.menuSeparator3.Text = "-";
			// 
			// menuProminent
			// 
			this.menuProminent.Description = "Toggle the current group between being prominent and not";
			this.menuProminent.Text = "Prominent";
			this.menuProminent.Click += new System.EventHandler(this.menuProminent_Click);
			this.menuProminent.Update += new System.EventHandler(this.menuProminent_Update);
			// 
			// menuProminentOnly
			// 
			this.menuProminentOnly.Description = "Should only the prominent group be shown when in prominent mode";
			this.menuProminentOnly.Text = "Prominent Group Only";
			this.menuProminentOnly.Click += new System.EventHandler(this.menuProminentOnly_Click);
			this.menuProminentOnly.Update += new System.EventHandler(this.menuProminentOnly_Update);
			// 
			// menuSeparator4
			// 
			this.menuSeparator4.Description = "Menu";
			this.menuSeparator4.Text = "-";
			// 
			// menuResizeThin
			// 
			this.menuResizeThin.Description = "Modify the size of resize bars between groups to be thin";
			this.menuResizeThin.Text = "Make Resize Bar Thin (2 pixels)";
			this.menuResizeThin.Click += new System.EventHandler(this.menuResizeThin_Click);
			// 
			// menuResizeMedium
			// 
			this.menuResizeMedium.Description = "Modify the size of resize bars between groups to be medium";
			this.menuResizeMedium.Text = "Make Resize Bar Medium (4 pixels)";
			this.menuResizeMedium.Click += new System.EventHandler(this.menuResizeMedium_Click);
			// 
			// menuResizeThick
			// 
			this.menuResizeThick.Description = "Modify the size of resize bars between groups to be thick";
			this.menuResizeThick.Text = "Make Resize Bar Thick (8 pixels)";
			this.menuResizeThick.Click += new System.EventHandler(this.menuResizeThick_Click);
			// 
			// menuSeparator5
			// 
			this.menuSeparator5.Description = "Menu";
			this.menuSeparator5.Text = "-";
			// 
			// menuResizeLock
			// 
			this.menuResizeLock.Description = "Determine if the user is allowed to resize the spacing of groups";
			this.menuResizeLock.Text = "ResizeLock";
			this.menuResizeLock.Click += new System.EventHandler(this.menuResizeLock_Click);
			this.menuResizeLock.Update += new System.EventHandler(this.menuResizeLock_Update);
			// 
			// menuLayoutLock
			// 
			this.menuLayoutLock.Description = "Determine if the user is allowed to change the layout of the groups";
			this.menuLayoutLock.Text = "LayoutLock";
			this.menuLayoutLock.Click += new System.EventHandler(this.menuLayoutLock_Click);
			this.menuLayoutLock.Update += new System.EventHandler(this.menuLayoutLock_Update);
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
			// groupTabs
			// 
			this.groupTabs.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.groupTabs.ImageSize = new System.Drawing.Size(16, 16);
			this.groupTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("groupTabs.ImageStream")));
			this.groupTabs.TransparentColor = System.Drawing.Color.Magenta;
			// 
			// tabbedGroups1
			// 
			this.tabbedGroups1.AllowDrop = true;
			this.tabbedGroups1.AtLeastOneLeaf = true;
			this.tabbedGroups1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabbedGroups1.ImageList = this.groupTabs;
			this.tabbedGroups1.Location = new System.Drawing.Point(0, 47);
			this.tabbedGroups1.Name = "tabbedGroups1";
			this.tabbedGroups1.ProminentLeaf = null;
			this.tabbedGroups1.ResizeBarColor = System.Drawing.SystemColors.Control;
			this.tabbedGroups1.Size = new System.Drawing.Size(520, 359);
			this.tabbedGroups1.TabIndex = 1;
			this.tabbedGroups1.ExternalDrop += new Crownwood.DotNetMagic.Controls.TabbedGroups.ExternalDropHandler(this.OnExternalDrop);
			this.tabbedGroups1.TabControlCreated += new Crownwood.DotNetMagic.Controls.TabbedGroups.TabControlCreatedHandler(this.OnTabControlCreated);
			// 
			// statusBarControl1
			// 
			this.statusBarControl1.Location = new System.Drawing.Point(0, 406);
			this.statusBarControl1.Name = "statusBarControl1";
			this.statusBarControl1.PadTop = 3;
			this.statusBarControl1.Size = new System.Drawing.Size(520, 24);
			this.statusBarControl1.StatusPanels.AddRange(new Crownwood.DotNetMagic.Controls.StatusPanel[] {
																											  this.statusPanelText,
																											  this.statusPanelDate});
			this.statusBarControl1.TabIndex = 3;
			// 
			// statusPanelText
			// 
			this.statusPanelText.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.statusPanelText.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.statusPanelText.AutoSizing = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusPanelText.Location = new System.Drawing.Point(2, 2);
			this.statusPanelText.Name = "statusPanelText";
			this.statusPanelText.Size = new System.Drawing.Size(492, 16);
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
			// toolControl1
			// 
			this.toolControl1.Commands.AddRange(new Crownwood.DotNetMagic.Controls.Command.CommandBase[] {
																											 this.buttonOffice2003,
																											 this.buttonIDE2005,
																											 this.buttonIDE,
																											 this.buttonPlain,
																											 this.separatorCommand1,
																											 this.buttonAdd,
																											 this.buttonRemove});
			this.toolControl1.Location = new System.Drawing.Point(0, 25);
			this.toolControl1.Name = "toolControl1";
			this.toolControl1.Padding.Bottom = 3;
			this.toolControl1.Padding.Top = 1;
			this.toolControl1.Size = new System.Drawing.Size(520, 22);
			this.toolControl1.TabIndex = 4;
			this.toolControl1.UpdateTimeout = 100;
			// 
			// buttonOffice2003
			// 
			this.buttonOffice2003.ButtonStyle = Crownwood.DotNetMagic.Common.ButtonStyle.ToggleButton;
			this.buttonOffice2003.Tag = null;
			this.buttonOffice2003.Text = "Office2003";
			this.buttonOffice2003.Tooltip = "Use the Office2003 visual style";
			this.buttonOffice2003.Update += new System.EventHandler(this.buttonOffice2003_Update);
			this.buttonOffice2003.Click += new System.EventHandler(this.buttonOffice2003_Click);
			// 
			// buttonIDE2005
			// 
			this.buttonIDE2005.ButtonStyle = Crownwood.DotNetMagic.Common.ButtonStyle.ToggleButton;
			this.buttonIDE2005.Tag = null;
			this.buttonIDE2005.Text = "IDE2005";
			this.buttonIDE2005.Tooltip = "Use the IDE2005 visual style";
			this.buttonIDE2005.Update += new System.EventHandler(this.buttonIDE2005_Update);
			this.buttonIDE2005.Click += new System.EventHandler(this.buttonIDE2005_Click);
			// 
			// buttonIDE
			// 
			this.buttonIDE.ButtonStyle = Crownwood.DotNetMagic.Common.ButtonStyle.ToggleButton;
			this.buttonIDE.Tag = null;
			this.buttonIDE.Text = "IDE";
			this.buttonIDE.Tooltip = "Use the IDE visual style";
			this.buttonIDE.Update += new System.EventHandler(this.buttonIDE_Update);
			this.buttonIDE.Click += new System.EventHandler(this.buttonIDE_Click);
			// 
			// buttonPlain
			// 
			this.buttonPlain.ButtonStyle = Crownwood.DotNetMagic.Common.ButtonStyle.ToggleButton;
			this.buttonPlain.Tag = null;
			this.buttonPlain.Text = "Plain";
			this.buttonPlain.Tooltip = "Use the Plain visual style";
			this.buttonPlain.Update += new System.EventHandler(this.buttonPlain_Update);
			this.buttonPlain.Click += new System.EventHandler(this.buttonPlain_Click);
			// 
			// separatorCommand1
			// 
			this.separatorCommand1.Tag = null;
			// 
			// buttonAdd
			// 
			this.buttonAdd.Tag = null;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.Tooltip = "Add a new page";
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Tag = null;
			this.buttonRemove.Text = "Remove";
			this.buttonRemove.Tooltip = "Remove the current page";
			this.buttonRemove.Update += new System.EventHandler(this.buttonRemove_Update);
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 900;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// SampleTabbedGroups
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 430);
			this.Controls.Add(this.tabbedGroups1);
			this.Controls.Add(this.toolControl1);
			this.Controls.Add(this.menuControl1);
			this.Controls.Add(this.statusBarControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SampleTabbedGroups";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "DotNetMagic - SampleTabbedGroups";
			((System.ComponentModel.ISupportInitialize)(this.tabbedGroups1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolControl1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new SampleTabbedGroups());
		}

		private int NextImage()
		{
			_image = ++_image % 7;
			return _image;
		}

		
		private void ChangeStyle(VisualStyle style)
		{
			// Update each of the contained elements
			menuControl1.Style = style;
			toolControl1.Style = style;
			statusBarControl1.Style = style;
			tabbedGroups1.Style = style;
			
			TabGroupLeaf leaf = tabbedGroups1.FirstLeaf();
			while(leaf != null)
			{
				leaf.TabControl.ShowArrows = (style != VisualStyle.IDE2005);
				leaf.TabControl.ShowDropSelect = (style == VisualStyle.IDE2005); 
				leaf = tabbedGroups1.NextLeaf(leaf);
			}
		}

		private Crownwood.DotNetMagic.Controls.TabPage NewTabPage()
		{
			RichTextBox rtb = new RichTextBox();
			rtb.BorderStyle = BorderStyle.None;
			
			Crownwood.DotNetMagic.Controls.TabPage newPage = new Crownwood.DotNetMagic.Controls.TabPage("Page" + _count++, rtb, NextImage());
			newPage.Selected = true;
			
			return newPage;       
		}

		private void OnExternalDrop(Crownwood.DotNetMagic.Controls.TabbedGroups tg, 
									Crownwood.DotNetMagic.Controls.TabGroupLeaf tgl,
									Crownwood.DotNetMagic.Controls.TabControl tc,
									Crownwood.DotNetMagic.Controls.TabbedGroups.DragProvider dp)
        {
            // Create a new tab page
            Crownwood.DotNetMagic.Controls.TabPage tp = NewTabPage();
            
            // Define the text in this control
            (tp.Control as RichTextBox).Text = "Dragged from node '" + (string)dp.Tag + "'";
            
            // We want the new page to become selected
            tp.Selected = true;
            
            // Add new page into the destination tab control
            tgl.TabPages.Add(tp);
        }

		private void OnTabControlCreated(Crownwood.DotNetMagic.Controls.TabbedGroups tg, Crownwood.DotNetMagic.Controls.TabControl tc)
		{
			// Place a thin border between edge of the tab control and inside contents
			tc.ControlTopOffset = 3;			
			tc.ControlBottomOffset = 3;			
			tc.ControlLeftOffset = 3;			
			tc.ControlRightOffset = 3;			
		}
		
		private void timer_Tick(object sender, System.EventArgs e)
		{
			// Update the date and time section of the status bar
			statusPanelDate.Text = DateTime.Now.ToShortDateString();
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
		
		private void buttonOffice2003_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.Office2003);		
		}

		private void buttonOffice2003_Update(object sender, System.EventArgs e)
		{
			ButtonCommand bc = sender as ButtonCommand;
			bc.ButtonStyle = ButtonStyle.ToggleButton;
			bc.Pushed = (menuControl1.Style == VisualStyle.Office2003);		
		}

		private void buttonIDE2005_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.IDE2005);		
		}

		private void buttonIDE2005_Update(object sender, System.EventArgs e)
		{
			ButtonCommand bc = sender as ButtonCommand;
			bc.ButtonStyle = ButtonStyle.ToggleButton;
			bc.Pushed = (menuControl1.Style == VisualStyle.IDE2005);		
		}

		private void buttonIDE_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.IDE);
		}

		private void buttonIDE_Update(object sender, System.EventArgs e)
		{
			ButtonCommand bc = sender as ButtonCommand;
			bc.ButtonStyle = ButtonStyle.ToggleButton;
			bc.Pushed = (menuControl1.Style == VisualStyle.IDE);		
		}

		private void buttonPlain_Click(object sender, System.EventArgs e)
		{
			ChangeStyle(VisualStyle.Plain);
		}

		private void buttonPlain_Update(object sender, System.EventArgs e)
		{
			ButtonCommand bc = sender as ButtonCommand;
			bc.ButtonStyle = ButtonStyle.ToggleButton;
			bc.Pushed = (menuControl1.Style == VisualStyle.Plain);
		}

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

		private void menuAddPage_Click(object sender, System.EventArgs e)
		{
			if (tabbedGroups1.ActiveLeaf != null)
				tabbedGroups1.ActiveLeaf.TabPages.Add(NewTabPage());
		}

		private void buttonAdd_Click(object sender, System.EventArgs e)
		{
			menuAddPage_Click(this, EventArgs.Empty);
		}

		private void menuRemovePage_Click(object sender, System.EventArgs e)
		{
			Crownwood.DotNetMagic.Controls.TabControl tc = null;

			if (tabbedGroups1.ActiveLeaf != null)
				tc = tabbedGroups1.ActiveLeaf.GroupControl as Crownwood.DotNetMagic.Controls.TabControl;
            
			// Did we find a current tab control?
			if (tc != null)
			{
				// Does it have a selected tab?
				if (tc.SelectedTab != null)
				{
					// Remove the page
					tc.TabPages.Remove(tc.SelectedTab);
				}
			}
		}

		private void menuRemovePage_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
		
			Crownwood.DotNetMagic.Controls.TabControl tc = null;

			if (tabbedGroups1.ActiveLeaf != null)
				tc = tabbedGroups1.ActiveLeaf.GroupControl as Crownwood.DotNetMagic.Controls.TabControl;

			// Did we find a current tab control?
			if ((tc != null) && (tc.SelectedTab != null))
				command.Enabled = true;
			else
				command.Enabled = false;
		}

		private void buttonRemove_Click(object sender, System.EventArgs e)
		{
			menuRemovePage_Click(this, EventArgs.Empty);
		}

		private void buttonRemove_Update(object sender, System.EventArgs e)
		{
			ButtonCommand command = sender as ButtonCommand;
		
			Crownwood.DotNetMagic.Controls.TabControl tc = null;

			if (tabbedGroups1.ActiveLeaf != null)
				tc = tabbedGroups1.ActiveLeaf.GroupControl as Crownwood.DotNetMagic.Controls.TabControl;

			// Did we find a current tab control?
			if ((tc != null) && (tc.SelectedTab != null))
				command.Enabled = true;
			else
				command.Enabled = false;
		}

		private void menuInitSimple_Click(object sender, System.EventArgs e)
		{
			// Remove all existing content
			tabbedGroups1.RootSequence.Clear();
			
			// Switch to ordering horizontal
			tabbedGroups1.RootDirection = LayoutDirection.Horizontal;

			// Reset count back again
			_count = 1;
			_image = -1;

			// Access the default leaf group
			TabGroupLeaf tgl1 = tabbedGroups1.RootSequence[0] as TabGroupLeaf;
            
			// Add a new leaf group in the same sequence
			TabGroupLeaf tgl2 = tabbedGroups1.RootSequence.AddNewLeaf();
            
			// Add a two pages to the leaf
			tgl1.TabPages.Add(NewTabPage());
			tgl2.TabPages.Add(NewTabPage());

			// Setup spacing
			tgl1.Space = 60;
			tgl2.Space = 40;

			// Ask control to reposition children according to new spacing
			tabbedGroups1.RootSequence.Reposition();
		}

		private void menuInitMedium_Click(object sender, System.EventArgs e)
		{
			// Remove all existing content
			tabbedGroups1.RootSequence.Clear();

			// Switch to ordering horizontal
			tabbedGroups1.RootDirection = LayoutDirection.Horizontal;

			// Reset count back again
			_count = 0;
			_image = -1;

			// Access the default leaf group
			TabGroupLeaf tgl1 = tabbedGroups1.RootSequence[0] as TabGroupLeaf;
            
			// Add a new sequence to end of root
			TabGroupSequence tgs = tabbedGroups1.RootSequence.AddNewSequence();
            
			// Add two leafs into the new sequence
			TabGroupLeaf tgl2 = tgs.AddNewLeaf() as TabGroupLeaf;
			TabGroupLeaf tgl3 = tgs.AddNewLeaf() as TabGroupLeaf;
            
			// Add a page two each tab leaf
			tgl1.TabPages.Add(NewTabPage());
			tgl2.TabPages.Add(NewTabPage());
			tgl3.TabPages.Add(NewTabPage());
			
			// Setup spacing
			tgl1.Space = 35;
			tgs.Space = 65;
			tgl2.Space = 30;
			tgl3.Space = 70;

			// Ask control to reposition children according to new spacing
			tabbedGroups1.RootSequence.Reposition();
		}

		private void menuInitComplex_Click(object sender, System.EventArgs e)
		{
			// Remove all existing content
			tabbedGroups1.RootSequence.Clear();

			// Reset count back again
			_count = 0;
			_image = -1;
			
			// Switch to ordering vertical
			tabbedGroups1.RootDirection = LayoutDirection.Vertical;

			// Access the default leaf group
			TabGroupLeaf tgl1 = tabbedGroups1.RootSequence[0] as TabGroupLeaf;
            
			// Add a new sequence to end of root
			TabGroupSequence tgs1 = tabbedGroups1.RootSequence.AddNewSequence();
			TabGroupSequence tgs2 = tabbedGroups1.RootSequence.AddNewSequence();
            
			// Add two leafs into the first sequence
			TabGroupLeaf tgl2 = tgs1.AddNewLeaf() as TabGroupLeaf;
			TabGroupLeaf tgl3 = tgs1.AddNewLeaf() as TabGroupLeaf;
            
			// Add three leafs into the second sequence
			TabGroupLeaf tgl4 = tgs2.AddNewLeaf() as TabGroupLeaf;
			TabGroupLeaf tgl5 = tgs2.AddNewLeaf() as TabGroupLeaf;
			TabGroupLeaf tgl6 = tgs2.AddNewLeaf() as TabGroupLeaf;

			// Add a page two each tab leaf and two pages to some of them
			tgl1.TabPages.Add(NewTabPage());
			tgl2.TabPages.Add(NewTabPage());
			tgl2.TabPages.Add(NewTabPage());
			tgl3.TabPages.Add(NewTabPage());
			tgl3.TabPages.Add(NewTabPage());
			tgl4.TabPages.Add(NewTabPage());
			tgl5.TabPages.Add(NewTabPage());
			tgl6.TabPages.Add(NewTabPage());
			
			// Setup spacing
			tgl1.Space = 20;
			tgs1.Space = 30;
			tgs2.Space = 50;

			// Ask control to reposition children according to new spacing
			tabbedGroups1.RootSequence.Reposition();
		}

		private void menuHideAll_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.DisplayTabMode = DisplayTabModes.HideAll;
		}

		private void menuHideAll_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.RadioCheck = (tabbedGroups1.DisplayTabMode == DisplayTabModes.HideAll);
			command.Checked = (tabbedGroups1.DisplayTabMode == DisplayTabModes.HideAll);
		}

		private void menuShowAll_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.DisplayTabMode = DisplayTabModes.ShowAll;
		}

		private void menuShowAll_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.RadioCheck = (tabbedGroups1.DisplayTabMode == DisplayTabModes.ShowAll);
			command.Checked = (tabbedGroups1.DisplayTabMode == DisplayTabModes.ShowAll);
		}

		private void menuShowActive_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.DisplayTabMode = DisplayTabModes.ShowActiveLeaf;
		}

		private void menuShowActive_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.RadioCheck = (tabbedGroups1.DisplayTabMode == DisplayTabModes.ShowActiveLeaf);
			command.Checked = (tabbedGroups1.DisplayTabMode == DisplayTabModes.ShowActiveLeaf);
		}

		private void menuShowOver_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.DisplayTabMode = DisplayTabModes.ShowMouseOver;
		}

		private void menuShowOver_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.RadioCheck = (tabbedGroups1.DisplayTabMode == DisplayTabModes.ShowMouseOver);
			command.Checked = (tabbedGroups1.DisplayTabMode == DisplayTabModes.ShowMouseOver);
		}

		private void menuShowActiveOver_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.DisplayTabMode = DisplayTabModes.ShowActiveAndMouseOver;
		}

		private void menuShowActiveOver_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.RadioCheck = (tabbedGroups1.DisplayTabMode == DisplayTabModes.ShowActiveAndMouseOver);
			command.Checked = (tabbedGroups1.DisplayTabMode == DisplayTabModes.ShowActiveAndMouseOver);
		}

		private void menuShowMultiple_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.DisplayTabMode = DisplayTabModes.ShowOnlyMultipleTabs;
		}

		private void menuShowMultiple_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.RadioCheck = (tabbedGroups1.DisplayTabMode == DisplayTabModes.ShowOnlyMultipleTabs);
			command.Checked = (tabbedGroups1.DisplayTabMode == DisplayTabModes.ShowOnlyMultipleTabs);
		}

		private void menuRebalance_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.Rebalance();
		}

		private void menuProminent_Click(object sender, System.EventArgs e)
		{
			if (tabbedGroups1.ProminentLeaf != null)
				tabbedGroups1.ProminentLeaf = null;
			else
			{
				if (tabbedGroups1.ActiveLeaf != null)
					tabbedGroups1.ProminentLeaf = tabbedGroups1.ActiveLeaf;
			}
		}

		private void menuProminent_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Enabled = (tabbedGroups1.ActiveLeaf != null);
			command.Checked = (tabbedGroups1.ProminentLeaf != null);
		}

		private void menuProminentOnly_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.ProminentOnly = !tabbedGroups1.ProminentOnly;
		}

		private void menuProminentOnly_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = tabbedGroups1.ProminentOnly;
		}

		private void menuResizeThin_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.ResizeBarVector = 2;
		}

		private void menuResizeMedium_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.ResizeBarVector = 4;
		}

		private void menuResizeThick_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.ResizeBarVector = 8;
		}

		private void menuResizeLock_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.ResizeBarLock = !tabbedGroups1.ResizeBarLock;
		}

		private void menuResizeLock_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = tabbedGroups1.ResizeBarLock;
		}

		private void menuLayoutLock_Click(object sender, System.EventArgs e)
		{
            tabbedGroups1.LayoutLock = !tabbedGroups1.LayoutLock;
		}

		private void menuLayoutLock_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = tabbedGroups1.LayoutLock;
		}

		private void menuFocusRectangles_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.FeedbackStyle = DragFeedbackStyle.Outline;		
		}

		private void menuFocusRectangles_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (tabbedGroups1.FeedbackStyle == DragFeedbackStyle.Outline);	
		}

		private void menuSemiBlocks_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.FeedbackStyle = DragFeedbackStyle.Solid;		
		}

		private void menuSemiBlocks_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (tabbedGroups1.FeedbackStyle == DragFeedbackStyle.Solid);	
		}

		private void menuDiamonds_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.FeedbackStyle = DragFeedbackStyle.Diamonds;		
		}

		private void menuDiamonds_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (tabbedGroups1.FeedbackStyle == DragFeedbackStyle.Diamonds);	
		}

		private void menuSquares_Click(object sender, System.EventArgs e)
		{
			tabbedGroups1.FeedbackStyle = DragFeedbackStyle.Squares;		
		}

		private void menuSquares_Update(object sender, System.EventArgs e)
		{
			MenuCommand command = sender as MenuCommand;
			command.Checked = (tabbedGroups1.FeedbackStyle == DragFeedbackStyle.Squares);	
		}
	}
}
