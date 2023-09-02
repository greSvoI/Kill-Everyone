using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class Weapon : MonoBehaviour
	{
		[SerializeField] private ParticleSystem []fireEffect;
		[SerializeField] public Transform muzzle;

		public void Fire()
		{
			foreach (var p in fireEffect)
			{
				p.Emit(1);
			}
		}
	}
}
