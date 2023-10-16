using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class BloodDecal : MonoBehaviour
	{
		private MeshRenderer meshRenderer;
		private void Start()
		{
			meshRenderer = GetComponent<MeshRenderer>();	
		}
		public void Active(Vector3 position)
		{
			this.gameObject.transform.parent = null;
			meshRenderer.enabled = true;
			this.gameObject.transform.position = position;
		}
		public void Initialize(Material material, Vector3 scale)
		{
			meshRenderer  = this.gameObject.GetComponent<MeshRenderer>();
			this.gameObject.transform.localScale = scale;
			meshRenderer.enabled = false;
		}
	}
}
