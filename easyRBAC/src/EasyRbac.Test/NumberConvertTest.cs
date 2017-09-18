using System;
using System.Collections.Generic;
using System.Text;
using EasyRbac.Utils;
using Xunit;

namespace EasyRbac.Test
{
    public class NumberConvertTest
    {
        [Fact]
        public void Zero_Convert_success()
        {
            var convert = new NumberConvert();
            var result = convert.ToString(0);
            Assert.Equal("0",result);
        }

        [Fact]
        public void AynNum_Convert_success()
        {
            var convert = new NumberConvert();
            var result = convert.ToString(18);
            Assert.Equal("i", result);

            var rr = result.ToString().PadLeft(2, '0');
            Assert.Equal(rr,"0i");

            
            result = convert.ToString(999);
            rr = result.PadLeft(2, '0');
            Assert.Equal("g7", result);
            Assert.Equal(rr,"g7");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(100)]
        [InlineData(1024)]
        [InlineData(99999)]
        public void Generate_test_success(int value)
        {
            var convert = new NumberConvert();
            var result = convert.ToString(value);
            Assert.Equal(value,convert.FromString(result));
        }

      }
}
