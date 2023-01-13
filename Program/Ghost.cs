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
        protected float houseStartTime;
        private float speed;

        public Ghost(Vector2 pPosition, Color pBackgroundColour)
        {
            Position = pPosition;
            backgroundColour = pBackgroundColour;
            state = States.House;
            houseStartTime = 0;
        }

        public void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawCircle(Position, 25, 16, colour);
            pShapeBatcher.DrawRectangle(new Vector2(0, -12.5f) + Position, true, 25, 50, colour);
            pShapeBatcher.DrawCircle(new Vector2(-20, -23f) + Position, 5, 3, backgroundColour);
            pShapeBatcher.DrawCircle(new Vector2(-10, -23f) + Position, 5, 3, backgroundColour);
            pShapeBatcher.DrawCircle(new Vector2(0, -23f) + Position, 5, 3, backgroundColour);
            pShapeBatcher.DrawCircle(new Vector2(10, -23f) + Position, 5, 3, backgroundColour);
            pShapeBatcher.DrawCircle(new Vector2(20, -23f) + Position, 5, 3, backgroundColour);
            pShapeBatcher.DrawCircle(new Vector2(-10, 5) + Position, 7, 10, Color.White);
            pShapeBatcher.DrawCircle(new Vector2(10, 5) + Position, 7, 10, Color.White);
            pShapeBatcher.DrawCircle(new Vector2(-5, 5) + Position, 3.5f, 10, Color.DarkBlue);
            pShapeBatcher.DrawCircle(new Vector2(15, 5) + Position, 3.5f, 10, Color.DarkBlue);
        }

        public void Update(Pacman pPacman, Graph pGraph, float pSeconds)
        {
            switch (state)
            {
                case States.House:
                    House(pSeconds, pGraph);
                    break;
                case States.Corner:
                    Corner(pSeconds, pGraph);
                    break;
                case States.Chase:
                    Chase(pSeconds, pPacman, pGraph);
                    break;
                case States.Escape:
                    Escape(pSeconds, pPacman, pGraph);
                    break;
            }
        }

        abstract protected void House(float pSeconds, Graph pGraph);
        abstract protected void Corner(float pSeconds, Graph pGraph);
        abstract protected void Chase(float pSeconds, Pacman pPacman, Graph pGraph);
        abstract protected void Escape(float pSeconds, Pacman pPacman, Graph pGraph);
    }

    internal class Blinky : Ghost
    {
        public Blinky(Vector2 pPosition, Color pBackgroundColour) : base(pPosition, pBackgroundColour)
        {
            Name = "Blinky";
            colour = Color.Red;
        }

        protected override void Chase(float pSeconds, Pacman pPacman, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void Corner(float pSeconds, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void Escape(float pSeconds, Pacman pPacman, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void House(float pSeconds, Graph pGraph)
        {
            //state = States.Corner;
            int currentNodeID = pGraph.GetClosesNodeID(Position);
            AStar pathFinding = new AStar(pGraph, currentNodeID, 0);

            List<Edge> path = pathFinding.ShortestPath;

            if (pGraph.GetNode(path[0].ToID).Position == Position)
            {
                path.RemoveAt(0);
            }
            else
            {
                Vector2 direction = (pGraph.GetNode(path[0].ToID).Position - Position);
                direction.Normalize();

                Position += direction * 0.1f;
            }
        }
    }

    internal class Pinky : Ghost
    {
        public Pinky(Vector2 pPosition, Color pBackgroundColour) : base(pPosition, pBackgroundColour)
        {
            Name = "Pinky";
            colour = Color.Pink;
        }

        protected override void Chase(float pSeconds, Pacman pPacman, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void Corner(float pSeconds, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void Escape(float pSeconds, Pacman pPacman, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void House(float pSeconds, Graph pGraph)
        {
            if(houseStartTime + 2 >= pSeconds) state = States.Corner;
        }
    }

    internal class Inky : Ghost
    {
        public Inky(Vector2 pPosition, Color pBackgroundColour) : base(pPosition, pBackgroundColour)
        {
            Name = "Inky";
            colour = Color.LightBlue;
        }

        protected override void Chase(float pSeconds, Pacman pPacman, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void Corner(float pSeconds, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void Escape(float pSeconds, Pacman pPacman, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void House(float pSeconds, Graph pGraph)
        {
            if(houseStartTime + 5 >= pSeconds) state = States.Corner;
        }
    }

    internal class Clyde : Ghost
    {
        public Clyde(Vector2 pPosition, Color pBackgroundColour) : base(pPosition, pBackgroundColour)
        {
            Name = "Clyde";
            colour = Color.Orange;
        }

        protected override void Chase(float pSeconds, Pacman pPacman, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void Corner(float pSeconds, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void Escape(float pSeconds, Pacman pPacman, Graph pGraph)
        {
            throw new NotImplementedException();
        }

        protected override void House(float pSeconds, Graph pGraph)
        {
            if(houseStartTime + 8 >= pSeconds) state = States.Corner;
        }
    }
}
