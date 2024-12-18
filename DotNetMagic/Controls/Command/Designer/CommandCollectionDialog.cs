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
using System.Windows.Forms;
using Crownwood.DotNetMagic.Win32;
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls.Command;

namespace Crownwood.DotNetMagic.Controls.Command
{
	/// <summary>
	/// Form used to edit a command collection.
	/// </summary>
	public class CommandCollectionDialog : Form
	{
		private Crownwood.DotNetMagic.Controls.TreeControl treeControl1;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.Button buttonAddButton;
		private System.Windows.Forms.Button buttonAddSeparator;
		private System.Windows.Forms.Button buttonMoveUp;
		private System.Windows.Forms.Button buttonMoveDown;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonClearAll;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.MenuItem menuAddButton;
		private System.Windows.Forms.MenuItem menuAddSeparator;
		private System.Windows.Forms.MenuItem menuSep1;
		private System.Windows.Forms.MenuItem menuMoveUp;
		private System.Windows.Forms.MenuItem menuMoveDown;
		private System.Windows.Forms.MenuItem menuSep2;
		private System.Windows.Forms.MenuItem menuRemove;
		private System.Windows.Forms.MenuItem menuSep3;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.Label labelCollection;
		private System.Windows.Forms.Label labelProperties;
		private System.Windows.Forms.MenuItem menuClearAll;
		
		/// <summary>
		/// Initializes a new instance of the NodeCollectionDialog class.
		/// </summary>
        public CommandCollectionDialog()
        {
			// Required for Windows Form Designer support
			InitializeComponent();
		}

