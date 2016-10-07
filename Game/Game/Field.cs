using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game15
{
    public struct Field
    {
        public Dictionary<int, Point> pointsByValue;
        public Dictionary<Point, int> valuesByPoint;

        public Field(int size)
        {
            pointsByValue = new Dictionary<int, Point>(size * size);
            valuesByPoint = new Dictionary<Point, int>(size * size);
        }

        public Field(Field field)
        {
            pointsByValue = new Dictionary<int, Point>(field.pointsByValue.Count);
            valuesByPoint = new Dictionary<Point, int>(field.valuesByPoint.Count);

            foreach (var entry in field.pointsByValue)
                pointsByValue.Add(entry.Key, entry.Value);

            foreach (var entry in field.valuesByPoint)
                valuesByPoint.Add(entry.Key, entry.Value);
        }
    }
}
