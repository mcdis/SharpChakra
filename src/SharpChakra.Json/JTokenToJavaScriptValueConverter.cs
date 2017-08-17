using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
   /// <summary>
   /// https://www.microsoft.com/developerblog/2016/06/02/hybrid-apps-using-c-and-javascript-with-chakracore/
   /// </summary>
   public sealed class JTokenToJavaScriptValueConverter
   {
      private static readonly JTokenToJavaScriptValueConverter SInstance =
          new JTokenToJavaScriptValueConverter();

      private JTokenToJavaScriptValueConverter() { }

      public static JavaScriptValue Convert(JToken _token)
      {
         return SInstance.Visit(_token);
      }

      private JavaScriptValue Visit(JToken _token)
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

      private JavaScriptValue VisitArray(JArray _token)
      {
         var n = _token.Count;
         var array = AddRef(JavaScriptValue.CreateArray((uint)n));
         for (var i = 0; i < n; ++i)
         {
            var value = Visit(_token[i]);
            array.SetIndexedProperty(JavaScriptValue.FromInt32(i), value);
            value.Release();
         }

         return array;
      }

      private JavaScriptValue VisitBoolean(JValue _token)
      {
         return _token.Value<bool>()
             ? JavaScriptValue.True
             : JavaScriptValue.False;
      }

      private JavaScriptValue VisitFloat(JValue _token)
      {
         return AddRef(JavaScriptValue.FromDouble(_token.Value<double>()));
      }

      private JavaScriptValue VisitInteger(JValue _token)
      {
         return AddRef(JavaScriptValue.FromDouble(_token.Value<double>()));
      }

      private JavaScriptValue VisitNull(JToken _token)
      {
         return JavaScriptValue.Null;
      }

      private JavaScriptValue VisitObject(JObject _token)
      {
         var jsonObject = AddRef(JavaScriptValue.CreateObject());
         foreach (var entry in _token)
         {
            var value = Visit(entry.Value);
            var propertyId = JavaScriptPropertyId.FromString(entry.Key);
            jsonObject.SetProperty(propertyId, value, true);
            value.Release();
         }

         return jsonObject;
      }

      private JavaScriptValue VisitString(JValue _token)
      {
         return AddRef(JavaScriptValue.FromString(_token.Value<string>()));
      }

      private JavaScriptValue VisitUndefined(JToken _token)
      {
         return JavaScriptValue.Undefined;
      }

      private JavaScriptValue AddRef(JavaScriptValue _value)
      {
         _value.AddRef();
         return _value;
      }
   }
}
