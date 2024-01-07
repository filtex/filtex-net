using System;
using FiltexNet.Builders.Memory.Operators;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Memory.Operators
{
    public class InOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringAndValueIsNotArrayAndContains()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndValueIsNotArrayAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Test"
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringAndValueContains()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", new[] { "Filtex" });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndValueNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", new[] { "Filter" });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (decimal)1000
            });
            var expression = InOperator.Build(FieldType.FieldTypeNumber, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberAndValueIsNotArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (decimal)1000
            });
            var expression = InOperator.Build(FieldType.FieldTypeNumber, "Value", (decimal)100);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsNumberAndValueContains()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (decimal)100
            });
            var expression = InOperator.Build(FieldType.FieldTypeNumber, "Value", new[] { (decimal)100 });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberAndValueNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (decimal)100
            });
            var expression = InOperator.Build(FieldType.FieldTypeNumber, "Value", new[] { (decimal)101 });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = true
            });
            var expression = InOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanAndValueIsNotArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = true
            });
            var expression = InOperator.Build(FieldType.FieldTypeBoolean, "Value", false);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsBooleanAndValueContains()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = false
            });
            var expression = InOperator.Build(FieldType.FieldTypeBoolean, "Value", new[] { false });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanAndValueNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = false
            });
            var expression = InOperator.Build(FieldType.FieldTypeBoolean, "Value", new[] { true });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = DateTime.Now
            });
            var expression = InOperator.Build(FieldType.FieldTypeDate, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateAndValueIsNotArrayAndContains()
        {
            // Arrange
            var now = DateTime.Now;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = now
            });
            var expression = InOperator.Build(FieldType.FieldTypeDate, "Value", now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateAndValueIsNotArrayAndNotContain()
        {
            // Arrange
            var now = DateTime.Now;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = now
            });
            var expression = InOperator.Build(FieldType.FieldTypeDate, "Value", now.AddHours(24));

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateAndValueContains()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = InOperator.Build(FieldType.FieldTypeDate, "Value", new[] { value });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateAndValueNotContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = InOperator.Build(FieldType.FieldTypeDate, "Value", new[] { value.AddDays(1) });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = 60
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsTimeAndValueIsNotArrayAndContains()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = 60
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", 60);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeAndValueIsNotArrayAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = 60
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", 100);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsTimeAndValueContains()
        {
            // Arrange
            var value = 60;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = InOperator.Build(FieldType.FieldTypeTime, "Value", new[] { value });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeAndValueNotContain()
        {
            // Arrange
            var value = 60;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = InOperator.Build(FieldType.FieldTypeTime, "Value", new[] { value + 1 });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = DateTime.Now
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateTimeAndValueIsNotArray()
        {
            // Arrange
            var now = DateTime.Now;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = now
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeAndValueIsNotArrayAndNotContain()
        {
            // Arrange
            var now = DateTime.Now;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = now
            });
            var expression = InOperator.Build(FieldType.FieldTypeString, "Value", now.AddHours(24));

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateTimeAndValueContains()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = InOperator.Build(FieldType.FieldTypeDateTime, "Value", new[] { value });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeAndValueNotContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = InOperator.Build(FieldType.FieldTypeDateTime, "Value", new[] { value.AddDays(1) });

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
            var expression = InOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

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
            var expression = InOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

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
            var expression = InOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

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
            var expression = InOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

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
            var expression = InOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

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
            var expression = InOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }
    }
}