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

		public override bool ReadyToStart()
		{
			return detectionController.IsGrounded();
		}

		public override void UpdateState()
		{

			if(input.Move != Vector2.zero)
			{
				float speed = input.Sprint ? _sprintSpeed : _moveSpeed;
				Vector2 direction = new Vector2(input.Move.x, input.Move.y).normalized;
				moveController.Move(speed,direction);
				float magnituda = input.Sprint ? 1f : 0.5f;
				animatorController.MovementUpdate(magnituda);
			}
			else
			{
				animatorController.MovementUpdate(0f);
			}
		}
	}
}
