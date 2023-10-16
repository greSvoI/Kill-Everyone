using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class Locomotion : AbstractAbilityState
	{
		[SerializeField] private string _animatorStateName;
		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _sprintSpeed;
		[SerializeField] private float _duration = 0.2f;
		public override void FixedUpdateState()
		{
			
		}

		public override void OnStartState()
		{
			animatorController.SetAnimationState(_animatorStateName, 0,_duration);
		}

		public override void OnStopState()
		{
			
		}

		public override bool ReadyToStart()
		{
			return detectionController.IsGrounded();
		}

		public override void UpdateState()
		{
			float speed = input.Sprint ? _moveSpeed : _sprintSpeed;
			moveController.Move(input.Move, speed);
		}
	}
}
