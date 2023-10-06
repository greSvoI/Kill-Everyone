using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform RightHand;
    [SerializeField] private float weightHand;

    private Animator animator;
	void Update()
    {
        animator = GetComponent<Animator>();
    }
	private void OnAnimatorIK(int layerIndex)
	{
		animator.SetIKPosition(AvatarIKGoal.RightHand,RightHand.position);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weightHand);

        animator.SetIKRotation(AvatarIKGoal.RightHand, RightHand.rotation);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weightHand);

		animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
		animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weightHand);

		animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
		animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weightHand);

	}
}
