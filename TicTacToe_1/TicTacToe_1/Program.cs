﻿// https://www.geeksforgeeks.org/finding-optimal-move-in-tic-tac-toe-using-minimax-algorithm-in-game-theory/

// C# program to find the 
// next optimal move for a player 
using System;
using System.Collections.Generic;

class GFG
{
    class Move
    {
        public int row, col, move;
    };

    // Original implementation had pre-assigned players.
    //static char player = 'x', opponent = 'o';

    // This function returns true if there are moves 
    // remaining on the board. It returns false if 
    // there are no moves left to play. 
    static Boolean isMovesLeft(char[,] board)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (board[i, j] == '_')
                    return true;
        return false;
    }

    // This is the evaluation function as discussed 
    // in the previous article ( http://goo.gl/sJgv68 ) 
    static int evaluate(char[,] b, char player, char opponent)
    {
        // Checking for Rows for X or O victory. 
        for (int row = 0; row < 3; row++)
        {
            if (b[row, 0] == b[row, 1] &&
                b[row, 1] == b[row, 2])
            {
                if (b[row, 0] == player)
                    return +10;
                else if (b[row, 0] == opponent)
                    return -10;
            }
        }

        // Checking for Columns for X or O victory. 
        for (int col = 0; col < 3; col++)
        {
            if (b[0, col] == b[1, col] &&
                b[1, col] == b[2, col])
            {
                if (b[0, col] == player)
                    return +10;

                else if (b[0, col] == opponent)
                    return -10;
            }
        }

        // Checking for Diagonals for X or O victory. 
        if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
        {
            if (b[0, 0] == player)
                return +10;
            else if (b[0, 0] == opponent)
                return -10;
        }

        if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
        {
            if (b[0, 2] == player)
                return +10;
            else if (b[0, 2] == opponent)
                return -10;
        }

        // Else if none of them have won then return 0 
        return 0;
    }

    // This is the minimax function. It considers all 
    // the possible ways the game can go and returns 
    // the value of the board 
    static int minimax(char[,] board,
                    int depth, Boolean isMax,
                    char player, char opponent)
    {
        int score = evaluate(board, player, opponent);

        // If Maximizer has won the game 
        // return his/her evaluated score 
        if (score == 10)
            return score;

        // If Minimizer has won the game 
        // return his/her evaluated score 
        if (score == -10)
            return score;

        // If there are no more moves and 
        // no winner then it is a tie 
        if (isMovesLeft(board) == false)
            return 0;

        // If this maximizer's move 
        if (isMax)
        {
            int best = -1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == '_')
                    {
                        // Make the move 
                        board[i, j] = player;

                        // Call minimax recursively and choose 
                        // the maximum value 
                        best = Math.Max(best, minimax(board,
                                        depth + 1, !isMax, player, opponent));

                        // Undo the move 
                        board[i, j] = '_';
                    }
                }
            }
            return best;
        }

        // If this minimizer's move 
        else
        {
            int best = 1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == '_')
                    {
                        // Make the move 
                        board[i, j] = opponent;

                        // Call minimax recursively and choose 
                        // the minimum value 
                        best = Math.Min(best, minimax(board,
                                        depth + 1, !isMax, player, opponent));

                        // Undo the move 
                        board[i, j] = '_';
                    }
                }
            }
            return best;
        }
    }

    // This will return the best possible 
    // move for the player 
    static Move findBestMove(char[,] board, char player, char opponent)
    {
        int bestVal = -1000;
        Move bestMove = new Move();
        bestMove.row = -1;
        bestMove.col = -1;
        bestMove.move = 0;

        // Traverse all cells, evaluate minimax function 
        // for all empty cells. And return the cell 
        // with optimal value. 
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Check if cell is empty 
                if (board[i, j] == '_')
                {
                    // Make the move 
                    board[i, j] = player;

                    // compute evaluation function for this 
                    // move. 
                    int moveVal = minimax(board, 0, false, player, opponent);

                    // Undo the move 
                    // Do we want to remove this or update it per-player?
                    board[i, j] = '_';

                    // If the value of the current move is 
                    // more than the best value, then update 
                    // best/ 
                    if (moveVal > bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
            }
        }

        Console.Write("The value of the best Move " +
                            "is : {0}\n\n", bestVal);
        bestMove.move = bestVal;

        return bestMove;
    }

    static void drawTheBoard(char[,] theBoard)
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                Console.Write("{0} ", theBoard[row, col]);
            }
            Console.Write("\n");
        }
        Console.Write("\n\n");
    }

    // Driver code 
    public static void Main(String[] args)
    {
//        char human = 'o';
//        char computer = 'x';

        // Original implementation is finding the optimal move for 'x'
        char[,] board = {{ '_', '_', '_' },
                         { '_', '_', '_' },
                         { '_', '_', '_' }};

        drawTheBoard(board);

        // If the computer is making the first move, then the position should be random.
        int optimalScore = 0;

        for (int moveNum = 0; optimalScore == 0; moveNum++)
        {
            Move bestMove = findBestMove(board, 'x', 'o');
            optimalScore = bestMove.move;
            Console.Write("The Optimal Move is :\n");
            Console.Write("ROW: {0} COL: {1} : {2}\n\n",
                    bestMove.row, bestMove.col, bestMove.move);

            if (optimalScore != 0)
            {
                break;
            }

            board[bestMove.row, bestMove.col] = 'x';
            drawTheBoard(board);

            bestMove = findBestMove(board, 'o', 'x');
            optimalScore = bestMove.move;
            Console.Write("The Optimal Move is :\n");
            Console.Write("ROW: {0} COL: {1} : {2}\n\n",
                    bestMove.row, bestMove.col, bestMove.move);

            if (optimalScore != 0)
            {
                break;
            }

            board[bestMove.row, bestMove.col] = 'o';
            drawTheBoard(board);
        }

    }
}

// This code is contributed by 29AjayKumar 

