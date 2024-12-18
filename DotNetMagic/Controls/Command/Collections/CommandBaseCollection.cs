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
using Crownwood.DotNetMagic.Common;
using Crownwood.DotNetMagic.Controls.Command;

namespace Crownwood.DotNetMagic.Controls
{
	/// <summary>
	/// Provides a collection container for CommandBase derived instances.
	/// </summary>
	public class CommandBaseCollection : CollectionWithEvents
	{
		/// <summary>
		/// Adds the specified CommandBase object to the collection.
		/// </summary>
		/// <param name="value">The CommandBase object to add to the collection.</param>
		/// <returns>The CommandBase object added to the collection.</returns>
		public CommandBase Add(CommandBase value)
		{
			// Use base class to process actual collection operation
			base.List.Add(value as object);

			return value;
		}

		/// <summary>
		/// Adds an array of CommandBase objects to the collection.
		/// </summary>
		/// <param name="values">An array of CommandBase objects to add to the collection.</param>
		public void AddRange(CommandBase[] values)
		{
			// Use existing method to add each array entry
			foreach(CommandBase command in values)
				Add(command);
		}

		/// <summary>
		/// Removes a CommandBase from the collection.
		/// </summary>
		/// <param name="value">A CommandBase to remove from the collection.</param>
		public void Remove(CommandBase value)
		{
			// Use base class to process actual collection operation
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts a CommandBase instance into the collection at the specified location.
		/// </summary>
		/// <param name="index">The location in the collection where you want to add the CommandBase.</param>
		/// <param name="value">The CommandBase object to insert.</param>
		public void Insert(int index, CommandBase value)
		{
			// Use base class to process actual collection operation
			base.List.Insert(index, value as object);
		}

		/// <summary>
		/// Determines whether a CommandBase is in the collection.
		/// </summary>
		/// <param name="value">The CommandBase to locate in the collection.</param>
		/// <returns>true if item is found in the collection; otherwise, false.</returns>
		public bool Contains(CommandBase value)
		{
			// Use base class to process actual collection operation
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the CommandBase at the specified index.
		/// </summary>
		public CommandBase this[int index]
		{
			// Use base class to process actual collection operation
			get { return (base.List[index] as CommandBase); }
		}

		/// <summary>
		/// Returns the index of the first occurrence of the given CommandBase.
		/// </summary>
		/// <param name="value">The CommandBase to locate.</param>
		/// <returns>Index of object; otherwise -1</returns>
		public int IndexOf(CommandBase value)
		{
			// Find the 0 based index of the requested entry
			return base.List.IndexOf(value);
		}
	}
}
