using System;
using Scriptable_Objects;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour, IGameEventListener
{
	[SerializeField] GameEvent levelSuccess;
	[SerializeField] LevelData levelData;
	[SerializeField] IntVariable successCounter;
	
	
	public void OnEventRaised()
	{
		GenerateNewLevel();
	}

	void OnEnable()
	{
		levelSuccess.RegisterListener(this);
	}

	void OnDisable()
	{
		levelSuccess.UnregisterListener(this);
	}

	void Awake()
	{
		int currentLevel = PlayerPrefs.GetInt("currentLevel");

		if (currentLevel == 0)
		{
			PlayerPrefs.SetInt("currentLevel", 1);
		}

		levelData.CurrentLevel = currentLevel;
	}

	void Start()
	{
		foreach (Platform levelDataPlatform in levelData.platforms)
		{
			if (levelDataPlatform.platformLevel == levelData.CurrentLevel)
			{
				Platform spawnedPlatform = Instantiate(levelDataPlatform);
				Platform.activePlatforms.Enqueue(spawnedPlatform);
				return;
			}
		}
		
		int randomLevel = Random.Range(0, 11);
		Platform spawnedPlatform1 = Instantiate(levelData.platforms[randomLevel]);
		Platform.activePlatforms.Enqueue(spawnedPlatform1);
	}

	void GenerateNewLevel()
	{
		Platform lastSpawnedPlatform = Platform.activePlatforms.Dequeue();
		Destroy(lastSpawnedPlatform.gameObject, 15f);

		successCounter.RunTimeValue = successCounter.InitialValue;
		
		if (levelData.platforms.Length <= levelData.CurrentLevel)
		{
			levelData.CurrentLevel++;
			int randomLevel = Random.Range(0, 11);
			Platform spawnedPlatform = Instantiate(levelData.platforms[randomLevel], lastSpawnedPlatform.platformEndingPoint.position, Quaternion.identity);
			Platform.activePlatforms.Enqueue(spawnedPlatform);
		}
		else
		{
			levelData.CurrentLevel++;
			Platform spawnedPlatform = Instantiate(levelData.platforms[levelData.CurrentLevel - 1], lastSpawnedPlatform.platformEndingPoint.position, Quaternion.identity);
			Platform.activePlatforms.Enqueue(spawnedPlatform);
		}
	}

	void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("currentLevel", levelData.CurrentLevel);
	}
}