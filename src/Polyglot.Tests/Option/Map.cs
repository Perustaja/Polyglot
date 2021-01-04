using System;
using Perustaja.Polyglot.Option;
using Xunit;

namespace Polyglot.Tests
{
    public class Map
    {
        [Fact]
        public void Maps_To_Some_If_Some()
        {
            int val = 10;
            var o = Option<int>.Some(val);
            Assert.Equal(o.Map<string>(o => o.ToString()).Unwrap(), val.ToString());
        }

        [Fact]
        public void Maps_To_None_If_None()
        {
            var o = Option<int>.None;
            Assert.Throws<Exception>(o.Map<string>(o => o.ToString()).Unwrap);
        }
    }
}
