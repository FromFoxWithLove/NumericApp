using Calculator.Enums;
using Calculator.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class ElementsParser_UT
    {
        [Theory]
        [MemberData(nameof(Operators_TypeOperator))]
        public void GetType_Operators_TypeOperator(char element)
        {
            // Arrange
            var expected = ElementsTypeEnum.Operator;

            // Act
            var result = ElementsHelper.GetElementsType(element);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object> Operators_TypeOperator()
        {
            return new[]
            {
                new object[] { '+' },
                new object[] { '-' },
                new object[] { '*' },
                new object[] { '/' },
                new object[] { '^' },
            };
        }

        [Theory]
        [MemberData(nameof(Parentheses_TypeParentheses))]
        public void GetType_Parentheses_TypeParentheses(char element)
        {
            // Arrange
            var expected = ElementsTypeEnum.Parentheses;

            // Act
            var result = ElementsHelper.GetElementsType(element);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object> Parentheses_TypeParentheses()
        {
            return new[]
            {
                new object[] { '(' },
                new object[] { ')' },
            };
        }

        [Theory]
        [MemberData(nameof(Number_TypeNumber))]
        public void GetType_Number_TypeNumber(char element)
        {
            // Arrange
            var expected = ElementsTypeEnum.Number;

            // Act
            var result = ElementsHelper.GetElementsType(element);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object> Number_TypeNumber()
        {
            return new[]
            {
                new object[] { '0' },
                new object[] { '1' },
                new object[] { '2' },
                new object[] { '3' },
                new object[] { '4' },
                new object[] { '5' },
                new object[] { '6' },
                new object[] { '7' },
                new object[] { '8' },
                new object[] { '9' },
            };
        }

        [Theory]
        [MemberData(nameof(Point_TypePoint))]
        public void GetType_Point_TypePoint(char element)
        {
            // Arrange
            var expected = ElementsTypeEnum.Point;

            // Act
            var result = ElementsHelper.GetElementsType(element);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object> Point_TypePoint()
        {
            return new[]
            {
                new object[] { '.' },
                new object[] { ',' },
            };
        }


        [Theory]
        [MemberData(nameof(Value_ArgumentException))]
        public void Validate_Value_ArgumentException(char element)
        {
            // Arrange

            // Act
            Action act = () => ElementsHelper.GetElementsType(element);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        public static IEnumerable<object> Value_ArgumentException()
        {
            return new[]
            {
                new object[] { 'q' },
                new object[] { '@' },
                new object[] { ' ' },
                new object[] { '}' },
            };
        }
    }
}
