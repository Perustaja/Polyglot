using Perustaja.Polyglot.Result;
using Xunit;

namespace Perustaja.Polyglot.ResultTests
{
    public class AndThen
    {
        [Fact]
        public void Returns_Ok_With_Func_Result_If_Ok()
        {
            int val = 10;
            var r = Result<int, string>.Ok(val).AndThen(SquareIfOk);

            Assert.Equal(val * 2, r.Unwrap());
        }

        [Fact]
        public void Returns_Untouched_Err_If_Err()
        {
            string val = "Error";
            var r = Result<int, string>.Err(val).AndThen(SquareIfOk);

            Assert.Equal(val, r.UnwrapErr());
        }

        private Result<int, string> SquareIfOk(Result<int, string> res)
            => res.IsOk()
            ? res.Map(n => n * 2)
            : res;
    }
}