using Perustaja.Polyglot.Option;
using Xunit;

namespace Perustaja.Polyglot.OptionTests
{
    public class Ok
    {
        [Fact]
        public void OkOr_Returns_Ok_If_Some()
        {
            int val = 10;
            var r = Option<int>.Some(val).OkOr<string>("Error!");

            Assert.True(r.IsOk());
            Assert.Equal(val, r.Unwrap());
        }

        [Fact]
        public void OkOr_Returns_Err_If_None()
        {
            string fallback = "Default";
            var r = Option<int>.None.OkOr<string>(fallback);

            Assert.True(r.IsErr());
            Assert.Equal(fallback, r.UnwrapErr());
        }

        [Fact]
        public void OkOrElse_Returns_Ok_If_Some()
        {
            int val = 10;
            var r = Option<int>.Some(val).OkOrElse<string>(() => val.ToString());

            Assert.True(r.IsOk());
            Assert.Equal(val, r.Unwrap());
        }

        [Fact]
        public void OkOrElse_Returns_Err_If_None()
        {
            int fallback = 10;
            var r = Option<int>.None.OkOrElse<string>(() => fallback.ToString());

            Assert.True(r.IsErr());
            Assert.Equal(fallback.ToString(), r.UnwrapErr());
        }
    }
}