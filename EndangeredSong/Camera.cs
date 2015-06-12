using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndangeredSong
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        public Vector2 center;
        int mapX;
        int mapY;
        public Camera(Viewport newView)
        {
            view = newView;
            mapX = 4000;
            mapY = 3000;
        }

        public void Update(GameTime gameTime, Player player, int screenWidth, int screenHeight)
        {

            center = new Vector2();

            if ((player.getPosition().X + player.getDimension().X / 2) + 20  >= screenWidth / 2 && player.getPosition().X + player.getDimension().X / 2 <= mapX - screenWidth / 2)
            {
                center.X = player.getPosition().X + (player.getDimension().X / 2) - (screenWidth / 2) ;
            }

            else if (((player.getPosition().X + player.getDimension().X /2) + 20 ) < screenWidth / 2) //+ player.getDimension().X / 2
            {
                center.X = -20;
            }

            else 
            {
                center.X = mapX - screenWidth;
            }


            if (player.getPosition().Y + player.getDimension().Y / 2 + 20 >= screenHeight / 2 && player.getPosition().Y + player.getDimension().Y/2 <= mapY - screenHeight / 2)
            {
                center.Y = player.getPosition().Y + (player.getDimension().Y / 2) - (screenHeight / 2);
            }

            else if (player.getPosition().Y + player.getDimension().Y / 2 + 20 < screenHeight / 2)
            {
                center.Y = -20;
            }

            else
            {
                center.Y = mapY - screenHeight;
            }
//            center.Y = player.getPosition().Y + (player.getDimension().Y / 2) - (screenHeight / 2);
//            center = new Vector2(player.getPosition().X + (player.getDimension().X / 2) - (screenWidth / 2), player.getPosition().Y + (player.getDimension().Y / 2) - (screenHeight / 2));
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }



        public void Update(GameTime gameTime, ScreenManager menu, int screenWidth, int screenHeight)
        {
            center = new Vector2(menu.getPosition().X + (menu.getDimension().X / 2) - (screenWidth / 2), menu.getPosition().Y + (menu.getDimension().Y / 2) - (screenHeight / 2));
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}
