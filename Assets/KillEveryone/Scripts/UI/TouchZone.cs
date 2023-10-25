using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace KillEveryone
{
	public class TouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		public RectTransform TouchLook;
		public RectTransform StickLook;

		public bool invertXOutputValue;
		public bool invertYOutputValue;

		public Vector2 stickDirection;
		public void OnDrag(PointerEventData eventData)
		{
			StickLook.position = eventData.position;
			Vector2 clamped = ClampValuesToMagnitude(eventData.delta);
			stickDirection =  ApplyInversionFilter(clamped);

			EventManager.TouchStickLook?.Invoke(stickDirection);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			StickLook.gameObject.SetActive(true);
			StickLook.position = eventData.position;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			EventManager.TouchStickLook?.Invoke(Vector2.zero);
			StickLook.gameObject.SetActive(false);
		}
		private Vector2 ClampValuesToMagnitude(Vector2 position)
		{
			return Vector2.ClampMagnitude(position, 1);
		}
		private Vector2 ApplyInversionFilter(Vector2 position)
		{
			if (invertXOutputValue)
			{
				position.x = InvertValue(position.x);
			}

			if (invertYOutputValue)
			{
				position.y = InvertValue(position.y);
			}

			return position;
		}
		private float InvertValue(float value)
		{
			return -value;
		}
	}
}
