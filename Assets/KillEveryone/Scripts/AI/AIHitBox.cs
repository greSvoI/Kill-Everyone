using UnityEngine;

namespace KillEveryone
{
	public class AIHitBox : MonoBehaviour
	{
		

		[Header("Part of the body")]
		[Header("Select body if there are no parts")]
		[SerializeField] private EnumClass.BodyParts bodyPart;

		[Header("One part body")]
		[SerializeField] private GameObject bodyPartMesh;
		[SerializeField] private GameObject bodySkinnedMesh;

		[Header("Many part body")]
		[SerializeField] private GameObject[] bodyPartMeshes;
		[SerializeField] private GameObject[] bodySkinnedMeshs;

		[Header("Strenght force")]
		[SerializeField] private float _explosionForce = 25f;
		[SerializeField] private float _explosionRadius = 25f;

		private AIController controller;
		private float _health;
		private float _timeDestroy;
		private float _damage;

		RaycastHit hitInfo;

		private void Start()
		{
			controller = GetComponentInParent<AIController>();
			if (bodyPart == EnumClass.BodyParts.Body) _health = controller.dataOrks.BodyHealth;
			else if(bodyPart == EnumClass.BodyParts.Head) _health = controller.dataOrks.HeadHealth;
			else _health = controller.dataOrks.BodyPartHealth;
			_timeDestroy = controller.dataOrks.TimeLifeAfterDie;
		}
		//Take damage from weapon method
		public void TakeDamageBodyPart(float damage,RaycastHit hitInfo,EnumClass.WeaponClass weaponClass)
		{
			this.hitInfo = hitInfo;
			_damage = damage;

			switch (bodyPart)
			{
				case EnumClass.BodyParts.Body:
					controller.TakeDamage(_damage);
					if (!controller.IsAlive)
						controller.SetRagdoll(false);
					break;

				case EnumClass.BodyParts.Head:
					HeadPart(weaponClass);
					break;

				case EnumClass.BodyParts.Hand:
					HandLegPart(false);
					break;

				case EnumClass.BodyParts.Leg:
						
					HandLegPart(true);
					break;
			}
			
		
		}
		public void TakeDamageExplosion(Vector3 position)
		{

		}
		private void HeadPart(EnumClass.WeaponClass weapon)
		{
			
			switch(weapon)
			{
				case EnumClass.WeaponClass.Rifle:

					controller.TakeDamage(_damage * 2f);
					if(!controller.IsAlive)
					{
						ExplosionBodyPart(true);
					}
				break;

				case EnumClass.WeaponClass.Sniper:
						ExplosionBodyPart(true);
					break;
			}
		}

		private void HandLegPart(bool isCritical)
		{
			if(EnumClass.BodyParts.Leg == bodyPart)
			controller.IsCriticalDamage = isCritical;

			if (bodySkinnedMesh)
			{
				if (bodySkinnedMesh.TryGetComponent<SkinnedMeshRenderer>(out SkinnedMeshRenderer meshRenderer))
					meshRenderer.enabled = false;
				else
					bodySkinnedMesh.GetComponent<MeshRenderer>().enabled = false;
			}

			if (bodyPartMesh)
			{
				bodyPartMesh.transform.position = hitInfo.point - bodyPartMesh.GetComponent<MeshFilter>().mesh.bounds.center;
				bodyPartMesh.transform.parent = null;
				bodyPartMesh.GetComponent<BodyPart>().Active(hitInfo.point);
			}
		}
		private void ExplosionBodyPart(bool kinematic)
		{
			if(bodySkinnedMesh)
			{
				if(bodySkinnedMesh.TryGetComponent<SkinnedMeshRenderer>(out SkinnedMeshRenderer meshRenderer))
					meshRenderer.enabled = false;
				else
					bodySkinnedMesh.GetComponent<MeshRenderer>().enabled = false;
			}

			if(bodyPartMesh)
			{
				bodyPartMesh.transform.position = hitInfo.point - bodyPartMesh.GetComponent<MeshFilter>().mesh.bounds.center;
				bodyPartMesh.transform.parent = null;
				bodyPartMesh.GetComponent<BodyPart>().Active(hitInfo.point);
			}


			//AIHitBox[] hitBox = gameObject.GetComponentsInChildren<AIHitBox>();
			//foreach (AIHitBox box in hitBox)
			//{
			//	if (box != this)
			//	{
			//		box.bodySkinnedMesh.SetActive(false);
			//		if(bodyPartMesh)
			//		box.bodyPartMesh.GetComponent<BodyPart>().Active(hitInfo.point, !kinematic);
			//	}
			//}



			foreach (var mesh in bodySkinnedMeshs)
			{
				mesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
			}

			foreach (var part in bodyPartMeshes)
			{
				BodyPart _part = part.GetComponent<BodyPart>();
				_part.Active(hitInfo.point);
				//_part.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, hitInfo.point, _explosionRadius);
			}
			controller.SetRagdoll(false);
			gameObject.SetActive(false);
		}
	}
}
