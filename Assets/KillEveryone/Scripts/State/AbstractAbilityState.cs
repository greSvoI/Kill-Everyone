using System;
using UnityEngine;

namespace KillEveryone
{
	public abstract class AbstractAbilityState : MonoBehaviour
	{
		[SerializeField] private int statePriority = 0;

		public bool IsStateRunning { get; private set; }

		public event Action<AbstractAbilityState> abilityStopped = null;
		public event Action<AbstractAbilityState> abilityStarted = null;

		public int StatePriority { get { return statePriority; } }

		protected PlayerInput input;
		protected AnimatorController animatorController;
		protected MovementController moveController;
		protected DetectionController detectionController;



		// start time and stop time
		public float StartTime { get; private set; } = 0;
		public float StopTime { get; private set; } = 0;

		protected virtual void Start()
		{
			moveController = GetComponent<MovementController>();
			detectionController = GetComponent<DetectionController>();
			animatorController = GetComponent<AnimatorController>();
			input = GetComponent<PlayerInput>();
			
		}

		public void StartState()
		{
			IsStateRunning = true;
			StartTime = Time.time;
			OnStartState();
			abilityStarted?.Invoke(this);
		}
		public void StopState()
		{
			if (Time.time - StartTime < 0.1f)
				return;

			IsStateRunning = false;
			StopTime = Time.time;
			OnStopState();
			abilityStopped?.Invoke(this);

		}

		public abstract bool ReadyToStart();

		public abstract void OnStartState();

		public abstract void UpdateState();
		public abstract void FixedUpdateState();

		public abstract void OnStopState();

    }
}
