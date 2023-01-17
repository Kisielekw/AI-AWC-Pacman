using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Pacman
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ShapeBatcher _shapeBatcher;

        private List<Wall> walls;

        private Pacman Pacman;

        private Blinky Blinky;
        private Pinky Pinky;
        private Inky Inky;
        private Clyde Clyde;

        private Color backgroundColour;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            backgroundColour = Color.Black;
            _graphics.PreferredBackBufferHeight = 950;
            _graphics.PreferredBackBufferWidth = 950;
            _graphics.ApplyChanges();

            walls = new List<Wall>();
            InitializeWalls();

            Pacman = new Pacman(new Vector2(75, 75));
            
            Blinky = new Blinky(new Vector2(475, 525), backgroundColour, walls);
            Pinky = new Pinky(new Vector2(475, 475), backgroundColour, walls);
            Inky = new Inky(new Vector2(525, 475), backgroundColour, Blinky, walls);
            Clyde = new Clyde(new Vector2(425, 475), backgroundColour,Pacman, walls);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _shapeBatcher = new ShapeBatcher(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            float seccondsElapst = (float)gameTime.TotalGameTime.TotalSeconds;
            if (Pacman.IsAlive)
            {
                //Blinky.Update(Pacman, seccondsElapst);
                //Pinky.Update(Pacman, seccondsElapst);
                //Inky.Update(Pacman, seccondsElapst);
                Clyde.Update(Pacman, seccondsElapst);
                Pacman.Update(walls, new Ghost[] {Blinky, Pinky, Inky, Clyde});
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColour);

            // TODO: Add your drawing code here
            _shapeBatcher.Begin();
            Blinky.Draw(_shapeBatcher);
            Pinky.Draw(_shapeBatcher);
            Inky.Draw(_shapeBatcher);
            Clyde.Draw(_shapeBatcher);

            Pacman.Draw(_shapeBatcher);

            foreach(Wall wall in walls)
            {
                wall.Draw(_shapeBatcher);
            }

            _shapeBatcher.End();

            base.Draw(gameTime);
        }

        private void InitializeWalls()
        {
            for(int x = 25; x <= 925; x += 50)
            {
                for(int y = 25; y <= 925; y += 900)
                {
                    walls.Add(new Wall(new Vector2(x, y)));
                }
            }

            for(int y = 75; y <= 875; y += 50)
            {
                for(int x = 25; x <= 925; x += 900)
                {
                    if(y != 475)
                    {
                        walls.Add(new Wall(new Vector2(x, y)));
                    }
                }
            }

            walls.Add(new Wall(new Vector2(75, 225)));
            walls.Add(new Wall(new Vector2(875, 225)));

            walls.Add(new Wall(new Vector2(75, 425)));
            walls.Add(new Wall(new Vector2(125, 425)));
            walls.Add(new Wall(new Vector2(175, 425)));
            walls.Add(new Wall(new Vector2(875, 425)));
            walls.Add(new Wall(new Vector2(825, 425)));
            walls.Add(new Wall(new Vector2(775, 425)));

            walls.Add(new Wall(new Vector2(75, 525)));
            walls.Add(new Wall(new Vector2(125, 525)));
            walls.Add(new Wall(new Vector2(175, 525)));
            walls.Add(new Wall(new Vector2(875, 525)));
            walls.Add(new Wall(new Vector2(825, 525)));
            walls.Add(new Wall(new Vector2(775, 525)));

            walls.Add(new Wall(new Vector2(125, 125)));
            walls.Add(new Wall(new Vector2(175, 125)));
            walls.Add(new Wall(new Vector2(225, 125)));
            walls.Add(new Wall(new Vector2(275, 125)));
            walls.Add(new Wall(new Vector2(325, 125)));
            walls.Add(new Wall(new Vector2(375, 125)));
            walls.Add(new Wall(new Vector2(825, 125)));
            walls.Add(new Wall(new Vector2(775, 125)));
            walls.Add(new Wall(new Vector2(725, 125)));
            walls.Add(new Wall(new Vector2(675, 125)));
            walls.Add(new Wall(new Vector2(625, 125)));
            walls.Add(new Wall(new Vector2(575, 125)));

            walls.Add(new Wall(new Vector2(275, 175)));
            walls.Add(new Wall(new Vector2(275, 225)));
            walls.Add(new Wall(new Vector2(675, 175)));
            walls.Add(new Wall(new Vector2(675, 225)));

            walls.Add(new Wall(new Vector2(175, 225)));
            walls.Add(new Wall(new Vector2(175, 275)));
            walls.Add(new Wall(new Vector2(175, 325)));
            walls.Add(new Wall(new Vector2(125, 325)));
            walls.Add(new Wall(new Vector2(775, 225)));
            walls.Add(new Wall(new Vector2(775, 275)));
            walls.Add(new Wall(new Vector2(775, 325)));
            walls.Add(new Wall(new Vector2(825, 325)));

            walls.Add(new Wall(new Vector2(275, 325)));
            walls.Add(new Wall(new Vector2(325, 325)));
            walls.Add(new Wall(new Vector2(375, 325)));
            walls.Add(new Wall(new Vector2(675, 325)));
            walls.Add(new Wall(new Vector2(625, 325)));
            walls.Add(new Wall(new Vector2(575, 325)));

            walls.Add(new Wall(new Vector2(475, 125)));
            walls.Add(new Wall(new Vector2(475, 175)));
            walls.Add(new Wall(new Vector2(475, 225)));
            walls.Add(new Wall(new Vector2(425, 225)));
            walls.Add(new Wall(new Vector2(375, 225)));
            walls.Add(new Wall(new Vector2(525, 225)));
            walls.Add(new Wall(new Vector2(575, 225)));

            walls.Add(new Wall(new Vector2(475, 325)));
            walls.Add(new Wall(new Vector2(475, 375)));
            walls.Add(new Wall(new Vector2(475, 425)));
            walls.Add(new Wall(new Vector2(425, 425)));
            walls.Add(new Wall(new Vector2(375, 425)));
            walls.Add(new Wall(new Vector2(525, 425)));
            walls.Add(new Wall(new Vector2(575, 425)));
            walls.Add(new Wall(new Vector2(375, 475)));
            walls.Add(new Wall(new Vector2(375, 525)));
            walls.Add(new Wall(new Vector2(575, 475)));
            walls.Add(new Wall(new Vector2(575, 525)));
            walls.Add(new Wall(new Vector2(525, 525)));
            walls.Add(new Wall(new Vector2(425, 525)));

            walls.Add(new Wall(new Vector2(275, 425)));
            walls.Add(new Wall(new Vector2(275, 475)));
            walls.Add(new Wall(new Vector2(275, 525)));
            walls.Add(new Wall(new Vector2(675, 425)));
            walls.Add(new Wall(new Vector2(675, 475)));
            walls.Add(new Wall(new Vector2(675, 525)));

            walls.Add(new Wall(new Vector2(475, 875)));
            walls.Add(new Wall(new Vector2(475, 825)));
            walls.Add(new Wall(new Vector2(475, 775)));

            walls.Add(new Wall(new Vector2(125, 825)));
            walls.Add(new Wall(new Vector2(175, 825)));
            walls.Add(new Wall(new Vector2(825, 825)));
            walls.Add(new Wall(new Vector2(775, 825)));

            walls.Add(new Wall(new Vector2(275, 825)));
            walls.Add(new Wall(new Vector2(325, 825)));
            walls.Add(new Wall(new Vector2(375, 825)));
            walls.Add(new Wall(new Vector2(675, 825)));
            walls.Add(new Wall(new Vector2(625, 825)));
            walls.Add(new Wall(new Vector2(575, 825)));

            walls.Add(new Wall(new Vector2(125, 725)));
            walls.Add(new Wall(new Vector2(175, 725)));
            walls.Add(new Wall(new Vector2(125, 675)));
            walls.Add(new Wall(new Vector2(125, 625)));

            walls.Add(new Wall(new Vector2(825, 725)));
            walls.Add(new Wall(new Vector2(775, 725)));
            walls.Add(new Wall(new Vector2(825, 675)));
            walls.Add(new Wall(new Vector2(825, 625)));

            walls.Add(new Wall(new Vector2(225, 625)));
            walls.Add(new Wall(new Vector2(275, 625)));
            walls.Add(new Wall(new Vector2(325, 625)));
            walls.Add(new Wall(new Vector2(275, 675)));
            walls.Add(new Wall(new Vector2(275, 725)));

            walls.Add(new Wall(new Vector2(375, 725)));

            walls.Add(new Wall(new Vector2(425, 625)));
            walls.Add(new Wall(new Vector2(475, 625)));
            walls.Add(new Wall(new Vector2(525, 625)));
            walls.Add(new Wall(new Vector2(475, 675)));

            walls.Add(new Wall(new Vector2(575, 725)));

            walls.Add(new Wall(new Vector2(625, 625)));
            walls.Add(new Wall(new Vector2(675, 625)));
            walls.Add(new Wall(new Vector2(725, 625)));
            walls.Add(new Wall(new Vector2(675, 675)));
            walls.Add(new Wall(new Vector2(675, 725)));
        }

            
        
    }
}