using System;
using FiltexNet.Builders.Memory.Operators;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Memory.Operators
{
    public class NotContainOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringAndNullAndValueIsEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (string)null
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeString, "Value", "");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringAndNullAndValueIsNotEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (string)null
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeString, "Value", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringAndEmptyAndValueIsNotEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = string.Empty
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeString, "Value", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndEmptyAndValueIsEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = string.Empty
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeString, "Value", "");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new string[] { }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeStringArray, "Values", "Filtex");

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
            var expression = NotContainOperator.Build(FieldType.FieldTypeStringArray, "Values", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[]
                {
                    "Dog"
                }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeStringArray, "Values", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringArrayAndNotEmptyAndContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { "Filtex" }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeStringArray, "Values", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsNumberArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = new int[] { }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeNumberArray, "Value", 1000);

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
            var expression = NotContainOperator.Build(FieldType.FieldTypeNumberArray, "Value", 1000);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsNumberArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = new[] { 100 }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeNumberArray, "Value", 200);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberArrayAndNotEmptyAndContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = new[] { 100 }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeNumberArray, "Value", 100);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsBooleanArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new bool[] { }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeBooleanArray, "Values", false);

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
            var expression = NotContainOperator.Build(FieldType.FieldTypeBooleanArray, "Values", true);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsBooleanArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { true }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeBooleanArray, "Values", false);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanArrayAndNotEmptyAndContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { true }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeBooleanArray, "Values", true);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new DateTime[] { }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateArray, "Values", DateTime.Now);

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
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateArray, "Values", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { value }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateArray, "Values", value.AddHours(24));

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateArrayAndNotEmptyAndContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { value }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateArray, "Values", value);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsTimeArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new int[] { }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeTimeArray, "Values", 60);

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
            var expression = NotContainOperator.Build(FieldType.FieldTypeTimeArray, "Values", 60);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsTimeArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { 60 }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeTimeArray, "Values", 120);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeArrayAndNotEmptyAndContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { 60 }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeTimeArray, "Values", 60);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateTimeArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new DateTime[] { }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", DateTime.Now);

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
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateTimeArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { value }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", value.AddHours(24));

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeArrayAndNotEmptyAndContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { value }
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", value);

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
                Values = (int?)null
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeNumber, "Value", 100);

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
                Values = (bool?)null
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeBoolean, "Value", true);

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
                Values = (DateTime?)null
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeDate, "Value", DateTime.Now);

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
                Values = (int?)null
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeTime, "Value", 60);

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
                Values = (DateTime?)null
            });
            var expression = NotContainOperator.Build(FieldType.FieldTypeDateTime, "Value", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }
    }
}