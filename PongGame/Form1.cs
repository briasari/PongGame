using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongGame
{
    public partial class Form1 : Form
    {

        Rectangle player1 = new Rectangle(10, 140, 10, 60);
        Rectangle player2 = new Rectangle(10, 210, 10, 60);
        Rectangle border = new Rectangle(8, 208, 14, 64);
        Rectangle ball = new Rectangle(295, 195, 10, 10);

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 4;
        int ballXSpeed = 6;
        int ballYSpeed = -6;

        int round = 1;

        bool wPressed = false;
        bool aPressed = false;
        bool sPressed = false;
        bool dPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ball.X = ball.X + ballXSpeed;
            ball.Y = ball.Y + ballYSpeed;

            if (ball.Y <= 0 || ball.Y >= this.Height - ball.Height)
            {
                ballYSpeed = -ballYSpeed;
            }
            if (ball.X <= 0 || ball.X >= this.Width - ball.Width)
            {
                ballXSpeed = -ballXSpeed;
            }

            //move player1
            if (wPressed == true && player1.Y > 0)
            {
                player1.Y = player1.Y - playerSpeed;
            }
            if (sPressed == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y = player1.Y + playerSpeed;
            }
            if (dPressed == true && player1.X < this.Width - player1.Width)
            {
                player1.X = player1.X + playerSpeed;
            }
            if (aPressed == true && player1.X > 0)
            {
                player1.X = player1.X - playerSpeed;
            }

            //move player2
            if (upPressed == true && player2.Y > 0)
            {
                player2.Y = player2.Y - playerSpeed;
            }
            if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y = player2.Y + playerSpeed;
            }
            if (rightPressed == true && player1.X < this.Width - player2.Width)
            {
                player2.X = player2.X + playerSpeed;
            }
            if (leftPressed == true && player2.X > 0)
            {
                player2.X = player2.X - playerSpeed;
            }

            //check for intersection with border depending on round
            if (round % 2 == 0)
            {
                border.X = player2.X - 2;
                border.Y = player2.Y - 2;

                if (border.IntersectsWith(ball))
                {
                    ballXSpeed = -ballXSpeed;
                    ball.X = player2.X + player2.Width;
                    round++;
                }
            }
            else
            {
                border.X = player1.X -2;
                border.Y = player1.Y -2;

                if (border.IntersectsWith(ball))
                {
                    ballXSpeed = -ballXSpeed;
                    ball.X = player1.X + player1.Width;
                    round++;
                }
            }



            //check if ball goes off left side of screen
            if (ball.X <= 0)
            {
                if (border.X == player2.X - 2)
                {
                    player1Score++;
                    p1ScoreLabel.Text = $"{player1Score}";
                    ball.X = 295;
                    ball.Y = 195;
                }
                else
                {
                    player2Score++;
                    p2ScoreLabel.Text = $"{player2Score}";
                    ball.X = 295;
                    ball.Y = 195;
                }

                int randValue = randGen.Next(1, 3);
                if (randValue == 1)
                {
                    ballYSpeed = -6;
                }
                else
                {
                    ballYSpeed = 6;
                }
            }

            //check if ball goes off right side of screen
            if (ball.X == this.Width)
            {
                ballXSpeed = -ballXSpeed;
            }



            //check for winner
            if (player1Score == 3)
            {
                winLabel.Text = "Player 1 Wins!";
                gameTimer.Stop();
            }

            if (player2Score == 3)
            {
                winLabel.Text = "Player 2 Wins!";
                gameTimer.Stop();
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, border);
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
        }
    }
}
