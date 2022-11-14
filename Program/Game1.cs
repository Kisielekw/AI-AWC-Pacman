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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _shapeBatcher.Begin();
            
            _shapeBatcher.DrawCircle(new Vector2(175, 250), 25, 16, Color.Red);
            _shapeBatcher.DrawRectangle(new Vector2(150, 225), 25, 50, Color.Red);
            _shapeBatcher.DrawCircle(new Vector2(155, 227), 5, 3, Color.CornflowerBlue);
            _shapeBatcher.DrawCircle(new Vector2(165, 227), 5, 3, Color.CornflowerBlue);
            _shapeBatcher.DrawCircle(new Vector2(175, 227), 5, 3, Color.CornflowerBlue);
            _shapeBatcher.DrawCircle(new Vector2(185, 227), 5, 3, Color.CornflowerBlue);
            _shapeBatcher.DrawCircle(new Vector2(195, 227), 5, 3, Color.CornflowerBlue);
            _shapeBatcher.DrawCircle(new Vector2(165, 255), 7, 10, Color.White);
            _shapeBatcher.DrawCircle(new Vector2(185, 255), 7, 10, Color.White);
            _shapeBatcher.DrawCircle(new Vector2(170, 255), 3.5f, 10, Color.DarkBlue);
            _shapeBatcher.DrawCircle(new Vector2(190, 255), 3.5f, 10, Color.DarkBlue);
            _shapeBatcher.End();

            base.Draw(gameTime);
        }
    }
}