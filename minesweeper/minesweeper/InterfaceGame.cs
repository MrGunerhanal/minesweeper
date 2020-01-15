using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public interface InterfaceGame
    {
        event EventHandler mineCounterChanged;
        event EventHandler<MineEventArgs> ClickMine;

        void Run();

    }
}
