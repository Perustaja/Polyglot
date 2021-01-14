using System;
using Perustaja.Polyglot.Result;
using Xunit;

namespace Perustaja.Polyglot.ResultTests
{
    public class Unwrap
    {
        [Fact]
        public void Unwrap_Returns_Value_If_Ok()
        {
            int value = 10;
            var r = Result<int, string>.Ok(10);

            Assert.Equal(value, r.Unwrap());
        }

        [Fact]
        public void Unwrap_Throws_If_Err()
        {
            var r = Result<int, string>.Err("Hi");
            Assert.Throws<Exception>(() => r.Unwrap());
        }

        [Fact]
        public void UnwrapErr_Returns_Value_If_Err()
        {
            int value = 10;
            var r = Result<string, int>.Err(10);

            Assert.Equal(value, r.UnwrapErr());
        }

        [Fact]
        public void UnwrapErr_Throws_If_Ok()
        {
            var r = Result<string, int>.Err(10);
            Assert.Throws<Exception>(() => r.Unwrap());
        }
    }
}