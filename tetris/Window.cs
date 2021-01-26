using System;
using System.Collections.Generic;
using System.Text;

namespace tetris
{
    internal class Window
    {
        public bool[,] Grid { get; set; }
        public bool[,] GridStatic { get; set; }
        public bool[,] PreviewGrid { get; set; }
        public int Height { get; }
        public int Width { get; }

        public Window(int height, int width)
        {
            Grid = new bool[height, width];
            GridStatic = new bool[height, width];
            PreviewGrid = new bool[7, 12];
            Height = height;
            Width = width;
        }

        public void ShowScore(int originX, int originY, int score)
        {
            Console.SetCursorPosition(originX, originY);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"SCORE: {score}");
            Console.ResetColor();
        }

        public void ShowMainWindow(int originX, int originY)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            // string block = "\u2585";
            string block = "\u2588";

            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(originX, originY);
                for (int j = 0; j < Width; j++)
                {
                    DrawGridBlock(block, i, j);
                }
                originY++;
            }
            Console.CursorVisible = false;
        }

        private void DrawGridBlock(string block, int i, int j)
        {
            if (Grid[i, j])
            {
                Console.Write(block);
            }
            else
            {
                Console.Write(" ");
            }
        }

        public void ShowPreviewWindow(int originX, int originY, Block prevBlock)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string block = "\u2588"; //2585

            bool[,] previewBlock = prevBlock.GetBlockCurrentStatus();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PreviewGrid[i + 1, j + 4] = previewBlock[i, j];
                }
            }

            for (int i = 0; i < 7; i++)
            {
                Console.SetCursorPosition(originX, originY);
                for (int j = 0; j < 12; j++)
                {
                    if (PreviewGrid[i, j])
                    {
                        Console.Write(block);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                originY++;
            }
            Console.CursorVisible = false;
        }

        public void ClearPreviewWindow()
        {
            for (int i = 0; i < 12; i++)
            {
                PreviewGrid[0, i] = true;
            }
            for (int i = 1; i < 7 - 1; i++)
            {
                PreviewGrid[i, 0] = true;
                PreviewGrid[i, 11] = true;
            }

            for (int i = 0; i < 12; i++)
            {
                PreviewGrid[6, i] = true;
            }
        }

        public void SetFrameAroundGrid()
        {
            for (int i = 0; i < Width; i++)
            {
                GridStatic[0, i] = true;
            }
            for (int i = 1; i < Height - 1; i++)
            {
                GridStatic[i, 0] = true;
                GridStatic[i, Width - 1] = true;
            }

            for (int i = 0; i < Width; i++)
            {
                GridStatic[Height - 1, i] = true;
            }
        }

        public void RefreshGrid()
        {
            Grid = (bool[,])GridStatic.Clone();
        }

        public void UpdateStaticGrid()
        {
            GridStatic = (bool[,])Grid.Clone();
        }
    }
}