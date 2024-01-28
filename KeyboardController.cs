using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FirstGame
{
    internal class KeyboardController : IController
    {

        private Dictionary<Keys, ICommand> inputMapping;
        private KeyboardState prevState;

        public KeyboardController(Game1 game)
        {
            ICommand quit = new QuitCommand(game);
            ICommand sprite1 = new SwitchSpriteCommand(game, 0);
            ICommand sprite2 = new SwitchSpriteCommand(game, 1);
            ICommand sprite3 = new SwitchSpriteCommand(game, 2);
            ICommand sprite4 = new SwitchSpriteCommand(game, 3);

            // Mapping of pressed keys to corresponding commands
            inputMapping = new Dictionary<Keys, ICommand>()
            {
                { Keys.NumPad0, quit },
                { Keys.NumPad1, sprite1 },
                { Keys.NumPad2, sprite2 },
                { Keys.NumPad3, sprite3 },
                { Keys.NumPad4, sprite4 },
                { Keys.D0, quit },
                { Keys.D1, sprite1 },
                { Keys.D2, sprite2 },
                { Keys.D3, sprite3 },
                { Keys.D4, sprite4 }
            };
        }

        public void Update()
        {
            KeyboardState currState = Keyboard.GetState();
            Keys[] currKeys = currState.GetPressedKeys();

            // Check each pressed key to see if commands need to be run
            for (int i = 0; i<currKeys.Length; i++)
            {
                Keys key = currKeys[i];
                // Only execute commands when first pressed
                if (prevState.IsKeyUp(key) && inputMapping.ContainsKey(key))
                {
                    inputMapping[key].Execute();
                }
            }

            prevState = currState;

        }
    }
}
