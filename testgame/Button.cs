using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;


namespace testgame
{
    public class Button
    {
        public Vector2 position;
        
        public Texture2D texture;

        public Rectangle rectangle => new((int)position.X - (texture.Width / 2), (int)position.Y - (texture.Height / 2), texture.Width, texture.Height);



    }
}
