using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace testgame
{
    internal class DrawingMethods
    {
        public static void drawTextInDrawMethod(string stringg, Vector2 position, SpriteBatch _spriteBatch, SpriteFont font)
        {
            //_spriteBatch.Begin();
            _spriteBatch.DrawString(font, stringg, position, Color.White);
            //_spriteBatch.End();
        }

        public static void drawTextOutDrawMethod(string stringg, Vector2 position, SpriteBatch _spriteBatch, SpriteFont font)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, stringg, position, Color.White);
            _spriteBatch.End();
        }

        public static void drawAfterImages(Box box, SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < box.afterimagesPositions.Length; i++)
            {
                float colourMultiplier = ((float)(box.afterimagesPositions.Length - (i + 1)) / (float)(box.afterimagesPositions.Length + 1));
                _spriteBatch.Draw(box.texture, box.afterimagesPositions[i], null, box.colour * colourMultiplier, 0f, new Vector2(box.texture.Width / 2, box.texture.Height / 2), new Vector2(2, 2), SpriteEffects.None, 0f); //draw afterimages
            }
        }

        public static void drawGame(SpriteBatch _spriteBatch, List<Box> boxes, Texture2D boxTexture, GraphicsDeviceManager _graphics, SpriteFont font)
        {
            _spriteBatch.Begin();

            for (int i = 0; i < boxes.Count; i++)
            {
                _spriteBatch.Draw(boxTexture, boxes[i].position, null, boxes[i].colour, 0f, new Vector2(boxTexture.Width / 2, boxTexture.Height / 2), new Vector2(2, 2), SpriteEffects.None, 0f);
                DrawingMethods.drawAfterImages(boxes[i], _spriteBatch);
            }

            drawTextInDrawMethod("press y to fullscreen toggle", new Vector2(100, 100), _spriteBatch, font);
            drawTextInDrawMethod("press enter to spawn a box and backspace to delete one", new Vector2(100, 200), _spriteBatch, font);

            _spriteBatch.End();
        }

        public static void drawTitleScreen(SpriteBatch _spriteBatch, Texture2D titleBackground, Texture2D startTexture, Vector2 startPosition, Texture2D settingsTexture, Vector2 settingsPosition, GraphicsDeviceManager _graphics)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(titleBackground, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), null, Color.White, 0f,
                new Vector2(titleBackground.Width / 2, titleBackground.Height / 2),
                new Vector2((float)_graphics.PreferredBackBufferWidth / (float)titleBackground.Width, (float)_graphics.PreferredBackBufferHeight / (float)titleBackground.Height),
                SpriteEffects.None, 1f);
            _spriteBatch.Draw(startTexture, startPosition, null, Color.White, 0f,
                new Vector2(startTexture.Width / 2, startTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.Draw(settingsTexture, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 350), null, Color.White, 0f,
                new Vector2(settingsTexture.Width / 2, settingsTexture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
    }
}
