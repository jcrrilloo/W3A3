using System;
class Program
{
    static int BoardSize = 12;
    static int attemptedMoves = 0;

    static int[] xMove = { 2, 1, -1, -2, -2, -1, 1, 2 };
    static int[] yMove = { 1, 2, 2, 1, -1, -2, -2, -1 };

    static int[,] boardGrid = new int[BoardSize, BoardSize];

    // driver code
    public static void Main()
    {
        solveKT();
        Console.ReadLine();
    }

    static void solveKT()
    {
        for (int x = 0; x < BoardSize; x++)
            for (int y = 0; y < BoardSize; y++)
                boardGrid[x, y] = -1;
        int startX = 0;
        int startY = 0;

        // check if the starting position is  valid
        if(startX < 0 || startX >= BoardSize || startY < 0 || startY >= BoardSize)
        {
            Console.WriteLine("Invalid starting positiono. ");
            return;
        }

        // set starting position for the knight
        boardGrid[startX, startY] = 0;
        // count the total number of guesses
        attemptedMoves = 0;

        if (!solveKTUtil(startX, startY, 1))
        {
            Console.WriteLine("Solution odoes not exist for {0}, {1} ", startX, startY);
        }
        else
        {
            printSolution(boardGrid);
            Console.Out.WriteLine("Total attempted moves {0}", attemptedMoves);
        }
    }

    static bool solveKTUtil(int x, int y, int moveCount)
    {
        attemptedMoves++;
        if (attemptedMoves % 1000000 == 0) Console.Out.WriteLine("Attempts: {0}", attemptedMoves);
        if (moveCount == BoardSize * BoardSize)
            return true;

        // calculate accessibility for each   potential move
        List<(int, int, int)> moves = new List<(int, int, int)>();
        for(int k = 0; k < 8; k++)
        {
            int next_x = x + xMove[k];
            int next_y = y + yMove[k];
            if(isSquareSafe(next_x, next_y))
            {
                int accessibility = countAccessibleSquares(next_x, next_y);
                moves.Add((next_x, next_y, accessibility));
            }
        }
        // sort moves based on accessibility
        moves.Sort((a, b) => a.Item3.CompareTo(b.Item3));

        foreach(var move in moves)
        {
            int next_x = move.Item1;
            int next_y = move.Item2;
            boardGrid[next_x, next_y] = moveCount;

            if (solveKTUtil(next_x, next_y, moveCount + 1))
                return true;
            else
                boardGrid[next_x, next_y] = -1; 
        }
        return false;
    }
    static int countAccessibleSquares(int x, int y)
    {
        int count = 0;
        for(int i = 0; i < 8; i++)
        {
            int next_x = x + xMove[i];
            int next_y = y + yMove[i];

            if(isSquareSafe(next_x, next_y))
            {
                count++;
            }
        }
        return count; 
    }
    static bool isSquareSafe(int x, int y)
    {
        return (x >= 0 && x < BoardSize &&
            y >= 0 && y < BoardSize &&
            boardGrid[x, y] == 1);
    }

    static void printSolution(int[,] solution)
    {
        for (int x = 0; x < BoardSize; x++)
        {
            for (int y = 0; y < BoardSize; y++)
                Console.Write(solution[x, y] + " ");

            Console.WriteLine();
        }
    }
}