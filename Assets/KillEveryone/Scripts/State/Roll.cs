using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class Roll : AbstractAbilityState
	{
		public override void FixedUpdateState()
		{
			
		}

		public override void OnStartState()
		{
			
		}

		public override bool ReadyToStart()
		{
			return input.Roll && detectionController.IsGrounded();
		}

		public override void UpdateState()
		{
			
		}
	}
}
