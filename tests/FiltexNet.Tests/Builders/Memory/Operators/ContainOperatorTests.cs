using System;
using FiltexNet.Builders.Memory.Operators;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Memory.Operators
{
    public class ContainOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndNullAndValueIsEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (string)null
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeString, "Value", "");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndNullAndValueIsNotEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (string)null
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeString, "Value", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndEmptyAndValueIsNotEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = string.Empty
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeString, "Value", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringAndEmptyAndValueIsEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = string.Empty
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeString, "Value", "");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new string[] { }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeStringArray, "Values", "Filtex");

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
            var expression = ContainOperator.Build(FieldType.FieldTypeStringArray, "Values", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[]
                {
                    "Dog"
                }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeStringArray, "Values", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringArrayAndNotEmptyAndContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { "Filtex" }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeStringArray, "Values", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = new int[] { }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeNumberArray, "Value", 1000);

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
            var expression = ContainOperator.Build(FieldType.FieldTypeNumberArray, "Value", 1000);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = new[] { 100 }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeNumberArray, "Value", 200);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsNumberArrayAndNotEmptyAndContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = new[] { 100 }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeNumberArray, "Value", 100);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new bool[] { }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeBooleanArray, "Values", false);

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
            var expression = ContainOperator.Build(FieldType.FieldTypeBooleanArray, "Values", true);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { true }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeBooleanArray, "Values", false);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsBooleanArrayAndNotEmptyAndContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { true }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeBooleanArray, "Values", true);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new DateTime[] { }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeDateArray, "Values", DateTime.Now);

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
            var expression = ContainOperator.Build(FieldType.FieldTypeDateArray, "Values", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { value }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeDateArray, "Values", value.AddHours(24));

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateArrayAndNotEmptyAndContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { value }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeDateArray, "Values", value);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new int[] { }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeTimeArray, "Values", 60);

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
            var expression = ContainOperator.Build(FieldType.FieldTypeTimeArray, "Values", 60);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { 60 }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeTimeArray, "Values", 120);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsTimeArrayAndNotEmptyAndContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { 60 }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeTimeArray, "Values", 60);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeArrayAndEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new DateTime[] { }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", DateTime.Now);

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
            var expression = ContainOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeArrayAndNotEmptyAndNotContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { value }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", value.AddHours(24));

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateTimeArrayAndNotEmptyAndContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Values = new[] { value }
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeDateTimeArray, "Values", value);

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
                Values = (int?)null
            });
            var expression = ContainOperator.Build(FieldType.FieldTypeNumber, "Value", 100);

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
            var expression = ContainOperator.Build(FieldType.FieldTypeBoolean, "Value", true);

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
            var expression = ContainOperator.Build(FieldType.FieldTypeDate, "Value", DateTime.Now);

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
            var expression = ContainOperator.Build(FieldType.FieldTypeTime, "Value", 60);

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
            var expression = ContainOperator.Build(FieldType.FieldTypeDateTime, "Value", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }
    }
}