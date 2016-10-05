using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace puzzle15.Tests
{
    [TestClass]
    public class ImmutableGameDecoratorTest : GameTest
    {
        public override Game GameGenerator(int[] set)
        {
            return new ImmutableGameDecorator(set); 
        }
    }
}
