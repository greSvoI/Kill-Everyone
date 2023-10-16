using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArmaturePart : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] public MeshRenderer mr;

	[SerializeField] private Transform memoryTransform;
	[SerializeField] private ArmatureBodyPart bodyPart;

	public bool IsReturn = false;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		mr = GetComponent<MeshRenderer>();
		bodyPart = GetComponentInParent<ArmatureBodyPart>();
		memoryTransform = transform;
	}
	public void Deactivate()
    {
		rb.isKinematic = true;
		mr.enabled = false;
    }
	public void Activate()
	{
		transform.parent = null;
		IsReturn = false;
		rb.isKinematic = false;
		mr.enabled = true;
		rb.AddExplosionForce(bodyPart.ExplosionForce, bodyPart.ExplosionCenter.position, bodyPart.ExplosionRadius);
	}
	public void ReturnPosition()
	{
		rb.isKinematic = true;
		transform.parent = bodyPart.transform;
		//transform.rotation = Quaternion.FromToRotation(,new Vector3(memoryTransform.rotation.x, memoryTransform.rotation.y, memoryTransform.rotation.z));
		transform.position = Vector3.Lerp(transform.position,Vector3.zero, bodyPart.ReturnTime);
		if(transform.position == Vector3.zero)
			IsReturn = true;
	}
	private void OnCollisionEnter(Collision collision)
	{
		
	}
}
