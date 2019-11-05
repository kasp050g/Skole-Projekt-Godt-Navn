using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace SkoleProjekt_GodtNavn
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Gameworld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D collisionTexture;


        public List<GameObject> gameObjects = new List<GameObject>();
        List<GameObject> gameObjectsToBeDeleted = new List<GameObject>();
        List<GameObject> newGameObjects = new List<GameObject>();
        public static Vector2 ScreenSize { get; private set; }
        public Camera camera = new Camera();
        public static Player player = new Player();

        public Gameworld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ScreenSetting();
        }

        public void ScreenSetting()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1800;
            graphics.PreferredBackBufferHeight = 1000;

            ScreenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
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
            gameObjects.Add(player);
            foreach (GameObject go in gameObjects)
            {
                go.Initialize();
            }
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
            font = Content.Load<SpriteFont>("Font/NormalFont");
            collisionTexture = Content.Load<Texture2D>("Texture/Component/CollisionTexture");

            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(Content);
            }
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

            // TODO: Add your update logic here
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
            }

            camera.Follow(player);

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
            spriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: camera.Transform);

            ShowGameObjectList();

            // Draw all Gameobjects
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
                // only draw collisionBox in Debug.
#if DEBUG
                DrawCollisionBox(go);
#endif
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Instantiate(GameObject go)
        {

        }

        public void Destroy(GameObject go)
        {

        }

        private void DrawCollisionBox(GameObject go)
        {
            Rectangle collisionBox = go.CollisionBox;
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rigthLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rigthLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void ShowGameObjectList()
        {
#if DEBUG
            // Draw GameObject List
            int ySpace = 30;
            spriteBatch.DrawString(font, "GameObject List:", new Vector2(10, 10), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);

            foreach (GameObject go in gameObjects)
            {
                spriteBatch.DrawString(font, go.transform.position.ToString(), new Vector2(10, ySpace), Color.Black);
                ySpace += 20;
            }
#endif
        }
    }
}
