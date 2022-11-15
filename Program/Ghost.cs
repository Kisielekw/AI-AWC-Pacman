using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pacman
{
    internal abstract class Ghost
    {
        public string Name { get; protected set; }
        public Vector2 Position { get; protected set; }

        protected Color colour;

        public Ghost(Vector2 pPosition)
        {
            Position = pPosition;
        }

        public void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawCircle(new Vector2(0, 12.5f) + Position, 25, 16, colour);
            pShapeBatcher.DrawRectangle(Position, true, 25, 50, colour);
            pShapeBatcher.DrawCircle(new Vector2(-20, -9.5f) + Position, 5, 3, Color.CornflowerBlue);
            pShapeBatcher.DrawCircle(new Vector2(-10, -9.5f) + Position, 5, 3, Color.CornflowerBlue);
            pShapeBatcher.DrawCircle(new Vector2(0, -9.5f) + Position, 5, 3, Color.CornflowerBlue);
            pShapeBatcher.DrawCircle(new Vector2(10, -9.5f) + Position, 5, 3, Color.CornflowerBlue);
            pShapeBatcher.DrawCircle(new Vector2(20, -9.5f) + Position, 5, 3, Color.CornflowerBlue);
            pShapeBatcher.DrawCircle(new Vector2(-10, 17.5f) + Position, 7, 10, Color.White);
            pShapeBatcher.DrawCircle(new Vector2(10, 17.5f) + Position, 7, 10, Color.White);
            pShapeBatcher.DrawCircle(new Vector2(-5, 17.5f) + Position, 3.5f, 10, Color.DarkBlue);
            pShapeBatcher.DrawCircle(new Vector2(15, 17.5f) + Position, 3.5f, 10, Color.DarkBlue);
        }
    }

    internal class Blinky : Ghost
    {
        public Blinky(Vector2 pPosition) : base(pPosition)
        {
            Name = "Blinky";
            colour = Color.Red;
        }
    }

    internal class Pinky : Ghost
    {
        public Pinky(Vector2 pPosition) : base(pPosition)
        {
            Name = "Pinky";
            colour = Color.Pink;
        }
    }

    internal class Inky : Ghost
    {
        public Inky(Vector2 pPosition) : base(pPosition)
        {
            Name = "Inky";
            colour = Color.LightBlue;
        }
    }

    internal class Clyde : Ghost
    {
        public Clyde(Vector2 pPosition) : base(pPosition)
        {
            Name = "Clyde";
            colour = Color.Orange;
        }
    }
}
