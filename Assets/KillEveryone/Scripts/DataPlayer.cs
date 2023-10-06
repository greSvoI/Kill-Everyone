using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "KillEveryone/DataPlayer")]
public class DataPlayer : ScriptableObject
{
	public float Health;
	public TrailRenderer TrailRenderer;
	public ParticleSystem StoneEffect;
	public ParticleSystem EnemyEffect;
	public ParticleSystem WoodEffect;
	public ParticleSystem MetallEffect;

}
