using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;


namespace EndangeredSong
{
    class MiniMap
    {
        Texture2D tex;      //Texture2D that is drawn by the spritebatch
        Color[] pixels;     //Array of pixel colors that is loaded into the Texture2D
        int width;          
        int height;
        public MiniMap(int width, int height, GraphicsDevice g)
        {
            this.width = width;
            this.height = height;
            tex = new Texture2D(g, width, height, false, SurfaceFormat.Color);
            pixels = new Color[width * height];
            this.setAllPixels(System.Drawing.Color.DarkGreen);
        }
        public void setAllPixels(System.Drawing.Color c)    //sets all pixels in the map to Color c (background color)
        {
            for (int y = 0; y < this.height; y++)
                for (int x = 0; x < this.width; x++)
                    pixels[(y * this.width) + x] = new Color(c.R, c.G, c.B, c.A);

        }
        public void setPixel(int x, int y, System.Drawing.Color c)  //sets pixel at (x, y) to Color c
        {
            if (x < width && y < height && x >= 0 && y >= 0)
            pixels[(y * this.width) + x] = new Color(c.R, c.G, c.B, c.A);                
        }

        public void Update(GraphicsDevice g, ArrayList hidingPlaces, ArrayList harmonians, ArrayList water, BIOAgent bio, Player player)
        {
            int x1, x2, y1, y2;

            this.setAllPixels(System.Drawing.Color.DarkGreen);

            for(int i = 0; i < hidingPlaces.Count; i++)
            {                
                x1 = (int)((HidingPlace)hidingPlaces[i]).getPosition().X / 20;
                y1 = (int)((HidingPlace)hidingPlaces[i]).getPosition().Y / 20;
                x2 = x1+(int)((HidingPlace)hidingPlaces[i]).getDimension().X / 20;
                y2 = y1+(int)((HidingPlace)hidingPlaces[i]).getDimension().Y / 20;

                for (int x = x1; x < x2; x++)
                    for (int y = y1; y < y2; y++)
                        this.setPixel(x, y, System.Drawing.Color.LightGreen);
            }
            
            for (int i = 0; i < harmonians.Count; i++)
            {
                x1 = (int)((Harmonian)harmonians[i]).getPosition().X / 20;
                y1 = (int)((Harmonian)harmonians[i]).getPosition().Y / 20;
                x2 = x1+(int)((Harmonian)harmonians[i]).getDimension().X / 20;
                y2 = y1+(int)((Harmonian)harmonians[i]).getDimension().Y / 20;
                for (int x = x1; x < x2; x++)
                    for (int y = y1; y < y2; y++)
                        this.setPixel(x, y, System.Drawing.Color.Orange);
            }

            for (int i = 0; i < water.Count; i++)
            {
                x1 = (int)((Water)water[i]).getPosition().X / 20;
                y1 = (int)((Water)water[i]).getPosition().Y / 20;
                x2 = x1 + (int)((Water)water[i]).getDimension().X / 20;
                y2 = y1 + (int)((Water)water[i]).getDimension().Y / 20;
                for (int x = x1; x < x2; x++)
                    for (int y = y1; y < y2; y++)
                        this.setPixel(x, y, System.Drawing.Color.Blue);
            }

            if (bio.isOnScreen())
            {
                x1 = (int)bio.getPosition().X / 20;
                y1 = (int)bio.getPosition().Y / 20;
                x2 = x1 + (int)bio.getDimension().X / 20;
                y2 = y1 + (int)bio.getDimension().Y / 20;
                for (int x = x1; x < x2; x++)
                    for (int y = y1; y < y2; y++)
                        this.setPixel(x, y, System.Drawing.Color.Purple);
            }

            x1 = (int)player.getPosition().X / 20;
            y1 = (int)player.getPosition().Y / 20;
            x2 = x1+(int)player.getDimension().X / 20;
            y2 = y1+(int)player.getDimension().Y / 20;
            for (int x = x1; x < x2; x++)
                for (int y = y1; y < y2; y++)
                    this.setPixel(x, y, System.Drawing.Color.Red);

            tex.SetData<Color>(pixels);     //load Color array data into texture
        }
        public void Draw(SpriteBatch sb, int posX, int posY)
        {           
            sb.Draw(tex, new Rectangle(posX, posY, this.width, this.height), Color.White);
        }
    }
}
