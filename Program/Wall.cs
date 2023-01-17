using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    internal class Wall
    {
        public Vector2 Position { get; private set; }

        public Wall(Vector2 pPosition)
        {
            Position = pPosition;
        }

        public void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawRectangle(Position, true, 49, 49, Color.Navy);
        }
    }
}
