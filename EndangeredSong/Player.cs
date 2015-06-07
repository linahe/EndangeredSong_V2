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

        bool isDead;
        
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

            this.isDead = false;
	    }
       
        public Vector2 getPosition()
        {
            return this.pos;
            //return this.pos + (this.dim / 2);
        }

        public Vector2 getDimension()
        {
            return this.dim;
        }
        public int getNumFound()
        {
            return this.numFound;
        }
        public Vector2 getFollowingPosition(int x)
        {
            return new Vector2(followingPositions[x, 0], followingPositions[x, 1]);
        }
        public Rectangle getRect()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y);
        }
        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("Harmonian.png");
        }
        public bool isHidden()
        {
            return this.isHid;
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
            if(!this.isHid)
                sb.Draw(image, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
        }

        public void Update(Controls controls, GameTime gameTime)
        {
            Move(controls);
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
                    if (((Harmonian)harmonians[i]).getFound() && ((Harmonian)harmonians[i]).getHid() 
                                                              && h.getRect().Intersects(((Harmonian)harmonians[i]).getRect()))
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
                    if (((Harmonian)harmonians[i]).getFound() && !((Harmonian)harmonians[i]).getHid()
                                                              && h.getRect().Intersects(((Harmonian)harmonians[i]).getRect()))
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

        public void Die()
        {
            this.isDead = true;
        }

        public void Move(Controls controls)
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

            this.pos.X += (int)(direction.X * 6);
            this.pos.Y += (int)(direction.Y * 6);
            }


        }
       
    }

}
