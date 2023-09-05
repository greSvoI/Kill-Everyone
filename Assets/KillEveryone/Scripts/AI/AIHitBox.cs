using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class AIHitBox : MonoBehaviour
{
    public enum BodyParts { Head,Body,BodyHandPart,BodyLegPart}

    [SerializeField] private BodyParts bodyPart;
    [SerializeField] private GameObject rigidMesh;
    [SerializeField] private GameObject bodyMesh;
    [SerializeField] private float _health;
    [SerializeField] private float _timeDestroy;
	[SerializeField] private AIController controller;

	private void Start()
	{
		controller = GetComponentInParent<AIController>();
		if (bodyPart == BodyParts.Body) _health = controller._healthBody;
		else _health = controller._healthBodyPart;
		
		_timeDestroy = controller._timeDestroy;
	}
	public void TakeDamageBodyPart(float damage,Vector3 direction)
    {
		controller.TakeDamage();
        _health -= damage;
        if (_health <= 0)
        {
			switch (bodyPart)
			{
				case BodyParts.Head:
					BodyPart(direction);
                    controller.SetRagdoll(false);
					gameObject.SetActive(false);
					break;
                case BodyParts.Body:
					controller.SetRagdoll(false);
                    break;
                case BodyParts.BodyHandPart:
					BodyPart(direction);
					BodyPartChild(direction);
					break;
				case BodyParts.BodyLegPart:
					controller.IsCriticalDamage = true;
					BodyPart(direction);
					BodyPartChild(direction);
					break;
			}
            
        }
    }
	private void BodyPartChild(Vector3 direction)
	{
		AIHitBox[] aIHit = gameObject.GetComponentsInChildren<AIHitBox>();
		foreach (AIHitBox hitBox in aIHit)
		{
			if (hitBox != this)
			//hitBox.ChildBodyPart(rigidMesh.transform);
			{
				hitBox.bodyMesh.SetActive(false);
				hitBox.rigidMesh.GetComponent<MeshRenderer>().enabled = true;
				//StartCoroutine(DestroyBodyPart(rigidMesh));
			}
		}
		//StartCoroutine(DestroyBodyPart(rigidMesh));
		gameObject.SetActive(false);
	}
	private void BodyPart(Vector3 direction)
	{
		bodyMesh.SetActive(false);
		rigidMesh.GetComponent<MeshRenderer>().enabled = true;
		rigidMesh.GetComponent<Rigidbody>().isKinematic = false;
		//rigidMesh.GetComponent<Rigidbody>().AddForce(direction);
	}

	//public void ChildBodyPart(Transform parent)
 //   {
 //       bodyMesh.SetActive(false);
	//	rigidMesh.GetComponent<MeshRenderer>().enabled = true;
	//	StartCoroutine(DestroyBodyPart(rigidMesh));
	//}
	private IEnumerator DestroyBodyPart(GameObject rigidMesh)
	{
		yield return new WaitForSeconds(_timeDestroy);
        Destroy(rigidMesh, 0.2f);
	}
}
