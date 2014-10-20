using UnityEngine;
using System.Collections;
using System;

public class PanCamera : MonoBehaviour {

	Vector2 _lastMousePosition;

	// Use this for initialization
	void Start () {
		_lastMousePosition = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCamera();
	}

	
	/// <summary>
	/// Move/pan the camera around.
	/// </summary>
	void UpdateCamera()
	{
		//prevent the mouse from updating the game when its outside.
		Rect screenRect = new Rect(0,0, Screen.width, Screen.height);
		if (!screenRect.Contains(Input.mousePosition))
			return;

		float cameraPanSpeed = .1f;
		// cameraUpDownSpeed = 500;
		
		//this.Position = Vector3.Lerp(this.Position, new Vector3(this.Position.X, cameraHeight, this.Position.Z), .1f);
		Vector3 deltaPosition = RightMouseDown(cameraPanSpeed);
		
		this.transform.position += deltaPosition;
		//this.Target += deltaPosition;
	}

	public Vector3 RightMouseDown(float moveSpeed)
	{
		Vector3 deltaPosition = Vector3.zero;
		var mouseState = Input.mousePosition;

		Vector2 mousePosition = new Vector2(mouseState.x, mouseState.y);
		bool mousePressed = Input.GetMouseButton(0);

		if (mousePressed)
		{
			Vector2 delta = mousePosition - _lastMousePosition;
			deltaPosition += new Vector3(delta.x, 0, delta.y) * -(float)moveSpeed;
		}
		
		_lastMousePosition = mousePosition;
		
		return deltaPosition;
	}
}
