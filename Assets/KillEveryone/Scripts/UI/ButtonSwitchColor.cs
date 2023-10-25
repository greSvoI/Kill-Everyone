using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KillEveryone
{
	public class ButtonSwitchColor : MonoBehaviour
	{
		private Image _image;
		private bool _active = false;
		private void Start()
		{
			_image = GetComponent<Image>();
		}
		public void ActiveColor()
		{
			_active = !_active;

			if(_active)
			_image.color = Color.red;
			else
			_image.color = Color.white;
		}

	}
}
