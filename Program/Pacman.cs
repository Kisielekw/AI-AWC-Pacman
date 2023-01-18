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
        public bool IsAlive;
        public Vector2 Position { get; private set; }
        public Vector2 ClosestNodePosition
        {
            get
            {
                float x = Position.X + 25;
                float y = Position.Y + 25;

                x = x % 50;
                y = y % 50;

                if(x >= 25)
                {
                    x = Position.X + (50 - x);
                }
                else
                {
                    x = Position.X - x;
                }

                if (y >= 25)
                {
                    y = Position.Y + (50 - y);
                }
                else
                {
                    y = Position.Y - y;
                }

                return new Vector2(x, y);
            }
        }
        public Vector2 direction { get; private set; }

        private bool powerUp;
        private float powerUpStart;
        private KeyboardState oldState;

        public Pacman(Vector2 pPosition)
        {
            Position = pPosition;
            direction = Vector2.Zero;
            IsAlive = true;
            powerUp = false;
        }

        public void Draw(ShapeBatcher pShapeBatcher)
        {
            pShapeBatcher.DrawCircle(Position, 25, 16, Color.Gold);
        }

        public void Update(List<Wall> pWalls, Ghost[] pGhosts, List<PowerUp> pPowerUps, float pSeconds)
        {
            KeyboardState state = Keyboard.GetState();

            if (Position == new Vector2(975, 475)) Position = new Vector2(-25, 475);
            else if (Position == new Vector2(-25, 475)) Position = new Vector2(975, 475);

            if (state.IsKeyDown(Keys.W) && !oldState.IsKeyDown(Keys.W))
            {
                direction = new Vector2(0, 1);
                Position = new Vector2(ClosestNodePosition.X, Position.Y);
            }
            else if (state.IsKeyDown(Keys.S) && !oldState.IsKeyDown(Keys.S))
            {
                direction = new Vector2(0, -1);
                Position = new Vector2(ClosestNodePosition.X, Position.Y);
            }
            else if (state.IsKeyDown(Keys.A) && !oldState.IsKeyDown(Keys.A))
            {
                direction = new Vector2(-1, 0);
                Position = new Vector2(Position.X, ClosestNodePosition.Y);
            }
            else if (state.IsKeyDown(Keys.D) && !oldState.IsKeyDown(Keys.D))
            {
                direction = new Vector2(1, 0);
                Position = new Vector2(Position.X, ClosestNodePosition.Y);
            }

            if(pWalls.Where(x => x.Position == Position + (direction * 50)).Count() == 0)
            {
                Position += direction * 2.5f;
            }

            CheckHit(pGhosts, pSeconds);
            CheckPowerUps(pPowerUps, pGhosts, pSeconds);
            if (powerUpStart + 5 <= pSeconds) powerUp = false;

            oldState = Keyboard.GetState();
        }

        private void CheckHit(Ghost[] ghosts, float pSeconds)
        {
            foreach(Ghost ghost in ghosts)
            {
                if ((ghost.Position - Position).Length() < 49)
                {
                    if (powerUp) ghost.Eaten(pSeconds);
                    else IsAlive = false;
                    return;
                }
            }
        }

        private void CheckPowerUps(List<PowerUp> pPowerUps, Ghost[] ghosts, float pSeconds)
        {
            for(int i = 0; i < pPowerUps.Count; i++)
            {
                if (pPowerUps[i].Position == ClosestNodePosition)
                {
                    pPowerUps.RemoveAt(i);
                    powerUpStart = pSeconds;
                    powerUp = true;
                    foreach(Ghost ghost in ghosts)
                    {
                        ghost.SetFrightend(pSeconds);
                    }
                    return;
                }
            }
        }
    }
}
