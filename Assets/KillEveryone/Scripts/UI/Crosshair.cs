using Cinemachine;
using KillEveryone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KillEveryone
{
	public class Crosshair : MonoBehaviour
	{
		public int weaponID;
		public GameObject aim;
		public GameObject noAim;

		public RawImage left;
		public RawImage right;

		Vector3 leftDefaultPos;
		Vector3 rightDefaultPos;

		Vector3 leftExpandPos;
		Vector3 rightExpandPos;

		private CrosshairSwitcher switcher;
		public bool _isDynamic = false;
		public float _expandAmount = 20f;
		public float returnToCenterSpeed;

		private void Start()
		{
			switcher = GetComponentInParent<CrosshairSwitcher>();
			if(left && right)
			{
				leftDefaultPos = left.transform.position;
				rightDefaultPos = right.transform.position;

				leftExpandPos = new Vector3(left.transform.position.x - _expandAmount, left.transform.position.y, left.transform.position.z);
				rightExpandPos = new Vector3(right.transform.position.x + _expandAmount, right.transform.position.y, right.transform.position.z);
			}
		}
		public void Switch(bool state)
		{
			aim.SetActive(state);
			noAim.SetActive(!state);
		}
		void LateUpdate()
		{
			if (_isDynamic)
				ShrinkCrosshairToNormal(switcher.enemy);
		}

		private void ShrinkCrosshairToNormal(bool state) //every frame do the opposide of Expand to return the crosshair back to the middle
		{
			if(state)
			{
				left.transform.position = Vector3.Lerp(left.transform.position, Vector3.zero, returnToCenterSpeed);
				right.transform.position = Vector3.Lerp(right.transform.position,Vector3.zero, returnToCenterSpeed);
				left.color = Color.green;
				right.color = Color.green;
			}
			else
			{
				left.transform.position = Vector3.Lerp(left.transform.position,leftDefaultPos, returnToCenterSpeed);
				right.transform.position = Vector3.Lerp(right.transform.position, rightDefaultPos, returnToCenterSpeed);
				left.color = Color.red;
				right.color = Color.red;
			}
			
		}
		public void SetShrinkSpeed(float shrinkSpeed) //this allows the gun script to tell us how fast we want to return crosshair sides to center
		{
			returnToCenterSpeed = shrinkSpeed;
		}

	}
}
