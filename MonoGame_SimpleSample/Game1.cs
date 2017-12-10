using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace MonoGame_SimpleSample
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
	enum GameState 
	{
		playing,
		paused
	}
	
	
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch playerSpriteBatch;
        Texture2D playerTexture;
        Texture2D boxTexture;
        AnimatedSprite playerSprite;
        ScrooligBackgroundSprite leftBackground;
        ScrooligBackgroundSprite rightBackground;
        //Sprite boxSprite;
        GameState currentGameState = GameState.playing;
        List<Sprite> imageSpriteList;
        SpriteFont spriteFont;
        bool isPauseKeyHeld = false;
  
        Sprite testSprite;
        string []fileContent;
        string collisionText = "";
        SpriteFont HUDFont;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerSpriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("professor_walk_cycle_no_hat");
            boxTexture = Content.Load<Texture2D>("box");
            //forestImg = Content.Load<Texture2D>("forest");
            //leftBackgoundPosVect = new Vector2(0, 180);
            //rightBackgoundPosVect = new Vector2(forestImg.Width - 4, 180);
            //boxSprite = new Sprite(boxTexture, new Vector2(20, 400));
            leftBackground = new ScrooligBackgroundSprite(Content.Load<Texture2D>("background"), new Vector2(0, 0));
            rightBackground = new ScrooligBackgroundSprite(Content.Load<Texture2D>("background"), new Vector2(Content.Load<Texture2D>("background").Width, 0));
            playerSprite = new AnimatedSprite(playerTexture, new Vector2(0, 1080-300), 4, 9, 1080 - 300, Content.Load<Effect>("shaderEffect"));
            testSprite = new Sprite(playerTexture, new Vector2(300, 200));
            HUDFont = Content.Load<SpriteFont>("HUDFont");
      
            imageSpriteList = new List<Sprite>();

            //FileStream F = new FileStream("imagesList", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fileContent = File.ReadAllLines("imagesList.txt");
            
            foreach (string line in fileContent)
            {
                string[] data = line.Split(' ');
                imageSpriteList.Add(new Sprite(Content.Load<Texture2D>("lol"), new Vector2( int.Parse(data[1]), int.Parse(data[2]))));
            }

            //playerSprite = new TestSprite(playerTexture, Vector2.Zero);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyboardState = Keyboard.GetState();
            if( keyboardState.IsKeyDown(Keys.P) && !isPauseKeyHeld)
            {
                if (currentGameState == GameState.playing)
                        currentGameState = GameState.paused;
                else currentGameState = GameState.playing;
            }
            //This should be in the Input Manager - differentiate between pressed and held
            isPauseKeyHeld = keyboardState.IsKeyUp(Keys.P) ? false : true;
            // TODO: Add your update logic here
            switch (currentGameState)
			{
				case GameState.playing:
				{
					playerSprite.Update(gameTime);
                    testSprite.Update(gameTime);
                    if (keyboardState.IsKeyDown(Keys.D))
                    {
                        leftBackground.Update(gameTime);
                        rightBackground.Update(gameTime);
                    }
                                
                    collisionText = playerSprite.IsCollidingWith(testSprite) ? "there is a collision" : " there is no collision";
                        //check collisions

                        //if ((rightBackground.Position.X <= rightBackground.Texture.Width)
                        //{
                        //    leftBackground.setPositionX(graphics.PreferredBackBufferWidth);
                        //}
                        //if ((leftBackground.Position.X + leftBackground.Texture.Width) <= graphics.PreferredBackBufferWidth)
                        //{
                        //    rightBackground.setPositionX(graphics.PreferredBackBufferWidth);
                        //}
                        collisionText = playerSprite.IsCollidingWith(testSprite) ? "there is a collision" : " there is no collision";
                }
				break;
				
				case GameState.paused:
				{
					
				}
				break;
				
			}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            playerSpriteBatch.Begin(effect: playerSprite.shaderEffect);

            switch (currentGameState)
			{
				case GameState.playing:
				{
                    leftBackground.Draw(spriteBatch);
                    rightBackground.Draw(spriteBatch);

                    playerSprite.Draw(spriteBatch);
                    //testSprite.Draw(spriteBatch);
                    foreach (Sprite sprite in imageSpriteList)
                        sprite.Draw(spriteBatch);
                    int a = graphics.PreferredBackBufferWidth;
                    spriteBatch.DrawString(HUDFont, a.ToString(), new Vector2(300, 0), Color.Red);
                    //spriteBatch.DrawString(HUDFont, fileContent, new Vector2(300, 0), Color.Red);
                    //boxSprite.Draw(spriteBatch);               
                }
				break;
				case GameState.paused:
				{
                    spriteBatch.DrawString(HUDFont, "Game Paused", Vector2.Zero, Color.White);
                }
                break;
			}
            playerSpriteBatch.End();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
