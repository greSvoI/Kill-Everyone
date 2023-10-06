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

		[Header("Part of the body")]
		[Header("Select body if there are no parts")]
		[SerializeField] private BodyParts bodyPart;

		[Header("One part body")]
		[SerializeField] private GameObject bodyPartMesh;
		[SerializeField] private GameObject bodyMesh;

		[Header("Many part body")]
		[SerializeField] private GameObject[] bodyPartMeshs;
		[SerializeField] private GameObject[] bodyMeshs;

		[Header("Strenght fly")]
		[SerializeField] private float _explosionForce = 25f;
		[SerializeField] private float _explosionRadius = 25f;

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
		public void TakeDamageBodyPart(float damage,Vector3 position)
		{
			controller.TakeDamage(damage);
			if (bodyPart == BodyParts.Body)
				return;

			_health -= damage;
		
			if(_health<=0)
			{
				switch (bodyPart)
				{
					case BodyParts.Head:
						BodyPart(position, false);
						controller.SetRagdoll(false, damage);
						gameObject.SetActive(false);
						break;
					case BodyParts.BodyHandPart:
						BodyPart(position, false);
						break;
					case BodyParts.BodyLegPart:
						controller.IsCriticalDamage = true;
						BodyPart(position, false);
						break;
				}
			}
		}
		public void BodyPart(Vector3 position,bool kinematic)
		{
			
			if(bodyMesh)
			{
				if(bodyMesh.TryGetComponent<SkinnedMeshRenderer>(out SkinnedMeshRenderer meshRenderer))
					meshRenderer.enabled = false;
				else
					bodyMesh.GetComponent<MeshRenderer>().enabled = false;
			}

			
			
			if(bodyPartMesh)
			{
				bodyPartMesh.transform.position = position - bodyPartMesh.GetComponent<MeshFilter>().mesh.bounds.center;
				bodyPartMesh.transform.parent = null;
				bodyPartMesh.GetComponent<BodyPart>().Active(position, kinematic);
			}


				AIHitBox[] hitBox = gameObject.GetComponentsInChildren<AIHitBox>();
				foreach (AIHitBox box in hitBox)
				{
					if (box != this)
					{
						box.bodyMesh.SetActive(false);
						if(bodyPartMesh)
						box.bodyPartMesh.GetComponent<BodyPart>().Active(position, !kinematic);
					}
				}
				gameObject.SetActive(false);
			
			
				foreach(var mesh in bodyMeshs)
				mesh.SetActive(false);

			foreach (var part in bodyPartMeshs)
				{
					part.GetComponent<MeshRenderer>().enabled = true;
					Rigidbody rb = part.GetComponent<Rigidbody>();
					rb.isKinematic = false;
					rb.AddExplosionForce(_explosionForce,transform.position,_explosionRadius);
				}

			
		}
	}
}
