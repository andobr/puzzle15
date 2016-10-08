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
                return ApplySteps().valuesByPoint[new Point(x, y)];
            }
        }

        public override Point GetLocation(int value)
        {
            if (!field.pointsByValue.ContainsKey(value))
                throw new NonexistentPuzzleException($"There is no value {value}");
            return ApplySteps().pointsByValue[value];
        }

        private Field ApplySteps()
        {
            var temp = new Field(field);
            foreach (var value in steps)
            {
                Point vPoint = temp.pointsByValue[value]; 
                Point zPoint = temp.pointsByValue[0]; 

                temp.valuesByPoint[vPoint] = 0;
                temp.valuesByPoint[zPoint] = value;

                temp.pointsByValue[0] = vPoint;
                temp.pointsByValue[value] = zPoint;
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
