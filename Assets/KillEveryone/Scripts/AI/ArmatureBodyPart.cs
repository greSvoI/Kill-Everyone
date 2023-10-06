using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmatureBodyPart : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rigidbodies;
	[SerializeField] private bool _isReturn = false;

	[SerializeField] private SkinnedMeshRenderer BodyPart;

	[SerializeField] public Transform ExplosionCenter;
	[SerializeField] public float ExplosionForce;
	[SerializeField] public float ExplosionRadius;
	[SerializeField] public float ReturnTime;

	private void Start()
	{
		ExplosionCenter = transform;
		rigidbodies = GetComponentsInChildren<Rigidbody>();
		foreach (var part in rigidbodies)
		{
			
			part.GetComponent<ArmaturePart>().Deactivate();
		}
	}
	private void Update()
	{
		if(_isReturn)
		{
			bool flag = false;
			foreach(var part in rigidbodies)
			{
				ArmaturePart pp = part.GetComponent<ArmaturePart>();
				pp.GetComponent<Rigidbody>().isKinematic = true;
				pp.transform.parent = transform;
				//pp.transform.localPosition =  pp.transform.localPosition - pp.mr.bounds.center;
				pp.transform.localPosition = Vector3.Lerp(pp.transform.localPosition,Vector3.zero, ReturnTime);

				if (!pp.IsReturn)
				{
					pp.ReturnPosition();
					flag = false;
				}
			}
			if (flag)
			{
				BodyPart.enabled = true;
				_isReturn = false;
			}
		}
	}
	[ContextMenu("Activate")]
	public void Activate()
	{
		BodyPart.enabled = false;
		foreach (var part in rigidbodies)
		{
			GameObject temp = part.gameObject;
			temp.transform.parent = null;
			temp.GetComponent<Rigidbody>().isKinematic = false;
			temp.GetComponent<Rigidbody>().AddExplosionForce(25f, transform.position, 25f);
			temp.GetComponent<MeshRenderer>().enabled = true;
			//part.GetComponent<ArmaturePart>().Activate();
		}
	}
	[ContextMenu("Return")]
	public void Return()
	{
		_isReturn = true;
	}
}
