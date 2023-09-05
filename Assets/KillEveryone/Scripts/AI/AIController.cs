using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private Transform target;
	[SerializeField] private ParticleSystem damageEffect;


	[SerializeField] private LayerMask groundLayer;

    private NavMeshAgent agent;
	private Animator animator;
	private Rigidbody[] ragdoll;


	public bool _isCritical = false;
	public float _healthBodyPart;
	public float _healthBody;
	public float _timeDestroy;
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
		agent.speed = Random.Range(2.5f, 4.0f);
		animator = GetComponent<Animator>();
		ragdoll = GetComponentsInChildren<Rigidbody>();
		SetRagdoll(true);
	}
	private void Update()
	{
		if(!animator.enabled)
		{
			agent.isStopped = true;
		}
			agent.destination = target.position;
			animator.SetFloat("Speed", agent.velocity.magnitude);
	}
	public void TakeDamage()
	{
		if(Physics.Raycast(transform.position + Vector3.up,Vector3.down, out RaycastHit hitInfo,2f,groundLayer)) {

			damageEffect.transform.position = hitInfo.point;
			damageEffect.transform.forward = hitInfo.normal;
			//damageEffect.transform.rotation = Quaternion.identity;
			damageEffect.Emit(1);
		}
	}
	public bool _state = false;
	[ContextMenu("Ragdoll")]
	public void TempRagdoll()
	{
		foreach (var rag in ragdoll)
		{
			rag.isKinematic = _state;
		}
		animator.enabled = _state;

		if(_state)
		{
			animator.enabled = true;
			animator.SetBool("isCritical", true);
		}
	}
	public void SetRagdoll(bool state)
	{
		foreach (var rag in ragdoll)
		{
			rag.isKinematic = state;
		}
		animator.enabled = state;
		
		if(!state) StartCoroutine(Die(_timeDestroy));
	}

	private IEnumerator Die(object timeDestroy)
	{
		yield return new WaitForSeconds(_timeDestroy);
		Destroy(gameObject,0.2f);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(transform.position + Vector3.up, Vector3.down);
	}


}
