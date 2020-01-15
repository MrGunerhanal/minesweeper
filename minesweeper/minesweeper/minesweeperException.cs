using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class minesweeperException : ApplicationException
    {
        public minesweeperException(string message) : base(message) { }
    

    public minesweeperException(string message, Exception innerException) : base(message, innerException) { }
    }
}
