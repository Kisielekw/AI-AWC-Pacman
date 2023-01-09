using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Pacman
{
    internal abstract class Ghost
    {
        protected enum States { House, Corner, Chase, Escape }

        public string Name { get; protected set; }
        public Vector2 Position { get; protected set; }

        protected Color colour;
        protected Color backgroundColour;
        protected States state;

        public Ghost(Vector2 pPosition, Color pBackgroundColour)
        {
            Position = pPosition;
            backgroundColour = pBackgroundColour;
            state = States.House;
        }

        public void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawCircle(new Vector2(0, 12.5f) + Position, 25, 16, colour);
            pShapeBatcher.DrawRectangle(Position, true, 25, 50, colour);
            pShapeBatcher.DrawCircle(new Vector2(-20, -9.5f) + Position, 5, 3, backgroundColour);
            pShapeBatcher.DrawCircle(new Vector2(-10, -9.5f) + Position, 5, 3, backgroundColour);
            pShapeBatcher.DrawCircle(new Vector2(0, -9.5f) + Position, 5, 3, backgroundColour);
            pShapeBatcher.DrawCircle(new Vector2(10, -9.5f) + Position, 5, 3, backgroundColour);
            pShapeBatcher.DrawCircle(new Vector2(20, -9.5f) + Position, 5, 3, backgroundColour);
            pShapeBatcher.DrawCircle(new Vector2(-10, 17.5f) + Position, 7, 10, Color.White);
            pShapeBatcher.DrawCircle(new Vector2(10, 17.5f) + Position, 7, 10, Color.White);
            pShapeBatcher.DrawCircle(new Vector2(-5, 17.5f) + Position, 3.5f, 10, Color.DarkBlue);
            pShapeBatcher.DrawCircle(new Vector2(15, 17.5f) + Position, 3.5f, 10, Color.DarkBlue);
        }

        public void Update(Pacman pPacman, float pSeconds)
        {
            switch (state)
            {
                case States.House:
                    House(pSeconds);
                    break;
                case States.Corner:
                    Corner(pSeconds);
                    break;
                case States.Chase:
                    Chase(pSeconds, pPacman);
                    break;
                case States.Escape:
                    Escape(pSeconds, pPacman);
                    break;
            }
    }

        abstract protected void House(float pSeconds);
        abstract protected void Corner(float pSeconds);
        abstract protected void Chase(float pSeconds, Pacman pPacman);
        abstract protected void Escape(float pSeconds, Pacman pPacman);
    }

    internal class Blinky : Ghost
    {
        public Blinky(Vector2 pPosition, Color pBackgroundColour) : base(pPosition, pBackgroundColour)
        {
            Name = "Blinky";
            colour = Color.Red;
        }

        protected override void Chase(float pSeconds, Pacman pPacman)
        {
            throw new NotImplementedException();
        }

        protected override void Corner(float pSeconds)
        {
            throw new NotImplementedException();
        }

        protected override void Escape(float pSeconds, Pacman pPacman)
        {
            throw new NotImplementedException();
        }

        protected override void House(float pSeconds)
        {
            throw new NotImplementedException();
        }
    }

    internal class Pinky : Ghost
    {
        public Pinky(Vector2 pPosition, Color pBackgroundColour) : base(pPosition, pBackgroundColour)
        {
            Name = "Pinky";
            colour = Color.Pink;
        }

        protected override void Chase(float pSeconds, Pacman pPacman)
        {
            throw new NotImplementedException();
        }

        protected override void Corner(float pSeconds)
        {
            throw new NotImplementedException();
        }

        protected override void Escape(float pSeconds, Pacman pPacman)
        {
            throw new NotImplementedException();
    }

        protected override void House(float pSeconds)
        {
            throw new NotImplementedException();
        }
    }

    internal class Inky : Ghost
    {
        public Inky(Vector2 pPosition, Color pBackgroundColour) : base(pPosition, pBackgroundColour)
        {
            Name = "Inky";
            colour = Color.LightBlue;
        }

        protected override void Chase(float pSeconds, Pacman pPacman)
        {
            throw new NotImplementedException();
        }

        protected override void Corner(float pSeconds)
        {
            throw new NotImplementedException();
        }

        protected override void Escape(float pSeconds, Pacman pPacman)
        {
            throw new NotImplementedException();
        }

        protected override void House(float pSeconds)
        {
            throw new NotImplementedException();
        }
    }

    internal class Clyde : Ghost
    {
        public Clyde(Vector2 pPosition, Color pBackgroundColour) : base(pPosition, pBackgroundColour)
        {
            Name = "Clyde";
            colour = Color.Orange;
        }

        protected override void Chase(float pSeconds, Pacman pPacman)
        {
            throw new NotImplementedException();
        }

        protected override void Corner(float pSeconds)
        {
            throw new NotImplementedException();
        }

        protected override void Escape(float pSeconds, Pacman pPacman)
        {
            throw new NotImplementedException();
        }

        protected override void House(float pSeconds)
        {
            throw new NotImplementedException();
        }
    }
}
