using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace KillEveryone
{
	public class AnimatorController : MonoBehaviour
	{
		private Animator animator;
		private PlayerInput input;
		private WeaponController weaponController;
	

		[Tooltip("Acceleration and deceleration")]
		[SerializeField] private float SpeedChangeRate = 10.0f;

		[Header("Parametrs name")]
		[SerializeField] private string isAiming;
		[SerializeField] private string isFire;
		[SerializeField] private string isDrawWeapon;
		[SerializeField] private string isReload;
		[SerializeField] private string horizontal;
		[SerializeField] private string vertical;
		[SerializeField] private string magnituda;
		[SerializeField] private string weaponID;

		[Header("Layers name")]
		[SerializeField] private string onlyArms;
		[SerializeField] private string upperBody;

		private float _animationBlend = 0f;

		private string drawWeapon = "Draw Weapon HighLeft";

		private int _hashHorizontal;
		private int _hashVertical;
		private int _hashMagnituda;
		private int _hashIsAiming;
		private int _hashIsFire;
		private int _hashIsDrawWeapon;
		private int _hashIsReload;
		private int _hashWeaponID;

		private int _layerOnlyArms;
		private int _layerUpperBody;


		public bool IsReload => animator.GetBool(_hashIsReload);
		public bool IsDrawWeapon => animator.GetBool(_hashIsDrawWeapon);

		private void Start()
		{
			animator = GetComponent<Animator>();
			input = GetComponent<PlayerInput>();
			weaponController = GetComponent<WeaponController>();
			HashAnimationName();

			EventManager.Aim += OnAim;
			EventManager.Fire += OnFire;
			EventManager.Weapon += OnWeapon;
			EventManager.Reload += OnReload;
		}

		private void OnReload()
		{
			if(!IsReload && !IsDrawWeapon)
				animator.SetBool(_hashIsReload, true);
			
		}

		public void EquipWeapon(int id)
		{
			if (!IsReload && !IsDrawWeapon)
			{
				animator.SetBool(_hashIsDrawWeapon, true);
				animator.SetInteger(_hashWeaponID, id);
			}
		}
		private void OnWeapon(int obj)
		{

			//if(!IsReload && !IsDrawWeapon)
			//{
			//	animator.SetBool(_hashIsDrawWeapon, true);
			//	animator.SetInteger(_hashWeaponID,obj);
			//}
		}

		private void OnFire(bool obj)
		{
			//animator.SetBool(_hashIsFire, obj);
		}

		private void OnAim(bool obj)
		{

		}

		private void HashAnimationName()
		{
			_hashHorizontal = Animator.StringToHash(horizontal);
			_hashVertical = Animator.StringToHash(vertical);
			_hashMagnituda = Animator.StringToHash(magnituda);
			_hashIsAiming = Animator.StringToHash(isAiming);
			_hashIsFire = Animator.StringToHash(isFire);
			_hashIsReload = Animator.StringToHash(isReload);
			_hashIsDrawWeapon = Animator.StringToHash(isDrawWeapon);
			_hashWeaponID = Animator.StringToHash(weaponID);

			_layerOnlyArms = animator.GetLayerIndex(onlyArms);
			_layerUpperBody = animator.GetLayerIndex(upperBody);
		}

		private void Update()
		{
			UpdateAnimatorParametrs();


		}
		private void FixedUpdate()
		{
			
		}
		public void UpdateAnimatorParametrs()
		{
			float magnituda = 0f;
			//if (input.Move == Vector2.zero)
			//{
			//	animator.SetFloat(_hashHorizontal, 0f, 0.2f, Time.deltaTime);
			//	animator.SetFloat(_hashVertical, 0f, 0.2f, Time.deltaTime);
				
			//}
			//else
			//{
			//	animator.SetFloat(_hashHorizontal,input.Move.x, 0.2f, Time.deltaTime);
			//	animator.SetFloat(_hashVertical, input.Move.y, 0.2f, Time.deltaTime);
			//	magnituda = input.Sprint ? input.Magnituda / 2f : input.Magnituda;
			//}


			magnituda = input.Sprint ? input.Magnituda / 2f : input.Magnituda;

			//if (magnituda > 0.0f && magnituda < 0.5f) magnituda = 0.5f;

			_animationBlend = Mathf.Lerp(_animationBlend, magnituda, Time.deltaTime * SpeedChangeRate);
			animator.SetFloat(_hashMagnituda, _animationBlend, 0.2f, Time.deltaTime);
			animator.SetFloat(_hashHorizontal, input.Move.x, 0.2f, Time.deltaTime);
			animator.SetFloat(_hashVertical, input.Move.y, 0.2f, Time.deltaTime);

			//if (_animationBlend < 0.1f)
			//{
			//	animator.SetFloat(_hashMagnituda, 0f);
			//	animator.SetFloat(_hashHorizontal, 0f);
			//	animator.SetFloat(_hashVertical, 0f);

			//}
			//else
			//{
			//	animator.SetFloat(_hashMagnituda, _animationBlend, 0.2f, Time.deltaTime);
			//	animator.SetFloat(_hashHorizontal, input.Move.x, 0.2f, Time.deltaTime);
			//	animator.SetFloat(_hashVertical, input.Move.y, 0.2f, Time.deltaTime);
			//}

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

		public void SetAnimationState(string stateName, int layerIndex = 0, float transitionDuration = 0.1f)
		{
			if (animator.HasState(layerIndex, Animator.StringToHash(stateName)))
				animator.CrossFadeInFixedTime(stateName, transitionDuration, layerIndex);
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
		private void OnDestroy()
		{
			EventManager.Aim -= OnAim;
			EventManager.Fire -= OnFire;
			EventManager.Weapon -= OnWeapon;
		}
	}
}
