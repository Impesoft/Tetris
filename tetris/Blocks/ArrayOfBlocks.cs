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
                {0,2,0,0},
                {0,2,0,0},
                {0,2,0,0},
                {0,2,0,0} };
            arrayOfBlocks[1] = new int[,] {
                {0,0,0,0 },
                {0,3,3,0},
                {0,3,3,0},
                {0,0,0,0} };
            arrayOfBlocks[2] = new int[,] {
                {0,0,0,0},
                {0,4,0,0},
                {0,4,0,0},
                {0,4,4,0} };
            arrayOfBlocks[3] = new int[,] {
                {0,0,0,0},
                {0,0,5,0},
                {0,0,5,0},
                {0,5,5,0}};
            arrayOfBlocks[4] = new int[,] {
                {0,0,0,0},
                {0,0,0,0},
                {0,0,6,0},
                {0,6,6,6}};
            arrayOfBlocks[5] = new int[,] {
                {0,0,0,0},
                {0,0,7,0},
                {0,7,7,0},
                {0,7,0,0}};
            arrayOfBlocks[6] = new int[,] {
                {0,0,0,0},
                {0,8,0,0},
                {0,8,8,0},
                {0,0,8,0}};
        }

        public int[,][,] Blocks = new int[blockcount, rotationsteps][,];

        public int[,] CreateBlockArray(int blocknr)
        {
            int blocknummer = 0;
            foreach (int[,] block in arrayOfBlocks)
            {
                //  int[,] boolBlock = ConvertToBool(block);

                Blocks[blocknummer, 0] = block;
                Blocks[blocknummer, 1] = RotateMatrix(block, 1);
                Blocks[blocknummer, 2] = RotateMatrix(block, 2);
                Blocks[blocknummer, 3] = RotateMatrix(block, 3);
                blocknummer++;
            }
            return Blocks[blocknr, 0];
        }

        private int[,] ConvertToBool(int[,] block)
        {
            //int[,] ret = new int[4, 4];

            //for (int i = 0; i <= 3; ++i)
            //{
            //    for (int j = 0; j <= 3; ++j)
            //    {
            //        if (block[i, j] == 1)
            //        {
            //            ret[i, j] = 1;
            //        }
            //        else
            //        {
            //            ret[i, j] = 0;
            //        }
            //    }
            //}

            //return ret;
            return block;
        }

        private int[,] RotateMatrix(int[,] matrix, int numberOfRotations)
        {
            for (int rotation = 0; rotation < numberOfRotations; rotation++)
            {
                int[,] ret = new int[4, 4];

                for (int i = 0; i < 4; ++i)
                {
                    for (int j = 0; j < 4; ++j)
                    {
                        ret[i, j] = matrix[3 - j, i];
                    }
                }
                matrix = ret;
            }
            return matrix;
        }
    }
}