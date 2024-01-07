using System;
using FiltexNet.Builders.Memory.Operators;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Memory.Operators
{
    public class NotBlankOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (string)null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = string.Empty
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = Array.Empty<string>()
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeStringArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = (string[])null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeStringArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = new int[] { }
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (int[])null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new bool[] { }
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeBooleanArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = (bool[])null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeBooleanArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new DateTime[] { }
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = (DateTime[])null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new int[] { }
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeTimeArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = (int[])null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeTimeArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new DateTime[] { }
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = (DateTime[])null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumber()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (int?)null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeNumber, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBoolean()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (bool?)null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDate()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (DateTime?)null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDate, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTime()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (int?)null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeTime, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTime()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (DateTime?)null
            });
            var expression = NotBlankOperator.Build(FieldType.FieldTypeDateTime, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }
    }
}