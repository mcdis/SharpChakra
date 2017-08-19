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

      private JsValueToJTokenConverter() { }

      public static JToken Convert(JsValue _value) => SInstance.Visit(_value);
      private JToken Visit(JsValue _value)
      {
         switch (_value.ValueType)
         {
            case JsValueType.Array:
               return VisitArray(_value);
            case JsValueType.Boolean:
               return VisitBoolean(_value);
            case JsValueType.Null:
               return VisitNull(_value);
            case JsValueType.Number:
               return VisitNumber(_value);
            case JsValueType.Object:
               return VisitObject(_value);
            case JsValueType.String:
               return VisitString(_value);
            case JsValueType.Undefined:
               return VisitUndefined(_value);
            case JsValueType.Function:
            case JsValueType.Error:
            default:
               throw new NotSupportedException();
         }
      }

      private JToken VisitArray(JsValue _value)
      {
         var array = new JArray();
         var propertyId = JsPropertyId.FromString("length");
         var length = (int)_value.GetProperty(propertyId).ToDouble();
         for (var i = 0; i < length; ++i)
         {
            var index = JsValue.FromInt32(i);
            var element = _value.GetIndexedProperty(index);
            array.Add(Visit(element));
         }

         return array;
      }

      private JToken VisitBoolean(JsValue _value) => _value.ToBoolean() ? STrue : SFalse;
      private JToken VisitNull(JsValue _value) => SNull;
      private JToken VisitNumber(JsValue _value)
      {
         var number = _value.ToDouble();

         return number % 1 == 0
             ? new JValue((long)number)
             : new JValue(number);
      }

      private JToken VisitObject(JsValue _value)
      {
         var jsonObject = new JObject();
         var properties = Visit(_value.GetOwnPropertyNames()).ToObject<string[]>();
         foreach (var property in properties)
         {
            var propertyId = JsPropertyId.FromString(property);
            var propertyValue = _value.GetProperty(propertyId);
            jsonObject.Add(property, Visit(propertyValue));
         }

         return jsonObject;
      }

      private JToken VisitString(JsValue _value) => JValue.CreateString(_value.ToString());
      private JToken VisitUndefined(JsValue _value) => SUndefined;
   }
}
