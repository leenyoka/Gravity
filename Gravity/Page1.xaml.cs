using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Gravity
{
    public partial class Page1 : PhoneApplicationPage
    {
        DispatcherTimer BallTimer;
        int ballSpeed;
        public Page1()
        {
            InitializeComponent();
            BallTimer = new DispatcherTimer();
            ballSpeed = 400;
            BallTimer.Interval = new TimeSpan(0, 0, 0, 0, ballSpeed);
            BallTimer.Tick += new EventHandler(BallControler);
            ballMovement = new BallMovement();
            LastMove = Move.right;

        }
        Move LastMove;
        public void BallControler(object o, EventArgs e)
        {
            int changeInY = 0;
            int changeInX = 0;

            if (ballMovement.Vertical == verticalMovement.up)
            {
                changeInY += 1;

            }
            else
            {
                changeInY -= 1;
            }


            if (ballMovement.Horizontal == horizontalMovement.right)
            {
                changeInX += 1;
            }
            else
            {
                changeInX -= 1;
            }

            BallGoes(changeInX, changeInY);
            
                
        }
        public void BallGoes(int changeInX, int ChangeInY)
        {
            if (changeInX < 0 && !BallCanMoveLeft(ball.X, ball.Y))
            {
                ballMovement.Horizontal = horizontalMovement.right;
                changeInX *= -1;
            }
            if (changeInX > 0 && !BallCanMoveRight(ball.X, ball.Y))
            {
                ballMovement.Horizontal = horizontalMovement.left;
                changeInX *= -1;
            }

            if (ChangeInY < 0 && !BallCanMoveDown(ball.X, ball.Y))
            {
                ballMovement.Vertical = verticalMovement.up;
                ChangeInY *= -1;
            }
            if (ChangeInY > 0 && !BallCanMoveUp(ball.X, ball.Y))
            {
                ballMovement.Vertical = verticalMovement.down;
                ChangeInY *= -1;
            }

            Block newBlock = GetGridBlock(ball.X + changeInX, ball.Y + ChangeInY);
            Block block = GetGridBlock(ball.X, ball.Y);

            if (newBlock.Holds != Holds.Blocker)
            {
                if (newBlock.Y > -6 && !(Holds.Food == newBlock.Holds))
                {

                    block.Holds = Holds.None;
                    block.Background = getMyImage(Holds.None);


                    ball.Y += ChangeInY;
                    ball.X += changeInX;

                    newBlock.Holds = Holds.Ball;
                    newBlock.Background = getMyImage(Holds.Ball);
                }
                else if (newBlock.Holds == Holds.Food)
                {
                    block.Holds = Holds.None;
                    block.Background = getMyImage(Holds.None);


                    ball.Y += ChangeInY;
                    ball.X += changeInX;

                    newBlock.Holds = Holds.Ball;
                    newBlock.Background = getMyImage(Holds.Ball);

                    //then change direction
                    JustAteFood();
                }
                else
                {
                    BallTimer.Stop();
                    MessageBox.Show("You Lose", "Game Over", MessageBoxButton.OK);
                    NavigationService.GoBack();
                }
                
            }
            else
            {
                blocking(block, changeInX, ChangeInY);
            }

            


        }
        public void JustAteFood()
        {
            

            if (ballMovement.Vertical == verticalMovement.up)
            {
                ballMovement.Vertical = verticalMovement.down;
            }
            else
            {
                ballMovement.Vertical = verticalMovement.up;
            }

            if (AteHalf())
            {
                //MessageBox.Show("Next level", "Level Complete", MessageBoxButton.OK);
                txtLevel.Content = level.ToString();
                BallTimer.Stop();
                levelNotifier.Visibility = System.Windows.Visibility.Visible;
                
            }
        }

        public bool AteHalf()
        {
            
            int count = 0;

            foreach (Block bloc in ContentPanel.Children)
            {
                if (bloc.Holds == Holds.Food)
                {
                    count++;
                }
            }

            if (count -4 < ((FoodCount / 2)))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public void blocking(Block current, int changeInX, int ChangeInY)
        {
            ballMovement.Vertical = verticalMovement.up;

            if (RandomNum() == 2)
            {
                ballMovement.Horizontal = horizontalMovement.left;
            }
            else
            {
                ballMovement.Horizontal = horizontalMovement.right;
            }


            //if (current.X > 4 && current.X < 8)
            //{
            //    //y movement

            //}
            //else
            //{
            //    ballMovement.Vertical = verticalMovement.up;

            //    if (ballMovement.Horizontal == horizontalMovement.left)
            //    {
            //        ballMovement.Horizontal = horizontalMovement.right;
            //    }
            //    else
            //    {
            //        ballMovement.Horizontal = horizontalMovement.left;
            //    }

            //}
        }
        public bool BallCanMoveLeft(int OldX, int OldY)
        {
            //Block Block = GetGridBlock(OldX - 1, OldY);

            if (OldX -1 < 0)
            {
                return false;
            }
            return true;

        }

        public bool BallCanMoveRight(int OldX, int OldY)
        {
            //Block Block = GetGridBlock(OldX + 1, OldY);

            if (OldX + 1 > 12)
            {
                return false;
            }
            return true;

        }

        public bool BallCanMoveDown(int OldX, int OldY)
        {
            //Block Block = GetGridBlock(OldX , OldY -1);

            if (OldY -1 < -6)
            {
                return false;
            }
            
            return true;

        }

        public bool BallCanMoveUp(int OldX, int OldY)
        {
            //Block Block = GetGridBlock(OldX, OldY +1);

            if (OldY + 1 > 8)
            {
                return false;
            }
            return true;

        }
        public int RandomNum()
        {
            int rand = RandomNumber(0, 9);

            if (rand > 5)
                return 1;
            else
                return 2;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            levelNotifier.Visibility = System.Windows.Visibility.Collapsed;
            Start();
            level = 1;
        }
        public void Start()
        {
            ContentPanel.Children.Clear();
            FoodCount = 0;
            ballSpeed -= 50;
            BallTimer.Interval = new TimeSpan(0, 0, 0, 0, ballSpeed);
            InitializeGrid();
            InitializeBlocker();
            InitializeBall();
            InitiateBAllMovement();
            InitializeFood();

            BallTimer.Start();
        }
        int FoodCount;

        public void InitializeFood()
        {
            for (int m = 7; m > 3; m--)//y
            {
                for (int n = 1; n <= 11; n++)//x
                {
                    Block current = GetGridBlock(n, m);
                    current.Holds = Holds.Food;
                    current.Background = getMyImage(Holds.Food);

                    FoodCount++;
                }
            }
        }
        public void InitializeGrid()
        {
            for (int m = 8; m >= -6; m--)//y
            {
                for (int n = 0; n <= 12; n++)//x
                {
                    Block block = new Block(GetSide(n), GetPosition(n), n, m);
                    block.Background = null;// getMyImage(block.Holds);
                    block.Width = 35;
                    block.Height = 45;

                    ContentPanel.Children.Add(block);
                }
            }
        }
        public Side GetSide(int x)
        {
            if (x == 0)
            {
                return Side.Left;
            }
            else if (x == 12)
            {
                return Side.Right;
            }
            else
            {
                return Side.None;
            }
        }

        public Position GetPosition(int x)
        {
            if (x <= 4)
            {
                return Position.Left;
            }
            else if (x > 4 && x <= 6)
            {
                return Position.Middle;
            }
            else
            {
                return Position.Right;
            }
        }

        public ImageBrush getMyImage(Holds holds)
        {
            string holder = "";

            if (holds == Holds.Ball)
            {
                holder = "Ball";
            }
            else if (holds == Holds.Blocker)
            {
                holder = "Blocker";
            }
            else if (holds == Holds.Food)
            {
                holder = "Food";
            }
            else
            {
                //return null;
                holder = "Empty";
            }

            ImageBrush bi = new ImageBrush();
            bi.ImageSource = new BitmapImage(new Uri(@"../Images/" + holder+ ".png", UriKind.Relative));
            return bi;
        }

        public Block GetGridBlock(int x, int y)
        {
            Block wantedBlock = new Block();
            foreach (Block block in ContentPanel.Children)
            {
                if (x == block.X && y == block.Y)
                {
                    wantedBlock = block;
                    break;
                }
            }
            return wantedBlock;
        }

        List<Piece> BlockerPieces = new List<Piece>();

        int level;

        public void InitializeBlocker()
        {
            ClearBlocker();
            level++;
            StartBlocker(level);


        }
        public void StartBlocker(int level)
        {
            List<Block> blocks = new List<Block>();

            blocks.Add(GetGridBlock(5, -6));
            blocks.Add(GetGridBlock(6, -6));
            blocks.Add(GetGridBlock(7, -6));

            if (level >= 2)
            {
                blocks.Add(GetGridBlock(8, -6));
            }

            if (level >= 4)
            {
                blocks.Add(GetGridBlock(4, -6));
            }

            if (level >= 6)
            {
                blocks.Add(GetGridBlock(3, -6));
            }


            foreach (Block bloc in blocks)
            {
                bloc.Holds = Holds.Blocker;
                bloc.Background = getMyImage(Holds.Blocker);
                BlockerPieces.Add(new Piece(bloc.X, bloc.Y));
            }
        }
        public void ClearBlocker()
        {
            BlockerPieces.Clear();
            foreach (Block bloc in ContentPanel.Children)
            {
                if (bloc.Holds == Holds.Blocker)
                {
                    bloc.Holds = Holds.None;
                    bloc.Background = getMyImage(Holds.None);
                }
            }
        }


        Piece ball;
        public void InitializeBall()
        {
            Block block = GetGridBlock(RandomNumber(0, 8), -5);
            ball = new Piece(block.X, block.Y);

            block.Holds = Holds.Ball;
            block.Background = getMyImage(Holds.Ball);
        }
        

        public void BlockerMove(Move move)
        {
            int newx = 0;


            if (move == Move.left)
            {
                newx -= 1;
                LastMove = Move.left;
            }
            else
            {
                newx += 1;
               LastMove = Move.right;
            }
            bool canMove = true;

            foreach (Piece pc in BlockerPieces)
            {
                if (pc.X + newx > 12 || pc.X + newx < 0)
                {
                    canMove = false;
                    break;
                }
            }
            if (canMove && levelNotifier.Visibility == System.Windows.Visibility.Collapsed)
            {
                BlockerGoes(newx);
            }
        }
        public void BlockerGoes(int value)
        {
            foreach (Piece pc in BlockerPieces)
            {
                Block bloc = GetGridBlock(pc.X, pc.Y);

                bloc.Holds = Holds.None;
                bloc.Background = getMyImage(Holds.None);
                
            }
            for(int m=0; m < BlockerPieces.Count; m++)
            {
                BlockerPieces[m].X += value;

                Block bloc = GetGridBlock(BlockerPieces[m].X, BlockerPieces[m].Y);

                bloc.Holds = Holds.Blocker;
                bloc.Background = getMyImage(Holds.Blocker);
            }
        }

        private void onMove(object sender, DragCompletedGestureEventArgs e)
        {
            if (e.HorizontalVelocity < 0)
            {
                Move move1 = Move.left;
                BlockerMove(move1);
            }

                // User flicked towards right
            else if (e.HorizontalVelocity > 0)
            {
                Move move = Move.right;
                BlockerMove(move);
                //moves right
            }
        }

        public int RandomNumber(int minimum, int limit)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            return rand.Next(minimum, limit);
        }
        public void InitiateBAllMovement()
        {
            ballMovement.Vertical = verticalMovement.up;

            ballMovement.Horizontal = GetHorizontalMovement(RandomNum());
        }
        public horizontalMovement GetHorizontalMovement(int value)
        {
            if (value == 1)
            {
                return horizontalMovement.left;
            }
            else
            {
                return horizontalMovement.right;
            }
        }
        BallMovement ballMovement;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            levelNotifier.Visibility = System.Windows.Visibility.Collapsed;
            Start();
        }
    }
}