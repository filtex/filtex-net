using System;
using System.Collections.Generic;
using FiltexNet.Exceptions;
using FiltexNet.Utils;
using Xunit;

namespace FiltexNet.Tests.Utils
{
    public class CastUtilsTests
    {
        [Fact]
        public void IsArray_ShouldReturnFalse_WhenInputIsNotArray()
        {
            // Act
            // Arrange
            Assert.False(CastUtils.IsArray("test"));
            Assert.False(CastUtils.IsArray(100));
            Assert.False(CastUtils.IsArray(true));
            Assert.False(CastUtils.IsArray(DateTime.Now));
            Assert.False(CastUtils.IsArray(60));
            Assert.False(CastUtils.IsArray(new object()));
        }

        [Fact]
        public void IsArray_ShouldReturnTrue_WhenInputIsArray()
        {
            // Act
            // Arrange
            Assert.True(CastUtils.IsArray(new string[] { }));
            Assert.True(CastUtils.IsArray(new double[] { }));
            Assert.True(CastUtils.IsArray(new bool[] { }));
            Assert.True(CastUtils.IsArray(new DateTime[] { }));
            Assert.True(CastUtils.IsArray(new int[] { }));
        }

        [Fact]
        public void Array_ShouldReturnError_WhenInputIsNotValid()
        {
            // Arrange
            object data = "test";

            Assert.Throws<CastException>(() =>
            {
                // Act
                var result = CastUtils.Array(data);

                // Assert
                Assert.Null(result);
            });
        }

        [Fact]
        public void Array_ShouldReturnArray_WhenInputIsValid()
        {
            // Arrange
            object data = new[]
            {
                "test1",
                "test2"
            };

            // Act
            var result = CastUtils.Array(data);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            Assert.Equal("test1", result[0]);
            Assert.Equal("test2", result[1]);
        }

        [Fact]
        public void IsString_ShouldReturnFalse_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object()
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsString(input);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void IsString_ShouldReturnTrue_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                Convert.ToInt16(100),
                Convert.ToInt32(100),
                Convert.ToInt64(100),
                Convert.ToUInt16(100),
                Convert.ToUInt32(100),
                Convert.ToUInt64(100),
                Convert.ToDouble(100),
                Convert.ToDecimal(100),
                true,
                "test",
                DateTime.Now,
                60,
                (object)null
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsString(input);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void String_ShouldReturnError_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object()
            };

