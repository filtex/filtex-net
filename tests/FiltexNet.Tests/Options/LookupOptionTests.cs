using FiltexNet.Exceptions;
using FiltexNet.Models;
using FiltexNet.Options;
using Xunit;

namespace FiltexNet.Tests.Options
{
    public class LookupOptionTests
    {
        [Fact]
        public void New_ShouldReturnLookupOption()
        {
            // Act
            var opt = LookupOption.New();

            // Assert
            Assert.NotNull(opt);
            Assert.Null(opt._key);
            Assert.Null(opt._values);
        }

        [Fact]
        public void Key_ShouldSetKeyAndReturnItself()
        {
            // Arrange
            var opt = LookupOption.New();

            // Act
            var result = opt.Key("some_key");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result._key);
            Assert.Null(result._values);
            Assert.Equal("some_key", result._key);
            Assert.Equal(opt._values, result._values);
        }

        [Fact]
        public void Values_ShouldSetValuesAndReturnItself()
        {
            // Arrange
            var opt = LookupOption.New();

            var lookups = new[]
            {
                new Lookup("Enabled", true),
                new Lookup("Disabled", true)
            };

            // Act
            var result = opt.Values(lookups);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result._key);
            Assert.NotNull(result._values);
            Assert.Equal(opt._key, result._key);
            Assert.Equal(lookups, result._values);
        }

        [Fact]
        public void Build_ShouldReturnError_WhenKeyIsNotDefined()
        {
            // Arrange
            var opt = LookupOption.New().Values(new[]
            {
                new Lookup("Enabled", true),
                new Lookup("Disabled", true)
            });

            Assert.Throws<LookupException>(() =>
            {
                // Act
                var result = opt.Build();

                // Assert
                Assert.Null(result);
            });
        }

        [Fact]
        public void Build_ShouldReturnError_WhenValuesAreNotDefined()
        {
            // Arrange
            var opt = LookupOption.New().Key("some_key");

            Assert.Throws<LookupException>(() =>
            {
                // Act
                var result = opt.Build();

                // Assert
                Assert.Null(result);
            });
        }

        [Fact]
        public void Build_ShouldReturnLookupMap_WhenKeyAndValuesAreDefined()
        {
            // Arrange
            var opt = LookupOption.New().Key("some_key").Values(new[]
            {
                new Lookup("Enabled", true),
                new Lookup("Disabled", true)
            });

            // Act
            var result = opt.Build();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Item1);
            Assert.Equal(2, result.Item2.Length);
        }
    }
}