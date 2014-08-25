using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.Models
{
	public class BillboardSystem
	{
		/// <summary>
		/// Vertex buffer field.
		/// </summary>
		private VertexBuffer _vertexBuffer;

		/// <summary>
		/// Index buffer field.
		/// </summary>
		private IndexBuffer _indexBuffer;

		/// <summary>
		/// Position of each billboard.
		/// </summary>
		private VertexPositionTexture[] _particles;

		/// <summary>
		/// Indices of vertices.
		/// </summary>
		private int[] _indices;

		/// <summary>
		/// Number of billboards field.
		/// </summary>
		private int _numberOfBillboards;

		/// <summary>
		/// Size of the billboard.
		/// </summary>
		private Vector2 _billboardSize;

		/// <summary>
		/// Texture to use for this billboard.
		/// </summary>
		private Texture2D _texture;

		/// <summary>
		/// Graphics device handler.
		/// </summary>
		private GraphicsDevice _graphicsDevice;

		/// <summary>
		/// Effectfield.
		/// </summary>
		private Effect _effect;

		private bool _ensureOcclusion = true;

		/// <summary>
		/// Billboard constructor.
		/// </summary>
		/// <param name="graphicsDevice"></param>
		/// <param name="content"></param>
		/// <param name="texture"></param>
		/// <param name="billboardSize"></param>
		/// <param name="particlePositions"></param>
		public BillboardSystem(GraphicsDevice graphicsDevice, ContentManager content, Texture2D texture, Vector2 billboardSize, Vector3[] particlePositions)
		{
			_numberOfBillboards = particlePositions.Length;
			_billboardSize = billboardSize;
			_graphicsDevice = graphicsDevice;
			_texture = texture;

			_effect = content.Load<Effect>(@"Shaders\BillboardEffect");

			GenerateParticles(particlePositions);
		}

		/// <summary>
		/// This method generates particles for the billboard system.
		/// </summary>
		/// <param name="particlePositions">Position of the billboards.</param>
		private void GenerateParticles(Vector3[] particlePositions)
		{
			_particles = new VertexPositionTexture[_numberOfBillboards * 4];
			_indices = new int[_numberOfBillboards * 6];

			int x = 0;
			for (int i = 0; i < _numberOfBillboards * 4; i += 4)
			{
				Vector3 position = particlePositions[i / 4];

				// four corners of the square.
				_particles[i + 0] = new VertexPositionTexture(position, new Vector2(0, 0));
				_particles[i + 1] = new VertexPositionTexture(position, new Vector2(0, 1));
				_particles[i + 2] = new VertexPositionTexture(position, new Vector2(1, 1));
				_particles[i + 3] = new VertexPositionTexture(position,	new Vector2(1, 0));

				// Add 6 indices to form two triangles
				_indices[x++] = i + 0;
				_indices[x++] = i + 3;
				_indices[x++] = i + 2;
				_indices[x++] = i + 2;
				_indices[x++] = i + 1;
				_indices[x++] = i + 0;
			}

			_vertexBuffer = new VertexBuffer(_graphicsDevice, typeof(VertexPositionTexture), _numberOfBillboards * 4, BufferUsage.WriteOnly);
			_vertexBuffer.SetData<VertexPositionTexture>(_particles);

			_indexBuffer = new IndexBuffer(_graphicsDevice, IndexElementSize.ThirtyTwoBits, _numberOfBillboards * 6, BufferUsage.WriteOnly);
			_indexBuffer.SetData<int>(_indices);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="view"></param>
		/// <param name="projection"></param>
		/// <param name="up"></param>
		/// <param name="right"></param>
		private void SetEffectParameters(Matrix view, Matrix projection, Vector3 up, Vector3 right)
		{
			_effect.Parameters["ParticleTexture"].SetValue(_texture);
			_effect.Parameters["View"].SetValue(view);
			_effect.Parameters["Projection"].SetValue(projection);
			_effect.Parameters["Size"].SetValue(_billboardSize / 2f);
			_effect.Parameters["Up"].SetValue(up);
			_effect.Parameters["Side"].SetValue(right);

			_effect.CurrentTechnique.Passes[0].Apply();
		}

		/// <summary>
		/// Class level draw method to encapsulate billboard draws.
		/// </summary>
		/// <param name="view"></param>
		/// <param name="projection"></param>
		/// <param name="up"></param>
		/// <param name="right"></param>
		public void Draw(Matrix view, Matrix projection, Vector3 up, Vector3 right)
		{
			_graphicsDevice.SetVertexBuffer(_vertexBuffer);
			_graphicsDevice.Indices = _indexBuffer;

			// Enable alpha blending
			_graphicsDevice.BlendState = BlendState.AlphaBlend;

			SetEffectParameters(view, projection, up, right);

			if (_ensureOcclusion)
			{
				DrawOpaquePixels();
				DrawTransparentPixels();
			}
			else
			{
				_graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
				_effect.Parameters["AlphaTest"].SetValue(false);
				DrawBillboards();
			}

			// Reset render states
			_graphicsDevice.BlendState = BlendState.Opaque;
			_graphicsDevice.DepthStencilState = DepthStencilState.Default;

			// clean up
			_graphicsDevice.SetVertexBuffer(null);
			_graphicsDevice.Indices = null;
		}

		private void DrawOpaquePixels()
		{
			_graphicsDevice.DepthStencilState = DepthStencilState.Default;

			_effect.Parameters["AlphaTest"].SetValue(true);
			_effect.Parameters["AlphaTestGreater"].SetValue(true);

			DrawBillboards();
		}

		private void DrawTransparentPixels()
		{
			_graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;

			_effect.Parameters["AlphaTest"].SetValue(true);
			_effect.Parameters["AlphaTestGreater"].SetValue(false);

			DrawBillboards();
		}

		private void DrawBillboards()
		{
			_effect.CurrentTechnique.Passes[0].Apply();

			_graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4 * _numberOfBillboards, 0, 2 * _numberOfBillboards);
		}

	}
}
