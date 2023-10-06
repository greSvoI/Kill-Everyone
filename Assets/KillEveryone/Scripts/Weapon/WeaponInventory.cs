using UnityEngine;

namespace KillEveryone
{
	public class WeaponInventory : MonoBehaviour
	{
		
		public GameObject render;

		public int WeaponID;

		public bool IsActive { set=>render.SetActive(value); }
	} 
}
