using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class Strafe : AbstractAbilityState
	{
		[SerializeField] private string _animatorStateName;
		[SerializeField] private float _speed = 2f;
		[SerializeField] private float _duration = 0.2f;
		[SerializeField] private float _speedRotate = 2f;

		public override void FixedUpdateState()
		{
			
		}

		public override void OnStartState()
		{
			animatorController.SetAnimationState(_animatorStateName, 0, _duration);
		}

		public override void OnStopState()
		{
			
		}

		public override bool ReadyToStart()
		{
			return input.Aim && detectionController.IsGrounded();
		}

		public override void UpdateState()
		{

			moveController.Move( input.Move, _speed, false);

			Quaternion rot = Quaternion.LookRotation(Camera.main.transform.forward);
			transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * _speedRotate);
			transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

			if (!input.Aim)
				StopState();
		}
	}
}
