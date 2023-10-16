using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class Rocket : MonoBehaviour
	{
		public bool fly = false;
		public float t = 0;
	 	public Vector3 p0;
		public Vector3 p1;
		public Vector3 p2;
		public Vector3 p3;

		RaycastHit hit;

		public ParticleSystem explosion;
		public bool isTrigger = false;

		private void Update()
		{
			if(fly)
			{
				t += Time.deltaTime;
				transform.position = GetPoint(p0,p1,p2,p3,t);
			}
		}
		public void StartFly(Vector3 p0,Vector3 hit,ParticleSystem particle)
		{
			
			explosion = particle;
			this.p0 = p0;
			this.p3 = hit;
			p1 = p0 + Vector3.forward + Vector3.up * 2f;
			p2 = p3 - Vector3.forward + Vector3.up * 2f;
			fly = true;
		}
		public Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			Vector3 p01 = Vector3.Lerp(p0, p1, t);
			Vector3 p12 = Vector3.Lerp(p1, p2, t);
			Vector3 p23 = Vector3.Lerp(p2, p3, t);

			Vector3 p012 = Vector3.Lerp(p01, p12, t);
			Vector3 p123 = Vector3.Lerp(p12, p23, t);

			Vector3 p = Vector3.Lerp(p012, p123, t);
			return p;
		}
		private void OnCollisionEnter(Collision collision)
		{
			Debug.Log(collision.collider.gameObject.layer);
		}
		private void OnTriggerEnter(Collider other)
		{

			if(other.gameObject.layer != LayerMask.NameToLayer("Player"))
			if(!isTrigger)
			{
				if (other.gameObject.TryGetComponent<AIHitBox>(out AIHitBox hitBox))
				{
					hitBox.TakeDamageExplosion(other.gameObject.transform.position);
				}
				this.gameObject.GetComponent<ParticleSystem>().Stop();
				ParticleSystem obj = Instantiate(explosion, transform);
				obj.transform.position = transform.position;
				isTrigger = true;
			}
		}

		private IEnumerator DestroyObject(float v)
		{
			yield return new WaitForSeconds(v);
			Destroy(this.gameObject);
		}
	}
}
