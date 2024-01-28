using Microsoft.Xna.Framework;
namespace FirstGame
{
    internal class AutoAtlas : IAtlas
    {
        private Rectangle sheetArea;
        private int rows;
        private int cols;
        private int padding;

        private int frame;
        private int frameCount;

        public AutoAtlas(Rectangle sheetArea, int rows, int cols, int padding)
        {
            this.sheetArea = sheetArea;
            this.rows = rows;
            this.cols = cols;
            this.padding = padding;

            frame = 0;
            frameCount = rows * cols;
        }

        public Rectangle currentRect()
        {
            // Calculate the row and column indices of the current frame
            int paddedWidth = (sheetArea.Width + padding) / cols;
            int paddedHeight = (sheetArea.Height + padding) / rows;
            int row = frame / cols;
            int column = frame % cols;

            // Calculate bounds of the frame as Rectangle
            Rectangle sourceRectangle = new Rectangle(sheetArea.X + paddedWidth * column, sheetArea.Y + paddedHeight * row, paddedWidth - padding, paddedHeight - padding);
            return sourceRectangle;
        }

        public Vector2 centerPoint()
        {
            // Centerpoint is top-left corner
            return Vector2.Zero;
        }

        public float duration()
        {
            // Frames have constant timing
            return 1.0f;
        }

        public void advance()
        {
            // Increment once linearly through frames
            frame++;
            if (frame >= frameCount)
                frame = 0;
        }
    }
}
