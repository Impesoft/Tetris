namespace tetris
{
    public class ArrayOfBlocks
    {
        public int[,] Line, Cube, LShape, LShape2, TShape, SShape, SShape2;
        private const int blockcount = 7;
        private const int rotationsteps = 4;
        private int[][,] arrayOfBlocks = new int[7][,];

        public ArrayOfBlocks()
        {
            arrayOfBlocks[0] = new int[,] {
                {0,1,0,0},
                {0,1,0,0},
                {0,1,0,0},
                {0,1,0,0} };
            arrayOfBlocks[1] = new int[,] {
                {0,0,0,0 },
                {0,1,1,0},
                {0,1,1,0},
                {0,0,0,0} };
            arrayOfBlocks[2] = new int[,] {
                {0,0,0,0},
                {0,1,0,0},
                {0,1,0,0},
                {0,1,1,0} };
            arrayOfBlocks[3] = new int[,] {
                {0,0,0,0},
                {0,0,1,0},
                {0,0,1,0},
                {0,1,1,0}};
            arrayOfBlocks[4] = new int[,] {
                {0,0,0,0},
                {0,0,0,0},
                {0,0,1,0},
                {0,1,1,1}};
            arrayOfBlocks[5] = new int[,] {
                {0,0,0,0},
                {0,0,1,0},
                {0,1,1,0},
                {0,1,0,0}};
            arrayOfBlocks[6] = new int[,] {
                {0,0,0,0},
                {0,1,0,0},
                {0,1,1,0},
                {0,0,1,0}};
        }

        public bool[,][,] Blocks = new bool[blockcount, rotationsteps][,];

        public bool[,] CreateBlockArray(int blocknr)
        {
            int blocknummer = 0;
            foreach (int[,] block in arrayOfBlocks)
            {
                bool[,] boolBlock = ConvertToBool(block);

                Blocks[blocknummer, 0] = boolBlock;
                Blocks[blocknummer, 1] = RotateMatrix(boolBlock, 1);
                Blocks[blocknummer, 2] = RotateMatrix(boolBlock, 2);
                Blocks[blocknummer, 3] = RotateMatrix(boolBlock, 3);
                blocknummer++;
            }
            return Blocks[blocknr, 0];
        }

        private bool[,] ConvertToBool(int[,] block)
        {
            bool[,] ret = new bool[4, 4];

            for (int i = 0; i <= 3; ++i)
            {
                for (int j = 0; j <= 3; ++j)
                {
                    if (block[i, j] == 1)
                    {
                        ret[i, j] = true;
                    }
                    else
                    {
                        ret[i, j] = false;
                    }
                }
            }

            return ret;
        }

        private bool[,] RotateMatrix(bool[,] matrix, int numberOfRotations)
        {
            for (int rotation = 0; rotation < numberOfRotations; rotation++)
            {
                bool[,] ret = new bool[4, 4];

                for (int i = 0; i <= 3; ++i)
                {
                    for (int j = 0; j <= 3; ++j)
                    {
                        ret[i, j] = matrix[j, i];
                    }
                }
                matrix = ret;
            }
            return matrix;
        }
    }
}