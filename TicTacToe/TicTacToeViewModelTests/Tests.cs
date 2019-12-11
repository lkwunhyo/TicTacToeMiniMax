using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System;

namespace QUT
{
    [TestClass]
    public class TicTacToeViewModelTests
    {
        private TicTacToeViewModel viewModel;

        private TicTacToeSquareModel square1, square2, square3, square4, square5, square6, square7, square8, square9;

        private void PlayGame(bool humanFirst, int size, params ValueTuple<int, int>[] moves)
        {
            viewModel = new TicTacToeViewModel();

            viewModel.AutoStartComputerMove = false;
            // Computer doesn't automatically start thinking about it's move when it's the computer's turn
            // so that we can examine the state of the system before the computer move starts

            viewModel.StartNewGame(size, humanFirst);

            square1 = viewModel.FindSquare(0, 0);
            square2 = viewModel.FindSquare(0, 1);
            square3 = viewModel.FindSquare(0, 2);
            square4 = viewModel.FindSquare(1, 0);
            square5 = viewModel.FindSquare(1, 1);
            square6 = viewModel.FindSquare(1, 2);
            square7 = viewModel.FindSquare(2, 0);
            square8 = viewModel.FindSquare(2, 1);
            square9 = viewModel.FindSquare(2, 2);

            foreach (var move in moves)
                viewModel.HumanMove(viewModel.FindSquare(move.Item1, move.Item2));
        }


        [TestMethod]
        public void Test()
        {
            PlayGame(true, 3);

            // test that the outcome is as expected ...
            Assert.AreEqual("Your turn ...", viewModel.Message);
            Assert.IsFalse(viewModel.IsGameOver);
            Assert.IsTrue(viewModel.IsHumanTurn);

            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                {
                    var square = viewModel.FindSquare(row, col);
                    Assert.IsTrue(square.IsHumanTurn);
                    Assert.IsTrue(square.IsEnabled);
                    Assert.IsFalse(square.HighLight);
                    Assert.AreEqual("", square.Piece);
                    Assert.AreEqual(row, square.row);
                    Assert.AreEqual(col, square.col);
                }
        }

        [TestMethod]
        public void Test1()
        {
            PlayGame(true, 3);

            // Human selects top left square ...
            viewModel.HumanMove(square1);

            Assert.AreEqual("Please wait, the computer is thinking ...", viewModel.Message);
            Assert.IsFalse(viewModel.IsGameOver);
            Assert.IsFalse(viewModel.IsHumanTurn);

            Assert.IsFalse(square1.IsHumanTurn);
            Assert.IsFalse(square1.IsEnabled);
            Assert.IsFalse(square1.HighLight);
            Assert.AreEqual("X", square1.Piece);
        }

        [TestMethod]
        public async Task Test15()
        {
            PlayGame(true, 3, (0, 0));

            // Wait for computer to select a square
            var square = await viewModel.ComputerMove();

            // Computer should choose middle square
            Assert.AreEqual(1, square.row);
            Assert.AreEqual(1, square.col);

            Assert.IsFalse(viewModel.IsGameOver);
            Assert.AreEqual("Your turn ...", viewModel.Message);
            Assert.IsTrue(viewModel.IsHumanTurn);

            Assert.IsTrue(square1.IsHumanTurn);
            Assert.IsFalse(square1.IsEnabled);
            Assert.IsFalse(square1.HighLight);
            Assert.AreEqual("X", square1.Piece);

            Assert.IsTrue(square5.IsHumanTurn);
            Assert.IsFalse(square5.IsEnabled);
            Assert.IsFalse(square5.HighLight);
            Assert.AreEqual("O", square5.Piece);
        }

        [TestMethod]
        public void Test152()
        {
            PlayGame(true, 3, (0, 0), (1, 1));

            // Human chooses top middle square
            viewModel.HumanMove(square2);

            Assert.IsFalse(viewModel.IsGameOver);
            Assert.AreEqual("Please wait, the computer is thinking ...", viewModel.Message);
            Assert.IsFalse(viewModel.IsHumanTurn);

            Assert.IsFalse(square1.IsHumanTurn);
            Assert.IsFalse(square1.IsEnabled);
            Assert.IsFalse(square1.HighLight);
            Assert.AreEqual("X", square1.Piece);

            Assert.IsFalse(square5.IsHumanTurn);
            Assert.IsFalse(square5.IsEnabled);
            Assert.IsFalse(square5.HighLight);
            Assert.AreEqual("O", square5.Piece);

            Assert.IsFalse(square2.IsHumanTurn);
            Assert.IsFalse(square2.IsEnabled);
            Assert.IsFalse(square2.HighLight);
            Assert.AreEqual("X", square2.Piece);
        }

