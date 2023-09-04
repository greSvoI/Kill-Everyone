using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class Weapon : MonoBehaviour
	{
		[SerializeField] private ParticleSystem []fireEffect;
	    public Transform muzzle;
		public Transform leftHandIK;
		public Transform rightHandIK;

		public void Fire()
		{
			foreach (var p in fireEffect)
			{
				p.Emit(1);
			}
		}
		public void Reload()
		{
			
		}
	}
}
