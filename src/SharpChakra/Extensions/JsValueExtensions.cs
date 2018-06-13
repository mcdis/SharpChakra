using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpChakra.Extensions
{
    public static class JsValueExtensions
    {
        public static IEnumerable<string> EnumeratePropertyNames(this JsValue val)
        {
            var lenId = JsPropertyId.FromString("length");
            var names = val.GetOwnPropertyNames();
            var len = names.GetProperty(lenId).ToInt32();
            for (var i = 0; i < len; i++)
            {
                yield return names.GetIndexedProperty(JsValue.FromInt(i)).ToString();
            }
        }

        public static IEnumerable<JsValue> EnumerateArrayValues(this JsValue val)
        {
            if (val.ValueType != JsValueType.Array)
                throw new InvalidOperationException("Can't enumerate non array value");
            var lenId = JsPropertyId.FromString("length");
            var len = val.GetProperty(lenId).ToInt32();
            for (var i = 0; i < len; i++)
            {
                yield return val.GetIndexedProperty(JsValue.FromInt(i));
            }
        }

        public static IEnumerable<KeyValuePair<string, JsValue>> EnumerateProperties(this JsValue _this)
            => from nameVal in EnumerateArrayValues(_this.GetOwnPropertyNames())
                let name = nameVal.ToString()
                let propId = JsPropertyId.FromString(name)
                let val = _this.GetProperty(propId)
                select new KeyValuePair<string, JsValue>(name, val);
    }
}