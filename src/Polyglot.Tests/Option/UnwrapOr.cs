using System;
using Perustaja.Polyglot.Option;
using Xunit;

namespace Polyglot.Tests
{
    public class UnwrapOr
    {
        [Fact]
        public void Returns_Default_If_None()
        {
            var value = "DefaultValue";
            var o = Option<string>.None;
            Assert.Equal(o.UnwrapOr(value), value);
        }
        [Fact]
        public void Returns_Value_If_Some()
        {
            var value = "Returned";
            var o = Option<string>.Some(value);
            Assert.Equal(o.UnwrapOr("DefaultValue"), value);
        }
    }
}
