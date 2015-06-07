using System;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;
/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace EndangeredSong
{
    class BIOAgent : Sprite
    {
	    int maxX;
        int maxY;
        bool isActive;
        
        float frameRate = 0.10f;
        const float timer = 0.10f;
        int frames = 0;
        const int FRAMES = 3;
        Texture2D frame1;
        Texture2D frame2;
        Texture2D frame3;

        public BIOAgent(int x, int y, int width, int height, int maxX, int maxY)
	    {
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = width;
            this.dim.Y = height;
            this.maxX = maxX;
            this.maxY = maxY;
            this.frameRate = 1;
            this.isActive = false;            
	    }
        public Rectangle getRect()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y); 
        }
        public Vector2 getPosition()
        {
            return this.pos;
        }
        public void setPosition(Vector2 newPosition)
        {
            this.pos = newPosition;
        }
        public Vector2 getDimension()
        {
            return this.dim;
        }
        public void LoadContent(ContentManager content)
        {
            frame1 = content.Load<Texture2D>("BIOAgentConceptArt.png");
            frame2 = content.Load<Texture2D>("BIOAgentConceptArt3.png");
            frame3 = content.Load<Texture2D>("BIOAgentConceptArt2.png");
        }
        public bool isOnScreen()
        {
            return this.isActive;
        }
        public void Draw(SpriteBatch sb)
        {
            if (isActive)
            {
                if(frames == 0)
                    sb.Draw(frame1, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
                if (frames == 1)
                    sb.Draw(frame2, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
                if (frames == 2)
                    sb.Draw(frame3, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
                if (frames == 3)
                    sb.Draw(frame2, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
            }
        }     
        public void activate()
        {
            this.isActive = true;
        }
        public void disactivate()
        {
            this.isActive = false;
        }
        public void notActive()
        { 
        
        }
        public void Update(Controls controls, GameTime gameTime, Player player, ArrayList harmonians)
        {
            Rectangle r;

            for (int i = 0; i < harmonians.Count; i++)  //loops through harmonian and checks for death of found, unhidden harmonians
            {

                r = ((Harmonian)harmonians[i]).getRect();

                if (this.getRect().Intersects(r) && ((Harmonian)harmonians[i]).getFound() && !((Harmonian)harmonians[i]).getHid())
                {
                    //((Harmonian)harmonians[i]).Die();
                    //Debug.WriteLine("HARMONIAN DEATH");
                }
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameRate -= elapsed;
            if(frameRate < 0)
            {
                frames++;
                if (frames > 3)
                {
                    frames = 0;
                }
                frameRate = timer;
            }
            
            

            //checks for player death
            r = player.getRect();
            if (this.getRect().Intersects(r))
            {                  
                //player.Die();
                //Debug.WriteLine("PLAYER DEATH");
            }            
            Move(controls, player);
        }


        public void Move(Controls controls, Player player)
        {
            Vector2 direction = player.getPosition() -  this.pos;
            if (direction.Length() > 10)
            {
                direction.Normalize();
                this.pos = this.pos + direction * 6;
            }
        }

        public void Animate()
        {

        }
    }
}