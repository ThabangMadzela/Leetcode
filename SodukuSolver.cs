using System;

class Solution
{
    private int[] rows;
    private int[] cols;
    private int[] subgrids;

    public void SolveSudoku(char[][] board)
    {
        rows = new int[9];
        cols = new int[9];
        subgrids = new int[9];

        // Pre-compute the availability of numbers in each row, column, and subgrid
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row][col] != '.')
                {
                    int num = board[row][col] - '0';
                    int mask = 1 << num;
                    rows[row] |= mask;
                    cols[col] |= mask;
                    subgrids[row / 3 * 3 + col / 3] |= mask;
                }
            }
        }

        Solve(board);
    }

    private bool Solve(char[][] board)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row][col] == '.')
                {
                    for (char num = '1'; num <= '9'; num++)
                    {
                        if (IsValid(board, row, col, num))
                        {
                            board[row][col] = num;

                            int mask = 1 << (num - '0');
                            rows[row] |= mask;
                            cols[col] |= mask;
                            subgrids[row / 3 * 3 + col / 3] |= mask;

                            if (Solve(board))
                                return true;

                            // Backtrack
                            board[row][col] = '.';
                            rows[row] &= ~mask;
                            cols[col] &= ~mask;
                            subgrids[row / 3 * 3 + col / 3] &= ~mask;
                        }
                    }

                    return false;
                }
            }
        }

        return true;
    }

    private bool IsValid(char[][] board, int row, int col, char num)
    {
        int mask = 1 << (num - '0');

        if ((rows[row] & mask) != 0)
            return false;

        if ((cols[col] & mask) != 0)
            return false;

        if ((subgrids[row / 3 * 3 + col / 3] & mask) != 0)
            return false;

        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Solution solution = new Solution();

        char[][] board = new char[][] {
            new char[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
            new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
            new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
            new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
            new char[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
            new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
            new char[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
            new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
            new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
        };

        solution.SolveSudoku(board);

        Console.WriteLine("Solved Sudoku:");
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                Console.Write(board[row][col] + " ");
            }
            Console.WriteLine();
        }
    }
}
