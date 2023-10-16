using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "KillEveryone/Weapon/DataHitEffect")]

public class DataHitEffect : ScriptableObject
{
    public ParticleSystem WoodEffect;
    public ParticleSystem StoneEffect;
    public ParticleSystem MetallEffect;
    public ParticleSystem SandEffect;
    public ParticleSystem EnemyEffect;

    public ParticleSystem BigExplosion;
}
