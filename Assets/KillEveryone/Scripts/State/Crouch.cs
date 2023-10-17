using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class Crouch : AbstractAbilityState
	{
		[SerializeField] private string _animatorStateName;
		[SerializeField] private float _speed = 2f;
		[SerializeField] private float _duration = 0.2f;
		public override void FixedUpdateState()
		{
			
		}

		public override void OnStartState()
		{
			animatorController.SetAnimationState(_animatorStateName,0,_duration);
		}

		public override bool ReadyToStart()
		{
			return input.Crouch && detectionController.IsGrounded();
		}

		public override void UpdateState()
		{
	
			Vector2 direction = new Vector2(input.Move.x, input.Move.y).normalized;
			moveController.Move(direction,_speed);
			
			if (!input.Crouch)
			{
				
				StopState();
			}
		}
		public override void OnStopState()
		{
			
			
		}
	}
}
