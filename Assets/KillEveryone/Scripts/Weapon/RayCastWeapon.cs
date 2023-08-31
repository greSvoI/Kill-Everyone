using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class RayCastWeapon : MonoBehaviour
	{
		public bool _isFiring = false;

		public void StartFire()
		{
			_isFiring = true;
		}
		public void StopFire()
		{
			_isFiring = false;
		}
	}
}
