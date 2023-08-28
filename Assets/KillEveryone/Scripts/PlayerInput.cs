using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public class PlayerInput : MonoBehaviour
	{
		private PlayerInputSystem input;
		[SerializeField] private Vector2 _move;
		[SerializeField] private Vector2 _look;
		[SerializeField] private bool _sprint;
		[SerializeField] private bool _rool;
		[SerializeField] private bool _crouch;
		public Vector2 Move => _move;
		public Vector2 Look => _look;
		public bool Sprint => _sprint;
		public bool Roll => _rool;
		public bool Crouch => _crouch;

		private void Awake()
		{
			if (input == null)
			input = new PlayerInputSystem();

			input.Player.Move.performed += i => _move = i.ReadValue<Vector2>();
			input.Player.Move.canceled += i => _move = i.ReadValue<Vector2>();

			input.Player.Sprint.performed += i => _sprint = i.ReadValueAsButton();

			input.Player.Look.performed += i => _look = i.ReadValue<Vector2>();
			input.Player.Look.canceled += i => _look = i.ReadValue<Vector2>();

			input.Player.Roll.performed += i => _rool = i.ReadValueAsButton();
			input.Player.Roll.canceled += i => _rool = i.ReadValueAsButton();

			input.Player.Crouch.performed += i => _crouch = i.ReadValueAsButton();
			input.Player.Crouch.canceled += i => _crouch = i.ReadValueAsButton();
		}

		private void OnEnable()
		{
			input.Enable();
		}
		private void OnDisable()
		{
			input.Disable();
		}
	}
}

