using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ShapeBatcher _shapeBatcher;

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
            Pinky = new Pinky(new Vector2(200, 100));
            Inky = new Inky(new Vector2(300, 100));
            Clyde = new Clyde(new Vector2(400, 100));
            Blinky = new Blinky(new Vector2(100, 100), backgroundColour);
            Pinky = new Pinky(new Vector2(200, 100), backgroundColour);
            Inky = new Inky(new Vector2(300, 100), backgroundColour);
            Clyde = new Clyde(new Vector2(400, 100), backgroundColour);

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
            _shapeBatcher.End();

            base.Draw(gameTime);
        }
    }
}