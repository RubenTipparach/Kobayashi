using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FleetHackers.Models
{
	public class Skybox
	{
		BasicModel model;
        Effect effect;
        GraphicsDevice graphics;

		public Skybox(ContentManager Content, GraphicsDevice GraphicsDevice, 
            TextureCube Texture)
        {
            model = new BasicModel(Content.Load<Model>("Models\\skysphere_mesh"), Vector3.Zero,
                Vector3.Zero, Vector3.One, GraphicsDevice);

            effect = Content.Load<Effect>("Shaders\\skysphere_effect");
            effect.Parameters["CubeMap"].SetValue(Texture);
            
            model.SetModelEffect(effect, false);

            this.graphics = GraphicsDevice;
        }

        public void Draw(Matrix View, Matrix Projection, Vector3 CameraPosition)
        {
            // Disable the depth buffer
            graphics.DepthStencilState = DepthStencilState.None;

            // Move the model with the sphere
            model.Position = CameraPosition;

            model.Draw(View, Projection, CameraPosition);

            graphics.DepthStencilState = DepthStencilState.Default;
        }
	}
}
