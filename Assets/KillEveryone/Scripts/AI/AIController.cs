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
		

		[Space(2)]
		[Header("Random speed agent")]
		[Range(0f, 5f)]
		[SerializeField] private float _minSpeedAgent;
		[Range(0f, 5f)]
		[SerializeField] private float _maxSpeedAgent;
		[Space(1)]
		[Header("Basic materials add blood(Noise shader graph)")]
		[SerializeField] private float _noise = 1000f;
		[SerializeField] private SkinnedMeshRenderer [] materials;

		[Space(1f)]
		[Header("Blood decals in floor")]
		private List<BloodDecal> bloodDecals = new List<BloodDecal>(); [Header("Scale blood decal")]
		[Range(1f, 3f)]
		[SerializeField] private float _minScaleDecal;
		[Range(1f, 3f)]
		[SerializeField] private float _maxScaleDecal;
		[Space(1f)]

		private NavMeshAgent agent;
		private Animator animator;
		private Rigidbody[] ragdoll;


		//Save decals in root
		private Transform parentBloodDecals;

		//Animator override layer
		private int layerOverride;
		private float layerWeight = 0f;


		public bool _isCritical = false;
		public float _timeDestroy;

		private int _bloodDecalIndex = 0;
		private bool _decalIsDaraw = false;


		private float _healthBody;
		private bool _isAlive = true;

		public DataOrks dataOrks;
		//Layer override in injured
		public bool IsCriticalDamage { get => animator.GetBool("isCritical"); set => CriticalDamage(value); }
		public float Health { get => _healthBody; }
		public bool IsAlive { get => _isAlive; }
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
			SetRagdoll(true);

			agent.speed = Random.Range(_minSpeedAgent, _maxSpeedAgent);

			for (int i = 0; i < dataOrks.BloodDecals.Length; i++)
			{
				GameObject decal = Instantiate(dataOrks.PrefabDecal);
				Vector3 scale = new Vector3(Random.Range(_minScaleDecal,_maxScaleDecal), 0.001f, Random.Range(_minScaleDecal,_maxScaleDecal));
				Material material = dataOrks.BloodDecals[Random.Range(0, dataOrks.BloodDecals.Length - 1)];
				decal.GetComponent<BloodDecal>().Initialize(material,scale);
				decal.transform.SetParent(parentBloodDecals);
				bloodDecals.Add(decal.GetComponent<BloodDecal>());
			}

			_healthBody = dataOrks.BodyHealth;
			_timeDestroy = dataOrks.TimeLifeAfterDie;
		}
		private void Update()
		{
			if(!detectionController.IsGrounded())
			{
				SetRagdoll(false);
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
			_healthBody -= damage;
			if (_healthBody <= 0)
			{
				_isAlive = false;
			}

			//layerWeight += 0.2f;
			//agent.speed = agent.speed - layerWeight * 2;
			//if(layerWeight > 0.5f) IsCriticalDamage = true;
			animator.SetLayerWeight(layerOverride, layerWeight);

			//if(materials !=  null)
			//{
			//	foreach(var mesh in materials)
			//	{
			//		var m = mesh.material;
			//		m.SetFloat("_Noise", _noise);
			//		mesh.material = m;
			//	}
			//}

			//Decals in floor
			//DrawDecal(transform.position);

		}
		public void DrawDecal(Vector3 position)
		{
			if (Physics.Raycast(position + Vector3.up, Vector3.down, out RaycastHit hitInfo, 2f, groundLayer))
			{
				_bloodDecalIndex++;
				
				if (_bloodDecalIndex == dataOrks.BloodDecals.Length - 1) 
					_bloodDecalIndex = 0;

				bloodDecals[_bloodDecalIndex].Active(hitInfo.point);
			}
		}
		public void SetRagdoll(bool state)
		{
			if(!state) _isAlive = false;

			foreach (var rag in ragdoll)
			{
				if(rag!=null)
				{
					rag.isKinematic = state;
					if(!state)
					{
						//if(damage>20)
						//	rag.AddForce(Camera.main.transform.forward * 20f,ForceMode.Impulse);
						//else if(damage >=100)
						//	rag.AddForce(Camera.main.transform.forward * 200f, ForceMode.Impulse);
						
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
