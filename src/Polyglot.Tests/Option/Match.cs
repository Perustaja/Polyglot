using System;
using Perustaja.Polyglot.Option;
using Xunit;

namespace Polyglot.Tests
{
    public class Match
    {
        #region ReturnVersion
        [Fact]
        public void Returned_Proper_If_Some()
        {
            int value = 10;
            var o = Option<int>.Some(value);
            var r = o.Match<string>(
                s => s.ToString(),
                () => String.Empty
            );
            Assert.Equal(value.ToString(), r);
        }

        [Fact]
        public void Returns_Proper_If_None()
        {
            var o = Option<int>.None;
            var r = o.Match<string>(
                s => s.ToString(),
                () => String.Empty
            );
            Assert.Equal(String.Empty, r);
        }
        #endregion

        #region VoidVersion
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
        #endregion
    }
}
