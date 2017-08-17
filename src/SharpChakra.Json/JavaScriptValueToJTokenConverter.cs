using System;
using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
   /// <summary>
   /// https://www.microsoft.com/developerblog/2016/06/02/hybrid-apps-using-c-and-javascript-with-chakracore/
   /// </summary>
   public sealed class JavaScriptValueToJTokenConverter
   {
      private static readonly JToken STrue = new JValue(true);
      private static readonly JToken SFalse = new JValue(false);
      private static readonly JToken SNull = JValue.CreateNull();
      private static readonly JToken SUndefined = JValue.CreateUndefined();

      private static readonly JavaScriptValueToJTokenConverter SInstance =
          new JavaScriptValueToJTokenConverter();

      private JavaScriptValueToJTokenConverter() { }

      public static JToken Convert(JavaScriptValue _value) => SInstance.Visit(_value);
      private JToken Visit(JavaScriptValue _value)
      {
         switch (_value.ValueType)
         {
            case JavaScriptValueType.Array:
               return VisitArray(_value);
            case JavaScriptValueType.Boolean:
               return VisitBoolean(_value);
            case JavaScriptValueType.Null:
               return VisitNull(_value);
            case JavaScriptValueType.Number:
               return VisitNumber(_value);
            case JavaScriptValueType.Object:
               return VisitObject(_value);
            case JavaScriptValueType.String:
               return VisitString(_value);
            case JavaScriptValueType.Undefined:
               return VisitUndefined(_value);
            case JavaScriptValueType.Function:
            case JavaScriptValueType.Error:
            default:
               throw new NotSupportedException();
         }
      }

      private JToken VisitArray(JavaScriptValue _value)
      {
         var array = new JArray();
         var propertyId = JavaScriptPropertyId.FromString("length");
         var length = (int)_value.GetProperty(propertyId).ToDouble();
         for (var i = 0; i < length; ++i)
         {
            var index = JavaScriptValue.FromInt32(i);
            var element = _value.GetIndexedProperty(index);
            array.Add(Visit(element));
         }

         return array;
      }

      private JToken VisitBoolean(JavaScriptValue _value)
      {
         return _value.ToBoolean() ? STrue : SFalse;
      }

      private JToken VisitNull(JavaScriptValue _value)
      {
         return SNull;
      }

      private JToken VisitNumber(JavaScriptValue _value)
      {
         var number = _value.ToDouble();

         return number % 1 == 0
             ? new JValue((long)number)
             : new JValue(number);
      }

      private JToken VisitObject(JavaScriptValue _value)
      {
         var jsonObject = new JObject();
         var properties = Visit(_value.GetOwnPropertyNames()).ToObject<string[]>();
         foreach (var property in properties)
         {
            var propertyId = JavaScriptPropertyId.FromString(property);
            var propertyValue = _value.GetProperty(propertyId);
            jsonObject.Add(property, Visit(propertyValue));
         }

         return jsonObject;
      }

      private JToken VisitString(JavaScriptValue _value)
      {
         return JValue.CreateString(_value.ToString());
      }

      private JToken VisitUndefined(JavaScriptValue _value)
      {
         return SUndefined;
      }
   }
}
