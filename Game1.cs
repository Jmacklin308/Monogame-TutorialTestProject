using Apos.Gui;
using FontStashSharp;
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
        
        
        //gui
        private IMGUI _ui;
        

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
            //apos GUI setup
            FontSystem fontSystem = FontSystemFactory.Create(GraphicsDevice);
            fontSystem.AddFont(TitleContainer.OpenStream($"{Content.RootDirectory}/DroidSans.ttf"));
            
            GuiHelper.Setup(this, fontSystem);
            _ui = new IMGUI();
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //draw player
            playerSpriteSheet = Content.Load<Texture2D>("character-animations");
        }

        protected override void Update(GameTime gameTime)
        {
            CheckPlayerInput((float)gameTime.ElapsedGameTime.TotalSeconds);
            CallMenu(gameTime);

            //core update (dont mess with this)
            base.Update(gameTime);
        }

        private void CallMenu(GameTime gametime)
        {
            //Call update setup at start
            GuiHelper.UpdateSetup(gametime);
            _ui.UpdateAll(gametime);
            
            bool _showFun = false;
            
            
            //Create the UI
            MenuPanel.Push();
            if (Button.Put("TEST HAPPY FUN TIME").Clicked)
            {
                _showFun = !_showFun;
            }

            if (_showFun)
            {
                Label.Put("This is fun!!!");
            }

            if (Button.Put("QUIT").Clicked)
            {
                Exit();
            }

            MenuPanel.Pop();
            
            //clean up
            GuiHelper.UpdateCleanup();
            

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
            
                //draw gui
                _ui.Draw(gameTime);
                //draw player
                _spriteBatch.Draw(playerSpriteSheet, playerPos,Color.White);
                
            
            
            _spriteBatch.End();
            
            
            //base draw (dont mess with this!)
            base.Draw(gameTime);
        }
    }
}