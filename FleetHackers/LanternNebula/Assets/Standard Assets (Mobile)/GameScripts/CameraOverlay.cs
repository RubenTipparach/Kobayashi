using UnityEngine;
using System.Collections;
using System;
using FleetHackers.DrawingHelpers;

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
		HexagonCoordinates hc = new HexagonCoordinates();
		// CubeCoordinates cubeCoordinate = new CubeCoordinates(0,0,0);
		// DrawHexagon(cubeCoordinate, 10);
		// cubeCoordinate = hc.MoveInCubeCoordinates(cubeCoordinate, 0);
		// DrawHexagon(cubeCoordinate, 10);

		//AxisCoordinates ac = new AxisCoordinates(0,0);	
		//for(int i = 0; i < 10; i++)
		//{
		//ac = hc.MoveInAxialCoordinates(ac, 1);
		//DrawHexagon(ac, 10);
		//}
		AxisCoordinates[] ac = new AxisCoordinates[]{
			new AxisCoordinates(0,0),
			new AxisCoordinates(1,0),
			new AxisCoordinates(2,0),
			new AxisCoordinates(3,0),

			new AxisCoordinates(0,1),
			new AxisCoordinates(1,1),
			new AxisCoordinates(2,1),
			new AxisCoordinates(3,1),

			new AxisCoordinates(-1,2),
			new AxisCoordinates(0,2),
			new AxisCoordinates(1,2),
			new AxisCoordinates(2,2),

			new AxisCoordinates(-1,3),
			new AxisCoordinates(0,3),
			new AxisCoordinates(1,3),
			new AxisCoordinates(2,3),
		};
		AxisCoordinates[] ac1 = new AxisCoordinates[]{
			new AxisCoordinates(0,0),
		};
		foreach (AxisCoordinates a in ac)
		{
			DrawHexagon(a, 10);
		}
	}


	public void DrawHexagon(AxisCoordinates axisCorrdinate, float size)
	{
		Color color = Color.white;

		// using new formula
		Vector2 gridPosition = (new Vector2(
			size * (Mathf.Sqrt(3) * (axisCorrdinate.Q + axisCorrdinate.R*0.5f)),
			size * (axisCorrdinate.R * 1.5f)));

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float rayDistance;
		Vector3 mousePlanePosition;

		if (groundPlane.Raycast(ray, out rayDistance))
		{
			mousePlanePosition = ray.GetPoint(rayDistance);
			//float relativeQ = (1.0f/3.0f * Mathf.Sqrt(3) * gridPosition.x - 1.0f/3.0f * gridPosition.y)/size;
			//float relativeR = 2.0f/3.0f * gridPosition.y / size;
			//to find the approximation, converto cube coordinates, round the cube, then convert back to axial for optim.
			//or not, just use this hack method
			DrawLine(
				new Vector3(mousePlanePosition.x + size, 0, mousePlanePosition.z ),
				new Vector3(mousePlanePosition.x - size, 0, mousePlanePosition.z ), Color.green);
			DrawLine(
				new Vector3(mousePlanePosition.x, 0, mousePlanePosition.z + size),
				new Vector3(mousePlanePosition.x, 0, mousePlanePosition.z - size), Color.green);

			var selectedHex = HexagonCoordinates.ConvertPointCoordToAxialCoord(mousePlanePosition.x + 5, mousePlanePosition.z + 10f, size);
			//var selectedHex = HexagonCoordinates.ConvertPointCoordToAxialCoord(mousePlanePosition.x, mousePlanePosition.z, size);
			if (axisCorrdinate.Equals(selectedHex))
			{
				color = Color.blue;
			}
		}
		
		Vector3[] hexPoints = new Vector3[6];
		for (int k = 0; k < 6; k++)
		{
			float angle = 2 * Mathf.PI / 6 * (k + 0.5f);
			float X = gridPosition.x + (size - 1.0f) * (float)Math.Cos(angle);
			float Y = gridPosition.y + (size - 1.0f) * (float)Math.Sin(angle);
			
			hexPoints[k] = new Vector3(X, 0, Y);
			
			if (k != 0)
			{
				DrawLine(hexPoints[k], hexPoints[k - 1], color);
			}
			if (k == 5)
			{
				DrawLine(hexPoints[0], hexPoints[5], color);
				gridPosition.y += 3 ;
			}
		}
		DrawLine(
			new Vector3(gridPosition.x + 5, 0, gridPosition.y - 2.5f),
			new Vector3(gridPosition.x - 5, 0, gridPosition.y - 2.5f), Color.red);
		DrawLine(
			new Vector3(gridPosition.x, 0, gridPosition.y + 2.5f),
			new Vector3(gridPosition.x, 0, gridPosition.y - 7.5f), Color.red);
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
