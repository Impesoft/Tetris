using System;
using System.Collections.Generic;
using System.Text;

namespace tetris
{
    internal class BlockL : Block
    {
        public BlockL()
        {
            Rotation1 = new bool[,] { { false, false, false, false }, { false, true, true, false }, { false, false, true, false, }, { false, false, true, false, } };
            Rotation2 = new bool[,] { { false, false, false, false }, { false, true, true, true }, { false, true, false, false }, { false, false, false, false } };
            Rotation3 = new bool[,] { { false, false, false, false }, { false, true, false, false }, { false, true, false, false }, { false, true, true, false, } };
            Rotation4 = new bool[,] { { false, false, false, false }, { false, false, false, true }, { false, true, true, true }, { false, false, false, false } };

            BlockRotations[0] = Rotation1;
            BlockRotations[1] = Rotation2;
            BlockRotations[2] = Rotation3;
            BlockRotations[3] = Rotation4;
        }
    }
}