		/// <summary>
		/// Initializes a new instance of the NodeCollectionDialog class.
		/// </summary>
		/// <param name="original">Original Nodes to be edited.</param>
		public CommandCollectionDialog(CommandBaseCollection original)
		{
			// Required for Windows Form Designer support
			InitializeComponent();
			
			// Create a new per command
			foreach(CommandBase command in original)
			{
				// Create a new tree node to hold the command
				Node newNode = new Node();
				
				// Create a copy to attach to the node
				CommandBase copy = (CommandBase)command.Clone();

				if (copy.GetType() == typeof(SeparatorCommand))
					newNode.Text = "(Separator)";
				else if (copy.GetType() == typeof(ButtonCommand))
				{
					ButtonCommand button = copy as ButtonCommand;

					// Special case the absense of text
					if (button.Text.Length == 0)
						newNode.Text = "<Empty>";
					else
						newNode.Text = button.Text;
					
					// We want to know when the text for the node changes
					button.TextChanged += new EventHandler(OnTextChanged);
				}
			
				// Attached a copy of the command to the node
				newNode.Tag = copy;
			
				// Append to end of the list
				treeControl1.Nodes.Add(newNode);
			}
			
			// Set correct initial state of the buttons
			UpdateButtonState();
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the class.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged; false to release only unmanaged. </param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Remove any event handlers
				foreach(Node n in treeControl1.Nodes)
				{
					ButtonCommand button = n.Tag as ButtonCommand;
			
					// Remove any handler from the command
					if (button != null)
						button.TextChanged -= new EventHandler(OnTextChanged);
				}
			}

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.treeControl1 = new Crownwood.DotNetMagic.Controls.TreeControl();
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.menuAddButton = new System.Windows.Forms.MenuItem();
			this.menuAddSeparator = new System.Windows.Forms.MenuItem();
			this.menuSep1 = new System.Windows.Forms.MenuItem();
			this.menuMoveUp = new System.Windows.Forms.MenuItem();
			this.menuMoveDown = new System.Windows.Forms.MenuItem();
			this.menuSep2 = new System.Windows.Forms.MenuItem();
			this.menuRemove = new System.Windows.Forms.MenuItem();
			this.menuSep3 = new System.Windows.Forms.MenuItem();
			this.menuClearAll = new System.Windows.Forms.MenuItem();
			this.labelCollection = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonAddButton = new System.Windows.Forms.Button();
			this.buttonAddSeparator = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.buttonClearAll = new System.Windows.Forms.Button();
			this.buttonMoveUp = new System.Windows.Forms.Button();
			this.buttonMoveDown = new System.Windows.Forms.Button();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.labelProperties = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// treeControl1
			// 
			this.treeControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.treeControl1.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.treeControl1.BorderStyle = Crownwood.DotNetMagic.Controls.TreeBorderStyle.Solid;
			this.treeControl1.BoxDrawStyle = Crownwood.DotNetMagic.Controls.DrawStyle.Plain;
			this.treeControl1.CheckDrawStyle = Crownwood.DotNetMagic.Controls.DrawStyle.Plain;
			this.treeControl1.ContextMenuNode = this.contextMenu;
			this.treeControl1.ContextMenuSpace = this.contextMenu;
			this.treeControl1.GroupFont = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.treeControl1.HotBackColor = System.Drawing.Color.Empty;
			this.treeControl1.HotForeColor = System.Drawing.Color.Empty;
			this.treeControl1.Location = new System.Drawing.Point(120, 32);
			this.treeControl1.Name = "treeControl1";
			this.treeControl1.SelectedNode = null;
			this.treeControl1.SelectedNoFocusBackColor = System.Drawing.SystemColors.Control;
			this.treeControl1.SelectMode = Crownwood.DotNetMagic.Controls.SelectMode.Single;
			this.treeControl1.Size = new System.Drawing.Size(200, 344);
			this.treeControl1.TabIndex = 0;
			this.treeControl1.Text = "treeControl1";
			this.treeControl1.AfterSelect += new Crownwood.DotNetMagic.Controls.NodeEventHandler(this.treeControl1_AfterSelect);
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.menuAddButton,
																						this.menuAddSeparator,
																						this.menuSep1,
																						this.menuMoveUp,
																						this.menuMoveDown,
																						this.menuSep2,
																						this.menuRemove,
																						this.menuSep3,
																						this.menuClearAll});
			this.contextMenu.Popup += new System.EventHandler(this.contextMenu_Popup);
			// 
			// menuAddButton
			// 
			this.menuAddButton.Index = 0;
			this.menuAddButton.Text = "Add Button";
			this.menuAddButton.Click += new System.EventHandler(this.buttonAddButton_Click);
			// 
			// menuAddSeparator
			// 
			this.menuAddSeparator.Index = 1;
			this.menuAddSeparator.Text = "Add Separator";
			this.menuAddSeparator.Click += new System.EventHandler(this.buttonAddSeparator_Click);
			// 
			// menuSep1
			// 
			this.menuSep1.Index = 2;
			this.menuSep1.Text = "-";
			// 
			// menuMoveUp
			// 
			this.menuMoveUp.Index = 3;
			this.menuMoveUp.Text = "Move Up";
			this.menuMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
			// 
			// menuMoveDown
			// 
			this.menuMoveDown.Index = 4;
			this.menuMoveDown.Text = "Move Down";
			this.menuMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
			// 
			// menuSep2
			// 
			this.menuSep2.Index = 5;
			this.menuSep2.Text = "-";
			// 
			// menuRemove
			// 
			this.menuRemove.Index = 6;
			this.menuRemove.Text = "Remove";
			this.menuRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// menuSep3
			// 
			this.menuSep3.Index = 7;
			this.menuSep3.Text = "-";
			// 
			// menuClearAll
			// 
			this.menuClearAll.Index = 8;
			this.menuClearAll.Text = "Clear All";
			this.menuClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
			// 
			// labelCollection
			// 
			this.labelCollection.Location = new System.Drawing.Point(120, 8);
			this.labelCollection.Name = "labelCollection";
			this.labelCollection.Size = new System.Drawing.Size(112, 23);
			this.labelCollection.TabIndex = 1;
			this.labelCollection.Text = "Command Collection";
			this.labelCollection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonOK.Location = new System.Drawing.Point(480, 388);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonCancel.Location = new System.Drawing.Point(392, 388);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonAddButton
			// 
			this.buttonAddButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonAddButton.Location = new System.Drawing.Point(16, 32);
			this.buttonAddButton.Name = "buttonAddButton";
			this.buttonAddButton.Size = new System.Drawing.Size(88, 23);
			this.buttonAddButton.TabIndex = 6;
			this.buttonAddButton.Text = "Add Button";
			this.buttonAddButton.Click += new System.EventHandler(this.buttonAddButton_Click);
			// 
			// buttonAddSeparator
			// 
			this.buttonAddSeparator.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonAddSeparator.Location = new System.Drawing.Point(16, 64);
			this.buttonAddSeparator.Name = "buttonAddSeparator";
			this.buttonAddSeparator.Size = new System.Drawing.Size(88, 23);
			this.buttonAddSeparator.TabIndex = 7;
			this.buttonAddSeparator.Text = "Add Separator";
			this.buttonAddSeparator.Click += new System.EventHandler(this.buttonAddSeparator_Click);
			// 
			// buttonRemove
			// 
			this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonRemove.Location = new System.Drawing.Point(16, 176);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(88, 23);
			this.buttonRemove.TabIndex = 9;
			this.buttonRemove.Text = "Remove";
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// buttonClearAll
			// 
			this.buttonClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClearAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonClearAll.Location = new System.Drawing.Point(16, 352);
			this.buttonClearAll.Name = "buttonClearAll";
			this.buttonClearAll.Size = new System.Drawing.Size(88, 23);
			this.buttonClearAll.TabIndex = 10;
			this.buttonClearAll.Text = "Clear All";
			this.buttonClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
			// 
			// buttonMoveUp
			// 
			this.buttonMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonMoveUp.Location = new System.Drawing.Point(16, 104);
			this.buttonMoveUp.Name = "buttonMoveUp";
			this.buttonMoveUp.Size = new System.Drawing.Size(88, 23);
			this.buttonMoveUp.TabIndex = 11;
			this.buttonMoveUp.Text = "Move Up";
			this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
			// 
			// buttonMoveDown
			// 
			this.buttonMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonMoveDown.Location = new System.Drawing.Point(16, 136);
			this.buttonMoveDown.Name = "buttonMoveDown";
			this.buttonMoveDown.Size = new System.Drawing.Size(88, 23);
			this.buttonMoveDown.TabIndex = 12;
			this.buttonMoveDown.Text = "Move Down";
			this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
			// 
			// propertyGrid
			// 
			this.propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid.CommandsVisibleIfAvailable = true;
			this.propertyGrid.HelpVisible = false;
			this.propertyGrid.LargeButtons = false;
			this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid.Location = new System.Drawing.Point(336, 32);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.propertyGrid.Size = new System.Drawing.Size(219, 344);
			this.propertyGrid.TabIndex = 13;
			this.propertyGrid.Text = "propertyGrid";
			this.propertyGrid.ToolbarVisible = false;
			this.propertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// labelProperties
			// 
			this.labelProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelProperties.Location = new System.Drawing.Point(336, 8);
			this.labelProperties.Name = "labelProperties";
			this.labelProperties.Size = new System.Drawing.Size(112, 23);
			this.labelProperties.TabIndex = 14;
			this.labelProperties.Text = "Command Properties";
			this.labelProperties.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CommandCollectionDialog
			// 
			this.ClientSize = new System.Drawing.Size(568, 430);
			this.ControlBox = false;
			this.Controls.Add(this.labelProperties);
			this.Controls.Add(this.propertyGrid);
			this.Controls.Add(this.buttonMoveDown);
			this.Controls.Add(this.buttonMoveUp);
			this.Controls.Add(this.buttonClearAll);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.buttonAddSeparator);
			this.Controls.Add(this.buttonAddButton);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.labelCollection);
			this.Controls.Add(this.treeControl1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(480, 328);
			this.Name = "CommandCollectionDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Command Collection Editor";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Gets the collection of edited nodes.
		/// </summary>
		public CommandBaseCollection Commands
		{
			get 
			{
				// Create new collection for returning commands
				CommandBaseCollection ret = new CommandBaseCollection();
				
				// Add each command from the tree
				foreach(Node n in treeControl1.Nodes)
					ret.Add(n.Tag as CommandBase);
					
				return ret;
			}
		}

		private void UpdateButtonState()
		{
			bool nodeSelected = (treeControl1.SelectedNode != null);

			buttonAddButton.Enabled = true;
			buttonAddSeparator.Enabled = true;
			buttonMoveUp.Enabled = nodeSelected && (treeControl1.SelectedNode.Index > 0);
			buttonMoveDown.Enabled = nodeSelected && (treeControl1.SelectedNode.Index < (treeControl1.Nodes.Count - 1));
			buttonRemove.Enabled = nodeSelected;
			buttonClearAll.Enabled = (treeControl1.Nodes.Count > 0);
		}

		private void contextMenu_Popup(object sender, System.EventArgs e)
		{
			// Is there  a node selected?
			bool nodeSelected = (treeControl1.SelectedNode != null);

			menuAddButton.Enabled = true;
			menuAddSeparator.Enabled = true;
			menuMoveUp.Enabled = nodeSelected && (treeControl1.SelectedNode.Index > 0);
			menuMoveDown.Enabled = nodeSelected && (treeControl1.SelectedNode.Index < (treeControl1.Nodes.Count - 1));
			menuRemove.Enabled = nodeSelected;
			menuClearAll.Enabled = (treeControl1.Nodes.Count > 0);
		}

		private void treeControl1_AfterSelect(Crownwood.DotNetMagic.Controls.TreeControl tc, Crownwood.DotNetMagic.Controls.NodeEventArgs e)
		{
			// Update the properties control with the newly selected control
			propertyGrid.SelectedObject = treeControl1.SelectedNode.Tag;
			UpdateButtonState();
		}

		private void buttonAddButton_Click(object sender, System.EventArgs e)
		{
			// Create a new button instance
			ButtonCommand newButton = new ButtonCommand();
			
			// Hook into changes of the button text
			newButton.TextChanged += new EventHandler(OnTextChanged);
			
			// Create a new tree node to hold the button
			Node newNode = new Node(newButton.Text);
			
			// Attached the button to the node
			newNode.Tag = newButton;
			
			// Append to end of the list
			treeControl1.Nodes.Add(newNode);
			
			// Select the new entry
			newNode.Select();
			
			// Put focus back to the tree control
			treeControl1.Focus();
			
			UpdateButtonState();
		}

		private void buttonAddSeparator_Click(object sender, System.EventArgs e)
		{
			// Create a new button instance
			SeparatorCommand newSep = new SeparatorCommand();
			
			// Create a new tree node to hold the separator
			Node newNode = new Node("(Separator)");
			
			// Attached the separator to the node
			newNode.Tag = newSep;
			
			// Append to end of the list
			treeControl1.Nodes.Add(newNode);
			
			// Select the new entry
			newNode.Select();

			// Put focus back to the tree control
			treeControl1.Focus();

			UpdateButtonState();
		}

		private void buttonMoveUp_Click(object sender, System.EventArgs e)
		{
			// Grab the actual node to move
			Node node = treeControl1.SelectedNode;
			
			// Find its index position
			int index = treeControl1.SelectedNode.Index;
			
			// Remove it from the control
			node.ParentNodes.Remove(node);
			
			// Put back one position higher up
			treeControl1.Nodes.Insert(index - 1, node);
		
			// Select the new inserted entry again
			node.Select();
		
			// Put focus back to the tree control
			treeControl1.Focus();

			UpdateButtonState();
		}

		private void buttonMoveDown_Click(object sender, System.EventArgs e)
		{
			// Grab the actual node to move
			Node node = treeControl1.SelectedNode;
			
			// Find its index position
			int index = treeControl1.SelectedNode.Index;
			
			// Remove it from the control
			node.ParentNodes.Remove(node);
			
			// Put back one position higher up
			treeControl1.Nodes.Insert(index + 1, node);
		
			// Select the new inserted entry again
			node.Select();

			// Put focus back to the tree control
			treeControl1.Focus();

			UpdateButtonState();
		}

		private void buttonRemove_Click(object sender, System.EventArgs e)
		{
			// No longer this object selected
			propertyGrid.SelectedObject = null;

			ButtonCommand button = treeControl1.SelectedNode.Tag as ButtonCommand;
			
			// Remove any handler from the command
			if (button != null)
				button.TextChanged -= new EventHandler(OnTextChanged);

			// Remove the selected item
			treeControl1.Nodes.Remove(treeControl1.SelectedNode);
			
			// Put focus back to the tree control
			treeControl1.Focus();

			UpdateButtonState();
		}

		private void buttonClearAll_Click(object sender, System.EventArgs e)
		{
			// Remove any event handlers
			foreach(Node n in treeControl1.Nodes)
			{
				ButtonCommand button = n.Tag as ButtonCommand;
			
				// Remove any handler from the command
				if (button != null)
					button.TextChanged -= new EventHandler(OnTextChanged);
			}
					
			// Remove all the entries
			treeControl1.Nodes.Clear();

			// Put focus back to the tree control
			treeControl1.Focus();

			// No longer any object selected
			propertyGrid.SelectedObject = null;

			UpdateButtonState();
		}
		
		private void OnTextChanged(object sender, System.EventArgs e)
		{
			// Convert to correct class
			ButtonCommand button = sender as ButtonCommand;
		
			// Find the node the contains this command
			foreach(Node n in treeControl1.Nodes)
			{
				if (n.Tag == button)
				{

					// Update node with the new text
					if (button.Text.Length == 0)
						n.Text = "<Empty>";
					else
						n.Text = button.Text;

					break;
				}
			}
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
