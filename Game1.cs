using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TutorialTestProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        //player
        private Texture2D playerSpriteSheet;
        private Vector2 playerPos;
        private float playerSpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            playerPos = new Vector2(_graphics.PreferredBackBufferWidth * 0.5f, _graphics.PreferredBackBufferHeight * 0.5f);
            playerSpeed = 10.0f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerSpriteSheet = Content.Load<Texture2D>("character-animations");
        }

        protected override void Update(GameTime gameTime)
        {

            CheckPlayerInput((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        private void CheckPlayerInput(float totalSeconds)
        {
              
            var keyboardState = Keyboard.GetState();
            var gamepadState = GamePad.GetState(PlayerIndex.One);
            
            //keyboard input
            if (keyboardState.IsKeyDown(Keys.W))
                playerPos.Y -= playerSpeed * totalSeconds;
            if (keyboardState.IsKeyDown(Keys.S))
                playerPos.Y += playerSpeed * totalSeconds;
            if (keyboardState.IsKeyDown(Keys.A))
                playerPos.X -= playerSpeed * totalSeconds;
            if (keyboardState.IsKeyDown(Keys.D))
                playerPos.X += playerSpeed * totalSeconds;
            
            
            //close the game 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            _spriteBatch.Begin();
            _spriteBatch.Draw(playerSpriteSheet, playerPos,Color.White);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}