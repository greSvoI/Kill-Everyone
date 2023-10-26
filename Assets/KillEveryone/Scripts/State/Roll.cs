using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class Roll : AbstractAbilityState
	{

		[SerializeField] private string freeRoll;
		[SerializeField] private string aimRoll;
		[SerializeField] private float rollSpeed;
		[SerializeField] private int _layerIndex = 0;
		[SerializeField] private float _duration = 0.2f;

		private int hashAimRoll;
		private int hashFreeRoll;

		public override void FixedUpdateState()
		{
			
		}

		public override void OnStartState()
		{
			animatorController.SetAnimationState(freeRoll, _layerIndex, _duration);
		}

		public override void OnStopState()
		{
			
		}

		public override bool ReadyToStart()
		{
			return input.Roll && detectionController.IsGrounded() && !input.Fire && !input.Reload;
		}

		public override void UpdateState()
		{
			moveController.StopMovement();
			moveController.Move(transform.forward * rollSpeed);

			if (animatorController.HasFinishedAnimation(0.8f))
				StopState();
		}
	}
}
