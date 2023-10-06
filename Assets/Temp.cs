using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    Rigidbody[] rigidbodies;
	public float explosionForce;
	public float explosionRadius;
	public GameObject gameObject;
	Animator animator;
	private void Start()
	{
		animator = GetComponent<Animator>();
		//rigidbodies = GetComponentsInChildren<Rigidbody>();
		//foreach(var rigidbody in rigidbodies)
		//	rigidbody.isKinematic = true;
	}
	public Transform IK;
	public float IKWeight;
	private void OnAnimatorIK(int layerIndex)
	{
		animator.SetIKPosition(AvatarIKGoal.RightFoot,IK.transform.position);
		animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, IKWeight);
	}
	public void TakeDamage(Vector3 center)
	{
		gameObject.SetActive(false);
		foreach (var rigidbody in rigidbodies)
		{
			rigidbody.isKinematic = false;
			rigidbody.AddExplosionForce(explosionForce, center, explosionRadius);
		}
	}
}
