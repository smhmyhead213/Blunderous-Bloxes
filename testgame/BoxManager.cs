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
    public class BoxManager
    {
        public static void addBox(List<Box> boxes, GraphicsDeviceManager _graphics, ref int boxCreationCooldown, Texture2D boxTexture)
        {
                boxes.Add(new Box(new Vector2(100, 0), new Vector2(_graphics.PreferredBackBufferWidth / 2, 40), boxTexture, Methods.generateRandomColour()));
                boxCreationCooldown = 5;
        }

        public static void deleteBox(List<Box> boxes, ref int boxDeletionCooldown)
        {
            boxes.Remove(boxes[boxes.Count - 1]);
            boxDeletionCooldown = 5;
        }

        public static void initiallyCreateBoxes(int numberOfBoxes, List<Box> boxes, GraphicsDeviceManager _graphics, Texture2D texture)
        {
            for (int i = 1; i < numberOfBoxes + 1; i++)
                boxes.Add(new Box(new Vector2(100, 0),
                new Vector2(_graphics.PreferredBackBufferWidth * i / (numberOfBoxes + 1),
                _graphics.PreferredBackBufferHeight * i / (numberOfBoxes + 1)), texture, Methods.generateRandomColour()));
        }
    }
}
