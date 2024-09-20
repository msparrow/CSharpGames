// Models/TicTacToeGame.cs

using System;
using System.Collections.Generic;

namespace CSharpGames.Models
{
    public class TicTacToeGame
    {
        public int Width { get; set; } = 300;
        public int Height { get; set; } = 300;
        public int CellSize { get; set; } = 100;
        public string[][] Board { get; set; }
        public string CurrentPlayer { get; set; } = "X";
        public bool Running { get; set; } = true;

        public TicTacToeGame()
        {
            Board = new string[3][];
            for (int i = 0; i < 3; i++)
            {
                Board[i] = new string[3];
                for (int j = 0; j < 3; j++)
                {
                    Board[i][j] = "";
                }
            }
        }

        public bool HandleClick(double mouseX, double mouseY)
        {
            if (!Running)
                return false;

            int col = (int)(mouseX / CellSize);
            int row = (int)(mouseY / CellSize);

            if (row < 0 || row >= 3 || col < 0 || col >= 3)
                return false;

            if (Board[row][col] == "")
            {
                Board[row][col] = CurrentPlayer;
                if (CheckWinner(CurrentPlayer))
                {
                    Running = false;
                    return true; // Indicates a win
                }
                else if (CheckDraw())
                {
                    Running = false;
                    return true; // Indicates a draw
                }
                else
                {
                    CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";
                }
            }
            return false;
        }

        public bool CheckWinner(string player)
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (Board[row][0] == player && Board[row][1] == player && Board[row][2] == player)
                    return true;
            }

            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (Board[0][col] == player && Board[1][col] == player && Board[2][col] == player)
                    return true;
            }

            // Check diagonals
            if (Board[0][0] == player && Board[1][1] == player && Board[2][2] == player)
                return true;

            if (Board[0][2] == player && Board[1][1] == player && Board[2][0] == player)
                return true;

            return false;
        }

        public bool CheckDraw()
        {
            foreach (var row in Board)
            {
                foreach (var cell in row)
                {
                    if (cell == "")
                        return false;
                }
            }
            return true;
        }

        public void ResetGame()
        {
            Board = new string[3][];
            for (int i = 0; i < 3; i++)
            {
                Board[i] = new string[3];
                for (int j = 0; j < 3; j++)
                {
                    Board[i][j] = "";
                }
            }
            CurrentPlayer = "X";
            Running = true;
        }
    }
}