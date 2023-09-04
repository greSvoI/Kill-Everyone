using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class WeaponController : MonoBehaviour
	{
		public class Bullet
		{

		}

		[SerializeField] private Weapon weapon;
		[SerializeField] private Transform leftHand;
		[SerializeField] private Transform rightHand;

		public bool _isFiring = false;
		public bool _isEquip = false;

		public RigController rigLayers;

		public Transform rayCastOrigin;
		public Transform rayCastDestination;
		[SerializeField] private Transform weaponHolder;

		public TrailRenderer traceEffect;

		public ParticleSystem metallEffect;
		public ParticleSystem stoneEffect;
		public ParticleSystem woodEffect;
		public ParticleSystem enemyEffect;

		public float _fireRate = 0.3f;

		float _lastShot;

		Ray ray;
		RaycastHit hitInfo;

		private void Start()
		{
			rigLayers = GetComponent<RigController>();
			EventManager.Fire += OnFire;
			Weapon exist = GetComponentInChildren<Weapon>();
			if(exist)
			{
				Equip(exist);
			}
		}

		public void Equip(Weapon newWeapon)
		{
			weapon = newWeapon;
			weapon.transform.parent = weaponHolder;
			weapon.transform.localPosition = Vector3.zero;
			weapon.transform.localRotation = Quaternion.identity;

			leftHand.localPosition = weapon.leftHandIK.localPosition;
			leftHand.localRotation = weapon.leftHandIK.localRotation;

			rightHand.localPosition = weapon.rightHandIK.localPosition;
			rightHand.localRotation = weapon.rightHandIK .localRotation;

			rayCastOrigin = weapon.muzzle;
			_isEquip = true;
		}

		private void Update()
		{
			if(weapon && _isEquip)
			{
				if (_isFiring && rigLayers.aimRigLayer.weight > 0.9f)
				{

					if (Time.time > _lastShot + _fireRate)
					{
						Shoot();
						_lastShot = Time.time;
					}
				}
			}
		}
		private void OnFire(bool obj)
		{
			_isFiring = obj;
		}
		private void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			if(rayCastOrigin != null)
			Gizmos.DrawLine(rayCastOrigin.transform.position, rayCastDestination.transform.position);
		}
		public void Shoot()
		{
			
			_isFiring = true;
			weapon.Fire();
			ray.origin = rayCastOrigin.position;
			ray.direction = rayCastDestination.position - rayCastOrigin.position;

			var tracer = Instantiate(traceEffect, ray.origin, Quaternion.identity);
			tracer.AddPosition(ray.origin);
			if(Physics.Raycast(ray, out hitInfo))
			{
				
				switch(hitInfo.collider.gameObject.layer)
				{
					case 6:
						HitEffect(metallEffect);
					break;
						
					case 7:
						HitEffect(woodEffect);
						break;
					case 8:
						HitEffect(stoneEffect);
						break;
					case 9:
						HitEffect(enemyEffect);
						hitInfo.collider.GetComponent<AIHitBox>().TakeDamage(10, ray.direction);
						break;
					default:
						HitEffect(stoneEffect);
						break;
				}

				tracer.transform.position = hitInfo.point;
			}
			

		}
		private void HitEffect(ParticleSystem particle)
		{
			particle.transform.position = hitInfo.point;
			particle.transform.forward = hitInfo.normal;
			particle.Emit(1);
		}

		private void OnDestroy()
		{
			EventManager.Fire -= OnFire;
		}
	}
}
