using System.Collections.Generic;
using Perustaja.Polyglot.Option;
using Xunit;

namespace Perustaja.Polyglot.OptionTests
{
    public class EquityAndHashing
    {
        [Fact]
        public void Equals_Compares_Same_Somes_Properly()
        {
            int value = 10;
            var o1 = Option<int>.Some(value);
            var o2 = Option<int>.Some(value);

            Assert.StrictEqual(o1, o2);
            Assert.Equal(o1.GetHashCode(), o2.GetHashCode());
        }

        [Fact]
        public void Equals_Compares_Diff_Somes_Properly()
        {
            int value = 10;
            var o1 = Option<int>.Some(value);
            var o2 = Option<int>.Some(value + 1);

            Assert.NotStrictEqual(o1, o2);
            Assert.NotEqual(o1.GetHashCode(), o2.GetHashCode());
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

        [Fact]
        public void Somes_With_Diff_Values_Keys_Dont_Collide()
        {
            var set = new HashSet<Option<int>>();
            uint size = 1000;
            for (int i = 0; i < size; i++)
                set.Add(Option<int>.Some(i));

            Assert.True(set.Count == size);
        }

        [Fact]
        public void Somes_With_Same_Values_Do_Collide()
        {
            var set = new HashSet<Option<int>>();
            for (int i = 0; i < 10; i++)
                set.Add(Option<int>.Some(1));

            Assert.True(set.Count == 1);
        }

        [Fact]
        public void Nones_Do_Collide()
        {
            var set = new HashSet<Option<int>>();
            for (int i = 0; i < 10; i++)
                set.Add(Option<int>.None);

            Assert.True(set.Count == 1);
        }
    }
}