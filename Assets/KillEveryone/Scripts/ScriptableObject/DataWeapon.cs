using KillEveryone;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon",menuName = "KillEveryone/Weapon/DataWeapon")]
public class DataWeapon : ScriptableObject
{
	public EnumClass.WeaponClass WeaponClass;
	public EnumClass.WeaponHolder WeaponHolder;

	public TrailRenderer tracerShootEffect;
	public ParticleSystem tracerRocketEffect;
	public ParticleSystem explosionEffect;

	public AudioClip Fire;
	public AudioClip EmptyClip;
	public AudioClip Reload;
	public AudioClip Equip;

	public float FireRate;
	public float Damage;
	public int BulletMax;
	public int BulletClip;
	public int BulletCount;
	public int WeaponID;
	public int ReloadID;
	//public int ClassID { get => (int)enumClass.weapon;}
	//public int AnimID { get => (int)enumClass.holder;}
}
