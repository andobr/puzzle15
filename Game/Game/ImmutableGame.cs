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
    public class ImmutableGame : Game
    {
        public ImmutableGame(params int[] numbers) : base(numbers) { }

        private ImmutableGame(ImmutableGame game)
        {
            size = game.size;
            field = new Field(game.field);
        }

        protected override Game Replace(int value)
        {
            var game = new ImmutableGame(this);

            game.field.valuesByPoint[GetLocation(value)] = 0;
            game.field.valuesByPoint[GetLocation(0)] = value;

            game.field.pointsByValue[0] = GetLocation(value);
            game.field.pointsByValue[value] = GetLocation(0);

            return game;
        }
    }
}
