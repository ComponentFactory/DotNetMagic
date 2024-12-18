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

namespace Crownwood.DotNetMagic.Controls.Command
{
	/// <summary>
	/// Provides a collection container for CommandState instances.
	/// </summary>
	public class CommandStateCollection : CollectionWithEvents
	{
		/// <summary>
		/// Adds the specified CommandState object to the collection.
		/// </summary>
		/// <param name="value">The CommandState object to add to the collection.</param>
		/// <returns>The CommandState object added to the collection.</returns>
		public CommandState Add(CommandState value)
		{
			// Use base class to process actual collection operation
			base.List.Add(value as object);

			return value;
		}

		/// <summary>
		/// Adds an array of CommandState objects to the collection.
		/// </summary>
		/// <param name="values">An array of CommandState objects to add to the collection.</param>
		public void AddRange(CommandState[] values)
		{
			// Use existing method to add each array entry
			foreach(CommandState command in values)
				Add(command);
		}

		/// <summary>
		/// Removes a CommandState from the collection.
		/// </summary>
		/// <param name="value">A CommandState to remove from the collection.</param>
		public void Remove(CommandState value)
		{
			// Use base class to process actual collection operation
			base.List.Remove(value as object);
		}

		/// <summary>
		/// Inserts a CommandState instance into the collection at the specified location.
		/// </summary>
		/// <param name="index">The location in the collection where you want to add the CommandState.</param>
		/// <param name="value">The CommandState object to insert.</param>
		public void Insert(int index, CommandState value)
		{
			// Use base class to process actual collection operation
			base.List.Insert(index, value as object);
		}

		/// <summary>
		/// Determines whether a CommandState is in the collection.
		/// </summary>
		/// <param name="value">The CommandState to locate in the collection.</param>
		/// <returns>true if item is found in the collection; otherwise, false.</returns>
		public bool Contains(CommandState value)
		{
			// Use base class to process actual collection operation
			return base.List.Contains(value as object);
		}

		/// <summary>
		/// Gets the CommandState at the specified index.
		/// </summary>
		public CommandState this[int index]
		{
			// Use base class to process actual collection operation
			get { return (base.List[index] as CommandState); }
		}

		/// <summary>
		/// Returns the index of the first occurrence of the given CommandState.
		/// </summary>
		/// <param name="value">The CommandState to locate.</param>
		/// <returns>Index of object; otherwise -1</returns>
		public int IndexOf(CommandState value)
		{
			// Find the 0 based index of the requested entry
			return base.List.IndexOf(value);
		}
	}
}
