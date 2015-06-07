using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
//using OpenTK;
using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;

namespace EndangeredSong
{

    public class Game1 : Game
    {
  //      Song song;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Controls controls;

        Menu menu;
        bool started;
        Camera camera;
        MiniMap map;


        ArrayList harmonians;
        ArrayList hidingPlaces;
        ArrayList decorations;
        ArrayList water;

        Player player;
        BIOAgent b1;

        Random rand;
        int dimX;
        int dimY;
        int screenWidth;
        int screenHeight;
        int harmonianCount;
        double elapsedTime;
        int[,] coordPlaces;


        SoundEffect song1;
        SoundEffect song2;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Endangered Song";
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        protected override void Initialize()
        {
            IsMouseVisible = false;
            camera = new Camera(GraphicsDevice.Viewport);
            GraphicsDevice.Viewport = new Viewport(0, 0, 4000, 3000);
            screenWidth = 1300;
            screenHeight = 700;
            graphics.PreferredBackBufferWidth = screenWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = screenHeight;   // set this value to the desired height of your window          
            graphics.ApplyChanges();
            dimX = GraphicsDevice.Viewport.Bounds.Width;
            dimY = GraphicsDevice.Viewport.Bounds.Height;

            harmonians = new ArrayList();
            hidingPlaces = new ArrayList();
            decorations = new ArrayList();
            water = new ArrayList();
            coordPlaces = new int[16, 2] { { 300, 2200 }, { 700, 1800 }, { 700, 1200 }, { 700, 1500 }, { 750, 100 }, {800, 2700}, { 1200, 2500 }, {1600, 1300 } , { 2000, 1800} , {2400, 200}, {2450, 200} , {2600, 100} , {3000 , 1300} , {3400, 1700 } , {3500 , 1800} , {3600 , 1750} }; 
            player = new Player(300, 1500, 200, 120, dimX, dimY);
            b1 = new BIOAgent(600, 300, 200, 350, dimX, dimY);
            menu = new Menu(0, 0, screenWidth, screenHeight);
            map = new MiniMap(200, 150, graphics.GraphicsDevice);
            started = false;
            harmonianCount = 1;

            controls = new Controls();
            rand = new Random();

            for (int j = 0; j < 50; j ++ )
            {
                Decor dec = new Decor(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 50, 50);
                decorations.Add(dec);
            }

            for (int i = 0; i < 16; i++)    
            {                
                HidingPlace p = new HidingPlace(coordPlaces[i, 0], coordPlaces[i,1], 400, 500, rand.Next(1, 4));                
                hidingPlaces.Add(p);
            }

            for (int k = 0; k < 3; k++)
            {
                Water w = new Water(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 450, 200);
                water.Add(w);
            }

            Harmonian h1 = new Harmonian(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 200, 120, dimX, dimY, "Harmonian1");
            harmonians.Add(h1);
            Harmonian h2 = new Harmonian(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 200, 120, dimX, dimY, "Harmonian3");
            harmonians.Add(h2);
            Harmonian h3 = new Harmonian(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 200, 120, dimX, dimY, "Harmonian4");
            harmonians.Add(h3);
            Harmonian h4 = new Harmonian(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 200, 120, dimX, dimY, "Harmonian5");
            harmonians.Add(h4);
            Harmonian h5 = new Harmonian(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 200, 120, dimX, dimY, "Harmonian6");
            harmonians.Add(h2);
            Harmonian h6 = new Harmonian(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 200, 120, dimX, dimY, "Harmonian7");
            harmonians.Add(h2);
            Harmonian h7 = new Harmonian(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 200, 120, dimX, dimY, "Harmonian8");
            harmonians.Add(h7);
            Harmonian h8 = new Harmonian(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 200, 120, dimX, dimY, "Harmonian9");
            harmonians.Add(h8);
            Harmonian h9 = new Harmonian(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 200, 120, dimX, dimY, "Harmonian10");
            harmonians.Add(h9);
            Harmonian h10 = new Harmonian(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 200, 120, dimX, dimY, "Harmonian11");
            harmonians.Add(h10);

            song1 = Content.Load<SoundEffect>(@"1Music");

            var songInstance = song1.CreateInstance();
            songInstance.IsLooped = true;
            songInstance.Play();
                
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);

            b1.LoadContent(this.Content);
            player.LoadContent(this.Content);
            menu.LoadContent(this.Content);

            for (int j = 0; j < decorations.Count; j++)
                ((Decor)decorations[j]).LoadContent(this.Content);
            for (int l = 0; l < water.Count; l++)
                ((Water)water[l]).LoadContent(this.Content);
            for (int i = 0; i < hidingPlaces.Count; i++)                           
                ((HidingPlace)hidingPlaces[i]).LoadContent(this.Content);
            for (int k = 0; k < harmonians.Count; k++)
                ((Harmonian)harmonians[k]).LoadContent(this.Content);            
        }

        protected override void UnloadContent()
        {
        }

        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
                started = true;

            controls.Update();

            if (started)
            {
                camera.Update(gameTime, player, screenWidth, screenHeight);
                for (int j = 0; j < decorations.Count; j++ )
                    ((Decor)decorations[j]).Update(controls, gameTime);
                for (int l = 0; l < water.Count; l++)
                    ((Water)water[l]).Update(controls, gameTime, player);
                for (int i = 0; i < hidingPlaces.Count; i++)
                    ((HidingPlace)hidingPlaces[i]).Update(controls, gameTime, player, harmonians);
                for (int k = 0; k < harmonians.Count; k++)
                    ((Harmonian)harmonians[k]).Update(controls, gameTime, player);

                b1.Update(controls, gameTime, player, harmonians);
                player.Update(controls, gameTime);
                map.Update(graphics.GraphicsDevice, hidingPlaces, harmonians, water, b1, player);

                elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTime%20 >= 5 && !b1.isOnScreen() ) // add bool?
                {
                    b1.activate();
                    b1.setPosition(new Vector2(rand.Next(0, 4000), rand.Next(0, 3000)));
                    //b1.setPosition(new Vector2(player.getPosition().X, player.getPosition().Y));                    
                }

                if (elapsedTime % 20 >= 10) 
                {
                    b1.disactivate();
                    elapsedTime = 0;
                }
            }

            else
            {
                menu.Update();
                camera.Update(gameTime, menu, screenWidth, screenHeight);
            }
            
            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            if (!started)
                menu.Draw(spriteBatch);
            else
            {               
                for (int j = 0; j < decorations.Count; j++ )
                    ((Decor)decorations[j]).Draw(spriteBatch);
                for (int l = 0; l < water.Count; l++)
                    ((Water)water[l]).Draw(spriteBatch);               
                for (int i = 0; i < hidingPlaces.Count; i++)  
                    ((HidingPlace)hidingPlaces[i]).Draw(spriteBatch);
                for (int k = 0; k < harmonians.Count; k++)
                    ((Harmonian)harmonians[k]).Draw(spriteBatch);
                b1.Draw(spriteBatch);
                player.Draw(spriteBatch);
                map.Draw(spriteBatch, (int)camera.center.X + screenWidth - 200, (int)camera.center.Y);                
            }
                        
            spriteBatch.End();           
            base.Draw(gameTime);
        }
    }
}
