namespace tetris
{
    public class Block
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int BlockNr { get; }

        protected ArrayOfBlocks Blocks { get; set; } = new ArrayOfBlocks();

        public Block(int blockNr)
        {
            BlockNr = blockNr;
            Blocks.CreateBlockArray(blockNr);
            PosX = 1;
            PosY = 1;
        }

        private int _rotationIndex;

        protected int RotationIndex
        {
            get { return _rotationIndex; }
            set
            {
                _rotationIndex = value % 4;
            }
        }

        public void Turn()
        {
            RotationIndex += 3;
        }

        public void UndoTurn()
        {
            RotationIndex++;
        }

        public void MoveLeft()
        {
            PosX--;
        }

        public void MoveRight()
        {
            PosX++;
        }

        public void MoveDown()
        {
            PosY++;
        }

        public void MoveUp()
        {
            PosY--;
        }

        public int[,] GetBlockCurrentStatus()
        {
            return Blocks.Blocks[BlockNr, RotationIndex];
        }
    }
}