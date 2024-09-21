using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private float zoomSpeed = 0.5f;
	[SerializeField] private float minZoom = 2f;
	[SerializeField] private float maxZoom = 10f;
	[SerializeField] private float dragSpeed = 2f;

	// Các giới hạn cho camera
	[SerializeField] private float minX = -10f;
	[SerializeField] private float maxX = 10f;
	[SerializeField] private float minY = -10f;
	[SerializeField] private float maxY = 10f;

	private Camera cam;
	private Vector3 dragOrigin;

	private void Start()
	{
		cam = Camera.main;
	}

	private void Update()
	{
		// Zoom
		float scrollData = Input.GetAxis("Mouse ScrollWheel");
		cam.orthographicSize -= scrollData * zoomSpeed;
		cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);

		// Drag
		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
		}

		if (Input.GetMouseButton(0))
		{
			Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
			cam.transform.position += difference * dragSpeed;

			// Giới hạn vị trí camera sau khi kéo
			cam.transform.position = new Vector3(
				Mathf.Clamp(cam.transform.position.x, minX, maxX),
				Mathf.Clamp(cam.transform.position.y, minY, maxY),
				cam.transform.position.z
			);
		}
	}
}
