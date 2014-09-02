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
			if (movementDataReporter.traveling)
			{
				Vector3 flatNewCoords = new Vector3(movementDataReporter.newCoordinates.X, model.Position.Y, movementDataReporter.newCoordinates.Z);

				_source = model.Position;
				_destination = movementDataReporter.newCoordinates;
				_point = flatNewCoords;

				Matrix rotationTo = Matrix.Invert(Matrix.CreateLookAt(model.Position, flatNewCoords, Vector3.Up));
				
				Quaternion sample = Quaternion.CreateFromRotationMatrix(rotationTo);

				model.Rotation = Quaternion.Slerp(model.Rotation, sample, .2f);

				//model.Position += Vector3.Transform(Vector3.Forward, model.Rotation) * (float)gameTime.ElapsedGameTime.TotalMilliseconds * .5f;
				model.Position = Vector3.Lerp(model.Position, movementDataReporter.newCoordinates, .01f);

				if (Vector3.Distance(model.Position, movementDataReporter.newCoordinates) < 10)
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
