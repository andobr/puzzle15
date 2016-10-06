using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game15.Tests
{
    [TestClass]
    public class ImmutableGameTest : GameTest
    {
        public override Game GameGenerator(int[] set)
        {
            return new ImmutableGame(set);
        }

        [TestMethod]
        public override void ShiftReturnCorrectObject()
        {
            var game = GameGenerator(correctSet);

            var newGame = game.Shift(movablePuzzleBeforeFirstStep);

            Assert.AreEqual(0, game[0, 0]);
            Assert.AreEqual(1, game[0, 1]);
            Assert.AreEqual(2, game[1, 0]);
            Assert.AreEqual(3, game[1, 1]);

            Assert.AreEqual(1, newGame[0, 0]);
            Assert.AreEqual(0, newGame[0, 1]);
            Assert.AreEqual(2, newGame[1, 0]);
            Assert.AreEqual(3, newGame[1, 1]);
        }
    }
}
