using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KillEveryone
{
	public  class EventManager : MonoBehaviour
	{
		public static Action<Vector2> TouchInventory {  get;set; }
		public static Action<Vector2> TouchStickLook {  get;set; }
		public static Action<bool> Aim {  get; set; }
		public static Action<bool> Fire {  get; set; }
		public static Action<int> Weapon { get; set; }
		public static Action<bool> Equip { get; set; }
		public static Action Reload { get; set; }
	}
}
