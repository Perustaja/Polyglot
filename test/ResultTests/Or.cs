using Perustaja.Polyglot.Result;
using Xunit;

namespace Perustaja.Polyglot.ResultTests
{
    public class Or
    {
        [Fact]
        public void Or_Returns_Original_If_Ok()
        {
            var original = Result<int, string>.Ok(10);
            var result = original.Or(Result<int, string>.Ok(20));

            Assert.StrictEqual(original, result);
        }

        [Fact]
        public void Or_Returns_Other_If_Err()
        {
            var other = Result<int, string>.Ok(10);
            var result = Result<int, string>.Err("ruh roh").Or(other);

            Assert.StrictEqual(other, result);           
        }
    }
}