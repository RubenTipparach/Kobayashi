using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackers.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.Models
{
	public class BasicModel
	{
		/// <summary>
		/// Used to hold transformation operations.
		/// </summary>
		private Matrix[] _modelTransforms;

		/// <summary>
		/// Graphics device for controling graphics. Modularizing this class.
		/// </summary>
		private GraphicsDevice _graphicsDevice;

		/// <summary>
		/// Bounding sphere to be used for culling.
		/// </summary>
		private BoundingSphere _boundingSphere;

		/// <summary>
		/// Gets or sets the material.
		/// </summary>
		/// <value>
		/// The material.
		/// </value>
		public Material Material { get; set; }


		/// <summary>
		/// Initializes a new instance of the <see cref="BasicModel"/> class.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation in Quaternions.</param>
		/// <param name="scale">The scale.</param>
		/// <param name="graphicsDevice">The graphics device.</param>
		public BasicModel(Model model, Vector3 position, Vector3 rotation, Vector3 scale, GraphicsDevice graphicsDevice)
		{
			Quaternion quaternionRotation = Quaternion.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z);
			Initialize(model, position, quaternionRotation, scale, graphicsDevice);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicModel"/> class.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation in Euler Angles.</param>
		/// <param name="scale">The scale.</param>
		/// <param name="graphicsDevice">The graphics device.</param>
		public BasicModel(Model model, Vector3 position, Quaternion rotation, Vector3 scale, GraphicsDevice graphicsDevice)
		{
			Initialize(model, position, rotation, scale, graphicsDevice);
		}

		/// <summary>
		/// Initializes the specified model.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="position">The position.</param>
		/// <param name="rotation">The rotation.</param>
		/// <param name="scale">The scale.</param>
		/// <param name="graphicsDevice">The graphics device.</param>
		public void Initialize(Model model, Vector3 position, Quaternion rotation, Vector3 scale, GraphicsDevice graphicsDevice)
		{
			this.Model = model;
			this.Material = new Material();

			this.Position = position;
			this.Rotation = rotation;
			this.Scale = scale;

			_graphicsDevice = graphicsDevice;

			_modelTransforms = new Matrix[model.Bones.Count];
			model.CopyAbsoluteBoneTransformsTo(_modelTransforms);

			BuildBoundingSphere();
			GenerateTags();
		}

		/// <summary>
		/// Generates the tags.
		/// </summary>
		private void GenerateTags()
		{
			foreach (ModelMesh mesh in Model.Meshes)
				foreach (ModelMeshPart part in mesh.MeshParts)
					if (part.Effect is BasicEffect)
					{
						BasicEffect effect = (BasicEffect)part.Effect;
						MeshTag tag = new MeshTag(effect.DiffuseColor, effect.Texture,
							effect.SpecularPower);
						part.Tag = tag;
					}
		}

		/// <summary>
		/// This method is responsible for allowing the Model to render itself.
		/// TO DO: We can make this into some kind of interface.
		/// </summary>
		/// <param name="view">Camera postion and rotation.</param>
		/// <param name="projection">Camera viewing angle and depth.</param>
		public void Draw(Matrix view, Matrix projection, Vector3 CameraPosition)
		{
			//Matrix baseWorld = Matrix.CreateScale(Scale)
			//	* Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z)
			//	* Matrix.CreateTranslation(Position);

			Matrix baseWorld = Matrix.CreateScale(Scale)
				* Matrix.CreateFromQuaternion(Rotation)
				* Matrix.CreateTranslation(Position);

			foreach (ModelMesh mesh in Model.Meshes)
			{
				Matrix localWorld = _modelTransforms[mesh.ParentBone.Index] * baseWorld;

				foreach (ModelMeshPart meshpart in mesh.MeshParts)
				{
					Effect effect = meshpart.Effect;
					if (effect is BasicEffect)
					{
						((BasicEffect)effect).World = localWorld;
						((BasicEffect)effect).View = view;
						((BasicEffect)effect).Projection = projection;
						((BasicEffect)effect).EnableDefaultLighting();
					}
					else
					{
						SetEffectParameter(effect, "World", localWorld);
						SetEffectParameter(effect, "View", view);
						SetEffectParameter(effect, "Projection", projection);
						SetEffectParameter(effect, "CameraPosition", CameraPosition);

						Material.SetEffectParameters(effect);
					}
				}

				mesh.Draw();
			}
		}

		/// <summary>
		/// Sets the model effect.
		/// </summary>
		/// <param name="effect">The effect.</param>
		/// <param name="CopyEffect">if set to <c>true</c> [copy effect].</param>
		public void SetModelEffect(Effect effect, bool CopyEffect)
		{
			foreach (ModelMesh mesh in Model.Meshes)
				foreach (ModelMeshPart part in mesh.MeshParts)
				{
					Effect toSet = effect;

					//Copy the effect if necessary
					if (CopyEffect)
						toSet = effect.Clone();

					MeshTag tag = ((MeshTag)part.Tag);

					//If this ModelMeshPart has a texture, set it to the effect
					if (tag.Texture != null)
					{
						SetEffectParameter(toSet, "BasicTexture", tag.Texture);
						SetEffectParameter(toSet, "TextureEnabled", true);
					}
					else
						SetEffectParameter(toSet, "TexeturedEnabled", false);

					//Set our remaining parameters to the effect
					SetEffectParameter(toSet, "DiffuseColor", tag.Color);
					SetEffectParameter(toSet, "SpecularPower", tag.SpecularPower);

					part.Effect = toSet;
				}
		}


		/// <summary>
		/// Sets the effect parameter.
		/// </summary>
		/// <param name="effect">The effect.</param>
		/// <param name="paramName">Name of the parameter.</param>
		/// <param name="val">The value.</param>
		private void SetEffectParameter(Effect effect, string paramName, object val)
		{
			if (effect.Parameters[paramName] == null)
				return;

			if (val is Vector3)
				effect.Parameters[paramName].SetValue((Vector3)val);
			else if (val is bool)
				effect.Parameters[paramName].SetValue((bool)val);
			else if (val is Matrix)
				effect.Parameters[paramName].SetValue((Matrix)val);
			else if (val is Texture2D)
				effect.Parameters[paramName].SetValue((Texture2D)val);
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
					_modelTransforms[mesh.ParentBone.Index]);
				boundingSphere = BoundingSphere.CreateMerged(boundingSphere, transformed);
			}

			_boundingSphere = boundingSphere;
		}

		/// <summary>
		/// Gets the bounding sphere object.
		/// </summary>
		public BoundingSphere BoundingSphere
		{
			get
			{
				Matrix worldTransforms = Matrix.CreateScale(Scale) * Matrix.CreateTranslation(Position);
				BoundingSphere transformed = _boundingSphere;
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

		///// <summary>
		///// Gets or sets the rotation of the model.
		///// </summary>
		//public Vector3 Rotation
		//{
		//	get;
		//	set;
		//}

		public Quaternion Rotation
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
