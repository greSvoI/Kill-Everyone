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


		public bool _isFiring = false;

		public RigController rig;
		public Transform rayCastOrigin;
		public Transform rayCastDestination;
		public TrailRenderer traceEffect;

		public ParticleSystem[] fireEffect;


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
			rig = GetComponent<RigController>();
			EventManager.Fire += OnFire;
		}

		private void Update()
		{
			if (_isFiring && rig.aimRigLayer.weight > 0.9f) { 

				if(Time.time > _lastShot + _fireRate)
				{
					Shoot();
					_lastShot = Time.time;
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
			Gizmos.DrawLine(rayCastOrigin.transform.position, rayCastDestination.transform.position);
		}
		public void Shoot()
		{
			
			_isFiring = true;
			foreach (var p in fireEffect)
			{
				p.Emit(1);
			}
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
