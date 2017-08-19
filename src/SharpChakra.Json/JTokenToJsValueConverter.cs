using System;
using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
   /// <summary>
   /// https://www.microsoft.com/developerblog/2016/06/02/hybrid-apps-using-c-and-javascript-with-chakracore/
   /// </summary>
   public sealed class JTokenToJsValueConverter
   {
      private static readonly JTokenToJsValueConverter SInstance =
          new JTokenToJsValueConverter();

      private JTokenToJsValueConverter() { }

      public static JsValue Convert(JToken _token) => SInstance.Visit(_token);
      private JsValue Visit(JToken _token)
      {
         if (_token == null)
            throw new ArgumentNullException(nameof(_token));

         switch (_token.Type)
         {
            case JTokenType.Array:
               return VisitArray((JArray)_token);
            case JTokenType.Boolean:
               return VisitBoolean((JValue)_token);
            case JTokenType.Float:
               return VisitFloat((JValue)_token);
            case JTokenType.Integer:
               return VisitInteger((JValue)_token);
            case JTokenType.Null:
               return VisitNull(_token);
            case JTokenType.Object:
               return VisitObject((JObject)_token);
            case JTokenType.String:
               return VisitString((JValue)_token);
            case JTokenType.Undefined:
               return VisitUndefined(_token);
            default:
               throw new NotSupportedException();
         }
      }

      private JsValue VisitArray(JArray _token)
      {
         var n = _token.Count;
         var array = AddRef(JsValue.CreateArray((uint)n));
         for (var i = 0; i < n; ++i)
         {
            var value = Visit(_token[i]);
            array.SetIndexedProperty(JsValue.FromInt32(i), value);
            value.Release();
         }

         return array;
      }

      private JsValue VisitBoolean(JValue _token)
         => _token.Value<bool>()
         ? JsValue.True
         : JsValue.False;
      private JsValue VisitFloat(JValue _token) => AddRef(JsValue.FromDouble(_token.Value<double>()));
      private JsValue VisitInteger(JValue _token) => AddRef(JsValue.FromDouble(_token.Value<double>()));
      private JsValue VisitNull(JToken _token) => JsValue.Null;
      private JsValue VisitObject(JObject _token)
      {
         var jsonObject = AddRef(JsValue.CreateObject());
         foreach (var entry in _token)
         {
            var value = Visit(entry.Value);
            var propertyId = JsPropertyId.FromString(entry.Key);
            jsonObject.SetProperty(propertyId, value, true);
            value.Release();
         }

         return jsonObject;
      }

      private JsValue VisitString(JValue _token) => AddRef(JsValue.FromString(_token.Value<string>()));
      private JsValue VisitUndefined(JToken _token) => JsValue.Undefined;
      private JsValue AddRef(JsValue _value)
      {
         _value.AddRef();
         return _value;
      }
   }
}
