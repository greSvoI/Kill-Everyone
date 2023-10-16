using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	[ExecuteInEditMode]
	public class CrosshairSwitcher : MonoBehaviour
	{
		public CinemachineStateDrivenCamera drivenCamera;

		public Crosshair[] crosshairs;
		public Crosshair currentCrosshair;

		public LayerMask layerMask;
		public float radiusSphere;
		public bool enemy;
		private Camera mainCamera;
		private Animator animator;

		private int _currentWeapon=0;

		RaycastHit hitInfo;
		private void Start()
		{
			EventManager.Aim += OnAim;
			EventManager.Weapon += OnWeapon;

			mainCamera = Camera.main;
			animator = GetComponent<Animator>();
			crosshairs = GetComponentsInChildren<Crosshair>();

		}
		private void Update()
		{
			enemy = Physics.SphereCast(mainCamera.transform.position, radiusSphere, mainCamera.transform.forward, out hitInfo, 999f, layerMask);
		}
		private void OnDrawGizmos()
		{
			
			//if(enemy)
			//{
			//	Gizmos.color = Color.black;
			//	Gizmos.DrawSphere(hitInfo.point,radiusSphere);
			//}
			//else
			//{
			//	Gizmos.color = Color.yellow;
			//	Gizmos.DrawSphere(hitInfo.point,radiusSphere);
			//}
		}
		private void OnWeapon(int obj)
		{
			_currentWeapon = obj;
			animator.SetInteger("WeaponID", _currentWeapon);
			foreach (Crosshair crosshair in crosshairs)
			{
				if(crosshair.weaponID == obj) {
					crosshair.noAim.SetActive(true);
					currentCrosshair = crosshair;
				}
				else
				{
					crosshair.aim.SetActive(false);
					crosshair.noAim.SetActive(false);
				}
			}
		}

		private void OnAim(bool obj)
		{
			if(currentCrosshair != null)
			currentCrosshair.Switch(obj);

			if(obj && _currentWeapon != 0)
				drivenCamera.Priority = 11;
			else
				drivenCamera.Priority = 1;
		}
		private void OnDestroy()
		{
			EventManager.Aim -= OnAim;
			EventManager.Weapon -= OnWeapon;
		}
	}
}
