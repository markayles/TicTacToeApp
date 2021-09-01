using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeLibrary.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public BoardMark Mark { get; set; }
        public bool IsAI { get; set; } = false;

        public PlayerModel(string name, BoardMark mark, bool isAI = false)
        {
            Name = name;
            Mark = mark;
            IsAI = isAI;
        }
    }
}
