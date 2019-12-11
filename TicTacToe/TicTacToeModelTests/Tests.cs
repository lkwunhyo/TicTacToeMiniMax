using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace QUT
{
    [TestClass]
    public abstract class TicTacToeModelTests<Game, Move, Player> where Game: ITicTacToeGame<Player> where Move: ITicTacToeMove
    {
        protected ITicTacToeModel<Game, Move, Player> modelUnderTest;
        protected abstract ITicTacToeModel<Game, Move, Player> ModelUnderTest();

        protected Player Cross => modelUnderTest.Cross;
        protected Player Nought => modelUnderTest.Nought;

        protected Game GenerateGame(Player first, int size, params ValueTuple<int,int>[] moves)
        {
            var game = modelUnderTest.GameStart(first, size);

            foreach (var move in moves)
                game = modelUnderTest.ApplyMove(game, modelUnderTest.CreateMove(move.Item1, move.Item2));

            return game;
        }
		
		[TestInitialize]
		public void Initialize()
		{
			modelUnderTest = ModelUnderTest();
		}

        [TestMethod]
        public void TestGameStat()
        {
            var game = GenerateGame(Cross, 3);
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome()
        {
            var game = GenerateGame(Cross, 3);
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest()
        {
            var game = GenerateGame(Cross, 3);
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 1), (0, 2), (1, 0), (1, 1), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat1()
        {
            var game = GenerateGame(Cross, 3, (0, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome1()
        {
            var game = GenerateGame(Cross, 3, (0, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest1()
        {
            var game = GenerateGame(Cross, 3, (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat12()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome12()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest12()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 0), (1, 1), (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat123()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome123()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest123()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat124()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome124()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest124()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (1, 1), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat1245()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome1245()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest1245()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat12457()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0), (1, 1), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome12457()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0), (1, 1), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsWin);
            var win = outcome as TicTacToeOutcome<Player>.Win;
            Assert.AreEqual(Cross, win.winner);
            var expectedLine = new List<ValueTuple<int, int>>() { (0, 0), (1, 0), (2, 0) };
            var actualCount = 0;
            foreach (var square in win.line)
            {
                actualCount++;
                Assert.IsTrue(expectedLine.Contains((square.Item1, square.Item2)));
            }
            Assert.AreEqual(expectedLine.Count, actualCount);
        }

        [TestMethod]
        public void TestGameStat125()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome125()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest125()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (1, 0), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat1253()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome1253()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest1253()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 0), (1, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat12539()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1), (0, 2), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome12539()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1), (0, 2), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsWin);
            var win = outcome as TicTacToeOutcome<Player>.Win;
            Assert.AreEqual(Cross, win.winner);
            var expectedLine = new List<ValueTuple<int, int>>() { (0, 0), (1, 1), (2, 2) };
            var actualCount = 0;
            foreach (var square in win.line)
            {
                actualCount++;
                Assert.IsTrue(expectedLine.Contains((square.Item1, square.Item2)));
            }
            Assert.AreEqual(expectedLine.Count, actualCount);
        }

        [TestMethod]
        public void TestGameStat126()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome126()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest126()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat127()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome127()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest127()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (1, 0), (1, 1), (1, 2), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat128()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome128()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest128()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat129()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome129()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest129()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat13()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome13()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest13()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 0), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat132()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome132()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest132()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 2), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat134()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome134()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest134()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (1, 1), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat135()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome135()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest135()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat136()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome136()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest136()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 0), (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat137()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome137()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest137()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (1, 0), (1, 1), (1, 2), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat138()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome138()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest138()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat139()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome139()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest139()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (1, 0), (1, 1), (1, 2), (2, 0), (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat14()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome14()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest14()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (0, 2), (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat142()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome142()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest142()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (1, 1), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat1423()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome1423()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest1423()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat1425()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome1425()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest1425()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat14236()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome14236()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest14236()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat142369()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("O", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome142369()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest142369()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1), (2, 0), (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat1423695()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("O", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome1423695()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest1423695()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat14236958()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("O", game.getPiece(2, 1));
            Assert.AreEqual("O", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome14236958()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest14236958()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat142369587()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1), (2, 1), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("O", game.getPiece(2, 1));
            Assert.AreEqual("O", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome142369587()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1), (2, 1), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsDraw);
        }

        [TestMethod]
        public void TestGameStat14253()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (1, 1), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome14253()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (1, 1), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsWin);
            var win = outcome as TicTacToeOutcome<Player>.Win;
            Assert.AreEqual(Cross, win.winner);
            var expectedLine = new List<ValueTuple<int, int>>() { (0, 0), (0, 1), (0, 2) };
            var actualCount = 0;
            foreach (var square in win.line)
            {
                actualCount++;
                Assert.IsTrue(expectedLine.Contains((square.Item1, square.Item2)));
            }
            Assert.AreEqual(expectedLine.Count, actualCount);
        }

        [TestMethod]
        public void TestGameStat15()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome15()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest15()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (0, 2), (1, 0), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat152()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome152()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest152()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat1523()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome1523()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest1523()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat15238()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome15238()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest15238()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 0), (1, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat152387()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2), (2, 1), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("O", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome152387()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2), (2, 1), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsWin);
            var win = outcome as TicTacToeOutcome<Player>.Win;
            Assert.AreEqual(Nought, win.winner);
            var expectedLine = new List<ValueTuple<int, int>>() { (0, 2), (1, 1), (2, 0) };
            var actualCount = 0;
            foreach (var square in win.line)
            {
                actualCount++;
                Assert.IsTrue(expectedLine.Contains((square.Item1, square.Item2)));
            }
            Assert.AreEqual(expectedLine.Count, actualCount);
        }

        [TestMethod]
        public void TestGameStat153()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome153()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest153()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat156()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome156()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest156()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (0, 2), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat159()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome159()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest159()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (1, 0), (1, 2), (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat16()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("O", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome16()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest16()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (1, 1), (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat162()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("O", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome162()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest162()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat163()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("O", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome163()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest163()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (1, 0), (1, 1), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat164()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("O", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome164()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest164()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat165()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("O", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome165()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest165()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (0, 2), (1, 0), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat167()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("O", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome167()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest167()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (0, 2), (1, 0), (1, 1), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat168()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("O", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome168()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest168()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat169()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("O", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome169()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest169()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat19()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("O", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome19()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest19()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat192()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("O", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome192()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest192()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat193()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("O", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome193()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest193()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (1, 0), (1, 1), (1, 2), (2, 0), (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat195()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("O", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome195()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest195()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat196()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("O", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome196()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest196()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 0), (1, 1), (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat2()
        {
            var game = GenerateGame(Cross, 3, (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome2()
        {
            var game = GenerateGame(Cross, 3, (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest2()
        {
            var game = GenerateGame(Cross, 3, (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (1, 1), (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat21()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome21()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest21()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 0), (1, 1), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat213()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome213()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest213()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 0), (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat214()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome214()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest214()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1), (1, 2), (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat2145()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome2145()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest2145()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat21458()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0), (1, 1), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome21458()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0), (1, 1), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest21458()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0), (1, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat215()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome215()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest215()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat2154()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome2154()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest2154()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 0), (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat21548()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1), (1, 0), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome21548()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1), (1, 0), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsWin);
            var win = outcome as TicTacToeOutcome<Player>.Win;
            Assert.AreEqual(Cross, win.winner);
            var expectedLine = new List<ValueTuple<int, int>>() { (0, 1), (1, 1), (2, 1) };
            var actualCount = 0;
            foreach (var square in win.line)
            {
                actualCount++;
                Assert.IsTrue(expectedLine.Contains((square.Item1, square.Item2)));
            }
            Assert.AreEqual(expectedLine.Count, actualCount);
        }

        [TestMethod]
        public void TestGameStat216()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome216()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest216()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat217()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome217()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest217()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1), (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat218()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome218()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest218()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat219()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome219()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest219()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1), (2, 0), (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat24()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome24()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest24()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat241()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (0, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome241()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (0, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest241()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (1, 1), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat243()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome243()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest243()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat245()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome245()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest245()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat246()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome246()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest246()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat247()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome247()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest247()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat248()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome248()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest248()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat249()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome249()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest249()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat25()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome25()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest25()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (1, 0), (1, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat251()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (0, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome251()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (0, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest251()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat254()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome254()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest254()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat257()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome257()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest257()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (1, 0), (1, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat258()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome258()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest258()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (1, 0), (1, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat27()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("O", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome27()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest27()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat271()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (0, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("O", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome271()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (0, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest271()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (1, 0), (1, 1), (1, 2), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat273()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("O", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome273()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest273()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat274()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("O", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome274()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest274()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat275()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("O", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome275()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest275()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat276()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("O", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome276()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest276()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat278()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("O", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome278()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest278()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat279()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("O", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome279()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest279()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat28()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("O", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome28()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest28()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (1, 0), (1, 1), (1, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat281()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (0, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("O", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome281()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (0, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest281()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat284()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("O", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome284()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest284()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat285()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("O", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome285()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest285()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat287()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("O", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome287()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest287()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat3()
        {
            var game = GenerateGame(Cross, 3, (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome3()
        {
            var game = GenerateGame(Cross, 3, (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest3()
        {
            var game = GenerateGame(Cross, 3, (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat32()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome32()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest32()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1), (1, 2), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat325()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome325()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest325()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (1, 0), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat3251()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1), (0, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome3251()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1), (0, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest3251()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 0), (1, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat32517()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1), (0, 0), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome32517()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1), (0, 0), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsWin);
            var win = outcome as TicTacToeOutcome<Player>.Win;
            Assert.AreEqual(Cross, win.winner);
            var expectedLine = new List<ValueTuple<int, int>>() { (0, 2), (1, 1), (2, 0) };
            var actualCount = 0;
            foreach (var square in win.line)
            {
                actualCount++;
                Assert.IsTrue(expectedLine.Contains((square.Item1, square.Item2)));
            }
            Assert.AreEqual(expectedLine.Count, actualCount);
        }

        [TestMethod]
        public void TestGameStat326()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome326()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest326()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (1, 0), (1, 1), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat3265()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome3265()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest3265()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat32659()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2), (1, 1), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome32659()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2), (1, 1), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsWin);
            var win = outcome as TicTacToeOutcome<Player>.Win;
            Assert.AreEqual(Cross, win.winner);
            var expectedLine = new List<ValueTuple<int, int>>() { (0, 2), (1, 2), (2, 2) };
            var actualCount = 0;
            foreach (var square in win.line)
            {
                actualCount++;
                Assert.IsTrue(expectedLine.Contains((square.Item1, square.Item2)));
            }
            Assert.AreEqual(expectedLine.Count, actualCount);
        }

        [TestMethod]
        public void TestGameStat4()
        {
            var game = GenerateGame(Cross, 3, (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome4()
        {
            var game = GenerateGame(Cross, 3, (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest4()
        {
            var game = GenerateGame(Cross, 3, (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (1, 1), (1, 2), (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat41()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome41()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest41()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (0, 2), (1, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat415()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome415()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest415()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat4152()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1), (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome4152()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1), (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest4152()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (1, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat41526()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1), (0, 1), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome41526()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1), (0, 1), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsWin);
            var win = outcome as TicTacToeOutcome<Player>.Win;
            Assert.AreEqual(Cross, win.winner);
            var expectedLine = new List<ValueTuple<int, int>>() { (1, 0), (1, 1), (1, 2) };
            var actualCount = 0;
            foreach (var square in win.line)
            {
                actualCount++;
                Assert.IsTrue(expectedLine.Contains((square.Item1, square.Item2)));
            }
            Assert.AreEqual(expectedLine.Count, actualCount);
        }

        [TestMethod]
        public void TestGameStat5()
        {
            var game = GenerateGame(Cross, 3, (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome5()
        {
            var game = GenerateGame(Cross, 3, (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest5()
        {
            var game = GenerateGame(Cross, 3, (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat51()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome51()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest51()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 1), (0, 2), (1, 0), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat512()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("X", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome512()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest512()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat513()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (0, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("X", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome513()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (0, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest513()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat516()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (1, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome516()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (1, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest516()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat519()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome519()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest519()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (2, 0) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat52()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome52()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest52()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (1, 0), (1, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat521()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (0, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("X", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome521()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (0, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest521()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 2), (1, 0), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat524()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome524()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest524()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (1, 2), (2, 0), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat527()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome527()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest527()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (1, 0), (1, 2), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat528()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome528()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest528()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 2), (2, 0), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat7()
        {
            var game = GenerateGame(Cross, 3, (2, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome7()
        {
            var game = GenerateGame(Cross, 3, (2, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest7()
        {
            var game = GenerateGame(Cross, 3, (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat74()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome74()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest74()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (1, 1), (2, 1), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat748()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome748()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest748()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (0, 0), (0, 1), (0, 2), (1, 1), (1, 2), (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat7485()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1), (1, 1));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome7485()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1), (1, 1));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsUndecided);
        }

        [TestMethod]
        public void TestFindBest7485()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            var bestMoves = new List<ValueTuple<int, int>>() { (2, 2) };
            Assert.IsTrue(bestMoves.Contains((move.Row, move.Col)));
        }

        [TestMethod]
        public void TestGameStat74859()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1), (1, 1), (2, 2));
            Assert.AreEqual(3, game.Size);
            Assert.AreEqual(Nought, game.Turn);
            Assert.AreEqual("", game.getPiece(0, 0));
            Assert.AreEqual("", game.getPiece(0, 1));
            Assert.AreEqual("", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(1, 0));
            Assert.AreEqual("O", game.getPiece(1, 1));
            Assert.AreEqual("", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(2, 0));
            Assert.AreEqual("X", game.getPiece(2, 1));
            Assert.AreEqual("X", game.getPiece(2, 2));
        }

        [TestMethod]
        public void TestOutcome74859()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1), (1, 1), (2, 2));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsWin);
            var win = outcome as TicTacToeOutcome<Player>.Win;
            Assert.AreEqual(Cross, win.winner);
            var expectedLine = new List<ValueTuple<int, int>>() { (2, 0), (2, 1), (2, 2) };
            var actualCount = 0;
            foreach (var square in win.line)
            {
                actualCount++;
                Assert.IsTrue(expectedLine.Contains((square.Item1, square.Item2)));
            }
            Assert.AreEqual(expectedLine.Count, actualCount);
        }

        [TestMethod]
        public void TestGameStatBig()
        {
            var game = GenerateGame(Nought, 5, (0, 0), (1, 0), (0, 1), (1, 1), (0, 2), (1, 2), (0, 3), (1, 3), (0, 4));
            Assert.AreEqual(5, game.Size);
            Assert.AreEqual(Cross, game.Turn);
            Assert.AreEqual("O", game.getPiece(0, 0));
            Assert.AreEqual("O", game.getPiece(0, 1));
            Assert.AreEqual("O", game.getPiece(0, 2));
            Assert.AreEqual("O", game.getPiece(0, 3));
            Assert.AreEqual("O", game.getPiece(0, 4));
            Assert.AreEqual("X", game.getPiece(1, 0));
            Assert.AreEqual("X", game.getPiece(1, 1));
            Assert.AreEqual("X", game.getPiece(1, 2));
            Assert.AreEqual("X", game.getPiece(1, 3));
            Assert.AreEqual("", game.getPiece(1, 4));
            Assert.AreEqual("", game.getPiece(2, 0));
            Assert.AreEqual("", game.getPiece(2, 1));
            Assert.AreEqual("", game.getPiece(2, 2));
            Assert.AreEqual("", game.getPiece(2, 3));
            Assert.AreEqual("", game.getPiece(2, 4));
            Assert.AreEqual("", game.getPiece(3, 0));
            Assert.AreEqual("", game.getPiece(3, 1));
            Assert.AreEqual("", game.getPiece(3, 2));
            Assert.AreEqual("", game.getPiece(3, 3));
            Assert.AreEqual("", game.getPiece(3, 4));
            Assert.AreEqual("", game.getPiece(4, 0));
            Assert.AreEqual("", game.getPiece(4, 1));
            Assert.AreEqual("", game.getPiece(4, 2));
            Assert.AreEqual("", game.getPiece(4, 3));
            Assert.AreEqual("", game.getPiece(4, 4));
        }

        [TestMethod]
        public void TestOutcomeBig()
        {
            var game = GenerateGame(Nought, 5, (0, 0), (1, 0), (0, 1), (1, 1), (0, 2), (1, 2), (0, 3), (1, 3), (0, 4));
            var outcome = modelUnderTest.GameOutcome(game);
            Assert.IsTrue(outcome.IsWin);
            var win = outcome as TicTacToeOutcome<Player>.Win;
            Assert.AreEqual(Nought, win.winner);
            var expectedLine = new List<ValueTuple<int, int>>() { (0, 0), (0, 1), (0, 2), (0, 3), (0, 4) };
            var actualCount = 0;
            foreach (var square in win.line)
            {
                actualCount++;
                Assert.IsTrue(expectedLine.Contains((square.Item1, square.Item2)));
            }
            Assert.AreEqual(expectedLine.Count, actualCount);
        }
    }

    public abstract class TicTacToeModelWithBasicMiniMaxTests<Game, Move, Player> : TicTacToeModelTests<Game, Move, Player> where Game : ITicTacToeGame<Player> where Move : ITicTacToeMove
    {
        [TestMethod]
        public void TestFindBestNodeCount()
        {
            var game = GenerateGame(Cross, 3);
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(526906, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1()
        {
            var game = GenerateGame(Cross, 3, (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(57113, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount12()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(7764, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount123()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1253, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount124()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(983, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1245()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(149, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount125()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1001, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1253()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(150, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount126()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1265, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount127()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(923, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount128()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1441, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount129()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(897, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount13()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(7296, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount132()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1193, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount134()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(839, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount135()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(897, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount136()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1309, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount137()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(971, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount138()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1145, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount139()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(941, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount14()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(7764, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount142()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(983, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1423()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(234, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1425()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(149, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount14236()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(55, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount142369()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(14, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1423695()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(5, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount14236958()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(2, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount15()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(7044, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount152()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(911, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1523()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(190, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount15238()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(36, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount153()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(879, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount156()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1205, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount159()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1053, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount16()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(6272, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount162()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(795, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount163()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(923, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount164()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(995, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount165()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(825, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount167()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(735, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount168()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1101, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount169()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(897, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount19()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(7404, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount192()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(867, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount193()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(971, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount195()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1101, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount196()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1313, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount2()
        {
            var game = GenerateGame(Cross, 3, (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(61313, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount21()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(8284, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount213()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1193, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount214()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1369, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount2145()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(214, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount21458()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0), (1, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(40, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount215()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(995, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount2154()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(137, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount216()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1205, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount217()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1309, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount218()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(899, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount219()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1313, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount24()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(7260, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount241()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(983, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount243()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(795, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount245()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(923, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount246()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1337, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount247()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1265, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount248()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(855, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount249()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1101, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount25()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(7816, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount251()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(911, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount254()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1237, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount257()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1205, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount258()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1109, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount27()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(6792, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount271()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(839, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount273()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(867, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount274()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1205, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount275()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(819, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount276()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1017, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount278()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(899, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount279()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1145, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount28()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(8824, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount281()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(995, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount284()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1337, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount285()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1277, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount287()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1441, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount3()
        {
            var game = GenerateGame(Cross, 3, (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(57113, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount32()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(7764, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount325()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1001, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount3251()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(150, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount326()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(983, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount3265()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(149, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount4()
        {
            var game = GenerateGame(Cross, 3, (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(61313, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount41()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(8284, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount415()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(995, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount4152()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(137, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount5()
        {
            var game = GenerateGame(Cross, 3, (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(53201, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount51()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(6524, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount512()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(995, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount513()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(897, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount516()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(819, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount519()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1101, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount52()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(6776, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount521()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1001, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount524()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(923, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount527()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(825, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount528()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1277, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount7()
        {
            var game = GenerateGame(Cross, 3, (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(57113, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount74()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(7764, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount748()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(983, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount7485()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(149, NodeCounter.Count);
        }
    }

    public abstract class TicTacToeModelWithAlphaBetaPruningTests<Game, Move, Player> : TicTacToeModelTests<Game, Move, Player> where Game : ITicTacToeGame<Player> where Move : ITicTacToeMove
    {
        [TestMethod]
        public void TestFindBestNodeCount()
        {
            var game = GenerateGame(Cross, 3);
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(16087, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1()
        {
            var game = GenerateGame(Cross, 3, (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1836, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount12()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(430, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount123()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(254, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount124()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(220, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1245()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(25, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount125()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(230, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1253()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(10, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount126()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(393, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount127()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(66, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount128()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(566, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount129()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 1), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(184, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount13()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(299, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount132()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(149, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount134()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(149, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount135()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(285, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount136()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(293, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount137()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(213, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount138()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(338, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount139()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (0, 2), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(164, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount14()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(47, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount142()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(46, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1423()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(35, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1425()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(2, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount14236()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(27, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount142369()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(11, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1423695()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(5, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount14236958()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 0), (0, 1), (0, 2), (1, 2), (2, 2), (1, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(2, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount15()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(691, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount152()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(57, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount1523()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(46, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount15238()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 1), (0, 2), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(8, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount153()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(86, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount156()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(180, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount159()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 1), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(268, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount16()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(132, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount162()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(68, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount163()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(63, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount164()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(232, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount165()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(174, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount167()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(93, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount168()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(334, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount169()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (1, 2), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(174, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount19()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(162, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount192()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(76, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount193()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(85, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount195()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(337, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount196()
        {
            var game = GenerateGame(Cross, 3, (0, 0), (2, 2), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(400, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount2()
        {
            var game = GenerateGame(Cross, 3, (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(2362, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount21()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(1285, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount213()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(44, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount214()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(328, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount2145()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(129, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount21458()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 0), (1, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(12, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount215()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(197, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount2154()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(26, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount216()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(227, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount217()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(421, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount218()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(121, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount219()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (0, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(404, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount24()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(47, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount241()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(46, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount243()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(44, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount245()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(127, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount246()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(211, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount247()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(274, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount248()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(87, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount249()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(223, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount25()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(568, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount251()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(57, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount254()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(222, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount257()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(272, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount258()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (1, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(97, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount27()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(46, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount271()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(45, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount273()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(52, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount274()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(340, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount275()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(248, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount276()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(34, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount278()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(76, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount279()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(126, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount28()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(567, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount281()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(138, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount284()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(365, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount285()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(367, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount287()
        {
            var game = GenerateGame(Cross, 3, (0, 1), (2, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(123, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount3()
        {
            var game = GenerateGame(Cross, 3, (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(2786, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount32()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(575, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount325()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(240, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount3251()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(10, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount326()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(355, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount3265()
        {
            var game = GenerateGame(Cross, 3, (0, 2), (0, 1), (1, 2), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(99, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount4()
        {
            var game = GenerateGame(Cross, 3, (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(3094, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount41()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(736, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount415()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(161, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount4152()
        {
            var game = GenerateGame(Cross, 3, (1, 0), (0, 0), (1, 1), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(10, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount5()
        {
            var game = GenerateGame(Cross, 3, (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(2080, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount51()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(533, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount512()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(197, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount513()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (0, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(163, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount516()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (1, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(132, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount519()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 0), (2, 2));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(221, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount52()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(231, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount521()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (0, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(230, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount524()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(123, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount527()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(71, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount528()
        {
            var game = GenerateGame(Cross, 3, (1, 1), (0, 1), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(162, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount7()
        {
            var game = GenerateGame(Cross, 3, (2, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(3388, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount74()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(816, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount748()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(414, NodeCounter.Count);
        }

        [TestMethod]
        public void TestFindBestNodeCount7485()
        {
            var game = GenerateGame(Cross, 3, (2, 0), (1, 0), (2, 1), (1, 1));
            var move = modelUnderTest.FindBestMove(game);
            Assert.AreEqual(99, NodeCounter.Count);
        }
    }


    [TestClass]
    public class FSharpPureWithBasicMiniMaxTests : TicTacToeModelWithBasicMiniMaxTests<FSharpPureTicTacToeModel.GameState, FSharpPureTicTacToeModel.Move, FSharpPureTicTacToeModel.Player>
    {
        protected override ITicTacToeModel<FSharpPureTicTacToeModel.GameState, FSharpPureTicTacToeModel.Move, FSharpPureTicTacToeModel.Player> ModelUnderTest()
        {
            return new FSharpPureTicTacToeModel.BasicMiniMax();
        }
    }

    [TestClass]
    public class FSharpPureWithAlphaBetaPruningTests : TicTacToeModelWithAlphaBetaPruningTests<FSharpPureTicTacToeModel.GameState, FSharpPureTicTacToeModel.Move, FSharpPureTicTacToeModel.Player>
    {
        protected override ITicTacToeModel<FSharpPureTicTacToeModel.GameState, FSharpPureTicTacToeModel.Move, FSharpPureTicTacToeModel.Player> ModelUnderTest()
        {
            return new FSharpPureTicTacToeModel.WithAlphaBetaPruning();
        }
    }

    [TestClass]
    public class FSharpImpureWithAlphaBetaPruningTests : TicTacToeModelWithAlphaBetaPruningTests<FSharpImpureTicTacToeModel.GameState, FSharpImpureTicTacToeModel.Move, FSharpImpureTicTacToeModel.Player>
    {
        protected override ITicTacToeModel<FSharpImpureTicTacToeModel.GameState, FSharpImpureTicTacToeModel.Move, FSharpImpureTicTacToeModel.Player> ModelUnderTest()
        {
            return new FSharpImpureTicTacToeModel.WithAlphaBetaPruning();
        }
    }

    [TestClass]
    public class CSharpPureWithAlphaBetaPruningTests : TicTacToeModelWithAlphaBetaPruningTests<CSharpTicTacToe.Game, CSharpTicTacToe.Move, CSharpTicTacToe.Player>
    {
        protected override ITicTacToeModel<CSharpTicTacToe.Game, CSharpTicTacToe.Move, CSharpTicTacToe.Player> ModelUnderTest()
        {
            return new CSharpTicTacToe.WithAlphaBetaPruning();
        }
    }
}
