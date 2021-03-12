using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Air_Hockey
{
    // Hunter Hansen
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
        int puckXSpeed = 0;
        int puckYSpeed = 0;
        int puckHeight = 25;
        int puckWidth = 25;
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
        #region SoundPlayers
        SoundPlayer hit = new SoundPlayer(Properties.Resources.hit);
        SoundPlayer goal = new SoundPlayer(Properties.Resources.goal);
        SoundPlayer gameWin = new SoundPlayer(Properties.Resources.gameWin);
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
            #region Puck Movement
            puckX += puckXSpeed;
            puckY += puckYSpeed;
            #endregion
            #region Player Movement
            #region Player 1 Movement
            //Player 1 Controls
            if (wDown == true && player1Y > 260)
            {
                player1Y -= paddleSpeed;
            }
            if (aDown == true && player1X > 10)
            {
                player1X -= paddleSpeed;
            }
            if (sDown == true && player1Y < this.Height - 10 - paddleHeight)
            {
                player1Y += paddleSpeed;
            }
            if (dDown == true && player1X < this.Width - 10 - paddleHeight)
            {
                player1X += paddleSpeed;
            }
            #endregion
            #region Player 2 Movement
            //Player 2 Controls
            if (upArrowDown == true && player2Y > 10)
            {
                player2Y -= paddleSpeed;
            }
            if (leftArrowDown == true && player2X > 10)
            {
                player2X -= paddleSpeed;
            }
            if (downArrowDown == true && player2Y < 260 - 10 - paddleHeight)
            {
                player2Y += paddleSpeed;
            }
            if (rightArrowDown == true && player2X < this.Width - 10 - paddleHeight)
            {
                player2X += paddleSpeed;
            }
            #endregion
            #endregion
            #region Collisions
            #region Puck Collisions
            //Wall Puck Collisions
            if (puckX > this.Width - 20 - puckHeight || puckX < 10)
            {
                puckXSpeed *= -1;
            }
            if (puckY < 10 || puckY > this.Height - 20 - puckHeight)
            {
                puckYSpeed *= -1;
            }
            #endregion
            #region Player Puck Collisions
            //Player Puck Collisions
            if (puckXSpeed != -1000)
            {
                #region Player And Puck Hitboxes
                Rectangle player1Rec = new Rectangle(player1X, player1Y, paddleWidth, paddleHeight);
                Rectangle player2Rec = new Rectangle(player2X, player2Y, paddleWidth, paddleHeight);
                Rectangle puckRec = new Rectangle(puckX + 10, puckY, 5, puckHeight);
                Rectangle puckLeft = new Rectangle(puckX + 2, puckY, 2, puckHeight);
                Rectangle puckRight = new Rectangle(puckX + 23, puckY, 2, puckHeight);
                #endregion
                #region PLayer 1 Puck Collisions
                if (player1Rec.IntersectsWith(puckRec))
                {
                    if (puckXSpeed == 0)
                    {
                        puckXSpeed = 6;
                        puckYSpeed = 6;
                    }
                    else
                    {
                       puckYSpeed *= -1;

                       puckX = X;
                       puckY = Y;
                       hit.Play();
                    }
                }
                else if(player1Rec.IntersectsWith(puckLeft) || player1Rec.IntersectsWith(puckRight))
                {
                    if (puckXSpeed == 0)
                    {
                        puckXSpeed = 6;
                        puckYSpeed = 6;
                    }
                    else
                    {
                        puckXSpeed *= -1;

                         puckX = X;
                         puckY = Y;
                         hit.Play();
                    }
                }
                #endregion
                #region Player 2 Puck Collisions
                if (player2Rec.IntersectsWith(puckRec))
                {
                    if (puckXSpeed == 0)
                    {
                        puckXSpeed = 6;
                        puckYSpeed = 6;
                    }
                    else
                    {
                        puckYSpeed *= -1;

                        puckX = X;
                        puckY = Y;
                        hit.Play();
                    }
                }
                else if (player2Rec.IntersectsWith(puckLeft) || player2Rec.IntersectsWith(puckRight))
                {
                    if (puckXSpeed == 0)
                    {
                        puckXSpeed = 6;
                        puckYSpeed = 6;
                    }
                    else
                    {
                        puckXSpeed *= -1;

                        puckX = X;
                        puckY = Y;
                        hit.Play();
                    }
                }
                #endregion
               
            }
            #endregion
            #endregion
            #region Scoring
            #region Hit Boxes
            Rectangle player2Net = new Rectangle(140, 0, 110, 15);
            Rectangle player1Net = new Rectangle(140, 475, 110, 15);
            Rectangle puckGoal = new Rectangle(puckX , puckY, puckWidth, puckHeight);
            #endregion
            #region Player 1 Goal
            if (puckGoal.IntersectsWith(player1Net))
            {
                player2Score++;
                player2ScoreLabel.Text = $"{player2Score}";

                puckX = 177;
                puckY = 239;

                puckYSpeed = 0;
                puckXSpeed = 0;
                goal.Play();
            }
            #endregion
            #region Player 2 Goal
            if (puckGoal.IntersectsWith(player2Net))
            {
                player1Score++;
                player1ScoreLabel.Text = $"{player1Score}";

                puckX = 177;
                puckY = 239;

                puckYSpeed = 0;
                puckXSpeed = 0;
                goal.Play();
            }
            #endregion
            #region Game Winning
            if (player1Score == 3)
            {
                winLabel.Text = "Player 1 Won!";
                gameWin.Play();
                gameTimer.Enabled = false;
            }
            else if (player2Score == 3)
            {
                winLabel.Text = "Player 2 Won!";
                gameWin.Play();
                gameTimer.Enabled = false;
            }
            #endregion
            #endregion
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
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
            
            e.Graphics.DrawRectangle(greyPen, this.Width - 10, 0, 10, 600);
            e.Graphics.FillRectangle(greyBrush, this.Width - 10, 0, 10, 600);
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