using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Missive_CSharp
{
	public class MissiveAggregator
	{
		public static MissiveAggregator instance = new MissiveAggregator();

		public MissiveAggregator()
		{
			//All missive types automatically added to dictionary
			foreach (var type in Assembly.GetAssembly(typeof(Missive)).GetTypes().Where(t => t.BaseType == typeof(Missive)))
			{
				Console.WriteLine(type.ToString());
				listeners.Add(type, new HashSet<IMissiveListener>());
			}
		}
	
		private readonly Dictionary<Type, HashSet<IMissiveListener>> listeners = 
			new Dictionary<Type, HashSet<IMissiveListener> >();
	
		private readonly HashSet<IMissiveListener<Missive>> globalListeners = new HashSet< IMissiveListener<Missive> >();

		public void Publish<T>(T missive) where T:Missive
		{	
			//Publish missive to global listeners.
			foreach (var listener in globalListeners)
			{
				listener.HandleMissive(missive);
			}
		
			//Publish missive to all listeners.
			try
			{
				foreach (var listener in listeners[typeof(T)])
				{
					if (!(listener is IMissiveListener<T>))
					{
						Console.WriteLine("Wrong IListener type registered, should be of type {1} is of type {0}", listener.GetType(), typeof(T));
						return;
					}
				
					(listener as IMissiveListener<T>).HandleMissive(missive);
				}
			}
			catch(KeyNotFoundException exception)
			{
				Console.WriteLine("{0}: Tried to publish an missive not registered in the assembly. Looked for {1} with" +
				                  "	basetype {2} (should inherit from Missive.", exception, typeof(T), typeof(T).BaseType);
			}
		}

		public void Register<T>(IMissiveListener<T> listener) where T:Missive
		{
			try
			{
				listeners[typeof(T)].Add(listener);
			}
			catch (KeyNotFoundException e)
			{
				Console.WriteLine("{0}: Tried to register a listener handling an Missive type ({1} base {2}) that " +
				                  "was not found in the assembly.", e, typeof(T), typeof(T).BaseType);
			}
		}

		public void Unregister<T>(IMissiveListener<T> listener) where T : Missive
		{
			try
			{
				listeners[typeof(T)].Remove(listener);
			}
			catch (KeyNotFoundException e)
			{
				Console.WriteLine("{0}: Tried to register a listener handling an Missive type ({1} base {2}) that " +
				                  "was not found in the assembly.", e, typeof(T), typeof(T).BaseType);
			}
		}
		

		public void RegisterGlobalListener(IMissiveListener<Missive> listener)
		{
			globalListeners.Add(listener);
		}

	}
}
