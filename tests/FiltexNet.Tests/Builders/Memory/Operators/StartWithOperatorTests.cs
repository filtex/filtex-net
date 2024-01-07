using System;
using FiltexNet.Builders.Memory.Operators;
using FiltexNet.Builders.Memory.Utils;
using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Builders.Memory.Operators
{
    public class StartWithOperatorTests
    {
        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndNullAndValueIsEmpty()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = (string)null
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeString, "Value", "");

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
            var expression = StartWithOperator.Build(FieldType.FieldTypeString, "Value", "Filtex");

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
                Value = ""
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeString, "Value", "Filtex");

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
                Value = ""
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeString, "Value", "");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Build_ShouldReturnFalse_WhenFieldTypeIsStringAndDoesNotStartWithValue()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeString, "Value", "ex");

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Build_ShouldReturnTrue_WhenFieldTypeIsStringAndStartsWithValue()
        {
            // Arrange
            var data = MemoryUtils.ObjectToDictionary(new
            {
                Value = "Filtex"
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeString, "Value", "Fil");

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
                Value = 100
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeNumber, "Value", 100);

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
                Value = new[] { 100 }
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeNumberArray, "Value", 100);

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
                Value = true
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeBoolean, "Value", true);

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
                Value = new[] { true }
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeBooleanArray, "Value", true);

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
                Value = DateTime.Now
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeDate, "Value", DateTime.Now);

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
                Value = new[] { DateTime.Now }
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeDateArray, "Value", DateTime.Now);

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
                Value = 60
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeTime, "Value", 60);

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
                Value = new[] { 60 }
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeTimeArray, "Value", 60);

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
                Value = DateTime.Now
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeDateTime, "Value", DateTime.Now);

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
                Value = new[] { DateTime.Now }
            });
            var expression = StartWithOperator.Build(FieldType.FieldTypeDateTimeArray, "Value", DateTime.Now);

            // Act
            var result = expression.Fn(data);

            // Assert
            Assert.False(result);
        }
    }
}