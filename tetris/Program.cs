using System;

namespace tetris
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 60;

            Console.BufferHeight = 40;
            Console.BufferWidth = 60;

            Game game = new Game();
            game.RunGame();
        }
    }
}