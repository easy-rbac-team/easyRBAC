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
