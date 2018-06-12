using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SharpChakra
{
    public struct JsContext
    {
        private readonly IntPtr _pReference;

        internal JsContext(IntPtr reference)
        {
            _pReference = reference;
        }

        public static JsContext Current { get; internal set; }

        public static JsContext Invalid => new JsContext(IntPtr.Zero);

        public JsValue Global
        {
            get
            {
                EnsureCurrent();
                Native.ThrowIfError(Native.JsGetGlobalObject(out var value));
                return value;
            }
        }

        public static bool HasException
        {
            get
            {
                Native.ThrowIfError(Native.JsHasException(out var hasException));
                return hasException;
            }
        }

        public JsRuntime Runtime
        {
            get
            {
                Native.ThrowIfError(Native.JsGetRuntime(this, out var handle));
                return handle;
            }
        }

        public bool IsValid => _pReference != IntPtr.Zero;

        public bool IsActive => Equals(Current, this);

        public uint Idle()
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsIdle(out var ticks));
            return ticks;
        }

        public JsModuleRecord CreateModule(JsModuleRecord reference, JsValue specifier)
        {
            Native.ThrowIfError(Native.JsInitializeModuleRecord(reference, specifier, out var module));
            return module;
        }

        public JsModuleRecord CreateModule(JsModuleRecord reference, string specifier = "")
        {
            return CreateModule(reference, CreateString(specifier));
        }

        public JsValue ParseScript(string script, JsSourceContext sourceContext, string sourceName)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsParseScript(script, sourceContext, sourceName, out var result));
            return result;
        }

        public JsValue ParseScript(string script, byte[] buffer, JsSourceContext sourceContext, string sourceName)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsParseSerializedScript(script, buffer, sourceContext, sourceName, out var result));
            return result;
        }

        public JsValue ParseScript(string script)
        {
            return ParseScript(script, JsSourceContext.None, string.Empty);
        }

        public JsValue ParseScript(string script, byte[] buffer)
        {
            return ParseScript(script, buffer, JsSourceContext.None, string.Empty);
        }

        public JsValue RunScript(string script, JsSourceContext sourceContext, string sourceName)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsRunScript(script, sourceContext, sourceName, out var result));
            return result;
        }

        public JsValue RunScript(string script, byte[] buffer, JsSourceContext sourceContext, string sourceName)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsRunSerializedScript(script, buffer, sourceContext, sourceName, out var result));
            return result;
        }

        public JsValue RunScript(string script)
        {
            return RunScript(script, JsSourceContext.None, string.Empty);
        }

        public JsValue RunScript(string script, byte[] buffer)
        {
            return RunScript(script, buffer, JsSourceContext.None, string.Empty);
        }

        public ulong SerializeScript(string script, byte[] buffer)
        {
            EnsureCurrent();
            var bufferSize = (ulong) buffer.Length;
            Native.ThrowIfError(Native.JsSerializeScript(script, buffer, ref bufferSize));
            return bufferSize;
        }

        public JsValue GetAndClearException()
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsGetAndClearException(out var reference));
            return reference;
        }

        public void SetException(JsValue exception)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsSetException(exception));
        }

        public void EnsureCurrent()
        {
            if (!IsActive)
            {
                Native.JsSetCurrentContext(this);
            }
        }

        public uint AddRef()
        {
            Native.ThrowIfError(Native.JsContextAddRef(this, out var count));
            return count;
        }

        public uint Release()
        {
            Native.ThrowIfError(Native.JsContextRelease(this, out var count));
            return count;
        }
        public JsValue Create(object val)
        {
            switch (val)
            {
                case null:
                    return CreateNull();
                case short i16:
                    return CreateInt32(i16);
                case ushort ui16:
                    return CreateInt32(ui16);
                case int i32:
                    return CreateInt32(i32);
                case uint ui32:
                    return CreateInt32((int)ui32);
                case string s:
                    return CreateString(s);
                case char ch:
                    return CreateString(ch.ToString());
                case float f32:
                    return CreateDouble(f32);
                case double f64:
                    return CreateDouble(f64);
                case bool b:
                    return CreateBoolean(b);
                case Action a:
                    return CreateFunction(a);
                case Func<JsValue> func:
                    return CreateFunction(func);
                default:
                    return CreateUndefined();
            }
        }

        [Pure]
        public JsValue CreateUndefined()
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsGetUndefinedValue(out var value));
            return value;
        }

        [Pure]
        public JsValue CreateNull()
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsGetNullValue(out var value));
            return value;
        }

        [Pure]
        public JsValue CreateTrue()
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsGetTrueValue(out var value));
            return value;
        }

        [Pure]
        public JsValue CreateFalse()
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsGetFalseValue(out var value));
            return value;
        }


        [Pure]
        public JsValue CreateBoolean(bool value)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsBoolToBoolean(value, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateDouble(double value)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsDoubleToNumber(value, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateInt32(int value)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsIntToNumber(value, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateString(string value)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsPointerToString(value, new UIntPtr((uint)value.Length), out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateObject()
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateObject(out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateExternalObject(IntPtr data, JsObjectFinalizeCallback finalizer)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateExternalObject(data, finalizer, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateFunction(JsNativeFunction function)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateFunction(function, IntPtr.Zero, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateFunction(JsNativeFunction function, IntPtr callbackData)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateFunction(function, callbackData, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateArray(uint length)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateArray(length, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateError(JsValue message)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateError(message, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateRangeError(JsValue message)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateRangeError(message, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateReferenceError(JsValue message)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateReferenceError(message, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateSyntaxError(JsValue message)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateSyntaxError(message, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateTypeError(JsValue message)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateTypeError(message, out var reference));
            return reference;
        }

        [Pure]
        public JsValue CreateUriError(JsValue message)
        {
            EnsureCurrent();
            Native.ThrowIfError(Native.JsCreateUriError(message, out var reference));
            return reference;
        }

        public JsValue CreateFunction(Action x)
        {
            var that = this;

            return CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) =>
            {
                x();
                return that.CreateUndefined();
            });
        }

        public JsValue CreateFunction(Func<JsValue> x)
        {
            return CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) => x());
        }

        public JsValue CreateFunction(Action<JsNativeFunctionArgs> handler)
        {
            var that = this;

            return CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) =>
            {
                handler(new JsNativeFunctionArgs
                {
                    Callee = callee,
                    IsConstructCall = isConstructCall,
                    Arguments = arguments,
                    ArgumentCount = argumentCount,
                    CallbackData = callbackData
                });

                return that.CreateUndefined();
            });
        }

        public JsValue CreateFunction(Func<JsNativeFunctionArgs, JsValue> handler)
        {
            return CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData)
                => handler(new JsNativeFunctionArgs
                {
                    Callee = callee,
                    IsConstructCall = isConstructCall,
                    Arguments = arguments,
                    ArgumentCount = argumentCount,
                    CallbackData = callbackData
                }));
        }
    }
}