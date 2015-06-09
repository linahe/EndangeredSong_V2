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
        bool hidden;
        bool found;
        int maxX;
        int maxY;
        int foundPosition;
        string songName;
        SoundEffect song;
        SoundEffectInstance s;
        Texture2D image;
        bool dead;


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
            this.hidden = false;
            this.found = false;
            this.foundPosition = -1;
            this.songName = sn;
            this.dead = false;

	    }

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("Harmonian.png");
            song = content.Load<SoundEffect>(@songName);
            s = song.CreateInstance();
            s.Volume = 0;
            s.Play();
        }

        public bool isFound()
        {
            return this.found;
        }

        public bool isHid()
        {
            return this.hidden;
        }

        public bool isDead()
        {
            return this.dead;
        }

        public void setFound(bool b)
        {
            this.found = b;
        }
        public void setHid(bool b)
        {
            this.hidden = b;
        }
        public void Draw(SpriteBatch sb)
        {
          if(!this.hidden && !this.dead)
              sb.Draw(image, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
        }

        public void Update(Controls controls, GameTime gameTime, Player player, BIOAgent b)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;

            Move(controls, player);
            
            if (timer < 0)
            { 
                s.Play();
                timer = TIMER;
            }
            if (this.found && !b.isActive)
            {
                s.Volume = 1;
            }
            if (b.isActive || player.isDead())
                s.Volume = 0;
            }

        public void Move(Controls controls, Player player)
        {
            Vector2 direction = player.getPosition() - this.pos;

            if (direction.Length() < 100 && !this.found)
            {
                this.foundPosition = player.getNumFound();
                player.foundHarmonian();
                this.found = true;
            }
                
            if (this.found && !this.hidden)
            {
                if (direction.Length() > 100)
                {
                    direction += player.getFollowingPosition(this.foundPosition % 8);
                    direction.Normalize();
                    this.pos = this.pos + direction * 6;
                }
            }
        }

        public void BIOAgentsAreComing(BIOAgent bioagent)
        {
                s.Volume = 0;
        }
       public void Die()
        {
            this.dead = true;
            s.Stop();
            Console.WriteLine("HARMONIAN DIED");
        }

        
    }
}