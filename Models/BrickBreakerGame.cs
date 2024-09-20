// Models/BrickBreakerGame.cs

using System;
using System.Collections.Generic;

namespace CSharpGames.Models
{
    public class BrickBreakerGame
    {
        // Game dimensions
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 600;

        // Paddle properties
        public Paddle Paddle { get; set; }

        // Ball properties
        public Ball Ball { get; set; }

        // Bricks
        public List<Brick> Bricks { get; set; }

        // Game state
        public bool Running { get; set; } = true;
        public int BricksDestroyed { get; set; } = 0;

        // Input keys
        public Dictionary<string, bool> Keys { get; set; }

        private Random _random = new Random();

        public BrickBreakerGame()
        {
            Paddle = new Paddle
            {
                X = Width / 2 - 50,
                Y = Height - 30,
                Width = 100,
                Height = 10,
                Speed = 10,
                Velocity = 0
            };

            Ball = new Ball
            {
                X = Width / 2,
                Y = Height / 2,
                Radius = 10,
                DX = _random.Next(0, 2) == 0 ? -2 : 2,
                DY = _random.Next(0, 2) == 0 ? -2 : 2
            };

            Bricks = CreateBricks();
            Keys = new Dictionary<string, bool>
            {
                { "ArrowLeft", false },
                { "ArrowRight", false }
            };
        }

        public List<Brick> CreateBricks()
        {
            var bricks = new List<Brick>();
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var brick = new Brick
                    {
                        X = col * 80 + 10,
                        Y = row * 30 + 30,
                        Width = 70,
                        Height = 20,
                        Hit = false
                    };
                    bricks.Add(brick);
                }
            }
            return bricks;
        }

        public void UpdateGameState()
        {
            UpdatePaddleVelocity();
            MovePaddle();
            MoveBall();
            CheckCollisions();
        }

        public void UpdatePaddleVelocity()
        {
            if (Keys["ArrowLeft"])
            {
                Paddle.Velocity = -Paddle.Speed;
            }
            else if (Keys["ArrowRight"])
            {
                Paddle.Velocity = Paddle.Speed;
            }
            else
            {
                Paddle.Velocity = 0;
            }
        }

        public void MovePaddle()
        {
            Paddle.X += Paddle.Velocity;
            Paddle.X = Math.Max(0, Math.Min(Paddle.X, Width - Paddle.Width));
        }

        public void MoveBall()
        {
            Ball.X += Ball.DX;
            Ball.Y += Ball.DY;

            // Check for wall collisions
            if (Ball.X - Ball.Radius <= 0 || Ball.X + Ball.Radius >= Width)
            {
                Ball.DX *= -1;
            }
            if (Ball.Y - Ball.Radius <= 0)
            {
                Ball.DY *= -1;
            }
            if (Ball.Y + Ball.Radius >= Height)
            {
                Running = false; // Game over
            }
        }

        public void CheckCollisions()
        {
            // Check for paddle collision
            if (Ball.X >= Paddle.X && Ball.X <= Paddle.X + Paddle.Width &&
                Ball.Y + Ball.Radius >= Paddle.Y && Ball.Y - Ball.Radius <= Paddle.Y + Paddle.Height)
            {
                Ball.DY *= -1;
                double hitPos = (Ball.X - Paddle.X) / Paddle.Width;
                Ball.DX = (hitPos - 0.5) * 5; // Adjust angle based on where it hits the paddle
            }

            // Check for brick collisions
            foreach (var brick in Bricks)
            {
                if (!brick.Hit)
                {
                    if (Ball.X >= brick.X && Ball.X <= brick.X + brick.Width &&
                        Ball.Y + Ball.Radius >= brick.Y && Ball.Y - Ball.Radius <= brick.Y + brick.Height)
                    {
                        Ball.DY *= -1;
                        brick.Hit = true;
                        BricksDestroyed += 1;
                        IncreaseBallSpeed();
                        break; // Exit after first collision
                    }
                }
            }
        }

        public void IncreaseBallSpeed()
        {
            Ball.DX *= 1.05;
            Ball.DY *= 1.05;
        }

        public void HandleKeyDown(string key)
        {
            if (Keys.ContainsKey(key))
            {
                Keys[key] = true;
            }
        }

        public void HandleKeyUp(string key)
        {
            if (Keys.ContainsKey(key))
            {
                Keys[key] = false;
            }
        }

        public void ResetGame()
        {
            // Reinitialize the game state
            Paddle.X = Width / 2 - 50;
            Paddle.Y = Height - 30;
            Paddle.Velocity = 0;

            Ball.X = Width / 2;
            Ball.Y = Height / 2;
            Ball.DX = _random.Next(0, 2) == 0 ? -2 : 2;
            Ball.DY = _random.Next(0, 2) == 0 ? -2 : 2;

            Bricks = CreateBricks();
            Running = true;
            BricksDestroyed = 0;
        }
    }

    public class Paddle
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Speed { get; set; }
        public double Velocity { get; set; }
    }

    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Radius { get; set; }
        public double DX { get; set; }
        public double DY { get; set; }
    }

    public class Brick
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Hit { get; set; }
    }
}
