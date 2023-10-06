using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CubeTemplate : MonoBehaviour
{
    public Vector3 memoryTransform;
	public Rigidbody body;
	public Template template;
	public Transform parent;
	public MeshRenderer meshRenderer;

	private void Start()
	{
		body = GetComponent<Rigidbody>();
		template = GetComponentInParent<Template>();
		meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.enabled = false;
		parent = template.transform;
		memoryTransform = transform.localPosition;
	}
	public void ActiveRigidBody(Transform explosion)
	{
		transform.parent = null;
		body.isKinematic = false;
		meshRenderer.enabled = true;
		body.AddExplosionForce(template.explosionForce, explosion.position,template.explosionRadius);
	}
	public void GetStart()
    {
		transform.parent = parent;
        body.isKinematic = true;
		transform.rotation = Quaternion.identity;
    }
}
