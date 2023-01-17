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
        protected enum States { House, ToCorner, Corner, Chase, Escape, Reset }

        public string Name { get; protected set; }
        public Vector2 Position { get; protected set; }

        protected AStar pathFinding;
        protected List<Wall> walls;
        protected Color colour;
        protected Color backgroundColour;
        protected States state;
        protected float speed;
        protected float startTime;
        protected int pathCount;

        public Ghost(Vector2 pPosition, Color pBackgroundColour, List<Wall> pWalls)
        {
            Position = pPosition;
            pathFinding = new AStar();
            backgroundColour = pBackgroundColour;
            state = States.House;
            startTime = 0;
            speed = 2.5f;
            walls = pWalls;
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

        public void Update(Pacman pPacman, float pSeconds)
        {
            switch (state)
            {
                case States.House:
                    House(pSeconds);
                    break;
                case States.ToCorner:
                    ToCorner(pSeconds);
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
                case States.Reset:
                    Reset(pSeconds);
                    break;
            }
        }

        abstract protected void House(float pSeconds);
        private void ToCorner(float pSeconds)
        {
            if (pathFinding.Path[pathCount] == Position)
            {
                pathCount++;
                if (pathCount == pathFinding.Path.Length)
                {
                    state = States.Corner;
                    pathCount = 0;
                    startTime = pSeconds;
                    return;
                }
            }

            Vector2 direction = pathFinding.Path[pathCount] - Position;
            direction.Normalize();
            Position += direction * speed;
        }
        abstract protected void Corner(float pSeconds);
        abstract protected void Chase(float pSeconds, Pacman pPacman);
        abstract protected void Escape(float pSeconds, Pacman pPacman);
        abstract protected void Reset(float pSeconds);
    }

    internal class Blinky : Ghost
    {
        public Blinky(Vector2 pPosition, Color pBackgroundColour, List<Wall> pWalls) : base(pPosition, pBackgroundColour, pWalls)
        {
            Name = "Blinky";
            colour = Color.Red;
        }

        protected override void Reset(float pSeconds)
        {
            if (pathFinding.Path[pathCount] == Position)
            {
                pathCount++;
                if(pathCount == pathFinding.Path.Length)
                {
                    state = States.House;
                    pathCount = 0;
                    startTime = pSeconds;
                    return;
                }
            }

            Vector2 direction = pathFinding.Path[pathCount] - Position;
            direction.Normalize();

            Position += direction * (speed * 2);
        }

        protected override void Chase(float pSeconds, Pacman pPacman)
        {
            float x = Position.X + 25;
            float y = Position.Y + 25;

            if(x % 50 == 0 && y % 50 == 0)
            {
                if(startTime + 20 <= pSeconds)
                {
                    state = States.ToCorner;
                    startTime = pSeconds;
                    pathFinding.CreatePath(Position, new Vector2(875, 875), walls);
                    return;
                }
                else pathFinding.CreatePath(Position, pPacman.ClosestNodePosition, walls);
            }

            Vector2 direction = pathFinding.Path[1] - Position;
            direction.Normalize();

            Position += direction * speed;
        }

        protected override void Corner(float pSeconds)
        {
            Vector2[] corrnerPath = new Vector2[4]
            {
                new Vector2(725, 875),
                new Vector2(725, 775),
                new Vector2(875, 775),
                new Vector2(875, 875)
            };

            if(startTime + 4 <= pSeconds && (Position.X + 25) % 50 == 0 && (Position.Y + 25) % 50 == 0) 
            {
                state = States.Chase;
                startTime = pSeconds;
                return;
            }

            if (corrnerPath[pathCount] == Position)
            {
                pathCount++;
                if (pathCount == 4) pathCount = 0;
            }

            Vector2 direction = corrnerPath[pathCount] - Position;
            direction.Normalize();

            Position += direction * speed;
        }

        protected override void Escape(float pSeconds, Pacman pPacman)
        {
            if((pPacman.Position - Position).Length() >= 100 && (Position.X + 25) % 50 == 0  && (Position.Y + 25) % 50 == 0)
            {
                pathFinding.CreatePath(Position, new Vector2(875, 875), walls);
            }
            else if((Position.X + 25) % 50 == 0 && (Position.Y + 25) % 50 == 0)
            {
                Vector2 targetVector = Position - pPacman.Position;
                targetVector.Normalize();
                targetVector = (targetVector * 2) + Position;
            }
        }

        protected override void House(float pSeconds)
        {
            state = States.ToCorner;
            pathFinding.CreatePath(Position,new Vector2(875, 875), walls);
            pathCount = 0;
        }
    }

    internal class Pinky : Ghost
    {
        public Pinky(Vector2 pPosition, Color pBackgroundColour, List<Wall> pWalls) : base(pPosition, pBackgroundColour, pWalls)
        {
            Name = "Pinky";
            colour = Color.Pink;
        }

        protected override void Chase(float pSeconds, Pacman pPacman)
        {
            float x = Position.X + 25;
            float y = Position.Y + 25;

            Vector2 target = pPacman.ClosestNodePosition + (pPacman.direction * 100);

            if (x % 50 == 0 && y % 50 == 0)
            {
                if (startTime + 20 <= pSeconds)
                {
                    state = States.ToCorner;
                    startTime = pSeconds;
                    pathFinding.CreatePath(Position, new Vector2(75, 875), walls);
                    return;
                }
                else
                {
                    if(AStar)
                    pathFinding.CreatePath(Position, pPacman.ClosestNodePosition, walls);
                }
            }

            Vector2 direction = pathFinding.Path[1] - Position;
            direction.Normalize();

            Position += direction * speed;
        }

        protected override void Corner(float pSeconds)
        {
            Vector2[] corrnerPath = new Vector2[4]
            {
                new Vector2(225, 875),
                new Vector2(225, 775),
                new Vector2(75, 775),
                new Vector2(75, 875)
            };

            if (startTime + 4 <= pSeconds && (Position.X + 25) % 50 == 0 && (Position.Y + 25) % 50 == 0)
            {
                state = States.Chase;
                startTime = pSeconds;
                return;
            }

            if (corrnerPath[pathCount] == Position)
            {
                pathCount++;
                if (pathCount == 4) pathCount = 0;
            }

            Vector2 direction = corrnerPath[pathCount] - Position;
            direction.Normalize();

            Position += direction * speed;
        }

        protected override void Escape(float pSeconds, Pacman pPacman)
        {
            throw new NotImplementedException();
        }

        protected override void House(float pSeconds)
        {
            if (startTime + 2 <= pSeconds) 
            { 
                state = States.ToCorner; 
                pathFinding.CreatePath(Position, new Vector2(75, 875), walls);
                pathCount = 0;
            }
        }

        protected override void Reset(float pSeconds)
        {
            throw new NotImplementedException();
        }
    }

    internal class Inky : Ghost
    {
        public Inky(Vector2 pPosition, Color pBackgroundColour, List<Wall> pWalls) : base(pPosition, pBackgroundColour, pWalls)
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
            if(startTime + 5 >= pSeconds) state = States.ToCorner;
        }

        protected override void Reset(float pSeconds)
        {
            throw new NotImplementedException();
        }
    }

    internal class Clyde : Ghost
    {
        public Clyde(Vector2 pPosition, Color pBackgroundColour, List<Wall> pWalls) : base(pPosition, pBackgroundColour, pWalls)
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
            if(startTime + 8 >= pSeconds) state = States.ToCorner;
        }

        protected override void Reset(float pSeconds)
        {
            throw new NotImplementedException();
        }
    }
}
