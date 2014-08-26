using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class MeshTag
	{
		/// <summary>
		/// The color
		/// </summary>
		public Vector3 Color;

		/// <summary>
		/// The texture
		/// </summary>
		public Texture2D Texture;

		/// <summary>
		/// The specular power
		/// </summary>
		public float SpecularPower;

		/// <summary>
		/// The cached effect
		/// </summary>
		public Effect CachedEffect = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="MeshTag"/> class.
		/// </summary>
		/// <param name="Color">The color.</param>
		/// <param name="Texture">The texture.</param>
		/// <param name="SpecularPower">The specular power.</param>
		public MeshTag(Vector3 Color, Texture2D Texture,
			float SpecularPower)
		{
			this.Color = Color;
			this.Texture = Texture;
			this.SpecularPower = SpecularPower;
		}
	}
}
