using System;
using Scriptable_Objects;
using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
	[SerializeField] LevelData levelData;
	[SerializeField] TextMeshProUGUI levelText;

	void OnEnable()
	{
		levelData.OnCurrentLevelChange.AddListener(ChangeLevelText);
		ChangeLevelText(levelData.CurrentLevel);
	}

	void ChangeLevelText(int level)
	{
		levelText.text = "LEVEL: " + level;
	}
}