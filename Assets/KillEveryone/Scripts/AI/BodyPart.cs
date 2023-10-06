using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent (typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshFilter))]
	public class BodyPart : MonoBehaviour
	{
		private MeshRenderer meshRenderer;
		private Rigidbody rigidBody;
		private GameObject decal;
		private AIController controller;

		[SerializeField] private DataZombie dataZombie;

		private float _timeDestroy = 20f;
		public float TimeDestroy { set => _timeDestroy = value; }

		private void Start()
		{
			meshRenderer = GetComponent<MeshRenderer>();
			rigidBody = GetComponent<Rigidbody>();
			controller = GetComponentInParent<AIController>();
			meshRenderer.enabled = false;

			decal = Instantiate(controller.dataZombie.prefabDecal);
			Material material = controller.dataZombie.bloodDecals[Random.Range(0,controller.dataZombie.bloodDecals.Length - 1)];
			decal.GetComponent<BloodDecal>().Initialize(material, new Vector3(2f, 0.001f, 2f));
		}
		
		[ContextMenu("Rename object to part body")]
		public void RenameObject()
		{
			char[]value=  new char[2] {' ','_' };
			string name = this.gameObject.name;
			name = name.Substring(0,name.IndexOfAny(value));
			this.gameObject.name = name + "_Part";
		}

		public void Active(Vector3 position,bool kinematic)
		{
			meshRenderer.enabled = true;
			rigidBody.isKinematic = kinematic;
			StartCoroutine(StartDestroy());
		}
		private IEnumerator StartDestroy()
		{
			yield return new WaitForSeconds(_timeDestroy);
			Destroy(gameObject,0.2f);
		}
		private void OnCollisionEnter(Collision collision)
		{
			if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Default"))
			{
				rigidBody.isKinematic = true;
			}
			if (collision.gameObject.layer == 11)
			{

				decal.GetComponent<BloodDecal>().Active(collision.contacts[0].point);
				decal.transform.rotation = Quaternion.Euler(-90, collision.contacts[0].point.x, collision.contacts[0].point.z);
			}
		}
	}
}

