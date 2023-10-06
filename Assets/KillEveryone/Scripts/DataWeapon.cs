using KillEveryone;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "Weapon", menuName = "KillEveryone/DataWeapon")]
public class DataWeapon : ScriptableObject
{
    public Vector3 RightHandPosition;
    public Vector3 LeftHandPosition;

    public Quaternion RightHandRotation;
    public Quaternion LeftHandRotation;

    public DrawWeaponID DrawWeaponID;

    public float FireRate;
    public float Damage;
    public int BulletMax;
    public int BulletClip;
    public int BulletCount;
    public int WeaponID;

}
