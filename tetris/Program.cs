using System;

namespace tetris
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 100;

            Console.BufferHeight = 40;
            Console.BufferWidth = 100;

            Game game = new Game();
            game.RunGame();
        }
    }
}