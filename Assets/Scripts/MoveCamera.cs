using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour 
{
	//
	// VARIABLES
	//
	
	public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
	public float panSpeed = 1.0f;		// Speed of the camera when being panned
	public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth
	
	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isPanning;		// Is the camera being panned?
	private bool isRotating;	// Is the camera being rotated?
	private bool isZooming;		// Is the camera zooming?
	
	//
	// UPDATE
	//
	
	void Update () 
	{
		// Get the left mouse button
		if(Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}
		
		// Get the right mouse button
		if(Input.GetMouseButtonDown(0))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isPanning = true;
		}
		
		// Get the middle mouse button
		if(Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isZooming = true;

		}
		
		// Disable movements on button release
		if (!Input.GetMouseButton(1)) isRotating=false;
		if (!Input.GetMouseButton(0)) isPanning=false;
		if (!Input.GetMouseButton(2)) isZooming=false;
		
		// Rotate camera along X and Y axis
		if (isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin) * 50.0f;
			mouseOrigin = Input.mousePosition;
			
			transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
		}
		
		// Move the camera on it's XY plane
		if (isPanning)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			mouseOrigin = Input.mousePosition;
			
			Vector3 move = new Vector3(-pos.x * panSpeed, -pos.y * panSpeed, 0);
			transform.Translate(move, Space.Self);
		}
		
		// Move the camera linearly along Z axis
		if (isZooming)
		{
			/*
			Debug.Log("input" 	
			          + Input.mousePosition.x.ToString() + " "
			          + Input.mousePosition.y.ToString() + " "
			          + Input.mousePosition.z.ToString() + " ");
			Debug.Log("Origin" 	
			          + mouseOrigin.x.ToString() + " "
			          + mouseOrigin.y.ToString() + " "
			          + mouseOrigin.z.ToString() + " ");
			*/
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			mouseOrigin = Input.mousePosition;
			
			Vector3 move = pos.y * zoomSpeed * transform.forward; 
			transform.Translate(move, Space.World);
		}
	}
}