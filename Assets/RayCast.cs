using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RayCast : MonoBehaviour
{
	private void Update()
	{
		Debug.DrawRay(transform.position, transform.forward,Color.red,999f);
	}
}
