using Calculator;
using Calculator.Abstractions;
using Calculator.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Numeric.Tests.UnitTests.Services.Calculator
{
    public class Calculator_UT
    {

        [Theory]
        [InlineData("")]
        public void Calculate_ZeroLength_ArgumentException(string expression)
        {
            // Arrange
            var mockValidator = new Mock<IValidator>();
            mockValidator.Setup(x => x.Validate(It.IsAny<string>())).Returns(false);
            var mockParser = new Mock<IExpressionParser>();

            var calculator = new CalculatorClass(mockValidator.Object, mockParser.Object);

            // Act
            Action act = () => calculator.Calculate(expression);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [InlineData(null)]
        public void Calculate_NullValue_ArgumentNullException(string expression)
        {
            // Arrange
            var mockValidator = new Mock<IValidator>();
            mockValidator.Setup(x => x.Validate(It.IsAny<string>())).Returns(false);
            var mockParser = new Mock<IExpressionParser>();

            var calculator = new CalculatorClass(mockValidator.Object, mockParser.Object);

            // Act
            Action act = () => calculator.Calculate(expression);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Theory]
        [MemberData(nameof(NotValidCharacters_ArgumentException))]
        public void Calculate_NotValidCharacters_ArgumentException(string expression)
        {
            // Arrange
            var mockValidator = new Mock<IValidator>();
            mockValidator.Setup(x => x.Validate(It.IsAny<string>())).Returns(false);
            var mockParser = new Mock<IExpressionParser>();

            var calculator = new CalculatorClass(mockValidator.Object, mockParser.Object);

            // Act
            Action act = () => calculator.Calculate(expression);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        public static IEnumerable<object> NotValidCharacters_ArgumentException()
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
        [MemberData(nameof(DivideByZero_DivideByZeroException))]
        public void Calculate_DivideByZero_DivideByZeroException(string expression)
        {
            // Arrange
            var mockValidator = new Mock<IValidator>();
            mockValidator.Setup(x => x.Validate(It.IsAny<string>())).Returns(true);
            var mockParser = new Mock<ExpressionParser>();

            var calculator = new CalculatorClass(mockValidator.Object, mockParser.Object);

            // Act
            Action act = () => calculator.Calculate(expression);

            // Assert
            Assert.Throws<DivideByZeroException>(act);
        }

        public static IEnumerable<object> DivideByZero_DivideByZeroException()
        {
            return new[]
            {
                new object[] { "1/0" },
                new object[] { "50/(2*(2+1-3))-1+100" },
            };
        }

        [Theory]
        [MemberData(nameof(NegativeNumberToFractionalPower_NegativeNumberToFractionalPowerException))]
        public void Calculate_NegativeNumberToFractionalPower_NegativeNumberToFractionalPowerException(string expression)
        {
            // Arrange
            var mockValidator = new Mock<IValidator>();
            mockValidator.Setup(x => x.Validate(It.IsAny<string>())).Returns(true);
            var mockParser = new Mock<ExpressionParser>();

            var calculator = new CalculatorClass(mockValidator.Object, mockParser.Object);

            // Act
            Action act = () => calculator.Calculate(expression);

            // Assert
            Assert.Throws<NegativeNumberToFractionalPowerException>(act);
        }

        public static IEnumerable<object> NegativeNumberToFractionalPower_NegativeNumberToFractionalPowerException()
        {
            return new[]
            {
                new object[] { "(-2)^(4/7)" },
                new object[] { "(-8)^0.5" },
            };
        }

        [Theory]
        [MemberData(nameof(NotValidExpression_ArgumentException))]
        public void Calculate_NotValidExpression_ArgumentException(string expression)
        {
            // Arrange
            var mockValidator = new Mock<IValidator>();
            mockValidator.Setup(x => x.Validate(It.IsAny<string>())).Returns(false);
            var mockParser = new Mock<IExpressionParser>();

            var calculator = new CalculatorClass(mockValidator.Object, mockParser.Object);

            // Act
            Action act = () => calculator.Calculate(expression);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        public static IEnumerable<object> NotValidExpression_ArgumentException()
        {
            return new[]
            {
                new object[] { "12+33+" },
                new object[] { "+22*5" },
                new object[] { "12//43+4" },
                new object[] { "()+4-5" },
                new object[] { "(1+1" },
                new object[] { ")+1+1+("},
                new object[] { "(23+15(" },
                new object[] { "32*21)" },
                new object[] { "()+34+1" },
                new object[] { "1.+56" },
                new object[] { ".5-44" },
                new object[] { "5-44." },
                new object[] { "34+.+2" },
                new object[] { "44+.(22*4)" },
                new object[] { "3.(20 - 5)"},
            };
        }

        [Theory]
        [MemberData(nameof(ValidExpression_Value))]
        public void Calculate_ValidExpression_Value(string expression, double expectedValue)
        {
            // Arrange
            var mockValidator = new Mock<IValidator>();
            mockValidator.Setup(x => x.Validate(It.IsAny<string>())).Returns(true);
            var mockParser = new Mock<ExpressionParser>();

            var calculator = new CalculatorClass(mockValidator.Object, mockParser.Object);

            // Act
            var result = calculator.Calculate(expression);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        public static IEnumerable<object> ValidExpression_Value()
        {
            return new[]
            {
                new object[] { "12+33", 45 },
                new object[] { "12/ 43+4", 4.27906976744186},
                new object[] { "- 2 + 4 - 5", -3 },
                new object[] { "( 1+ 1) ", 2 },
                new object[] { "2*(1+4)", 10 },
                new object[] { "50/(2*(1+4))", 5 },
                new object[] { "50/(2*(1+4))-1+100", 104 },
                new object[] { "29.5 + 0.5 - 10 * 2", 10 },
                new object[] { "(2*((1+5)*(9-3)))/2", 36 },
                new object[] { "2^(1+1)^2^2", 65536 },
                new object[] { "8^0.5", 2.8284271247461903 },
                new object[] { "2*(-3-2)", -10 },
                new object[] { "2^-2", 0.25 }
            };
        }
    }
}
