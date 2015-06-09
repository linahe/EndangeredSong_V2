﻿using Microsoft.Xna.Framework;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Controls controls;


        Camera camera;
        MiniMap map;
        ScreenManager manager;

        const int harmoniansizeX = 100;
        const int harmoniansizeY = 60;

        ArrayList harmonians;
        ArrayList hidingPlaces;
        ArrayList decorations;
        ArrayList water;


        SoundEffectInstance songInstance;
        Player player;
        BIOAgent b1;

        SoundEffectInstance bioTrouble;
        SoundEffect BIOAgentTheme;

        Random rand;
        int dimX;
        int dimY;
        int screenWidth;
        int screenHeight;
        double elapsedTime;
        int[,] coordPlaces;
        Texture2D endPlace;
        Rectangle endPlaceRect;


        SoundEffect song1;
        
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



            endPlaceRect = new Rectangle(3800, 200, 200, 200);
            harmonians = new ArrayList();
            hidingPlaces = new ArrayList();
            decorations = new ArrayList();
            water = new ArrayList();

            
            coordPlaces = new int[12, 3] { { 150, 2700, 3 }, { 700, 2400, 3 }, { 1050, 1500, 2 }, { 1300, 600, 3 },  {1200, 2700, 3} , { 2000, 2000, 2 },  {2400, 200, 2}, {2750, 2500, 3}, { 2900, 1500, 3 },  {3000, 600, 2 }  ,
                                         {3600, 1300, 3 } , { 3800, 2800, 2} };

            player = new Player(100, 1800, harmoniansizeX, harmoniansizeY, dimX, dimY);
            b1 = new BIOAgent(400, 200, 200, 350, dimX, dimY);
            manager = new ScreenManager(0, 0, screenWidth, screenHeight);
            map = new MiniMap(200, 150, graphics.GraphicsDevice);


            controls = new Controls();
            rand = new Random();


            for (int j = 0; j < 50; j ++ )
            {
                Decor dec = new Decor(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 50, 50);
                decorations.Add(dec);
            }

            for (int i = 0; i < 12; i++)    
            {
                HidingPlace p;
                int x;
                int y;
                if (coordPlaces[i, 2] == 3)
                {
                    x = 400;
                    y = 500;
                }
                else
                {
                    x = 325;
                    y = 300;
                }
                p = new HidingPlace(coordPlaces[i, 0], coordPlaces[i, 1], x, y, coordPlaces[i, 2]);
                hidingPlaces.Add(p);
            }
            for (int k = 0; k < 3; k++)
            {
                Water w = new Water(rand.Next(0, dimX - 100), rand.Next(0, dimY - 100), 450, 200);
                water.Add(w);
            }

            Harmonian h1 = new Harmonian(700, 1900, harmoniansizeX, harmoniansizeY, dimX, dimY, "Harmonian1");
            harmonians.Add(h1);
            Harmonian h2 = new Harmonian(900, 400, harmoniansizeX, harmoniansizeY, dimX, dimY, "Harmonian3");
            harmonians.Add(h2);
            Harmonian h3 = new Harmonian(1300, 2500, harmoniansizeX, harmoniansizeY, dimX, dimY, "Harmonian4");
            harmonians.Add(h3);
            Harmonian h4 = new Harmonian(2000, 2600, harmoniansizeX, harmoniansizeY, dimX, dimY, "Harmonian5");
            harmonians.Add(h4);
            Harmonian h5 = new Harmonian(2800, 2300, harmoniansizeX, harmoniansizeY, dimX, dimY, "Harmonian6");
            harmonians.Add(h5);
            Harmonian h6 = new Harmonian(3800, 2300, harmoniansizeX, harmoniansizeY, dimX, dimY, "Harmonian7");
            harmonians.Add(h6);
            Harmonian h7 = new Harmonian(3400, 1600, harmoniansizeX, harmoniansizeY, dimX, dimY, "Harmonian8");
            harmonians.Add(h7);
            Harmonian h8 = new Harmonian(500, 600, harmoniansizeX, harmoniansizeY, dimX, dimY, "Harmonian9");
            harmonians.Add(h8);
            Harmonian h9 = new Harmonian(2500, 1200, harmoniansizeX, harmoniansizeY, dimX, dimY, "Harmonian10");
            harmonians.Add(h9);
            Harmonian h10 = new Harmonian(700, 300, harmoniansizeX, harmoniansizeY, dimX, dimY, "Harmonian11");
            harmonians.Add(h10);

            song1 = Content.Load<SoundEffect>(@"1Music");

            songInstance = song1.CreateInstance();
            songInstance.IsLooped = true;
            songInstance.Play();


            BIOAgentTheme = Content.Load<SoundEffect>(@"BIOAgents");
            bioTrouble = BIOAgentTheme.CreateInstance();
            
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
            manager.LoadContent(this.Content);

            endPlace = Content.Load<Texture2D>("star.png");

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

            controls.Update();
            if(!manager.isOnMainGame() || player.isDead())                    
            {
                manager.Update(gameTime, controls);
                camera.Update(gameTime, manager, screenWidth, screenHeight);
            }
            else
            {
                player.Update(controls, gameTime, water);
                camera.Update(gameTime, player, screenWidth, screenHeight);
                for (int j = 0; j < decorations.Count; j++ )
                    ((Decor)decorations[j]).Update(controls, gameTime);
                for (int l = 0; l < water.Count; l++)
                    ((Water)water[l]).Update(controls, gameTime, player);
                for (int i = 0; i < hidingPlaces.Count; i++)
                    ((HidingPlace)hidingPlaces[i]).Update(controls, gameTime, player, harmonians);
                for (int k = 0; k < harmonians.Count; k++)
                    ((Harmonian)harmonians[k]).Update(controls, gameTime, player, b1);


                b1.Update(controls, gameTime, player, harmonians);                
                map.Update(graphics.GraphicsDevice, hidingPlaces, harmonians, water, b1, player, endPlaceRect);
               
                if (player.intersects(new Vector2(endPlaceRect.X, endPlaceRect.Y), new Vector2(endPlaceRect.Width, endPlaceRect.Height))) 
                {
                    manager.setToGameWon();
                    song1.Dispose();
                    bioTrouble.Dispose();
                }
                if(player.isDead())
                {
                    manager.setToGameOver();
                    song1.Dispose();
                    bioTrouble.Dispose();
                }

                elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTime >= 5 && !b1.isOnScreen() ) // add bool?
                {
                    b1.activate();
                    songInstance.Volume = 0;
                    bioTrouble.Play();
                    b1.spawn();
                }

                if (elapsedTime >= 12) 
                {
                    b1.disactivate();
                    songInstance.Volume = 1;
                    bioTrouble.Stop();
                    elapsedTime = 0;
                }
                
            }

        
            
            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            if (!manager.isOnMainGame() || player.isDead())
                manager.Draw(spriteBatch);
            else
            {
                spriteBatch.Draw(endPlace, endPlaceRect, Color.White);

                for (int j = 0; j < decorations.Count; j++)
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
