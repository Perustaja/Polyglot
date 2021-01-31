using Perustaja.Polyglot.Result;
using Xunit;

namespace Perustaja.Polyglot.ResultTests
{
    public class Match
    {
        [Fact]
        public void Invokes_OkAction_If_Ok()
        {
            var bystander = 10;
            var o = Result<int, string>.Ok(10);
            o.Match(
                t => bystander += t,
                e => { }
            );
            Assert.Equal(20, bystander);
        }

        [Fact]
        public void Invokes_ErrAction_If_Err()
        {
            var bystander = 10;
            var o = Result<int, string>.Err("Oops");
            o.Match(
                t => bystander += t,
                e => { }
            );
            Assert.Equal(10, bystander);
        }
    }
}