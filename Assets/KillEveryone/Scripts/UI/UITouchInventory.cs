using KillEveryone;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITouchInventory : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	Vector2 clampedDirection;
	public void OnDrag(PointerEventData eventData)
	{
		clampedDirection = ClampValuesToMagnitude(eventData.delta);
		EventManager.TouchInventory?.Invoke(clampedDirection);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		clampedDirection = Vector2.zero;
		EventManager.TouchInventory?.Invoke(clampedDirection);
	}

	private Vector2 ClampValuesToMagnitude(Vector2 position)
	{
		return Vector2.ClampMagnitude(position, 1);
	}
}
