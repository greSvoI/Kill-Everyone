using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

namespace KillEveryone
{
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(DetectionController))]
	public class AIController : MonoBehaviour
	{
		DetectionController detectionController;
		[SerializeField] private Transform target;
		
		[SerializeField] private LayerMask groundLayer;
		[SerializeField] public DataZombie dataZombie;

		[Header("Scale blood decal")]
		[Range(1f, 3f)]
		[SerializeField] private float _minScaleDecal;
		[Range(1f, 3f)]
		[SerializeField] private float _maxScaleDecal;

		[Space(2)]
		[Header("Random speed agent")]
		[Range(0f, 5f)]
		[SerializeField] private float _minSpeedAgent;
		[Range(0f, 5f)]
		[SerializeField] private float _maxSpeedAgent;

		[SerializeField] private SkinnedMeshRenderer [] materials; 

		private List<BloodDecal> bloodDecals = new List<BloodDecal>();

		private NavMeshAgent agent;
		private Animator animator;
		private Rigidbody[] ragdoll;

		private Transform parentBloodDecals;

		//Animator override layer
		private int layerOverride;
		private float layerWeight = 0f;

		public bool _isCritical = false;
		public float _healthBody;
		public float _timeDestroy;
		private int _bloodDecalIndex = 0;
		private bool _decalIsDaraw = false;

		public bool IsCriticalDamage { get => animator.GetBool("isCritical"); set => CriticalDamage(value); }
		private void CriticalDamage(bool value)
		{
			animator.SetBool("isCritical", value);
			agent.baseOffset = 0.0f;
			agent.speed = 1.0f;
		}


		private void Start()
		{
			detectionController = GetComponent<DetectionController>();

			if (target == null) 
				target = GameObject.FindGameObjectWithTag("Player").transform;

			if(parentBloodDecals == null)
			{
				GameObject decals = new GameObject("BloodDecals");
				decals.transform.SetParent(transform);
				parentBloodDecals = decals.transform;
			}
		
			

			agent = GetComponent<NavMeshAgent>();
			animator = GetComponent<Animator>();
			ragdoll = GetComponentsInChildren<Rigidbody>();

			layerOverride = animator.GetLayerIndex("Override");
			SetRagdoll(true,0f);

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

			_healthBody = dataZombie.BodyHealth;
			_timeDestroy = dataZombie.TimeLifeAfterDie;
		}
		private void Update()
		{
			if(!detectionController.IsGrounded())
			{
				SetRagdoll(false,0f);
			}
			if (!animator.enabled)
			{
				agent.isStopped = true;
			}
			agent.destination = target.position;
			animator.SetFloat("Speed", agent.velocity.magnitude);
		}
		public void TakeDamage(float damage)
		{
			layerWeight += 0.2f;
			agent.speed = agent.speed - layerWeight * 2;
			if(layerWeight > 0.5f) IsCriticalDamage = true;
			animator.SetLayerWeight(layerOverride, layerWeight);

			if(materials !=  null)
			{
				//float noise = (damage * 4) * 10;
				foreach(var mesh in materials)
				{
					var m = mesh.material;
					m.SetFloat("_Noise", Random.Range(500,2000));
					mesh.material = m;
				}
			}

			if(!_decalIsDaraw)
			if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 2f, groundLayer))
			{
				_bloodDecalIndex++;
				_decalIsDaraw = true;
				if (_bloodDecalIndex == dataZombie.bloodDecals.Length - 1) _bloodDecalIndex = 0;
				bloodDecals[_bloodDecalIndex].Active(hitInfo.point);
			}

			_healthBody -= damage;
			if (_healthBody <= 0) SetRagdoll(false,damage);
		}
		public void SetRagdoll(bool state,float damage)
		{
			foreach (var rag in ragdoll)
			{
				if(rag!=null)
				{
					rag.isKinematic = state;
					if(!state)
					{
						if(damage>20)
							rag.AddForce(Camera.main.transform.forward * 20f,ForceMode.Impulse);
						else if(damage >=100)
							rag.AddForce(Camera.main.transform.forward * 200f, ForceMode.Impulse);
						
					}
				}
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
