using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class AnimatorController : MonoBehaviour
	{
		private Animator animator;
		private PlayerInput input;

		[Header("Parametrs name")]
		[SerializeField] private string horizontal;
		[SerializeField] private string vertical;
		[SerializeField] private string magnituda;

		private int _hashHorizontal;
		private int _hashVertical;
		private int _hashMagnituda;

		private void Start()
		{
			animator = GetComponent<Animator>();
			input = GetComponent<PlayerInput>();
			HashAnimationName();
		}
		private void HashAnimationName()
		{
			_hashHorizontal = Animator.StringToHash(horizontal);
			_hashVertical = Animator.StringToHash(vertical);
			_hashMagnituda = Animator.StringToHash(magnituda);
		}

		private void Update()
		{
               
        }
		public bool HasFinishedAnimation(string stateName, int layerIndex = 0)
		{
			var stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);

			if (animator.IsInTransition(layerIndex)) return false;

			if (stateInfo.IsName(stateName))
			{
				float normalizeTime = Mathf.Repeat(stateInfo.normalizedTime, 1);
				if (normalizeTime >= 0.95f) return true;
			}

			return false;
		}

		public bool HasFinishedAnimation(float time,int layerIndex = 0)
		{
			return animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime >= time && !animator.IsInTransition(layerIndex);
		}
		public void PlayAnimation(string name,int layer)
		{
			
		}
		public void SetAnimationState(string stateName, int layerIndex = 0, float transitionDuration = 0.1f)
		{
			if (animator.HasState(layerIndex, Animator.StringToHash(stateName)))
				animator.CrossFadeInFixedTime(stateName, transitionDuration, layerIndex);
		}
		public void MovementUpdate(float magnituda)
		{
			animator.SetFloat(_hashMagnituda,magnituda, 0.2f, Time.deltaTime);
		}
		public void FallingUpdate()
		{

		}
		public void StrafeUpdate()
		{

		}
		private void OnFootstep()
		{

		}
	}
}
