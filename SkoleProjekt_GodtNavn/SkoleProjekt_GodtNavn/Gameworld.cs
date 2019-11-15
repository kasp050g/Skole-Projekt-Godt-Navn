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
                goblinTotem.Remove(go);
            }
            foreach (GameObject go in gameObjectsToBeDelete)
            {
                golemTotem.Remove(go);
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
            Player.HealthAndMana.GUI_Setup();
            // Spell Bar UI
            Player.GUI_Spell_Bar.SetUp();

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

            SetEnemys();
        }

        List<GameObject> goblinTotem = new List<GameObject>();
        Goblin goblinTest;

        List<GameObject> golemTotem = new List<GameObject>();
        Golem golemTest;

        public void ReloadEnemy()
        {
            if(goblinTotem.Count <= 0)
            {
                goblinTest = new Goblin();
                goblinTest.Health.currentValue = 10 * Player.Level;
                goblinTest.Health.maxValue = 10 * Player.Level;
                goblinTest.Transform.Position = new Vector2(4123, 700);
                goblinTest.deleteOnDeath = true;
                goblinTotem.Add(goblinTest);
                Instatiate(goblinTest);
            }

            if (golemTotem.Count <= 0)
            {
                golemTest = new Golem();
                golemTest.Transform.Scale = 2;
                golemTest.IsMelee = false;
                golemTest.Health.currentValue = 20 * Player.Level;
                golemTest.Health.maxValue = 20 * Player.Level;
                golemTest.Transform.Position = new Vector2(7826, -528);
                golemTest.deleteOnDeath = true;
                golemTotem.Add(golemTest);
                Instatiate(golemTest);
            }
        }
        public void SetEnemys()
        {
            // Enemy's
            #region Spwan Goblins

            List<Vector2> tmp_Vector = new List<Vector2>();

            for (int i = 1; i < 10; i++)
            {
                tmp_Vector.Add(new Vector2(1000 * i, 500));
                tmp_Vector.Add(new Vector2(1000 * i, -500));
            }


            for (int i = 0; i < tmp_Vector.Count; i++)
            {
                Goblin goblin01 = new Goblin();
                goblin01.Health.currentValue = 15;
                goblin01.Health.maxValue = 15;
                goblin01.Transform.Scale = 2;
                goblin01.Transform.Position = tmp_Vector[i];
                gameObjects.Add(goblin01);
            }

            #endregion


            Golem golem01 = new Golem();
            golem01.Health.currentValue = 500;
            golem01.Health.maxValue = 500;
            golem01.IsMelee = false;
            golem01.MeleeRange = 300;
            golem01.SpellRange = 700;
            golem01.MaxAggroRange = 1200;
            golem01.Transform.Scale = 4;
            golem01.IsBoss = true;
            golem01.Color = Color.Pink;
            golem01.Transform.Position = new Vector2(11000, 30);
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
            ShowGameObjectList();
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
            spriteBatch.DrawString(font, "GameObject List:", new Vector2(10, 10), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 1);

            foreach (GameObject go in gameObjects)
            {
                spriteBatch.DrawString(font, go.Transform.Position.ToString(), new Vector2(10, ySpace), Color.Black, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 1);
                ySpace += 20;
            }
#endif
        }
    }
}
