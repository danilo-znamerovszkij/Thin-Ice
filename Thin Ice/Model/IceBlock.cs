using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thin_Ice.Model
{
    class IceBlock : Piece
    {
        public IceBlock(int x, int y)
        {
            _xPosition = x;
            _yPosition = y;

            setAppearance();
        }

        private void setAppearance()
        {
            _image = new Uri("pack://application:,,,/Thin Ice;component/Resources/wall.png");
        }


    }
}
