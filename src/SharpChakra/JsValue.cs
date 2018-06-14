using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SharpChakra
{
    public struct JsValue
    {
        internal readonly IntPtr Reference;

        private JsValue(IntPtr reference)
        {
            Reference = reference;
        }

        public static JsValue Invalid => new JsValue(IntPtr.Zero);

        public JsContext Context
        {
            get
            {
                Native.ThrowIfError(Native.JsGetContextOfObject(this, out var context));
                return context;
            }
        }

        public static JsValue Undefined
        {
            get
            {
                JsContext.Current.ThrowIfInvalid();
                Native.ThrowIfError(Native.JsGetUndefinedValue(out var value));
                return value;
            }
        }

        public static JsValue Null
        {
            get
            {
                JsContext.Current.ThrowIfInvalid();
                Native.ThrowIfError(Native.JsGetNullValue(out var value));
                return value;
            }
        }

        public static JsValue True
        {
            get
            {
                JsContext.Current.ThrowIfInvalid();
                Native.ThrowIfError(Native.JsGetFalseValue(out var value));
                return value;
            }
        }

        public static JsValue False
        {
            get
            {
                JsContext.Current.ThrowIfInvalid();
                Native.ThrowIfError(Native.JsGetFalseValue(out var value));
                return value;
            }
        }

        public static JsValue FromInt(int value)
        {
            JsContext.Current.ThrowIfInvalid();
            Native.ThrowIfError(Native.JsIntToNumber(value, out var reference));
            return reference;
        }

        public static JsValue CreateObject()
        {
            JsContext.Current.ThrowIfInvalid();
            Native.ThrowIfError(Native.JsCreateObject(out var reference));
            return reference;
        }

        public static JsValue CreateArray(uint length)
        {
            JsContext.Current.ThrowIfInvalid();
            Native.ThrowIfError(Native.JsCreateArray(length, out var reference));
            return reference;
        }

        public static JsValue FromString(string value)
        {
            JsContext.Current.ThrowIfInvalid();
            Native.ThrowIfError(Native.JsPointerToString(value, new UIntPtr((uint)value.Length), out var reference));
            return reference;
        }

        public static JsValue FromDouble(double value)
        {
            JsContext.Current.ThrowIfInvalid();
            Native.ThrowIfError(Native.JsDoubleToNumber(value, out var reference));
            return reference;
        }

        public static JsValue CreateFunction(JsNativeFunction function)
        {
            JsContext.Current.ThrowIfInvalid();
            Native.ThrowIfError(Native.JsCreateFunction(function, IntPtr.Zero, out var reference));
            return reference;
        }

        public static JsValue CreateFunction(JsNativeFunction function, IntPtr callbackData)
        {
            JsContext.Current.ThrowIfInvalid();
            Native.ThrowIfError(Native.JsCreateFunction(function, callbackData, out var reference));
            return reference;
        }

        public static JsValue CreateFunction(Action x)
        {
            return CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) =>
            {
                x();

                return Undefined;
            });
        }

        public static JsValue CreateFunction(Func<JsValue> func)
        {
            return CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) => func());
        }

        public static JsValue CreateFunction(Action<JsFunctionArgs> handler)
        {
            return CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) =>
            {
                var args = new JsFunctionArgs
                {
                    Callee = callee,
                    IsConstructCall = isConstructCall,
                    Arguments = arguments,
                    ArgumentCount = argumentCount,
                    CallbackData = callbackData
                };

                handler(args);

                return Undefined;
            });
        }

        public static JsValue CreateFunction(Func<JsFunctionArgs, JsValue> handler)
        {
            return CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) =>
            {
                var args = new JsFunctionArgs
                {
                    Callee = callee,
                    IsConstructCall = isConstructCall,
                    Arguments = arguments,
                    ArgumentCount = argumentCount,
                    CallbackData = callbackData
                };

                return handler(args);
            });
        }

        public bool IsValid => Reference != IntPtr.Zero;

        public JsValueType ValueType
        {
            get
            {
                Native.ThrowIfError(Native.JsGetValueType(this, out var type));
                return type;
            }
        }

        public int StringLength
        {
            get
            {
                Native.ThrowIfError(Native.JsGetStringLength(this, out var length));
                return length;
            }
        }

        public JsValue Prototype
        {
            get
            {
                Native.ThrowIfError(Native.JsGetPrototype(this, out var prototypeReference));
                return prototypeReference;
            }
            set => Native.ThrowIfError(Native.JsSetPrototype(this, value));
        }

        public bool IsExtensionAllowed
        {
            get
            {
                Native.ThrowIfError(Native.JsGetExtensionAllowed(this, out var allowed));
                return allowed;
            }
        }

        public bool HasExternalData
        {
            get
            {
                Native.ThrowIfError(Native.JsHasExternalData(this, out var hasExternalData));
                return hasExternalData;
            }
        }

        public IntPtr ExternalData
        {
            get
            {
                Native.ThrowIfError(Native.JsGetExternalData(this, out var data));
                return data;
            }
            set => Native.ThrowIfError(Native.JsSetExternalData(this, value));
        }
        
        public uint AddRef()
        {
            Native.ThrowIfError(Native.JsAddRef(this, out var count));
            return count;
        }

        public uint Release()
        {
            Native.ThrowIfError(Native.JsRelease(this, out var count));
            return count;
        }

        public bool ToBoolean()
        {
            Native.ThrowIfError(Native.JsBooleanToBool(this, out var value));
            return value;
        }

        public double ToDouble()
        {
            Native.ThrowIfError(Native.JsNumberToDouble(this, out var value));
            return value;
        }

        public int ToInt32()
        {
            Native.ThrowIfError(Native.JsNumberToInt(this, out var value));
            return value;
        }

        public new string ToString()
        {
            Native.ThrowIfError(Native.JsStringToPointer(this, out var buffer, out var length));
            return Marshal.PtrToStringUni(buffer, (int) length);
        }

        public JsValue ConvertToBoolean()
        {
            Native.ThrowIfError(Native.JsConvertValueToBoolean(this, out var booleanReference));
            return booleanReference;
        }

        public JsValue ConvertToNumber()
        {
            Native.ThrowIfError(Native.JsConvertValueToNumber(this, out var numberReference));
            return numberReference;
        }

        public JsValue ConvertToString()
        {
            Native.ThrowIfError(Native.JsConvertValueToString(this, out var stringReference));
            return stringReference;
        }

        public JsValue ConvertToObject()
        {
            Native.ThrowIfError(Native.JsConvertValueToObject(this, out var objectReference));
            return objectReference;
        }

        public void PreventExtension()
        {
            Native.ThrowIfError(Native.JsPreventExtension(this));
        }

        public JsValue GetOwnPropertyDescriptor(JsPropertyId propertyId)
        {
            Native.ThrowIfError(Native.JsGetOwnPropertyDescriptor(this, propertyId, out var descriptorReference));
            return descriptorReference;
        }

        public JsValue GetOwnPropertyNames()
        {
            Native.ThrowIfError(Native.JsGetOwnPropertyNames(this, out var propertyNamesReference));
            return propertyNamesReference;
        }

        public bool HasProperty(JsPropertyId propertyId)
        {
            Native.ThrowIfError(Native.JsHasProperty(this, propertyId, out var hasProperty));
            return hasProperty;
        }

        public JsValue GetProperty(JsPropertyId id)
        {
            Native.ThrowIfError(Native.JsGetProperty(this, id, out var propertyReference));
            return propertyReference;
        }

        public JsValue SetProperty(JsPropertyId id, JsValue value, bool useStrictRules = true)
        {
            Native.ThrowIfError(Native.JsSetProperty(this, id, value, useStrictRules));
            return this;
        }

        public JsValue DeleteProperty(JsPropertyId propertyId, bool useStrictRules)
        {
            Native.ThrowIfError(Native.JsDeleteProperty(this, propertyId, useStrictRules, out var returnReference));
            return returnReference;
        }

        public bool DefineProperty(JsPropertyId propertyId, JsValue propertyDescriptor)
        {
            Native.ThrowIfError(Native.JsDefineProperty(this, propertyId, propertyDescriptor, out var result));
            return result;
        }

        public bool DefineProperty(JsPropertyId propertyId, PropertyInfo property, object instance)
        {
            var context = Context;
            var descriptor = CreateObject();

            descriptor.SetProperty("configurable", True);
            descriptor.SetProperty("enumerable", False);

            if (property.CanRead)
            {
                descriptor.SetProperty("get", CreateFunction(() => FromObject(property.GetValue(instance))));
            }

            if (property.CanWrite)
            {
                descriptor.SetProperty("set", CreateFunction(e =>
                {
                    if (e.ArgumentCount < 2)
                    {
                        return Invalid;
                    }

                    var value = e.Arguments[1].ToObject(property.PropertyType);
                    property.SetValue(instance, value);

                    return Undefined;
                }));
            }

            return DefineProperty(propertyId, descriptor);
        }

        public bool HasIndexedProperty(JsValue index)
        {
            Native.ThrowIfError(Native.JsHasIndexedProperty(this, index, out var hasProperty));
            return hasProperty;
        }

        public JsValue GetIndexedProperty(JsValue index)
        {
            Native.ThrowIfError(Native.JsGetIndexedProperty(this, index, out var propertyReference));
            return propertyReference;
        }

        public void SetIndexedProperty(JsValue index, JsValue value) =>
            Native.ThrowIfError(Native.JsSetIndexedProperty(this, index, value));

        public void DeleteIndexedProperty(JsValue index)
        {
            Native.ThrowIfError(Native.JsDeleteIndexedProperty(this, index));
        }

        public bool Equals(JsValue other)
        {
            Native.ThrowIfError(Native.JsEquals(this, other, out var equals));
            return equals;
        }

        public bool StrictEquals(JsValue other)
        {
            Native.ThrowIfError(Native.JsStrictEquals(this, other, out var equals));
            return equals;
        }

        public JsValue CallFunction(JsValue thisArg, params JsValue[] values)
        {
            var arguments = new[] {thisArg}.Concat(values).ToArray();

            Context.ThrowIfInvalid();
            Native.ThrowIfError(Native.JsCallFunction(this, arguments, (ushort)arguments.Length, out var returnReference));
            return returnReference;
        }

        public JsValue ConstructObject(params JsValue[] arguments)
        {
            if (arguments.Length > ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(arguments));

            Native.ThrowIfError(Native.JsConstructObject(this, arguments, (ushort) arguments.Length,
                out var returnReference));
            return returnReference;
        }

        public static JsValue FromMethod<T>(string methodName, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Static, Type[] types = null)
        {
            return FromMethod(typeof(T), methodName, bindingAttr, types);
        }

        public static JsValue FromMethod(MethodInfo method, object instance)
        {
            var parameters = method.GetParameters();
            var direct = parameters.Length == 1 && parameters[0].ParameterType == typeof(JsFunctionArgs);

            return CreateFunction(args =>
            {
                object[] pars;

                if (direct)
                {
                    pars = new object[] {args};
                }
                else
                {
                    if (parameters.Length > args.ArgumentCount - 1)
                    {
                        return Invalid;
                    }

                    pars = new object[parameters.Length];
                    for (var i = 0; i < pars.Length; i++)
                    {
                        pars[i] = args.Arguments[i + 1].ToObject(parameters[i].ParameterType);
                    }
                }

                var res = method.Invoke(instance, BindingFlags.Instance | BindingFlags.Public, null, pars, CultureInfo.InvariantCulture);

                if (method.ReturnType == typeof(void))
                {
                    return Undefined;
                }

                if (method.ReturnType == typeof(JsValue))
                {
                    return (JsValue) res;
                }

                return FromObject(res);
            });
        }

        public static JsValue FromMethod(Type type, string methodName, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Static, Type[] types = null)
        {
            var method = types == null
                ? type.GetMethod(methodName, bindingAttr)
                : type.GetMethod(methodName, bindingAttr, null, types, null);

            if (method == null)
            {
                throw new MissingMethodException(type.Name, methodName);
            }

            return FromMethod(method, null);
        }

        public static JsValue FromMethod<T>(T instance, string methodName, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance, Type[] types = null)
        {
            var type = typeof(T);
            var method = types == null
                ? typeof(T).GetMethod(methodName, bindingAttr)
                : typeof(T).GetMethod(methodName, bindingAttr, null, types, null);

            if (method == null)
            {
                throw new MissingMethodException(type.Name, methodName);
            }

            return FromMethod(method, instance);
        }

        public static JsValue CreateProxy(object instance)
        {
            var proxy = CreateObject();
            var type = instance.GetType();

            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(m => !m.IsSpecialName))
            {
                var name = char.ToLowerInvariant(method.Name[0]) + method.Name.Substring(1);

                proxy.SetProperty(name, FromMethod(method, instance));
            }

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var name = char.ToLowerInvariant(property.Name[0]) + property.Name.Substring(1);

                proxy.DefineProperty(name, property, instance);
            }

            return proxy;
        }

        public T ToObject<T>()
        {
            return (T)ToObject(typeof(T));
        }

        public object ToObject(Type parameterType)
        {
            if (ValueType == JsValueType.Undefined || ValueType == JsValueType.Null)
                return parameterType.IsValueType ? Activator.CreateInstance(parameterType) : null;
            if (parameterType == typeof(short))
                return (short)ToInt32();
            if (parameterType == typeof(ushort))
                return (ushort)ToInt32();
            if (parameterType == typeof(int))
                return ToInt32();
            if (parameterType == typeof(uint))
                return (uint)ToInt32();
            if (parameterType == typeof(string))
                return ToString();
            if (parameterType == typeof(char))
                return ToString()[0];
            if (parameterType == typeof(float))
                return (float)ToDouble();
            if (parameterType == typeof(double))
                return (float)ToDouble();
            if (parameterType == typeof(bool))
                return ToBoolean();
            throw new NotSupportedException();
        }

        public static JsValue FromObject(object val)
        {
            var context = JsContext.Current;

            switch (val)
            {
                case null:
                    return Null;
                case JsValue jsValue:
                    return jsValue;
                case short i16:
                    return FromInt(i16);
                case ushort ui16:
                    return FromInt(ui16);
                case int i32:
                    return FromInt(i32);
                case uint ui32:
                    return FromInt((int)ui32);
                case string s:
                    return FromString(s);
                case char ch:
                    return FromString(ch.ToString());
                case float f32:
                    return FromDouble(f32);
                case double f64:
                    return FromDouble(f64);
                case bool b:
                    return b ? True : False;
                case Action a:
                    return CreateFunction(a);
                case Func<JsValue> func:
                    return CreateFunction(func);
                case Action<JsFunctionArgs> a:
                    return CreateFunction(a);
                case Func<JsFunctionArgs, JsValue> func:
                    return CreateFunction(func);
                default:
                    return CreateProxy(val);
            }
        }

        public static implicit operator string(JsValue value)
        {
            return value.ToString();
        }

        public static implicit operator JsValue(string value)
        {
            return FromString(value);
        }

        public static implicit operator int(JsValue value)
        {
            return value.ToInt32();
        }

        public static implicit operator JsValue(int value)
        {
            return FromInt(value);
        }

        public static implicit operator bool(JsValue value)
        {
            return value.ToBoolean();
        }

        public static implicit operator JsValue(bool value)
        {
            return value ? True : False;
        }
    }
}