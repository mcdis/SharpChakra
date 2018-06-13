using System;
using System.Threading.Tasks;
using SharpChakra.Tasks;

namespace SharpChakra
{
    public struct JsContext
    {
        private static readonly TaskFactory TaskFactory = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(1));

        internal readonly IntPtr Reference;

        internal JsContext(IntPtr reference)
        {
            Reference = reference;
        }

        public static JsContext Current { get; internal set; }

        public static JsContext Invalid => new JsContext(IntPtr.Zero);

        public bool IsValid => Reference != IntPtr.Zero;

        public bool IsActive => Current.Reference == Reference;

        public Task RequestScopeAsync(Action<JsContext> action)
        {
            var context = this;

            if (Current.Reference == Reference)
            {
                action(context);
                return Task.FromResult(true);
            }

            return TaskFactory.StartNew(() =>
            {
                Native.ThrowIfError(Native.JsSetCurrentContext(context));
                action(context);
                Native.ThrowIfError(Native.JsSetCurrentContext(Invalid));
            });
        }

        public Task<T> RequestScopeAsync<T>(Func<JsContext, T> action)
        {
            var context = this;

            if (Current.Reference == Reference)
            {
                return Task.FromResult(action(context));
            }

            return TaskFactory.StartNew(() =>
            {
                Native.ThrowIfError(Native.JsSetCurrentContext(context));
                var result = action(context);
                Native.ThrowIfError(Native.JsSetCurrentContext(Invalid));

                return result;
            });
        }

        internal void ThrowIfInvalid()
        {
            if (!IsValid)
            {
                throw new InvalidOperationException("Invalid state");
            }

            if (!IsActive)
            {
                throw new InvalidOperationException("The state is not active");
            }
        }

        public JsValue Global
        {
            get
            {
                ThrowIfInvalid();
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
                ThrowIfInvalid();
                Native.ThrowIfError(Native.JsGetRuntime(this, out var handle));
                return handle;
            }
        }

        public uint Idle()
        {
            ThrowIfInvalid();
            Native.ThrowIfError(Native.JsIdle(out var ticks));
            return ticks;
        }

        public JsModuleRecord CreateModule(JsModuleRecord reference, JsValue specifier)
        {
            ThrowIfInvalid();
            Native.ThrowIfError(Native.JsInitializeModuleRecord(reference, specifier, out var module));
            return module;
        }

        public JsValue ParseScript(string script, JsSourceContext sourceContext, string sourceName)
        {
            ThrowIfInvalid();
            Native.ThrowIfError(Native.JsParseScript(script, sourceContext, sourceName, out var result));
            return result;
        }

        public JsValue ParseScript(string script, byte[] buffer, JsSourceContext sourceContext, string sourceName)
        {
            ThrowIfInvalid();
            Native.ThrowIfError(Native.JsParseSerializedScript(script, buffer, sourceContext, sourceName, out var result));
            return result;
        }

        public JsValue ParseScript(string script)
        {
            ThrowIfInvalid();
            return ParseScript(script, JsSourceContext.None, string.Empty);
        }

        public JsValue ParseScript(string script, byte[] buffer)
        {
            ThrowIfInvalid();
            return ParseScript(script, buffer, JsSourceContext.None, string.Empty);
        }

        public JsValue RunScript(string script, JsSourceContext sourceContext, string sourceName)
        {
            ThrowIfInvalid();
            Native.ThrowIfError(Native.JsRunScript(script, sourceContext, sourceName, out var result));
            return result;
        }

        public JsValue RunScript(string script, byte[] buffer, JsSourceContext sourceContext, string sourceName)
        {
            ThrowIfInvalid();
            Native.ThrowIfError(Native.JsRunSerializedScript(script, buffer, sourceContext, sourceName, out var result));
            return result;
        }

        public JsValue RunScript(string script)
        {
            ThrowIfInvalid();
            return RunScript(script, JsSourceContext.None, string.Empty);
        }

        public JsValue RunScript(string script, byte[] buffer)
        {
            ThrowIfInvalid();
            return RunScript(script, buffer, JsSourceContext.None, string.Empty);
        }

        public ulong SerializeScript(string script, byte[] buffer)
        {
            ThrowIfInvalid();
            var bufferSize = (ulong)buffer.Length;
            Native.ThrowIfError(Native.JsSerializeScript(script, buffer, ref bufferSize));
            return bufferSize;
        }

        public JsValue GetAndClearException()
        {
            ThrowIfInvalid();
            Native.ThrowIfError(Native.JsGetAndClearException(out var reference));
            return reference;
        }

        public void SetException(JsValue exception)
        {
            ThrowIfInvalid();
            Native.ThrowIfError(Native.JsSetException(exception));
        }

        public uint AddRef()
        {
            ThrowIfInvalid();
            Native.ThrowIfError(Native.JsContextAddRef(this, out var count));
            return count;
        }

        public uint Release()
        {
            ThrowIfInvalid();
            Native.ThrowIfError(Native.JsContextRelease(this, out var count));
            return count;
        }
    }
}