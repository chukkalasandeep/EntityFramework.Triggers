﻿using System;
using System.Collections.Concurrent;

#if EF_CORE
namespace EntityFrameworkCore.Triggers
#else
namespace EntityFramework.Triggers
#endif
{
	internal static class TriggerInvokerAsyncCache
	{
		public static ITriggerInvokerAsync Get(Type dbContextType)
		{
			return cache.GetOrAdd(dbContextType, ValueFactory);
		}

		private static ITriggerInvokerAsync ValueFactory(Type type)
		{
			var triggerInvokerType = typeof(TriggerInvoker<>).MakeGenericType(type);
			return (ITriggerInvokerAsync)Activator.CreateInstance(triggerInvokerType);
		}

		private static readonly ConcurrentDictionary<Type, ITriggerInvokerAsync> cache = new ConcurrentDictionary<Type, ITriggerInvokerAsync>();
	}

	public static class GenericServiceCache<TInterface, TConstructedSingleGenericArgImplementation>
	where TConstructedSingleGenericArgImplementation : TInterface
	{
		private static readonly Type GenericTypeDefinition = typeof(TConstructedSingleGenericArgImplementation).GetGenericTypeDefinition();
		private static readonly ConcurrentDictionary<Type, TInterface> cache = new ConcurrentDictionary<Type, TInterface>();
		private static TInterface Factory(Type type) => (TInterface) Activator.CreateInstance(GenericTypeDefinition.MakeGenericType(type));

		public static TInterface GetOrAdd(Type genericArgumentType) => cache.GetOrAdd(genericArgumentType, Factory);
	}
}