using System;
using FiltexNet.Builders.Memory.Operators;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Memory.Operators
{
    public class LessThanOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (decimal?)null
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeNumber, "Value", (decimal)100);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsNumberAndLess()
        {
            // Arrange
            var value = (decimal)99;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeNumber, "Value", value + 1);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberAndEqual()
        {
            // Arrange
            var value = (decimal)100;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeNumber, "Value", value);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberAndGreater()
        {
            // Arrange
            var value = (decimal)100;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeNumber, "Value", value - 1);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (DateTime?)null
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeDate, "Value", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateAndLess()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeDate, "Value", value.AddDays(1));

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateAndEqual()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeDate, "Value", value);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateAndGreater()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeDate, "Value", value.AddDays(-1));

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (int?)null
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeTime, "Value", 60);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsTimeAndLess()
        {
            // Arrange
            var value = 60;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeTime, "Value", value + 1);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeAndEqual()
        {
            // Arrange
            var value = 60;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeTime, "Value", value);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeAndGreater()
        {
            // Arrange
            var value = 60;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression =
                LessThanOperator.Build(FieldType.FieldTypeTime, "Value", value - 1);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeAndNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (DateTime?)null
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeDateTime, "Value", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateTimeAndLess()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeDateTime, "Value", value.AddHours(24));

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeAndEqual()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeDateTime, "Value", value);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeAndGreater()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeDateTime, "Value", value.AddHours(-24));

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsString()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (string[])null
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

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
                Value = false
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (bool[])null
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (int[])null
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (DateTime[])null
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (int[])null
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (DateTime[])null
            });
            var expression = LessThanOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }
    }
}