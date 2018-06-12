using System;
using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
    /// <summary>
    /// https://www.microsoft.com/developerblog/2016/06/02/hybrid-apps-using-c-and-javascript-with-chakracore/
    /// </summary>
    public sealed class JsValueToJTokenConverter
    {
        private static readonly JToken STrue = new JValue(true);
        private static readonly JToken SFalse = new JValue(false);
        private static readonly JToken SNull = JValue.CreateNull();
        private static readonly JToken SUndefined = JValue.CreateUndefined();

        private static readonly JsValueToJTokenConverter SInstance =
            new JsValueToJTokenConverter();

        private JsValueToJTokenConverter()
        {
        }

        public static JToken Convert(JsValue value) => SInstance.Visit(value);

        private JToken Visit(JsValue value)
        {
            switch (value.ValueType)
            {
                case JsValueType.Array:
                    return VisitArray(value);
                case JsValueType.Boolean:
                    return VisitBoolean(value);
                case JsValueType.Null:
                    return VisitNull(value);
                case JsValueType.Number:
                    return VisitNumber(value);
                case JsValueType.Object:
                    return VisitObject(value);
                case JsValueType.String:
                    return VisitString(value);
                case JsValueType.Undefined:
                    return VisitUndefined(value);
                case JsValueType.Function:
                case JsValueType.Error:
                default:
                    throw new NotSupportedException();
            }
        }

        private JToken VisitArray(JsValue value)
        {
            var array = new JArray();
            var propertyId = JsPropertyId.FromString("length");
            var length = value.GetProperty(propertyId).ToInt32();
            var context = value.Context;

            for (var i = 0; i < length; ++i)
            {
                var index = context.CreateInt32(i);
                var element = value.GetIndexedProperty(index);
                array.Add(Visit(element));
            }

            return array;
        }

        private JToken VisitBoolean(JsValue value) => value.ToBoolean() ? STrue : SFalse;

        private JToken VisitNull(JsValue value) => SNull;

        private JToken VisitNumber(JsValue value)
        {
            var number = value.ToDouble();

            return number % 1 == 0
                ? new JValue((long) number)
                : new JValue(number);
        }

        private JToken VisitObject(JsValue value)
        {
            var jsonObject = new JObject();
            var properties = Visit(value.GetOwnPropertyNames()).ToObject<string[]>();
            foreach (var property in properties)
            {
                var propertyId = JsPropertyId.FromString(property);
                var propertyValue = value.GetProperty(propertyId);
                jsonObject.Add(property, Visit(propertyValue));
            }

            return jsonObject;
        }

        private JToken VisitString(JsValue value) => JValue.CreateString(value.ToString());

        private JToken VisitUndefined(JsValue value) => SUndefined;
    }
}