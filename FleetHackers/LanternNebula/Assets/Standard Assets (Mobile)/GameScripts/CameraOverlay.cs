using UnityEngine;
using System.Collections;
using System;
using FleetHackers.HexagonLibrary;
using System.Collections.Generic;

public class CameraOverlay : MonoBehaviour {

	public Material mat;
	Plane groundPlane;

	// Use this for initialization
	void Start () {
		Camera.main.backgroundColor = (Color.black);
		groundPlane = new Plane(Vector3.up, 0);
		Screen.showCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W))
		{
			this.transform.position += Vector3.forward;
		}
		if (Input.GetKey(KeyCode.S))
		{
			this.transform.position += Vector3.back;
		}
		if (Input.GetKey(KeyCode.A))
		{
			this.transform.position += Vector3.left;
		}
		if (Input.GetKey(KeyCode.D))
		{
			this.transform.position += Vector3.right;
		}
	
		if (Input.GetKey(KeyCode.E))
		{
			this.transform.position += Vector3.up;
		}
		if (Input.GetKey(KeyCode.C))
		{
			this.transform.position += Vector3.down;
		}
	}

	void OnPostRender()
	{
		Overlay();
		//Debug.Log("ruben is cool!");
	}

	void Overlay()
	{
		mat.SetPass(0);

		GL.Begin(GL.LINES);
		
		// DrawLine(Vector3.zero, Vector3.one*100); Test

		// Standard method without the grid.
		//DrawHexagonGrid(Vector2.zero, new Vector2(2,2), 10, Color.cyan);

		// New method which is grid based.
		RenderHexGrid();

		GL.End();
		
	}

	public void RenderHexGrid()
	{
		HexagonMap hc = new HexagonMap();

		AxisCoordinate[] ac = new AxisCoordinate[]{
			new AxisCoordinate(0,0),
			new AxisCoordinate(1,0),
			new AxisCoordinate(2,0),
			new AxisCoordinate(3,0),

			new AxisCoordinate(0,1),
			new AxisCoordinate(1,1),
			new AxisCoordinate(2,1),
			new AxisCoordinate(3,1),

			new AxisCoordinate(-1,2),
			new AxisCoordinate(0,2),
			new AxisCoordinate(1,2),
			new AxisCoordinate(2,2),

			new AxisCoordinate(-1,3),
			new AxisCoordinate(0,3),
			new AxisCoordinate(1,3),
			new AxisCoordinate(2,3),
		};

		var ac1 = new List<AxisCoordinate>();

		foreach (AxisCoordinate a in ac)
		{
			DrawHexagon(a, 10);
		}
	}

	public void DrawHexagon(AxisCoordinate axisCorrdinate, float size)
	{
		Color color = Color.white;

		// using new formula
		Vector2 gridPosition = (new Vector2(
			size * (Mathf.Sqrt(3) * (axisCorrdinate.Q + axisCorrdinate.R*0.5f)),
			size * (axisCorrdinate.R * 1.5f)));

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float rayDistance;
		Vector3 mousePlanePosition;
		Color crossair = Color.white;


		if (groundPlane.Raycast(ray, out rayDistance))
		{
			mousePlanePosition = ray.GetPoint(rayDistance);
			DrawLine(
				new Vector3(mousePlanePosition.x + size, 0, mousePlanePosition.z ),
				new Vector3(mousePlanePosition.x - size, 0, mousePlanePosition.z ), Color.green);
			DrawLine(
				new Vector3(mousePlanePosition.x, 0, mousePlanePosition.z + size),
				new Vector3(mousePlanePosition.x, 0, mousePlanePosition.z - size), Color.green);

			float angleSideRight = 2 * Mathf.PI / 6 * (1 + 0.5f);
			float angleSideLeft = 2 * Mathf.PI / 6 * (5 + 0.5f);
			float diameter = size * (float)Math.Cos(angleSideLeft) - size * (float)Math.Cos(angleSideRight);

			var angle = 2.0f * Mathf.PI / 6.0f * (0.5f);
			var x = gridPosition.x + size * Mathf.Cos(angle);
			var y = gridPosition.y + size * Mathf.Sin(angle);
			//var selectedHex = HexagonCoordinates.ConvertPointCoordToAxialCoord(mousePlanePosition.x + 5, mousePlanePosition.z + 10f, diameter);
			var selectedHex = HexagonMap.ConvertPointCoordToAxialCoord(mousePlanePosition.x + diameter, mousePlanePosition.z + size, diameter, size);
			if (axisCorrdinate.Equals(selectedHex))
			{
				crossair = Color.red;
			}
		}
		
		Vector3[] hexPoints = new Vector3[6];
		for (int k = 0; k < 6; k++)
		{
			var angle = 2.0f * Mathf.PI / 6.0f * (k + 0.5f);
			
			var x = gridPosition.x + size * Mathf.Cos(angle);
			var y = gridPosition.y + size * Mathf.Sin(angle);

			if(k == 0)
			{
				hexPoints[0] = new Vector3(x, 0, y);
			}
			else
			{
				hexPoints[k] = new Vector3(x, 0, y);
				DrawLine(hexPoints[k - 1], hexPoints[k], color);
			}
		}
		DrawLine(hexPoints[5], hexPoints[0], color);

		DrawLine(
			new Vector3(gridPosition.x + 5, 0, gridPosition.y),
			new Vector3(gridPosition.x - 5, 0, gridPosition.y), crossair);
		DrawLine(
			new Vector3(gridPosition.x, 0, gridPosition.y +5 ),
			new Vector3(gridPosition.x, 0, gridPosition.y - 5), crossair);
	}

	// TODO: Use hexagon library later. So we can navigate stuff.
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="gridPosition"></param>
	/// <param name="size"></param>
	/// <param name="color"></param>
	public void DrawHexagonGrid(Vector2 gridStartingPosition, Vector2 gridSize, float size, Color color)
	{
		
		int xSize = (int)gridSize.x;
		int ySize = (int)gridSize.y;
		Vector2 gridPosition = gridStartingPosition;
		
		float angleDown = 2 * Mathf.PI / 6 * (0 + 0.5f);
		float angleUp = 2 * Mathf.PI / 6 * (3 + 0.5f);
		float height1 = size * (float)Math.Sin(angleDown) - size * (float)Math.Sin(angleUp);
		//Console.WriteLine(height1);
		
		float angleSideRight = 2 * Mathf.PI / 6 * (1 + 0.5f);
		float angleSideLeft = 2 * Mathf.PI / 6 * (5 + 0.5f);
		float diameter = size * (float)Math.Cos(angleSideLeft) - size * (float)Math.Cos(angleSideRight);
		//Console.WriteLine(diameter);
		
		for (int i = 0; i <= xSize; i++)
		{
			for (int j = 0; j < ySize; j++ )
			{
				Vector3[] hexPoints = new Vector3[6];
				for (int k = 0; k < 6; k++)
				{
					float angle = 2 * Mathf.PI / 6 * (k + 0.5f);
					float X = gridPosition.x + size * (float)Math.Cos(angle);
					float Y = gridPosition.y + size * (float)Math.Sin(angle);
					
					hexPoints[k] = new Vector3(X, 0, Y);
					
					if (k != 0)
					{
						DrawLine(hexPoints[k], hexPoints[k - 1], color);
					}
					if (k == 5)
					{
						DrawLine(hexPoints[0], hexPoints[5], color);
						gridPosition.y += 3 * size;
					}
				}
			}
			float yOffset = ((i +1) % 2);
			gridPosition.y = gridStartingPosition.y + yOffset * 1.5f * size;
			gridPosition.x += diameter;
			
			//gridPosition = new Vector2(gridStartingPosition.X + size  * 1.74f* i, gridStartingPosition.Y);
		}
	}
	
	public void DrawLine(Vector3 p1, Vector3 p2, Color color)
	{
		mat.SetPass(0);
		GL.Begin(GL.LINES);
		
		GL.Color(color);
		
		GL.Vertex3(p1.x, p1.y, p1.z);
		GL.Vertex3(p2.x, p2.y, p2.z);
		
		GL.End();
	}
}
