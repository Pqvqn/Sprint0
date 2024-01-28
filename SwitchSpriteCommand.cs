namespace FirstGame
{
    internal class SwitchSpriteCommand : ICommand
    {

        private int spriteIdx;
        private Game1 game;

        public SwitchSpriteCommand(Game1 game, int spriteIdx) {
            this.game = game;
            this.spriteIdx = spriteIdx;
        }

        public void Execute()
        {
            // Switch game's sprite index
            game.SpriteIdx = spriteIdx;
        }
    }
}
