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

namespace Crownwood.DotNetMagic.Controls
{
	/// <summary>
	/// Form used to edit a node collection.
	/// </summary>
	public class NodeCollectionDialog : Form
	{
		private Crownwood.DotNetMagic.Controls.TreeControl treeControl1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonInsert;
		private System.Windows.Forms.Button buttonAppend;
		private System.Windows.Forms.Button buttonAddChild;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonClearAll;
		private System.Windows.Forms.Button buttonMoveDown;
		private System.Windows.Forms.Button buttonMoveUp;
		private System.Windows.Forms.MenuItem insertNode;
		private System.Windows.Forms.MenuItem appendNode;
		private System.Windows.Forms.MenuItem moveUp;
		private System.Windows.Forms.MenuItem moveDown;
		private System.Windows.Forms.MenuItem removeNode;
		private System.Windows.Forms.MenuItem clearAllNodes;
		private System.Windows.Forms.MenuItem menuAddChild;
		private System.Windows.Forms.MenuItem menuSep1;
		private System.Windows.Forms.MenuItem menuSep2;
		private System.Windows.Forms.MenuItem menuSep3;
		private System.Windows.Forms.MenuItem menuSep4;
		private System.Windows.Forms.Label label2;
		
		/// <summary>
		/// Initializes a new instance of the NodeCollectionDialog class.
		/// </summary>
        public NodeCollectionDialog()
        {
			// Required for Windows Form Designer support
			InitializeComponent();
		}

