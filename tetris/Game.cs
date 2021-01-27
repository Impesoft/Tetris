using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace tetris
{
    internal class Game
    {
        private Window window;
        private int[,] CurrentBlockStatus { get; set; }
        private Block CurrentBlock { get; set; }
        private Block PreviewBlock { get; set; }
        public bool NewBlock { get; set; } = true;
        public int Score { get; set; }
        public bool GameOver { get; set; }

        public Game()
        {
            window = new Window(12, 22);
        }

        public void RunGame()
        {
            CreateRandomBlock();
            window.ClearPreviewWindow();

            CurrentBlock = PreviewBlock;
            CurrentBlockStatus = CurrentBlock.GetBlockCurrentStatus();

            Thread levelSpeed = new Thread(MoveDown);
            levelSpeed.Start();

            window.SetFrameAroundGrid();

            while (!GameOver)
            {
                CurrentBlock = PreviewBlock;
                CreateRandomBlock();

                while (!NewBlock)
                {
                    CurrentBlockStatus = CurrentBlock.GetBlockCurrentStatus();
                    PutBlockInGrid();
                    PlayerInput();
                    window.ShowPreviewWindow(5, 2, PreviewBlock);
                    window.ShowScore(5, 9, Score);
                    window.ShowMainWindow(5, 10);
                    window.RefreshGrid();
                }
                CheckLineWasMade();
                CheckGameOver();
            }
            char block = '\u2588';
            string line = new string(block, 12);
            string gameOver = block + "GAME  OVER" + block;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(window.Width / 2 - 1, 10 + window.Height / 2 - 1);
            Console.Write(line);
            Console.SetCursorPosition(window.Width / 2 - 1, 10 + window.Height / 2);
            Console.Write(gameOver);
            Console.SetCursorPosition(window.Width / 2 - 1, 10 + window.Height / 2 + 1);
            Console.Write(line);
            Console.ResetColor();
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void CheckGameOver()
        {
            for (int x = 1; x < window.Width - 1; x++)
            {
                if (window.GridStatic[x, 1] > 0)
                {
                    GameOver = true;
                }
            }
        }

        private void CheckLineWasMade()
        {
            //convert grid in list with lines

            List<int[]> linelist = new List<int[]>();

            for (int y = 0; y < window.Height; y++)
            {
                int[] line = new int[window.Width];

                for (int x = 0; x < window.Width; x++)
                {
                    line[x] = window.GridStatic[x, y];
                }
                linelist.Add(line);
            }

            // if line was found, remove from list

            int lineIndex = 0;

            foreach (var line in linelist.ToList())
            {
                int counter = 0;

                for (int x = 0; x < window.Width; x++)
                {
                    if (line[x] > 0)
                    {
                        counter++;
                    }
                }
                if (counter == window.Width && lineIndex != 0 && lineIndex != window.Height - 1)
                {
                    linelist.RemoveAt(lineIndex);
                    Score += 10;

                    //add empty line at index 1
                    linelist.Insert(1, MakeEmptyLine());
                }
                lineIndex++;
            }

            //update grid
            int listIndex = 0;

            foreach (var line in linelist)
            {
                for (int x = 0; x < window.Width; x++)
                {
                    window.Grid[x, listIndex] = line[x];
                }
                listIndex++;
            }

            window.UpdateStaticGrid();
        }

        private int[] MakeEmptyLine()
        {
            int[] emptyLine = new int[window.Width];

            emptyLine[0] = 1;
            emptyLine[window.Width - 1] = 1;

            for (int x = 1; x < window.Width - 1; x++)
            {
                emptyLine[x] = 0;
            }
            return emptyLine;
        }

        public void CreateRandomBlock()
        {
            Random rnd = new Random();
            int blockNr = rnd.Next(6);
            PreviewBlock = new Block(blockNr);
            NewBlock = false;
        }

        public void PutBlockInGrid()
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (CurrentBlockStatus[x, y] > 0)
                    {
                        window.Grid[x + CurrentBlock.PosX, y + CurrentBlock.PosY] = CurrentBlockStatus[x, y];
                    }
                }
            }
        }

        public void PlayerInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey keyPressed = 0;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                switch (keyPressed)
                {
                    case ConsoleKey.UpArrow:

                        CurrentBlock.Turn();
                        CurrentBlockStatus = CurrentBlock.GetBlockCurrentStatus();

                        if (HitSomething())
                        {
                            CurrentBlock.UndoTurn();
                            CurrentBlockStatus = CurrentBlock.GetBlockCurrentStatus();
                        }

                        break;

                    case ConsoleKey.LeftArrow:

                        CurrentBlock.MoveLeft();

                        if (HitSomething())
                        {
                            CurrentBlock.MoveRight();
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        CurrentBlock.MoveRight();

                        if (HitSomething())
                        {
                            CurrentBlock.MoveLeft();
                        }

                        break;

                    case ConsoleKey.DownArrow:
                        CurrentBlock.MoveDown();

                        if (HitSomething())
                        {
                            CurrentBlock.MoveUp();
                        }
                        break;

                    case ConsoleKey.NumPad1:
                        PutBlockInGrid();
                        window.UpdateStaticGrid();
                        NewBlock = true;

                        break;

                    default:
                        break;
                }
            }
        }

        public bool HitSomething()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (CurrentBlockStatus[x, y] > 0 && (window.GridStatic[x + CurrentBlock.PosX, y + CurrentBlock.PosY]) > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void MoveDown()
        {
            while (true)
            {
                CurrentBlock.MoveDown();

                if (HitSomething())
                {
                    CurrentBlock.MoveUp();
                    PutBlockInGrid();
                    window.UpdateStaticGrid();
                    NewBlock = true;
                }
                Thread.Sleep(1000 - Score % 1000);
            }
        }
    }
}