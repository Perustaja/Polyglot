using Perustaja.Polyglot.Option;
using Xunit;

namespace Perustaja.Polyglot.OptionTests
{
    public class EquityAndHashing
    {
        [Fact]
        public void Equals_Compares_Some_Some_Properly()
        {
            int value = 10;
            var o1 = Option<int>.Some(value);
            var o2 = Option<int>.Some(value);

            Assert.StrictEqual(o1, o2);
            Assert.Equal(o1.GetHashCode(), o2.GetHashCode());
        }

        [Fact]
        public void Equals_Compares_Some_None_Properly()
        {
            int value = 10;
            var o1 = Option<int>.Some(value);
            var o2 = Option<int>.None;

            Assert.NotStrictEqual(o1, o2);
            Assert.NotEqual(o1.GetHashCode(), o2.GetHashCode());
        }

        [Fact]
        public void Equals_Compares_None_None_Properly()
        {
            var o1 = Option<int>.None;
            var o2 = Option<int>.None;

            Assert.StrictEqual(o1, o2);
            Assert.Equal(o1.GetHashCode(), o2.GetHashCode());
        }

        [Fact]
        public void Equals_Compares_None_Some_Properly()
        {
            var o1 = Option<int>.Some(10);
            var o2 = Option<int>.None;

            Assert.NotStrictEqual(o1, o2);
            Assert.NotEqual(o1.GetHashCode(), o2.GetHashCode());
        }
    }
}