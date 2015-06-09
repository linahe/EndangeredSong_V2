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
        public bool isActive;
        bool flipped = false;
        Vector2 origin;
        float frameRate = 0.10f;
        const float timer = 0.10f;
        int frames = 0;
        const int FRAMES = 3;
        float moveTimer = 2;
        float distance = 1000;
        Random rand;
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
            origin = new Vector2(this.dim.X / 2, this.dim.Y / 2);

            rand = new Random();
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
            if (isActive && flipped)
            {
                if(frames == 0)
                    sb.Draw(frame1, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), null, Color.White, 0, origin, SpriteEffects.FlipHorizontally, 0f);
                if (frames == 1)
                    sb.Draw(frame2, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), null, Color.White, 0, origin, SpriteEffects.FlipHorizontally, 0f);
                if (frames == 2)
                    sb.Draw(frame3, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), null, Color.White, 0, origin, SpriteEffects.FlipHorizontally, 0f);
                if (frames == 3)
                    sb.Draw(frame2, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), null, Color.White, 0, origin, SpriteEffects.FlipHorizontally, 0f);
            }
            else if (isActive && !flipped)
            {
                if (frames == 0)
                    sb.Draw(frame1, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), null, Color.White, 0, origin, SpriteEffects.None, 0f);
                if (frames == 1)
                    sb.Draw(frame2, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), null, Color.White, 0, origin, SpriteEffects.None, 0f);
                if (frames == 2)
                    sb.Draw(frame3, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), null, Color.White, 0, origin, SpriteEffects.None, 0f);
                if (frames == 3)
                    sb.Draw(frame2, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), null, Color.White, 0, origin, SpriteEffects.None, 0f);
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
        public void spawn(Player player)
        {

            distance = Math.Abs((player.getPosition() - pos).Length());
            while(distance > 900 || distance < 600)
            {

                this.setPosition(rand.Next(0, 4000), rand.Next(0, 3000));
                distance = (player.getPosition() - pos).Length();
            }

            
        }
        public void Update(Controls controls, GameTime gameTime, Player player, ArrayList harmonians)
        {

            if (this.isActive && this.intersects(player) && !(player.isHidden())) //if bioAgents intersects with player
            {
                player.Die();
                player.deadHarmonians(this, harmonians);
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
         
            Move(controls, player);
        }


        public void Move(Controls controls, Player player)
        {
            Vector2 direction = player.getPosition() -  this.pos;
            if (direction.Length() > 10)
            {
                direction.Normalize();
            }
            
            if (direction.X > 0)
                flipped = true;
            else
                flipped = false;

            if (player.isHidden())
            {
                this.pos = this.pos - direction * 3;
                flipped = !flipped;
            }
            else
                this.pos = this.pos + direction * 6;
        }

        public void Animate()
        {

        }
    }
}