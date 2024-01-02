using FiltexNet.Constants;
using Xunit;

namespace FiltexNet.Tests.Constants
{
    public class FieldTypeTests
    {
        [Fact]
        public void IsArray_ShouldReturnFalse_WhenValueIsNotArray()
        {
            // Arrange
            var samples = new[]
            {
                FieldType.FieldTypeUnknown,
                FieldType.FieldTypeString,
                FieldType.FieldTypeNumber,
                FieldType.FieldTypeBoolean,
                FieldType.FieldTypeDate,
                FieldType.FieldTypeTime,
                FieldType.FieldTypeDateTime
            };

            foreach (var sample in samples)
            {
                // Act
                var result = sample.IsArray();

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void IsArray_ShouldReturnTrue_WhenValueIsArray()
        {
            // Arrange
            var samples = new[]
            {
                FieldType.FieldTypeStringArray,
                FieldType.FieldTypeNumberArray,
                FieldType.FieldTypeBooleanArray,
                FieldType.FieldTypeDateArray,
                FieldType.FieldTypeTimeArray,
                FieldType.FieldTypeDateTimeArray
            };

            foreach (var sample in samples)
            {
                // Act
                var result = sample.IsArray();

                // Assert
                Assert.True(result);
            }
        }
    }
}