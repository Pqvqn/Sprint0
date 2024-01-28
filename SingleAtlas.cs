using Microsoft.Xna.Framework;

namespace FirstGame
{
    internal class SingleAtlas : IAtlas
    {

        private Vector2 center;
        private Rectangle rect;

        public  SingleAtlas(Rectangle rectangle, Vector2 centerPoint) {
            this.rect = rectangle;
            this.center = centerPoint;
        }

        public void advance() 
        {
            // Ignore request to change frame
            return;
        }

        public Vector2 centerPoint()
        {
            return center;
        }

        public Rectangle currentRect()
        {
            return rect;
        }

        public float duration()
        {
            // Return zero, as static sprites have no timing
            return 0;
        }
    }
}