		/// <summary>
		/// Initializes a new instance of the NodeCollectionDialog class.
		/// </summary>
		/// <param name="original">Original Nodes to be edited.</param>
		public NodeCollectionDialog(NodeCollection original)
		{
			// Required for Windows Form Designer support
			InitializeComponent();
			
			// We need to clone all the incoming nodes so that we are changing the copy
			// and not the original, just in case the user decides to make changes and then
			// cancel the changes.
			foreach(Node n in original)
				treeControl1.Nodes.Add((Node)n.Clone());
				
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
			this.insertNode = new System.Windows.Forms.MenuItem();
			this.appendNode = new System.Windows.Forms.MenuItem();
			this.menuSep1 = new System.Windows.Forms.MenuItem();
			this.menuAddChild = new System.Windows.Forms.MenuItem();
			this.menuSep2 = new System.Windows.Forms.MenuItem();
			this.moveUp = new System.Windows.Forms.MenuItem();
			this.moveDown = new System.Windows.Forms.MenuItem();
			this.menuSep3 = new System.Windows.Forms.MenuItem();
			this.removeNode = new System.Windows.Forms.MenuItem();
			this.menuSep4 = new System.Windows.Forms.MenuItem();
			this.clearAllNodes = new System.Windows.Forms.MenuItem();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonInsert = new System.Windows.Forms.Button();
			this.buttonAppend = new System.Windows.Forms.Button();
			this.buttonAddChild = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.buttonClearAll = new System.Windows.Forms.Button();
			this.buttonMoveDown = new System.Windows.Forms.Button();
			this.buttonMoveUp = new System.Windows.Forms.Button();
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
			this.treeControl1.FocusNode = null;
			this.treeControl1.GroupFont = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.treeControl1.HotBackColor = System.Drawing.Color.Empty;
			this.treeControl1.HotForeColor = System.Drawing.Color.Empty;
			this.treeControl1.Location = new System.Drawing.Point(104, 32);
			this.treeControl1.Name = "treeControl1";
			this.treeControl1.SelectedNode = null;
			this.treeControl1.SelectedNoFocusBackColor = System.Drawing.SystemColors.Control;
			this.treeControl1.SelectMode = Crownwood.DotNetMagic.Controls.SelectMode.Single;
			this.treeControl1.Size = new System.Drawing.Size(224, 376);
			this.treeControl1.TabIndex = 0;
			this.treeControl1.Text = "treeControl1";
			this.treeControl1.AfterSelect += new Crownwood.DotNetMagic.Controls.NodeEventHandler(this.treeControl1_AfterSelect);
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.insertNode,
																						this.appendNode,
																						this.menuSep1,
																						this.menuAddChild,
																						this.menuSep2,
																						this.moveUp,
																						this.moveDown,
																						this.menuSep3,
																						this.removeNode,
																						this.menuSep4,
																						this.clearAllNodes});
			this.contextMenu.Popup += new System.EventHandler(this.contextMenu_Popup);
			// 
			// insertNode
			// 
			this.insertNode.Index = 0;
			this.insertNode.Text = "Insert";
			this.insertNode.Click += new System.EventHandler(this.insertNode_Click);
			// 
			// appendNode
			// 
			this.appendNode.Index = 1;
			this.appendNode.Text = "Append";
			this.appendNode.Click += new System.EventHandler(this.appendNode_Click);
			// 
			// menuSep1
			// 
			this.menuSep1.Index = 2;
			this.menuSep1.Text = "-";
			// 
			// menuAddChild
			// 
			this.menuAddChild.Index = 3;
			this.menuAddChild.Text = "Add Child";
			this.menuAddChild.Click += new System.EventHandler(this.menuAddChild_Click);
			// 
			// menuSep2
			// 
			this.menuSep2.Index = 4;
			this.menuSep2.Text = "-";
			// 
			// moveUp
			// 
			this.moveUp.Index = 5;
			this.moveUp.Text = "Move Up";
			this.moveUp.Click += new System.EventHandler(this.moveUp_Click);
			// 
			// moveDown
			// 
			this.moveDown.Index = 6;
			this.moveDown.Text = "Move Down";
			this.moveDown.Click += new System.EventHandler(this.moveDown_Click);
			// 
			// menuSep3
			// 
			this.menuSep3.Index = 7;
			this.menuSep3.Text = "-";
			// 
			// removeNode
			// 
			this.removeNode.Index = 8;
			this.removeNode.Text = "Remove";
			this.removeNode.Click += new System.EventHandler(this.removeNode_Click);
			// 
			// menuSep4
			// 
			this.menuSep4.Index = 9;
			this.menuSep4.Text = "-";
			// 
			// clearAllNodes
			// 
			this.clearAllNodes.Index = 10;
			this.clearAllNodes.Text = "Clear All";
			this.clearAllNodes.Click += new System.EventHandler(this.clearAllNodes_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(104, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 1;
			this.label1.Text = "Node Collection";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonOK.Location = new System.Drawing.Point(416, 424);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.HelpVisible = false;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
			this.propertyGrid1.Location = new System.Drawing.Point(344, 32);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.propertyGrid1.Size = new System.Drawing.Size(232, 376);
			this.propertyGrid1.TabIndex = 1;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ToolbarVisible = false;
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(344, 8);
			this.label2.Name = "label2";
			this.label2.TabIndex = 5;
			this.label2.Text = "Node Properties";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonCancel.Location = new System.Drawing.Point(501, 424);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonInsert
			// 
			this.buttonInsert.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonInsert.Location = new System.Drawing.Point(16, 32);
			this.buttonInsert.Name = "buttonInsert";
			this.buttonInsert.TabIndex = 6;
			this.buttonInsert.Text = "Insert";
			this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
			// 
			// buttonAppend
			// 
			this.buttonAppend.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonAppend.Location = new System.Drawing.Point(16, 64);
			this.buttonAppend.Name = "buttonAppend";
			this.buttonAppend.TabIndex = 7;
			this.buttonAppend.Text = "Append";
			this.buttonAppend.Click += new System.EventHandler(this.buttonAppend_Click);
			// 
			// buttonAddChild
			// 
			this.buttonAddChild.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonAddChild.Location = new System.Drawing.Point(16, 96);
			this.buttonAddChild.Name = "buttonAddChild";
			this.buttonAddChild.TabIndex = 8;
			this.buttonAddChild.Text = "Add Child";
			this.buttonAddChild.Click += new System.EventHandler(this.buttonAddChild_Click);
			// 
			// buttonRemove
			// 
			this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonRemove.Location = new System.Drawing.Point(16, 224);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.TabIndex = 9;
			this.buttonRemove.Text = "Remove";
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// buttonClearAll
			// 
			this.buttonClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClearAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonClearAll.Location = new System.Drawing.Point(16, 384);
			this.buttonClearAll.Name = "buttonClearAll";
			this.buttonClearAll.TabIndex = 10;
			this.buttonClearAll.Text = "Clear All";
			this.buttonClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
			// 
			// buttonMoveDown
			// 
			this.buttonMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonMoveDown.Location = new System.Drawing.Point(16, 176);
			this.buttonMoveDown.Name = "buttonMoveDown";
			this.buttonMoveDown.Size = new System.Drawing.Size(72, 23);
			this.buttonMoveDown.TabIndex = 14;
			this.buttonMoveDown.Text = "Move Down";
			this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
			// 
			// buttonMoveUp
			// 
			this.buttonMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonMoveUp.Location = new System.Drawing.Point(16, 144);
			this.buttonMoveUp.Name = "buttonMoveUp";
			this.buttonMoveUp.Size = new System.Drawing.Size(72, 23);
			this.buttonMoveUp.TabIndex = 13;
			this.buttonMoveUp.Text = "Move Up";
			this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
			// 
			// NodeCollectionDialog
			// 
			this.ClientSize = new System.Drawing.Size(592, 462);
			this.ControlBox = false;
			this.Controls.Add(this.buttonMoveDown);
			this.Controls.Add(this.buttonMoveUp);
			this.Controls.Add(this.buttonClearAll);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.buttonAddChild);
			this.Controls.Add(this.buttonAppend);
			this.Controls.Add(this.buttonInsert);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.propertyGrid1);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.treeControl1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(480, 384);
			this.Name = "NodeCollectionDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Node Collection Editor";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Gets the collection of edited nodes.
		/// </summary>
		public NodeCollection Nodes
		{
			get { return treeControl1.Nodes; }
		}

