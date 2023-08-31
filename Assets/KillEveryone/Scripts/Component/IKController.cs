using System;
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

		[SerializeField] private Transform spine1;
		[SerializeField] private Transform lookAtPosition;


		[Space(2)]
		[Header("Weight IK")]
		[SerializeField] private float leftHandWeight = 0f;
		[SerializeField] private float rightHandWeight = 0f;

		[SerializeField] private float _lookWeight;
		[SerializeField] private float _bodyWeight;
		[SerializeField] private float _headWeight;
		[SerializeField] private float _eyesWeight;
		[SerializeField] private float _clampWeight;

		public Vector3 pos;

		private void Start()
		{
			animator = GetComponent<Animator>();
			EventManager.Aim += OnAim;
		}
		private void Update()
		{
			//LeftHand();
		}
		[ContextMenu("Print1")]
		private void LeftHand1()
		{
			float positionY = (spine1.localEulerAngles.y / 100);
			positionY = Mathf.Clamp(positionY, 0.1f, 0.0f);
			Vector3 temp = Vector3.zero;
			temp = leftHand.localPosition;
			Debug.Log(temp);
			temp.y = positionY;

			leftHand.position = temp;
		}
		private void LeftHand()
		{
			float positionY = (spine1.localEulerAngles.y / 100);
			positionY = Mathf.Clamp(positionY, 0.1f, 0.0f);
			Vector3 temp = Vector3.zero;
			temp = leftHand.position;
			temp.y = positionY;

			leftHand.position = temp;
		}

		#region EventManager
		private void OnAim(bool obj)
		{
			leftHandWeight = obj ? 1f : 0f;
		}
		#endregion

		private void OnAnimatorIK(int layerIndex)
		{
			animator.SetLookAtPosition(lookAtPosition.position);
			animator.SetLookAtWeight(_lookWeight,_bodyWeight,_headWeight,_eyesWeight,_clampWeight);
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
		private void OnDestroy()
		{
			EventManager.Aim -= OnAim;
		}
	}
}
