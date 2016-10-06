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
        protected int size;
        protected Field field;

        public Game(params int[] numbers)
        {
            size = (int)Math.Sqrt(numbers.Length);
            CheckSquareNumber(numbers);
            CheckSet(numbers);
            field = new Field(size);
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    field.valuesByPoint.Add(new Point(x, y), numbers[x * size + y]);
                    field.pointsByValue.Add(numbers[x * size + y], new Point(x, y));
                }
        }

        public virtual int this[int x, int y]
        {
            get
            {
                if (x >= size || 0 > x || y >= size || 0 > y)
                    throw new NonexistentPointException($"There is no point [{x},{y}]");
                return (int)field.valuesByPoint[new Point(x, y)];
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
            List<int> correctNumbers = new List<int>();
            for (int num = 0; num < numbers.Length; num++)
                if (numbers.Contains(num) && !correctNumbers.Contains(num))
                    correctNumbers.Add(num);
                else
                    throw new IncorrectPuzzlesSetException("The set must contain all numbers from 0 to its length minus one");
            return;
        }

        public virtual Point GetLocation(int value)
        {
            if (!field.pointsByValue.ContainsKey(value))
                throw new NonexistentPuzzleException($"There is no value {value}");
            return (Point)field.pointsByValue[value];
        }

        public Game Shift(int value)
        {
            Point vPoint = GetLocation(value);
            Point zPoint = GetLocation(0);

            if (!(Math.Abs(vPoint.x - zPoint.x) == 1 && vPoint.y == zPoint.y || 
                Math.Abs(vPoint.y - zPoint.y) == 1 && vPoint.x == zPoint.x))
            {
                throw new ImmovablePuzzleException("Puzzle can not be moved");
            }
            return Replace(value);
        }

        protected virtual Game Replace(int value)
        {
            Point vPoint = GetLocation(value);
            Point zPoint = GetLocation(0);

            field.valuesByPoint[vPoint] = 0;
            field.valuesByPoint[zPoint] = value;

            field.pointsByValue[0] = vPoint;           
            field.pointsByValue[value] = zPoint;

            return this;
        }
    }
}
