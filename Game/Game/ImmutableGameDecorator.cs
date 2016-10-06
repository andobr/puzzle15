using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Game15
{
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
                return (int)ApplySteps().valuesByPoint[new Point(x, y)];
            }
        }

        private Point GetLocation(int value, Hashtable table)
        {
            if (!table.ContainsKey(value))
                throw new NonexistentPuzzleException($"There is no value {value}");
            return (Point)table[value];
        }

        public override Point GetLocation(int value)
        {
            if (!field.pointsByValue.ContainsKey(value))
                throw new NonexistentPuzzleException($"There is no value {value}");
            return (Point)ApplySteps().pointsByValue[value];
        }

        private Field ApplySteps()
        {
            var temp = new Field(field);
            foreach (var value in steps)
            {
                Point valuePoint = GetLocation(value, temp.pointsByValue);
                Point zeroPoint = GetLocation(0, temp.pointsByValue);

                temp.valuesByPoint[new Point(valuePoint.x, valuePoint.y)] = 0;
                temp.valuesByPoint[new Point(zeroPoint.x, zeroPoint.y)] = value;

                temp.pointsByValue[0] = new Point(valuePoint.x, valuePoint.y);
                temp.pointsByValue[value] = new Point(zeroPoint.x, zeroPoint.y);
            }
            return temp;
        }

        protected override Game Replace(int value)
        {
            steps.Enqueue(value);
            return this;
        }
    }
}
