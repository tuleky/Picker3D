using Scriptable_Objects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class CanvasManager : MonoBehaviour
{
	[SerializeField] IntVariable successCounter;

	
	public void Restart()
	{
		successCounter.RunTimeValue = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
