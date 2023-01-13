using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Pacman
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ShapeBatcher _shapeBatcher;

        private List<Wall> walls;
        private Graph graph;

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
            _graphics.PreferredBackBufferHeight = 950;
            _graphics.PreferredBackBufferWidth = 950;
            _graphics.ApplyChanges();

            backgroundColour = Color.Black;

            walls = new List<Wall>();
            InitializeWalls();

            graph = new Graph();
            InitializeGraph();

            Pacman = new Pacman(new Vector2(100, 200));

            Blinky = new Blinky(new Vector2(475, 575), backgroundColour);
            Pinky = new Pinky(new Vector2(475, 475), backgroundColour);
            Inky = new Inky(new Vector2(425, 475), backgroundColour);
            Clyde = new Clyde(new Vector2(525, 475), backgroundColour);

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
            float elapstTime = gameTime.ElapsedGameTime.Seconds;
            Pacman.Update();

            Blinky.Update(Pacman, graph, elapstTime);
            //Pinky.Update(elapstTime);
            //Inky.Update(elapstTime);
            //Clyde.Update(elapstTime);

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

            //Pacman.Draw(_shapeBatcher);

            foreach(Wall wall in walls)
            {
                wall.Draw(_shapeBatcher);
            }

            foreach(Edge edge in graph.Edges)
            {
                Vector2 start = graph.GetNode(edge.FromID).Position;
                Vector2 end = graph.GetNode(edge.ToID).Position;

                Vector2 size = end - start;

                Vector2 pos = start + (size / 2);

                _shapeBatcher.DrawRectangle(pos, true, MathF.Abs(size.Y + 2), MathF.Abs(size.X + 2), Color.Green);
            }

            foreach(Node node in graph.Nodes)
            {
                _shapeBatcher.DrawCircle(node.Position, 5, 8, Color.White);
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

        private void InitializeGraph()
        {
            int count = 0;

            graph.AddNode(new Vector2(75, 75));     //Bottom left corrner ID:0
            graph.AddNode(new Vector2(125, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(175, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(275, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(375, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(475, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(575, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(625, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(675, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(775, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(825, 75));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 75));    //Bottom right corner ID: 16
            graph.AddEdge(count, count + 1);
            count++;

            graph.AddNode(new Vector2(875, 125));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(825, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(825, 225));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(825, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 325));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(825, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(775, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 425));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 475));   //Right tunnel enterace ID: 30
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 525));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(775, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(825, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 625));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 675));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 725));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 825));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 875)); //Top right corner ID: 41
            graph.AddEdge(count, count + 1);
            count++;

            graph.AddNode(new Vector2(775, 475));
            graph.AddEdge(30, count + 1);
            count++;
            graph.AddNode(new Vector2(825, 475));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(875, 475));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(925, 475));
            graph.AddEdge(count, count + 1);
            count++;

            graph.AddNode(new Vector2(825, 875));
            graph.AddEdge(41, count + 1);
            count++;
            graph.AddNode(new Vector2(775, 875));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 875));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(675, 875));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(625, 875));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(575, 875));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 875));   //Top right wall ID: 52
            graph.AddEdge(count, count + 1);
            count++;

            graph.AddNode(new Vector2(525, 825));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 725));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(475, 725));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 725));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 825));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 875));   //Top left wall ID: 60
            graph.AddEdge(count, count + 1);
            count++;

            graph.AddNode(new Vector2(575, 775));
            graph.AddEdge(54, count + 1);
            count++;
            graph.AddNode(new Vector2(625, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(675, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(775, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(825, 775));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 39);
            count++;


            graph.AddNode(new Vector2(725, 825));
            graph.AddEdge(count + 1, 48);
            graph.AddEdge(count + 1, 64);
            count++;

            graph.AddNode(new Vector2(725, 725));
            graph.AddEdge(count + 1, 64);
            count++;
            graph.AddNode(new Vector2(725, 675));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(775, 675));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(775, 625));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 33);
            count++;

            graph.AddNode(new Vector2(625, 725));
            graph.AddEdge(count + 1, 62);
            count++;
            graph.AddNode(new Vector2(625, 675));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(575, 675));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 675));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 55);
            count++;

            graph.AddNode(new Vector2(375, 875));
            graph.AddEdge(60, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 875));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(275, 875));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 875));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(175, 875));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(125, 875));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 875));    //Top left corner ID: 82
            graph.AddEdge(count, count + 1);
            count++;

            graph.AddNode(new Vector2(75, 825));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 725));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 675));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 625));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(125, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(175, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 525));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 475));   //Left tunnel enterance ID: 93
            graph.AddEdge(count, count + 1);
            count++;

            graph.AddNode(new Vector2(675, 575));
            graph.AddEdge(32, count + 1);
            count++;
            graph.AddNode(new Vector2(625, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(575, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(475, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(375, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 575));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(275, 575));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 91);
            count++;

            graph.AddNode(new Vector2(575, 625));
            graph.AddEdge(96, count + 1);
            graph.AddEdge(count + 1, 74);
            count++;

            graph.AddNode(new Vector2(375, 775));
            graph.AddEdge(58, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(275, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(175, 775));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(125, 775));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(84, count + 1);
            count++;

            graph.AddNode(new Vector2(225, 825));
            graph.AddEdge(107, count + 1);
            graph.AddEdge(count + 1, 79);
            count++;

            graph.AddNode(new Vector2(225, 725));
            graph.AddEdge(79, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 675));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(175, 675));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(175, 625));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 90);
            count++;

            graph.AddNode(new Vector2(425, 675));
            graph.AddEdge(57, count + 1);
            count++;
            graph.AddNode(new Vector2(375, 675));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 675));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 725));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 105);
            count++;

            graph.AddNode(new Vector2(375, 625));
            graph.AddEdge(100, count + 1);
            graph.AddEdge(count + 1, 116);
            count++;

            graph.AddNode(new Vector2(175, 475));
            graph.AddEdge(93, count + 1);
            count++;
            graph.AddNode(new Vector2(125, 475));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 475));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(25, 475));
            graph.AddEdge(count, count + 1);
            count++;

            graph.AddNode(new Vector2(225, 425));
            graph.AddEdge(93, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(175, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(125, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 325));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(125, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(125, 225));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(125, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(75, 125));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 0);
            count++;

            graph.AddNode(new Vector2(625, 525));
            graph.AddEdge(95, count + 1);
            count++;
            graph.AddNode(new Vector2(625, 475));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(625, 425));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(625, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(675, 375));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(27, count + 1);
            count++;
            graph.AddNode(new Vector2(575, 375));
            graph.AddEdge(140, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 325));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(575, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(625, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(625, 225));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(625, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(575, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 125));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 9);
            count++;

            graph.AddNode(new Vector2(725, 325));
            graph.AddEdge(28, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 225));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(725, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(775, 175));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 19);
            count++;

            graph.AddNode(new Vector2(675, 275));
            graph.AddEdge(146, count + 1);
            graph.AddEdge(count + 1, 153);
            count++;

            graph.AddNode(new Vector2(325, 525));
            graph.AddEdge(101, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 475));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 425));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(375, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 375));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 325));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(375, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 225));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(325, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(375, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 125));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 7);
            count++;

            graph.AddNode(new Vector2(475, 525));
            graph.AddEdge(98, count + 1);
            count++;
            graph.AddNode(new Vector2(475, 475));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(425, 475));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(525, 475));
            graph.AddEdge(175, count + 1);
            count++;

            graph.AddNode(new Vector2(275, 275));
            graph.AddEdge(167, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 275));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 225));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(225, 175));
            graph.AddEdge(count, count + 1);
            count++;
            graph.AddNode(new Vector2(175, 175));
            graph.AddEdge(count, count + 1);
            graph.AddEdge(count + 1, 133);
            count++;

            graph.AddNode(new Vector2(475, 275));
            graph.AddEdge(144, count + 1);
            graph.AddEdge(count + 1, 165);
            count++;

            graph.AddNode(new Vector2(275, 375));
            graph.AddEdge(161, count + 1);
            graph.AddEdge(count + 1, 125);
            count++;

            graph.AddNode(new Vector2(225, 325));
            graph.AddEdge(125, count + 1);
            graph.AddEdge(count + 1, 178);
            count++;
        }
    }
}