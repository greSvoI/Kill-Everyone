using System;
using UnityEngine;


namespace KillEveryone
{
	public class Weapon : MonoBehaviour
	{
	
		[SerializeField] private GameObject render;
		[SerializeField] private ParticleSystem []fireEffect;


		[SerializeField] private Transform rightHandIK;
		[SerializeField] private Transform leftHandIK;
	    [SerializeField] private Transform muzzle;
		[SerializeField] private AudioSource audioSource;

		public EnumClass.WeaponClass weaponClass;
		public EnumClass.WeaponHolder weaponHolder;

		public DataWeapon dataWeapon;

		public TrailRenderer tracerShootEffect;

		public int weaponID;
		public int animationID;
		public int bulletCount;
		public int bulletClip;
		public int bulletMax;
		public float fireRate;
		public float damage;

		public RaycastHit hitInfo;
		public Transform Muzzle => muzzle;
		public Transform LeftHand => leftHandIK;
		public Transform RightHand => rightHandIK;

		public bool IsActive {  set => render.SetActive(value); }

		private void Start()
		{
			weaponClass = dataWeapon.WeaponClass;
			weaponHolder = dataWeapon.WeaponHolder;

			audioSource = GetComponent<AudioSource>();
			bulletCount = dataWeapon.BulletCount;
			bulletClip = dataWeapon.BulletClip;
			bulletMax = dataWeapon.BulletMax;
			fireRate = dataWeapon.FireRate;
			weaponID = dataWeapon.WeaponID;
			damage = dataWeapon.Damage;

			tracerShootEffect = dataWeapon.tracerShootEffect;
		}
		
		public void Shoot()
		{
			foreach (var p in fireEffect)
			{
				p.Emit(1);
			}
			bulletClip--;
			bulletCount--;

			var tracer = Instantiate(tracerShootEffect, muzzle.position, Quaternion.identity);
			tracer.AddPosition(muzzle.position);
			tracer.transform.position = hitInfo.point;
		}

		public void Attack(RaycastHit hit)
		{
			hitInfo = hit;
			switch (weaponClass)
			{
				
				case EnumClass.WeaponClass.Rocket:
					StartRocket();
					break;
				case EnumClass.WeaponClass.Melee: 

					break;
				default:
					Shoot();
					break;
			}
			
		}

		private void StartRocket()
		{
			foreach (var p in fireEffect)
			{
				p.Emit(1);
			}
			var obj = Instantiate(dataWeapon.tracerRocketEffect,muzzle.position,Quaternion.identity);
			obj.GetComponent<Rocket>().StartFly(muzzle.position,hitInfo.point,dataWeapon.explosionEffect);
		}

		public void Reload()
		{
			
		}
	}
}
