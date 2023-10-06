using UnityEngine;

[CreateAssetMenu(fileName = "Zombie",menuName = "KillEveryone/DataZombie")]
public class DataZombie : ScriptableObject
{
    public float BodyHealth;
    public float HeadHealth;
    public float BodyPartHealth;
    public float TimeLifeAfterDie;
    public float BodyLifeAfterDie;
    public GameObject prefabDecal;
    public Material[] bloodDecals;
}
