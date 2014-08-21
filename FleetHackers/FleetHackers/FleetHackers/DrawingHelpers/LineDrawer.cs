using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.DrawingHelpers
{
	public class LineDrawer : IDisposable
	{
		/// <summary>
		/// Maximum number of vertices allowed.
		/// </summary>
		public const int MAX_VERTS = 2000;

		/// <summary>
		/// Maximum number of indices.
		/// </summary>
		public const int MAX_INDICES = 2000;

		/// <summary>
		/// Basic effect used for rendering polygon lines.
		/// </summary>
		private BasicEffect basicEffect;

		/// <summary>
		/// Dynamic vertex buffer.
		/// </summary>
		private DynamicVertexBuffer vertexBuffer;

		/// <summary>
		/// Dynamic index buffer.
		/// </summary>
		private DynamicIndexBuffer indexBuffer;

		/// <summary>
		/// An array of indices.
		/// </summary>
		private ushort[] indices = new ushort[MAX_INDICES];

		/// <summary>
		/// A bunch of vertices.
		/// </summary>
		private VertexPositionColor[] vertices = new VertexPositionColor[MAX_VERTS];

		/// <summary>
		/// Number of used indices.
		/// </summary>
		private int indexCount;

		/// <summary>
		/// Number of vertices.
		/// </summary>
		private int vertexCount;

		/// <summary>
		/// Main line drawer constructor.
		/// </summary>
		/// <param name="graphicsDevice">Graphics device for rendering.</param>
		public LineDrawer(GraphicsDevice graphicsDevice)
		{
			vertexBuffer = new DynamicVertexBuffer(graphicsDevice, typeof(VertexPositionColor), MAX_VERTS, BufferUsage.WriteOnly);
			indexBuffer = new DynamicIndexBuffer(graphicsDevice, typeof(ushort), MAX_INDICES, BufferUsage.WriteOnly);

			basicEffect = new BasicEffect(graphicsDevice);
			basicEffect.LightingEnabled = false;
			basicEffect.VertexColorEnabled = true;
			basicEffect.TextureEnabled = false;
		}

		/// <summary>
		/// Begin drawing lines.
		/// </summary>
		/// <param name="view">Camera view.</param>
		/// <param name="projection">Camera projection.</param>
		public void Begin(Matrix view, Matrix projection)
		{
			basicEffect.World = Matrix.Identity;
			basicEffect.View = view;
			basicEffect.Projection = projection;

			vertexCount = 0;
			indexCount = 0;
		}

		/// <summary>
		/// Check if we have enough room to draw an object.
		/// </summary>
		/// <param name="numVerts">Number of vertices</param>
		/// <param name="numIndices">Nnumber of indices.</param>
		/// <returns></returns>
		private bool Reserve(int numVerts, int numIndices)
		{
			if (numVerts > MAX_VERTS || numIndices > MAX_INDICES)
			{
				return false;
			}

			if (vertexCount + numVerts > MAX_VERTS || indexCount + numIndices >= MAX_INDICES)
			{
				End();
			}

			return true;
		}

		/// <summary>
		/// Draw a standard wire grid.
		/// </summary>
		/// <param name="xAxis">Range of the X axis of the grid.</param>
		/// <param name="yAxis">Range of the Y axis of the grid.</param>
		/// <param name="origin">Where the corner of the grid starts.</param>
		/// <param name="iXDivisions">How many divisions along the X axis.</param>
		/// <param name="iYDivisions">How many division along the Y axis.</param>
		/// <param name="color">Color of the grid.</param>
		public void DrawWireGrid(Vector3 xAxis, Vector3 yAxis, Vector3 origin, int iXDivisions, int iYDivisions, Color color)
		{
			Vector3 pos = origin;
			Vector3 step = xAxis / iXDivisions;

			for (int i = 0; i <= iXDivisions; i++)
			{
				//draw line
				DrawLine(pos, pos + yAxis, color);
				pos += step;
			}

			pos = origin;
			step = yAxis / iYDivisions;

			for (int i = 0; i <= iYDivisions; i++)
			{
				//draw line
				DrawLine(pos, pos + xAxis, color);
				pos += step;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridPosition"></param>
		/// <param name="size"></param>
		/// <param name="color"></param>
		public void DrawHexagonGrid(Vector2 gridStartingPosition, Vector2 gridSize, float size, Color color)
		{

			int xSize = (int)gridSize.X;
			int ySize = (int)gridSize.Y;
			Vector2 gridPosition = gridStartingPosition;

			float angleDown = 2 * MathHelper.Pi / 6 * (0 + 0.5f);
			float angleUp = 2 * MathHelper.Pi / 6 * (3 + 0.5f);
			float height1 = size * (float)Math.Sin(angleDown) - size * (float)Math.Sin(angleUp);
			//Console.WriteLine(height1);

			float angleSideRight = 2 * MathHelper.Pi / 6 * (1 + 0.5f);
			float angleSideLeft = 2 * MathHelper.Pi / 6 * (5 + 0.5f);
			float diameter = size * (float)Math.Cos(angleSideLeft) - size * (float)Math.Cos(angleSideRight);
			//Console.WriteLine(diameter);

			for (int i = 0; i <= xSize; i++)
			{
				for (int j = 0; j < ySize; j++ )
				{
					Vector3[] hexPoints = new Vector3[6];
					for (int k = 0; k < 6; k++)
					{
						float angle = 2 * MathHelper.Pi / 6 * (k + 0.5f);
						float X = gridPosition.X + size * (float)Math.Cos(angle);
						float Y = gridPosition.Y + size * (float)Math.Sin(angle);

						hexPoints[k] = new Vector3(X, 0, Y);

						if (k != 0)
						{
							DrawLine(hexPoints[k], hexPoints[k - 1], color);
						}
						if (k == 5)
						{
							DrawLine(hexPoints[0], hexPoints[5], color);
							gridPosition.Y += 3 * size;
						}
					}
				}
				float yOffset = ((i +1) % 2);
				gridPosition.Y = gridStartingPosition.Y + yOffset * 1.5f * size;
				gridPosition.X += diameter;
				
				//gridPosition = new Vector2(gridStartingPosition.X + size  * 1.74f* i, gridStartingPosition.Y);
			}
		}

		/// <summary>
		/// Draw a simple line.
		/// </summary>
		/// <param name="startVector">Starting point of the line.</param>
		/// <param name="endVector">Ending point of the line.</param>
		/// <param name="color">Color assigned to the line.</param>
		public void DrawLine(Vector3 startVector, Vector3 endVector, Color color)
		{
			if (Reserve(2,2))
			{
				indices[indexCount++] = (ushort)vertexCount;
				indices[indexCount++] = (ushort)(vertexCount + 1);
				vertices[vertexCount++] = new VertexPositionColor(startVector, color);
				vertices[vertexCount++] = new VertexPositionColor(endVector, color);
			}
		}

		/// <summary>
		/// Draw a shape thing.
		/// </summary>
		/// <param name="postionArray">An array of positions.</param>
		/// <param name="indexArray">An array of indices.</param>
		/// <param name="color">Color of the lines. Note, that it is possible to
		/// have one color transition in the other color.</param>
		public void DrawShape(Vector3[] postionArray, ushort[] indexArray, Color color)
		{
			if(Reserve(postionArray.Length, indexArray.Length))
			{
				for (int i = 0; i < indexArray.Length; i++)
				{
					indices[indexCount] = (ushort)(vertexCount + indexArray[i]);
				}

				for (int i = 0; i < postionArray.Length; i++)
				{
					vertices[vertexCount++] = new VertexPositionColor(postionArray[i], color);
				}
			}
		}

		/// <summary>
		/// Ends line drawing and resores render states.
		/// </summary>
		public void End()
		{
			if (indexCount > 0)
			{
				vertexBuffer.SetData(vertices, 0, vertexCount, SetDataOptions.Discard);
				indexBuffer.SetData(indices, 0, indexCount, SetDataOptions.Discard);

				GraphicsDevice graphicsDevice = basicEffect.GraphicsDevice;
				graphicsDevice.SetVertexBuffer(vertexBuffer);
				graphicsDevice.Indices = indexBuffer;

				foreach(EffectPass pass in basicEffect.CurrentTechnique.Passes)
				{
					pass.Apply();
					graphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, vertexCount, 0, indexCount / 2);
				}

				graphicsDevice.SetVertexBuffer(null);
				graphicsDevice.Indices = null;
			}

			indexCount = 0;
			vertexCount = 0;
		}

		/// <summary>
		/// A deconstructor? Cool.
		/// </summary>
		~LineDrawer()
		{ 
			Dispose(false);
		}
		
		/// <summary>
		/// Dispose of resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose of all those drawing tools.
		/// </summary>
		/// <param name="disposing">Tells whether we want to throw things away.</param>
		protected virtual void Dispose(bool disposing)
		{
			if(disposing)
			{
				if (vertexBuffer != null)
				{
					vertexBuffer.Dispose();
				}

				if (indexBuffer != null)
				{
					indexBuffer.Dispose();
				}

				if (basicEffect != null)
				{
					basicEffect.Dispose();
				}
			}
		}
	}
}
