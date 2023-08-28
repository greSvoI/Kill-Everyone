using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace KillEveryone
{
	public class CameraController : MonoBehaviour
	{

		private PlayerInput input;

		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 70.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -30.0f;
		[Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
		public float CameraAngleOverride = 0.0f;

		[SerializeField] private Transform _followCamera;

		[SerializeField] private float _speedChangeRate = 5f;
		[SerializeField] private float _sensivity = 5f;

		private float _cameraInputX;
		private float _cameraInputY;
		private void Start()
		{
			input = GetComponent<PlayerInput>();
		}

		private void Update()
		{
			
		}
		private void LateUpdate()
		{
			CameraRotation();
		}
		private void CameraRotation()
		{
			_cameraInputY += input.Look.y * _sensivity;
			_cameraInputX += input.Look.x * _sensivity;

			_cameraInputX = ClampAngle(_cameraInputX, float.MinValue, float.MaxValue);
			_cameraInputY = ClampAngle(_cameraInputY, BottomClamp, TopClamp);

			_followCamera.transform.rotation = Quaternion.Euler(_cameraInputY, _cameraInputX, 0f);
		}
		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}
	}

}