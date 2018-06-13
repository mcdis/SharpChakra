using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;

namespace SharpChakra
{
    public struct JsContext
    {
        private static readonly JsSemaphore Semaphore = new JsSemaphore();
        internal readonly IntPtr Reference;

        internal JsContext(IntPtr reference)
        {
            Reference = reference;
        }

        public static JsContext Current { get; internal set; }

        public static JsContext Invalid => new JsContext(IntPtr.Zero);

        public JsValue Global
        {
            get
            {
                try
                {
                    Semaphore.WaitOne(this);
                    EnsureCurrent();
                    Native.ThrowIfError(Native.JsGetGlobalObject(out var value));
                    return value;
                }
                finally
                {
                    Semaphore.Release();
                }
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

        public static bool HasException
        {
            get
            {
                Native.ThrowIfError(Native.JsHasException(out var hasException));
                return hasException;
            }
        }

        public bool IsValid => Reference != IntPtr.Zero;

        public bool IsActive => Equals(Current, this);

        public uint Idle()
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsIdle(out var ticks));
                return ticks;
            }
            finally
            {
                Semaphore.Release();
            }
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
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsParseScript(script, sourceContext, sourceName, out var result));
                return result;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        public JsValue ParseScript(string script, byte[] buffer, JsSourceContext sourceContext, string sourceName)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsParseSerializedScript(script, buffer, sourceContext, sourceName, out var result));
                return result;
            }
            finally
            {
                Semaphore.Release();
            }
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
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsRunScript(script, sourceContext, sourceName, out var result));
                return result;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        public JsValue RunScript(string script, byte[] buffer, JsSourceContext sourceContext, string sourceName)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsRunSerializedScript(script, buffer, sourceContext, sourceName, out var result));
                return result;
            }
            finally
            {
                Semaphore.Release();
            }
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
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                var bufferSize = (ulong) buffer.Length;
                Native.ThrowIfError(Native.JsSerializeScript(script, buffer, ref bufferSize));
                return bufferSize;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        public JsValue GetAndClearException()
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsGetAndClearException(out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        public void SetException(JsValue exception)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsSetException(exception));
            }
            finally
            {
                Semaphore.Release();
            }
        }

        internal void EnsureCurrent()
        {
            if (!IsActive)
            {
                Native.JsSetCurrentContext(this);
            }
        }

        public uint AddRef()
        {
            try
            {
                Semaphore.WaitOne(this);
                Native.ThrowIfError(Native.JsContextAddRef(this, out var count));
                return count;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        public uint Release()
        {
            try
            {
                Semaphore.WaitOne(this);
                Native.ThrowIfError(Native.JsContextRelease(this, out var count));
                return count;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateUndefined()
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsGetUndefinedValue(out var value));
                return value;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateNull()
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsGetNullValue(out var value));
                return value;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateTrue()
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsGetTrueValue(out var value));
                return value;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateFalse()
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsGetFalseValue(out var value));
                return value;
            }
            finally
            {
                Semaphore.Release();
            }
        }


        [Pure]
        public JsValue CreateBoolean(bool value)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsBoolToBoolean(value, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateDouble(double value)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsDoubleToNumber(value, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateInt32(int value)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsIntToNumber(value, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateString(string value)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(
                    Native.JsPointerToString(value, new UIntPtr((uint) value.Length), out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateObject()
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateObject(out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateExternalObject(IntPtr data, JsObjectFinalizeCallback finalizer)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateExternalObject(data, finalizer, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateFunction(JsNativeFunction function)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateFunction(function, IntPtr.Zero, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateFunction(JsNativeFunction function, IntPtr callbackData)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateFunction(function, callbackData, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateArray(uint length)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateArray(length, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateError(JsValue message)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateError(message, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateRangeError(JsValue message)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateRangeError(message, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateReferenceError(JsValue message)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateReferenceError(message, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateSyntaxError(JsValue message)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateSyntaxError(message, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateTypeError(JsValue message)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateTypeError(message, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }

        [Pure]
        public JsValue CreateUriError(JsValue message)
        {
            try
            {
                Semaphore.WaitOne(this);
                EnsureCurrent();
                Native.ThrowIfError(Native.JsCreateUriError(message, out var reference));
                return reference;
            }
            finally
            {
                Semaphore.Release();
            }
        }
    }

    public struct JsContextLock { }
}