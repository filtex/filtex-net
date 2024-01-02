using FiltexNet.Constants;
using FiltexNet.Exceptions;
using FiltexNet.Models;
using FiltexNet.Options;
using Xunit;

namespace FiltexNet.Tests.Options
{
    public class FieldOptionTests
    {
        [Fact]
        public void New_ShouldReturnFieldOption()
        {
            // Act
            var opt = FieldOption.New();

            // Assert
            Assert.NotNull(opt);
            Assert.Null(opt._name);
            Assert.Null(opt._label);
            Assert.Null(opt._lookup);
            Assert.Null(opt._type);
            Assert.False(opt._isArray);
            Assert.False(opt._isNullable);
        }

        [Fact]
        public void String_ShouldSetFieldTypeAsStringAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.String();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeString, result._type);
        }

        [Fact]
        public void Number_ShouldSetFieldTypeAsNumberAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.Number();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeNumber, result._type);
        }

        [Fact]
        public void Boolean_ShouldSetFieldTypeAsBooleanAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.Boolean();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeBoolean, result._type);
        }

        [Fact]
        public void Date_ShouldSetFieldTypeAsDateAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.Date();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeDate, result._type);
        }

        [Fact]
        public void Time_ShouldSetFieldTypeAsTimeAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.Time();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeTime, result._type);
        }

        [Fact]
        public void DateTime_ShouldSetFieldTypeAsDateTimeAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.DateTime();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeDateTime, result._type);
        }

        [Fact]
        public void Array_ShouldSetIsArrayAsTrueAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.Array();

            // Assert
            Assert.NotNull(result);
            Assert.True(result._isArray);
        }

        [Fact]
        public void Nullable_ShouldSetIsNullableAsTrueAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.Nullable();

            // Assert
            Assert.NotNull(result);
            Assert.True(result._isNullable);
        }

        [Fact]
        public void Name_ShouldSetNameAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.Name("Some Name");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result._name);
            Assert.Equal("Some Name", result._name);
        }

        [Fact]
        public void Label_ShouldSetLabelAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.Label("Some Label");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result._label);
            Assert.Equal("Some Label", result._label);
        }

        [Fact]
        public void Lookup_ShouldSetLookupAndReturnItself()
        {
            // Arrange
            var opt = FieldOption.New();

            // Act
            var result = opt.Lookup("some_key");

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result._lookup);
            Assert.Equal("some_key", result._lookup);
        }

        [Fact]
        public void Build_ShouldReturnError_WhenFieldTypeIsNotDefined()
        {
            // Arrange
            var opt = FieldOption.New().Name("Some Name").Label("Some Label");

            Assert.Throws<FieldException>(() =>
            {
                // Act
                var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

                // Assert
                Assert.Null(result);
            });
        }

        [Fact]
        public void Build_ShouldReturnError_WhenNameIsNotDefined()
        {
            // Arrange
            var opt = FieldOption.New().String().Label("Some Label");

            Assert.Throws<FieldException>(() =>
            {
                // Act
                var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

                // Assert
                Assert.Null(result);
            });
        }

        [Fact]
        public void Build_ShouldReturnError_WhenLabelIsNotDefined()
        {
            // Arrange
            var opt = FieldOption.New().String().Name("Some Name");

            Assert.Throws<FieldException>(() =>
            {
                // Act
                var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

                // Assert
                Assert.Null(result);
            });
        }

        [Fact]
        public void Build_ShouldSetFieldTypeAsStringArray_WhenTypeIsStringAndArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().String().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeStringArray.Name, result.Type);
            Assert.Equal("Some Name", result.Name);
            Assert.Equal("Some Label", result.Label);
        }

        [Fact]
        public void Build_ShouldSetFieldTypeAsNumberArray_WhenTypeIsNumberAndArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().Number().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeNumberArray.Name, result.Type);
            Assert.Equal("Some Name", result.Name);
            Assert.Equal("Some Label", result.Label);
        }

        [Fact]
        public void Build_ShouldSetFieldTypeAsBooleanArray_WhenTypeIsBooleanAndArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().Boolean().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeBooleanArray.Name, result.Type);
            Assert.Equal("Some Name", result.Name);
            Assert.Equal("Some Label", result.Label);
        }

        [Fact]
        public void Build_ShouldSetFieldTypeAsDateArray_WhenTypeIsDateAndArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().Date().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeDateArray.Name, result.Type);
            Assert.Equal("Some Name", result.Name);
            Assert.Equal("Some Label", result.Label);
        }

        [Fact]
        public void Build_ShouldSetFieldTypeAsTimeArray_WhenTypeIsTimeAndArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().Time().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeTimeArray.Name, result.Type);
            Assert.Equal("Some Name", result.Name);
            Assert.Equal("Some Label", result.Label);
        }

        [Fact]
        public void Build_ShouldSetFieldTypeAsDateTimeArray_WhenTypeIsDateTimeAndArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().DateTime().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeDateTimeArray.Name, result.Type);
            Assert.Equal("Some Name", result.Name);
            Assert.Equal("Some Label", result.Label);
        }

        [Fact]
        public void Build_ShouldSetValues_WhenValuesAreDefined()
        {
            // Arrange
            var opt = FieldOption.New().DateTime().Array().Name("Some Name").Label("Some Label").Lookup("some_key");

            // Act
            var result = opt.Build(new[]
            {
                ("some_key", new[]
                {
                    new Lookup("Enabled", true),
                    new Lookup("Disabled", true)
                })
            });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(FieldType.FieldTypeDateTimeArray.Name, result.Type);
            Assert.Equal("Some Name", result.Name);
            Assert.Equal("Some Label", result.Label);
            Assert.Equal(2, result.Values.Length);
        }

        [Fact]
        public void Build_ShouldAddDefaultOperators_WhenDefinitionsAreValid()
        {
            // Arrange
            var opt = FieldOption.New().String().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorEqual.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorNotEqual.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorIn.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorNotIn.Name);
        }

        [Fact]
        public void Build_ShouldAddBlankOperators_WhenArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().String().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorBlank.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorNotBlank.Name);
        }

        [Fact]
        public void Build_ShouldAddBlankOperators_WhenNullableIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().String().Nullable().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorBlank.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorNotBlank.Name);
        }

        [Fact]
        public void Build_ShouldAddContainOperators_WhenArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().String().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorContain.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorNotContain.Name);
        }

        [Fact]
        public void Build_ShouldAddContainOperators_WhenTypeIsString()
        {
            // Arrange
            var opt = FieldOption.New().String().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorContain.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorNotContain.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorStartWith.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorNotStartWith.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorEndWith.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorNotEndWith.Name);
        }

        [Fact]
        public void Build_ShouldAddCompareOperators_WhenTypeIsNumber()
        {
            // Arrange
            var opt = FieldOption.New().Number().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThanOrEqual.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThanOrEqual.Name);
        }

        [Fact]
        public void Build_ShouldAddCompareOperators_WhenTypeIsNumberAndArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().Number().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThanOrEqual.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThanOrEqual.Name);
        }

        [Fact]
        public void Build_ShouldAddCompareOperators_WhenTypeIsDate()
        {
            // Arrange
            var opt = FieldOption.New().Date().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThanOrEqual.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThanOrEqual.Name);
        }

        [Fact]
        public void Build_ShouldAddCompareOperators_WhenTypeIsDateAndArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().Date().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThanOrEqual.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThanOrEqual.Name);
        }

        [Fact]
        public void Build_ShouldAddCompareOperators_WhenTypeIsTime()
        {
            // Arrange
            var opt = FieldOption.New().Time().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThanOrEqual.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThanOrEqual.Name);
        }

        [Fact]
        public void Build_ShouldAddCompareOperators_WhenTypeIsTimeAndArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().Time().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThanOrEqual.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThanOrEqual.Name);
        }

        [Fact]
        public void Build_ShouldAddCompareOperators_WhenTypeIsDateTime()
        {
            // Arrange
            var opt = FieldOption.New().DateTime().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThanOrEqual.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThanOrEqual.Name);
        }

        [Fact]
        public void Build_ShouldAddCompareOperators_WhenTypeIsDateTimeAndArrayIsDefined()
        {
            // Arrange
            var opt = FieldOption.New().DateTime().Array().Name("Some Name").Label("Some Label");

            // Act
            var result = opt.Build(new[] { ((string, Lookup[]))(null, null) });

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorGreaterThanOrEqual.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThan.Name);
            Assert.Contains(result.Operators, x => x == Operator.OperatorLessThanOrEqual.Name);
        }
    }
}