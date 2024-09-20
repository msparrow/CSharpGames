// Models/PongGame.cs

using System;

namespace CSharpGames.Models
{
    public class PongGame
    {
        // Game dimensions
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 600;

        // Paddle properties
        public int PaddleWidth { get; set; } = 10;
        public int PaddleHeight { get; set; } = 100;
        public double LeftPaddleY { get; set; }
        public double RightPaddleY { get; set; }

        // Ball properties
        public double BallX { get; set; }
        public double BallY { get; set; }
        public double BallSpeedX { get; set; } = 4;
        public double BallSpeedY { get; set; } = 4;
        public int BallSize { get; set; } = 10;

        // Paddle movement
        public double PaddleSpeed { get; set; } = 6;
        public bool UpPressed { get; set; } = false;
        public bool DownPressed { get; set; } = false;

        // Game state
        public bool Running { get; set; } = true;

        public PongGame()
        {
            LeftPaddleY = (Height - PaddleHeight) / 2;
            RightPaddleY = (Height - PaddleHeight) / 2;
            BallX = Width / 2;
            BallY = Height / 2;
        }

        public void Update()
        {
            // Move ball
            BallX += BallSpeedX;
            BallY += BallSpeedY;

            // Ball collision with top and bottom
            if (BallY - BallSize <= 0 || BallY + BallSize >= Height)
            {
                BallSpeedY *= -1;
            }

            // Ball collision with paddles
            if ((BallX - BallSize <= PaddleWidth && BallY >= LeftPaddleY && BallY <= LeftPaddleY + PaddleHeight) ||
                (BallX + BallSize >= Width - PaddleWidth && BallY >= RightPaddleY && BallY <= RightPaddleY + PaddleHeight))
            {
                BallSpeedX *= -1;
                IncreaseSpeed();
            }

            // Ball out of bounds
            if (BallX - BallSize <= 0 || BallX + BallSize >= Width)
            {
                ResetBall();
            }

            // Move right paddle based on user input
            if (UpPressed && RightPaddleY > 0)
            {
                RightPaddleY -= PaddleSpeed;
            }
            if (DownPressed && RightPaddleY < Height - PaddleHeight)
            {
                RightPaddleY += PaddleSpeed;
            }

            // Move left paddle automatically (AI)
            if (BallY < LeftPaddleY + PaddleHeight / 2)
            {
                LeftPaddleY -= PaddleSpeed;
            }
            else if (BallY > LeftPaddleY + PaddleHeight / 2)
            {
                LeftPaddleY += PaddleSpeed;
            }

            // Keep left paddle within bounds
            LeftPaddleY = Math.Max(0, Math.Min(LeftPaddleY, Height - PaddleHeight));
        }

        public void IncreaseSpeed()
        {
            BallSpeedX *= 1.04;
            BallSpeedY *= 1.04;
            PaddleSpeed *= 1.05;
        }

        public void ResetBall()
        {
            BallX = Width / 2;
            BallY = Height / 2;
            BallSpeedX = 4 * (BallSpeedX > 0 ? 1 : -1); // Keep direction
            BallSpeedY = 4 * (BallSpeedY > 0 ? 1 : -1);
            PaddleSpeed = 6;
        }

        public void ChangeDirection(int keyCode, bool isPressed)
        {
            // Key down events
            if (keyCode == 38) // Up arrow
            {
                UpPressed = isPressed;
            }
            else if (keyCode == 40) // Down arrow
            {
                DownPressed = isPressed;
            }
        }
    }
}