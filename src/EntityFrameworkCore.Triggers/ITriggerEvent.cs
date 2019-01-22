﻿using System;
using System.Threading.Tasks;

#if EF_CORE
using Microsoft.EntityFrameworkCore;
namespace EntityFrameworkCore.Triggers
#else
using System.Data.Entity;
namespace EntityFramework.Triggers
#endif
{
	[Obsolete("This interface is for internal use by the library. It is not intended to be used by calling code.")]
	public interface ITriggerEvent
	{
		void Raise(Object entry);
	}
	
	public interface ITriggerEvent<out TEntry>
	: ITriggerEvent
	where TEntry : IEntry
	{
		void Add(Action<TEntry> handler);
		void Remove(Action<TEntry> handler);
		void Add(Func<TEntry, Task> handler);
		void Remove(Func<TEntry, Task> handler);
	}

	public interface ITriggerEvent<out TEntry, out TEntity>
	: ITriggerEvent<TEntry>
	where TEntry : IEntry<TEntity>
	where TEntity : class
	{
		new void Add(Action<TEntry> handler);
		new void Remove(Action<TEntry> handler);
		new void Add(Func<TEntry, Task> handler);
		new void Remove(Func<TEntry, Task> handler);
	}

	public interface ITriggerEvent<out TEntry, out TEntity, out TDbContext>
	: ITriggerEvent<TEntry, TEntity>
	where TEntry : IEntry<TEntity, TDbContext>
	where TEntity : class
	where TDbContext : DbContext
	{
		new void Add(Action<TEntry> handler);
		new void Remove(Action<TEntry> handler);
		new void Add(Func<TEntry, Task> handler);
		new void Remove(Func<TEntry, Task> handler);
	}
}