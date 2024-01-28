using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FirstGame
{
    internal interface ISprite
    {

        Vector2 Position { set; get; }

        // Draw sprite to given SpriteBatch
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
