using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects
{
	[CreateAssetMenu]
	public class GameEvent : ScriptableObject
	{
		readonly List<IGameEventListener> _listeners = new List<IGameEventListener>();

		public void Raise()
		{
			for (int i = _listeners.Count - 1; i >= 0; i--)
				_listeners[i].OnEventRaised();
		}

		public void RegisterListener(IGameEventListener listener)
		{
			_listeners.Add(listener);
		}

		public void UnregisterListener(IGameEventListener listener)
		{
			_listeners.Remove(listener);
		}
	}
}
