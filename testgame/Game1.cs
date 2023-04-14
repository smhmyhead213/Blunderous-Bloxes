using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
//to do 26/02/23: use a list/enum to store all buttons, and recognise each button by its index

namespace testgame
{
    public class Game1 : Game
    {
        //graphics stuff
        
        SpriteFont font;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SoundEffect music;
        SoundEffectInstance musicInstance;
        //---------------------

        //variable initialisation

        
        float currentTime = 0f;
        bool isSpacePressed = false;
        int totalUpdates = 0;
        int boxCreationCooldown = 5;
        int boxDeletionCooldown = 5;
        int screenModeSwitchCooldown = 60;

        int numberOfBoxes = 1;
        bool isMusicPlaying=false;
        Texture2D boxTexture;
        Texture2D titleBackground;


        GameState gameState = new GameState();
        

        List<Box> boxes = new List<Box>();


        Button startButton = new Button();
        Button settingsButton = new Button();

        public enum Buttons
        {
            startButton,
            settingsButton,
        }

        //texture;

        //set g
        float g = 9.81f;

        public Game1()
        {
            Content.RootDirectory = "Content";

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            _graphics.IsFullScreen = true;


        }


        protected override void Initialize()
        {
            gameState.state = GameState.GameStates.TitleScreen;

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
                
            font = Content.Load<SpriteFont>("arial");
            Texture2D texture = Content.Load<Texture2D>("box");
            Texture2D titleBackgroundLoad = Content.Load<Texture2D>("TitleScreen/title");
            Texture2D settingsLoad = Content.Load<Texture2D>("TitleScreen/settings");
            Texture2D startLoad = Content.Load<Texture2D>("TitleScreen/start");
            music = Content.Load<SoundEffect>("Blunder");
            musicInstance = music.CreateInstance();
            musicInstance.IsLooped = true;
            musicInstance.Play();
            //gameMusic = Content.Load<SoundEffect>("Blunder");

            IsMouseVisible = true;

            BoxManager.initiallyCreateBoxes(numberOfBoxes, boxes, _graphics, texture);

            boxTexture = texture;
            titleBackground = titleBackgroundLoad;

            


            startButton.position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 150);
            startButton.texture = startLoad;
            
            settingsButton.position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 350);
            settingsButton.texture = settingsLoad;

        }

        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (startButton.rectangle.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
                gameState.state = GameState.GameStates.InGame;

            if (settingsButton.rectangle.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
                gameState.state = GameState.GameStates.Settings;

            //decrement cooldowns
            if (boxCreationCooldown > 0)
                boxCreationCooldown--;

            if (boxDeletionCooldown > 0)
                boxDeletionCooldown--;

            if (screenModeSwitchCooldown > 0)
                screenModeSwitchCooldown--;


            Methods.HandleScreenStateChanges(kstate, _graphics, boxes, screenModeSwitchCooldown, gameState);


            if (gameState.state == GameState.GameStates.InGame)
            {
                totalUpdates++;

                if (kstate.IsKeyDown(Keys.Enter) && boxCreationCooldown == 0)
                {
                    BoxManager.addBox(boxes, _graphics, ref boxCreationCooldown, boxTexture);
                }

                if (kstate.IsKeyDown(Keys.Back) && boxDeletionCooldown == 0 && boxes.Count > 0)
                {
                    BoxManager.deleteBox(boxes, ref boxDeletionCooldown);
                }


                for (int i = 0; i < boxes.Count; i++)
                {
                    BoxMovementMethods.handleSpaceBarBoost(kstate, boxes, i);
                    BoxMovementMethods.handleBoxMovement(boxes, i, _graphics, gameTime, g);
                }
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (gameState.state == GameState.GameStates.InGame)
            {
                DrawingMethods.drawGame(_spriteBatch, boxes, boxTexture, _graphics, font);
            }

            else if (gameState.state == GameState.GameStates.TitleScreen)
            {
                DrawingMethods.drawTitleScreen(_spriteBatch, titleBackground, startButton.texture, startButton.position, settingsButton.texture, settingsButton.position, _graphics);
            }

            base.Draw(gameTime);
        }

        
    }
}
