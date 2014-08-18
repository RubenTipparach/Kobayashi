using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.Models
{
	public class BasicModel
	{
		/// <summary>
		/// Used to hold transformation operations.
		/// </summary>
		private Matrix[] modelTransforms;

		/// <summary>
		/// Graphics device for controling graphics. Modularizing this class.
		/// </summary>
		private GraphicsDevice graphicsDevice;

		/// <summary>
		/// Bounding sphere to be used for culling.
		/// </summary>
		private BoundingSphere boundingSphere;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="model">Pass in a loaded model object.</param>
		/// <param name="position">Position of the object.</param>
		/// <param name="Rotation">Orientation of the object.</param>
		/// <param name="Scale">The size of the object with relation to its absolute size.</param>
		/// <param name="graphicsDevice">Allows contol of the graphics device object.</param>
		public BasicModel(Model model, Vector3 position, Vector3 rotation, Vector3 scale, GraphicsDevice graphicsDevice)
		{
			this.Model = model;
			this.Position = position;
			this.Rotation = rotation;
			this.Scale = scale;
			this.graphicsDevice = graphicsDevice;

			modelTransforms = new Matrix[model.Bones.Count];
			model.CopyAbsoluteBoneTransformsTo(modelTransforms);
			BuildBoundingSphere();
		}

		/// <summary>
		/// This method is responsible for allowing the Model to render itself.
		/// TO DO: We can make this into some kind of interface.
		/// </summary>
		/// <param name="view">Camera postion and rotation.</param>
		/// <param name="projection">Camera viewing angle and depth.</param>
		public void Draw(Matrix view, Matrix projection)
		{
			Matrix baseWorld = Matrix.CreateScale(Scale)
				* Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z)
				* Matrix.CreateTranslation(Position);

			foreach (ModelMesh mesh in Model.Meshes)
			{
				Matrix localWorld = modelTransforms[mesh.ParentBone.Index] * baseWorld;

				foreach ( ModelMeshPart meshpart in mesh.MeshParts)
				{
					BasicEffect effect = (BasicEffect)meshpart.Effect;
					effect.World = localWorld;
					effect.View = view;
					effect.Projection = projection;

					effect.EnableDefaultLighting();
				}

				mesh.Draw();
			}
		}

		/// <summary>
		/// Create a bounding sphere for this model object.
		/// </summary>
		private void BuildBoundingSphere()
		{
			BoundingSphere boundingSphere = new BoundingSphere(Vector3.Zero, 0);

			foreach(ModelMesh mesh in Model.Meshes)
			{
				BoundingSphere transformed = mesh.BoundingSphere.Transform(
					modelTransforms[mesh.ParentBone.Index]);
				boundingSphere = BoundingSphere.CreateMerged(boundingSphere, transformed);
			}

			this.boundingSphere = boundingSphere;
		}

		/// <summary>
		/// Gets the bounding sphere object.
		/// </summary>
		public BoundingSphere BoundingSphere
		{
			get
			{
				Matrix worldTransforms = Matrix.CreateScale(Scale) * Matrix.CreateTranslation(Position);
				BoundingSphere transformed = boundingSphere;
				transformed = transformed.Transform(worldTransforms);

				return transformed;
			}
		}

		/// <summary>
		/// Gets or sets postion of the model.
		/// </summary>
		public Vector3 Position
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the rotation of the model.
		/// </summary>
		public Vector3 Rotation
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the scale.
		/// </summary>
		public Vector3 Scale
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the model object.
		/// </summary>
		public Model Model
		{
			get;
			set;
		}

	}
}
