using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class IKController : MonoBehaviour
	{
		private Animator animator;
		[SerializeField] private Transform leftHand;
		[SerializeField] private Transform rightHand;

		[Space(2)]
		[Header("Weight IK")]
		[SerializeField] private float leftHandWeight = 0f;
		[SerializeField] private float rightHandWeight = 0f;

		[SerializeField] private float lookWeight;
		[SerializeField] private float bodyWeight;
		[SerializeField] private float headWeight;
		[SerializeField] private float eyesWeight;
		[SerializeField] private float clampWeight;
		private void Start()
		{
			animator = GetComponent<Animator>();
		}

		private void OnAnimatorIK(int layerIndex)
		{
			//animator.SetLookAtPosition(lookAtPosition.position);
			//animator.SetLookAtWeight(lookWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
			//LeftHand
			if (leftHand != null)
			{
				animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
				animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);

				animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
				animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
			}

			//RightHand
			if (rightHand != null)
			{
				animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
				animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);

				animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
				animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
			}

			

		}
	}
}
