using Microsoft.Xna.Framework;

namespace FirstGame
{
    internal class ManualAtlas : IAtlas
    {

        private Rectangle[] rects;
        private Vector2[] centers;
        private float[] durs;
        private int frame;


        public ManualAtlas(Rectangle[] rectangles, Vector2[] centerPoints, float[] durations){
            this.rects = rectangles;
            this.centers = centerPoints;
            this.durs = durations;
            frame = 0;
        }

        public Rectangle currentRect()
        {
            return rects[frame];
        }

        public Vector2 centerPoint()
        {
            return centers[frame];
        }
        public float duration()
        {
            return durs[frame];
        }

        public void advance()
        {
            frame = (frame + 1) % rects.Length;
        }
    }
}
