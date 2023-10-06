using KillEveryone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HeadPart : MonoBehaviour
{
	private GameObject decal;

	[SerializeField] private DataZombie dataZombie;
	private void Start()
	{
		decal = Instantiate(dataZombie.prefabDecal);
		Material material = dataZombie.bloodDecals[Random.Range(0, dataZombie.bloodDecals.Length - 1)];
		decal.GetComponent<BloodDecal>().Initialize(material, new Vector3(2f,0.001f,2f));
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.layer == 0 )
		{
			
		}
		if(collision.gameObject.layer == 11 ) {

			decal.GetComponent<BloodDecal>().Active(collision.contacts[0].point);
			decal.transform.rotation = Quaternion.Euler(-90, collision.contacts[0].point.x, collision.contacts[0].point.z);
		}
	}
}
