using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

namespace KillEveryone
{
	public class AIHitBox : MonoBehaviour
	{
		public enum BodyParts { Head, Body, BodyHandPart, BodyLegPart }

		[SerializeField] private BodyParts bodyPart;
		[SerializeField] private GameObject rigidMesh;
		[SerializeField] private GameObject bodyMesh;
		private AIController controller;
		private float _health;
		private float _timeDestroy;

		private void Start()
		{
			controller = GetComponentInParent<AIController>();
			if (bodyPart == BodyParts.Body) _health = controller.dataZombie.BodyHealth;
			else if(bodyPart == BodyParts.Head) _health = controller.dataZombie.HeadHealth;
			else _health = controller.dataZombie.BodyPartHealth;

			_timeDestroy = controller.dataZombie.TimeLifeAfterDie;
		}
		public void TakeDamageBodyPart(float damage, Vector3 direction)
		{
			controller.TakeDamage();
			_health -= damage;
			if (_health <= 0)
			{
				switch (bodyPart)
				{
					case BodyParts.Head:
						BodyPart(direction);
						controller.SetRagdoll(false);
						gameObject.SetActive(false);
						break;
					case BodyParts.Body:
						controller.SetRagdoll(false);
						break;
					case BodyParts.BodyHandPart:
						BodyPart(direction);
						BodyPartChild(direction);
						break;
					case BodyParts.BodyLegPart:
						controller.IsCriticalDamage = true;
						BodyPart(direction);
						BodyPartChild(direction);
						break;
				}

			}
		}
		private void BodyPartChild(Vector3 direction)
		{
			AIHitBox[] aIHit = gameObject.GetComponentsInChildren<AIHitBox>();
			foreach (AIHitBox hitBox in aIHit)
			{
				if (hitBox != this)
				{
					hitBox.bodyMesh.SetActive(false);
					hitBox.rigidMesh.GetComponent<MeshRenderer>().enabled = true;
				}
			}
			gameObject.SetActive(false);
		}
		private void BodyPart(Vector3 direction)
		{
			bodyMesh.SetActive(false);

			if (rigidMesh.TryGetComponent<BodyPart>(out BodyPart part))
			{
				part.TimeDestroy = controller.dataZombie.TimeLifeAfterDie;
				part.Active();
			}
			else
			{
				rigidMesh.GetComponent<MeshRenderer>().enabled = true;
				rigidMesh.GetComponent<Rigidbody>().isKinematic = false;
			}
			
			rigidMesh.transform.parent = null;
		}
	}
}
