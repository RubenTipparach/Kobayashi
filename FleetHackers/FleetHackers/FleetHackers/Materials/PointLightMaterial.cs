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
	public class PointLightMaterial : Material
	{
		/// <summary>
		/// Gets or sets the color of the ambient light.
		/// </summary>
		/// <value>
		/// The color of the ambient light.
		/// </value>
		public Vector3 AmbientLightColor { get; set; }

		/// <summary>
		/// Gets or sets the light position.
		/// </summary>
		/// <value>
		/// The light position.
		/// </value>
		public Vector3 LightPosition { get; set; }

		/// <summary>
		/// Gets or sets the color of the light.
		/// </summary>
		/// <value>
		/// The color of the light.
		/// </value>
		public Vector3 LightColor { get; set; }

		/// <summary>
		/// Gets or sets the light attenuation.
		/// </summary>
		/// <value>
		/// The light attenuation.
		/// </value>
		public float LightAttenuation { get; set; }

		/// <summary>
		/// Gets or sets the light falloff.
		/// </summary>
		/// <value>
		/// The light falloff.
		/// </value>
		public float LightFalloff { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PointLightMaterial"/> class.
		/// </summary>
		public PointLightMaterial()
		{
			AmbientLightColor = new Vector3(.15f, .15f, .15f);
			LightPosition = new Vector3(0, 0, 0);
			LightColor = new Vector3(.85f, .85f, .85f);
			LightAttenuation = 5000;
			LightFalloff = 2;
		}

		/// <summary>
		/// Sets the effect parameters.
		/// </summary>
		/// <param name="effect">The effect.</param>
		public override void SetEffectParameters(Effect effect)
		{
			if (effect.Parameters["AmbientLightColor"] != null)
				effect.Parameters["AmbientLightColor"].SetValue(
					AmbientLightColor);

			if (effect.Parameters["LightColor"] != null)
				effect.Parameters["LightColor"].SetValue(LightColor);

			if (effect.Parameters["LightPosition"] != null)
				effect.Parameters["LightPosition"].SetValue(LightPosition);

			// if (effect.Parameters["LightAttenuation"] != null)
			//     effect.Parameters["LightAttenuation"].SetValue(
			//         LightAttenuation);

			if (effect.Parameters["LightFalloff"] != null)
				effect.Parameters["LightFalloff"].SetValue(LightFalloff);
		}
	}
}
