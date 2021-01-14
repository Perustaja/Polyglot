using System;
using System.Collections.Generic;
using Perustaja.Polyglot.Result;
using Xunit;

namespace Perustaja.Polyglot.ResultTests
{
    public class EquityAndHashing
    {
        [Fact]
        public void Equals_Compares_Same_Oks_Properly()
        {
            int value = 10;
            var o1 = Result<int, string>.Ok(value);
            var o2 = Result<int, string>.Ok(value);

            Assert.StrictEqual(o1, o2);
            Assert.Equal(o1.GetHashCode(), o2.GetHashCode());
        }

        [Fact]
        public void Equals_Compares_Diff_Oks_Properly()
        {
            int value = 10;
            var o1 = Result<int, string>.Ok(value);
            var o2 = Result<int, string>.Ok(value + 1);

            Assert.NotStrictEqual(o1, o2);
            Assert.NotEqual(o1.GetHashCode(), o2.GetHashCode());
        }

        [Fact]
        public void Equals_Compares_Same_Errs_Properly()
        {
            char value = 'c';
            var o1 = Result<int, char>.Err(value);
            var o2 = Result<int, char>.Err(value);

            Assert.StrictEqual(o1, o2);
            Assert.Equal(o1.GetHashCode(), o2.GetHashCode());
        }

        [Fact]
        public void Equals_Compares_Diff_Errs_Properly()
        {
            char value = 'c';
            var o1 = Result<int, char>.Err(value);
            var o2 = Result<int, char>.Err(Char.ToUpper(value));

            Assert.NotStrictEqual(o1, o2);
            Assert.NotEqual(o1.GetHashCode(), o2.GetHashCode());
        }

        [Fact]
        public void Equals_Compares_Diff_Ok_And_Err_Properly()
        {
            char value = 'c';
            var o1 = Result<int, char>.Ok(10);
            var o2 = Result<int, char>.Err(value);

            Assert.NotStrictEqual(o1, o2);
            Assert.NotEqual(o1.GetHashCode(), o2.GetHashCode());
        }

        [Fact]
        public void Same_Value_Ok_And_Err_Have_Diff_Hashes()
        {
            int value = 10;
            var o1 = Result<int, string>.Ok(value);
            var o2 = Result<string, int>.Err(value);

            // Results of different types cannot be compared
            Assert.NotEqual(o1.GetHashCode(), o2.GetHashCode());
        }

        [Fact]
        public void Oks_With_Diff_Values_Dont_Collide()
        {
            var set = new HashSet<Result<int, string>>();
            uint size = 1000;
            for (int i = 0; i < size; i++)
                set.Add(Result<int, string>.Ok(i));

            Assert.True(set.Count == size);
        }

        [Fact]
        public void Oks_With_Same_Values_Do_Collide()
        {
            var set = new HashSet<Result<int, string>>();
            for (int i = 0; i < 10; i++)
                set.Add(Result<int, string>.Ok(1));

            Assert.True(set.Count == 1);
        }

        [Fact]
        public void Errs_With_Diff_Values_Dont_Collide()
        {
            var set = new HashSet<Result<string, int>>();
            uint size = 1000;
            for (int i = 0; i < size; i++)
                set.Add(Result<string, int>.Err(i));

            Assert.True(set.Count == size);
        }

        [Fact]
        public void Errs_With_Same_Values_Do_Collide()
        {
            var set = new HashSet<Result<int, string>>();
            for (int i = 0; i < 10; i++)
                set.Add(Result<int, string>.Err("Hello!!"));

            Assert.True(set.Count == 1);
        }
    }
}