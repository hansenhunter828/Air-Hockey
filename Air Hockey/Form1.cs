using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Air_Hockey
{
    public partial class Form1 : Form
    {
        #region Variables
        #region Player Variables
        int player1X = 172;
        int player1Y = 438;
        int player1Score = 0;

        int player2X = 172;
        int player2Y = 18;
        int player2Score = 0;

        int paddleHeight = 40;
        int paddleWidth = 40;
        int paddleSpeed = 6;
        #endregion
        #region Puck Variables
        int puckX = 177;
        int puckY = 239;
        int puckXSpeed = 6;
        int puckYSpeed = 6;
        int puckHeight = 25;
        int puckWidth = 25;
        int accelTime;
        #endregion
        #region Controls Variables
        bool wDown = false;
        bool aDown = false;
        bool sDown = false;
        bool dDown = false;

        bool upArrowDown = false;
        bool leftArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;
        #endregion
        #region Brushes And Pens
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush greyBrush = new SolidBrush(Color.Gray);

        Pen redPen = new Pen(Color.Red);
        Pen bluePen = new Pen(Color.Blue);
        Pen blackPen = new Pen(Color.Black);
        Pen greyPen = new Pen(Color.Gray);
        #endregion
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            #region Player Controls
            switch (e.KeyCode)
            {
                #region Player 1 Controls
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                #endregion
                #region Player 2 Controls

                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                    #endregion
            }
            #endregion
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            #region Player Controls
            switch (e.KeyCode)
            {
                #region Player 1 Controls
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                #endregion
                #region Player 2 Controls
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                    #endregion
            }
            #endregion
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            int X = puckX;
            int Y = puckY;

            Rectangle puckRec = new Rectangle(puckX, puckY, puckWidth, puckHeight);
            #region Puck Movement
            puckX += puckXSpeed;
            puckY += puckYSpeed;
            accelTime++;
           /* if (accelTime % 25 == 0)
            {
                if (puckXSpeed < 0)
                {
                    puckXSpeed += 1;
                }
                else if (puckXSpeed > 0)
                {
                    puckXSpeed -= 1;
                }
                if (puckYSpeed < 0)
                {
                    puckYSpeed += 1;
                }
                else if (puckYSpeed > 0)
                {
                    puckYSpeed -= 1;
                }
           
            }
           */
            #endregion
            #region Player Movement
            //Player 1 Controls
            if (wDown == true && player1Y > 0)
            {
                player1Y -= paddleSpeed;
            }
            if (aDown == true && player1X > 0)
            {
                player1X -= paddleSpeed;
            }
            if (sDown == true && player1Y < this.Height - paddleHeight)
            {
                player1Y += paddleSpeed;
            }
            if (dDown == true && player1X < 370 - paddleHeight)
            {
                player1X += paddleSpeed;
            }
            //Player 2 Controls
            if (upArrowDown == true && player2Y > 0)
            {
                player2Y -= paddleSpeed;
            }
            if (leftArrowDown == true && player2X > 0)
            {
                player2X -= paddleSpeed;
            }
            if (downArrowDown == true && player2Y < this.Height - paddleHeight)
            {
                player2Y += paddleSpeed;
            }
            if (rightArrowDown == true && player2X < this.Width - paddleHeight)
            {
                player2X += paddleSpeed;
            }
            #endregion
            #region Collisions
            if (puckX > 365 - puckHeight || puckX < 10)
            {
                puckXSpeed *= -1;
            }
            if (puckY < 10 || puckY > 480 - puckHeight)
            {
                puckYSpeed *= -1;
            }
            if (puckXSpeed != -1000)
            {
                Rectangle player1Rec = new Rectangle(player1X, player1Y, paddleWidth, paddleHeight);
                Rectangle player2Rec = new Rectangle(player2X, player2Y, paddleWidth, paddleHeight);

                if (player1Rec.IntersectsWith(puckRec))
                {
                    //puckXSpeed *= -1;
                    //puckX = puckX + puckXSpeed + 1;
                    puckX = X;
                    puckY = Y;
                    puckYSpeed *= -1;
                    puckXSpeed *= -1;
                    accelTime = 0;
                    /*if (wDown == true)
                    {
                        puckYSpeed = 6;
                    }
                    if (sDown == true)
                    {
                        puckYSpeed = -6;
                    }
                    if (aDown == true)
                    {
                        puckXSpeed = -6;
                    }
                    if (dDown == true)
                    {
                        puckXSpeed = 6;
                    }
                    */

                    //puckY = puckY + puckYSpeed + 1;
                }
                else if (player2Rec.IntersectsWith(puckRec))
                {
                    puckX = X;
                    puckY = Y;
                    puckYSpeed *= -1;
                    accelTime = 0;

                    /*if (upArrowDown == true)
                    {
                        puckYSpeed = 6;
                    }
                    if (downArrowDown == true)
                    {
                        puckYSpeed = -6;
                    }
                    if (leftArrowDown == true)
                    {
                        puckXSpeed = -6;
                    }
                    if (rightArrowDown == true)
                    {
                        puckXSpeed = 6;
                    }
                    //puckY = puckY + puckYSpeed + 1;
                    */
                }
            }
            #endregion
            #region Scoring
            Rectangle player2Net = new Rectangle(140, 0, 110, 15);
            Rectangle player1Net = new Rectangle(140, 475, 110, 15);
            if (puckRec.IntersectsWith(player1Net))
            {
                player2Score++;
                player2ScoreLabel.Text = $"{player2Score}";

                puckX = 177;
                puckY = 239;
            }
            if (puckRec.IntersectsWith(player2Net))
            {
                player1Score++;
                player1ScoreLabel.Text = $"{player1Score}";

                puckX = 177;
                puckY = 239;
            }
            if (player1Score == 3)
            {
                winLabel.Text = "Player 1 Won!";
                gameTimer.Enabled = false;
            }
            else if(player2Score == 3)
            {
                winLabel.Text = "Player 2 Won!";
                gameTimer.Enabled = false;
            }
            #endregion
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine(player1X);
            Console.WriteLine(player1Y);

            #region Rink Lines
            e.Graphics.DrawRectangle(bluePen, 0, 30, 500, 10);
            e.Graphics.FillRectangle(blueBrush, 0, 30, 500, 10);

            e.Graphics.DrawRectangle(bluePen, 0, 450, 500, 10);
            e.Graphics.FillRectangle(blueBrush, 0, 450, 500, 10);

            e.Graphics.DrawRectangle(redPen, 0, 250, 165, 10);
            e.Graphics.FillRectangle(redBrush, 0, 250, 165, 10);

            e.Graphics.DrawRectangle(redPen, 215, 250, 165, 10);
            e.Graphics.FillRectangle(redBrush, 215, 250, 165, 10);

            e.Graphics.DrawRectangle(redPen, 0, 125, 500, 10);
            e.Graphics.FillRectangle(redBrush, 0, 125, 500, 10);

            e.Graphics.DrawRectangle(redPen, 0, 375, 500, 10);
            e.Graphics.FillRectangle(redBrush, 0, 375, 500, 10);

            e.Graphics.DrawEllipse(redPen, 165, 230, 50, 50);
            #endregion
            #region Rink Border
            //Right And Left Sides
            e.Graphics.DrawRectangle(greyPen, 0, 0, 10, 600);
            e.Graphics.FillRectangle(greyBrush, 0, 0, 10, 600);
            
            e.Graphics.DrawRectangle(greyPen, 365, 0, 10, 600);
            e.Graphics.FillRectangle(greyBrush, 365, 0, 10, 600);
            //Top Side
            e.Graphics.DrawRectangle(greyPen, 0, 0, 140, 10);
            e.Graphics.FillRectangle(greyBrush, 0, 0, 140, 10);

            e.Graphics.DrawRectangle(greyPen, 250, 0, 140, 10);
            e.Graphics.FillRectangle(greyBrush, 250, 0, 140, 10);
            //Bottom Side
            e.Graphics.DrawRectangle(greyPen, 0, 478, 140, 10);
            e.Graphics.FillRectangle(greyBrush, 0, 478, 140, 10);

            e.Graphics.DrawRectangle(greyPen, 250, 478, 140, 10);
            e.Graphics.FillRectangle(greyBrush, 250, 478, 140, 10);
            #endregion
            #region Player And Puck
            e.Graphics.FillEllipse(blackBrush, puckX, puckY, puckWidth, puckHeight);
            e.Graphics.FillEllipse(blueBrush, player1X, player1Y, paddleWidth, paddleHeight);
            e.Graphics.FillEllipse(redBrush, player2X, player2Y, paddleWidth, paddleHeight);
            #endregion
        }
    }
}
