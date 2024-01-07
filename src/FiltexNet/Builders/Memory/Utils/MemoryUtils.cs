using System.Collections.Generic;
using FiltexNet.Constants;
using FiltexNet.Utils;

namespace FiltexNet.Builders.Memory.Utils
{
    public static class MemoryUtils
    {
        public static bool CheckEquality(FieldType fieldType, object fieldValue, object value)
        {
            if (fieldType.Name == FieldType.FieldTypeString.Name)
            {
                return CastUtils.String(fieldValue).ToLowerInvariant() == CastUtils.String(value).ToLowerInvariant();
            }

            if (fieldType.Name == FieldType.FieldTypeStringArray.Name)
            {
                return CastUtils.String(fieldValue).ToLowerInvariant() == CastUtils.String(value).ToLowerInvariant();
            }

            if (fieldType.Name == FieldType.FieldTypeNumber.Name)
            {
                return CastUtils.Number(fieldValue) == CastUtils.Number(value);
            }

            if (fieldType.Name == FieldType.FieldTypeNumberArray.Name)
            {
                return CastUtils.Number(fieldValue) == CastUtils.Number(value);
            }

            if (fieldType.Name == FieldType.FieldTypeBoolean.Name)
            {
                return CastUtils.Boolean(fieldValue) == CastUtils.Boolean(value);
            }

            if (fieldType.Name == FieldType.FieldTypeBooleanArray.Name)
            {
                return CastUtils.Boolean(fieldValue) == CastUtils.Boolean(value);
            }

            if (fieldType.Name == FieldType.FieldTypeDate.Name)
            {
                return CastUtils.Date(fieldValue) == CastUtils.Date(value);
            }

            if (fieldType.Name == FieldType.FieldTypeDateArray.Name)
            {
                return CastUtils.Date(fieldValue) == CastUtils.Date(value);
            }

            if (fieldType.Name == FieldType.FieldTypeTime.Name)
            {
                return CastUtils.Time(fieldValue) == CastUtils.Time(value);
            }

            if (fieldType.Name == FieldType.FieldTypeTimeArray.Name)
            {
                return CastUtils.Time(fieldValue) == CastUtils.Time(value);
            }

            if (fieldType.Name == FieldType.FieldTypeDateTime.Name)
            {
                return CastUtils.DateTime(fieldValue) == CastUtils.DateTime(value);
            }

            if (fieldType.Name == FieldType.FieldTypeDateTimeArray.Name)
            {
                return CastUtils.DateTime(fieldValue) == CastUtils.DateTime(value);
            }

            return false;
        }
        
        public static IDictionary<string, object> ObjectToDictionary<T>(T obj)
        {
            var result = new Dictionary<string, object>();

            foreach (var field in typeof(T).GetFields())
            {
                var fieldName = field.Name;
                var fieldValue = field.GetValue(obj);
                
                result[fieldName] = fieldValue;
            }
            
            foreach (var property in typeof(T).GetProperties())
            {
                var fieldName = property.Name;
                var fieldValue = property.GetValue(obj);
                
                result[fieldName] = fieldValue;
            }

            return result;
        }
    }
}