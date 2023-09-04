using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
	private Animator animator;
	private Rigidbody[] ragdoll;

	public float _currentHealth;
	public float _maxHealth;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		ragdoll = GetComponentsInChildren<Rigidbody>();
		SetRagdoll(true);
	}
	private void Update()
	{
		agent.destination = target.position;
		animator.SetFloat("Speed", agent.velocity.magnitude);	
	}
	public void SetRagdoll(bool state)
	{
		foreach (var rag in ragdoll)
		{
			rag.isKinematic = state;
		}
		animator.enabled = state;
	}
	
}
