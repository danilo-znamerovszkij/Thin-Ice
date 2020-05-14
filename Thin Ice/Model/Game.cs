using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Shapes;
using Thin_Ice.Common;

namespace Thin_Ice.Model
{
    class Game : Observer
    {
        public static int IN_GAME = 1;

        public const int DIRECTION_UP = 0;
        public const int DIRECTION_DOWN = 1;
        public const int DIRECTION_RIGHT = 2;
        public const int DIRECTION_LEFT = 3;

        public const double DefaultGameWidth = 550;
        public const double DefaultGameHeight = 525;

        public const double GameBoardWidthScale = 100;
        public const double GameBoardHeightScale = 100;
        public const int StartLevel = 1;
        public const int EndLevel = 200;
        public const int DefaultGameStepMilliSeconds = 250;
        public const int DecreaseGameStepMilliSeconds = 1;
        public const int RestartStepMilliSeconds = 1000;
        public const int RestartCountdownStartSeconds = 5;
        public const double DirectionUpDegrees = 0;
        public const double DirectionRightDegrees = 90;
        public const double DirectionDownDegrees = 180;
        public const double DirectionLeftDegrees = 270;
        public const double StepSize = 2.5;
        public const double HeadWidth = 2.5;
        public const double HeadHeight = 2.5;
        public const double EyeOffet = 0.75;

        public const int BlockSize = 25;

        public const double EyeHeight = 1;
        public const double BodyWidth = 2.5;
        public const double BodyHeight = 2.5;
        public const double DefaultXposition = 50;
        public const double DefaultYposition = 50;
        public const double CherryWidth = 2.5;
        public const double CherryHeight = 2.5;
        public const int MinimumPosition = 5;
        public const int MaximumPosition = 95;
        public const double PlacementBuffer = 2;

        public Game()
        {
            //generate level
            //width 27, h=25


            Board = new ObservableCollection<Piece>();
            Player = new Player(50, 50);
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
            int tempx, tempy;
            while (steps<200)
            {
                
                int n = _randomNumber.Next(0, 3);
                
                tempx = Player.XPosition;
                tempy = Player.YPosition;
                if (!CheckWallCollision(n))
                {
                    Player.Direction = n;
                    Board.Add(new IceFloor(tempx , tempy));
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
                        foreach(var tile in Board)
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
        RaisePropertyChanged("Board");

        }

        public int GetState()
        {
            return IN_GAME;
        }

        public void ProccessKeyboardEvent(int direction)
        {
            if(!CheckWallCollision( direction))
                Player.Direction = direction;
            
            
            //int n = 5;
            //Random _randomNumber = new Random((int)DateTime.Now.Ticks);
            //n = _randomNumber.Next(5, 400);

            //foreach (var piece in Board)
            //{
            //    Console.WriteLine("Amount is {0} and type is {1}", money.amount, money.type);
            //    n = _randomNumber.Next(5, 400);

            //    piece.XPosition = 0;
            //    piece.YPosition = 5;

            //}
            //int x = 0;
        }

        private bool CheckWallCollision(int direction)
        {
            switch (direction)
            {

                case DIRECTION_UP:
                    
                    foreach (var p in Board)
                    {
                        Console.WriteLine(p.GetHashCode());
                        Console.WriteLine(p is IceBlock);
                        if (p.isMovable() && Player.isTopCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_DOWN:

                    foreach (var p in Board)
                    {

                        if (p.isMovable() && Player.isBottomCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_LEFT:

                    foreach (var p in Board)
                    {

                        if (p.isMovable() && Player.isLeftCollision(p))
                        {
                            return true;
                        }

                    }
                    break;
                case DIRECTION_RIGHT:

                    foreach (var p in Board)
                    {

                        if (p.isMovable() && Player.isRightCollision(p))
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

    }
}
