using System;
using Perustaja.Polyglot.Option;
using Xunit;

namespace Polyglot.Tests
{
    public class Unwrap
    {
        [Fact]
        public void Returns_Value_If_Some()
        {
            var value = "Returned";
            var o = Option<string>.Some(value);
            Assert.Equal(o.Unwrap(), value);
        }

        [Fact]
        public void Throws_If_None()
        {
            var o = Option<string>.None;
            Assert.Throws<Exception>(o.Unwrap);
        }

        [Fact]
        public void UnwrapOr_Returns_Value_If_Some()
        {
            var value = "Returned";
            var o = Option<string>.Some(value);
            Assert.Equal(o.UnwrapOr("DefaultValue"), value);
        }

        [Fact]
        public void UnwrapOr_Returns_Default_If_None()
        {
            var value = "DefaultValue";
            var o = Option<string>.None;
            Assert.Equal(o.UnwrapOr(value), value);
        }

        [Fact]
        public void UnwrapOrElse_Returns_Value_If_Some()
        {
            int num = 5;
            var o = Option<int>.Some(num);
            int r = o.UnwrapOrElse(() => num * 5);
            Assert.Equal(num, r);
        }

        [Fact]
        public void UnwrapOrElse_Returns_Default_If_None()
        {

            int num = 5;
            var o = Option<int>.None;
            int r = o.UnwrapOrElse(() => num * 5);
            Assert.Equal(num * 5, r);
        }

        [Fact]
        public void UnwrapOrDefault_Returns_Value_If_Some()
        {
            int num = 5;
            var o = Option<int>.Some(num);
            int r = o.UnwrapOrDefault();
            Assert.Equal(num, r);
        }

        [Fact]
        public void UnwrapOrDefault_Returns_Default_If_None()
        {
            var o = Option<int>.None;
            int r = o.UnwrapOrDefault();
            Assert.Equal(default(int), r);
        }
    }
}
