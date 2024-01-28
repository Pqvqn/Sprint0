using Microsoft.Xna.Framework;

namespace FirstGame
{
    internal interface IMovement
    {
        // Vector between moved body's true position and drawn position
        public Vector2 OffsetPosition { get; }

        // True if offset is being updated
        public bool IsMoving { get; }

        // Update position based on time since last game cycle
        public void Update(GameTime gameTime);

        // Begin or restart movement cycle
        public void Start(GameTime gameTime);

    }
}
