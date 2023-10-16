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
		private AIController controller;
		private GameObject decal;

		private bool _isActive = false;

		private float _timeDestroy = 20f;
		public float TimeDestroy { set => _timeDestroy = value; }

		private void Start()
		{
			meshRenderer = GetComponent<MeshRenderer>();
			rigidBody = GetComponent<Rigidbody>();
			controller = GetComponentInParent<AIController>();
			meshRenderer.enabled = false;

			//decal = Instantiate(controller.dataOrks.PrefabDecal,transform);
			//Material material = controller.dataOrks.BloodDecals[Random.Range(0, controller.dataOrks.BloodDecals.Length - 1)];

			//float min = controller.dataOrks.bloodDecalBodyPartsMin;
			//float max = controller.dataOrks.bloodDecalBodyPartsMax;
			//decal.GetComponent<BloodDecal>().Initialize(material,new Vector3(Random.Range(min,max), 0.001f, Random.Range(min,max)));
	
		}
		
		[ContextMenu("Rename object to part body")]
		public void RenameObject()
		{
			char[]value=  new char[2] {' ','_' };
			string name = this.gameObject.name;
			name = name.Substring(0,name.IndexOfAny(value));
			this.gameObject.name = name + "_Part";
		}
		public void TakeDamage(float damage)
		{
			controller.TakeDamage(damage);
		}
		public void Active(Vector3 position)
		{
			transform.parent = null;
			transform.position = position;
			meshRenderer.enabled = true;
			rigidBody.isKinematic = false;
			//StartCoroutine(StartDestroy());
		}
		public void DeActivate()
		{

		}
		private IEnumerator StartDestroy()
		{
			yield return new WaitForSeconds(_timeDestroy);
			Destroy(gameObject,0.2f);
		}
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.layer == 11 && !_isActive)
			{
				Debug.Log(transform.position);
				_isActive = true;
				controller.DrawDecal(transform.position);

			}
		}
	}
}

