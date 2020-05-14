using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thin_Ice.Model
{
    class Player : Piece
    {
        private int _direction;

        public Player(int x, int y)
        {
            _xPosition = x;
            _yPosition = y;
        }

        public int Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
                move(_direction);
                RaisePropertyChanged("XPosition");
                RaisePropertyChanged("YPosition");
                RaisePropertyChanged("DirectionOfTravel");
                RaisePropertyChanged("DirectionOfTravelDegrees");
            }
        }

        public void move(int direction)
        {

            if (direction == Game.DIRECTION_UP)
                _yPosition -= Game.BlockSize;
            else if (direction == Game.DIRECTION_DOWN)
                _yPosition += Game.BlockSize;
            else if (direction == Game.DIRECTION_RIGHT)
                _xPosition += Game.BlockSize;
            else if (direction == Game.DIRECTION_LEFT)
                _xPosition -= Game.BlockSize;

        }

        public override bool isMovable()
        {
            return false;
        }
    }
}
