using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;

namespace Pacman
{
    internal class Pacman
    {
        public Vector2 Position { get; private set; }

        private Vector2 direction;
        private float speed;

        private KeyboardState oldState;

        public Pacman(Vector2 pPosition)
        {
            Position = pPosition;
            direction = Vector2.Zero;
        }

        public void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawCircle(Position, 25, 16, Color.Gold);
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.W) && !oldState.IsKeyDown(Keys.W))
            {
                direction = new Vector2(0, 1);
            }
            if (state.IsKeyDown(Keys.S) && !oldState.IsKeyDown(Keys.S))
            {
                direction = new Vector2(0, -1);
            }
            if (state.IsKeyDown(Keys.D) && !oldState.IsKeyDown(Keys.D))
            {
                direction = new Vector2(1, 0);
            }
            if (state.IsKeyDown(Keys.A) && !oldState.IsKeyDown(Keys.A))
            {
                direction = new Vector2(-1, 0);
            }

            Position += direction * speed;

            oldState = state;
        }
    }
}
