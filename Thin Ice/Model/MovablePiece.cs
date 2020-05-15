namespace Thin_Ice.Model
{
    abstract class MovablePiece : Piece
    {
        
        public void Move(int direction)
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
    }
}