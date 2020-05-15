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
            //generate level
            //width 27, h=25
            IsLevelLost = 0;
            IsOnPause = 0;

            CurrentState = IN_GAME;

            Board = new ObservableCollection<Piece>();
            Player = new Player(50, 50);

            InitLevel(difficulty);

            RaisePropertyChanged("Score");
            RaisePropertyChanged("Board");

        }


        public void ProccessStateChangeEvent(int state)
        {
            switch (state)
            {

                case IN_GAME:


                    break;

                case RESTART:

                    InitLevel(difficulty);
                    break;

                case PAUSE:

                    SetOnPause();
                    break;

                case IN_MAIN_MENU:

                    break;
            }
        }

        private void SetOnPause()
        {
            if(CurrentState == PAUSE)
            {
                CurrentState = IN_GAME;
                IsOnPause = 0;
                RaisePropertyChanged("IsOnPause");
            }
            else
            {
                CurrentState = PAUSE;
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

            CheckIfPlayerStuck();

            CheckIfLevelComplete();
        }

        private void CheckIfPlayerStuck()
        {
            if ( (CheckWallCollision(Game.DIRECTION_DOWN) || CheckMeltedIceCollision(Game.DIRECTION_DOWN))
               && (CheckWallCollision(Game.DIRECTION_UP) || CheckMeltedIceCollision(Game.DIRECTION_UP))
               && (CheckWallCollision(Game.DIRECTION_LEFT) || CheckMeltedIceCollision(Game.DIRECTION_LEFT))
               && (CheckWallCollision(Game.DIRECTION_RIGHT) || CheckMeltedIceCollision(Game.DIRECTION_RIGHT)))
            {
                
                IsLevelLost = 1;
                RaisePropertyChanged("IsLevelLost");

                if (IsLevelLost == 1)
                {
                    System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    dispatcherTimer.Tick += levelLostTimer_Tick;
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
                    dispatcherTimer.Start();
                }
                
            }
                            
        }

        private void levelLostTimer_Tick(object sender, EventArgs e)
        {
            // code goes here
            IsLevelLost = 0;
            RaisePropertyChanged("IsLevelLost");
        }


        private bool CheckIfLevelComplete()
        {

            foreach (var p in Board)
            {

                if (p is NextLevelDoor && Player.isInTheSamePlace(p))
                {
                    IsLevelCompleted = true;

                    difficulty += 100;

                    //TODO: init next level
                    InitLevel(difficulty);

                    RaisePropertyChanged("Score");
                    RaisePropertyChanged("Board");
                    
                    return true;
                }

            }

            return false;

                //isCompleted = true;
                //if (currentLevel.equals(easyLevel.generate()))
                //{
                //    currentLevel = mediumLevel.generate();
                //}
                //else if (currentLevel.equals(mediumLevel.generate()))
                //{
                //    currentLevel = hardLevel.generate();
                //}
                //else if (currentLevel.equals(hardLevel.generate()))
                //{
                //    currentLevel = easyLevel.generate();
                //    endGame.init(stateObserver);
                //}

                ////initiate the next level
                //restartLevel();
                //repaint();
            
        }

        private void InitLevel(int difficulty)
        {
            Board.Clear();
            int tempScore = Score;
            Player.XPosition = 50;
            Player.YPosition = 50;

            for (int x = 0; x <= Game.DefaultGameWidth; x = x + Game.BlockSize)
            {
                for (int y = 0; y <= Game.DefaultGameHeight; y = y + Game.BlockSize)
                {
                    //draw border
                    if (x == 0 || y == 0
                        || x == Game.DefaultGameWidth - Game.BlockSize
                        || y == Game.DefaultGameHeight - Game.BlockSize)
                        Board.Add(new IceBlock(x, y));

                    //draw level

                }

            }

            Random _randomNumber = new Random((int)DateTime.Now.Ticks);
            int steps = 0;
            ObservableCollection<Piece> Level = new ObservableCollection<Piece>();
            int tempx, tempy, n, tempn =0;
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
                    {
                        Board.Add(new MoneyBag(tempx, tempy));

                    }
                    else
                        Board.Add(new IceFloor(tempx, tempy));
                }

                //add a random money bag
                

                if (steps == difficulty)
                {
                    Board.Add(new NextLevelDoor(Player.XPosition, Player.YPosition));
                }

                //save cell
                steps++;
            }


            for (int x = Game.BlockSize; x <= Game.DefaultGameWidth - Game.BlockSize; x = x + Game.BlockSize)
            {
                for (int y = Game.BlockSize; y <= Game.DefaultGameHeight - Game.BlockSize; y = y + Game.BlockSize)
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
                if (p is MoneyBag && Player.isInTheSamePlace(p))
                {
                    Board.Remove(p);
                    Score += 100;
                    RaisePropertyChanged("Score");


                    var image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri("pack://application:,,,/Thin Ice;component/Resources/key.gif");
                    image.EndInit();
                    Image img = new Image();
                    ImageBehavior.SetAnimatedSource( img, image);




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
                        
                        if (p is MeltedIce && Player.isTopCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_DOWN:

                    foreach (var p in Board)
                    {

                        if (p is MeltedIce && Player.isBottomCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_LEFT:

                    foreach (var p in Board)
                    {

                        if (p is MeltedIce && Player.isLeftCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_RIGHT:

                    foreach (var p in Board)
                    {

                        if (p is MeltedIce && Player.isRightCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
            }
            //    case RIGHT_COLLISION:

            //        for (int i = 0; i < blocks.size(); i++)
            //        {

            //            Block block = blocks.get(i);

            //            if (piece.isRightCollision(block))
            //            {
            //                return true;
            //            }
            //        }

            //        return false;

            //    case TOP_COLLISION:

            //        for (int i = 0; i < blocks.size(); i++)
            //        {

            //            Block block = blocks.get(i);

            //            if (piece.isTopCollision(block))
            //            {

            //                return true;
            //            }
            //        }

            //        return false;

            //    case BOTTOM_COLLISION:

            //        for (int i = 0; i < blocks.size(); i++)
            //        {

            //            Block block = blocks.get(i);

            //            if (piece.isBottomCollision(block))
            //            {

            //                return true;
            //            }
            //        }

            //        return false;

            //    default:
            //        break;
            //}

            return false;
        }

        private bool CheckWallCollision(int direction)
        {
            switch (direction)
            {

                case DIRECTION_UP:
                    
                    foreach (var p in Board)
                    {
                        if (p is IceBlock && Player.isTopCollision(p))
                        {
                            return true;
                        }


                    }
                    break;
                case DIRECTION_DOWN:

                    foreach (var p in Board)
                    {

                        if (p is IceBlock && Player.isBottomCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_LEFT:

                    foreach (var p in Board)
                    {

                        if (p is IceBlock && Player.isLeftCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_RIGHT:

                    foreach (var p in Board)
                    {

                        if (p is IceBlock && Player.isRightCollision(p))
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
                        if (p is IceFloor && Player.isTopCollision(p))
                        {
                            return true;
                        }


                    }
                    break;
                case DIRECTION_DOWN:

                    foreach (var p in Board)
                    {

                        if (p is IceFloor && Player.isBottomCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_LEFT:

                    foreach (var p in Board)
                    {

                        if (p is IceFloor && Player.isLeftCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_RIGHT:

                    foreach (var p in Board)
                    {

                        if (p is IceFloor && Player.isRightCollision(p))
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
        public Player Player { get; private set; }

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
        public int CurrentState { get; private set; }
    }
}
