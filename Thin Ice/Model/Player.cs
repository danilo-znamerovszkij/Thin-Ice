using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thin_Ice.Model
{
    class Player : MovablePiece
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
                Move(_direction);
                RaisePropertyChanged("XPosition");
                RaisePropertyChanged("YPosition");
                RaisePropertyChanged("DirectionOfTravel");
                RaisePropertyChanged("DirectionOfTravelDegrees");
            }
        }

    }
}
