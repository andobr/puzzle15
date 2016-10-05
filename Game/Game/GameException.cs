using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game15
{
    public class GameException : Exception
    {
        public GameException(string message) : base(message) { }
    }

    public class ImmovablePuzzleException : GameException
    {
        public ImmovablePuzzleException(string message) : base(message) { }
    }

    public class NonSquarePuzzlesSetException : GameException
    {
        public NonSquarePuzzlesSetException(string message) : base(message) { }
    }

    public class IncorrectPuzzlesSetException : GameException
    {
        public IncorrectPuzzlesSetException(string message) : base(message) { }
    }

    public class NonexistentPointException : GameException
    {
        public NonexistentPointException(string message) : base(message) { }
    }

    public class NonexistentPuzzleException : GameException
    {
        public NonexistentPuzzleException(string message) : base(message) { }
    }
}
