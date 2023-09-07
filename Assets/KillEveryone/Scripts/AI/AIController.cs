using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

namespace KillEveryone
{
	public class AIController : MonoBehaviour
	{
		[SerializeField] private Transform target;
		[SerializeField] private Transform parentBloodDecals;
		[SerializeField] private LayerMask groundLayer;
		[SerializeField] public DataZombie dataZombie;

		[Header("Scale blood decal")]
		[Range(1f, 3f)]
		[SerializeField] private float _minScaleDecal;
		[Range(1f, 3f)]
		[SerializeField] private float _maxScaleDecal;

		[Space(2)]
		[Header("Random speed agent")]
		[Range(2f, 5f)]
		[SerializeField] private float _minSpeedAgent;
		[Range(2f, 5f)]
		[SerializeField] private float _maxSpeedAgent;


		private List<BloodDecal> bloodDecals = new List<BloodDecal>();

		private NavMeshAgent agent;
		private Animator animator;
		private Rigidbody[] ragdoll;

		public bool _isCritical = false;
		public float _healthBodyPart;
		public float _healthBody;
		public float _timeDestroy;
		private int _bloodDecalIndex = 0;
		public bool IsCriticalDamage { get => animator.GetBool("isCritical"); set => CriticalDamage(value); }
		private void CriticalDamage(bool value)
		{
			animator.SetBool("isCritical", value);
			agent.baseOffset = 0.0f;
			agent.speed = 1.0f;
		}

		private void Start()
		{
			if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;

			agent = GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();
			ragdoll = GetComponentsInChildren<Rigidbody>();

			SetRagdoll(true);

			agent.speed = Random.Range(_minSpeedAgent, _maxSpeedAgent);

			for (int i = 0; i < dataZombie.bloodDecals.Length; i++)
			{
				GameObject decal = Instantiate(dataZombie.prefabDecal);
				Vector3 scale = new Vector3(Random.Range(_minScaleDecal,_maxScaleDecal), 0.001f, Random.Range(_minScaleDecal,_maxScaleDecal));
				Material material = dataZombie.bloodDecals[Random.Range(0, dataZombie.bloodDecals.Length - 1)];
				decal.GetComponent<BloodDecal>().Initialize(material,scale);
				decal.transform.SetParent(parentBloodDecals);
				bloodDecals.Add(decal.GetComponent<BloodDecal>());
			}
		}
		private void Update()
		{
			if (!animator.enabled)
			{
				agent.isStopped = true;
			}
			agent.destination = target.position;
			animator.SetFloat("Speed", agent.velocity.magnitude);
		}
		public void TakeDamage()
		{
			if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 2f, groundLayer))
			{
				_bloodDecalIndex++;
				if (_bloodDecalIndex == dataZombie.bloodDecals.Length - 1) _bloodDecalIndex = 0;
				bloodDecals[_bloodDecalIndex].Active(hitInfo.point);
			}
		}
		public void SetRagdoll(bool state)
		{
			foreach (var rag in ragdoll)
			{
				if(rag!=null)
				rag.isKinematic = state;
			}
			animator.enabled = state;

			if (!state) StartCoroutine(Die(_timeDestroy));
		}

		private IEnumerator Die(object timeDestroy)
		{
			yield return new WaitForSeconds(_timeDestroy);
			foreach(BloodDecal decal in bloodDecals)
				Destroy(decal.gameObject,0.2f);
			Destroy(gameObject, 0.2f);
		}
	}
}
