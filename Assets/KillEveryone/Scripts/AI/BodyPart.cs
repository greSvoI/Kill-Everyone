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
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField] private Rigidbody rigidBody;

		private float _timeDestroy = 5f;
		public float TimeDestroy { set => _timeDestroy = value; }

		private void Start()
		{
			meshRenderer = GetComponent<MeshRenderer>();
			rigidBody = GetComponent<Rigidbody>();
			meshRenderer.enabled = false;
		}

		public void Active()
		{
			meshRenderer.enabled = true;
			rigidBody.isKinematic = false;
			StartCoroutine(StartDestroy());
		}
		private IEnumerator StartDestroy()
		{
			yield return new WaitForSeconds(_timeDestroy);
			Destroy(gameObject,0.2f);
		}
	}
}

