namespace minesweeper
{
    public class Cover : CoverInterface
    {
        public Scene Scene { get; private set; }
        public int RowPosition { get; private set; }
        public int ColPosition { get; private set; }
        public bool IsFlagged { get; set; }
        public bool IsMined { get; set; }
        public bool IsRevealed { get; set; }
        
        public Cover(Scene scene, int rowPosition, int ColPosition)
        {
            this.Scene = scene;
            this.RowPosition = rowPosition;
            this.ColPosition = ColPosition;
        }

        public int Verify()
        {
            int counter = 0;

            if (!IsRevealed && !IsFlagged)
            {
                IsRevealed = true;

                for (int i = 0; i < 9; i++) // check the neighbours around bombs 
                {
                    if (i == 4) continue; // don't check itself
                    if (Scene.checkBomb(RowPosition + i / 3 - 1, ColPosition + i % 3 - 1)) counter++; // count if there is a bomb
                }

                if (counter == 0)
                {
                    for (int i = 0; i < 9; i++) // check the neighbours for bombs 
                    {
                        if (i == 4) continue; // don't check itself
                        Scene.openCover(RowPosition + i / 3 - 1, ColPosition + i % 3 - 1); // logic to reveal empty neighbours 
                    }
                }
            }

            return counter;
        }
    }
}