using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace FirstGame
{
    internal class MovingAnimatedSprite : ISprite
    {
        // Drawn sprite position, sum of actual position and movement's offset
        public Vector2 Position {
            get 
            { 
                return anchorPosition + offsetPosition;  
            }
            set
            {
                anchorPosition = value - offsetPosition;
            }
        }

        private Vector2 anchorPosition { get; set; }

        private Vector2 offsetPosition
        {
            get
            {
                return (movement == null) ? Vector2.Zero : movement.OffsetPosition;
            }
        }

        private IMovement movement;
        private IAtlas atlas;

        public Texture2D Texture { get; set; }

        // Number of frames to draw each second
        public float Framerate { get; set; }
       
        // Multiplier for size of drawn image
        private float scale;

        // Time that frame was last switched
        private TimeSpan timeAtLastFrame;


        public MovingAnimatedSprite(Texture2D texture, Vector2 position, IMovement movement, IAtlas atlas, int framerate, float scale)
        {
            Texture = texture;
            Position = position;
            this.movement = movement;
            this.atlas = atlas;
            this.scale = scale;

            Framerate = framerate;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Only move if a movement pattern has been assigned
            if (movement != null)
            {
                // Start movement if it hasn't been started yet
                if (!movement.IsMoving)
                {
                    movement.Start(gameTime);
                }
                // Pass update to movement
                movement.Update(gameTime);
            }

            // Switch frames if needed and based on framerate
            if (atlas.duration() > 0 && Framerate > 0 
                && (gameTime.TotalGameTime - timeAtLastFrame).TotalSeconds > atlas.duration() / Framerate)
            {
                atlas.advance();
                timeAtLastFrame = gameTime.TotalGameTime;
            }

            // Get spritesheet bounds of current frame from atlas
            Rectangle sourceRectangle = atlas.currentRect();
            // Calculate game position to draw sprite at
            Vector2 drawPos = Position - atlas.centerPoint() * scale;
            Rectangle destinationRectangle = new Rectangle((int)(drawPos.X), (int)(drawPos.Y), (int)(scale * sourceRectangle.Width), (int)(scale * sourceRectangle.Height));

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
