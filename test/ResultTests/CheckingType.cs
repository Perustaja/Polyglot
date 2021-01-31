using Perustaja.Polyglot.Option;
using Perustaja.Polyglot.Result;
using Xunit;

namespace Perustaja.Polyglot.ResultTests
{
    public class CheckingType
    {
        [Fact]
        public void Returns_Proper_Value_If_Some()
        {
            var ok = Result<string, int>.Ok("Hi!");

            Assert.True(ok.IsOk());
            Assert.False(ok.IsErr());
        }

        [Fact]
        public void Returns_Proper_Value_If_None()
        {
            var err = Result<string, int>.Err(10);

            Assert.False(err.IsOk());
            Assert.True(err.IsErr());
        }
    }
}