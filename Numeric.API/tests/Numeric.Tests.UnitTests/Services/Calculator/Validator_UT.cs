using Calculator.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Calculator.Tests
{
    public class Validator_UT
    {
        [Theory]
        [InlineData("")]
        public void Validate_ZeroLength_False(string expression)
        {
            // Arrange
            var validator = new Validator();
            var expected = false;

            // Act
            var result = validator.Validate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null)]
        public void Validate_NullValue_ArgumentNullException(string expression)
        {
            // Arrange
            var validator = new Validator();

            // Act
            Action act = () => validator.Validate(expression);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Theory]
        [MemberData(nameof(NotValidCharacters_False))]
        public void Validate_NotValidCharacters_False(string expression)
        {
            // Arrange
            var validator = new Validator();
            var expected = false;

            // Act
            var result = validator.Validate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object> NotValidCharacters_False()
        {
            return new[]
            {
                new object[] { " " },
                new object[] { "    " },
                new object[] { "\n" },
                new object[] { "sasdfg" },
                new object[] { "12-3+3d-2" },
                new object[] { "4eygo747gs9hg" },
                new object[] { "{2+2}" },
                new object[] { "[2+2]" },
            };
        }

        [Theory]
        [MemberData(nameof(NotValidExpression_False))]
        public void Validate_NotValidExpression_False(string expression)
        {
            // Arrange
            var validator = new Validator();
            var expected = false;

            // Act
            var result = validator.Validate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object> NotValidExpression_False()
        {
            return new[]
            {
                new object[] { "12+33+" },
                new object[] { "12+ 33" },
                new object[] { "+22*5" },
                new object[] { "12//43+4" },
                new object[] { "()+4-5" },
                new object[] { "(1+1" },
                new object[] { "32*21)" },
                new object[] { "()+34+1" },
                new object[] { "1.+56" },
                new object[] { ".5-44" },
                new object[] { "5-44." },
                new object[] { "34+.+2" },
                new object[] { "44+.(22*4)" },
                new object[] { "3.(20 - 5)" },
                new object[] { "(2+8)(3-7)" },
                new object[] { "10(3-7)" },
                new object[] { "10+)" },
                new object[] { "(2+8)5" },
            };
        }

        [Theory]
        [MemberData(nameof(ValidExpression_True))]
        public void Validate_ValidExpression_True(string expression)
        {
            // Arrange
            var validator = new Validator();
            var expected = true;

            // Act
            var result = validator.Validate(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object> ValidExpression_True()
        {
            return new[]
            {
                new object[] { "12+33" },
                new object[] { "12/43+4" },
                new object[] { "-2+4-5" },
                new object[] { "(1+1)" },
                new object[] { "2*(1+4)" },
                new object[] { "50/(2*(1+4))" },
                new object[] { "50/(2*(1+4))-1+100" },
                new object[] { "50.34/(2*(1+4))" },
                new object[] { "2^-2" }
            };
        }
    }
}
