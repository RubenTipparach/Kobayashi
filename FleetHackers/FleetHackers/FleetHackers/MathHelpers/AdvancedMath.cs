using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FleetHackers.MathHelpers
{
	public class AdvancedMath
	{
		public static Vector3 ToEulerAngles(Quaternion q)
		{
			// Store the Euler angles in radians
			Vector3 pitchYawRoll = new Vector3();

			double sqw = q.W * q.W;
			double sqx = q.X * q.X;
			double sqy = q.Y * q.Y;
			double sqz = q.Z * q.Z;

			// If quaternion is normalised the unit is one, otherwise it is the correction factor
			double unit = sqx + sqy + sqz + sqw;
			double test = q.X * q.Y + q.Z * q.W;

			if (test > 0.499f * unit)
			{
				// Singularity at north pole
				pitchYawRoll.Y = 2f * (float)Math.Atan2(q.X, q.W);  // Yaw
				pitchYawRoll.X = (float)Math.PI * 0.5f;             // Pitch
				pitchYawRoll.Z = 0f;                                // Roll
				return pitchYawRoll;
			}
			else if (test < -0.499f * unit)
			{
				// Singularity at south pole
				pitchYawRoll.Y = -2f * (float)Math.Atan2(q.X, q.W); // Yaw
				pitchYawRoll.X = -(float)Math.PI * 0.5f;            // Pitch
				pitchYawRoll.Z = 0f;                                // Roll
				return pitchYawRoll;
			}

			pitchYawRoll.Y = (float)Math.Atan2(2 * q.Y * q.W - 2 * q.X * q.Z, sqx - sqy - sqz + sqw);       // Yaw
			pitchYawRoll.X = (float)Math.Asin(2 * test / unit);                                             // Pitch
			pitchYawRoll.Z = (float)Math.Atan2(2 * q.X * q.W - 2 * q.Y * q.Z, -sqx + sqy - sqz + sqw);      // Roll

			return pitchYawRoll;
		}

		public static Matrix GetRotationMatrix(Vector3 source, Vector3 target)
		{
			float dot = Vector3.Dot(source, target);
			if (!float.IsNaN(dot))
			{
				float angle = (float)Math.Acos(dot);
				if (!float.IsNaN(angle))
				{
					Vector3 cross = Vector3.Cross(source, target);
					cross.Normalize();
					Matrix rotation = Matrix.CreateFromAxisAngle(cross, angle);
					return rotation;
				}
			}
			return Matrix.Identity;
		}

	}
}
