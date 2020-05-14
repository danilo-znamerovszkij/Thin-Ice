using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thin_Ice.Common;
using System.Windows.Input;
using Thin_Ice.Model;
using System.Collections.Specialized;

namespace Thin_Ice.ViewModel
{
    class Simple
    {
        public double Width
        {
            get
            {
                return 30;
            }
        }
    }
    class GameViewModel : Observer
    {

        public GameViewModel()
        {
            GameLogic = new Game();
            UpKeyPressedCommand = new KeyPressedCommand(OnUpKeyPressed);
            RightKeyPressedCommand = new KeyPressedCommand(OnRightKeyPressed);
            DownKeyPressedCommand = new KeyPressedCommand(OnDownKeyPressed);
            LeftKeyPressedCommand = new KeyPressedCommand(OnLeftKeyPressed);

        }


        public Game GameLogic { get; }

        /// <summary>
        /// Gets or sets the UP key pressed command.
        /// </summary>
        public ICommand UpKeyPressedCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the RIGHT key pressed command.
        /// </summary>
        public ICommand RightKeyPressedCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the DOWN key pressed command.
        /// </summary>
        public ICommand DownKeyPressedCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the LEFT key pressed command.
        /// </summary>
        public ICommand LeftKeyPressedCommand
        {
            get;
            private set;
        }

        private void OnUpKeyPressed(object arg)
        {
            //get state
            if (GameLogic.GetState() == Game.IN_GAME) GameLogic.ProccessKeyboardEvent(Game.DIRECTION_UP);

        }

        private void OnRightKeyPressed(object arg)
        {
            if (GameLogic.GetState() == Game.IN_GAME) GameLogic.ProccessKeyboardEvent(Game.DIRECTION_RIGHT);
        }

        private void OnDownKeyPressed(object arg)
        {
            if (GameLogic.GetState() == Game.IN_GAME) GameLogic.ProccessKeyboardEvent(Game.DIRECTION_DOWN);
        }

        private void OnLeftKeyPressed(object arg)
        {
            if (GameLogic.GetState() == Game.IN_GAME) GameLogic.ProccessKeyboardEvent(Game.DIRECTION_LEFT);
        }


    }
}
