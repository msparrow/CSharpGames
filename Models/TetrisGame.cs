using System;
using System.Collections.Generic;

namespace CSharpGames.Models
{
    public class TetrisGame
    {
        public int Cols { get; set; } = 10;
        public int Rows { get; set; } = 20;
        public int CellSize { get; set; } = 30;
        public int Width => Cols * CellSize;
        public int Height => Rows * CellSize;

        public int[][] Grid { get; set; }
        public Tetromino CurrentPiece { get; set; }
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        public bool GameOver { get; set; } = false;
        public double GameSpeed { get; set; } = 500;

        public List<Tetromino> Shapes { get; set; }

        public TetrisGame()
        {
            Grid = new int[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                Grid[i] = new int[Cols];
            }
            InitializeShapes();
            SpawnNewPiece();
        }

        private void InitializeShapes()
        {
            Shapes = new List<Tetromino>
            {
                new Tetromino { Shape = new int[][] { new int[] {1, 1, 1}, new int[] {0, 1, 0} }, Color = "red" },
                new Tetromino { Shape = new int[][] { new int[] {0, 1, 1}, new int[] {1, 1, 0} }, Color = "green" },
                new Tetromino { Shape = new int[][] { new int[] {1, 1, 0}, new int[] {0, 1, 1} }, Color = "yellow" },
                new Tetromino { Shape = new int[][] { new int[] {1, 1, 1, 1} }, Color = "orange" },
                new Tetromino { Shape = new int[][] { new int[] {1, 1}, new int[] {1, 1} }, Color = "purple" },
                new Tetromino { Shape = new int[][] { new int[] {1, 1, 1}, new int[] {1, 0, 0} }, Color = "white" },
                new Tetromino { Shape = new int[][] { new int[] {1, 1, 1}, new int[] {0, 0, 1} }, Color = "blue" }
            };
        }

        public void SpawnNewPiece()
        {
            Random rnd = new Random();
            CurrentPiece = Shapes[rnd.Next(Shapes.Count)];
            CurrentX = 3;
            CurrentY = 0;
            if (!ValidMove(CurrentPiece.Shape, CurrentX, CurrentY))
            {
                GameOver = true;
            }
        }

        public void RotatePiece()
        {
            int[][] rotatedShape = RotateMatrix(CurrentPiece.Shape);
            if (ValidMove(rotatedShape, CurrentX, CurrentY))
            {
                CurrentPiece.Shape = rotatedShape;
            }
        }

        private int[][] RotateMatrix(int[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            int[][] rotated = new int[cols][];
            for (int i = 0; i < cols; i++)
            {
                rotated[i] = new int[rows];
            }
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    rotated[x][rows - y - 1] = matrix[y][x];
                }
            }
            return rotated;
        }

        public bool ValidMove(int[][] shape, int offsetX, int offsetY)
        {
            int shapeRows = shape.Length;
            int shapeCols = shape[0].Length;
            for (int y = 0; y < shapeRows; y++)
            {
                for (int x = 0; x < shapeCols; x++)
                {
                    if (shape[y][x] != 0)
                    {
                        int newX = x + offsetX;
                        int newY = y + offsetY;
                        if (newX < 0 || newX >= Cols || newY >= Rows || (newY >= 0 && Grid[newY][newX] != 0))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public void FreezePiece()
        {
            int shapeRows = CurrentPiece.Shape.Length;
            int shapeCols = CurrentPiece.Shape[0].Length;
            for (int y = 0; y < shapeRows; y++)
            {
                for (int x = 0; x < shapeCols; x++)
                {
                    if (CurrentPiece.Shape[y][x] != 0 && CurrentY + y >= 0)
                    {
                        Grid[CurrentY + y][CurrentX + x] = 1; // You can store color index or value here
                    }
                }
            }
            ClearLines();
            SpawnNewPiece();
        }

        public void ClearLines()
        {
            int clearedLines = 0;
            for (int y = Rows - 1; y >= 0; y--)
            {
                bool fullLine = true;
                for (int x = 0; x < Cols; x++)
                {
                    if (Grid[y][x] == 0)
                    {
                        fullLine = false;
                        break;
                    }
                }
                if (fullLine)
                {
                    // Move all lines down
                    for (int row = y; row > 0; row--)
                    {
                        Grid[row] = (int[])Grid[row - 1].Clone();
                    }
                    // Clear top line
                    Grid[0] = new int[Cols];
                    y++; // Recheck same line
                    clearedLines++;
                }
            }
            if (clearedLines > 0)
            {
                GameSpeed *= 0.92;
            }
        }

        public void MoveDown()
        {
            if (ValidMove(CurrentPiece.Shape, CurrentX, CurrentY + 1))
            {
                CurrentY += 1;
            }
            else
            {
                FreezePiece();
            }
        }

        public void MoveLeft()
        {
            if (ValidMove(CurrentPiece.Shape, CurrentX - 1, CurrentY))
            {
                CurrentX -= 1;
            }
        }

        public void MoveRight()
        {
            if (ValidMove(CurrentPiece.Shape, CurrentX + 1, CurrentY))
            {
                CurrentX += 1;
            }
        }

        public void Drop()
        {
            while (ValidMove(CurrentPiece.Shape, CurrentX, CurrentY + 1))
            {
                CurrentY += 1;
            }
            FreezePiece();
        }

        public void Update()
        {
            if (GameOver)
            {
                return;
            }
            MoveDown();
        }
    }

    public class Tetromino
    {
        public int[][] Shape { get; set; }
        public string Color { get; set; }
    }
}