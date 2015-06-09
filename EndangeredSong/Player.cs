using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;
//using System.Drawing;
using System.Collections;

namespace EndangeredSong
{
    class Player : Sprite
    {
        int[,] followingPositions;
        bool isHid;
        int maxX;
        int maxY;
        int numFound;
        Texture2D image;
        bool dead;
        Vector2 origin;
        bool flipped;
        
        public Player (int x, int y, int width, int height, int maxX, int maxY)
	    {
            
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = width;
            this.dim.Y = height;

            this.maxX = maxX;
            this.maxY = maxY;

            this.isHid = false;

            followingPositions = new int[8, 2] {{-60, -60},{0, -80},{60, -60},{80, 0},{60, 60},{0, 80},{-60, 60},{-80, 0}};
            numFound = 0;
            origin = new Vector2(this.dim.X / 2, this.dim.Y / 2);
            this.dead = false;
            angle = 0;
	    }
       
        public int getNumFound()
        {
            return this.numFound;
        }
        public Vector2 getFollowingPosition(int x)
        {
            return new Vector2(followingPositions[x, 0], followingPositions[x, 1]);
        }
       
        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("Harmonian.png");
        }
        public bool isHidden()
        {
            return this.isHid;
        }
        public bool isDead()
        {
            return this.dead;
        }
        public void foundHarmonian()
        {
            this.numFound++;
        }
        public void Hide()
        {
            this.isHid = !this.isHid;
        }
        public void Draw(SpriteBatch sb)
        {
            if(!this.isHid && !this.dead && flipped)
                sb.Draw(image, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), null, Color.White, 0, origin, SpriteEffects.FlipHorizontally, 0f);
            if (!this.isHid && !this.dead && !flipped)
                sb.Draw(image, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), null, Color.White, 0, origin, SpriteEffects.None, 0f);
        }

        public void Update(Controls controls, GameTime gameTime, ArrayList obstacles)
        {
            if (!this.dead)
            {
                Move(controls, obstacles);
            }
        }

        public void HideHarmonians(HidingPlace h, ArrayList harmonians)
        {
            
            if (!h.isEmpty())   //tree is filled, take back all harmonians
            { 
                if (this.isHid)
                {
                    this.Hide();
                    h.empty();
                }               
                for (int i = 0; i < harmonians.Count; i++)
                {
                    
                    if (h.isEmpty())
                        break;
                    if (((Harmonian)harmonians[i]).isFound() && ((Harmonian)harmonians[i]).isHid() 
                                                              && h.intersects((Harmonian)harmonians[i]))
                    {
                        ((Harmonian)harmonians[i]).setHid(false);
                        h.empty();
                        this.numFound++;
                    }

                }
            }
            else        //tree is empty, fill with harmonians
            {
                for (int i = 0; i < harmonians.Count; i++)
                {
                    if (h.isFull())
                        break;                    
                    if (((Harmonian)harmonians[i]).isFound() && !((Harmonian)harmonians[i]).isHid()
                                                              && h.intersects((Harmonian)harmonians[i]))
                    {
                        ((Harmonian)harmonians[i]).setHid(true);
                        h.fill();
                        this.numFound--;
                    }
                    if (this.numFound == 0 && !h.isFull())
                    {
                        this.Hide();
                        h.fill();
                        break;
                    }
                }
            }
        }
        public void deadHarmonians(BIOAgent b, ArrayList harmonians)
        {
            for (int i = 0; i < harmonians.Count; i++)
                if (((Harmonian)harmonians[i]).isFound() && !((Harmonian)harmonians[i]).isHid()
                    && (!((Harmonian)harmonians[i]).isDead())
                    && b.intersects((Harmonian)harmonians[i]))
                {
                    ((Harmonian)harmonians[i]).Die();
                }
        }

        public void Die()
        {
            this.dead = true;
        }

        public void Move(Controls controls, ArrayList obstacles)
        {

            if(!this.isHid)
            {
                Vector2 direction = new Vector2();
            
                if (controls.isPressed(Keys.Right, Buttons.DPadRight) && this.pos.X < maxX-this.dim.X)
                    direction.X = 1;
                if (controls.isPressed(Keys.Left, Buttons.DPadLeft) && this.pos.X > 0)
                    direction.X = -1;
                if (controls.isPressed(Keys.Up, Buttons.DPadUp) && this.pos.Y > 0)
                    direction.Y = -1;
                if (controls.isPressed(Keys.Down, Buttons.DPadDown) && this.pos.Y < maxY-this.dim.Y)
                    direction.Y = 1;

                if (Math.Abs((int)direction.Y) > 0)
                    if (Math.Abs((int)direction.X) > 0)
                        direction.Normalize();

                Vector2 newX = this.pos;
                Vector2 newY = this.pos;

                newX.X += (int)(direction.X * 6);
                newY.Y += (int)(direction.Y * 6);

                bool updateX, updateY;
                updateX = true;
                updateY = true;

                for (int i = 0; i < obstacles.Count; i++)
                {
                    if (((Water)obstacles[i]).intersects(newX, this.dim))
                        updateX = false;
                    if (((Water)obstacles[i]).intersects(newY, this.dim))
                        updateY = false;
                }

                if (updateX)
                    this.pos.X += (int)(direction.X * 6);
                if (updateY)
                    this.pos.Y += (int)(direction.Y * 6);

                if (direction.X > 0)
                    flipped = true;
                else
                    flipped = false;
            }
            
        }

    }

}
