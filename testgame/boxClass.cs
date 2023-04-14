using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace testgame
{
    public class Box
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public Color colour;
        public Vector2[] afterimagesPositions = new Vector2[15];

        public Box(Vector2 boxSpeed, Vector2 boxPosition, Texture2D boxTexture, Color boxColour)
        { 
            velocity = boxSpeed;
            position = boxPosition;
            texture = boxTexture;
            colour = boxColour;
        }
    }
}
