﻿using UnityEngine;
using UnityEngine.Events;

namespace Scriptable_Objects
{
	public class GameEventListener : MonoBehaviour, IGameEventListener
	{
		public GameEvent Event;
		public UnityEvent Response;

		void OnEnable()
		{
			Event.RegisterListener(this);
		}

		void OnDisable()
		{
			Event.UnregisterListener(this);
		}

		public void OnEventRaised()
		{
			Response.Invoke();
		}
	}
}
