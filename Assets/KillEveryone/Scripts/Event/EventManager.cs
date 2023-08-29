using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public  class EventManager : MonoBehaviour
	{
		public static Action<bool> Aim {  get; set; }
	}
}
