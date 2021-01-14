using System;
using Perustaja.Polyglot.Option;
using Xunit;

namespace Perustaja.Polyglot.OptionTests
{
    public class Filter
    {
        [Fact]
        public void Filters_To_None_If_None()
        {
            Func<int, bool> predicate = n => n % 2 == 0;
            var o = Option<int>.None.Filter(predicate);

            Assert.True(o.IsNone());
        }

        [Fact]
        public void Filters_To_None_If_Predicate_Returns_False()
        {
            Func<int, bool> predicate = n => n % 2 == 0;
            int val = 3;
            var o = Option<int>.Some(val).Filter(predicate);

            Assert.True(!predicate(val));
            Assert.True(o.IsNone());
        }

        [Fact]
        public void Returns_Some_If_Predicate_Returns_True()
        {
            Func<int, bool> predicate = n => n % 2 == 0;
            int val = 2;
            var o = Option<int>.Some(val).Filter(predicate);

            Assert.True(predicate(val));
            Assert.True(o.IsSome());
        }
    }
}
