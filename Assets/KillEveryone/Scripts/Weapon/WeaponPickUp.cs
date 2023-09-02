using KillEveryone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public Weapon weaponPrefab;
	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent<WeaponController>(out WeaponController controller))
		{
			Weapon weapon = Instantiate(weaponPrefab);
			controller.Equip(weapon);
		}
	}
}
