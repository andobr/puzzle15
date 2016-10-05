using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game15.Tests
{
    [TestClass]
    public class GameTest
    {
        public int[] correctSet = new int[] { 0, 1, 2, 3 };
        public int[] nonSquareSet = new int[] { 0, 1, 2, 3, 4 };
        public int[] incorrectSet = new int[] { 0, 1, 2, 4 };

        public Point existingPoint = new Point(0, 0);
        public Point nonexistentPoint = new Point(2, 2);

        public int existingPuzzle = 0;
        public int nonexistentPuzzle = 4;

        public int movablePuzzle = 1;
        public int immovablePuzzle = 3;

        public virtual Game GameGenerator(int[] set)
        {
            return new Game(set);
        }

        [TestMethod]
        public virtual void ShiftCorrectlyMovesMovablePuzzle()
        {
            var game = GameGenerator(correctSet);

            var newGame = game.Shift(movablePuzzle);

            Assert.AreSame(game, newGame);
        }

        [TestMethod]
        [ExpectedException(typeof(ImmovablePuzzleException))]
        public void ShiftOfImmovablePuzzleThrowException()
        {
            GameGenerator(correctSet).Shift(immovablePuzzle);
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
