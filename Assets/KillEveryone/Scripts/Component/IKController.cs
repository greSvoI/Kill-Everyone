using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace KillEveryone
{
	public class IKController : MonoBehaviour
	{
		private Animator animator;
		private PlayerInput input;
		private AnimatorController animatorController;
		private WeaponController weaponController;
		

		[Header("IK Weapon Hand")]
		[SerializeField] private Transform leftHand;
		[SerializeField] private Transform leftHint;

		[SerializeField] private Transform rightHand;
		[SerializeField] private Transform rightHint;

		[SerializeField] private Transform spine1;
		[SerializeField] private Transform lookAtPosition;

		[Header("IK Weapon Holder")]
		[SerializeField] private Transform rightHolder;
		[SerializeField] private Transform leftHolder;

		[Space(2)]
		[Header("Weight IK")]
		[SerializeField] private float _leftHandWeight = 0f;
		[SerializeField] private float _rightHandWeight = 0f;

		[SerializeField] private float _lookWeight;
		[SerializeField] private float _bodyWeight;
		[SerializeField] private float _headWeight;
		[SerializeField] private float _eyesWeight;
		[SerializeField] private float _clampWeight;

		public Vector3 pos;

		Coroutine returAim;

		[SerializeField]  private float _upperAimDuration = 0.2f;
		[SerializeField]  private float _lowerAimDuration = 0.2f;

		public float LeftHandWeight => _leftHandWeight;
		public Transform LeftHand { get=>leftHand; set => leftHand = value; }
		public Transform RightHand { get=>rightHand; set => rightHand = value; }

		private void Start()
		{
			animator = GetComponent<Animator>();
			input = GetComponent<PlayerInput>();
			animatorController = GetComponent<AnimatorController>();
			weaponController = GetComponent<WeaponController>();
			

			EventManager.Aim += OnAim;
			EventManager.Weapon += OnWeapon;
			EventManager.Reload += OnReload;
		}

		private void OnReload()
		{
			OnWeapon(1);
		}

		private void OnWeapon(int obj)
		{
			if (obj != 0)
			{
				_rightHandWeight = 0f;
				_leftHandWeight = 0f;
				//StartCoroutine(ReturnAim());
			}
			else
				_leftHandWeight = 0.5f;
		}

		private IEnumerator ReturnAim()
		{
			bool draw = true;
			while (draw)
			{
				yield return null;
				if(!animatorController.IsDrawWeapon && !animatorController.IsReload) draw = false;
			}

			input.Aim = true;
		}
		private void FixedUpdate()
		{
			
		}
		private void Update()
		{
			if (weaponController.IsEquip)
			{

				if (animatorController.IsReload || animatorController.IsDrawWeapon)
				{
					LowerHandWeight();
					return;
				}

				if (input.Aim || input.Fire)
				{
					UpperHandWeight();
				}
				else
				{
					LowerHandWeight();
				}

			}
			else
			{
				LowerHandWeight();
			}

		}
		private void LowerHandWeight()
		{
			if (_leftHandWeight > 0f || _rightHandWeight > 0f)
			{
				_rightHandWeight -= Time.deltaTime / _lowerAimDuration;
				_leftHandWeight -= Time.deltaTime / _lowerAimDuration;
			}
		}
		private void UpperHandWeight()
		{
			if (_leftHandWeight < 1f )
			{
				if(_rightHandWeight < 1f)
				_rightHandWeight += Time.deltaTime / _upperAimDuration;

				//if(_rightHandWeight > 0.9f)
					_leftHandWeight += Time.deltaTime / _upperAimDuration;
			}
		}

		#region EventManager
		private void OnAim(bool obj)
		{
			
		}
		#endregion

		private void OnAnimatorIK(int layerIndex)
		{
			animator.SetLookAtPosition(lookAtPosition.position);
			animator.SetLookAtWeight(_lookWeight,_bodyWeight,_headWeight,_eyesWeight,_clampWeight);
			//LeftHand
			if (leftHand != null)
			{
				animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _leftHandWeight);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _leftHandWeight);

				animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
				animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);

				animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftHint.position);
				animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, _leftHandWeight);
			}

			//RightHand
			if (rightHand != null)
			{
				rightHand.LookAt(lookAtPosition.position);

				Vector3 rot = rightHand.rotation.eulerAngles;
				Quaternion quat = Quaternion.Euler(rot.x, rot.y, -90f);

				rightHand.rotation = Quaternion.Lerp(Quaternion.Euler(rot), quat, 1f);


				animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightHandWeight);
				animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);

				animator.SetIKRotationWeight(AvatarIKGoal.RightHand, _rightHandWeight);
				animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);

				animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightHint.position);
				animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, _rightHandWeight);
			}



		}
		private void OnDestroy()
		{
			EventManager.Aim -= OnAim;
			EventManager.Weapon -= OnWeapon;
			EventManager.Reload -= OnReload;
		}
	}
}
