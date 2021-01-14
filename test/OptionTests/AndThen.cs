using System;
using Perustaja.Polyglot.Option;
using Xunit;

namespace Perustaja.Polyglot.OptionTests
{
    public class AndThen
    {
        [Fact]
        public void Invokes_If_Some()
        {
            var o = Option<int>.Some(10);
            var r = o.AndThen(Square).AndThen(Square);
            Assert.Equal(Math.Pow(10, 4), r.Unwrap());
        }

        [Fact]
        public void Returns_None_If_None()
        {
            var o = Option<int>.None;
            var r = o.AndThen(Square).AndThen(Square);
            Assert.True(r.IsNone());
        }

        private Option<int> Square(int n)
            => n % 2 == 0 
            ? Option<int>.Some(n * n)
            : Option<int>.None;
    }
}
