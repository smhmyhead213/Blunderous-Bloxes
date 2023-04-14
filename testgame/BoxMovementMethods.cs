using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace testgame
{
    internal class BoxMovementMethods
    {
        public static void handleSpaceBarBoost(KeyboardState kstate, List<Box> boxes, int i)
        {
            int spaceTimer = 0;
            int spaceTimerCooldown = 30;
            int spaceBoost = 50;

            if (spaceTimer > 0)
                spaceTimer--;

            if (kstate.IsKeyDown(Keys.Space) && spaceTimer == 0)
            {
                
                spaceTimer = spaceTimerCooldown;
                if (boxes[i].velocity.Y > 0) //if falling
                    boxes[i].velocity.Y = 0;

                boxes[i].velocity.Y = boxes[i].velocity.Y - spaceBoost; //launch UPWARDS as up is the negative direction
            }
            
        }

        public static bool touchingBottom(Vector2 position, int height, int screenHeight)
        {
            if (position.Y + height >= screenHeight)
                return true;
            else return false;
            //if at the bottom
        }

        public static bool touchingTop(Vector2 position, int height)
        {
            if (position.Y + height <= 0)
                return true;
            else return false;
        }

        public static bool touchingRight(Vector2 position, int width, int screenWidth)
        {
            if (position.X + width >= screenWidth)
                return true;
            else return false;
        }

        public static bool touchingLeft(Vector2 position, int width)
        {
            if (position.X - width <= 0)
                return true;
            else return false;
        }

        public static void handleBoxMovement(List<Box> boxes, int i, GraphicsDeviceManager _graphics, GameTime gameTime, float g)
        {
            boxes[i].afterimagesPositions = Methods.moveVectorArrayElementsUpAndAddToStart(boxes[i].afterimagesPositions, boxes[i].position);






            boxes[i].velocity.Y = boxes[i].velocity.Y + g; //vertical acceleration

            boxes[i].position = boxes[i].position + boxes[i].velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //airResistance = new Vector2(0.5f * rho * dragCoefficient * area * boxSpeed.X * boxSpeed.X, 0.5f * rho * dragCoefficient * area * boxSpeed.Y * boxSpeed.Y);

            if (touchingBottom(boxes[i].position, boxes[i].texture.Height, _graphics.PreferredBackBufferHeight)) //if at the bottom
            {
                boxes[i].velocity.Y = boxes[i].velocity.Y * -1f;
                boxes[i].position.Y = _graphics.PreferredBackBufferHeight - (boxes[i].texture.Height + 0.1f);
            }
            if (touchingTop(boxes[i].position, boxes[i].texture.Height)) //if at top
            {
                boxes[i].velocity.Y = boxes[i].velocity.Y * -0.9f;
                boxes[i].position.Y = 0 - (boxes[i].texture.Height + 0.1f);
            }

            if (touchingRight(boxes[i].position, boxes[i].texture.Width, _graphics.PreferredBackBufferWidth)) //if at right
                boxes[i].velocity.X = boxes[i].velocity.X * -1f;

            if (touchingLeft(boxes[i].position, boxes[i].texture.Width)) //if at left
                boxes[i].velocity.X = boxes[i].velocity.X * -1f;



            for (int j = i + 1; j < boxes.Count; j++)
            {
                if (areBoxesColliding(boxes[i], boxes[j]))
                    handleCollsionRebounds(ref boxes[i].velocity, ref boxes[j].velocity, boxes[i].position, boxes[j].position);
            }
        }

        public static double distanceBetweenBoxes(Vector2 boxOnePosition, Vector2 boxTwoPosition)
        {
            return Math.Sqrt(Math.Pow((boxOnePosition.X - boxTwoPosition.X), 2) + Math.Pow((boxOnePosition.Y - boxTwoPosition.Y), 2));
        }
        public static bool areBoxesColliding(Box box1, Box box2)
        {
            float totalwidth = box1.texture.Width + box2.texture.Width;

            if (Math.Abs(box1.position.X - box2.position.X) <= totalwidth && Math.Abs(box1.position.Y - box2.position.Y) <= totalwidth)
                return true;

            return false;
        }

        public static void handleCollsionRebounds(ref Vector2 velocity1, ref Vector2 velocity2, Vector2 position1, Vector2 position2)
        {
            Vector2 velocityOfBoxOne = velocity1;
            Vector2 velocityOfBoxTwo = velocity2;

            velocity1 = velocityOfBoxTwo + (position1 - position2); //take main movement compnent and add movement along collision axis to prevent sticking
            velocity2 = velocityOfBoxOne + -(position1 - position2);


        }

    }

}

