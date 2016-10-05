using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game15
{
    public class Game
    {
        public int size;
        public Hashtable positions;

        public Game(params int[] numbers)
        {
            size = (int)Math.Sqrt(numbers.Length);
            CheckSquareNumber(numbers);
            CheckSet(numbers);
            positions = new Hashtable(size * size * 2);
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    positions.Add(new Point(x, y), numbers[x * size + y]);
                    positions.Add(numbers[x * size + y], new Point(x, y));
                }
        }

        public virtual int this[int x, int y]
        {
            get
            {
                if (x >= size || 0 > x || y >= size || 0 > y)
                    throw new NonexistentPointException($"There is no point [{x},{y}]");
                return (int)positions[new Point(x, y)];
            }
        }

        private void CheckSquareNumber(int[] numbers)
        {
            if (size * size != numbers.Length)
                throw new NonSquarePuzzlesSetException("The quantity of arguments must be square number");
            return;
        }

        private void CheckSet(int[] numbers)
        {
            int count = 0;
            for (int i = 0; i < numbers.Length; i++)
                if (numbers.Contains(i))
                    count++;
            if (count != numbers.Length)
                throw new IncorrectPuzzlesSetException("The set must contain all numbers from 0 to its length minus one");
            return;
        }

        public virtual Point GetLocation(int value)
        {
            if (!positions.ContainsValue(value))
                throw new NonexistentPuzzleException($"There is no value {value}");
            return (Point)positions[value];
        }

        public virtual Game Shift(int value)
        {
            Point valueIndex = GetLocation(value);
            Point zeroIndex = GetLocation(0);

            int x = valueIndex.x;
            int y = valueIndex.y;

            int x0 = zeroIndex.x;
            int y0 = zeroIndex.y;

            if (!(Math.Abs(x - x0) == 1 && y == y0 || Math.Abs(y - y0) == 1 && x == x0))
            {
                throw new ImmovablePuzzleException("Puzzle can not be moved");
            }

            positions[new Point(x, y)] = 0;
            positions[0] = new Point(x, y);

            positions[new Point(x0, y0)] = value;
            positions[value] = new Point(x0, y0);

            return this;
        }
    }
}
