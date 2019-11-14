using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;

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
        public static Color backgroundColour = Color.CornflowerBlue;



        public List<GameObject> gameObjects = new List<GameObject>();
        static public List<GameObject> gameObjectsToBeDelete = new List<GameObject>();
        static public List<GameObject> newGameObjects = new List<GameObject>();



        static private List<GUI_Component> uiList = new List<GUI_Component>();
        public static Vector2 ScreenSize { get; private set; }
        public Camera camera = new Camera();
        public static Player Player = new Player();
        public static SpriteContainer spriteContainer = new SpriteContainer();
        public static AudioPlayer audioPlayer = new AudioPlayer();

        public Gameworld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ScreenSetting();
        }

        public static void Instatiate(GameObject gameObject)
        {
            newGameObjects.Add(gameObject);
        }
        private void CallInstatiate()
        {
            for (int i = 0; i < newGameObjects.Count; i++)
            {
                newGameObjects[i].Initialize();
                newGameObjects[i].LoadContent(Content);
            }
            gameObjects.AddRange(newGameObjects);
            newGameObjects.Clear();
        }
        // Destroy
        public static void Destroy(GameObject gameObject)
        {
            gameObjectsToBeDelete.Add(gameObject);
        }
        private void CallDestroy()
        {
            foreach (GameObject go in gameObjectsToBeDelete)
            {
                gameObjects.Remove(go);
            }
            foreach (GameObject go in gameObjectsToBeDelete)
            {
                enemies.Remove(go);
            }
            gameObjectsToBeDelete.Clear();
        }

        public void ScreenSetting()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1800;
            graphics.PreferredBackBufferHeight = 1000;

            ScreenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }


        public static void AddGUI(GUI_Component gui_Component)
        {
            uiList.Add(gui_Component);
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            spriteContainer.LoadContent(Content);
            audioPlayer.LoadContent(Content);
            MakeWorld();
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            gameObjects.Add(Player);
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
            // UI Inventory
            Player.GUI_Inventory.LoadContent(Content);
            Player.GUI_Inventory.GUI_Setup();
            // UI Equipment
            Player.GUI_Equipment.LoadContent(Content);
            Player.GUI_Equipment.GUI_Setup();
            // Player UI
            Player.healthAndMana.GUI_Setup();

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

        public void MakeWorld()
        {
            audioPlayer.Song_Play("Song_Adventure",0.1f);
            World world = new World();
            gameObjects.Add(world);

            //SetEnemys();
        }

        List<GameObject> enemies = new List<GameObject>();
        Goblin goblinTest;

        public void ReloadEnemy()
        {
            if(enemies.Count <= 0)
            {
                goblinTest = new Goblin();
                goblinTest.health.currentValue = 5;
                goblinTest.transform.position = new Vector2(300, 300);
                goblinTest.deleteOnDeath = true;
                enemies.Add(goblinTest);
                Instatiate(goblinTest);
            }
        }
        public void SetEnemys()
        {
            // Enemy's
            goblinTest.health.currentValue = 5;
            goblinTest.transform.position = new Vector2(300, 300);
            gameObjects.Add(goblinTest);

            Golem golem01 = new Golem();
            golem01.health.currentValue = 5;
            golem01.isMelee = false;
            golem01.meleeRange = 500;
            golem01.transform.position = new Vector2(300, 100);
            gameObjects.Add(golem01);
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


            //var random = new Random();
            //backgroundColour = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

            
            // TODO: Add your update logic here
            audioPlayer.allSound(gameTime);
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);

                foreach (GameObject other in gameObjects)
                {
                    go.CheckCollision(other);
                }
            }

            foreach (GUI_Component ui in uiList)
            {
                ui.Update(gameTime);
            }

            camera.Follow(Player);

            base.Update(gameTime);


            CallDestroy();
            CallInstatiate();
            ReloadEnemy();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColour);

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

            // UI
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            foreach (GUI_Component ui in uiList)
            {
                ui.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();



            base.Draw(gameTime);
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
