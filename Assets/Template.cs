using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template : MonoBehaviour
{
	[SerializeField] private Rigidbody[] rigidbodies;
	[SerializeField] private GameObject[] objects;

	[SerializeField] private GameObject arm;


	
	public float explosionForce;
	public float explosionRadius;

	public bool backTransform = false;
	public float lerpTime = 2f;


	public Transform explosion;
	public float radius ;

	private void OnDrawGizmos()
	{
		
	}

	private void Start()
	{
		rigidbodies = GetComponentsInChildren<Rigidbody>();

		foreach (Rigidbody go in rigidbodies)
		{
			go.isKinematic = true;
			go.gameObject.AddComponent<CubeTemplate>();
		}
		
	}
	private void Update()
	{
        if (backTransform)
        {
            foreach(Rigidbody go in rigidbodies)
			{
				CubeTemplate cube = go.gameObject.GetComponent<CubeTemplate>();
				if (cube != null)
				{
					cube.transform.localPosition = Vector3.Lerp(cube.transform.localPosition, cube.memoryTransform, lerpTime);
					if (cube.transform.localPosition == cube.memoryTransform)
					{
						backTransform = false;
						cube.meshRenderer.enabled = false;
						arm.SetActive(true);
					}
				}
				
			}
        }
    }
	[ContextMenu("Active RigidBody")]
	public void ActiveRigidBody()
	{
		arm.SetActive(false);


		foreach (var rigidbody in rigidbodies)
		{
			rigidbody.gameObject.SetActive(true);
			rigidbody.GetComponent<CubeTemplate>().ActiveRigidBody(explosion);
		}
	}

	[ContextMenu("Get Transform")]
	public void GetTransform()
	{
		foreach (Rigidbody go in rigidbodies)
		{
			go.GetComponent<CubeTemplate>().GetStart();
		}
		backTransform = true;
	}
}
