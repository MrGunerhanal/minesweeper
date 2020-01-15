using System;

namespace minesweeper
{
    public class MineEventArgs : EventArgs
    {
        public int MineRow { get; set; }
        public int MineColumn { get; set; }

        public MineEventArgs(int row, int column) {

            this.MineRow = row;
            this.MineColumn = column;
        }
    }
}
