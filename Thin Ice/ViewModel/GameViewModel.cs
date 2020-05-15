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

            PKeyPressedCommand = new KeyPressedCommand(OnPKeyPressed);
            RKeyPressedCommand = new KeyPressedCommand(OnRKeyPressed);
            MKeyPressedCommand = new KeyPressedCommand(OnMKeyPressed);

            EnterKeyPressedCommand = new KeyPressedCommand(OnEnterKeyPressed);

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

        /// <summary>
        /// Gets or sets the P key pressed command.
        /// </summary>
        public ICommand PKeyPressedCommand
        {
            get;
            private set;
        }

        public ICommand RKeyPressedCommand
        {
            get;
            private set;
        }

        public ICommand MKeyPressedCommand
        {
            get;
            private set;
        }
        public ICommand EnterKeyPressedCommand 
        { 
            get; 
            private set; 
        }

        private void OnUpKeyPressed(object arg)
        {
            
            if (GameLogic.StateManager.CurrentState == Game.IN_GAME) GameLogic.ProccessPlayerMoveEvent(Game.DIRECTION_UP);

        }

        private void OnRightKeyPressed(object arg)
        {
            if (GameLogic.StateManager.CurrentState == Game.IN_GAME) GameLogic.ProccessPlayerMoveEvent(Game.DIRECTION_RIGHT);
        }

        private void OnDownKeyPressed(object arg)
        {
            if (GameLogic.StateManager.CurrentState == Game.IN_GAME) GameLogic.ProccessPlayerMoveEvent(Game.DIRECTION_DOWN);
        }

        private void OnLeftKeyPressed(object arg)
        {
            if (GameLogic.StateManager.CurrentState == Game.IN_GAME) GameLogic.ProccessPlayerMoveEvent(Game.DIRECTION_LEFT);
        }

        private void OnPKeyPressed(object arg)
        {
            GameLogic.ProccessStateChangeEvent(Game.PAUSE);
        }

        private void OnRKeyPressed(object arg)
        {
            GameLogic.ProccessStateChangeEvent(Game.RESTART);
        }

        private void OnMKeyPressed(object arg)
        {
            GameLogic.ProccessStateChangeEvent(Game.IN_MAIN_MENU);
        }

        private void OnEnterKeyPressed(object arg)
        {
            if (GameLogic.StateManager.CurrentState == Game.IN_FINAL_SCREEN) 
                GameLogic.ProccessStateChangeEvent(Game.IN_MAIN_MENU);
            else
                GameLogic.ProccessStateChangeEvent(Game.IN_GAME);
        }

    }
}
