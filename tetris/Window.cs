using System;

namespace tetris
{
    internal class Window
    {
        public int[,] Grid { get; set; }
        public int[,] GridStatic { get; set; }
        public int[,] PreviewGrid { get; set; }
        public int Height { get; }
        public int Width { get; }

        public Window(int height, int width)
        {
            Grid = new int[height, width];
            GridStatic = new int[height, width];
            PreviewGrid = new int[7, 12];
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
            int kleur = Grid[i, j];
            switch (kleur)
            {
                case 0:
                    Console.Write(" ");
                    break;

                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    Console.ForegroundColor = colors.Colors[kleur - 2];
                    Console.Write(block);
                    Console.ResetColor();
                    break;

                default:
                    Console.Write(block);
                    break;
            }
        }

        public void ShowPreviewWindow(int originX, int originY, Block prevBlock)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string block = "\u2588"; //2585

            int[,] previewBlock = prevBlock.GetBlockCurrentStatus();

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
                    if (PreviewGrid[i, j] > 0)
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
                PreviewGrid[0, i] = 1;
            }
            for (int i = 1; i < 7 - 1; i++)
            {
                PreviewGrid[i, 0] = 1;
                PreviewGrid[i, 11] = 1;
            }

            for (int i = 0; i < 12; i++)
            {
                PreviewGrid[6, i] = 1;
            }
        }

        public void SetFrameAroundGrid()
        {
            for (int i = 0; i < Width; i++)
            {
                GridStatic[0, i] = 1;
            }
            for (int i = 1; i < Height - 1; i++)
            {
                GridStatic[i, 0] = 1;
                GridStatic[i, Width - 1] = 1;
            }

            for (int i = 0; i < Width; i++)
            {
                GridStatic[Height - 1, i] = 1;
            }
        }

        public void RefreshGrid()
        {
            Grid = (int[,])GridStatic.Clone();
        }

        public void UpdateStaticGrid()
        {
            GridStatic = (int[,])Grid.Clone();
        }
    }
}