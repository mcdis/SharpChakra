using System;
using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
    /// <summary>
    /// https://www.microsoft.com/developerblog/2016/06/02/hybrid-apps-using-c-and-javascript-with-chakracore/
    /// </summary>
    public sealed class JTokenToJsValueConverter
    {
        private readonly JsContext _context;

        internal JTokenToJsValueConverter(JsContext context)
        {
            _context = context;
        }

        public JsValue Convert(JToken _token) => Visit(_token);

        private JsValue Visit(JToken token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            switch (token.Type)
            {
                case JTokenType.Array:
                    return VisitArray((JArray) token);
                case JTokenType.Boolean:
                    return VisitBoolean((JValue) token);
                case JTokenType.Float:
                    return VisitFloat((JValue) token);
                case JTokenType.Integer:
                    return VisitInteger((JValue) token);
                case JTokenType.Null:
                    return VisitNull(token);
                case JTokenType.Object:
                    return VisitObject((JObject) token);
                case JTokenType.String:
                    return VisitString((JValue) token);
                case JTokenType.Undefined:
                    return VisitUndefined(token);
                default:
                    throw new NotSupportedException();
            }
        }

        private JsValue VisitArray(JArray token)
        {
            var n = token.Count;
            var array = AddRef(_context.CreateArray((uint) n));
            for (var i = 0; i < n; ++i)
            {
                var value = Visit(token[i]);
                array.SetIndexedProperty(_context.CreateInt32(i), value);
                value.Release();
            }

            return array;
        }

        private JsValue VisitBoolean(JValue token)
            => token.Value<bool>()
                ? _context.CreateTrue()
                : _context.CreateFalse();

        private JsValue VisitFloat(JValue token) => AddRef(_context.CreateDouble(token.Value<double>()));

        private JsValue VisitInteger(JValue token) => AddRef(_context.CreateDouble(token.Value<double>()));

        private JsValue VisitNull(JToken token) => _context.CreateNull();

        private JsValue VisitObject(JObject token)
        {
            var jsonObject = AddRef(_context.CreateObject());
            foreach (var entry in token)
            {
                var value = Visit(entry.Value);
                var propertyId = JsPropertyId.FromString(entry.Key);
                jsonObject.SetProperty(propertyId, value, true);
                value.Release();
            }

            return jsonObject;
        }

        private JsValue VisitString(JValue token) => AddRef(_context.CreateString(token.Value<string>()));

        private JsValue VisitUndefined(JToken token) => _context.CreateUndefined();

        private JsValue AddRef(JsValue value)
        {
            value.AddRef();
            return value;
        }
    }
}