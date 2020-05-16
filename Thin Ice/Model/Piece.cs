using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thin_Ice.Common;

namespace Thin_Ice.Model
{
    abstract class Piece : Observer
    {
        #region Fields

        protected int _xPosition;
        protected int _yPosition;
        protected double _width = Game.BlockSize;
        protected double _height = Game.BlockSize;
        private readonly double _gameBoardHeightPixels = 100;
        private readonly double _gameBoardWidthPixels = 100;
        protected Uri _image;

        public int XPosition
        {
            get
            {
                
                return _xPosition;
            }
            set
            {

                _xPosition = value;
                RaisePropertyChanged("XPosition");
            }
        }

        public int YPosition
        {
            get
            {
                return _yPosition;
            }
            set
            {

                _yPosition = value;
                RaisePropertyChanged("YPosition");
            }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        public double Width
        {
            get
            {
                return _width;
            }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public double Height
        {
            get
            {
                return _height;
            }
        }

        /// <summary>
        /// Gets the width in pixels.
        /// </summary>
        public double WidthPixels
        {
            get
            {
                return (_width / Game.GameBoardWidthScale) * _gameBoardWidthPixels;
            }
        }

        /// <summary>
        /// Gets the height in pixels.
        /// </summary>
        public double HeightPixels
        {
            get
            {
                return (_height / Game.GameBoardHeightScale) * _gameBoardHeightPixels;
            }
        }

        public Uri Image
        {
            get
            {
                return _image;
            }
        }

        #endregion

        #region Methods
        public bool IsTopCollision(Piece piece)
        {
            return YPosition - Game.BlockSize == piece.YPosition && XPosition == piece.XPosition;
        }

        public bool IsBottomCollision(Piece piece)
        {
            return YPosition + Game.BlockSize == piece.YPosition && XPosition == piece.XPosition;
        }

        public bool IsLeftCollision(Piece piece)
        {
            return XPosition - Game.BlockSize == piece.XPosition && YPosition == piece.YPosition;
        }

        public bool IsRightCollision(Piece piece)
        {
            return XPosition + Game.BlockSize == piece.XPosition && YPosition == piece.YPosition;
        }

        public bool IsInTheSamePlace(Piece piece)
        {
            return YPosition == piece.YPosition && XPosition == piece.XPosition;
        }

        #endregion
    }
}
