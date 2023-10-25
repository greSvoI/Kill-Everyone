using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Windows;

namespace KillEveryone
{
	public class UIController : MonoBehaviour
	{

		public GameObject PlayerRender;
		public GameObject UI_Inventory;
		public GameObject UI_Input;
		public GameObject UI_Equip;

		public GameObject ButtonFire;
		public GameObject RightStick;

		private OnScreenButton buttonFire;

		private void Start()
		{
			buttonFire = RightStick.GetComponentInChildren<OnScreenButton>();
			EventManager.Weapon += OnWeapon;
		}
		private void OnDisable()
		{
			EventManager.Weapon -= OnWeapon;
		}
		private void OnWeapon(int obj)
		{
			if(obj != 0)
			{
				buttonFire.enabled = true;
				UI_Equip.SetActive(true);
				ButtonFire.SetActive(true);
			}
			else
			{
				buttonFire.enabled = false;
				UI_Equip.SetActive(false);
				ButtonFire.SetActive(false);
			}
		}

		public void ShowInventory()
		{

			Time.timeScale = 0f;
			UI_Input.SetActive(false);
			PlayerRender.SetActive(true);
			UI_Inventory.SetActive(true);
		}

		public void HideInventory()
		{
			Time.timeScale = 1f;
			UI_Input?.SetActive(true);
			PlayerRender.SetActive(false);
			UI_Inventory.SetActive(false);
		}
		public void WeaponButton(int index)
		{

		}
	}
}
