using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using SharpChakra.Json;

namespace SharpChakra.Extensions
{
   public static class JsProxy
   {
      public static JsValue New(object _object, JsNativeFunctionBuilder _builder)
      {
         var proxy = JsValue.CreateObject();
         var type = _object.GetType();
         var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
         var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
         foreach (var method in methods)
         {
            if (method.IsSpecialName)
               continue;
            var name = method.Name;
            var args = method.GetParameters();
            var noRet = method.ReturnType == typeof(void);
            proxy.SetProperty(name,
               _builder.New(_args =>
               {
                  if(args.Length > _args.ArgumentCount-1)
                     return JsValue.Invalid;
                  var pars = new object[args.Length];
                  for (var i = 0; i < pars.Length; i++)
                     pars[i] = ConvertToObject(args[i].ParameterType, _args.Arguments[i+1]);
                  if (noRet)
                  {
                     method.Invoke(_object,
                        BindingFlags.Instance | BindingFlags.Public,
                        null,
                        pars,
                        CultureInfo.InvariantCulture);
                     return JsValue.Undefined;
                  }
                  var res = method.Invoke(_object,
                     BindingFlags.Instance | BindingFlags.Public,
                     null,
                     pars,
                     CultureInfo.InvariantCulture);
                  return ConvertToJsValue(res);
               }),true);
         }
         foreach (var property in properties)
         {
            var descriptor = JsValue.CreateObject();
            descriptor.SetProperty("configurable", JsValue.False, true);
            if(property.CanRead)
               descriptor.SetProperty("get", _builder.New(() => ConvertToJsValue(property.GetValue(_object))), true);
            //descriptor.SetProperty("value", ConvertToJsValue(property.GetValue(_object)), true);
            proxy.DefineProperty(JsPropertyId.FromString(property.Name), descriptor);
         }
         return proxy;
      }
      private static object ConvertToObject(Type _parameterType, JsValue _val)
      {
         if (_parameterType == typeof(short))
            return (short) _val.ToInt32();
         if (_parameterType == typeof(ushort))
            return (ushort)_val.ToInt32();
         if (_parameterType == typeof(int))
            return _val.ToInt32();
         if (_parameterType == typeof(uint))
            return (uint)_val.ToInt32();
         if (_parameterType == typeof(string))
            return _val.ToString();
         if (_parameterType == typeof(char))
            return _val.ToString()[0];
         if (_parameterType == typeof(float))
            return (float)_val.ToDouble();
         if (_parameterType == typeof(double))
            return (float)_val.ToDouble();
         if (_parameterType == typeof(bool))
            return _val.ToBoolean();
         return _val.ToJToken().ToObject(_parameterType);
      }
      private static JsValue ConvertToJsValue(object _val)
      {
         switch (_val)
         {
            case short i16:
               return JsValue.FromInt32(i16);
            case ushort ui16:
               return JsValue.FromInt32(ui16);
            case int i32:
               return JsValue.FromInt32(i32);
            case uint ui32:
               return JsValue.FromInt32((int)ui32);
            case string s:
               return JsValue.FromString(s);
            case char ch:
               return JsValue.FromString(ch.ToString());
            case float f32:
               return JsValue.FromDouble(f32);
            case double f64:
               return JsValue.FromDouble(f64);
            case bool b:
               return JsValue.FromBoolean(b);
            default:
               return JToken.FromObject(_val).ToJsValue();
         }
      }
   }
}