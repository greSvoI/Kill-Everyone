using UnityEngine;

namespace KillEveryone
{
	[CreateAssetMenu(fileName = "Orks", menuName = "KillEveryone/DataOrks")]
	public class DataOrks : ScriptableObject
	{
		public float BodyHealth;
		public float HeadHealth;
		public float BodyPartHealth;
		public float TimeLifeAfterDie;
		public float BodyLifeAfterDie;
		public GameObject PrefabDecal;
		public Material[] BloodDecals;

		[Range(0f,5f)]
		public float bloodDecalBodyPartsMin;
		[Range(0f, 5f)]
		public float bloodDecalBodyPartsMax;


		public float FireRate;
	}
}
