using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PLAY
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MainScrolling ScrollingBack1;
        private MainScrolling ScrollingBack2;
        private Texture2D _playerTexture;
        private Vector2 _playerPosition;
        private Vector2 _playerVelocity;
        private float _gravity;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 750;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            _playerPosition = new Vector2(100, GraphicsDevice.Viewport.Height - 100);
            _playerVelocity = new Vector2(300, 0);
            _gravity = 500;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ScrollingBack1 = new MainScrolling(Content.Load<Texture2D>("ScrollingBack01"), new Rectangle(0, 0, 1200, 750));
            ScrollingBack2 = new MainScrolling(Content.Load<Texture2D>("ScrollingBack02"), new Rectangle(1200, 0, 1200, 750));
            _playerTexture = Content.Load<Texture2D>("player");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                _gravity = -_gravity;
                _playerVelocity.Y = -_playerVelocity.Y;
                _playerPosition.Y = _gravity > 0
                    ? GraphicsDevice.Viewport.Height - _playerTexture.Height
                    : 0;
            }

            _playerVelocity.Y += _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _playerPosition += _playerVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_playerPosition.Y < 0)
            {
                _playerPosition.Y = 0;
                _playerVelocity.Y = -_playerVelocity.Y;
            }
            else if (_playerPosition.Y > GraphicsDevice.Viewport.Height - _playerTexture.Height)
            {
                _playerPosition.Y = GraphicsDevice.Viewport.Height - _playerTexture.Height;
                _playerVelocity.Y = -_playerVelocity.Y;
            }

            // Добавленный код начинается здесь
            if (_playerPosition.X >= 0)
            {
                _playerPosition.X = 0;
                _playerVelocity.X = 0;
            }
            else if (_playerPosition.X > GraphicsDevice.Viewport.Width - _playerTexture.Width)
            {
                _playerPosition.X = GraphicsDevice.Viewport.Width - _playerTexture.Width;
                _playerVelocity.X = 0;
            }
            // И заканчивается здесь

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }

            if (ScrollingBack1.BackgrRectangle.X + ScrollingBack1.BackgrTexture.Width <= 0)
            {
                ScrollingBack1.BackgrRectangle.X = ScrollingBack2.BackgrRectangle.X + ScrollingBack2.BackgrTexture.Width;
            }

            if (ScrollingBack2.BackgrRectangle.X + ScrollingBack2.BackgrTexture.Width <= 0)
            {
                ScrollingBack2.BackgrRectangle.X = ScrollingBack1.BackgrRectangle.X + ScrollingBack2.BackgrTexture.Width;
            }

            ScrollingBack1.Update();
            ScrollingBack2.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            ScrollingBack1.Draw(_spriteBatch);

            ScrollingBack2.Draw(_spriteBatch);

            _spriteBatch.Draw(_playerTexture, _playerPosition, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}