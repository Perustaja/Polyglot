using Perustaja.Polyglot.Option;
using Xunit;

namespace Perustaja.Polyglot.OptionTests
{
    public class CheckingType
    {
        [Fact]
        public void Returns_Proper_Value_If_Some()
        {
            var o = Option<string>.Some("value");
            Assert.True(o.IsSome());
            Assert.False(o.IsNone());
        }

        [Fact]
        public void Returns_Proper_Value_If_None()
        {
            var o = Option<string>.None;
            Assert.False(o.IsSome());
            Assert.True(o.IsNone());
        }
    }
}