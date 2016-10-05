using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace puzzle15.Tests
{
    [TestClass]
    public class ImmutableGameTest : GameTest
    {
        public override Game GameGenerator(int[] set)
        {
            return new ImmutableGame(set);
        }

        [TestMethod]
        public override void ShiftCorrectlyMovesMovablePuzzle()
        {
            var game = GameGenerator(correctSet);

            var newGame = game.Shift(movablePuzzle);

            Assert.AreNotSame(game, newGame);
        }
    }
}
