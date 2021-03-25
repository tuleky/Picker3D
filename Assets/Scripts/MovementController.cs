using System;
using Scriptable_Objects;
using UnityEngine;

[Serializable]
public class Boundary
{
	public float xMin, xMax;
}

public class MovementController : MonoBehaviour, IGameEventListener
{
	[SerializeField] Boundary boundary = new Boundary();

	[SerializeField] GameEvent gameStarted;


	[SerializeField] float inputSensitivity;
	[SerializeField] float sideSpeed;
	[SerializeField] float forwardSpeed;
	[SerializeField] float fullyStopTime = 0.2f;
	[SerializeField] float sideStopTime = 0.07f;
	
	float _currentVelocity;
	Vector3 _currentVelocityVector;

	bool _canGo;

	Vector2? _mousePosition;
	Vector3 _moveVector;

	Rigidbody _rbd;
	
	public void ChangeMoveState(bool param)
	{
		_canGo = param;
	}
	
	public void OnEventRaised()
	{
		ChangeMoveState(true);
	}
	
	void OnEnable()
	{
		gameStarted.RegisterListener(this);
	}

	void OnDisable()
	{
		gameStarted.UnregisterListener(this);
	}
	
	void Start()
	{
		_rbd = GetComponent<Rigidbody>();
	}

	void Update()
	{
		GetInputAndMovePlayer();
	}
	
	void GetInputAndMovePlayer()
	{
		if (Input.GetMouseButton(0))
		{
			if (_mousePosition != null)
			{
				Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;

				if (mouseDeltaPos.x > inputSensitivity && _rbd.position.x < boundary.xMax)
					_moveVector.x = sideSpeed / 5f * mouseDeltaPos.x;
				else if (mouseDeltaPos.x < -inputSensitivity && _rbd.position.x > boundary.xMin)
					_moveVector.x = -sideSpeed / 5f * -mouseDeltaPos.x;
				else
					_moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity, sideStopTime);

				_mousePosition = Input.mousePosition;
			}
		}
		else
		{
			if (Mathf.Abs(_moveVector.x) > 0)
				_moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity, sideStopTime);
		}

		// take input from mouse, check for input difference
		if (Input.GetMouseButtonDown(0))
			_mousePosition = Input.mousePosition;

		if (Input.GetMouseButtonUp(0))
			_mousePosition = null;
	}

	void FixedUpdate()
	{
		if (_canGo)
			_rbd.velocity = new Vector3(_moveVector.x, _rbd.velocity.y, forwardSpeed);
		else
			_rbd.velocity = Vector3.SmoothDamp(_rbd.velocity, Vector3.zero, ref _currentVelocityVector, fullyStopTime);

		_rbd.position = new Vector3(Mathf.Clamp(_rbd.position.x, boundary.xMin, boundary.xMax), _rbd.position.y, _rbd.position.z);
	}
}