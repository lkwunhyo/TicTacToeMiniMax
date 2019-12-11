using System.Collections.Generic;
using System;

namespace QUT.CSharpTicTacToe
{
    public class Game : ITicTacToeGame<Player>
    {
        
        public string[,] Board { get; set; }
        public int Size { get; set; }
        public Player Turn { get; set; }

        public static T[,] GetNew2DArray<T>(int x, int y, T initialValue)
        {
            T[,] nums = new T[x, y];
            for (int i = 0; i < x * y; i++) nums[i % x, i / x] = initialValue;
            return nums;
        }

        public Game(Player first, int size)
        {
            this.Turn = first;
            this.Size = size;
            string[,] startBoard = GetNew2DArray<string>(size, size, "");
            this.Board = startBoard;
        }

        public string getPiece(int row, int col)
        {
            return this.Board[row, col];
        }
        

    }
}