using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TicTacToeLibrary;
using TicTacToeLibrary.Models;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            GreetingMessage();

            InitializeUser();

            if (AskIfSecondUser())
            {
                InitializeUser();
            }
            else
            {
                GameLogic.InitializeAIOpponent();
            }

            PlayerModel playerOfcurrentTurn;

            do
            {
                Console.Clear();
                DrawBoard();

                playerOfcurrentTurn = GameLogic.GetPlayerOfCurrentTurn();

                if (playerOfcurrentTurn.IsAI)
                {
                    Console.Write($"{playerOfcurrentTurn.Name} is choosing");

                    for (int i = 0; i < 3; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(400);
                    }

                    Console.WriteLine();
                    GameLogic.AIChoosePlacement();
                }
                else
                {
                    AskForPlayersTurnPlacement(playerOfcurrentTurn);
                }

            } while (GameLogic.GetGameState() == GameState.Turn);

            Console.Clear();
            DrawBoard();

            if (GameLogic.GetGameState() == GameState.Win)
            {
                Console.WriteLine($"{playerOfcurrentTurn.Name} won!");
            }
            else if(GameLogic.GetGameState() == GameState.Tie)
            {
                Console.WriteLine("The game was a tie.");
            }
            else
            {
                Console.WriteLine("Unexpected game state.");
            }

            Console.ReadLine();
        }

        private static void DrawBoard()
        {
            Console.WriteLine("    |    |    ");
            Console.WriteLine("    |    |    ");
            Console.WriteLine("   0|   1|   2");
            Console.WriteLine("--------------");
            Console.WriteLine("    |    |    ");
            Console.WriteLine("    |    |    ");
            Console.WriteLine("   3|   4|   5");
            Console.WriteLine("--------------");
            Console.WriteLine("    |    |    ");
            Console.WriteLine("    |    |    ");
            Console.WriteLine("   6|   7|   8");
            Console.WriteLine();

            int cellWidth = 4; // not including the border
            int cellHeight = 3; // not including the border

            // TODO: Eventually procedurally generate the game board based on sizes from variables

            GameBoardModel board = GameLogic.Board;

            int curWidth = 1;
            int curHeight = 1;
            for (int i = 0; i < board.BoardSpots.Length; i++)
            {
                Console.SetCursorPosition(curWidth, curHeight);

                if (board.BoardSpots[i] == BoardMark.Empty)
                {
                    //do nothing, leave it blank
                }
                else if (board.BoardSpots[i] == BoardMark.X)
                {
                    Console.Write($"X");
                }
                else if (board.BoardSpots[i] == BoardMark.O)
                {
                    Console.Write($"O");
                }
                else
                {
                    Console.Write($"?");
                }

                curWidth += cellWidth+1; // +1 to account for borders

                if ((i + 1) % 3 == 0)
                {
                    curWidth -= (cellWidth+1) * 3;
                    curHeight += cellHeight+1;
                }
            }

            Console.SetCursorPosition(0, 12);
        }

        private static void AskForPlayersTurnPlacement(PlayerModel currentPlayer)
        {
            bool validPlacement = false;

            do
            {
                Console.Write($"{currentPlayer.Name} ({currentPlayer.Mark}), Enter your choice: ");
                string result = Console.ReadLine();

                if (GameLogic.CheckIfPlacementValid(result))
                {
                    validPlacement = true;
                }
                else
                {
                    Console.WriteLine("You entered an invalid choice.");
                }

            } while (validPlacement == false);
        }

        private static bool AskIfSecondUser()
        {
            Console.Write("Is there another human player joining (y/n): ");
            string result = Console.ReadLine();

            if(result.ToLower() == "y")
            {
                return true;
            }

            return false;
        }

        private static void InitializeUser()
        {
            Console.Write("Enter your name: ");
            string result = Console.ReadLine();

            GameLogic.InitializeUser(result);
        }

        private static void GreetingMessage()
        {
            Console.WriteLine("Welcome to TicTacToe.");
            Console.WriteLine("Created by Mark Ayles");
            Console.WriteLine();
        }
    }
}
