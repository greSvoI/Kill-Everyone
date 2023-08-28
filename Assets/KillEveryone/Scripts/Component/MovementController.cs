using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

namespace KillEveryone
{
	public class MovementController : MonoBehaviour
	{
		private CharacterController controller;
		private PlayerInput input;
		// player
		private float _speed;
		private float _animationBlend;
		private float _targetRotation = 0.0f;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		private Transform mainCamera;

		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		[SerializeField] private float MoveSpeed = 2.0f;

		[Tooltip("Sprint speed of the character in m/s")]
		[SerializeField] private float SprintSpeed = 5.335f;

		[Tooltip("How fast the character turns to face movement direction")]
		[Range(0.0f, 0.3f)]
		[SerializeField] private float RotationSmoothTime = 0.12f;

		[Tooltip("Acceleration and deceleration")]
		[SerializeField] private float SpeedChangeRate = 10.0f;


		private float _velocity;
		private float _gravity = -9.81f;
		private float _gravityMultiply = 3f;


		private void Start()
		{
			controller = GetComponent<CharacterController>();
			input = GetComponent<PlayerInput>();
			mainCamera = Camera.main.transform;
		}
		private void Update()
		{
			ApllyGravity();
		}
		public void ApllyGravity()
		{
			if(controller.isGrounded && _velocity < 0.0f)
			{
				_velocity = -1.0f;
			}
			else
			{
				_velocity += _gravity * _gravityMultiply * Time.deltaTime;
			}
			
		}
		public void Move(float targetSpeed, Vector2 direction)
		{
			float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = input.Move.magnitude > 1 ? 1f: input.Move.magnitude;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset ||
				currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				targetSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
					Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				targetSpeed = Mathf.Round(targetSpeed * 1000f) / 1000f;
			}

			

			_targetRotation = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg +
								  mainCamera.transform.eulerAngles.y;
			float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
				RotationSmoothTime);

			// rotate to face input direction relative to camera position
			transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

			Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

			// _move the player
			controller.Move(targetDirection.normalized * (targetSpeed * Time.deltaTime) +
							 new Vector3(0.0f, _velocity, 0.0f) * Time.deltaTime);
		}
	}
}
