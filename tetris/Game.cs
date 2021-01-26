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
            window = new Window(22, 12);
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
            Console.Clear();
            Console.WriteLine("GAME OVER");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void CheckGameOver()
        {
            for (int i = 1; i < window.Width - 1; i++)
            {
                if (window.GridStatic[1, i] > 0)
                {
                    GameOver = true;
                }
            }
        }

        private void CheckLineWasMade()
        {
            //convert grid in list with lines

            List<int[]> linelist = new List<int[]>();

            for (int i = 0; i < window.Height; i++)
            {
                int[] line = new int[window.Width];

                for (int j = 0; j < window.Width; j++)
                {
                    line[j] = window.GridStatic[i, j];
                }
                linelist.Add(line);
            }

            // if line was found, remove from list

            int lineIndex = 0;

            foreach (var line in linelist.ToList())
            {
                int counter = 0;

                for (int i = 0; i < window.Width; i++)
                {
                    if (line[i] > 0)
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
                for (int i = 0; i < window.Width; i++)
                {
                    window.Grid[listIndex, i] = line[i];
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

            for (int i = 1; i < window.Width - 1; i++)
            {
                emptyLine[i] = 0;
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
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (CurrentBlockStatus[i, j] > 0)
                    {
                        window.Grid[i + CurrentBlock.PosY, j + CurrentBlock.PosX] = CurrentBlockStatus[i, j];
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
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (CurrentBlockStatus[i, j] > 0 && (window.GridStatic[i + CurrentBlock.PosY, j + CurrentBlock.PosX]) > 0)
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
                Thread.Sleep(1000);
            }
        }
    }
}