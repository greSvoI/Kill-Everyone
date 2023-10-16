using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
	private Camera mainCamera;
	private Ray ray;
	private RaycastHit hit;
	public LayerMask layerMask;
	private void Start()
	{
		mainCamera = Camera.main;
	}
	private void Update()
	{
		Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
		ray = mainCamera.ScreenPointToRay(screenCenter);

		Physics.Raycast(ray, out hit);
		transform.position = hit.point;
	}
}
