﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thin_Ice.Model
{
    class MeltedIce : Piece
    {
        public MeltedIce(int x, int y)
        {
            _xPosition = x;
            _yPosition = y;

            SetAppearance();
        }

        private void SetAppearance()
        {
            _image = new Uri("pack://application:,,,/Thin Ice;component/Resources/water.jpg");
        }
    }
}
