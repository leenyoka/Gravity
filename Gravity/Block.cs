using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Gravity
{
    public class Block: Grid
    {
        #region Properties
        int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        Holds holds;

        public Holds Holds
        {
            get { return holds; }
            set { holds = value; }
        }
        Side side;

        public Side Side
        {
            get { return side; }
            set { side = value; }
        }
        Position position;

        public Position Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion Properties

        #region Constructor

        public Block(Holds hold, Side sid, Position pos, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.holds = hold;
            this.side = sid;
            this.position = pos;
        }
        public Block(Side sid, Position pos, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.holds = Gravity.Holds.None;
            this.side = sid;
            this.position = pos;
        }
        public Block()
        {
        }

        #endregion Constructor

    }
    public enum Holds
    {
        Food, Ball,Blocker,None
    }
    public enum Side
    {
        Right, Left, None
    }
    public enum Position
    {
        Left, Middle, Right
    }

    public class Piece
    {
        int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Piece(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public enum Move
    {
        right, left
    }
}
