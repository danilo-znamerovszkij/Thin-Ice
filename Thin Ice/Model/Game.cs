using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thin_Ice.Common;

namespace Thin_Ice.Model
{
    class Game : Observer
    {
        public static int IN_GAME = 1;

        public static int DIRECTION_UP = 0;
        public static int DIRECTION_DOWN = 1;
        public static int DIRECTION_RIGHT = 2;
        public static int DIRECTION_LEFT = 3;

        public const double DefaultGameWidth = 500;
        public const double DefaultGameHeight = 500;
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

        public const double BlockSize = 20;

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
            Player = new Player(50,50);
            RaisePropertyChanged("TheCherry");
        }

        public int GetState()
            {
                return IN_GAME;
            }

        public void ProccessKeyboardEvent(int direction)
            {
            Player.Direction = direction;
            }

        public Player Player { get; private set; }



    }
}
