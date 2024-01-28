using Microsoft.Xna.Framework;
using System;

namespace FirstGame
{
    internal class FlyMovement : IMovement
    {
        public Vector2 OffsetPosition { get; private set; }

        private TimeSpan began; // Time when movement was started
        private TimeSpan duration; // Length of time for a full cycle
        private Vector2 distance; // Maximum x and y distances from center

        public bool IsMoving { get; private set; }

        public FlyMovement(Vector2 distance, TimeSpan duration)
        { 
            this.distance = distance;
            this.duration = duration;
            IsMoving = false;
        }

        public void Update(GameTime gameTime)
        {
            if (IsMoving)
            {
                // Calculate how far through the cycle to be
                float cycle = (float)((gameTime.TotalGameTime - began) / duration);
                float spot = (cycle % 2);
                // Set position to follow figure 8
                OffsetPosition = new Vector2((float)Math.Cos(2 * Math.PI * spot) * distance.X, (float)Math.Sin(4 * Math.PI * spot) * distance.Y);
            }
        }

        public void Start(GameTime gameTime)
        {
            began = gameTime.TotalGameTime;
            OffsetPosition = Vector2.Zero;
            IsMoving = true;
        }

    }
}
