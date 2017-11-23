using System.Reflection;

namespace SharpChakra.Extensions
{
   public static class JsProxy
   {
      public static JsValue New(object _object, JsNativeFunctionBuilder _builder)
      {
         var proxy = JsValue.CreateObject();
         var type = _object.GetType();
         var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
         foreach (var property in properties)
         {
            var descriptor = JsValue.CreateObject();
            descriptor.SetProperty("configurable", JsValue.False, true);
            if (property.CanRead)
               descriptor.SetProperty("get", _builder.New(() => ConvertToJsValue(property.GetValue(_object))), true);
            //descriptor.SetProperty("value", ConvertToJsValue(property.GetValue(_object)), true);
            proxy.DefineProperty(JsPropertyId.FromString(property.Name), descriptor);
         }
         return proxy;
      }

      private static JsValue ConvertToJsValue(object _val)
      {
         switch (_val)
         {
            case int i32:
               return JsValue.FromInt32(i32);
            case string s:
               return JsValue.FromString(s);
            case uint ui32:
               return JsValue.FromInt32((int)ui32);
            case float f32:
               return JsValue.FromDouble(f32);
            case double f64:
               return JsValue.FromDouble(f64);
            case bool b:
               return JsValue.FromBoolean(b);
            default:
               return JsValue.Undefined;
         }
      }
   }
}