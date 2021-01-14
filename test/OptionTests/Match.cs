using System;
using Perustaja.Polyglot.Option;
using Xunit;

namespace Perustaja.Polyglot.OptionTests
{
    public class Match
    {
        [Fact]
        public void Invokes_SomeAction_If_Some()
        {
            var bystander = 10;
            var o = Option<int>.Some(10);
            o.Match(
                s => bystander += s,
                () => { }
            );
            Assert.Equal(20, bystander);
        }

        [Fact]
        public void Invokes_NoneAction_If_None()
        {
            var bystander = 10;
            var o = Option<int>.None;
            o.Match(
                s => bystander += s,
                () => { }
            );
            Assert.Equal(10, bystander);
        }
    }
}
