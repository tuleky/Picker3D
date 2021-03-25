using Scriptable_Objects;
using UnityEngine;
public class SuccessArea : MonoBehaviour
{
	[SerializeField] GameEvent finalSuccess;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PlayerCore"))
			finalSuccess.Raise();
	}
}