using UnityEngine;
using UnityEngine.Events;

namespace Scriptable_Objects
{
	[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data", order = 0)]
	public class LevelData : ScriptableObject
	{
		[SerializeField] int _currentLevel;
		public IntEvent OnCurrentLevelChange;
		
		public Platform[] platforms;
		public int CurrentLevel
		{
			get => _currentLevel;
			set
			{
				_currentLevel = value;
				OnCurrentLevelChange.Invoke(_currentLevel);
			}
		}
	}
}
