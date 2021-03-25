using System;
using Cinemachine;
using Scriptable_Objects;
using UnityEngine;
public class CameraManager : MonoBehaviour, IGameEventListener
{
	[SerializeField] CinemachineVirtualCamera mainCamera;
	[SerializeField] CinemachineVirtualCamera panoramicCamera;
	[SerializeField] CinemachineVirtualCamera levelSuccessCamera;
	
	
	[SerializeField] GameEvent levelSuccess;

	void OnEnable()
	{
		levelSuccess.RegisterListener(this);
	}

	void OnDisable()
	{
		levelSuccess.UnregisterListener(this);
	}
	
	public void OnEventRaised()
	{
		levelSuccessCamera.enabled = true;
		mainCamera.enabled = false;
		panoramicCamera.enabled = false;
	}

	public void EnableMainCamera()
	{
		mainCamera.enabled = true;
		panoramicCamera.enabled = false;
		levelSuccessCamera.enabled = false;
	}

	public void EnablePanoramicCamera()
	{
		panoramicCamera.enabled = true;
		mainCamera.enabled = false; 
		levelSuccessCamera.enabled = false;
	}
}
