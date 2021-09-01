using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLibrary.Models
{
    public enum GameState
    {
        AwaitingUsers,  // Pre-game when not all users are initialized
        Turn,           // When the game is expecting a turn to be entered
        Tie,            // When the game has ended in a tie
        Win             // When the game has ended with a winner
    }

    public enum BoardMark
    {
        Empty,
        X,
        O
    }
}
