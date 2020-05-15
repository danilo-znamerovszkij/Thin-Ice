using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Thin_Ice.Common;
using WpfAnimatedGif;

namespace Thin_Ice.Model
{
    class Game : Observer
    {
        public const int PAUSE = 0;
        public const int IN_GAME = 1;
        public const int IN_MAIN_MENU = 2;
        public const int RESTART = 3;
        public const int IN_FINAL_SCREEN = 4;

        public const int DIRECTION_UP = 0;
        public const int DIRECTION_DOWN = 1;
        public const int DIRECTION_LEFT = 2;
        public const int DIRECTION_RIGHT = 3;

        public const double DefaultGameWidth = 550;
        public const double DefaultGameHeight = 525;

        public const double GameBoardWidthScale = 100;
        public const double GameBoardHeightScale = 100;

        public const int StartLevel = 1;
        public const int EndLevel = 200;

        public const int BlockSize = 25;

        public int difficulty = 30;
        


        public Game()
        {
            
            IsLevelLost = 0;
            IsOnPause = 0;
            MainMenuVisibility = 1;
            CongratsScreenVisibility = 0;

            StateManager = new StateManager
            {
                CurrentState = IN_MAIN_MENU
            };

            Board = new ObservableCollection<Piece>();
            Player = new Player(50, 50);
            Level = 1;

            InitLevel(difficulty);

            RaisePropertyChanged("Level");
            RaisePropertyChanged("Score");
            RaisePropertyChanged("Board");

        }


        public void ProccessStateChangeEvent(int state)
        {
            switch (state)
            {

                case IN_GAME:

                    InitGame();
                    break;

                case RESTART:

                    InitLevel(difficulty);
                    break;

                case PAUSE:

                    SetOnPause();
                    break;

                case IN_MAIN_MENU:

                    InitMainMenu();
                    break;

            }
        }

        private void InitMainMenu()
        {
            if (StateManager.CurrentState == PAUSE) SetOnPause();
            if (StateManager.CurrentState == IN_FINAL_SCREEN) ToggleCongratsScreen();

            StateManager.CurrentState = IN_MAIN_MENU;

            MainMenuVisibility = 1;
            RaisePropertyChanged("MainMenuVisibility");
        }

        private void InitGame()
        {
            if (StateManager.CurrentState == IN_MAIN_MENU)
            {
                MainMenuVisibility = 0;
                RaisePropertyChanged("MainMenuVisibility");

                difficulty = 30;
                Level = 1;
                InitLevel(difficulty);
                RaisePropertyChanged("Score");
            }

            StateManager.CurrentState = IN_GAME;

        }

        private void ToggleCongratsScreen()
        {
            CongratsScreenVisibility = (CongratsScreenVisibility == 1) ? 0 : 1;
            RaisePropertyChanged("CongratsScreenVisibility");

        }

        private void SetOnPause()
        {
            if (StateManager.CurrentState == PAUSE)
            {
                StateManager.CurrentState = IN_GAME;
                IsOnPause = 0;
                RaisePropertyChanged("IsOnPause");
            }
            else if (StateManager.CurrentState == IN_GAME)
            {
                StateManager.CurrentState = PAUSE;
                IsOnPause = 1;
                RaisePropertyChanged("IsOnPause");
            }

        }

        public void ProccessPlayerMoveEvent(int direction)
        {
            if (CheckWallCollision(direction))
                return;


            if (CheckMeltedIceCollision(direction))
                return;

            MeltTheIce(Player.XPosition, Player.YPosition);

            Player.Direction = direction;

            CheckMoneyBagCollision();

            CheckIfLevelComplete();

            CheckIfPlayerStuck();

        }

        private void CheckIfPlayerStuck()
        {
            if ((CheckWallCollision(Game.DIRECTION_DOWN) || CheckMeltedIceCollision(Game.DIRECTION_DOWN))
               && (CheckWallCollision(Game.DIRECTION_UP) || CheckMeltedIceCollision(Game.DIRECTION_UP))
               && (CheckWallCollision(Game.DIRECTION_LEFT) || CheckMeltedIceCollision(Game.DIRECTION_LEFT))
               && (CheckWallCollision(Game.DIRECTION_RIGHT) || CheckMeltedIceCollision(Game.DIRECTION_RIGHT)))
            {

                IsLevelLost = 1;
                RaisePropertyChanged("IsLevelLost");

                if (IsLevelLost == 1)
                {
                    System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    dispatcherTimer.Tick += LevelLostTimer_Tick;
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
                    dispatcherTimer.Start();
                }

            }

        }

        private void LevelLostTimer_Tick(object sender, EventArgs e)
        {
            IsLevelLost = 0;
            RaisePropertyChanged("IsLevelLost");
        }


        private bool CheckIfLevelComplete()
        {

            foreach (var p in Board)
            {

                if (p is NextLevelDoor && Player.IsInTheSamePlace(p))
                {
                    IsLevelCompleted = true;

                    if (Score >= 300)
                    {
                        ToggleCongratsScreen();
                        StateManager.CurrentState = IN_FINAL_SCREEN;
                    }


                    difficulty += 100;
                    Level++;

                    InitLevel(difficulty);

                    RaisePropertyChanged("Level");
                    RaisePropertyChanged("Score");
                    RaisePropertyChanged("Board");

                    return true;
                }

            }

            return false;

        }

