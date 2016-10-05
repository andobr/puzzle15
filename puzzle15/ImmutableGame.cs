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
    public class ImmutableGame : Game
    {
        public ImmutableGame(params int[] numbers) : base(numbers) { }

        public ImmutableGame(ImmutableGame game)
        {
            size = game.size;
            positions = DeepClone(game.positions);
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

            var game = new ImmutableGame(this);

            game.positions[new Point(x, y)] = 0;
            game.positions[0] = new Point(x, y);

            game.positions[new Point(x0, y0)] = value;
            game.positions[value] = new Point(x0, y0);

            return game;
        }

        protected static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
    }
}
