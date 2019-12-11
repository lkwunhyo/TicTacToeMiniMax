namespace QUT

// interface to represent the current state of the game
type ITicTacToeGame<'Player> = 
    abstract member Size: int with get     // a normal Tic Tac Toe game is 3 x 3, but any Size can be specified
    abstract member Turn: 'Player with get // the state of the game also includes which player's turn it is next
    abstract member getPiece: int * int -> string // the current state of the board represented by a function that maps squares with (row,column) coordinates to the piece on that square ("X" for Cross, "O" for Nought and "" for empty)

// interface to represent a Tic Tac Toe move (specified using row,column coordinates)
type ITicTacToeMove =
    abstract member Row: int with get
    abstract member Col: int with get

// returns Win if the winner has completed an entire line (returned as a sequence of (row,col) coordinates), 
// returns Draw if all lines contain at least one Cross and one Nought
// otherwise it returns Undecided
type TicTacToeOutcome<'Player> = Draw | Undecided | Win of winner:'Player * line:seq<int*int>

// interface to allow the view model to make use of different implementations of the model
type ITicTacToeModel<'Game, 'Move, 'Player when 'Game :> ITicTacToeGame<'Player> and 'Move :> ITicTacToeMove> =
    abstract member Cross : 'Player      // returns the player value for "X"
    abstract member Nought: 'Player      // returns the player value for "O"
    abstract member GameStart: 'Player * int -> 'Game // creates a new game with the given player going first on a board of size n x n
    abstract member CreateMove: int * int -> 'Move // creates a new move corresponding the square with the given row, column coordinates
    abstract member FindBestMove: 'Game -> 'Move // use minimax algorithm to determine the best move given the current game situation
    abstract member GameOutcome: 'Game -> TicTacToeOutcome<'Player> // determine if the given game has been won, lost, drawed or undecided
    abstract member ApplyMove: 'Game * 'Move -> 'Game // create a new game by applying the given move to the given game state