        private void InitLevel(int difficulty)
        {
            if(difficulty == 30)
            {
                Score = 0;
                difficulty = 30;
                Level = 1;
            }

            Board.Clear();

            int tempScore = Score;

            Player.XPosition = 50;
            Player.YPosition = 50;

            for (int x = 0; x <= Game.DefaultGameWidth; x += Game.BlockSize)
            {
                for (int y = 0; y <= Game.DefaultGameHeight; y += Game.BlockSize)
                {
                    //draw border
                    if (x == 0 || y == 0
                        || x == Game.DefaultGameWidth - Game.BlockSize
                        || y == Game.DefaultGameHeight - Game.BlockSize)
                        Board.Add(new IceBlock(x, y));
                }
            }

            Random _randomNumber = new Random((int)DateTime.Now.Ticks);
            int steps = 1;
            int tempx, tempy, n, tempn = 0;
            while (steps <= difficulty)
            {

                if (steps % 2 == 0)
                    n = _randomNumber.Next(1, 3);
                else
                    n = _randomNumber.Next(0, 2);

                if (tempn == 0) n = 3;


                tempn = n;
                tempx = Player.XPosition;
                tempy = Player.YPosition;
                if (!CheckWallCollision(n) && !CheckMoneyBagCollision() && !CheckIceFloorCollision(n))
                {
                    Player.Direction = n;
                    if (steps % 10 == 0)
                        Board.Add(new MoneyBag(tempx, tempy));
                    else
                        Board.Add(new IceFloor(tempx, tempy));
                }

                if (steps == difficulty)
                {
                    Board.Add(new NextLevelDoor(Player.XPosition, Player.YPosition));
                }

                steps++;
            }


            for (int x = Game.BlockSize; x <= Game.DefaultGameWidth - Game.BlockSize; x += Game.BlockSize)
            {
                for (int y = Game.BlockSize; y <= Game.DefaultGameHeight - Game.BlockSize; y += Game.BlockSize)
                {
                    //if ice floor put a block to the left
                    var flag = true;
                    foreach (var tile in Board)
                    {
                        if (tile.XPosition == x && tile.YPosition == y)
                        {

                            flag = false;
                            break;
                        }
                    }
                    if (flag) Board.Add(new IceBlock(x, y));

                }
            }

            Player.XPosition = 50;
            Player.YPosition = 50;
            Score = tempScore;

        }

        private bool CheckMoneyBagCollision()
        {

            foreach (var p in Board)
            {
                if (p is MoneyBag && Player.IsInTheSamePlace(p))
                {
                    Board.Remove(p);
                    Score += 100;
                    RaisePropertyChanged("Score");

                    return true;
                }

            }

            return false;

        }

        private bool CheckMeltedIceCollision(int direction)
        {
            switch (direction)
            {

                case DIRECTION_UP:

                    foreach (var p in Board)
                    {

                        if (p is MeltedIce && Player.IsTopCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_DOWN:

                    foreach (var p in Board)
                    {

                        if (p is MeltedIce && Player.IsBottomCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_LEFT:

                    foreach (var p in Board)
                    {

                        if (p is MeltedIce && Player.IsLeftCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_RIGHT:

                    foreach (var p in Board)
                    {

                        if (p is MeltedIce && Player.IsRightCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
            }

            return false;
        }

        private bool CheckWallCollision(int direction)
        {
            switch (direction)
            {

                case DIRECTION_UP:

                    foreach (var p in Board)
                    {
                        if (p is IceBlock && Player.IsTopCollision(p))
                        {
                            return true;
                        }


                    }
                    break;
                case DIRECTION_DOWN:

                    foreach (var p in Board)
                    {

                        if (p is IceBlock && Player.IsBottomCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_LEFT:

                    foreach (var p in Board)
                    {

                        if (p is IceBlock && Player.IsLeftCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_RIGHT:

                    foreach (var p in Board)
                    {

                        if (p is IceBlock && Player.IsRightCollision(p))
                        {
                            return true;
                        }

                    }
                    break;

            }


            return false;
        }

        private bool CheckIceFloorCollision(int direction)
        {
            switch (direction)
            {

                case DIRECTION_UP:

                    foreach (var p in Board)
                    {
                        if (p is IceFloor && Player.IsTopCollision(p))
                        {
                            return true;
                        }


                    }
                    break;
                case DIRECTION_DOWN:

                    foreach (var p in Board)
                    {

                        if (p is IceFloor && Player.IsBottomCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_LEFT:

                    foreach (var p in Board)
                    {

                        if (p is IceFloor && Player.IsLeftCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_RIGHT:

                    foreach (var p in Board)
                    {

                        if (p is IceFloor && Player.IsRightCollision(p))
                        {
                            return true;
                        }

                    }
                    break;

            }


            return false;
        }

        private void MeltTheIce(int x, int y)
        {
            Board.Add(new MeltedIce(x, y));
        }

        public int Level { get; private set; }
        public Player Player { get; private set; }
        public StateManager StateManager { get; private set; }
        public ObservableCollection<Piece> Board { get; private set; }

        public double BlockSizeWidth
        {
            get
            {
                return BlockSize;
            }
        }

        public double BlockSizeHeight
        {
            get
            {
                return BlockSize;
            }
        }

        public int Score { get; private set; }
        public bool IsLevelCompleted { get; private set; }
        public int IsLevelLost { get; private set; }
        public int IsOnPause { get; private set; }
        public int InMainMenu { get; private set; }
        public int MainMenuVisibility { get; private set; }
        public int CongratsScreenVisibility { get; private set; }
    }
}
