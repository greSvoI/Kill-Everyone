using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace KillEveryone
{
	public class RigController : MonoBehaviour
	{
		[SerializeField] public Rig aimRigLayer;
		[SerializeField] public Rig handRigLayer;
		[SerializeField] private WeaponController weaponController;
		[SerializeField] private float _aimDuraton = 0.2f;

		
		public bool _aimining = false;
 		private void Start()
		{
			weaponController = GetComponent<WeaponController>();
			EventManager.Aim += OnAim;
		}

		private void Update()
		{
			if(_aimining && weaponController._isEquip)
			{
				aimRigLayer.weight += Time.deltaTime / _aimDuraton;
				handRigLayer.weight += Time.deltaTime / _aimDuraton;
				
			}
			else
			{
				aimRigLayer.weight -= Time.deltaTime / _aimDuraton;
				handRigLayer.weight -= Time.deltaTime / _aimDuraton;
			}
		}
		private void OnAim(bool obj)
		{
			_aimining = obj;
		}
		private void OnDestroy()
		{
			EventManager.Aim -= OnAim;
		}
	}
}
