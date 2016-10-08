using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game15.Tests
{
    [TestClass]
    public class GameTest
    {
        protected int[] correctSet;
        protected int[] nonSquareSet;
        protected int[] incorrectSet;

        protected Point existingPoint;
        protected Point nonexistentPoint;

        protected int existingPuzzle;
        protected int nonexistentPuzzle;

        protected int movablePuzzleBeforeFirstStep;
        protected int immovablePuzzleBeforeFirstStep;

        protected int movablePuzzleAfterFirstStep;

        [TestInitialize()]
        public void Initialize()
        {
            correctSet = new int[] { 0, 1, 2, 3 };
            nonSquareSet = new int[] { 0, 1, 2, 3, 4 };
            incorrectSet = new int[] { 0, 1, 2, 2 };

            existingPoint = new Point(0, 0);
            nonexistentPoint = new Point(2, 2);

            existingPuzzle = 0;
            nonexistentPuzzle = 4;

            movablePuzzleBeforeFirstStep = 1;
            immovablePuzzleBeforeFirstStep = 3;

            movablePuzzleAfterFirstStep = 3;
        }

        public virtual Game GameGenerator(int[] set)
        {
            return new Game(set);
        }

        [TestMethod]
        public virtual void ShiftCorrectlyMovesMovablePuzzles()
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
