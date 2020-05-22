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
    public class BallMovement
    {
        verticalMovement vertical;

        public verticalMovement Vertical
        {
            get { return vertical; }
            set { vertical = value; }
        }

        horizontalMovement horizontal;

        public horizontalMovement Horizontal
        {
            get { return horizontal; }
            set { horizontal = value; }
        }
    }
    public enum verticalMovement
    {
        up, down
    }
    public enum horizontalMovement
    {
        left, right
    }
}
