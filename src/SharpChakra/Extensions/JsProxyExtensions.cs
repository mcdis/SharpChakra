using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpChakra.Extensions
{
    public static class JsProxyExtensions
    {
        public static JsValue CreateFunctionFromMethod<T>(this JsContext context, string methodName, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Static, Type[] types = null)
        {
            return CreateFunctionFromMethod(context, typeof(T), methodName, bindingAttr, types);
        }

        public static JsValue CreateFunctionFromMethod(this JsContext context, Type type, string methodName, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Static, Type[] types = null)
        {
            var method = types == null
                ? type.GetMethod(methodName, bindingAttr)
                : type.GetMethod(methodName, bindingAttr, null, types, null);

            if (method == null)
            {
                throw new MissingMethodException(type.Name, methodName);
            }

            return CreateFunction(context, method, null);
        }

        public static JsValue CreateFunctionFromMethod<T>(this JsContext context, T instance, string methodName, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance, Type[] types = null)
        {
            var type = typeof(T);
            var method = types == null 
                ? typeof(T).GetMethod(methodName, bindingAttr)
                : typeof(T).GetMethod(methodName, bindingAttr, null, types, null);

            if (method == null)
            {
                throw new MissingMethodException(type.Name, methodName);
            }

            return CreateFunction(context, method, instance);
        }

        public static JsValue CreateFunction(this JsContext context, MethodInfo method, object instance)
        {
            var parameters = method.GetParameters();
            var noRet = method.ReturnType == typeof(void);

            return context.CreateFunction(args =>
            {
                if (parameters.Length > args.ArgumentCount - 1)
                {
                    return JsValue.Invalid;
                }

                var pars = new object[parameters.Length];
                for (var i = 0; i < pars.Length; i++)
                {
                    pars[i] = args.Arguments[i + 1].ToObject(parameters[i].ParameterType);
                }

                if (noRet)
                {
                    method.Invoke(instance, BindingFlags.Instance | BindingFlags.Public, null, pars, CultureInfo.InvariantCulture);
                    return context.CreateUndefined();
                }

                var res = method.Invoke(instance, BindingFlags.Instance | BindingFlags.Public, null, pars, CultureInfo.InvariantCulture);
                return context.CreateValue(res);
            });
        }

        public static JsValue CreateDescriptor(this JsContext context, PropertyInfo property, object instance)
        {
            var descriptor = context.CreateObject();

            descriptor.SetProperty("configurable", context.CreateTrue());
            descriptor.SetProperty("enumerable", context.CreateTrue());

            if (property.CanRead)
            {
                descriptor.SetProperty("get", context.CreateFunction(() => context.CreateValue(property.GetValue(instance))));
            }

            if (property.CanWrite)
            {
                descriptor.SetProperty("set", context.CreateFunction(e =>
                {
                    if (e.ArgumentCount < 2)
                    {
                        return JsValue.Invalid;
                    }

                    var value = e.Arguments[1].ToObject(property.PropertyType);
                    property.SetValue(instance, value);

                    return context.CreateUndefined();
                }));
            }

            return descriptor;
        }

        public static JsValue CreateProxy(this JsContext context, object instance)
        {
            var proxy = context.CreateObject();
            var type = instance.GetType();

            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => !m.IsSpecialName))
            {
                proxy.SetProperty(method.Name, CreateFunction(context, method, instance));
            }

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                proxy.DefineProperty(JsPropertyId.FromString(property.Name), CreateDescriptor(context, property, instance));
            }

            return proxy;
        }

        public static T ToObject<T>(this JsValue val)
        {
            return (T) ToObject(val, typeof(T));
        }

        public static object ToObject(this JsValue val, Type parameterType)
        {
            if (val.ValueType == JsValueType.Undefined || val.ValueType == JsValueType.Null)
                return parameterType.IsValueType ? Activator.CreateInstance(parameterType) : null;
            if (parameterType == typeof(short))
                return (short)val.ToInt32();
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
            throw new NotSupportedException();
        }

        public static JsValue CreateValue(this JsContext context, object val)
        {
            switch (val)
            {
                case null:
                    return context.CreateNull();
                case short i16:
                    return context.CreateInt32(i16);
                case ushort ui16:
                    return context.CreateInt32(ui16);
                case int i32:
                    return context.CreateInt32(i32);
                case uint ui32:
                    return context.CreateInt32((int)ui32);
                case string s:
                    return context.CreateString(s);
                case char ch:
                    return context.CreateString(ch.ToString());
                case float f32:
                    return context.CreateDouble(f32);
                case double f64:
                    return context.CreateDouble(f64);
                case bool b:
                    return context.CreateBoolean(b);
                case Action a:
                    return context.CreateFunction(a);
                case Func<JsValue> func:
                    return context.CreateFunction(func);
                default:
                    return context.CreateUndefined();
            }
        }
    }
}
