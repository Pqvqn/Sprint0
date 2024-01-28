using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;

namespace FirstGame
{
    internal class MouseController : IController
    {
        enum Quadrants
        {
            Outside, Q1, Q2, Q3, Q4
        }

        private Viewport viewport;

        private Dictionary<Quadrants, ICommand> inputMapping;
        private MouseState prevState;

        private ICommand rightClickCommand;

        public MouseController(Viewport vp, Game1 game)
        {
            viewport = vp;
            ICommand sprite1 = new SwitchSpriteCommand(game, 0);
            ICommand sprite2 = new SwitchSpriteCommand(game, 1);
            ICommand sprite3 = new SwitchSpriteCommand(game, 2);
            ICommand sprite4 = new SwitchSpriteCommand(game, 3);

            // Mapping of left-clicked areas to corresponding commands
            inputMapping = new Dictionary<Quadrants, ICommand>()
            {
                { Quadrants.Q1, sprite1 },
                { Quadrants.Q2, sprite2 },
                { Quadrants.Q3, sprite3 },
                { Quadrants.Q4, sprite4 }

            };
            // Quit on right click
            rightClickCommand = new QuitCommand(game);
        }

        public void Update()
        {
            MouseState currState = Mouse.GetState();
            // Register left clicks only when button first pressed
            if (currState.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
            {
                // Execute command if valid quadrant clicked
                Quadrants q = GetQuadrant(viewport, currState.X, currState.Y);
                if (inputMapping.ContainsKey(q))
                {
                    inputMapping[q].Execute();
                }
            }
            // Handle right clicks
            else if (currState.RightButton == ButtonState.Pressed)
            {
                rightClickCommand.Execute();
            }
            prevState = currState;
        }

        private static Quadrants GetQuadrant(Viewport vp, int x, int y)
        {
            // Register clicks outside of game window
            if (x < 0 || y < 0 || x > vp.Bounds.Width || y > vp.Bounds.Height)
            {
                return Quadrants.Outside;
            }

            bool isLeft = x <= vp.Bounds.Width / 2;
            bool isTop = y <= vp.Bounds.Height / 2;

            // Translate clicked location into a known clickable quadrant
            if (isLeft && isTop)
            {
                return Quadrants.Q1;
            }
            else if (!isLeft && isTop)
            {
                return Quadrants.Q2;
            }
            else if (isLeft && !isTop)
            {
                return Quadrants.Q3;
            }
            else
            {
                return Quadrants.Q4;
            }
        }
    }
}
