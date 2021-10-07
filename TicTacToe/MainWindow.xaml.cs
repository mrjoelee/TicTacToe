using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //creating variabls for the game
        #region Private Members
        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;
        /// <summary>
        /// True if it is player 1's turn(X) or player's 2 turn (O)
        /// </summary>
        private bool mPlayer1Turn;
        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameended;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }


        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to start
        /// </summary>
        private void NewGame()
        {
            //Create a new blank array of free cells (9 cells)
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure Player 1 starts the game
            mPlayer1Turn = true;

            //Iterate every button on the grid..the "Container" is the grid which consists all the buttons
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Red;

            });

            //Make sure the game hasn't finished
            mGameended = false;
        }


        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">the events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after it finished
            if (mGameended)
            {
                NewGame();
                return;
            }
            // cast the sender to a button
            var button = (Button)sender;

            // find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //3 is rom the # of columns
            var index = column + (row * 3);

            //don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // set the cell value based on which players turn it is
            //if player1turn is true then cross else(:) than nought - compact way
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            //normal way
            //if(mPlayer1Turn)
            //mResults[index] = MarkType.Cross;
            //else
            //mResults[index] = MarkType.Nought;

            // set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // change noughts to green
            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Blue;
            }

            //Toggle the players turn
            mPlayer1Turn ^= true;
            //if (mPlayer1Turn)
            //{
            //    mPlayer1Turn = false;
            //}
            //else
            //{
            //    mPlayer1Turn = true;
            //}

            //check for a winner
            CheckForWinner();
        }
        /// <summary>
        /// Checks if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins

            // check for horizontal wins
            //row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults [2]) == mResults[0])
            {
                //Game ends
                mGameended = true;

                //highlight winning cells in Green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game ends
                mGameended = true;

                //highlight winning cells in Green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game ends
                mGameended = true;

                //highlight winning cells in Green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Vertical Wins
            // check for vertical wins
            //column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game ends
                mGameended = true;

                //highlight winning cells in Green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //column 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game ends
                mGameended = true;

                //highlight winning cells in Green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //column 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game ends
                mGameended = true;

                //highlight winning cells in Green
                Button2_0.Background = Button2_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal wins
            //check for  diagonal wins
            //top left bottom right
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game ends
                mGameended = true;

                //highlight winning cells in Green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //top right to bottom left
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game ends
                mGameended = true;

                //highlight winning cells in Green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion

            #region No winners
            // Check for no winner and full board
            if (!mResults.Any(result => result == MarkType.Free))
            {
                //Game ends
                mGameended = true;

                //turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });

            }
            #endregion
        }
    }

}
