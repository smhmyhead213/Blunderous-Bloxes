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
    internal class Methods
    {
        public static void HandleScreenStateChanges(KeyboardState kstate, GraphicsDeviceManager _graphics, List<Box> boxes, int screenModeSwitchCooldown, GameState gameState)
        {
            bool isFullscreenTransitioning = false;

            if (kstate.IsKeyDown(Keys.Y) && screenModeSwitchCooldown == 0 && isFullscreenTransitioning == false)
            {
                if (_graphics.IsFullScreen)
                {
                    _graphics.IsFullScreen = false; //go back to windowed

                    if (gameState.state == GameState.GameStates.InGame)
                        adjustBoxPositionsGoingBackToWindowed(ref boxes);
                    

                    _graphics.PreferredBackBufferWidth = 1200;
                    _graphics.PreferredBackBufferHeight = 675;



                }
                else
                {
                    _graphics.IsFullScreen = true; // going to fullscreen

                    _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

                }

                isFullscreenTransitioning = true;

                _graphics.ApplyChanges();

                isFullscreenTransitioning = false;

                screenModeSwitchCooldown = 60;
            }
        }
        

        public static Vector2[] moveVectorArrayElementsUpAndAddToStart(Vector2[] array, Vector2 newFirstPosition)
        {
            Vector2[] newArray = new Vector2[array.Length];
            newArray[0] = newFirstPosition;

            for (int i = 1; i < array.Length; i++)
            {
                newArray[i] = array[i - 1];
            }

            return newArray;
        }

        

        public static Color generateRandomColour()
        {
            Random random = new Random();

            return new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

        }

        public static void adjustBoxPositionsGoingBackToWindowed(ref List<Box> boxes)
        {
            foreach (Box box in boxes)
            {
                box.position = new Vector2(box.position.X / 1.6f, box.position.Y / 1.6f);
            }
        }
    }
}
