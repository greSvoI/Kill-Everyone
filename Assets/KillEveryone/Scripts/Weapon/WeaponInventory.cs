using UnityEngine;

namespace KillEveryone
{
	public class WeaponInventory : MonoBehaviour
	{
		[SerializeField] private int _weaponID;
		[SerializeField] private GameObject render;
		[SerializeField] private DataWeapon weapon;
		[SerializeField] private EnumClass.WeaponHolderBody holderBody;
		public int WeaponID=>_weaponID;
		private void Start()
		{
			_weaponID = weapon.WeaponID;
		}
		public bool IsActive { set=>render.SetActive(value); }
	} 
}
