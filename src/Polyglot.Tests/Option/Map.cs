using System;
using Perustaja.Polyglot.Option;
using Xunit;

namespace Polyglot.Tests
{
    public class Map
    {
        [Fact]
        public void Maps_To_Some_If_Some()
        {
            int val = 10;
            var o = Option<int>.Some(val);
            Assert.Equal(o.Map(o => o.ToString()).Unwrap(), val.ToString());
        }

        [Fact]
        public void Maps_To_None_If_None()
        {
            var o = Option<int>.None;
            Assert.Throws<Exception>(o.Map(o => o.ToString()).Unwrap);
        }

        [Fact]
        public void MapOr_Returns_Func_Result_If_Some()
        {
            int value = 10;
            string r = Option<int>.Some(value).MapOr("Hi!", n => n.ToString());
            Assert.Equal(value.ToString(), r);
        }

        [Fact]
        public void MapOr_Returns_Fallback_If_None()
        {
            string fallback = "Hi!";
            string r = Option<int>.None.MapOr(fallback, n => n.ToString());
            Assert.Equal(fallback, r);
        }

        [Fact]
        public void MapOrElse_Returns_Invocation_Result_If_Some()
        {
            int value = 10;
            string s = "hello";
            string r = Option<int>.Some(value).MapOrElse(
                () => s.ToUpper(), 
                s => s.ToString()
                );
            Assert.Equal(value.ToString(), r);
        }

        [Fact]
        public void MapOrElse_Returns_Fallback_Invocation_Result_If_None()
        {
            string greeting = "hello";
            string r = Option<int>.None.MapOrElse(
                () => greeting.ToUpper(), 
                s => s.ToString()
                );
            Assert.Equal(greeting.ToUpper(), r);
        }
    }
}
