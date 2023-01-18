using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    internal class PowerUp
    {
        public Vector2 Position;
        
        public PowerUp(Vector2 position)
        {
            Position = position;
        }

        public void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawCircle(Position, 12.5f, 10, Color.PeachPuff);
        }
    }
}
