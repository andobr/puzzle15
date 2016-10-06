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
        public Hashtable pointsByValue;
        public Hashtable valuesByPoint;

        public Field(int size)
        {
            pointsByValue = new Hashtable(size * size);
            valuesByPoint = new Hashtable(size * size);
        }

        public Field(Field field)
        {
            pointsByValue = new Hashtable(field.pointsByValue.Count);
            valuesByPoint = new Hashtable(field.valuesByPoint.Count);

            foreach (DictionaryEntry entry in field.pointsByValue)
                pointsByValue.Add(entry.Key, entry.Value);

            foreach (DictionaryEntry entry in field.valuesByPoint)
                valuesByPoint.Add(entry.Key, entry.Value);
        }
    }
}
