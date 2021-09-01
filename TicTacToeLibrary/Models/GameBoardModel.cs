using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLibrary.Models
{
    public class GameBoardModel
    {
        public BoardMark[] BoardSpots { get; set; } =
        {
            BoardMark.Empty, BoardMark.Empty, BoardMark.Empty,
            BoardMark.Empty, BoardMark.Empty, BoardMark.Empty,
            BoardMark.Empty, BoardMark.Empty, BoardMark.Empty
        };

        public static int[,] WinConditions { get; } =
        {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 6, 4, 2 }
        };
    }
}
