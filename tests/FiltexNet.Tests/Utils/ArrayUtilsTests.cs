using FiltexNet.Utils;
using Xunit;

namespace FiltexNet.Tests.Utils
{
    public class ArrayUtilsTests
    {
        [Fact]
        public void IsInAny_ShouldReturnFalse_WhenSourcesDoesNotContainItem()
        {
            // Act
            // Assert
            Assert.False(ArrayUtils.IsInAny("test", new[] { "test1" }, new[] { "test2" }));
            Assert.False(ArrayUtils.IsInAny(100, new[] { 90 }, new[] { 110 }));
            Assert.False(ArrayUtils.IsInAny(true, new[] { false }));
        }

        [Fact]
        public void IsInAny_ShouldReturnTrue_WhenSourcesContainsItem()
        {
            // Act
            // Assert
            Assert.True(ArrayUtils.IsInAny("test", new[] { "test" }, new[] { "test2" }, new[] { "test3" }));
            Assert.True(ArrayUtils.IsInAny(100, new[] { 90 }, new[] { 100, 110 }));
            Assert.True(ArrayUtils.IsInAny(true, new[] { false }, new[] { true, false }));
        }
    }
}