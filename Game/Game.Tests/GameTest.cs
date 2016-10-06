using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game15.Tests
{
    [TestClass]
    public class GameTest
    {
        public int[] correctSet = new int[] { 0, 1, 2, 3 };
        public int[] nonSquareSet = new int[] { 0, 1, 2, 3, 4 };
        public int[] incorrectSet = new int[] { 0, 1, 2, 2 };

        public Point existingPoint = new Point(0, 0);
        public Point nonexistentPoint = new Point(2, 2);

        public int existingPuzzle = 0;
        public int nonexistentPuzzle = 4;

        public int movablePuzzleBeforeFirstStep = 1;
        public int immovablePuzzleBeforeFirstStep = 3;

        public int movablePuzzleAfterFirstStep = 3;

        public virtual Game GameGenerator(int[] set)
        {
            return new Game(set);
        }

        [TestMethod]
        public virtual void ShiftCorreclyMovesMovablePuzzles()
        {
            var game = GameGenerator(correctSet);

            game = game.Shift(movablePuzzleBeforeFirstStep);
            game = game.Shift(movablePuzzleAfterFirstStep);

            Assert.AreEqual(1, game[0, 0]);
            Assert.AreEqual(3, game[0, 1]);
            Assert.AreEqual(2, game[1, 0]);
            Assert.AreEqual(0, game[1, 1]);

            Assert.AreEqual(new Point(1, 1), game.GetLocation(0));
            Assert.AreEqual(new Point(0, 0), game.GetLocation(1));
            Assert.AreEqual(new Point(1, 0), game.GetLocation(2));
            Assert.AreEqual(new Point(0, 1), game.GetLocation(3));
        }

        [TestMethod]
        public virtual void ShiftReturnCorrectObject()
        {
            var game = GameGenerator(correctSet);

            var newGame = game.Shift(movablePuzzleBeforeFirstStep);

            Assert.AreEqual(1, game[0, 0]);
            Assert.AreEqual(0, game[0, 1]);
            Assert.AreEqual(2, game[1, 0]);
            Assert.AreEqual(3, game[1, 1]);

            Assert.AreEqual(1, newGame[0, 0]);
            Assert.AreEqual(0, newGame[0, 1]);
            Assert.AreEqual(2, newGame[1, 0]);
            Assert.AreEqual(3, newGame[1, 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ImmovablePuzzleException))]
        public void ShiftOfImmovablePuzzleThrowException()
        {
            GameGenerator(correctSet).Shift(immovablePuzzleBeforeFirstStep);
        }

        [TestMethod]
        public void GetLocationOfExistingPuzzleReturnItsCoordinates()
        {
            var result = GameGenerator(correctSet).GetLocation(existingPuzzle);

            Assert.AreEqual(0, result.x);
            Assert.AreEqual(0, result.y);
        }

        [TestMethod]
        [ExpectedException(typeof(NonexistentPuzzleException))]
        public void GetLocationOfNonexistentPuzzleThrowException()
        {
            GameGenerator(correctSet).GetLocation(nonexistentPuzzle);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectPuzzlesSetException))]
        public void ConstructorThrowExceptionWhenSetIsIncorrect()
        {
            GameGenerator(incorrectSet);
        }

        [TestMethod]
        [ExpectedException(typeof(NonSquarePuzzlesSetException))]
        public void ConstructorThrowExceptionWhenQuantityOfArgumentsIsNotSquareNumber()
        {
            GameGenerator(nonSquareSet);
        }

        [TestMethod]
        public void IndexatorReturnValueOfExistingPoint()
        {
            var result = GameGenerator(correctSet)[existingPoint.x, existingPoint.y];

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NonexistentPointException))]
        public void IndexatorThrowExceptionOfNonexistentPoint()
        {
            var result = GameGenerator(correctSet)[nonexistentPoint.x, nonexistentPoint.y];
        }
    }
}