		private void clearAllNodes_Click(object sender, System.EventArgs e)
		{
			propertyGrid1.SelectedObject = null;
			treeControl1.Nodes.Clear();
		}

		private void appendNode_Click(object sender, System.EventArgs e)
		{
			Node newNode = new Node("Node");

			//...then add at end of the sibling collection of nodes
			treeControl1.SelectedNode.ParentNodes.Add(newNode);

			// Now select the new node
			treeControl1.SelectedNode = newNode;
		}

		private void insertNode_Click(object sender, System.EventArgs e)
		{
			Node newNode = new Node("Node");

			// If there is no selected node...
			if (treeControl1.SelectedNode == null)
			{
				//...then just add one to end of the root level
				treeControl1.Nodes.Add(newNode);
			}
			else
			{
				// Find index of the selected node in its collection
				int index = treeControl1.SelectedNode.ParentNodes.IndexOf(treeControl1.SelectedNode);

				// Insert new node before it in the sibling collection
				treeControl1.SelectedNode.ParentNodes.Insert(index, newNode);
			}

			// Now select the new node
			treeControl1.SelectedNode = newNode;
		}

		private void removeNode_Click(object sender, System.EventArgs e)
		{
			propertyGrid1.SelectedObject = null;
			treeControl1.SelectedNode.ParentNodes.Remove(treeControl1.SelectedNode);
		}

		private void menuAddChild_Click(object sender, System.EventArgs e)
		{
			Node newNode = new Node("Node");

			// Add new node at end of the child nodes
			treeControl1.SelectedNode.Nodes.Add(newNode);
		
			// Now select the new node
			treeControl1.SelectedNode = newNode;
		}

		private void buttonClearAll_Click(object sender, System.EventArgs e)
		{
			clearAllNodes_Click(treeControl1, EventArgs.Empty);
			UpdateButtonState();
		}

		private void buttonInsert_Click(object sender, System.EventArgs e)
		{
			insertNode_Click(treeControl1, EventArgs.Empty);
			UpdateButtonState();
		}

		private void buttonAppend_Click(object sender, System.EventArgs e)
		{
			appendNode_Click(treeControl1, EventArgs.Empty);
			UpdateButtonState();
		}

