namespace QUT.CSharpTicTacToe
{
    public class Move : ITicTacToeMove
    {
        
        public int Row { get; set; }
        public int Col { get; set; }

        public Move()
        {

        }

        public Move(int Row, int Col)
        {
            this.Row = Row;
            this.Col = Col;
        }

    }
}
