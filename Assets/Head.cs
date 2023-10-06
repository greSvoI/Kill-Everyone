using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
	public GameObject[] meshHead; 
    public Rigidbody[] rb;
    public MeshRenderer []mr;
	public float explosionForce;
	private void Start()
	{
		rb = GetComponentsInChildren<Rigidbody>();
		mr = GetComponentsInChildren<MeshRenderer>();
	}
	[ContextMenu("Start")]
	public void RB()
	{
		foreach(var mr in meshHead)
		{
			mr.SetActive(false);
		}

		foreach (MeshRenderer mr in mr)
		{
			mr.enabled = true;
		}
		foreach (Rigidbody _rb in rb)
		{
			_rb.isKinematic = false;
			_rb.AddExplosionForce(explosionForce,_rb.gameObject.transform.position, 20f,0.5f);
		}
	}
}
