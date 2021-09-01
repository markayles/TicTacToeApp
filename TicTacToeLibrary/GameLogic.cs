using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeLibrary.Models;

namespace TicTacToeLibrary
{
    public static class GameLogic
    {
        public static List<PlayerModel> Players { get; set; } = new List<PlayerModel>();
        public static GameBoardModel Board { get; set; } = new GameBoardModel();
        public static GameState State { get; set; } = GameState.AwaitingUsers;
        public static int CurrentTurnPlayerIndex;
        private static Random random = new Random();

        public static void InitializeUser(string name, bool isAI = false)
        {
            BoardMark mark = BoardMark.X;
            if(Players.Count == 1)
            {
                mark = BoardMark.O;
                State = GameState.Turn;
            }

            PlayerModel player = new PlayerModel(name, mark, isAI);
            Players.Add(player);
        }

        // Can be rewritten to be smarter later on. Random number works for now. Theoretically, this could result in an infinite loop.
        public static void AIChoosePlacement()
        {
            int choice;
            bool validChoice = false;

            while(validChoice == false)
            {
                choice = random.Next(0, 9);
                validChoice = CheckIfPlacementValid(choice.ToString());
            }
        }

        public static void InitializeAIOpponent()
        {
            InitializeUser("Computer", true);
        }

        public static GameState GetGameState()
        {
            return State;
        }

        public static PlayerModel GetPlayerOfCurrentTurn()
        {
            return Players[CurrentTurnPlayerIndex];
        }

        public static bool CheckIfPlacementValid(string choice)
        {
            if(int.TryParse(choice, out int resultInt))
            {
                if(resultInt < 9 && resultInt > -1)
                {
                    if(Board.BoardSpots[resultInt] == BoardMark.Empty)
                    {
                        Board.BoardSpots[resultInt] = Players[CurrentTurnPlayerIndex].Mark;
                        if(CheckIfEndOfGame() == false)
                        {
                            CurrentTurnPlayerIndex = Math.Abs(CurrentTurnPlayerIndex - 1);
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        public static bool CheckIfEndOfGame()
        {
            if (CheckIfPlayerWon())
            {
                State = GameState.Win;
                return true;
            }
            else if(CheckIfEmptySpotsRemain() == false)
            {
                State = GameState.Tie;
                return true;
            }

            return false;
        }

        // Surely there is a better way to detect a win. For now, we will just brute force it since there are only 8 win conditions anyway.
        private static bool CheckIfPlayerWon()
        {
            for (int i = 0; i < 8; i++)
            {
                if (Board.BoardSpots[GameBoardModel.WinConditions[i,0]] != BoardMark.Empty 
                    && Board.BoardSpots[GameBoardModel.WinConditions[i, 0]] == Board.BoardSpots[GameBoardModel.WinConditions[i, 1]]
                    && Board.BoardSpots[GameBoardModel.WinConditions[i, 0]] == Board.BoardSpots[GameBoardModel.WinConditions[i, 2]])
                {
                    return true;
                }
            }

            return false;
        }

        // Again, probably a better way to check if all spots are filled like tracking as spots are placed. Since there are only 9, this is no issue for now.
        private static bool CheckIfEmptySpotsRemain()
        {
            bool emptySpotFound = false;

            for (int i = 0; i < Board.BoardSpots.Length; i++)
            {
                if(Board.BoardSpots[i] == BoardMark.Empty)
                {
                    emptySpotFound = true;
                }
            }

            return emptySpotFound;
        }
    }
}
