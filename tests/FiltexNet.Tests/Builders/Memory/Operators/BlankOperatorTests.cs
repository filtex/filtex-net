using System;
using FiltexNet.Builders.Memory.Operators;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Memory.Operators
{
    public class BlankOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (string)null
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = string.Empty
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = Array.Empty<string>()
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeStringArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = (string[])null
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeStringArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsNumberArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = new int[] { }
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsNumberArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (int[])null
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsBooleanArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new bool[] { }
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeBooleanArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsBooleanArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = (bool[])null
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeBooleanArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new DateTime[] { }
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeDateArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = (DateTime[])null
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeDateArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsTimeArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new int[] { }
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeTimeArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsTimeArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = (int[])null
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeTimeArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateTimeArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new DateTime[] { }
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateTimeArrayAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = (DateTime[])null
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumber()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (int?)null
            });
            var expression = BlankOperator.Build(FieldType.FieldTypeNumber, "Value", null);

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
            var expression = BlankOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

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
            var expression = BlankOperator.Build(FieldType.FieldTypeDate, "Value", null);

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
            var expression = BlankOperator.Build(FieldType.FieldTypeTime, "Value", null);

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
            var expression = BlankOperator.Build(FieldType.FieldTypeDateTime, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }
    }
}