using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace KillEveryone
{
	public class PlayerInput : MonoBehaviour
	{
		private PlayerInputSystem input;
		[SerializeField] private Vector2 _move;
		[SerializeField] private Vector2 _look;
		[SerializeField] private float _magnituda;
		[SerializeField] private bool _sprint;
		[SerializeField] private bool _rool;
		[SerializeField] private bool _crouch;
		[SerializeField] private int _currentWeapon;
		[SerializeField] private bool _equip = false;

		[SerializeField] private bool _aim = false;
		[SerializeField] private bool _fire = false;
		public Vector2 Move => _move;
		public Vector2 Look => _look;
		public bool Sprint => _sprint;
		public bool Roll => _rool;
		public bool Crouch => _crouch;
		public bool Aim { get=>_aim; set { 
				_aim = value;
				EventManager.Aim?.Invoke(_aim);
			} }
		public bool Fire => _fire;
		public float Magnituda => _magnituda;
		public float CurrentWeapon => _currentWeapon;

		private void Awake()
		{
			if (input == null)
			input = new PlayerInputSystem();

			input.Player.Move.performed += i => _move = i.ReadValue<Vector2>();
			input.Player.Move.canceled += i => _move = i.ReadValue<Vector2>();

			input.Player.Sprint.performed += i => _sprint = i.ReadValueAsButton();

			input.Player.Look.performed += i => _look = i.ReadValue<Vector2>();
			input.Player.Look.canceled += i => _look = i.ReadValue<Vector2>();

			//input.Player.Roll.performed += i => _rool = i.ReadValueAsButton();
			//input.Player.Roll.canceled += i => _rool = i.ReadValueAsButton();

			input.Player.Roll.canceled += i => { SceneManager.LoadScene(0); };

			input.Player.Crouch.performed += i => _crouch = i.ReadValueAsButton();
			input.Player.Crouch.canceled += i => _crouch = i.ReadValueAsButton();

			//input.Player.Aim.performed += i => { 
			//	_aim = !_aim; EventManager.Aim?.Invoke(_aim);
			//};
			input.Player.Aim.performed += OnAim;

			input.Player.Fire.performed += i => { 
				_fire = i.ReadValueAsButton(); 
				EventManager.Fire?.Invoke(_fire);
			};
			input.Player.Fire.canceled += i => { _fire = i.ReadValueAsButton(); EventManager.Fire?.Invoke(_fire); };
			input.Player.Weapon.performed += OnWeapon;
			input.Player.Reload.performed += i => EventManager.Reload?.Invoke();
			EventManager.Equip += i => { _equip = i; };
		}

		private void OnAim(InputAction.CallbackContext context)
		{
			if(_equip)
			{
				Aim = !_aim;
				
			}
		}

		private void OnWeapon(InputAction.CallbackContext context)
		{
			if(!_aim)
			{
				int weapon_index = 0;
				int.TryParse(context.control.displayName, out weapon_index);

				if (CurrentWeapon == weapon_index) { _currentWeapon = 0; }
				else { _currentWeapon = weapon_index; }

				EventManager.Weapon?.Invoke(_currentWeapon);
			}
		}

		private void Update()
		{
			_magnituda = Mathf.Clamp01(Mathf.Abs(Move.x) + Mathf.Abs(Move.y));
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

