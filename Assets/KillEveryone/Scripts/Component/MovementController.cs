using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

namespace KillEveryone
{
	public class MovementController : MonoBehaviour
	{
		private CharacterController controller;
		private PlayerInput input;

		private Transform mainCamera;

		[Tooltip("How fast the character turns to face movement direction")]
		[Range(0.0f, 0.3f)]
		[SerializeField] private float RotationSmoothTime = 0.12f;

		[Tooltip("Acceleration and deceleration")]
		[SerializeField] private float SpeedChangeRate = 10.0f;

		private Vector3 velocity;
		private float _gravity = -9.81f;
		private float _gravityMultiply = 3f;
		private float _rotationVelocity;
		private float _speed;


		private void Start()
		{
			controller = GetComponent<CharacterController>();
			input = GetComponent<PlayerInput>();
			mainCamera = Camera.main.transform;
		}
		private void Update()
		{
			ApllyGravity();
			if(input.Move != Vector2.zero)
			{
				Move(input.Move, 5f);
			}
			controller.Move(velocity * Time.deltaTime);
		}
		public void ApllyGravity()
		{
			if(controller.isGrounded && velocity.y < 0.0f)
			{
				velocity.y = -1.0f;
			}
			else
			{
				velocity.y += _gravity * _gravityMultiply * Time.deltaTime;
			}
			
		}
		public void Move(Vector2 moveInput, float targetSpeed, bool rotateCharacter = true)
		{
			
			float targetRotation = 0f;
		    _speed = Mathf.Lerp(_speed, targetSpeed * input.Move.magnitude, Time.deltaTime * SpeedChangeRate);

			//if (_speed < 0.3f) _speed = 0f;

			if (moveInput != Vector2.zero)
			{
				targetRotation = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg +  mainCamera.transform.eulerAngles.y;
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, RotationSmoothTime);


				if (rotateCharacter)
					transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}

			Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
			velocity = targetDirection.normalized * _speed + new Vector3(0.0f, velocity.y, 0.0f);

		}
		private void OnAnimatorMove()
		{
			
		}
	}
}
