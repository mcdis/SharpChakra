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
      public static JsValue New(object _object, JsNativeFunctionBuilder builder)
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
            var parameters = method.GetParameters();
            var noRet = method.ReturnType == typeof(void);
            proxy.SetProperty(name,
               builder.New(args =>
               {
                  if(parameters.Length > args.ArgumentCount-1)
                     return JsValue.Invalid;
                  var pars = new object[parameters.Length];
                  for (var i = 0; i < pars.Length; i++)
                     pars[i] = ConvertToObject(parameters[i].ParameterType, args.Arguments[i+1]);
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
               descriptor.SetProperty("get", builder.New(() => ConvertToJsValue(property.GetValue(_object))), true);
            //descriptor.SetProperty("value", ConvertToJsValue(property.GetValue(_object)), true);
            proxy.DefineProperty(JsPropertyId.FromString(property.Name), descriptor);
         }
         return proxy;
      }
      private static object ConvertToObject(Type parameterType, JsValue val)
      {
         if (parameterType == typeof(short))
            return (short) val.ToInt32();
         if (parameterType == typeof(ushort))
            return (ushort)val.ToInt32();
         if (parameterType == typeof(int))
            return val.ToInt32();
         if (parameterType == typeof(uint))
            return (uint)val.ToInt32();
         if (parameterType == typeof(string))
            return val.ToString();
         if (parameterType == typeof(char))
            return val.ToString()[0];
         if (parameterType == typeof(float))
            return (float)val.ToDouble();
         if (parameterType == typeof(double))
            return (float)val.ToDouble();
         if (parameterType == typeof(bool))
            return val.ToBoolean();
         return val.ToJToken().ToObject(parameterType);
      }
      private static JsValue ConvertToJsValue(object val)
      {
         switch (val)
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
               return JToken.FromObject(val).ToJsValue();
         }
      }
   }
}