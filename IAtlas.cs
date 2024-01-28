using Microsoft.Xna.Framework;

namespace FirstGame
{
    internal interface IAtlas
    {
        // Bounds of current frame on spritesheet
        public Rectangle currentRect();

        // Position with rectangle to treat as center for drawing
        public Vector2 centerPoint();

        // Multiplier for amount of time to linger on this frame
        public float duration();

        // Continue to the next frame in the atlas
        public void advance();
    }
}
