using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace tic_tac_toe_with_human_players
{
    public enum Square
    {

        EMPTY,
        NOUGHT,
        CROSS
    }
    public enum Row
    {
        _1,
        _2,
        _3
    }

    public enum Column
    {
        _1,
        _2,
        _3,
    }
    public enum Status
    {
        USED,
        INVALID,
        WRITTEN
    }
    class Program
    {
        static void Main(string[] args)
        {
            Square[,] game_board = new Square[3, 3];
            Square player1 = Square.EMPTY;
            Square player2 = Square.EMPTY;

            Console.WriteLine("Player 1, noughts or crosses?(n/x)");
            player1 = SetPlayer(Console.ReadLine().ToCharArray().First());
            if(player1==Square.CROSS)
            {
                player2 = Square.NOUGHT;
            }
            if (player1==Square.NOUGHT)
            {
                player2 = Square.CROSS;
            }
            
            while (true)
            {
                ShowGame(ref game_board);
                Console.WriteLine("Player 1 go");
                int position = Convert.ToInt32(Console.ReadLine());

                switch (WritePosition(ref game_board, position, player1))
                {
                    case Status.INVALID:
                        Console.WriteLine("Position must be from 1-9");
                        break;
                    case Status.USED:
                        Console.WriteLine("That position has already been used");
                        break;
                    default:
                        break;
                }
                
                Console.Clear();
                ShowGame(ref game_board);
                if (Win(ref game_board, player1))
                {
                    Console.WriteLine("Player 1 wins!!");
                    break;
                }
                if(!Win(ref game_board, player1) && !Win(ref game_board, player1)
                    && NotContain(ref game_board))
                {
                    Console.WriteLine("It's a tie!");
                    break;
                }
                Console.WriteLine("Player 2 go");
                position = Convert.ToInt32(Console.ReadLine());

                switch (WritePosition(ref game_board, position, player2))
                {
                    case Status.INVALID:
                        Console.WriteLine("Position must be from 1-9");
                        break;
                    case Status.USED:
                        Console.WriteLine("That position has already been used");
                        break;
                    default:
                        break;
                }
                if (Win(ref game_board, player2))
                {
                    Console.WriteLine("Player 2 wins!!");
                    break;
                }
                if (!Win(ref game_board, player1) && !Win(ref game_board, player1)
                    && NotContain(ref game_board))
                {
                    Console.WriteLine("It's a tie!");
                    break;
                }
                Console.Clear();
            }
            


        }

        private static Square SetPlayer(char input)
        {
            Square player1 = Square.EMPTY;

            switch (input)
            {
                case 'n':
                    player1 = Square.NOUGHT;
                    break;
                case 'x':
                    player1 = Square.CROSS;
                    break;
                default:
                    break;
            }

            return player1;
        }

        static void ShowGame(ref Square[,] game_board)
        {
            int counter = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (game_board[i, j] == Square.NOUGHT)
                    {
                        Console.Write("[O]");
                    }

                    if (game_board[i, j] == Square.CROSS)
                    {
                        Console.Write("[X]");
                    }

                    if (game_board[i, j] == Square.EMPTY)
                    {
                        Console.Write("[" + Convert.ToString(counter) + "]");
                    }
                    counter++;
                }
                Console.WriteLine();
            }
        }

        static Status WritePosition(ref Square[,] game_board, int position, Square player)
        {
            if (position < 1 || 9 < position)
            {
                return Status.INVALID;
            }
            int counter = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    if (position == counter)
                    {
                        if (game_board[i, j] != Square.EMPTY)
                        {

                            return Status.USED;
                        }

                        game_board[i, j] = player;
                        return Status.WRITTEN;
                    }
                    counter++;
                }

            }
            return Status.INVALID;
        }
        static bool Win(ref Square[,] game_board, Square player)
        {
            return CountSquaresInColumn(player, Column._1, ref game_board) == 3
                || CountSquaresInColumn(player, Column._2, ref game_board) == 3
                || CountSquaresInColumn(player, Column._3, ref game_board) == 3
                || CountSquaresInRow(player, Row._1, ref game_board) == 3
                || CountSquaresInRow(player, Row._2, ref game_board) == 3
                || CountSquaresInRow(player, Row._3, ref game_board) == 3
                || CountSquaresInMainDiagonal(player, ref game_board) == 3
                || CountSquaresInSecondaryDiagonal(player, ref game_board) == 3;

        }

        

        static int CountSquaresInMainDiagonal(Square square, ref Square[,] game_board)
        {
            int SquareCount = 0;
            for (int i = 0; i < game_board.GetLength(0); i++)
            {
                if (game_board[i, i] == square)
                {
                    SquareCount++;
                }
            }
            return SquareCount;

        }

        static int CountSquaresInSecondaryDiagonal(Square square, ref Square[,] game_board)
        {
            int SquareCount = 0;
            for (int i = 0; i < game_board.GetLength(0); i++)
            {
                if (game_board[i, 2 - i] == square)
                {
                    SquareCount++;
                }
            }
            return SquareCount;

        }
        static int CountSquaresInRow(Square squares, Row row, ref Square[,] game_board)
        {
            int SquareCount = 0;
            for (int i = 0; i < game_board.GetLength(1); i++)
            {
                if (game_board[i, Convert.ToInt32(row)] == squares)
                {
                    SquareCount++;
                }
            }
            return SquareCount;
        }
        static int CountSquaresInColumn(Square squares, Column column, ref Square[,] game_board)
        {
            int SquareCount = 0;
            for (int i = 0; i < game_board.GetLength(0); i++)
            {
                if (game_board[Convert.ToInt32(column), i] == squares)
                {
                    SquareCount++;
                }
            }
            return SquareCount;
        }
        static bool NotContain(ref Square[,] game_board)
        {
            for (int i = 0; i < game_board.GetLength(0); i++)
            {
                for (int j = 0; j < game_board.GetLength(1); j++)
                {
                    if(game_board[i,j]==Square.EMPTY)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
