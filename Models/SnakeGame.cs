// Models/SnakeGame.cs

using System;
using System.Collections.Generic;

namespace CSharpGames.Models
{
    public class SnakeGame
    {
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 600;
        public List<Coordinate> Snake { get; set; }
        public Coordinate Direction { get; set; }
        public Coordinate Food { get; set; }
        public bool Running { get; set; }
        public double SnakeSpeed { get; set; } = 100;
        public int FoodEaten { get; set; }

        private Random _random = new Random();

        public SnakeGame()
        {
            Snake = new List<Coordinate> { new Coordinate(100, 100) };
            Direction = new Coordinate(20, 0); // Increased step size
            Food = RandomFood();
            Running = true;
            FoodEaten = 0;
        }

        public Coordinate RandomFood()
        {
            int x = _random.Next(0, (Width - 20) / 20) * 20;
            int y = _random.Next(0, (Height - 20) / 20) * 20;
            return new Coordinate(x, y);
        }

        public void Update()
        {
            int headX = Snake[0].X;
            int headY = Snake[0].Y;
            int moveX = Direction.X;
            int moveY = Direction.Y;
            Coordinate newHead = new Coordinate(headX + moveX, headY + moveY);

            if (Snake.Contains(newHead) || !IsWithinBounds(newHead))
            {
                Running = false;
                return;
            }

            Snake.Insert(0, newHead);

            if (newHead.Equals(Food))
            {
                Food = RandomFood();
                FoodEaten += 1;
                SnakeSpeed *= 0.95;
            }
            else
            {
                Snake.RemoveAt(Snake.Count - 1);
            }
        }

        public void ChangeDirection(int keyCode)
        {
            // Prevent reversing direction
            if (keyCode == 37 && Direction != new Coordinate(20, 0)) // Left arrow
            {
                Direction = new Coordinate(-20, 0);
            }
            else if (keyCode == 38 && Direction != new Coordinate(0, 20)) // Up arrow
            {
                Direction = new Coordinate(0, -20);
            }
            else if (keyCode == 39 && Direction != new Coordinate(-20, 0)) // Right arrow
            {
                Direction = new Coordinate(20, 0);
            }
            else if (keyCode == 40 && Direction != new Coordinate(0, -20)) // Down arrow
            {
                Direction = new Coordinate(0, 20);
            }
        }

        private bool IsWithinBounds(Coordinate point)
        {
            return point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
        }
    }

    public class Coordinate : IEquatable<Coordinate>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate() { }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinate other)
        {
            if (other == null)
                return false;

            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Coordinate);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}