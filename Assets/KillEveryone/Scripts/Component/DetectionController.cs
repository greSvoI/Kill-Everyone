using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	[ExecuteInEditMode]
	public class DetectionController : MonoBehaviour
	{

		[SerializeField] private LayerMask groundLayer;

		[Header("Check grounded")]
		[SerializeField] private float _groundOffset = 0.2f;
		[SerializeField] private float _groundRadius = 0.2f;

		private RaycastHit groundHit;
		private Vector3 spherePosition;



		private void Update()
		{

		}
		public bool ground;
		public bool IsGrounded()
		{
			 spherePosition = new Vector3(transform.position.x, transform.position.y - _groundOffset,
				transform.position.z);

			if (Physics.CheckSphere(spherePosition, _groundRadius, groundLayer, QueryTriggerInteraction.Ignore))
			{
				ground = true;
				return true;
			}
			ground = false;
			return false;
		}

		private void OnDrawGizmos()
		{
			Color redColor = Color.red;
			Color greenColor = Color.green;
			Gizmos.color = greenColor;	

			if(IsGrounded())
			{
				Gizmos.DrawSphere(spherePosition, _groundRadius);
			}
			else
			{
				Gizmos.color = redColor;
				Gizmos.DrawSphere(spherePosition, _groundRadius);
			}
		}
	}
}
