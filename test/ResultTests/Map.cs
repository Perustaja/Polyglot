using System;
using Perustaja.Polyglot.Result;
using Xunit;

namespace Perustaja.Polyglot.ResultTests
{
    public class Map
    {
        [Fact]
        public void Map_Applies_Func_To_Ok_If_Ok()
        {
            int val = 10;
            var r = Result<int, string>.Ok(val).Map(n => n.ToString());

            Assert.Equal(val.ToString(), r.Unwrap());
        }

        [Fact]
        public void Map_Returns_Err_If_Err()
        {
            int val = 10;
            var r = Result<string, int>.Err(val).Map(n => n.ToLower());

            Assert.True(r.IsErr());
            Assert.Equal(val, r.UnwrapErr());
        }

        [Fact]
        public void MapOr_Returns_Func_Result_If_Ok()
        {
            int val = 10;
            var r = Result<int, string>.Ok(val).MapOr("", val => val.ToString());

            Assert.Equal(val.ToString(), r);
        }

        [Fact]
        public void MapOr_Returns_Fallback_If_Err()
        {
            string fallback = "Default";
            var r = Result<int, string>.Err("").MapOr(fallback, val => val.ToString());

            Assert.Equal(fallback, r);
        }

        [Fact]
        public void MapOrElse_Returns_Func_Result_If_Ok()
        {
            int val = 10;
            var r = Result<int, string>.Ok(val).MapOrElse(() => "", val => val.ToString());

            Assert.Equal(val.ToString(), r);
        }

        [Fact]
        public void MapOrElse_Returns_Fallback_Func_Result_If_Err()
        {
            Func<string> fallbackFunc = 10.ToString;
            var r = Result<int, string>.Err("").MapOrElse(fallbackFunc, val => val.ToString());

            Assert.Equal(fallbackFunc(), r);
        }

        [Fact]
        public void MapErr_Applies_Func_To_Err_If_Err()
        {
            int val = 10;
            var r = Result<string, int>.Err(val).MapErr(n => n.ToString());

            Assert.Equal(val.ToString(), r.UnwrapErr());
        }

        [Fact]
        public void MapErr_Returns_Ok_If_Ok()
        {
            int val = 10;
            var r = Result<int, string>.Ok(val).MapErr(n => n.ToLower());

            Assert.Equal(val, r.Unwrap());
        }
    }
}