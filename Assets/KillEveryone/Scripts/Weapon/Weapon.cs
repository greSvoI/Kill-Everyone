using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public enum DrawWeaponID
	{
		LowLeft = 1, LowRight = 2,  HighLeft = 3, HighRight = 4
	}
	public class Weapon : MonoBehaviour
	{
	
		[SerializeField] private GameObject render;
		[SerializeField] private ParticleSystem []fireEffect;
		[SerializeField] private DataWeapon dataWeapon;

		[SerializeField] private Transform rightHandIK;
		[SerializeField] private Transform leftHandIK;
	    [SerializeField] private Transform muzzle;

		public int weaponID;
		public int bulletCount;
		public int bulletClip;
		public int bulletMax;
		public float fireRate;
		public float damage;

		public Transform Muzzle => muzzle;
		public Transform LeftHand => leftHandIK;
		public Transform RightHand => rightHandIK;

		public bool IsActive {  set => render.SetActive(value); }

		private void Start()
		{
			bulletCount = dataWeapon.BulletCount;
			bulletClip = dataWeapon.BulletClip;
			bulletMax = dataWeapon.BulletMax;
			fireRate = dataWeapon.FireRate;
			weaponID = dataWeapon.WeaponID;
			damage = dataWeapon.Damage;
		}
		private void Update()
		{
			
		}
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawRay(muzzle.position, transform.forward);
		}
		public void Fire()
		{
			foreach (var p in fireEffect)
			{
				p.Emit(1);
			}
		}
		public void Reload()
		{
			
		}
	}
}
