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
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using Crownwood.DotNetMagic.Common;

namespace Crownwood.DotNetMagic.Menus
{
	/// <summary>
	/// Information for editing the MenuCommandCollection types.
	/// </summary>
	public class MenuCollectionEditor : UITypeEditor
	{
		/// <summary>
		/// Gets the editor style used by the EditValue method.
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
		/// <returns>A UITypeEditorEditStyle enumeration value that indicates the style of editor.</returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			if ((context != null) && (context.Instance != null))
				return UITypeEditorEditStyle.Modal;
			else
				return base.GetEditStyle(context);
		}

		/// <summary>
		/// Edits the specified object's value using the editor style indicated by GetEditStyle.
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
		/// <param name="provider">An IServiceProvider that this editor can use to obtain services.</param>
		/// <param name="value">The object to edit.</param>
		/// <returns></returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
		{
			if ((context != null) && (context.Instance != null) && (provider != null))
			{
				// Convert to the correct class.
				MenuCommandCollection commands = value as MenuCommandCollection;
				
				// Create the dialog used to edit the menu commands
				MenuCollectionDialog dialog = new MenuCollectionDialog(commands);

				// Give user the chance to modify the nodes
				if (dialog.ShowDialog() == DialogResult.OK)
				{					
					// Reflect changes back into original copy and generate appropriate
					// component changes to designer to reflect these changes
					SynchronizeCollections(commands, dialog.Commands, context);
					
					// Notify container that value has been changed
					context.OnComponentChanged();
					
					// Need to repaint the control to take account of new values
					MenuControl mc = context.Instance as MenuControl;
					
					// Just in case it is null
					if (mc != null)
						mc.Invalidate();
				}
			}

			// Return the original value
			return value;
		}
		
		private void SynchronizeCollections(MenuCommandCollection orig, 
											MenuCommandCollection copy, 
											ITypeDescriptorContext context)
		{
			// Make a note of all original commands that are still in copy
			Hashtable both = new Hashtable();
			
			// First pass, scan looking for nodes that are in original and copy
			foreach(MenuCommand copyChild in copy)
			{
				// Does this node have an back pointer to its original?
				if (copyChild.Original != null)
				{
					// Then make a note that it is in both collections
					both.Add(copyChild.Original, copyChild.Original);

					// Update the original from the copy
					copyChild.Original.UpdateInstance(copyChild);
				}
			}
			
			int origCount = orig.Count;
			
			// Second pass, remove commands in the original but not in the copy
			for(int i=0; i<origCount; i++)
			{
				// Get access to the indexed command from original
				MenuCommand origChild = orig[i];
				
				// If not in the found collection...
				if (!both.ContainsKey(origChild))			
				{
					// ...then it has been removed from the copy, so delete it
					orig.Remove(origChild);
					
					// Must remove from context container so it is removed from designer tray
					context.Container.Remove(origChild as IComponent);
					
					// Must also remove everything below the command
					RemoveAllChildren(origChild, context);

					// Reduce the count and index to reflect change in collection contents
					--i;
					--origCount;
				}
			}
			
			int copyCount = copy.Count;
			
			// Third pass, add new commands from copy but not in original
			for(int i=0; i<copyCount; i++)
			{
				// Get access to the indexed node from copy
				MenuCommand copyChild = copy[i];
				
				// If this command is a new one then it will not have an 'original' property
				if (copyChild.Original == null)
				{
					// Add this node into the original at indexed position
					orig.Insert(i, copyChild);
					
					// It references itself in the new collection
					copyChild.Original = copyChild;

					// Must add into context container so it is added to the designer tray
					context.Container.Add(copyChild as IComponent);
					
					// Must also add everything below the command
					AddAllChildren(copyChild, context);
				}
			}
			
			// Fourth pass, set correct ordering to match copy
			for(int i=0; i<copyCount; i++)
			{
				// Grab indexed item from the copy
				MenuCommand copyChild = copy[i];
				
				// Get the command to look for in original
				MenuCommand origChild = copyChild.Original;
				
				// Find its indexed position in original collection
				int origIndex = orig.IndexOf(origChild);
				
				// If this is not its required position
				if (origIndex != i)
				{
					// Remove it from collection
					orig.Remove(origChild);
					
					// Insert back in correct place
					orig.Insert(i, origChild);
				}
			}
			
			// Fifth pass, process all children
			foreach(MenuCommand copyChild in copy)
			{
				// Does this node has an back pointer to its original?
				if (copyChild.Original != null)
				{
					// Recurse down into the child collections
					SynchronizeCollections(copyChild.Original.MenuCommands, copyChild.MenuCommands, context);
				}
			}
		}
		
		private void AddAllChildren(MenuCommand command, ITypeDescriptorContext context)
		{
			foreach(MenuCommand child in command.MenuCommands)
			{
				// Must add into context container so it is added to the designer tray
				context.Container.Add(child as IComponent);
				
				// Process all children of the child
				AddAllChildren(child, context);
			}
		}

		private void RemoveAllChildren(MenuCommand command, ITypeDescriptorContext context)
		{
			foreach(MenuCommand child in command.MenuCommands)
			{
				// Must remove from context container so it is removed from designer tray
				context.Container.Remove(child as IComponent);
				
				// Process all children of the child
				RemoveAllChildren(child, context);
			}
		}
	}
}
