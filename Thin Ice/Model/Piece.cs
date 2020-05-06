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

        protected double _xPosition;
        protected double _yPosition;
        protected double _width = Game.BlockSize;
        protected double _height = Game.BlockSize;
        private double _gameBoardHeightPixels = 100;
        private double _gameBoardWidthPixels = 100;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Piece()
        {

        }
        

        /// <summary>
        /// Gets or sets the current x ordinate.
        /// </summary>
        public double XPosition
        {
            get
            {
                return _xPosition;
            }
        }

        /// <summary>
        /// Gets or sets the current y ordinate.
        /// </summary>
        public double YPosition
        {
            get
            {
                return _yPosition;
            }
        }

        /// <summary>
        /// Gets or sets the current x ordinate in pixels.
        /// </summary>
        public double XPositionPixels
        {
            get
            {
                return (_xPosition / Game.GameBoardWidthScale) * _gameBoardWidthPixels;
            }
        }

        /// <summary>
        /// Gets or sets the current y ordinate in pixels.
        /// </summary>
        public double YPositionPixels
        {
            get
            {
                return (_yPosition / Game.GameBoardHeightScale) * _gameBoardHeightPixels;
            }
        }

        /// <summary>
        /// Gets or sets the current x ordinate in pixels, shifted for correct rendering on a screen.
        /// </summary>
        public double XPositionPixelsScreen
        {
            get
            {
                return ((_xPosition - (_width / 2.0)) / Game.GameBoardWidthScale) * _gameBoardWidthPixels;
            }
        }

        /// <summary>
        /// Gets or sets the current y ordinate in pixels, shifted for correct rendering on a screen.
        /// </summary>
        public double YPositionPixelsScreen
        {
            get
            {
                return ((_yPosition - (_height / 2.0)) / Game.GameBoardHeightScale) * _gameBoardHeightPixels;
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

        #endregion

        #region Methods
        #endregion
    }
}
