namespace tetris
{
    public abstract class Block
    {
        public int PosX { get; set; } = 4;
        public int PosY { get; set; }
        protected bool[,] Rotation1 { get; set; }
        protected bool[,] Rotation2 { get; set; }
        protected bool[,] Rotation3 { get; set; }
        protected bool[,] Rotation4 { get; set; }
        protected bool[][,] BlockRotations { get; set; } = new bool[4][,];

        private int _rotationIndex;

        protected int RotationIndex
        {
            get { return _rotationIndex; }
            set
            {
                _rotationIndex = value;

                if (_rotationIndex < 0)
                {
                    _rotationIndex = 3;
                }
                if (_rotationIndex > 3)
                {
                    _rotationIndex = 0;
                }
            }
        }

        public void Turn()
        {
            RotationIndex--;
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

        public bool[,] GetBlockCurrentStatus()
        {
            return BlockRotations[RotationIndex];
        }
    }
}