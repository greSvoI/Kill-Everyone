using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace KillEveryone
{
	public class WeaponController : MonoBehaviour
	{
		public class Bullet
		{

		}
		[SerializeField] private IKController ikController;
		[SerializeField] private AnimatorController animatorController;
		private AudioController audioController;


		[SerializeField] private Weapon currentWeapon;

		[SerializeField] private Weapon[] handHolder;
		[SerializeField] private WeaponInventory[] bodyHolder;

		[SerializeField] private TextMeshProUGUI bullet;

		public bool _isFiring = false;
		public bool _isEquip = false;

		public Transform rayCastOrigin;
		public Transform rayCastDestination;

		public TrailRenderer traceEffect;

		public ParticleSystem metallEffect;
		public ParticleSystem stoneEffect;
		public ParticleSystem woodEffect;
		public ParticleSystem enemyEffect;

		public float _fireRate = 0.1f;
		public float _damage;
		public int lastWeaponID = 0;
		public int currentWeaponID = 0;

		float _lastShot = 0f;

		Ray ray;
		RaycastHit hitInfo;

		bool _hideWeapon = false;


		public bool IsEquip => _isEquip;

		private void Start()
		{
			ikController = GetComponent<IKController>();
			animatorController = GetComponent<AnimatorController>();

			EventManager.Weapon += OnWeapon;
			EventManager.Reload += OnReload;
			EventManager.Fire += OnFire;
		}

		private void OnReload()
		{
			if(_isEquip)
			{
				audioController.PlayReload();
			}
		}

		private void OnWeapon(int obj)
		{
			//Hide Weapon
            if (obj == 0 && _isEquip)
            {
				_hideWeapon = true;
				_isEquip = false;
				lastWeaponID = currentWeaponID;
				animatorController.EquipWeapon(lastWeaponID);
            }
			///Draw Weapon
			else
			{
				if(currentWeaponID == 0)
				{
					currentWeaponID = lastWeaponID = obj;
					animatorController.EquipWeapon(currentWeaponID);
				}

				else
				{
					lastWeaponID = currentWeaponID;
					currentWeaponID = obj;
					animatorController.EquipWeapon(currentWeaponID);
					
				}

			}
        }

		public void Equip(Weapon newWeapon)
		{
			//currentWeapon = newWeapon;
			//currentWeapon.transform.parent = weaponHolder;
			//currentWeapon.transform.localPosition = Vector3.zero;
			//currentWeapon.transform.localRotation = Quaternion.identity;

			////leftHand.localPosition = currentWeapon.leftHandIK.localPosition;
			////leftHand.localRotation = currentWeapon.leftHandIK.localRotation;

			////rightHand.localPosition = currentWeapon.rightHandIK.localPosition;
			////rightHand.localRotation = currentWeapon.rightHandIK .localRotation;

			////rayCastOrigin = currentWeapon.muzzle;
			//_isEquip = true;
		}

		private void Update()
		{
			if (currentWeapon && _isEquip)
			{
				if (_isFiring && ikController.LeftHandWeight > 0.9f)
				{
					if (Time.time > _lastShot + currentWeapon.fireRate)
					{
						Shoot();
						_lastShot = Time.time;
					}
				}
			}
			BulletCheck();
		}

		private void BulletCheck()
		{
			if(currentWeapon)
			bullet.text = currentWeapon.bulletClip + " / " + currentWeapon.bulletCount;
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
			rayCastOrigin = currentWeapon.Muzzle;
			if(currentWeapon.bulletClip == 0)
			{
				//audioController.PlayEmpty();
				return;
			}
			currentWeapon.bulletClip--;
			currentWeapon.bulletCount--;
			
			currentWeapon.Fire();
			ray.origin = rayCastOrigin.position;
			ray.direction = rayCastDestination.position - rayCastOrigin.position;

			var tracer = Instantiate(traceEffect, ray.origin, Quaternion.identity);
			tracer.AddPosition(ray.origin);

			if(Physics.Raycast(ray, out hitInfo))
			{				


				switch (hitInfo.collider.gameObject.layer)
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

						if (hitInfo.collider.TryGetComponent<AIHitBox>(out AIHitBox hitBox))
						{
							hitBox.TakeDamageBodyPart(_damage, hitInfo.point);
						}

						
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

		public void DrawWeapon(int index)
		{

			if(_isEquip && !_hideWeapon)
			{
				foreach(Weapon weapon in handHolder)
				{
					if(weapon.weaponID == currentWeaponID)
					{
						weapon.IsActive = true;
						currentWeapon = weapon;
						SetParamWeapon();
					}
					if(weapon.weaponID == lastWeaponID)
						weapon.IsActive = false;
				}
				
				foreach(WeaponInventory weapon in bodyHolder)
				{
					if(weapon.WeaponID == currentWeaponID)
						weapon.IsActive = false;
					if(weapon.WeaponID == lastWeaponID)
						weapon.IsActive = true;
				}
				
			}
			else
			{
				//not working ???
				///handHolder.First(x => x.weaponID == currentWeaponID).IsActive = _isStateWeapon;
				///bodyHolder.First(x=> x.weaponID == currentWeaponID).IsActive = !_isStateWeapon;
				
				
				bool state = true;

				if (_hideWeapon)
				{
					state = false;
				}
				else
					_isEquip = true;

				foreach (Weapon weapon in handHolder)
				{
					if (weapon.weaponID == currentWeaponID)
					{
						weapon.IsActive = state;
						currentWeapon = weapon;
						SetParamWeapon();
						
					}
				}
				foreach (WeaponInventory weapon in bodyHolder)
				{
					if (weapon.WeaponID == currentWeaponID)
						weapon.IsActive = !state;
				}
				if(_hideWeapon)
				{
					currentWeapon = null;
					_hideWeapon = false;
					currentWeaponID = lastWeaponID = 0;
				}
				
			}

		}
		private void SetParamWeapon()
		{
			_fireRate = currentWeapon.fireRate;
			_damage = currentWeapon.damage;
			ikController.LeftHand = currentWeapon.LeftHand;
			ikController.RightHand = currentWeapon.RightHand;
		}
		private void OnDestroy()
		{
			EventManager.Fire -= OnFire;
		}
	}
}
