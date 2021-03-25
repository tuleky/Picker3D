using Scriptable_Objects;
using UnityEngine;

public class TapToStart : MonoBehaviour
{
	[SerializeField] GameEvent gameEvent;
	
	public void StartGame()
	{
		gameEvent.Raise();
		gameObject.SetActive(false);
	}
}