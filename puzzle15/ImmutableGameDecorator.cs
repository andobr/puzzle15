using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace puzzle15
{
    [Serializable]
    public class ImmutableGameDecorator : ImmutableGame
    {
        Queue<int> steps = new Queue<int>();

        public ImmutableGameDecorator(params int[] numbers) : base(numbers) { }

        public override int this[int x, int y]
        {
            get
            {
                if (x >= size || 0 > x || y >= size || 0 > y)
                    throw new NonexistentPointException($"There is no point [{x},{y}]");
                return (int)ApplySteps()[new Point(x, y)];
            }
        }

        private Hashtable ApplySteps()
        {
            var temp = DeepClone(positions);
            foreach (var i in steps)
            {
                Point pointV = GetLocation(i, temp);
                Point point0 = GetLocation(0, temp);
                temp[new Point(pointV.x, pointV.y)] = 0;
                temp[0] = new Point(pointV.x, pointV.y);
                temp[new Point(point0.x, pointV.y)] = i;
                temp[i] = new Point(point0.x, pointV.y);
            }
            return temp;
        }

        private Point GetLocation(int value, Hashtable table)
        {
            if (!table.ContainsValue(value))
                throw new NonexistentPuzzleException($"There is no value {value}");
            return (Point)table[value];
        }

        public override Point GetLocation(int value)
        {
            if (!positions.ContainsValue(value))
                throw new NonexistentPuzzleException($"There is no value {value}");
            return (Point)ApplySteps()[value];
        }

        public override Game Shift(int value)
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
          
            steps.Enqueue(value);

            return this;
        }
    }
}
