using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
//using System.Drawing;

namespace EndangeredSong
{
    class Harmonian : Sprite
    {
        bool isHid;
        bool isFound;
        int maxX;
        int maxY;
        int foundPosition;
        string songName;
        bool hasPlayed;
        SoundEffect song;
        SoundEffectInstance s;
        bool isDead;

        float timer = 24;         
        const float TIMER = 24;

        public Harmonian(int x, int y, int width, int height, int maxX, int maxY, string sn)
	    {            
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = width;
            this.dim.Y = height;
            this.maxX = maxX;
            this.maxY = maxY;
            this.isHid = false;
            this.isFound = false;
            this.foundPosition = -1;
            this.songName = sn;
            this.hasPlayed = false;
            this.isDead = false;
	    }

        public Vector2 getPosition()
        {
            return this.pos;
        }
        public Vector2 getDimension()
        {
            return this.dim;
        }
        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("Harmonian.png");
//            song = content.Load<SoundEffect>(@songName);
//            s = song.CreateInstance();
//            s.Volume = 0;
//            s.Play();
        }

        public bool getFound()
        {
            return this.isFound;
        }

        public bool getHid()
        {
            return this.isHid;
        }

        public bool getDead()
        {
            return this.isDead;
        }
        public Rectangle getRect()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y);
        }
        public void setFound(bool b)
        {
            this.isFound = b;
        }
        public void setHid(bool b)
        {
            this.isHid = b;
        }
        public void Draw(SpriteBatch sb)
        {

          if(!this.isHid && !this.isDead)
                sb.Draw(image, this.getRect(), Color.White);
        }

        public void Update(Controls controls, GameTime gameTime, Player player)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;

            Move(controls, player);
            if (timer < 0)
            { 
//                s.Play();
                timer = TIMER;
            }
            if (this.isFound)
            {
//                s.Volume = 1;
                //this.isHid = player.isHidden();
            } 
            }

        public void Move(Controls controls, Player player)
        {
            Vector2 direction = player.getPosition() - this.pos;

            if (direction.Length() < 100 && !this.isFound)
            {
                this.foundPosition = player.getNumFound();
                player.foundHarmonian();
                this.isFound = true;
            }
                
            if (this.isFound && !this.isHid)
            {
                if (direction.Length() > 100)
                {
                    direction += player.getFollowingPosition(this.foundPosition % 8);
                    direction.Normalize();
                    this.pos = this.pos + direction * 6;
                }
            }
        }

        //These are method stubs that may be necessary.
        public void BIOAgentsAreComing()
        {

        }
       public void Die()
        {
            this.isDead = true;
            Console.WriteLine("HARMONIAN DIED");
        }

        
    }
}