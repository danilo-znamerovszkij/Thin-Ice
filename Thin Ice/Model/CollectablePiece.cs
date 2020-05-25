using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thin_Ice.Model
{
    abstract class CollectablePiece : Piece
    {
        public bool IsCollectable() 
        {
            return true;
        }
    }
}
