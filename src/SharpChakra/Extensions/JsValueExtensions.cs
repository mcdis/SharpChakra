using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpChakra.Extensions
{
    public static class JsValueExtensions
    {
        public static JsValue SetProperty(this JsValue _this, string propertyId, JsValue value, bool useStrictRules = true)
        {
            _this.SetProperty(JsPropertyId.FromString(propertyId), value, useStrictRules);
            return _this;
        }

        public static JsValue GetProperty(this JsValue _this, string propertyId)
            => _this.GetProperty(JsPropertyId.FromString(propertyId));

        public static bool HasProperty(this JsValue _this, string propertyId)
            => _this.HasProperty(JsPropertyId.FromString(propertyId));

        public static IEnumerable<string> EnumeratePropertyNames(this JsValue _this)
        {
            var lenId = JsPropertyId.FromString("length");
            var names = _this.GetOwnPropertyNames();
            var len = names.GetProperty(lenId).ToInt32();
            for (var i = 0; i < len; i++)
            {
                yield return names.GetIndexedProperty(_this.Context.CreateInt32(i)).ToString();
            }
        }

        public static IEnumerable<JsValue> EnumerateArrayValues(this JsValue _this)
        {
            if (_this.ValueType != JsValueType.Array)
                throw new InvalidOperationException("Can't enumerate non array value");
            var lenId = JsPropertyId.FromString("length");
            var len = _this.GetProperty(lenId).ToInt32();
            for (var i = 0; i < len; i++)
            {
                yield return _this.GetIndexedProperty(_this.Context.CreateInt32(i));
            }
        }

        public static IEnumerable<KeyValuePair<string, JsValue>> EnumerateProperties(this JsValue _this)
            => from nameVal in _this.GetOwnPropertyNames().EnumerateArrayValues()
                let name = nameVal.ToString()
                let propId = JsPropertyId.FromString(name)
                let val = _this.GetProperty(propId)
                select new KeyValuePair<string, JsValue>(name, val);
    }
}