// Models/SpaceInvadersGame.cs

using System;
using System.Collections.Generic;

namespace CSharpGames.Models
{
    public class SpaceInvadersGame
    {
        // Game dimensions
        public int Width { get; set; } = 800;
        public int Height { get; set; } = 600;

        // Ship properties
        public Ship Ship { get; set; }

        // Bullets
        public List<Bullet> Bullets { get; set; }

        // Aliens
        public List<Alien> Aliens { get; set; }

        // Game state
        public bool Running { get; set; } = true;
        public int AliensRemaining => Aliens.FindAll(a => a.Alive).Count;

        // Movement direction
        public double DirectionX { get; set; } = 0;

        public SpaceInvadersGame()
        {
            Ship = new Ship
            {
                X = Width / 2 - 20,
                Y = Height - 50,
                Width = 40,
                Height = 10,
                Speed = 10
            };

            Bullets = new List<Bullet>();

            Aliens = CreateAliens();
        }

        public List<Alien> CreateAliens()
        {
            var aliens = new List<Alien>();
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var alien = new Alien
                    {
                        X = col * 60 + 30,
                        Y = row * 40 + 30,
                        Width = 40,
                        Height = 20,
                        Alive = true
                    };
                    aliens.Add(alien);
                }
            }
            return aliens;
        }

        public void UpdateGameState()
        {
            MoveShip();
            MoveBullets();
            CheckCollisions();
            UpdateAliens();
        }

        public void MoveShip()
        {
            Ship.X += DirectionX;
            Ship.X = Math.Max(0, Math.Min(Ship.X, Width - Ship.Width));
        }

        public void MoveBullets()
        {
            foreach (var bullet in Bullets)
            {
                bullet.Y -= bullet.Speed;
            }
            Bullets.RemoveAll(b => b.Y < 0);
        }

        public void CheckCollisions()
        {
            foreach (var alien in Aliens)
            {
                if (alien.Alive)
                {
                    foreach (var bullet in Bullets)
                    {
                        if (bullet.X >= alien.X && bullet.X <= alien.X + alien.Width &&
                            bullet.Y >= alien.Y && bullet.Y <= alien.Y + alien.Height)
                        {
                            alien.Alive = false;
                            bullet.Y = -1; // Mark bullet for removal
                            break; // Move to the next alien
                        }
                    }
                }
            }
            Bullets.RemoveAll(b => b.Y < 0);
        }

        public void UpdateAliens()
        {
            foreach (var alien in Aliens)
            {
                if (alien.Alive)
                {
                    alien.Y += 0.5; // Move aliens down slowly
                    if (alien.Y >= Height - alien.Height)
                    {
                        Running = false; // Game over
                    }
                }
            }
        }

        public void HandleKeyDown(int keyCode)
        {
            // Left arrow key
            if (keyCode == 37)
            {
                DirectionX = -Ship.Speed;
            }
            // Right arrow key
            else if (keyCode == 39)
            {
                DirectionX = Ship.Speed;
            }
            // Space bar
            else if (keyCode == 32)
            {
                ShootBullet();
            }
        }

        public void HandleKeyUp(int keyCode)
        {
            if (keyCode == 37 || keyCode == 39)
            {
                DirectionX = 0;
            }
        }

        public void ShootBullet()
        {
            var bullet = new Bullet
            {
                X = Ship.X + Ship.Width / 2 - 2,
                Y = Ship.Y,
                Width = 4,
                Height = 10,
                Speed = 5
            };
            Bullets.Add(bullet);
        }

        public void ResetGame()
        {
            // Reinitialize the game state
            Ship.X = Width / 2 - 20;
            Ship.Y = Height - 50;

            Bullets.Clear();

            Aliens = CreateAliens();

            Running = true;
            DirectionX = 0;
        }
    }

    public class Ship
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Speed { get; set; }
    }

    public class Bullet
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Speed { get; set; }
    }

    public class Alien
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Alive { get; set; }
    }
}