using System;
using Perustaja.Polyglot.Option;
using Xunit;

namespace Polyglot.Tests
{
    public class Unwrap
    {
        [Fact]
        public void Throws_If_None()
        {
            var o = Option<string>.None;
            Assert.Throws<Exception>(o.Unwrap);
        }

        [Fact]
        public void Returns_Value_If_Some()
        {
            var value = "Returned";
            var o = Option<string>.Some(value);
            Assert.Equal(o.Unwrap(), value);
        }
    }
}
