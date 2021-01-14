using Perustaja.Polyglot.Option;
using Xunit;

namespace Perustaja.Polyglot.OptionTests
{
    public class Or
    {
        [Fact]
        public void Or_Returns_Other_If_None()
        {
            var other = Option<int>.Some(10);
            var original = Option<int>.None.Or(other);

            Assert.Equal(Option<int>.Some(10), original);
        }

        [Fact]
        public void Or_Returns_Original_If_Some()
        {
            var other = Option<int>.Some(10);
            var original = Option<int>.Some(20).Or(other);

            Assert.Equal(Option<int>.Some(20), original);
        }

        [Fact]
        public void OrElse_Returns_Func_Result__If_None()
        {
            var other = Option<int>.Some(10);
            var original = Option<int>.None.OrElse(() => other);

            Assert.Equal(Option<int>.Some(10), original);
        }

        [Fact]
        public void OrElse_Returns_Original_If_Some()
        {
            var other = Option<int>.Some(10);
            var original = Option<int>.Some(20).OrElse(() => other);

            Assert.Equal(Option<int>.Some(20), original);
        }
    }
}