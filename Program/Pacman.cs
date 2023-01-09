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
            
        }
    }
}
