using System;
using System.Windows.Forms;

namespace CountTicTacToeBoards
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The players' positions.
        private char[,] Board = new char[3, 3];

        // The number of moves made.
        private int NumSquaresTaken = 0;

        // The number of wins for each player.
        private int NumXWins = 0;
        private int NumOWins = 0;
        private int NumTies = 0;

        private void countButton_Click(object sender, EventArgs e)
        {
            // Initialize the board.
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    Board[row, col] = ' ';

            NumSquaresTaken = 0;
            NumXWins = 0;
            NumOWins = 0;
            NumTies = 0;

            CountGames('X', 'O');

            xWinsTextBox.Text = NumXWins.ToString("n0");
            oWinsTextBox.Text = NumOWins.ToString("n0");
            tiesTextBox.Text = NumTies.ToString("n0");
            totalTextBox.Text = (NumXWins + NumOWins + NumTies).ToString("n0");
        }

        // Recursively examine all possible games.
        private void CountGames(char player1, char player2)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    // See if this position is taken.
                    if (Board[row, col] == ' ')
                    {
                        // Try this move.
                        Board[row, col] = player1;
                        NumSquaresTaken++;

                        // See if this ends the game.
                        if (IsWinner(row, col))
                        {
                            // Someone won.
                            if (player1 == 'X') NumXWins++;
                            else NumOWins++;
                        }
                        else if (NumSquaresTaken == 9)
                        {
                            // Cat's game.
                            NumTies++;
                        }
                        else
                        {
                            // The game is not over.
                            CountGames(player2, player1);
                        }

                        // Unmake the move.
                        Board[row, col] = ' ';
                        NumSquaresTaken--;
                    }
                }
            }
        }

        // Return true if the player who just took spare [r, c] has won.
        private bool IsWinner(int r, int c)
        {
            char player = Board[r, c];
            if ((player == Board[r, 0]) &&
                (player == Board[r, 1]) &&
                (player == Board[r, 2])) return true;
            if ((player == Board[0, c]) &&
                (player == Board[1, c]) &&
                (player == Board[2, c])) return true;
            if (r == c)
            {
                if ((player == Board[0, 0]) &&
                    (player == Board[1, 1]) &&
                    (player == Board[2, 2])) return true;
            }
            if (r + c == 2)
            {
                if ((player == Board[0, 2]) &&
                    (player == Board[1, 1]) &&
                    (player == Board[2, 0])) return true;
            }

            return false;
        }
    }
}
