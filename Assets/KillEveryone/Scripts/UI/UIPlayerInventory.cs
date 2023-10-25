using UnityEngine;

namespace KillEveryone
{
	public class UIPlayerInventory : MonoBehaviour
	{

		[Header("Animator ik params weapon")]
		[SerializeField] private Transform rightHand;
		[SerializeField] private Transform leftHand;

		[SerializeField] private Transform rightHint;
		[SerializeField] private Transform leftHint;

		[Header("Camera renderer rotate")]
		[SerializeField] private Transform cameraRoot;
		[SerializeField] private Transform followCamera;

		[SerializeField] private Vector3 _offsetFollowCamera;
		[SerializeField] private Vector2 _direction;
		[SerializeField] private float _rotationSpeed;


		[SerializeField] private float timeAnim = 2f;
		private float weight = 1f;

		private Animator animator;

		private void Start()
		{
			animator = GetComponent<Animator>();
			_direction = Vector2.zero;
		}

		private void OnTouchInput(Vector2 vector)
		{
			_direction = vector;
		}
		public float _touchInpuX;
		private void LateUpdate()
		{
			if(_direction != Vector2.zero)
			transform.rotation = transform.rotation * Quaternion.Euler(0f, -_direction.x * _rotationSpeed, 0f);
		}
		private void OnAnimatorIK(int layerIndex)
		{
			if (leftHand != null)
			{
				animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);

				animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
				animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);

				animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftHint.position);
				animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, weight);

				
			}

			//RightHand
			if (rightHand != null)
			{

				animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
				animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);

				animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
				animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);

				animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightHint.position);
				animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, weight);
			}
		}
		
		private void OnEnable()
		{
			EventManager.TouchInventory += OnTouchInput;
		}

		

		private void OnDisable()
		{
			EventManager.TouchInventory -= OnTouchInput;
		}
	}
}
