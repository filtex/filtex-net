using System;
using FiltexNet.Builders.Memory.Operators;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Memory.Operators
{
    public class NotInOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndValueIsNotArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeString, "Value", "Filtex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndValueContains()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeString, "Value", new[] { "Filtex" });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringAndValueNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeString, "Value", new[] { "Filter" });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (decimal)1000
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeNumber, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberAndValueIsNotArrayAndContains()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (decimal)1000
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeNumber, "Value", (decimal)1000);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsNumberAndValueIsNotArrayAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (decimal)1000
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeNumber, "Value", (decimal)100);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsNumberAndValueContains()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (decimal)100
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeNumber, "Value", new[] { (decimal)100 });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsNumberAndValueNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (decimal)100
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeNumber, "Value", new[] { (decimal)101 });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = true
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeBoolean, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanAndValueIsNotArrayAndContains()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = true
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeBoolean, "Value", true);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsBooleanAndValueIsNotArrayAndNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = true
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeBoolean, "Value", false);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsBooleanAndValueContains()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = false
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeBoolean, "Value", new[] { false });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsBooleanAndValueNotContain()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = false
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeBoolean, "Value", new[] { true });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = DateTime.Now
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeDate, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateAndValueIsNotArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = DateTime.Now
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeDate, "Value", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateAndValueContains()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeDate, "Value", new[] { value });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateAndValueNotContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeDate, "Value", new[] { value.AddDays(1) });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = 60
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeAndValueIsNotArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = 60
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeString, "Value", 60);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsTimeAndValueContains()
        {
            // Arrange
            var value = 60;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeTime, "Value", new[] { value });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsTimeAndValueNotContain()
        {
            // Arrange
            var value = 60;
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeTime, "Value", new[] { value + 1 });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeAndValueIsNull()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = DateTime.Now
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeString, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeAndValueIsNotArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = DateTime.Now
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeString, "Value", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeAndValueContains()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeDateTime, "Value", new[] { value });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsDateTimeAndValueNotContain()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var value = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = value
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeDateTime, "Value", new[] { value.AddDays(1) });

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (string[])null
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeStringArray, "Value", null);

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
            var expression = NotInOperator.Build(FieldType.FieldTypeNumberArray, "Value", null);

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
            var expression = NotInOperator.Build(FieldType.FieldTypeBooleanArray, "Value", null);

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
            var expression = NotInOperator.Build(FieldType.FieldTypeDateArray, "Value", null);

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
            var expression = NotInOperator.Build(FieldType.FieldTypeTimeArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact] public void Build_ShouldReturnFalse_WhenFieldTypeIsDateTimeArray()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (DateTime[])null
            });
            var expression = NotInOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", null);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }
    }
}