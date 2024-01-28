using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont font;
        private IController keyboard;
        private IController mouse;

        private ISprite[] sprites = new ISprite[] { null, null, null, null };
        internal int SpriteIdx { get; set; } // Index of currently displayed sprite in sprites array
        internal ISprite DispSprite {
            get
            {
                return sprites[SpriteIdx];
            }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Register controllers
            keyboard = new KeyboardController(this);
            mouse = new MouseController(_graphics.GraphicsDevice.Viewport, this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Credits");

            Texture2D texture = Content.Load<Texture2D>("SerJunkan");

            // Sprite that neither moves nor animates
            IAtlas sitAnim = new SingleAtlas(new Rectangle(22, 38, 12, 14), new Vector2(8, 9));
            ISprite sitSprite = new MovingAnimatedSprite(texture, new Vector2(400, 300), null, sitAnim, 0, 5);

            // Sprite that animates, but does not move
            Rectangle[] swingAnimRects = { new Rectangle(261, 435, 19, 22), new Rectangle(282, 434, 15, 23), new Rectangle(299, 435, 14, 22), new Rectangle(315, 435, 14, 22), new Rectangle(331, 435, 14, 22),
                new Rectangle(347, 434, 14, 23), new Rectangle(363, 434, 14, 23), new Rectangle(379, 442, 37, 17), new Rectangle(418, 439, 37, 20), new Rectangle(457, 440, 25, 18), new Rectangle(484, 440, 18, 18) };
            Vector2[] swingAnimCenters = { Vector2.Zero, new Vector2(-1, 1), new Vector2(-1, 0), new Vector2(-1, 0), new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(-1, 1), 
                new Vector2(10, -8), new Vector2(10, -5), new Vector2(1, -4), new Vector2(-1, -5)};
            float[] swingAnimDurs = { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f};
            IAtlas swingAnim = new ManualAtlas(swingAnimRects, swingAnimCenters, swingAnimDurs);
            ISprite swingSprite = new MovingAnimatedSprite(texture, new Vector2(345, 225), null, swingAnim, 10, 5);

            // Sprite that moves, but does not animate
            IMovement flyMove = new FlyMovement(new Vector2(200, 100), TimeSpan.FromSeconds(2.5));
            IAtlas flyAnim = new SingleAtlas(new Rectangle(291, 562, 24, 21), new Vector2(12, 14));
            ISprite flySprite = new MovingAnimatedSprite(texture, new Vector2(400, 300), flyMove, flyAnim, 0, 5);

            // Sprite that moves and animates
            IMovement walkMove = new PaceMovement(new Vector2(300, 0), TimeSpan.FromSeconds(1));
            IAtlas walkAnim = new AutoAtlas(new Rectangle(173, 185, 78, 19), 1, 4, 2);
            ISprite walkSprite = new MovingAnimatedSprite(texture, new Vector2(200, 245), walkMove, walkAnim, 10, 5);

            sprites = new ISprite[] { sitSprite, swingSprite, flySprite, walkSprite };            
            SpriteIdx = 0;
        }

        protected override void Update(GameTime gameTime)
        {
            // Pass updates to controllers
            keyboard.Update();
            mouse.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Draw credit text
            _spriteBatch.DrawString(font, "CREDITS\nProgram By: Pavan Rauch\nSprites From: https://www.spriters-resource.com/pc_computer/enterthegungeon/sheet/158552/", new Vector2(50, 20), Color.White);

            // Draw current sprite
            if (DispSprite != null)
                DispSprite.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
