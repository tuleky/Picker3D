using System;
using Object_Pool;
using Scriptable_Objects;
using UnityEngine;

public class AnimatedSpawner : MonoBehaviour, IGameEventListener
{
	[SerializeField] GameObject spawnObject;
	[SerializeField] IntVariable successCounter;

	[SerializeField] int startOrder;
	[SerializeField] GameEvent gameStarted;
	
	
	Animation _anim;
	ObjectPooler _objectPooler;

	void Start()
	{
		_anim = GetComponent<Animation>();
		_objectPooler = ObjectPooler.Instance;
	}

	public void CheckOrder()
	{
		if (startOrder == successCounter.RunTimeValue)
			// Start
			_anim.Play();
	}

	// this is called by animation
	public void SpawnBall()
	{
		//Instantiate(spawnObject, transform.position, Quaternion.identity);
		_objectPooler.SpawnFromPool("Ball", transform.position, Quaternion.identity);
	}

	public void Destroy()
	{
		Destroy(gameObject, 0.5f);
	}

	void OnEnable()
	{
		gameStarted.RegisterListener(this);
	}

	void OnDisable()
	{
		gameStarted.UnregisterListener(this);
	}

	public void OnEventRaised()
	{
		CheckOrder();
	}
}
