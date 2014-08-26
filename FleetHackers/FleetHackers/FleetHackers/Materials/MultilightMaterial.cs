using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.Materials
{
	/// <summary>
	/// 
	/// </summary>
	public class MultiLightingMaterial : Material
	{
		/// <summary>
		/// Gets or sets the color of the ambient.
		/// </summary>
		/// <value>
		/// The color of the ambient.
		/// </value>
		public Vector3 AmbientColor { get; set; }

		/// <summary>
		/// Gets or sets the light direction.
		/// </summary>
		/// <value>
		/// The light direction.
		/// </value>
		public Vector3[] LightDirection { get; set; }

		/// <summary>
		/// Gets or sets the color of the light.
		/// </summary>
		/// <value>
		/// The color of the light.
		/// </value>
		public Vector3[] LightColor { get; set; }

		/// <summary>
		/// Gets or sets the color of the specular.
		/// </summary>
		/// <value>
		/// The color of the specular.
		/// </value>
		public Vector3 SpecularColor { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MultiLightingMaterial"/> class.
		/// </summary>
		public MultiLightingMaterial()
		{
			AmbientColor = new Vector3(.1f, .1f, .1f);
			LightDirection = new Vector3[3];
			LightColor = new Vector3[] { Vector3.One, Vector3.One, 
                Vector3.One };
			SpecularColor = new Vector3(1, 1, 1);
		}

		/// <summary>
		/// Sets the effect parameters.
		/// </summary>
		/// <param name="effect">The effect.</param>
		public override void SetEffectParameters(Effect effect)
		{
			if (effect.Parameters["AmbientColor"] != null)
				effect.Parameters["AmbientColor"].SetValue(AmbientColor);

			if (effect.Parameters["LightDirection"] != null)
				effect.Parameters["LightDirection"].SetValue(LightDirection);

			if (effect.Parameters["LightColor"] != null)
				effect.Parameters["LightColor"].SetValue(LightColor);

			if (effect.Parameters["SpecularColor"] != null)
				effect.Parameters["SpecularColor"].SetValue(SpecularColor);
		}
	}
}
