// Models/FlappyBirdGame.cs

using System;
using System.Collections.Generic;

namespace CSharpGames.Models
{
    public class FlappyBirdGame
    {
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 600;

        public Bird Bird { get; set; }
        public List<Pipe> Pipes { get; set; }
        public int PipeWidth { get; set; } = 60;
        public int PipeGap { get; set; } = 250;
        public int FrameCount { get; set; } = 0;
        public int Score { get; set; } = 0;
        public bool Running { get; set; } = true;

        private Random _random = new Random();

        public FlappyBirdGame()
        {
            Bird = new Bird
            {
                X = 100,
                Y = Height / 2,
                Radius = 20,
                Velocity = 0,
                Gravity = 0.5,
                Lift = -10
            };
            Pipes = new List<Pipe>();
            CreatePipe();
        }

        public void CreatePipe()
        {
            int topHeight = _random.Next(50, Height - PipeGap - 50);
            int bottomHeight = Height - topHeight - PipeGap;
            Pipe pipe = new Pipe
            {
                Top = topHeight,
                Bottom = bottomHeight,
                X = Width
            };
            Pipes.Add(pipe);
        }

        public void UpdateGameState()
        {
            UpdateBird();
            UpdatePipes();
            CheckCollisions();
        }

        public void UpdateBird()
        {
            Bird.Velocity += Bird.Gravity;
            Bird.Y += Bird.Velocity;

            // Check for collision with top or bottom of the canvas
            if (Bird.Y > Height || Bird.Y < 0)
            {
                Running = false;
            }
        }

        public void UpdatePipes()
        {
            foreach (var pipe in Pipes)
            {
                pipe.X -= 2;
            }

            if (Pipes.Count > 0 && Pipes[0].X < -PipeWidth)
            {
                Pipes.RemoveAt(0);
                Score += 1;
            }

            FrameCount += 1;
            if (FrameCount % 100 == 0)
            {
                CreatePipe();
            }
        }

        public void CheckCollisions()
        {
            foreach (var pipe in Pipes)
            {
                if (Bird.X + Bird.Radius > pipe.X && Bird.X - Bird.Radius < pipe.X + PipeWidth)
                {
                    if (Bird.Y - Bird.Radius < pipe.Top || Bird.Y + Bird.Radius > Height - pipe.Bottom)
                    {
                        Running = false;
                    }
                }
            }
        }

        public void HandleKeyDown(int keyCode)
        {
            if (keyCode == 32) // Space bar
            {
                Bird.Velocity = Bird.Lift;
            }
        }

        public void ResetGame()
        {
            // Reinitialize the game state
            Bird = new Bird
            {
                X = 100,
                Y = Height / 2,
                Radius = 20,
                Velocity = 0,
                Gravity = 0.5,
                Lift = -10
            };
            Pipes.Clear();
            CreatePipe();
            FrameCount = 0;
            Score = 0;
            Running = true;
        }
    }

    public class Bird
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Radius { get; set; }
        public double Velocity { get; set; }
        public double Gravity { get; set; }
        public double Lift { get; set; }
    }

    public class Pipe
    {
        public int Top { get; set; }
        public int Bottom { get; set; }
        public double X { get; set; }
    }
}