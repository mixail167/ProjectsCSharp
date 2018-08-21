#define COUNT_MOVES

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Board values.
        private enum BoardValues
        {
            None = 0,
            Loss = 1,
            Draw = 2,
            Unknown = 3,
            Win = 4,
        }

        // Skill levels.
        private enum SkillLevels
        {
            Random,
            Beginner,
            Expert,
        }
        private SkillLevels SkillLevel = SkillLevels.Random;

        // The number of moves that have been made.
        private int NumSquaresTaken = 0;

        // The players' positions.
        private char CurrentPlayer = 'X';
        private char UserPlayer = 'X';
        private char ComputerPlayer = 'O';
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
            ResetGame('X', 'O');
        }

        // End.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Let the player be O.
        private void playOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame('O', 'X');

            // Let the computer move.
            MakeComputerMove();
        }

        // Let the player be X.
        private void playXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetGame('X', 'O');
        }

        // Prepare for a new game.
        private void ResetGame(char player, char computer)
        {
            Board = new char[3, 3];
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                {
                    BoardLabels[row, col].Text = "";
                    Board[row, col] = ' ';
                }
            UserPlayer = player;
            ComputerPlayer = computer;
            CurrentPlayer = 'X';
            NumSquaresTaken = 0;
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
            CurrentPlayer = ComputerPlayer;

            // Let the computer move.
            MakeComputerMove();
        }

        // Display a winner message.
        private void ShowWinner()
        {
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
        }

        // Return true if the player who just took spare [r, c] has won.
        private bool IsWinner(int r, int c)
        {
            bool isWinner = false;
            char player = Board[r, c];

            if ((player == Board[r, 0]) &&
                (player == Board[r, 1]) &&
                (player == Board[r, 2]))
            {
                isWinner = true;
            }
            else if ((player == Board[0, c]) &&
                     (player == Board[1, c]) &&
                     (player == Board[2, c]))
            {
                isWinner = true;
            }
            else if (r == c)
            {
                if ((player == Board[0, 0]) &&
                    (player == Board[1, 1]) &&
                    (player == Board[2, 2])) isWinner = true;
                else if ((player == Board[0, 2]) &&
                    (player == Board[1, 1]) &&
                    (player == Board[2, 0])) isWinner = true;
            }
            else if (r + c == 2)
            {
                if ((player == Board[0, 2]) &&
                    (player == Board[1, 1]) &&
                    (player == Board[2, 0])) isWinner = true;
            }

            return isWinner;
        }

#if COUNT_MOVES
        private int NumMovesTested;
#endif

        // Make the computer take a move.
        private void MakeComputerMove()
        {
#if COUNT_MOVES
            NumMovesTested = 0;
#endif

            int bestR, bestC;
            BoardValues bestValue;

            // Check the skill level.
            if (SkillLevel == SkillLevels.Random)
            {
                // Random moves.
                RandomMove(out bestR, out bestC);
            }
            else if (SkillLevel == SkillLevels.Beginner)
            {
                // Minimax looking 3 moves ahead.
                BoardValue(out bestValue, out bestR, out bestC,
                    ComputerPlayer, UserPlayer, 1, 3);
            }
            else
            {
                // Minimax looking 9 moves ahead.
                BoardValue(out bestValue, out bestR, out bestC,
                    ComputerPlayer, UserPlayer, 1, 9);
            }

#if COUNT_MOVES
            Console.WriteLine("Moves tested: " + NumMovesTested);
#endif

            // Make the move.
            Board[bestR, bestC] = ComputerPlayer;
            BoardLabels[bestR, bestC].Text = ComputerPlayer.ToString();
            NumSquaresTaken++;

            // See if there is a winner.
            if (IsWinner(bestR, bestC))
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

            // Switch whose move it is.
            CurrentPlayer = UserPlayer;
        }

        // Find the best board value for player1.
        private void BoardValue(out BoardValues bestValue, out int bestR, out int bestC, char player1, char player2, int depth, int maxDepth)
        {
            bestValue = BoardValues.Unknown;
            bestR = -1;
            bestC = -1;

            // If we are too deep, then we don't know.
            if ((depth > maxDepth) || (NumSquaresTaken == 9)) return;

            // Track the worst move for player2.
            BoardValues player2Value = BoardValues.Win;

            // Make test moves.
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    // See if this move is taken.
                    if (Board[row, col] == ' ')
                    {
#if COUNT_MOVES
                        NumMovesTested++;
#endif

                        // Try this move.
                        Board[row, col] = player1;
                        NumSquaresTaken++;

                        // See if this gives player1 a win.
                        if (IsWinner(row, col))
                        {
                            // This gives player1 a win and therefore player2 a loss.
                            // Take this move.
                            bestR = row;
                            bestC = col;
                            player2Value = BoardValues.Loss;
                        }
                        else
                        {
                            // Recursively try moves for player2.
                            int testR = -1;
                            int testC = -1;
                            BoardValues testValue = BoardValues.None;
                            BoardValue(out testValue, out testR, out testC,
                                player2, player1, depth + 1, maxDepth);

                            // See if this is an improvement for player 2.
                            if (player2Value >= testValue)
                            {
                                bestR = row;
                                bestC = col;
                                player2Value = testValue;
                            }
                        }

                        // Undo the move.
                        Board[row, col] = ' ';
                        NumSquaresTaken--;
                    }

                    // If player2 will lose, stop searching.
                    if (player2Value == BoardValues.Loss) break;
                }
                // If player2 will lose, stop searching.
                if (player2Value == BoardValues.Loss) break;
            } // End making test moves.

            // We now know the worst we can force player2 to do.
            // Convert that into a board value for player1.
            if (player2Value == BoardValues.Loss)
                bestValue = BoardValues.Win;
            else if (player2Value == BoardValues.Win)
                bestValue = BoardValues.Loss;
            else
                bestValue = player2Value;

            // Parameters bestValue, bestR, and bestC contain the best move we found.
        }

        private void RandomMove(out int bestR, out int bestC)
        {
#if COUNT_MOVES
            NumMovesTested++;
#endif
            bestR = -1;
            bestC = -1;

            // Pick a random move.
            Random rand = new Random();
            int move = rand.Next(0, 9 - NumSquaresTaken);

            // Find that move.
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Board[row, col] == ' ')
                    {
                        move--;
                        if (move < 0)
                        {
                            bestR = row;
                            bestC = col;
                            break;
                        }
                    }
                }
                if (bestR >= 0) break;
            }
        }

        // Set the skill level.
        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            randomToolStripMenuItem.Checked = false;
            expertToolStripMenuItem.Checked = false;
            SkillLevel = SkillLevels.Beginner;
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            beginnerToolStripMenuItem.Checked = false;
            expertToolStripMenuItem.Checked = false;
            SkillLevel = SkillLevels.Random;
        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            beginnerToolStripMenuItem.Checked = false;
            randomToolStripMenuItem.Checked = false;
            SkillLevel = SkillLevels.Expert;
        }
    }
}