        [TestMethod]
        public async Task Test1523()
        {
            PlayGame(true, 3, (0, 0), (1, 1), (0, 1));

            // Wait for computer to select a 2nd square
            var square = await viewModel.ComputerMove();

            // Computer should choose top right
            Assert.AreEqual(0, square.row);
            Assert.AreEqual(2, square.col);

            Assert.IsFalse(viewModel.IsGameOver);
            Assert.AreEqual("Your turn ...", viewModel.Message);
            Assert.IsTrue(viewModel.IsHumanTurn);

            Assert.IsTrue(square1.IsHumanTurn);
            Assert.IsFalse(square1.IsEnabled);
            Assert.IsFalse(square1.HighLight);
            Assert.AreEqual("X", square1.Piece);

            Assert.IsTrue(square5.IsHumanTurn);
            Assert.IsFalse(square5.IsEnabled);
            Assert.IsFalse(square5.HighLight);
            Assert.AreEqual("O", square5.Piece);

            Assert.IsTrue(square2.IsHumanTurn);
            Assert.IsFalse(square2.IsEnabled);
            Assert.IsFalse(square2.HighLight);
            Assert.AreEqual("X", square2.Piece);

            Assert.IsTrue(square3.IsHumanTurn);
            Assert.IsFalse(square3.IsEnabled);
            Assert.IsFalse(square3.HighLight);
            Assert.AreEqual("O", square3.Piece);
        }

        [TestMethod]
        public void Test15234()
        {
            PlayGame(true, 3, (0, 0), (1, 1), (0, 1), (0, 2));

            // Human "poorly" chooses left middle
            viewModel.HumanMove(square4);

            Assert.IsFalse(viewModel.IsGameOver);
            Assert.AreEqual("Please wait, the computer is thinking ...", viewModel.Message);
            Assert.IsFalse(viewModel.IsHumanTurn);

            Assert.IsFalse(square1.IsHumanTurn);
            Assert.IsFalse(square1.IsEnabled);
            Assert.IsFalse(square1.HighLight);
            Assert.AreEqual("X", square1.Piece);

            Assert.IsFalse(square5.IsHumanTurn);
            Assert.IsFalse(square5.IsEnabled);
            Assert.IsFalse(square5.HighLight);
            Assert.AreEqual("O", square5.Piece);

            Assert.IsFalse(square2.IsHumanTurn);
            Assert.IsFalse(square2.IsEnabled);
            Assert.IsFalse(square2.HighLight);
            Assert.AreEqual("X", square2.Piece);

            Assert.IsFalse(square3.IsHumanTurn);
            Assert.IsFalse(square3.IsEnabled);
            Assert.IsFalse(square3.HighLight);
            Assert.AreEqual("O", square3.Piece);

            Assert.IsFalse(square4.IsHumanTurn);
            Assert.IsFalse(square4.IsEnabled);
            Assert.IsFalse(square4.HighLight);
            Assert.AreEqual("X", square4.Piece);
        }

        [TestMethod]
        public async Task Test152347()
        {
            PlayGame(true, 3, (0, 0), (1, 1), (0, 1), (0, 2), (1, 0));

            // Wait for computer to select a 3nd square
            var square = await viewModel.ComputerMove();

            // Computer should choose bottom left
            Assert.AreEqual(2, square.row);
            Assert.AreEqual(0, square.col);

            Assert.IsTrue(viewModel.IsGameOver);
            Assert.AreEqual("Bad luck, the computer beat you!", viewModel.Message);
            Assert.IsFalse(viewModel.IsHumanTurn);

            Assert.IsFalse(square1.IsHumanTurn);
            Assert.IsFalse(square1.IsEnabled);
            Assert.IsFalse(square1.HighLight);
            Assert.AreEqual("X", square1.Piece);

            Assert.IsFalse(square5.IsHumanTurn);
            Assert.IsFalse(square5.IsEnabled);
            Assert.IsTrue(square5.HighLight);
            Assert.AreEqual("O", square5.Piece);

            Assert.IsFalse(square2.IsHumanTurn);
            Assert.IsFalse(square2.IsEnabled);
            Assert.IsFalse(square2.HighLight);
            Assert.AreEqual("X", square2.Piece);

            Assert.IsFalse(square3.IsHumanTurn);
            Assert.IsFalse(square3.IsEnabled);
            Assert.IsTrue(square3.HighLight);
            Assert.AreEqual("O", square3.Piece);

            Assert.IsFalse(square4.IsHumanTurn);
            Assert.IsFalse(square4.IsEnabled);
            Assert.IsFalse(square4.HighLight);
            Assert.AreEqual("X", square4.Piece);

            Assert.IsFalse(square7.IsHumanTurn);
            Assert.IsFalse(square7.IsEnabled);
            Assert.IsTrue(square7.HighLight);
            Assert.AreEqual("O", square7.Piece);

            // Game Over !!!
        }
    }
}