		private void buttonRemove_Click(object sender, System.EventArgs e)
		{
			removeNode_Click(treeControl1, EventArgs.Empty);
			UpdateButtonState();
		}

		private void buttonAddChild_Click(object sender, System.EventArgs e)
		{
			menuAddChild_Click(treeControl1, EventArgs.Empty);
			UpdateButtonState();
		}
		
		private void buttonMoveUp_Click(object sender, System.EventArgs e)
		{
			// Grab the actual node to move
			Node node = treeControl1.SelectedNode;

			// Is it expanded?
			bool expanded = node.Expanded;
			
			// Must collapse it
			if (expanded)
				node.Collapse();

			// Find the next displayed node
			Node prev = node.PreviousDisplayedNode;

			// Remove node from the tree			
			node.ParentNodes.Remove(node);

			// Add node before the previous one
			prev.ParentNodes.Insert(prev.Index, node);
		
			// If node was expanded, then put its expanded state back again
			if (expanded)
				node.Expand();

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
			
			// Is it expanded?
			bool expanded = node.Expanded;
			
			// Must collapse it
			if (expanded)
				node.Collapse();

			// Find the next displayed node
			Node next = node.NextDisplayedNode;

			// Remove node from the tree			
			node.ParentNodes.Remove(node);

			if (next.Expanded && (next.Nodes.VisibleCount > 0))
			{
				// Add as first child instead
				next.Nodes.Insert(0, node);
			}
			else
			{
				// Add node after the next one
				next.ParentNodes.Insert(next.Index + 1, node);
			}
					
			// If node was expanded, then put its expanded state back again
			if (expanded)
				node.Expand();
					
			// Select the new inserted entry again
			node.Select();
		
			// Put focus back to the tree control
			treeControl1.Focus();

			UpdateButtonState();
		}

		private void moveUp_Click(object sender, System.EventArgs e)
		{
			buttonMoveUp_Click(this, EventArgs.Empty);
		}

		private void moveDown_Click(object sender, System.EventArgs e)
		{
			buttonMoveDown_Click(this, EventArgs.Empty);
		}

		private void UpdateButtonState()
		{
			bool nodeSelected = (treeControl1.SelectedNode != null);

			buttonAppend.Enabled = nodeSelected;
			buttonAddChild.Enabled = nodeSelected;	
			buttonRemove.Enabled = nodeSelected;
			buttonMoveUp.Enabled = nodeSelected && (treeControl1.SelectedNode != treeControl1.GetFirstDisplayedNode());
			buttonMoveDown.Enabled = nodeSelected  && !InsideLastBranch(treeControl1.SelectedNode);
		}
		
		private bool InsideLastBranch(Node n)
		{
			// Start check from the last displayed node
			Node last = treeControl1.GetLastDisplayedNode();
			
			// Keep going until we have reach the root
			while(last != null)
			{
				// If our searched for node is in the last branch
				if (last == n)
					return true;
					
				// Move up a level
				last = last.Parent;
			}
			
			// No match, the given node is not in the last branch
			return false;
		}

		private void contextMenu_Popup(object sender, System.EventArgs e)
		{
			// Is there  a node selected?
			bool nodeSelected = (treeControl1.SelectedNode != null);
		
			// Some menu options are context dependant
			appendNode.Enabled = nodeSelected;
			menuAddChild.Enabled = nodeSelected;
			removeNode.Enabled = nodeSelected;
			moveUp.Enabled = nodeSelected && (treeControl1.SelectedNode != treeControl1.GetFirstDisplayedNode());
			moveDown.Enabled = nodeSelected  && !InsideLastBranch(treeControl1.SelectedNode);
		}

		private void treeControl1_AfterSelect(Crownwood.DotNetMagic.Controls.TreeControl tc, Crownwood.DotNetMagic.Controls.NodeEventArgs e)
		{
			// Update the properties control with the newly selected control
			propertyGrid1.SelectedObject = treeControl1.SelectedNode;
			UpdateButtonState();
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
