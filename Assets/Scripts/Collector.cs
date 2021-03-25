using System.Collections;
using System.Collections.Generic;
using Scriptable_Objects;
using TMPro;
using UnityEngine;
public class Collector : MonoBehaviour
{
	[SerializeField] GameEvent reachedCollector;
	[SerializeField] GameEvent success;
	[SerializeField] GameEvent fail;
	[SerializeField] IntVariable successCounter;

	[SerializeField] TextMeshPro ballCountText;
	[SerializeField] int nextLevelBallCount;
		
	[SerializeField] float playerWaitTime;
	int _ballCount;
	List<GameObject> _balls = new List<GameObject>();

	void Start()
	{
		ballCountText.text = _ballCount + "/" + nextLevelBallCount;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Ball"))
		{
			_balls.Add(collision.gameObject);
			_ballCount++;
			ballCountText.text = _ballCount + "/" + nextLevelBallCount;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PlayerCore"))
		{
			reachedCollector.Raise();
			StartCoroutine(WaitAndContinue(playerWaitTime));
		}
	}

	IEnumerator WaitAndContinue(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		CheckBallCondition();
	}

	void CheckBallCondition()
	{
		if (_ballCount >= nextLevelBallCount)
		{
			successCounter.RunTimeValue++;
			foreach (GameObject ball in _balls)
			{
				ball.SetActive(false);
			}
			success.Raise();
		}
		else
		{
			fail.Raise();
		}
	}
}
