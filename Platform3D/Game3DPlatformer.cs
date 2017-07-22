using _3DPlatformer.Engine.Entities;
using _3DPlatformer.Engine;
using _3DPlatformer.Engine.Collisions;
using _3DPlatformer.Engine.Primitives;
using _3DPlatformer.Graphics.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using _3DPlatformer.Engine.World;

namespace _3DPlatformer
{
    public class Game3DPlatformer : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Cuboid cuboid;

        private double updateTimeAccumulator;

        public BasicEffect BasicEffect { get; set; }
        public CubeEffect CubeEffect { get; private set; }
        Matrix world = Matrix.CreateTranslation(0, 0, 0);
        Matrix view = Matrix.CreateLookAt(new Vector3(3, 2, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        public Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 100f);
        Vector3 look = new Vector3(3, 2, 3);
        Vector3 position = new Vector3(3, 0, 3);
        Vector2 last;
        Vector2 rotation;
        Player player;
        public Texture2D test;
       
        public Camera Camera { get; set; }

        public InputManager InputManager { get; set; }
        public EntityManager EntityManager { get; set; }
        public CollisionManager CollisionManager { get; set; }

        public World World { get; set; }

        public SpriteFont BasicFont { get; private set; }
        public Model BaseCube { get; set; }

        public Game3DPlatformer()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Instance = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.PreferMultiSampling = true;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;
            this.IsFixedTimeStep = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            BasicFont = Content.Load<SpriteFont>("Basic");
            BaseCube = Content.Load<Model>("BaseCube");
            //CubeEffect = new CubeEffect(Content.Load<Effect>("CubeEffect"));
            InputManager = new InputManager();
            EntityManager = new EntityManager();
            CollisionManager = new CollisionManager(EntityManager);
            Camera = new Camera();

            test = Content.Load<Texture2D>("tiles"); 

            //CubeEffect.World = world;
            //CubeEffect.View = view;
            //CubeEffect.Projection = projection;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            BasicEffect = new BasicEffect(GraphicsDevice);
            BasicEffect.World = world;
            BasicEffect.View = view;
            BasicEffect.Projection = projection;
            BasicEffect.LightingEnabled = true;
            BasicEffect.EnableDefaultLighting();
            BasicEffect.TextureEnabled = true;
            BasicEffect.DirectionalLight0.SpecularColor = BasicEffect.DirectionalLight0.SpecularColor * 0.2f;
            //BasicEffect.DirectionalLight0.DiffuseColor = Color.White.ToVector3();
            //BasicEffect.DirectionalLight0.Direction = new Vector3(3, -2, 1);

            cuboid = new Cuboid(this, 0, 10, 0, 10, 2, 1);
            player = new Player();
            World = new World();
            World.EnqueueChunks();

            EntityManager.Add(player);

            Components.Add(InputManager);
            Components.Add(cuboid);
            Components.Add(EntityManager);
            Components.Add(Camera);

            Camera.Follow(player);

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
            
            updateTimeAccumulator += gameTime.ElapsedGameTime.TotalSeconds;

            //if (updateTimeAccumulator > 0.01)
            {
                gameTime = new GameTime(gameTime.TotalGameTime, new TimeSpan((long)(updateTimeAccumulator  * 10e6)));
                updateTimeAccumulator = 0;

                var mouseState = Mouse.GetState();
                var keyboardState = Keyboard.GetState();

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Vector2 delta = mouseState.Position.ToVector2() - last;

                    rotation.X += delta.X / 200;
                    rotation.Y += delta.Y / 200;
                }

                if (keyboardState.IsKeyDown(Keys.W))
                {
                    position += new Vector3((float)Math.Cos(rotation.X) * 4, 0, (float)Math.Sin(rotation.X) * 4) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (keyboardState.IsKeyDown(Keys.S))
                {
                    position += new Vector3(-(float)Math.Cos(rotation.X) * 4, 0, -(float)Math.Sin(rotation.X) * 4) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (keyboardState.IsKeyDown(Keys.A))
                {
                    position += new Vector3((float)Math.Cos(rotation.X - Math.PI / 2) * 4, 0, (float)Math.Sin(rotation.X - Math.PI / 2) * 4) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (keyboardState.IsKeyDown(Keys.D))
                {
                    position += new Vector3((float)Math.Cos(rotation.X + Math.PI / 2) * 4, 0, (float)Math.Sin(rotation.X + Math.PI / 2) * 4) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    position += new Vector3(0, 4f, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                if (keyboardState.IsKeyDown(Keys.LeftShift))
                {
                    position += new Vector3(0, -4f, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                //BasicEffect.View = Matrix.CreateLookAt(position, position + new Vector3((float)Math.Cos(rotation.X) * 3, -(float)Math.Tan(rotation.Y) * 3, (float)Math.Sin(rotation.X) * 3), new Vector3(0, 1, 0));

                last = mouseState.Position.ToVector2();

                // TODO: Add your update logic here

                World.Update(gameTime);

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Camera.Apply(CubeEffect);
            Camera.Apply(BasicEffect);

            World.Draw();

            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.DrawString(BasicFont, "" + (int)(1 / gameTime.ElapsedGameTime.TotalSeconds), Vector2.Zero, Color.Black);
            spriteBatch.End();

            GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

        }

        public static Game3DPlatformer Instance { get; private set; }
    }
}