            foreach (var input in sampleMap)
            {
                Assert.Throws<CastException>(() =>
                {
                    // Act
                    var result = CastUtils.String(input);

                    // Assert
                    Assert.Null(result);
                });
            }
        }

        [Fact]
        public void String_ShouldReturnValueAsString_WhenInputTypeIsSupported()
        {
            // Arrange
            var now = DateTime.Now;
            var sampleMap = new Dictionary<object, string>
            {
                { Convert.ToInt16(100), "100" },
                { Convert.ToInt32(10), "10" },
                { Convert.ToInt64(100), "100" },
                { Convert.ToUInt16(100), "100" },
                { Convert.ToUInt32(100), "100" },
                { Convert.ToUInt64(100), "100" },
                { Convert.ToDouble(10), "10" },
                { Convert.ToDecimal(100), "100" },
                { true, "True" },
                { "test", "test" },
                { now, now.ToString() },
                { 60, "60" }
            };

            foreach (var (input, output) in sampleMap)
            {
                // Act
                var result = CastUtils.String(input);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(output, result);
            }
        }

        [Fact]
        public void IsNumber_ShouldReturnFalse_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object(),
                "TEST",
                DateTime.Now
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsNumber(input);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void IsNumber_ShouldReturnTrue_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new object[]
            {
                Convert.ToInt16(100),
                Convert.ToInt32(100),
                Convert.ToInt64(100),
                Convert.ToUInt16(100),
                Convert.ToUInt32(100),
                Convert.ToUInt64(100),
                Convert.ToDouble(100),
                Convert.ToDecimal(100),
                true,
                false,
                "123"
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsNumber(input);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void Number_ShouldReturnError_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object(),
                "TEST",
                DateTime.Now
            };

            foreach (var input in sampleMap)
            {
                Assert.Throws<CastException>(() =>
                {
                    // Act
                    var result = CastUtils.Number(input);

                    // Assert
                    Assert.Null(result);
                });
            }
        }

        [Fact]
        public void Number_ShouldReturnValueAsNumber_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new Dictionary<object, double>
            {
                { Convert.ToInt16(100), 100 },
                { Convert.ToInt32(10), 10 },
                { Convert.ToInt64(100), 100 },
                { Convert.ToUInt16(100), 100 },
                { Convert.ToUInt32(100), 100 },
                { Convert.ToUInt64(100), 100 },
                { Convert.ToDouble(10), 10 },
                { Convert.ToDecimal(100), 100 },
                { true, 1 },
                { false, 0 },
                { "123", 123 }
            };

            foreach (var (input, output) in sampleMap)
            {
                // Act
                var result = CastUtils.Number(input);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(output, result);
            }
        }

        [Fact]
        public void IsBool_ShouldReturnFalse_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object(),
                "TEST",
                DateTime.Now,
                60,
                123
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsBoolean(input);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void IsBool_ShouldReturnTrue_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new object[]
            {
                true,
                false,
                "true",
                "false",
                "True",
                "False",
                "TRUE",
                "FALSE",
                1,
                0
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsBoolean(input);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void Bool_ShouldReturnError_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object(),
                "TEST",
                DateTime.Now,
                60,
                123
            };

            foreach (var input in sampleMap)
            {
                Assert.Throws<CastException>(() =>
                {
                    // Act
                    var result = CastUtils.Boolean(input);

                    // Assert
                    Assert.Null(result);
                });
            }
        }

        [Fact]
        public void Bool_ShouldReturnValueAsBoolean_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new Dictionary<object, bool>
            {
                { true, true },
                { false, false },
                { "true", true },
                { "false", false },
                { "True", true },
                { "False", false },
                { "TRUE", true },
                { "FALSE", false },
                { 1, true },
                { 0, false }
            };

            foreach (var (input, output) in sampleMap)
            {
                // Act
                var result = CastUtils.Boolean(input);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(output, result);
            }
        }

        [Fact]
        public void IsDate_ShouldReturnFalse_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object(),
                "TEST",
                60,
                123,
                true,
                false
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsDate(input);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void IsDate_ShouldReturnTrue_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new object[]
            {
                new DateTime(2020, 01, 01, 0, 0, 0, 0),
                new DateTime(2020, 01, 01, 10, 12, 32, 800),
                "2020-01-01",
                "2020-01-01 10:12:14",
                "2020-01-01 10:12:14.899",
                "2020-01-01T00:00:00Z"
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsDate(input);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void Date_ShouldReturnError_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object(),
                "TEST",
                60,
                123,
                true,
                false
            };

            foreach (var input in sampleMap)
            {
                Assert.Throws<CastException>(() =>
                {
                    // Act
                    var result = CastUtils.Date(input);

                    // Assert
                    Assert.Null(result);
                });
            }
        }

        [Fact]
        public void Date_ShouldReturnValueAsDate_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new Dictionary<object, DateTime>
            {
                {
                    new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc),
                    new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc)
                },
                {
                    new DateTime(2020, 01, 01, 10, 12, 32, 800, DateTimeKind.Utc),
                    new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc)
                },
                { "2020-01-01", new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc) },
                { "2020-01-01 10:12:14", new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc) },
                { "2020-01-01 10:12:14.899", new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc) },
                { "2020-01-01T00:00:00Z", new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc) }
            };

            foreach (var (input, output) in sampleMap)
            {
                // Act
                var result = CastUtils.Date(input);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(output, result);
            }
        }

        [Fact]
        public void IsTime_ShouldReturnFalse_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object(),
                "TEST",
                DateTime.Now
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsTime(input);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void IsTime_ShouldReturnTrue_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new object[]
            {
                // 60,
                // "10:12:11",
                "1H",
                "1H30M",
                "1h30m"
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsTime(input);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void Time_ShouldReturnError_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object(),
                "TEST",
                DateTime.Now
            };

            foreach (var input in sampleMap)
            {
                Assert.Throws<CastException>(() =>
                {
                    // Act
                    var result = CastUtils.Time(input);

                    // Assert
                    Assert.Null(result);
                });
            }
        }

        [Fact]
        public void Time_ShouldReturnValueAsTime_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new Dictionary<object, int>
            {
                { 10, 10 }
                // {"1h30m",    1*60*60 + 30*60},
                // {"01:30:00", 1*60*60 + 30*60},
                // {"01:30",    1*60*60 + 30*60},
            };

            foreach (var (input, output) in sampleMap)
            {
                // Act
                var result = CastUtils.Time(input);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(output, result);
            }
        }

        [Fact]
        public void IsDateTime_ShouldReturnFalse_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object(),
                "TEST",
                60,
                123,
                true,
                false
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsDateTime(input);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void IsDateTime_ShouldReturnTrue_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new object[]
            {
                new DateTime(2020, 01, 01, 0, 0, 0, 0),
                new DateTime(2020, 01, 01, 10, 12, 32, 800),
                "2020-01-01",
                "2020-01-01 10:12:14",
                "2020-01-01 10:12:14.899",
                "2020-01-01T00:00:00Z"
            };

            foreach (var input in sampleMap)
            {
                // Act
                var result = CastUtils.IsDateTime(input);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void DateTime_ShouldReturnError_WhenInputTypeIsNotSupported()
        {
            // Arrange
            var sampleMap = new[]
            {
                new object(),
                "TEST",
                60,
                123,
                true,
                false
            };

            foreach (var input in sampleMap)
            {
                Assert.Throws<CastException>(() =>
                {
                    // Act
                    var result = CastUtils.DateTime(input);

                    // Assert
                    Assert.Null(result);
                });
            }
        }

        [Fact]
        public void DateTime_ShouldReturnValueAsDateTime_WhenInputTypeIsSupported()
        {
            // Arrange
            var sampleMap = new Dictionary<object, DateTime>
            {
                {
                    new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc),
                    new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc)
                },
                {
                    new DateTime(2020, 01, 01, 10, 12, 32, 800, DateTimeKind.Utc),
                    new DateTime(2020, 01, 01, 10, 12, 32, 0, DateTimeKind.Utc)
                },
                { "2020-01-01", new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc) },
                { "2020-01-01 10:12:14", new DateTime(2020, 01, 01, 10, 12, 14, 0, DateTimeKind.Utc) },
                { "2020-01-01 10:12:14.899", new DateTime(2020, 01, 01, 10, 12, 14, 899, DateTimeKind.Utc) },
                { "2020-01-01T00:00:00Z", new DateTime(2020, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc) }
            };

            foreach (var (input, output) in sampleMap)
            {
                // Act
                var result = CastUtils.DateTime(input);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(output, result);
            }
        }
    }
}