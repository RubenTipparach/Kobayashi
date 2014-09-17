using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FleetHackers.DrawingHelpers;
using FleetHackers.EngineStructs;
using FleetHackers.MathHelpers;
using FleetHackers.Models;
using Microsoft.Xna.Framework;

namespace FleetHackers.UpdateHelpers
{
	public class ShipMovement
	{
		public static Nullable<Vector3> _source;

		public static Nullable<Vector3> _destination;

		public static Nullable<Vector3> _point;

		/// <summary>
		/// Moves the ship.
		/// </summary>
		/// <param name="movementDataReporter">The movement data reporter.</param>
		/// <param name="model">The model.</param>
		/// <returns></returns>
		public static MovementReport MoveShip(MovementReport movementDataReporter, GameTime gameTime, ref BasicModel model)
		{
			// something to control which ships move
			string[] moveShips = new string[movementDataReporter.PlayerShipSelected.Count];

			foreach (KeyValuePair<string, bool> ps in movementDataReporter.PlayerShipSelected)
			{
				if (ps.Value == true)
				{
					moveShips[0] = ps.Key;
				}
			}

			// now check if we need to move teh ship.
			if (movementDataReporter.traveling)
			{
				Vector3 flatNewCoords = new Vector3(movementDataReporter.newCoordinates.X, model.Position.Y, movementDataReporter.newCoordinates.Z);

				_source = model.Position;
				_destination = movementDataReporter.newCoordinates;
				_point = flatNewCoords;
				
				// Metod 1
				// Matrix rotationTo = Matrix.Invert(Matrix.CreateLookAt(model.Position, flatNewCoords, Vector3.Up));
				// Quaternion.CreateFromRotationMatrix(rotationTo);

				// Method 2 (more efficient)
				Vector3 target =  Vector3.Normalize(model.Position - flatNewCoords);
				float angle = (float)Math.Atan2(target.X, target.Z);
				Quaternion sample = Quaternion.CreateFromAxisAngle(Vector3.Up, angle);
				
				// Method 3 Cross Product, then ACos(A dot B), then Q.CreateFrom(Angle, Axis);

				// Do the interpolations.
				model.Rotation = Quaternion.Slerp(model.Rotation, sample, .2f);

				model.Position = Vector3.Lerp(model.Position, movementDataReporter.newCoordinates, .01f);

				if (Vector3.Distance(model.Position, movementDataReporter.newCoordinates) < 50)
				{
					movementDataReporter.traveling = false;
				}
			}
			return movementDataReporter;
		}

		public static void DrawHeading()
		{
			if (_source.HasValue && _destination.HasValue)
			{
				DebugDraw.DrawLine(_source.Value, _destination.Value, Color.Green);
			}
		}

		public static void DrawMarker()
		{
			if (_point.HasValue)
			{
				DebugDraw.DrawX(_point.Value, 100, Color.Orange);
			}
		}
		
	}
}
