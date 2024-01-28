using Microsoft.Xna.Framework;
using System;

namespace FirstGame
{
    internal class PaceMovement : IMovement
    {

        public Vector2 OffsetPosition { get; private set; }

        private TimeSpan began; // Time when movement was started
        private TimeSpan duration; // Length of time to traverse the distance one-way
        private Vector2 distance; // Furthest position to walk to relative to center

        public bool IsMoving { get; private set; }

        public PaceMovement(Vector2 paceDistance, TimeSpan paceDuration) {
            this.distance = paceDistance;
            this.duration = paceDuration;
            IsMoving = false;
        }

        public void Update(GameTime gameTime)
        {
            if (IsMoving)
            {
                // Calculate how far through the cycle to be
                float cycle = (float)((gameTime.TotalGameTime - began) / duration);
                float spot = (cycle % 2);
                // Walk backwards every other duration
                if (spot > 1)
                    spot = 2 - spot;
                // Set position to follow linear path
                OffsetPosition = distance * spot;
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
