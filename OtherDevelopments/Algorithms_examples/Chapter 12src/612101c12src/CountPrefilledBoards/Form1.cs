using System;
using System.Windows.Forms;

namespace CountPrefilledBoards
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // The number of moves made.
        private int NumSquaresTaken = 0;

        // The number of wins for each player.
        private int NumXWins = 0;
        private int NumOWins = 0;
        private int NumTies = 0;

        // The current player.
        // The players' positions.
        private char CurrentPlayer = 'X';
        private char OtherPlayer = 'O';
        private char[,] Board = new char[3, 3];
        private Label[,] BoardLabels = new Label[3, 3];

        // Load the BoardLabels array.
        private void Form1_Load(object sender, EventArgs e)
        {
            BoardLabels[0, 0] = label00;
            BoardLabels[0, 1] = label01;
            BoardLabels[0, 2] = label02;
            BoardLabels[1, 0] = label10;
            BoardLabels[1, 1] = label11;
            BoardLabels[1, 2] = label12;
            BoardLabels[2, 0] = label20;
            BoardLabels[2, 1] = label21;
            BoardLabels[2, 2] = label22;
            Reset();
        }

        // Count the possible wins and ties from this position.
        private void countButton_Click(object sender, EventArgs e)
        {
            if (CurrentPlayer == ' ')
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            NumXWins = 0;
            NumOWins = 0;
            NumTies = 0;

            CountGames(CurrentPlayer, OtherPlayer);

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
                            // player1 won.
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
                    } // End if the sqaure is open.
                } // Next col
            }  // Next row
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

        // Reset.
        private void resetButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        // The user clicked a square.
        private void labelSquare_Click(object sender, EventArgs e)
        {
            // See if a game is in progress.
            if (CurrentPlayer == ' ') return;

            // Get the row and column.
            Label label = sender as Label;
            string name = label.Name.Replace("label", "");
            int row = int.Parse(name.Substring(0, 1));
            int col = int.Parse(name.Substring(1, 1));

            // See if the spot is already taken.
            if (Board[row, col] != ' ')
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }

            // Take this square.
            Board[row, col] = CurrentPlayer;
            label.Text = CurrentPlayer.ToString();
            NumSquaresTaken++;

            // See if there is a winner.    
            if (IsWinner(row, col))
            {
                ShowWinner();
                return;
            }
            else if (NumSquaresTaken == 9)
            {
                // We have a cat's game.
                ShowCatsGame();
                return;
            }

            // Switch players.
            char temp = CurrentPlayer;
            CurrentPlayer = OtherPlayer;
            OtherPlayer = temp;
        }

        // Display a winner message.
        private void ShowWinner()
        {
            if (CurrentPlayer == 'X')
            {
                xWinsTextBox.Text = "1";
                oWinsTextBox.Text = "0";
            }
            else
            {
                xWinsTextBox.Text = "0";
                oWinsTextBox.Text = "1";
            }
            tiesTextBox.Text = "0";
            totalTextBox.Text = "1";

            MessageBox.Show("Player " + CurrentPlayer + " wins!",
                CurrentPlayer + " Wins", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            CurrentPlayer = ' ';
        }

        // Display a cat's game message.
        private void ShowCatsGame()
        {
            MessageBox.Show("It's a tie!",
                "Cat's Game", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            CurrentPlayer = ' ';

            xWinsTextBox.Text = "0";
            oWinsTextBox.Text = "0";
            tiesTextBox.Text = "1";
            totalTextBox.Text = "1";
        }

        // Prepare for a new game.
        private void Reset()
        {
            Board = new char[3, 3];
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                {
                    BoardLabels[row, col].Text = "";
                    Board[row, col] = ' ';
                }
            CurrentPlayer = 'X';
            OtherPlayer = 'O';
            NumSquaresTaken = 0;
            xWinsTextBox.Clear();
            oWinsTextBox.Clear();
            tiesTextBox.Clear();
            totalTextBox.Clear();
        }
    }
}
