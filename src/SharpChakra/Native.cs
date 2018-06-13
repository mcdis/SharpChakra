using System;
using System.Runtime.InteropServices;
using SharpChakra.Natives;

namespace SharpChakra
{
    /// <summary>
    /// Native interfaces.
    /// </summary>
    public static class Native
    {
#if NETSTANDARD
       private static readonly bool Is32 = RuntimeInformation.OSArchitecture == Architecture.X86;
       private static readonly bool Is64 = RuntimeInformation.OSArchitecture == Architecture.X64;
       private static readonly bool IsArm = RuntimeInformation.OSArchitecture == Architecture.Arm;
       private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
       private static readonly bool IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
       private static readonly bool IsMac = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
#else
        private static readonly bool Is32 = IntPtr.Size == 4;
#endif

        public static JsContext DefaultContext { get; }

        static Native()
        {
            JsGetCurrentContext(out var context);
            DefaultContext = context;
        }

        public static void ThrowIfError(JsErrorCode error)
        {
            if (error == JsErrorCode.NoError)
            {
                return;
            }

            switch (error)
            {
                case JsErrorCode.InvalidArgument:
                    throw new JsUsageException(error, "Invalid argument.");

                case JsErrorCode.NullArgument:
                    throw new JsUsageException(error, "Null argument.");

                case JsErrorCode.NoCurrentContext:
                    throw new JsUsageException(error, "No current context.");

                case JsErrorCode.InExceptionState:
                    throw new JsUsageException(error, "Runtime is in exception state.");

                case JsErrorCode.NotImplemented:
                    throw new JsUsageException(error, "Method is not implemented.");

                case JsErrorCode.WrongThread:
                    throw new JsUsageException(error, "Runtime is active on another thread.");

                case JsErrorCode.RuntimeInUse:
                    throw new JsUsageException(error, "Runtime is in use.");

                case JsErrorCode.BadSerializedScript:
                    throw new JsUsageException(error, "Bad serialized script.");

                case JsErrorCode.InDisabledState:
                    throw new JsUsageException(error, "Runtime is disabled.");

                case JsErrorCode.CannotDisableExecution:
                    throw new JsUsageException(error, "Cannot disable execution.");

                case JsErrorCode.AlreadyDebuggingContext:
                    throw new JsUsageException(error, "Context is already in debug mode.");

                case JsErrorCode.HeapEnumInProgress:
                    throw new JsUsageException(error, "Heap enumeration is in progress.");

                case JsErrorCode.ArgumentNotObject:
                    throw new JsUsageException(error, "Argument is not an object.");

                case JsErrorCode.InProfileCallback:
                    throw new JsUsageException(error, "In a profile callback.");

                case JsErrorCode.InThreadServiceCallback:
                    throw new JsUsageException(error, "In a thread service callback.");

                case JsErrorCode.CannotSerializeDebugScript:
                    throw new JsUsageException(error, "Cannot serialize a debug script.");

                case JsErrorCode.AlreadyProfilingContext:
                    throw new JsUsageException(error, "Already profiling this context.");

                case JsErrorCode.IdleNotEnabled:
                    throw new JsUsageException(error, "Idle is not enabled.");

                case JsErrorCode.OutOfMemory:
                    throw new JsEngineException(error, "Out of memory.");

                case JsErrorCode.ScriptException:
                {
                    var innerError = JsGetAndClearException(out var errorObject);

                    if (innerError != JsErrorCode.NoError)
                    {
                        throw new JsFatalException(innerError);
                    }

                    throw new JsScriptException(error, errorObject, "Script threw an exception.");
                }

                case JsErrorCode.ScriptCompile:
                {
                    var innerError = JsGetAndClearException(out var errorObject);

                    if (innerError != JsErrorCode.NoError)
                    {
                        throw new JsFatalException(innerError);
                    }

                    throw new JsScriptException(error, errorObject, "Compile error.");
                }

                case JsErrorCode.ScriptTerminated:
                    throw new JsScriptException(error, JsValue.Invalid, "Script was terminated.");

                case JsErrorCode.ScriptEvalDisabled:
                    throw new JsScriptException(error, JsValue.Invalid,
                        "Eval of strings is disabled in this runtime.");

                case JsErrorCode.Fatal:
                    throw new JsFatalException(error);

                default:
                    throw new JsFatalException(error);
            }
        }

        internal static JsErrorCode JsCreateRuntime(JsRuntimeAttributes attributes,
            JavaScriptThreadServiceCallback threadService,
            out JsRuntime runtime)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateRuntime(attributes, threadService, out runtime);
            if (IsWindows && Is64) return Native64.JsCreateRuntime(attributes, threadService, out runtime);
            if (IsWindows && IsArm) return NativeArm.JsCreateRuntime(attributes, threadService, out runtime);
            if (IsLinux && Is64) return Native64Linux.JsCreateRuntime(attributes, threadService, out runtime);
            if (IsMac && Is64) return Native64Mac.JsCreateRuntime(attributes, threadService, out runtime);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateRuntime(attributes, threadService, out runtime)
                : Native64.JsCreateRuntime(attributes, threadService, out runtime);
#endif
        }

        public static JsErrorCode JsCollectGarbage(JsRuntime handle)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCollectGarbage(handle);
            if (IsWindows && Is64) return Native64.JsCollectGarbage(handle);
            if (IsWindows && IsArm) return NativeArm.JsCollectGarbage(handle);
            if (IsLinux && Is64) return Native64Linux.JsCollectGarbage(handle);
            if (IsMac && Is64) return Native64Mac.JsCollectGarbage(handle);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCollectGarbage(handle)
                : Native64.JsCollectGarbage(handle);
#endif
        }

        public static JsErrorCode JsDisposeRuntime(JsRuntime handle)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDisposeRuntime(handle);
            if (IsWindows && Is64) return Native64.JsDisposeRuntime(handle);
            if (IsWindows && IsArm) return NativeArm.JsDisposeRuntime(handle);
            if (IsLinux && Is64) return Native64Linux.JsDisposeRuntime(handle);
            if (IsMac && Is64) return Native64Mac.JsDisposeRuntime(handle);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDisposeRuntime(handle)
                : Native64.JsDisposeRuntime(handle);
#endif
        }

        public static JsErrorCode JsGetRuntimeMemoryUsage(JsRuntime runtime, out UIntPtr memoryUsage)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetRuntimeMemoryUsage(runtime, out memoryUsage);
            if (IsWindows && Is64) return Native64.JsGetRuntimeMemoryUsage(runtime, out memoryUsage);
            if (IsWindows && IsArm) return NativeArm.JsGetRuntimeMemoryUsage(runtime, out memoryUsage);
            if (IsLinux && Is64) return Native64Linux.JsGetRuntimeMemoryUsage(runtime, out memoryUsage);
            if (IsMac && Is64) return Native64Mac.JsGetRuntimeMemoryUsage(runtime, out memoryUsage);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetRuntimeMemoryUsage(runtime, out memoryUsage)
                : Native64.JsGetRuntimeMemoryUsage(runtime, out memoryUsage);
#endif
        }

        public static JsErrorCode JsGetRuntimeMemoryLimit(JsRuntime runtime, out UIntPtr memoryLimit)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetRuntimeMemoryLimit(runtime, out memoryLimit);
            if (IsWindows && Is64) return Native64.JsGetRuntimeMemoryLimit(runtime, out memoryLimit);
            if (IsWindows && IsArm) return NativeArm.JsGetRuntimeMemoryLimit(runtime, out memoryLimit);
            if (IsLinux && Is64) return Native64Linux.JsGetRuntimeMemoryLimit(runtime, out memoryLimit);
            if (IsMac && Is64) return Native64Mac.JsGetRuntimeMemoryLimit(runtime, out memoryLimit);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetRuntimeMemoryLimit(runtime, out memoryLimit)
                : Native64.JsGetRuntimeMemoryLimit(runtime, out memoryLimit);
#endif
        }

        public static JsErrorCode JsSetRuntimeMemoryLimit(JsRuntime runtime, UIntPtr memoryLimit)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetRuntimeMemoryLimit(runtime, memoryLimit);
            if (IsWindows && Is64) return Native64.JsSetRuntimeMemoryLimit(runtime, memoryLimit);
            if (IsWindows && IsArm) return NativeArm.JsSetRuntimeMemoryLimit(runtime, memoryLimit);
            if (IsLinux && Is64) return Native64Linux.JsSetRuntimeMemoryLimit(runtime, memoryLimit);
            if (IsMac && Is64) return Native64Mac.JsSetRuntimeMemoryLimit(runtime, memoryLimit);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetRuntimeMemoryLimit(runtime, memoryLimit)
                : Native64.JsSetRuntimeMemoryLimit(runtime, memoryLimit);
#endif
        }

        public static JsErrorCode JsSetRuntimeMemoryAllocationCallback(JsRuntime runtime,
            IntPtr callbackState,
            JsMemoryAllocationCallback allocationCallback)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetRuntimeMemoryAllocationCallback(runtime, callbackState, allocationCallback);
            if (IsWindows && Is64) return Native64.JsSetRuntimeMemoryAllocationCallback(runtime, callbackState, allocationCallback);
            if (IsWindows && IsArm) return NativeArm.JsSetRuntimeMemoryAllocationCallback(runtime, callbackState, allocationCallback);
            if (IsLinux && Is64) return Native64Linux.JsSetRuntimeMemoryAllocationCallback(runtime, callbackState, allocationCallback);
            if (IsMac && Is64) return Native64Mac.JsSetRuntimeMemoryAllocationCallback(runtime, callbackState, allocationCallback);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetRuntimeMemoryAllocationCallback(runtime, callbackState, allocationCallback)
                : Native64.JsSetRuntimeMemoryAllocationCallback(runtime, callbackState, allocationCallback);
#endif
        }

        public static JsErrorCode JsSetRuntimeBeforeCollectCallback(JsRuntime runtime,
            IntPtr callbackState,
            JsBeforeCollectCallback beforeCollectCallback)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetRuntimeBeforeCollectCallback(runtime, callbackState, beforeCollectCallback);
            if (IsWindows && Is64) return Native64.JsSetRuntimeBeforeCollectCallback(runtime, callbackState, beforeCollectCallback);
            if (IsWindows && IsArm) return NativeArm.JsSetRuntimeBeforeCollectCallback(runtime, callbackState, beforeCollectCallback);
            if (IsLinux && Is64) return Native64Linux.JsSetRuntimeBeforeCollectCallback(runtime, callbackState, beforeCollectCallback);
            if (IsMac && Is64) return Native64Mac.JsSetRuntimeBeforeCollectCallback(runtime, callbackState, beforeCollectCallback);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetRuntimeBeforeCollectCallback(runtime, callbackState, beforeCollectCallback)
                : Native64.JsSetRuntimeBeforeCollectCallback(runtime, callbackState, beforeCollectCallback);
#endif
        }

        public static JsErrorCode JsContextAddRef(JsContext reference, out uint count)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsContextAddRef(reference, out count);
            if (IsWindows && Is64) return Native64.JsContextAddRef(reference, out count);
            if (IsWindows && IsArm) return NativeArm.JsContextAddRef(reference, out count);
            if (IsLinux && Is64) return Native64Linux.JsContextAddRef(reference, out count);
            if (IsMac && Is64) return Native64Mac.JsContextAddRef(reference, out count);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsContextAddRef(reference, out count)
                : Native64.JsContextAddRef(reference, out count);
#endif
        }

        public static JsErrorCode JsAddRef(JsValue reference, out uint count)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsAddRef(reference, out count);
            if (IsWindows && Is64) return Native64.JsAddRef(reference, out count);
            if (IsWindows && IsArm) return NativeArm.JsAddRef(reference, out count);
            if (IsLinux && Is64) return Native64Linux.JsAddRef(reference, out count);
            if (IsMac && Is64) return Native64Mac.JsAddRef(reference, out count);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsAddRef(reference, out count)
                : Native64.JsAddRef(reference, out count);
#endif
        }

        public static JsErrorCode JsContextRelease(JsContext reference, out uint count)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsContextRelease(reference, out count);
            if (IsWindows && Is64) return Native64.JsContextRelease(reference, out count);
            if (IsWindows && IsArm) return NativeArm.JsContextRelease(reference, out count);
            if (IsLinux && Is64) return Native64Linux.JsContextRelease(reference, out count);
            if (IsMac && Is64) return Native64Mac.JsContextRelease(reference, out count);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsContextRelease(reference, out count)
                : Native64.JsContextRelease(reference, out count);
#endif
        }

        public static JsErrorCode JsRelease(JsValue reference, out uint count)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsRelease(reference, out count);
            if (IsWindows && Is64) return Native64.JsRelease(reference, out count);
            if (IsWindows && IsArm) return NativeArm.JsRelease(reference, out count);
            if (IsLinux && Is64) return Native64Linux.JsRelease(reference, out count);
            if (IsMac && Is64) return Native64Mac.JsRelease(reference, out count);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsRelease(reference, out count)
                : Native64.JsRelease(reference, out count);
#endif
        }

        public static JsErrorCode JsCreateContext(JsRuntime runtime, out JsContext newContext)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateContext(runtime, out newContext);
            if (IsWindows && Is64) return Native64.JsCreateContext(runtime, out newContext);
            if (IsWindows && IsArm) return NativeArm.JsCreateContext(runtime, out newContext);
            if (IsLinux && Is64) return Native64Linux.JsCreateContext(runtime, out newContext);
            if (IsMac && Is64) return Native64Mac.JsCreateContext(runtime, out newContext);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateContext(runtime, out newContext)
                : Native64.JsCreateContext(runtime, out newContext);
#endif
        }

        public static JsErrorCode JsGetCurrentContext(out JsContext currentContext)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetCurrentContext(out currentContext);
            if (IsWindows && Is64) return Native64.JsGetCurrentContext(out currentContext);
            if (IsWindows && IsArm) return NativeArm.JsGetCurrentContext(out currentContext);
            if (IsLinux && Is64) return Native64Linux.JsGetCurrentContext(out currentContext);
            if (IsMac && Is64) return Native64Mac.JsGetCurrentContext(out currentContext);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetCurrentContext(out currentContext)
                : Native64.JsGetCurrentContext(out currentContext);
#endif
        }

        public static JsErrorCode JsSetCurrentContext(JsContext context)
        {
            var errorCode = JsSetCurrentContextImpl(context);

            if (errorCode != JsErrorCode.NoError)
            {
                return errorCode;
            }

            JsContext.Current = context;

            return errorCode;
        }

        private static JsErrorCode JsSetCurrentContextImpl(JsContext context)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetCurrentContext(context);
            if (IsWindows && Is64) return Native64.JsSetCurrentContext(context);
            if (IsWindows && IsArm) return NativeArm.JsSetCurrentContext(context);
            if (IsLinux && Is64) return Native64Linux.JsSetCurrentContext(context);
            if (IsMac && Is64) return Native64Mac.JsSetCurrentContext(context);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetCurrentContext(context)
                : Native64.JsSetCurrentContext(context);
#endif
        }

        public static JsErrorCode JsGetRuntime(JsContext context, out JsRuntime runtime)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetRuntime(context, out runtime);
            if (IsWindows && Is64) return Native64.JsGetRuntime(context, out runtime);
            if (IsWindows && IsArm) return NativeArm.JsGetRuntime(context, out runtime);
            if (IsLinux && Is64) return Native64Linux.JsGetRuntime(context, out runtime);
            if (IsMac && Is64) return Native64Mac.JsGetRuntime(context, out runtime);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetRuntime(context, out runtime)
                : Native64.JsGetRuntime(context, out runtime);
#endif
        }

        public static JsErrorCode JsIdle(out uint nextIdleTick)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsIdle(out nextIdleTick);
            if (IsWindows && Is64) return Native64.JsIdle(out nextIdleTick);
            if (IsWindows && IsArm) return NativeArm.JsIdle(out nextIdleTick);
            if (IsLinux && Is64) return Native64Linux.JsIdle(out nextIdleTick);
            if (IsMac && Is64) return Native64Mac.JsIdle(out nextIdleTick);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsIdle(out nextIdleTick)
                : Native64.JsIdle(out nextIdleTick);
#endif
        }

        public static JsErrorCode JsParseScript(string script,
            JsSourceContext sourceContext,
            string sourceUrl,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsParseScript(script, sourceContext, sourceUrl, out result);
            if (IsWindows && Is64) return Native64.JsParseScript(script, sourceContext, sourceUrl, out result);
            if (IsWindows && IsArm) return NativeArm.JsParseScript(script, sourceContext, sourceUrl, out result);
            if (IsLinux && Is64) return Native64Linux.JsParseScript(script, sourceContext, sourceUrl, out result);
            if (IsMac && Is64) return Native64Mac.JsParseScript(script, sourceContext, sourceUrl, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsParseScript(script, sourceContext, sourceUrl, out result)
                : Native64.JsParseScript(script, sourceContext, sourceUrl, out result);
#endif
        }

        public static JsErrorCode JsRunScript(string script,
            JsSourceContext sourceContext,
            string sourceUrl,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsRunScript(script, sourceContext, sourceUrl, out result);
            if (IsWindows && Is64) return Native64.JsRunScript(script, sourceContext, sourceUrl, out result);
            if (IsWindows && IsArm) return NativeArm.JsRunScript(script, sourceContext, sourceUrl, out result);
            if (IsLinux && Is64) return Native64Linux.JsRunScript(script, sourceContext, sourceUrl, out result);
            if (IsMac && Is64) return Native64Mac.JsRunScript(script, sourceContext, sourceUrl, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsRunScript(script, sourceContext, sourceUrl, out result)
                : Native64.JsRunScript(script, sourceContext, sourceUrl, out result);
#endif
        }

        public static JsErrorCode JsSerializeScript(string script, byte[] buffer, ref ulong bufferSize)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSerializeScript(script, buffer, ref bufferSize);
            if (IsWindows && Is64) return Native64.JsSerializeScript(script, buffer, ref bufferSize);
            if (IsWindows && IsArm) return NativeArm.JsSerializeScript(script, buffer, ref bufferSize);
            if (IsLinux && Is64) return Native64Linux.JsSerializeScript(script, buffer, ref bufferSize);
            if (IsMac && Is64) return Native64Mac.JsSerializeScript(script, buffer, ref bufferSize);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSerializeScript(script, buffer, ref bufferSize)
                : Native64.JsSerializeScript(script, buffer, ref bufferSize);
#endif
        }

        public static JsErrorCode JsParseSerializedScript(string script,
            byte[] buffer,
            JsSourceContext sourceContext,
            string sourceUrl,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsParseSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
            if (IsWindows && Is64) return Native64.JsParseSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
            if (IsWindows && IsArm) return NativeArm.JsParseSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
            if (IsLinux && Is64) return Native64Linux.JsParseSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
            if (IsMac && Is64) return Native64Mac.JsParseSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsParseSerializedScript(script, buffer, sourceContext, sourceUrl, out result)
                : Native64.JsParseSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
#endif
        }

        public static JsErrorCode JsRunSerializedScript(string script,
            byte[] buffer,
            JsSourceContext sourceContext,
            string sourceUrl,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsRunSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
            if (IsWindows && Is64) return Native64.JsRunSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
            if (IsWindows && IsArm) return NativeArm.JsRunSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
            if (IsLinux && Is64) return Native64Linux.JsRunSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
            if (IsMac && Is64) return Native64Mac.JsRunSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsRunSerializedScript(script, buffer, sourceContext, sourceUrl, out result)
                : Native64.JsRunSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
#endif
        }

        public static JsErrorCode JsGetPropertyIdFromName(string name, out JsPropertyId propertyId)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetPropertyIdFromName(name, out propertyId);
            if (IsWindows && Is64) return Native64.JsGetPropertyIdFromName(name, out propertyId);
            if (IsWindows && IsArm) return NativeArm.JsGetPropertyIdFromName(name, out propertyId);
            if (IsLinux && Is64) return Native64Linux.JsGetPropertyIdFromName(name, out propertyId);
            if (IsMac && Is64) return Native64Mac.JsGetPropertyIdFromName(name, out propertyId);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetPropertyIdFromName(name, out propertyId)
                : Native64.JsGetPropertyIdFromName(name, out propertyId);
#endif
        }

        public static JsErrorCode JsGetPropertyNameFromId(JsPropertyId propertyId, out string name)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetPropertyNameFromId(propertyId, out name);
            if (IsWindows && Is64) return Native64.JsGetPropertyNameFromId(propertyId, out name);
            if (IsWindows && IsArm) return NativeArm.JsGetPropertyNameFromId(propertyId, out name);
            if (IsLinux && Is64) return Native64Linux.JsGetPropertyNameFromId(propertyId, out name);
            if (IsMac && Is64) return Native64Mac.JsGetPropertyNameFromId(propertyId, out name);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetPropertyNameFromId(propertyId, out name)
                : Native64.JsGetPropertyNameFromId(propertyId, out name);
#endif
        }

        public static JsErrorCode JsGetUndefinedValue(out JsValue undefinedValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetUndefinedValue(out undefinedValue);
            if (IsWindows && Is64) return Native64.JsGetUndefinedValue(out undefinedValue);
            if (IsWindows && IsArm) return NativeArm.JsGetUndefinedValue(out undefinedValue);
            if (IsLinux && Is64) return Native64Linux.JsGetUndefinedValue(out undefinedValue);
            if (IsMac && Is64) return Native64Mac.JsGetUndefinedValue(out undefinedValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetUndefinedValue(out undefinedValue)
                : Native64.JsGetUndefinedValue(out undefinedValue);
#endif
        }

        public static JsErrorCode JsGetNullValue(out JsValue nullValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetNullValue(out nullValue);
            if (IsWindows && Is64) return Native64.JsGetNullValue(out nullValue);
            if (IsWindows && IsArm) return NativeArm.JsGetNullValue(out nullValue);
            if (IsLinux && Is64) return Native64Linux.JsGetNullValue(out nullValue);
            if (IsMac && Is64) return Native64Mac.JsGetNullValue(out nullValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetNullValue(out nullValue)
                : Native64.JsGetNullValue(out nullValue);
#endif
        }

        public static JsErrorCode JsGetTrueValue(out JsValue trueValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetTrueValue(out trueValue);
            if (IsWindows && Is64) return Native64.JsGetTrueValue(out trueValue);
            if (IsWindows && IsArm) return NativeArm.JsGetTrueValue(out trueValue);
            if (IsLinux && Is64) return Native64Linux.JsGetTrueValue(out trueValue);
            if (IsMac && Is64) return Native64Mac.JsGetTrueValue(out trueValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetTrueValue(out trueValue)
                : Native64.JsGetTrueValue(out trueValue);
#endif
        }

        public static JsErrorCode JsGetFalseValue(out JsValue falseValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetFalseValue(out falseValue);
            if (IsWindows && Is64) return Native64.JsGetFalseValue(out falseValue);
            if (IsWindows && IsArm) return NativeArm.JsGetFalseValue(out falseValue);
            if (IsLinux && Is64) return Native64Linux.JsGetFalseValue(out falseValue);
            if (IsMac && Is64) return Native64Mac.JsGetFalseValue(out falseValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetFalseValue(out falseValue)
                : Native64.JsGetFalseValue(out falseValue);
#endif
        }

        public static JsErrorCode JsBoolToBoolean(bool value, out JsValue booleanValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsBoolToBoolean(value, out booleanValue);
            if (IsWindows && Is64) return Native64.JsBoolToBoolean(value, out booleanValue);
            if (IsWindows && IsArm) return NativeArm.JsBoolToBoolean(value, out booleanValue);
            if (IsLinux && Is64) return Native64Linux.JsBoolToBoolean(value, out booleanValue);
            if (IsMac && Is64) return Native64Mac.JsBoolToBoolean(value, out booleanValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsBoolToBoolean(value, out booleanValue)
                : Native64.JsBoolToBoolean(value, out booleanValue);
#endif
        }

        public static JsErrorCode JsBooleanToBool(JsValue booleanValue, out bool boolValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsBooleanToBool(booleanValue, out boolValue);
            if (IsWindows && Is64) return Native64.JsBooleanToBool(booleanValue, out boolValue);
            if (IsWindows && IsArm) return NativeArm.JsBooleanToBool(booleanValue, out boolValue);
            if (IsLinux && Is64) return Native64Linux.JsBooleanToBool(booleanValue, out boolValue);
            if (IsMac && Is64) return Native64Mac.JsBooleanToBool(booleanValue, out boolValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsBooleanToBool(booleanValue, out boolValue)
                : Native64.JsBooleanToBool(booleanValue, out boolValue);
#endif
        }

        public static JsErrorCode JsConvertValueToBoolean(JsValue value, out JsValue booleanValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsConvertValueToBoolean(value, out booleanValue);
            if (IsWindows && Is64) return Native64.JsConvertValueToBoolean(value, out booleanValue);
            if (IsWindows && IsArm) return NativeArm.JsConvertValueToBoolean(value, out booleanValue);
            if (IsLinux && Is64) return Native64Linux.JsConvertValueToBoolean(value, out booleanValue);
            if (IsMac && Is64) return Native64Mac.JsConvertValueToBoolean(value, out booleanValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsConvertValueToBoolean(value, out booleanValue)
                : Native64.JsConvertValueToBoolean(value, out booleanValue);
#endif
        }

        public static JsErrorCode JsGetValueType(JsValue value, out JsValueType type)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetValueType(value, out type);
            if (IsWindows && Is64) return Native64.JsGetValueType(value, out type);
            if (IsWindows && IsArm) return NativeArm.JsGetValueType(value, out type);
            if (IsLinux && Is64) return Native64Linux.JsGetValueType(value, out type);
            if (IsMac && Is64) return Native64Mac.JsGetValueType(value, out type);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetValueType(value, out type)
                : Native64.JsGetValueType(value, out type);
#endif
        }

        public static JsErrorCode JsDoubleToNumber(double doubleValue, out JsValue value)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDoubleToNumber(doubleValue, out value);
            if (IsWindows && Is64) return Native64.JsDoubleToNumber(doubleValue, out value);
            if (IsWindows && IsArm) return NativeArm.JsDoubleToNumber(doubleValue, out value);
            if (IsLinux && Is64) return Native64Linux.JsDoubleToNumber(doubleValue, out value);
            if (IsMac && Is64) return Native64Mac.JsDoubleToNumber(doubleValue, out value);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDoubleToNumber(doubleValue, out value)
                : Native64.JsDoubleToNumber(doubleValue, out value);
#endif
        }

        public static JsErrorCode JsIntToNumber(int intValue, out JsValue value)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDoubleToNumber(intValue, out value);
            if (IsWindows && Is64) return Native64.JsDoubleToNumber(intValue, out value);
            if (IsWindows && IsArm) return NativeArm.JsDoubleToNumber(intValue, out value);
            if (IsLinux && Is64) return Native64Linux.JsDoubleToNumber(intValue, out value);
            if (IsMac && Is64) return Native64Mac.JsDoubleToNumber(intValue, out value);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDoubleToNumber(intValue, out value)
                : Native64.JsDoubleToNumber(intValue, out value);
#endif
        }

        public static JsErrorCode JsNumberToDouble(JsValue value, out double doubleValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsNumberToDouble(value, out doubleValue);
            if (IsWindows && Is64) return Native64.JsNumberToDouble(value, out doubleValue);
            if (IsWindows && IsArm) return NativeArm.JsNumberToDouble(value, out doubleValue);
            if (IsLinux && Is64) return Native64Linux.JsNumberToDouble(value, out doubleValue);
            if (IsMac && Is64) return Native64Mac.JsNumberToDouble(value, out doubleValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsNumberToDouble(value, out doubleValue)
                : Native64.JsNumberToDouble(value, out doubleValue);
#endif
        }

        public static JsErrorCode JsConvertValueToNumber(JsValue value, out JsValue numberValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsConvertValueToNumber(value, out numberValue);
            if (IsWindows && Is64) return Native64.JsConvertValueToNumber(value, out numberValue);
            if (IsWindows && IsArm) return NativeArm.JsConvertValueToNumber(value, out numberValue);
            if (IsLinux && Is64) return Native64Linux.JsConvertValueToNumber(value, out numberValue);
            if (IsMac && Is64) return Native64Mac.JsConvertValueToNumber(value, out numberValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsConvertValueToNumber(value, out numberValue)
                : Native64.JsConvertValueToNumber(value, out numberValue);
#endif
        }

        public static JsErrorCode JsGetStringLength(JsValue sringValue, out int length)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetStringLength(sringValue, out length);
            if (IsWindows && Is64) return Native64.JsGetStringLength(sringValue, out length);
            if (IsWindows && IsArm) return NativeArm.JsGetStringLength(sringValue, out length);
            if (IsLinux && Is64) return Native64Linux.JsGetStringLength(sringValue, out length);
            if (IsMac && Is64) return Native64Mac.JsGetStringLength(sringValue, out length);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetStringLength(sringValue, out length)
                : Native64.JsGetStringLength(sringValue, out length);
#endif
        }

        public static JsErrorCode JsPointerToString(string value, UIntPtr stringLength, out JsValue stringValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsPointerToString(value, stringLength, out stringValue);
            if (IsWindows && Is64) return Native64.JsPointerToString(value, stringLength, out stringValue);
            if (IsWindows && IsArm) return NativeArm.JsPointerToString(value, stringLength, out stringValue);
            if (IsLinux && Is64) return Native64Linux.JsPointerToString(value, stringLength, out stringValue);
            if (IsMac && Is64) return Native64Mac.JsPointerToString(value, stringLength, out stringValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsPointerToString(value, stringLength, out stringValue)
                : Native64.JsPointerToString(value, stringLength, out stringValue);
#endif
        }

        public static JsErrorCode JsStringToPointer(JsValue value, out IntPtr stringValue, out UIntPtr stringLength)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsStringToPointer(value, out stringValue, out stringLength);
            if (IsWindows && Is64) return Native64.JsStringToPointer(value, out stringValue, out stringLength);
            if (IsWindows && IsArm) return NativeArm.JsStringToPointer(value, out stringValue, out stringLength);
            if (IsLinux && Is64) return Native64Linux.JsStringToPointer(value, out stringValue, out stringLength);
            if (IsMac && Is64) return Native64Mac.JsStringToPointer(value, out stringValue, out stringLength);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsStringToPointer(value, out stringValue, out stringLength)
                : Native64.JsStringToPointer(value, out stringValue, out stringLength);
#endif
        }

        public static JsErrorCode JsConvertValueToString(JsValue value, out JsValue stringValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsConvertValueToString(value, out stringValue);
            if (IsWindows && Is64) return Native64.JsConvertValueToString(value, out stringValue);
            if (IsWindows && IsArm) return NativeArm.JsConvertValueToString(value, out stringValue);
            if (IsLinux && Is64) return Native64Linux.JsConvertValueToString(value, out stringValue);
            if (IsMac && Is64) return Native64Mac.JsConvertValueToString(value, out stringValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsConvertValueToString(value, out stringValue)
                : Native64.JsConvertValueToString(value, out stringValue);
#endif
        }

        public static JsErrorCode JsGetGlobalObject(out JsValue globalObject)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetGlobalObject(out globalObject);
            if (IsWindows && Is64) return Native64.JsGetGlobalObject(out globalObject);
            if (IsWindows && IsArm) return NativeArm.JsGetGlobalObject(out globalObject);
            if (IsLinux && Is64) return Native64Linux.JsGetGlobalObject(out globalObject);
            if (IsMac && Is64) return Native64Mac.JsGetGlobalObject(out globalObject);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetGlobalObject(out globalObject)
                : Native64.JsGetGlobalObject(out globalObject);
#endif
        }

        public static JsErrorCode JsCreateObject(out JsValue obj)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateObject(out obj);
            if (IsWindows && Is64) return Native64.JsCreateObject(out obj);
            if (IsWindows && IsArm) return NativeArm.JsCreateObject(out obj);
            if (IsLinux && Is64) return Native64Linux.JsCreateObject(out obj);
            if (IsMac && Is64) return Native64Mac.JsCreateObject(out obj);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateObject(out obj)
                : Native64.JsCreateObject(out obj);
#endif
        }

        public static JsErrorCode
            JsCreateExternalObject(IntPtr data, JsObjectFinalizeCallback finalizeCallback, out JsValue obj)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateExternalObject(data, finalizeCallback, out obj);
            if (IsWindows && Is64) return Native64.JsCreateExternalObject(data, finalizeCallback, out obj);
            if (IsWindows && IsArm) return NativeArm.JsCreateExternalObject(data, finalizeCallback, out obj);
            if (IsLinux && Is64) return Native64Linux.JsCreateExternalObject(data, finalizeCallback, out obj);
            if (IsMac && Is64) return Native64Mac.JsCreateExternalObject(data, finalizeCallback, out obj);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateExternalObject(data, finalizeCallback, out obj)
                : Native64.JsCreateExternalObject(data, finalizeCallback, out obj);
#endif
        }

        public static JsErrorCode JsConvertValueToObject(JsValue value, out JsValue obj)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsConvertValueToObject(value, out obj);
            if (IsWindows && Is64) return Native64.JsConvertValueToObject(value, out obj);
            if (IsWindows && IsArm) return NativeArm.JsConvertValueToObject(value, out obj);
            if (IsLinux && Is64) return Native64Linux.JsConvertValueToObject(value, out obj);
            if (IsMac && Is64) return Native64Mac.JsConvertValueToObject(value, out obj);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsConvertValueToObject(value, out obj)
                : Native64.JsConvertValueToObject(value, out obj);
#endif
        }

        public static JsErrorCode JsGetPrototype(JsValue obj, out JsValue prototypeObject)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetPrototype(obj, out prototypeObject);
            if (IsWindows && Is64) return Native64.JsGetPrototype(obj, out prototypeObject);
            if (IsWindows && IsArm) return NativeArm.JsGetPrototype(obj, out prototypeObject);
            if (IsLinux && Is64) return Native64Linux.JsGetPrototype(obj, out prototypeObject);
            if (IsMac && Is64) return Native64Mac.JsGetPrototype(obj, out prototypeObject);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetPrototype(obj, out prototypeObject)
                : Native64.JsGetPrototype(obj, out prototypeObject);
#endif
        }

        public static JsErrorCode JsSetPrototype(JsValue obj, JsValue prototypeObject)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetPrototype(obj, prototypeObject);
            if (IsWindows && Is64) return Native64.JsSetPrototype(obj, prototypeObject);
            if (IsWindows && IsArm) return NativeArm.JsSetPrototype(obj, prototypeObject);
            if (IsLinux && Is64) return Native64Linux.JsSetPrototype(obj, prototypeObject);
            if (IsMac && Is64) return Native64Mac.JsSetPrototype(obj, prototypeObject);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetPrototype(obj, prototypeObject)
                : Native64.JsSetPrototype(obj, prototypeObject);
#endif
        }

        public static JsErrorCode JsGetExtensionAllowed(JsValue obj, out bool value)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetExtensionAllowed(obj, out value);
            if (IsWindows && Is64) return Native64.JsGetExtensionAllowed(obj, out value);
            if (IsWindows && IsArm) return NativeArm.JsGetExtensionAllowed(obj, out value);
            if (IsLinux && Is64) return Native64Linux.JsGetExtensionAllowed(obj, out value);
            if (IsMac && Is64) return Native64Mac.JsGetExtensionAllowed(obj, out value);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetExtensionAllowed(obj, out value)
                : Native64.JsGetExtensionAllowed(obj, out value);
#endif
        }

        public static JsErrorCode JsPreventExtension(JsValue obj)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsPreventExtension(obj);
            if (IsWindows && Is64) return Native64.JsPreventExtension(obj);
            if (IsWindows && IsArm) return NativeArm.JsPreventExtension(obj);
            if (IsLinux && Is64) return Native64Linux.JsPreventExtension(obj);
            if (IsMac && Is64) return Native64Mac.JsPreventExtension(obj);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsPreventExtension(obj)
                : Native64.JsPreventExtension(obj);
#endif
        }

        public static JsErrorCode JsGetProperty(JsValue obj, JsPropertyId propertyId, out JsValue value)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetProperty(obj, propertyId, out value);
            if (IsWindows && Is64) return Native64.JsGetProperty(obj, propertyId, out value);
            if (IsWindows && IsArm) return NativeArm.JsGetProperty(obj, propertyId, out value);
            if (IsLinux && Is64) return Native64Linux.JsGetProperty(obj, propertyId, out value);
            if (IsMac && Is64) return Native64Mac.JsGetProperty(obj, propertyId, out value);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetProperty(obj, propertyId, out value)
                : Native64.JsGetProperty(obj, propertyId, out value);
#endif
        }

        public static JsErrorCode JsGetOwnPropertyDescriptor(JsValue obj,
            JsPropertyId propertyId,
            out JsValue propertyDescriptor)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetOwnPropertyDescriptor(obj, propertyId, out propertyDescriptor);
            if (IsWindows && Is64) return Native64.JsGetOwnPropertyDescriptor(obj, propertyId, out propertyDescriptor);
            if (IsWindows && IsArm) return NativeArm.JsGetOwnPropertyDescriptor(obj, propertyId, out propertyDescriptor);
            if (IsLinux && Is64) return Native64Linux.JsGetOwnPropertyDescriptor(obj, propertyId, out propertyDescriptor);
            if (IsMac && Is64) return Native64Mac.JsGetOwnPropertyDescriptor(obj, propertyId, out propertyDescriptor);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetOwnPropertyDescriptor(obj, propertyId, out propertyDescriptor)
                : Native64.JsGetOwnPropertyDescriptor(obj, propertyId, out propertyDescriptor);
#endif
        }

        public static JsErrorCode JsGetOwnPropertyNames(JsValue obj, out JsValue propertyNames)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetOwnPropertyNames(obj, out propertyNames);
            if (IsWindows && Is64) return Native64.JsGetOwnPropertyNames(obj, out propertyNames);
            if (IsWindows && IsArm) return NativeArm.JsGetOwnPropertyNames(obj, out propertyNames);
            if (IsLinux && Is64) return Native64Linux.JsGetOwnPropertyNames(obj, out propertyNames);
            if (IsMac && Is64) return Native64Mac.JsGetOwnPropertyNames(obj, out propertyNames);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetOwnPropertyNames(obj, out propertyNames)
                : Native64.JsGetOwnPropertyNames(obj, out propertyNames);
#endif
        }

        public static JsErrorCode
            JsSetProperty(JsValue obj, JsPropertyId propertyId, JsValue value, bool useStrictRules)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetProperty(obj, propertyId, value, useStrictRules);
            if (IsWindows && Is64) return Native64.JsSetProperty(obj, propertyId, value, useStrictRules);
            if (IsWindows && IsArm) return NativeArm.JsSetProperty(obj, propertyId, value, useStrictRules);
            if (IsLinux && Is64) return Native64Linux.JsSetProperty(obj, propertyId, value, useStrictRules);
            if (IsMac && Is64) return Native64Mac.JsSetProperty(obj, propertyId, value, useStrictRules);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetProperty(obj, propertyId, value, useStrictRules)
                : Native64.JsSetProperty(obj, propertyId, value, useStrictRules);
#endif
        }

        public static JsErrorCode JsHasProperty(JsValue obj, JsPropertyId propertyId, out bool hasProperty)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsHasProperty(obj, propertyId, out hasProperty);
            if (IsWindows && Is64) return Native64.JsHasProperty(obj, propertyId, out hasProperty);
            if (IsWindows && IsArm) return NativeArm.JsHasProperty(obj, propertyId, out hasProperty);
            if (IsLinux && Is64) return Native64Linux.JsHasProperty(obj, propertyId, out hasProperty);
            if (IsMac && Is64) return Native64Mac.JsHasProperty(obj, propertyId, out hasProperty);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsHasProperty(obj, propertyId, out hasProperty)
                : Native64.JsHasProperty(obj, propertyId, out hasProperty);
#endif
        }

        public static JsErrorCode JsDeleteProperty(JsValue obj,
            JsPropertyId propertyId,
            bool useStrictRules,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDeleteProperty(obj, propertyId, useStrictRules, out result);
            if (IsWindows && Is64) return Native64.JsDeleteProperty(obj, propertyId, useStrictRules, out result);
            if (IsWindows && IsArm) return NativeArm.JsDeleteProperty(obj, propertyId, useStrictRules, out result);
            if (IsLinux && Is64) return Native64Linux.JsDeleteProperty(obj, propertyId, useStrictRules, out result);
            if (IsMac && Is64) return Native64Mac.JsDeleteProperty(obj, propertyId, useStrictRules, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDeleteProperty(obj, propertyId, useStrictRules, out result)
                : Native64.JsDeleteProperty(obj, propertyId, useStrictRules, out result);
#endif
        }

        public static JsErrorCode JsDefineProperty(JsValue obj,
            JsPropertyId propertyId,
            JsValue propertyDescriptor,
            out bool result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDefineProperty(obj, propertyId, propertyDescriptor, out result);
            if (IsWindows && Is64) return Native64.JsDefineProperty(obj, propertyId, propertyDescriptor, out result);
            if (IsWindows && IsArm) return NativeArm.JsDefineProperty(obj, propertyId, propertyDescriptor, out result);
            if (IsLinux && Is64) return Native64Linux.JsDefineProperty(obj, propertyId, propertyDescriptor, out result);
            if (IsMac && Is64) return Native64Mac.JsDefineProperty(obj, propertyId, propertyDescriptor, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDefineProperty(obj, propertyId, propertyDescriptor, out result)
                : Native64.JsDefineProperty(obj, propertyId, propertyDescriptor, out result);
#endif
        }

        public static JsErrorCode JsHasIndexedProperty(JsValue obj, JsValue index, out bool result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsHasIndexedProperty(obj, index, out result);
            if (IsWindows && Is64) return Native64.JsHasIndexedProperty(obj, index, out result);
            if (IsWindows && IsArm) return NativeArm.JsHasIndexedProperty(obj, index, out result);
            if (IsLinux && Is64) return Native64Linux.JsHasIndexedProperty(obj, index, out result);
            if (IsMac && Is64) return Native64Mac.JsHasIndexedProperty(obj, index, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsHasIndexedProperty(obj, index, out result)
                : Native64.JsHasIndexedProperty(obj, index, out result);
#endif
        }

        public static JsErrorCode JsGetIndexedProperty(JsValue obj, JsValue index, out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetIndexedProperty(obj, index, out result);
            if (IsWindows && Is64) return Native64.JsGetIndexedProperty(obj, index, out result);
            if (IsWindows && IsArm) return NativeArm.JsGetIndexedProperty(obj, index, out result);
            if (IsLinux && Is64) return Native64Linux.JsGetIndexedProperty(obj, index, out result);
            if (IsMac && Is64) return Native64Mac.JsGetIndexedProperty(obj, index, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetIndexedProperty(obj, index, out result)
                : Native64.JsGetIndexedProperty(obj, index, out result);
#endif
        }

        public static JsErrorCode JsSetIndexedProperty(JsValue obj, JsValue index, JsValue value)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetIndexedProperty(obj, index, value);
            if (IsWindows && Is64) return Native64.JsSetIndexedProperty(obj, index, value);
            if (IsWindows && IsArm) return NativeArm.JsSetIndexedProperty(obj, index, value);
            if (IsLinux && Is64) return Native64Linux.JsSetIndexedProperty(obj, index, value);
            if (IsMac && Is64) return Native64Mac.JsSetIndexedProperty(obj, index, value);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetIndexedProperty(obj, index, value)
                : Native64.JsSetIndexedProperty(obj, index, value);
#endif
        }

        public static JsErrorCode JsDeleteIndexedProperty(JsValue obj, JsValue index)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDeleteIndexedProperty(obj, index);
            if (IsWindows && Is64) return Native64.JsDeleteIndexedProperty(obj, index);
            if (IsWindows && IsArm) return NativeArm.JsDeleteIndexedProperty(obj, index);
            if (IsLinux && Is64) return Native64Linux.JsDeleteIndexedProperty(obj, index);
            if (IsMac && Is64) return Native64Mac.JsDeleteIndexedProperty(obj, index);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDeleteIndexedProperty(obj, index)
                : Native64.JsDeleteIndexedProperty(obj, index);
#endif
        }

        public static JsErrorCode JsEquals(JsValue obj1, JsValue obj2, out bool result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsEquals(obj1, obj2, out result);
            if (IsWindows && Is64) return Native64.JsEquals(obj1, obj2, out result);
            if (IsWindows && IsArm) return NativeArm.JsEquals(obj1, obj2, out result);
            if (IsLinux && Is64) return Native64Linux.JsEquals(obj1, obj2, out result);
            if (IsMac && Is64) return Native64Mac.JsEquals(obj1, obj2, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsEquals(obj1, obj2, out result)
                : Native64.JsEquals(obj1, obj2, out result);
#endif
        }

        public static JsErrorCode JsStrictEquals(JsValue obj1, JsValue obj2, out bool result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsStrictEquals(obj1, obj2, out result);
            if (IsWindows && Is64) return Native64.JsStrictEquals(obj1, obj2, out result);
            if (IsWindows && IsArm) return NativeArm.JsStrictEquals(obj1, obj2, out result);
            if (IsLinux && Is64) return Native64Linux.JsStrictEquals(obj1, obj2, out result);
            if (IsMac && Is64) return Native64Mac.JsStrictEquals(obj1, obj2, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsStrictEquals(obj1, obj2, out result)
                : Native64.JsStrictEquals(obj1, obj2, out result);
#endif
        }

        public static JsErrorCode JsHasExternalData(JsValue obj, out bool value)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsHasExternalData(obj, out value);
            if (IsWindows && Is64) return Native64.JsHasExternalData(obj, out value);
            if (IsWindows && IsArm) return NativeArm.JsHasExternalData(obj, out value);
            if (IsLinux && Is64) return Native64Linux.JsHasExternalData(obj, out value);
            if (IsMac && Is64) return Native64Mac.JsHasExternalData(obj, out value);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsHasExternalData(obj, out value)
                : Native64.JsHasExternalData(obj, out value);
#endif
        }

        public static JsErrorCode JsGetExternalData(JsValue obj, out IntPtr externalData)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetExternalData(obj, out externalData);
            if (IsWindows && Is64) return Native64.JsGetExternalData(obj, out externalData);
            if (IsWindows && IsArm) return NativeArm.JsGetExternalData(obj, out externalData);
            if (IsLinux && Is64) return Native64Linux.JsGetExternalData(obj, out externalData);
            if (IsMac && Is64) return Native64Mac.JsGetExternalData(obj, out externalData);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetExternalData(obj, out externalData)
                : Native64.JsGetExternalData(obj, out externalData);
#endif
        }

        public static JsErrorCode JsSetExternalData(JsValue obj, IntPtr externalData)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetExternalData(obj, externalData);
            if (IsWindows && Is64) return Native64.JsSetExternalData(obj, externalData);
            if (IsWindows && IsArm) return NativeArm.JsSetExternalData(obj, externalData);
            if (IsLinux && Is64) return Native64Linux.JsSetExternalData(obj, externalData);
            if (IsMac && Is64) return Native64Mac.JsSetExternalData(obj, externalData);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetExternalData(obj, externalData)
                : Native64.JsSetExternalData(obj, externalData);
#endif
        }

        public static JsErrorCode JsCreateArray(uint length, out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateArray(length, out result);
            if (IsWindows && Is64) return Native64.JsCreateArray(length, out result);
            if (IsWindows && IsArm) return NativeArm.JsCreateArray(length, out result);
            if (IsLinux && Is64) return Native64Linux.JsCreateArray(length, out result);
            if (IsMac && Is64) return Native64Mac.JsCreateArray(length, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateArray(length, out result)
                : Native64.JsCreateArray(length, out result);
#endif
        }

        public static JsErrorCode JsCreatePromise(out JsValue promise, out JsValue resolveFunc, out JsValue rejectFunc)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreatePromise(out promise, out resolveFunc, out rejectFunc);
            if (IsWindows && Is64) return Native64.JsCreatePromise(out promise, out resolveFunc, out rejectFunc);
            if (IsWindows && IsArm) return NativeArm.JsCreatePromise(out promise, out resolveFunc, out rejectFunc);
            if (IsLinux && Is64) return Native64Linux.JsCreatePromise(out promise, out resolveFunc, out rejectFunc);
            if (IsMac && Is64) return Native64Mac.JsCreatePromise(out promise, out resolveFunc, out rejectFunc);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreatePromise(out promise, out resolveFunc, out rejectFunc)
                : Native64.JsCreatePromise(out promise, out resolveFunc, out rejectFunc);
#endif
        }

        public static JsErrorCode JsCallFunction(JsValue function,
            JsValue[] arguments,
            ushort argumentCount,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCallFunction(function, arguments, argumentCount, out result);
            if (IsWindows && Is64) return Native64.JsCallFunction(function, arguments, argumentCount, out result);
            if (IsWindows && IsArm) return NativeArm.JsCallFunction(function, arguments, argumentCount, out result);
            if (IsLinux && Is64) return Native64Linux.JsCallFunction(function, arguments, argumentCount, out result);
            if (IsMac && Is64) return Native64Mac.JsCallFunction(function, arguments, argumentCount, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCallFunction(function, arguments, argumentCount, out result)
                : Native64.JsCallFunction(function, arguments, argumentCount, out result);
#endif
        }

        public static JsErrorCode JsConstructObject(JsValue function,
            JsValue[] arguments,
            ushort argumentCount,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsConstructObject(function, arguments, argumentCount, out result);
            if (IsWindows && Is64) return Native64.JsConstructObject(function, arguments, argumentCount, out result);
            if (IsWindows && IsArm) return NativeArm.JsConstructObject(function, arguments, argumentCount, out result);
            if (IsLinux && Is64) return Native64Linux.JsConstructObject(function, arguments, argumentCount, out result);
            if (IsMac && Is64) return Native64Mac.JsConstructObject(function, arguments, argumentCount, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsConstructObject(function, arguments, argumentCount, out result)
                : Native64.JsConstructObject(function, arguments, argumentCount, out result);
#endif
        }

        public static JsErrorCode JsCreateFunction(JsNativeFunction nativeFunction, IntPtr externalData,
            out JsValue function)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateFunction(nativeFunction, externalData, out function);
            if (IsWindows && Is64) return Native64.JsCreateFunction(nativeFunction, externalData, out function);
            if (IsWindows && IsArm) return NativeArm.JsCreateFunction(nativeFunction, externalData, out function);
            if (IsLinux && Is64) return Native64Linux.JsCreateFunction(nativeFunction, externalData, out function);
            if (IsMac && Is64) return Native64Mac.JsCreateFunction(nativeFunction, externalData, out function);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateFunction(nativeFunction, externalData, out function)
                : Native64.JsCreateFunction(nativeFunction, externalData, out function);
#endif
        }

        public static JsErrorCode JsCreateError(JsValue message, out JsValue error)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateError(message, out error);
            if (IsWindows && Is64) return Native64.JsCreateError(message, out error);
            if (IsWindows && IsArm) return NativeArm.JsCreateError(message, out error);
            if (IsLinux && Is64) return Native64Linux.JsCreateError(message, out error);
            if (IsMac && Is64) return Native64Mac.JsCreateError(message, out error);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateError(message, out error)
                : Native64.JsCreateError(message, out error);
#endif
        }

        public static JsErrorCode JsCreateRangeError(JsValue message, out JsValue error)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateRangeError(message, out error);
            if (IsWindows && Is64) return Native64.JsCreateRangeError(message, out error);
            if (IsWindows && IsArm) return NativeArm.JsCreateRangeError(message, out error);
            if (IsLinux && Is64) return Native64Linux.JsCreateRangeError(message, out error);
            if (IsMac && Is64) return Native64Mac.JsCreateRangeError(message, out error);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateRangeError(message, out error)
                : Native64.JsCreateRangeError(message, out error);
#endif
        }

        public static JsErrorCode JsCreateReferenceError(JsValue message, out JsValue error)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateReferenceError(message, out error);
            if (IsWindows && Is64) return Native64.JsCreateReferenceError(message, out error);
            if (IsWindows && IsArm) return NativeArm.JsCreateReferenceError(message, out error);
            if (IsLinux && Is64) return Native64Linux.JsCreateReferenceError(message, out error);
            if (IsMac && Is64) return Native64Mac.JsCreateReferenceError(message, out error);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateReferenceError(message, out error)
                : Native64.JsCreateReferenceError(message, out error);
#endif
        }

        public static JsErrorCode JsCreateSyntaxError(JsValue message, out JsValue error)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateSyntaxError(message, out error);
            if (IsWindows && Is64) return Native64.JsCreateSyntaxError(message, out error);
            if (IsWindows && IsArm) return NativeArm.JsCreateSyntaxError(message, out error);
            if (IsLinux && Is64) return Native64Linux.JsCreateSyntaxError(message, out error);
            if (IsMac && Is64) return Native64Mac.JsCreateSyntaxError(message, out error);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateSyntaxError(message, out error)
                : Native64.JsCreateSyntaxError(message, out error);
#endif
        }

        public static JsErrorCode JsCreateTypeError(JsValue message, out JsValue error)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateTypeError(message, out error);
            if (IsWindows && Is64) return Native64.JsCreateTypeError(message, out error);
            if (IsWindows && IsArm) return NativeArm.JsCreateTypeError(message, out error);
            if (IsLinux && Is64) return Native64Linux.JsCreateTypeError(message, out error);
            if (IsMac && Is64) return Native64Mac.JsCreateTypeError(message, out error);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateTypeError(message, out error)
                : Native64.JsCreateTypeError(message, out error);
#endif
        }

        public static JsErrorCode JsCreateUriError(JsValue message, out JsValue error)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateURIError(message, out error);
            if (IsWindows && Is64) return Native64.JsCreateURIError(message, out error);
            if (IsWindows && IsArm) return NativeArm.JsCreateURIError(message, out error);
            if (IsLinux && Is64) return Native64Linux.JsCreateURIError(message, out error);
            if (IsMac && Is64) return Native64Mac.JsCreateURIError(message, out error);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateURIError(message, out error)
                : Native64.JsCreateURIError(message, out error);
#endif
        }

        public static JsErrorCode JsHasException(out bool hasException)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsHasException(out hasException);
            if (IsWindows && Is64) return Native64.JsHasException(out hasException);
            if (IsWindows && IsArm) return NativeArm.JsHasException(out hasException);
            if (IsLinux && Is64) return Native64Linux.JsHasException(out hasException);
            if (IsMac && Is64) return Native64Mac.JsHasException(out hasException);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsHasException(out hasException)
                : Native64.JsHasException(out hasException);
#endif
        }

        public static JsErrorCode JsGetAndClearException(out JsValue exception)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetAndClearException(out exception);
            if (IsWindows && Is64) return Native64.JsGetAndClearException(out exception);
            if (IsWindows && IsArm) return NativeArm.JsGetAndClearException(out exception);
            if (IsLinux && Is64) return Native64Linux.JsGetAndClearException(out exception);
            if (IsMac && Is64) return Native64Mac.JsGetAndClearException(out exception);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetAndClearException(out exception)
                : Native64.JsGetAndClearException(out exception);
#endif
        }

        public static JsErrorCode JsSetException(JsValue exception)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetException(exception);
            if (IsWindows && Is64) return Native64.JsSetException(exception);
            if (IsWindows && IsArm) return NativeArm.JsSetException(exception);
            if (IsLinux && Is64) return Native64Linux.JsSetException(exception);
            if (IsMac && Is64) return Native64Mac.JsSetException(exception);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetException(exception)
                : Native64.JsSetException(exception);
#endif
        }

        public static JsErrorCode JsDisableRuntimeExecution(JsRuntime runtime)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDisableRuntimeExecution(runtime);
            if (IsWindows && Is64) return Native64.JsDisableRuntimeExecution(runtime);
            if (IsWindows && IsArm) return NativeArm.JsDisableRuntimeExecution(runtime);
            if (IsLinux && Is64) return Native64Linux.JsDisableRuntimeExecution(runtime);
            if (IsMac && Is64) return Native64Mac.JsDisableRuntimeExecution(runtime);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDisableRuntimeExecution(runtime)
                : Native64.JsDisableRuntimeExecution(runtime);
#endif
        }

        public static JsErrorCode JsEnableRuntimeExecution(JsRuntime runtime)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsEnableRuntimeExecution(runtime);
            if (IsWindows && Is64) return Native64.JsEnableRuntimeExecution(runtime);
            if (IsWindows && IsArm) return NativeArm.JsEnableRuntimeExecution(runtime);
            if (IsLinux && Is64) return Native64Linux.JsEnableRuntimeExecution(runtime);
            if (IsMac && Is64) return Native64Mac.JsEnableRuntimeExecution(runtime);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsEnableRuntimeExecution(runtime)
                : Native64.JsEnableRuntimeExecution(runtime);
#endif
        }

        public static JsErrorCode JsIsRuntimeExecutionDisabled(JsRuntime runtime, out bool isDisabled)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsIsRuntimeExecutionDisabled(runtime, out isDisabled);
            if (IsWindows && Is64) return Native64.JsIsRuntimeExecutionDisabled(runtime, out isDisabled);
            if (IsWindows && IsArm) return NativeArm.JsIsRuntimeExecutionDisabled(runtime, out isDisabled);
            if (IsLinux && Is64) return Native64Linux.JsIsRuntimeExecutionDisabled(runtime, out isDisabled);
            if (IsMac && Is64) return Native64Mac.JsIsRuntimeExecutionDisabled(runtime, out isDisabled);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsIsRuntimeExecutionDisabled(runtime, out isDisabled)
                : Native64.JsIsRuntimeExecutionDisabled(runtime, out isDisabled);
#endif
        }

        public static JsErrorCode JsSetObjectBeforeCollectCallback(JsValue reference,
            IntPtr callbackState,
            JsObjectBeforeCollectCallback beforeCollectCallback)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetObjectBeforeCollectCallback(reference, callbackState, beforeCollectCallback);
            if (IsWindows && Is64) return Native64.JsSetObjectBeforeCollectCallback(reference, callbackState, beforeCollectCallback);
            if (IsWindows && IsArm) return NativeArm.JsSetObjectBeforeCollectCallback(reference, callbackState, beforeCollectCallback);
            if (IsLinux && Is64) return Native64Linux.JsSetObjectBeforeCollectCallback(reference, callbackState, beforeCollectCallback);
            if (IsMac && Is64) return Native64Mac.JsSetObjectBeforeCollectCallback(reference, callbackState, beforeCollectCallback);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetObjectBeforeCollectCallback(reference, callbackState, beforeCollectCallback)
                : Native64.JsSetObjectBeforeCollectCallback(reference, callbackState, beforeCollectCallback);
#endif
        }

        public static JsErrorCode JsCreateNamedFunction(JsValue name,
            JsNativeFunction nativeFunction,
            IntPtr callbackState,
            out JsValue function)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateNamedFunction(name, nativeFunction, callbackState, out function);
            if (IsWindows && Is64) return Native64.JsCreateNamedFunction(name, nativeFunction, callbackState, out function);
            if (IsWindows && IsArm) return NativeArm.JsCreateNamedFunction(name, nativeFunction, callbackState, out function);
            if (IsLinux && Is64) return Native64Linux.JsCreateNamedFunction(name, nativeFunction, callbackState, out function);
            if (IsMac && Is64) return Native64Mac.JsCreateNamedFunction(name, nativeFunction, callbackState, out function);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateNamedFunction(name, nativeFunction, callbackState, out function)
                : Native64.JsCreateNamedFunction(name, nativeFunction, callbackState, out function);
#endif
        }

        public static JsErrorCode JsSetPromiseContinuationCallback(
            JsPromiseContinuationCallback promiseContinuationCallback,
            IntPtr callbackState)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetPromiseContinuationCallback(promiseContinuationCallback, callbackState);
            if (IsWindows && Is64) return Native64.JsSetPromiseContinuationCallback(promiseContinuationCallback, callbackState);
            if (IsWindows && IsArm) return NativeArm.JsSetPromiseContinuationCallback(promiseContinuationCallback, callbackState);
            if (IsLinux && Is64) return Native64Linux.JsSetPromiseContinuationCallback(promiseContinuationCallback, callbackState);
            if (IsMac && Is64) return Native64Mac.JsSetPromiseContinuationCallback(promiseContinuationCallback, callbackState);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetPromiseContinuationCallback(promiseContinuationCallback, callbackState)
                : Native64.JsSetPromiseContinuationCallback(promiseContinuationCallback, callbackState);
#endif
        }

        public static JsErrorCode JsCreateArrayBuffer(uint byteLength, out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateArrayBuffer(byteLength, out result);
            if (IsWindows && Is64) return Native64.JsCreateArrayBuffer(byteLength, out result);
            if (IsWindows && IsArm) return NativeArm.JsCreateArrayBuffer(byteLength, out result);
            if (IsLinux && Is64) return Native64Linux.JsCreateArrayBuffer(byteLength, out result);
            if (IsMac && Is64) return Native64Mac.JsCreateArrayBuffer(byteLength, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateArrayBuffer(byteLength, out result)
                : Native64.JsCreateArrayBuffer(byteLength, out result);
#endif
        }

        public static JsErrorCode JsCreateTypedArray(JavaScriptTypedArrayType arrayType,
            JsValue arrayBuffer,
            uint byteOffset,
            uint elementLength,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateTypedArray(arrayType, arrayBuffer, byteOffset, elementLength, out result);
            if (IsWindows && Is64) return Native64.JsCreateTypedArray(arrayType, arrayBuffer, byteOffset, elementLength, out result);
            if (IsWindows && IsArm) return NativeArm.JsCreateTypedArray(arrayType, arrayBuffer, byteOffset, elementLength, out result);
            if (IsLinux && Is64) return Native64Linux.JsCreateTypedArray(arrayType, arrayBuffer, byteOffset, elementLength, out result);
            if (IsMac && Is64) return Native64Mac.JsCreateTypedArray(arrayType, arrayBuffer, byteOffset, elementLength, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateTypedArray(arrayType, arrayBuffer, byteOffset, elementLength, out result)
                : Native64.JsCreateTypedArray(arrayType, arrayBuffer, byteOffset, elementLength, out result);
#endif
        }

        public static JsErrorCode JsCreateDataView(JsValue arrayBuffer,
            uint byteOffset,
            uint byteOffsetLength,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateDataView(arrayBuffer, byteOffset, byteOffsetLength, out result);
            if (IsWindows && Is64) return Native64.JsCreateDataView(arrayBuffer, byteOffset, byteOffsetLength, out result);
            if (IsWindows && IsArm) return NativeArm.JsCreateDataView(arrayBuffer, byteOffset, byteOffsetLength, out result);
            if (IsLinux && Is64) return Native64Linux.JsCreateDataView(arrayBuffer, byteOffset, byteOffsetLength, out result);
            if (IsMac && Is64) return Native64Mac.JsCreateDataView(arrayBuffer, byteOffset, byteOffsetLength, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateDataView(arrayBuffer, byteOffset, byteOffsetLength, out result)
                : Native64.JsCreateDataView(arrayBuffer, byteOffset, byteOffsetLength, out result);
#endif
        }

        public static JsErrorCode JsGetArrayBufferStorage(JsValue arrayBuffer, out IntPtr buffer,
            out uint bufferLength)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetArrayBufferStorage(arrayBuffer, out buffer, out bufferLength);
            if (IsWindows && Is64) return Native64.JsGetArrayBufferStorage(arrayBuffer, out buffer, out bufferLength);
            if (IsWindows && IsArm) return NativeArm.JsGetArrayBufferStorage(arrayBuffer, out buffer, out bufferLength);
            if (IsLinux && Is64) return Native64Linux.JsGetArrayBufferStorage(arrayBuffer, out buffer, out bufferLength);
            if (IsMac && Is64) return Native64Mac.JsGetArrayBufferStorage(arrayBuffer, out buffer, out bufferLength);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetArrayBufferStorage(arrayBuffer, out buffer, out bufferLength)
                : Native64.JsGetArrayBufferStorage(arrayBuffer, out buffer, out bufferLength);
#endif
        }

        public static JsErrorCode JsGetTypedArrayStorage(JsValue typedArray,
            out IntPtr buffer,
            out uint bufferLength,
            out JavaScriptTypedArrayType arrayType,
            out int elementSize)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetTypedArrayStorage(typedArray, out buffer, out bufferLength, out arrayType, out elementSize);
            if (IsWindows && Is64) return Native64.JsGetTypedArrayStorage(typedArray, out buffer, out bufferLength, out arrayType, out elementSize);
            if (IsWindows && IsArm) return NativeArm.JsGetTypedArrayStorage(typedArray, out buffer, out bufferLength, out arrayType, out elementSize);
            if (IsLinux && Is64) return Native64Linux.JsGetTypedArrayStorage(typedArray, out buffer, out bufferLength, out arrayType, out elementSize);
            if (IsMac && Is64) return Native64Mac.JsGetTypedArrayStorage(typedArray, out buffer, out bufferLength, out arrayType, out elementSize);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetTypedArrayStorage(typedArray, out buffer, out bufferLength, out arrayType,
                    out elementSize)
                : Native64.JsGetTypedArrayStorage(typedArray, out buffer, out bufferLength, out arrayType,
                    out elementSize);
#endif
        }

        public static JsErrorCode JsGetDataViewStorage(JsValue dataView, out IntPtr buffer, out uint bufferLength)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetDataViewStorage(dataView, out buffer, out bufferLength);
            if (IsWindows && Is64) return Native64.JsGetDataViewStorage(dataView, out buffer, out bufferLength);
            if (IsWindows && IsArm) return NativeArm.JsGetDataViewStorage(dataView, out buffer, out bufferLength);
            if (IsLinux && Is64) return Native64Linux.JsGetDataViewStorage(dataView, out buffer, out bufferLength);
            if (IsMac && Is64) return Native64Mac.JsGetDataViewStorage(dataView, out buffer, out bufferLength);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetDataViewStorage(dataView, out buffer, out bufferLength)
                : Native64.JsGetDataViewStorage(dataView, out buffer, out bufferLength);
#endif
        }

        public static JsErrorCode JsGetPropertyIdType(JsPropertyId propertyId, out JsPropertyIdType propertyIdType)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetPropertyIdType(propertyId, out propertyIdType);
            if (IsWindows && Is64) return Native64.JsGetPropertyIdType(propertyId, out propertyIdType);
            if (IsWindows && IsArm) return NativeArm.JsGetPropertyIdType(propertyId, out propertyIdType);
            if (IsLinux && Is64) return Native64Linux.JsGetPropertyIdType(propertyId, out propertyIdType);
            if (IsMac && Is64) return Native64Mac.JsGetPropertyIdType(propertyId, out propertyIdType);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetPropertyIdType(propertyId, out propertyIdType)
                : Native64.JsGetPropertyIdType(propertyId, out propertyIdType);
#endif
        }

        public static JsErrorCode JsCreateSymbol(JsValue description, out JsValue symbol)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateSymbol(description, out symbol);
            if (IsWindows && Is64) return Native64.JsCreateSymbol(description, out symbol);
            if (IsWindows && IsArm) return NativeArm.JsCreateSymbol(description, out symbol);
            if (IsLinux && Is64) return Native64Linux.JsCreateSymbol(description, out symbol);
            if (IsMac && Is64) return Native64Mac.JsCreateSymbol(description, out symbol);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateSymbol(description, out symbol)
                : Native64.JsCreateSymbol(description, out symbol);
#endif
        }

        public static JsErrorCode JsGetSymbolFromPropertyId(JsPropertyId propertyId, out JsValue symbol)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetSymbolFromPropertyId(propertyId, out symbol);
            if (IsWindows && Is64) return Native64.JsGetSymbolFromPropertyId(propertyId, out symbol);
            if (IsWindows && IsArm) return NativeArm.JsGetSymbolFromPropertyId(propertyId, out symbol);
            if (IsLinux && Is64) return Native64Linux.JsGetSymbolFromPropertyId(propertyId, out symbol);
            if (IsMac && Is64) return Native64Mac.JsGetSymbolFromPropertyId(propertyId, out symbol);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetSymbolFromPropertyId(propertyId, out symbol)
                : Native64.JsGetSymbolFromPropertyId(propertyId, out symbol);
#endif
        }

        public static JsErrorCode JsGetPropertyIdFromSymbol(JsValue symbol, out JsPropertyId propertyId)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetPropertyIdFromSymbol(symbol, out propertyId);
            if (IsWindows && Is64) return Native64.JsGetPropertyIdFromSymbol(symbol, out propertyId);
            if (IsWindows && IsArm) return NativeArm.JsGetPropertyIdFromSymbol(symbol, out propertyId);
            if (IsLinux && Is64) return Native64Linux.JsGetPropertyIdFromSymbol(symbol, out propertyId);
            if (IsMac && Is64) return Native64Mac.JsGetPropertyIdFromSymbol(symbol, out propertyId);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetPropertyIdFromSymbol(symbol, out propertyId)
                : Native64.JsGetPropertyIdFromSymbol(symbol, out propertyId);
#endif
        }

        public static JsErrorCode JsGetOwnPropertySymbols(JsValue obj, out JsValue propertySymbols)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetOwnPropertySymbols(obj, out propertySymbols);
            if (IsWindows && Is64) return Native64.JsGetOwnPropertySymbols(obj, out propertySymbols);
            if (IsWindows && IsArm) return NativeArm.JsGetOwnPropertySymbols(obj, out propertySymbols);
            if (IsLinux && Is64) return Native64Linux.JsGetOwnPropertySymbols(obj, out propertySymbols);
            if (IsMac && Is64) return Native64Mac.JsGetOwnPropertySymbols(obj, out propertySymbols);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetOwnPropertySymbols(obj, out propertySymbols)
                : Native64.JsGetOwnPropertySymbols(obj, out propertySymbols);
#endif
        }

        public static JsErrorCode JsNumberToInt(JsValue value, out int intValue)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsNumberToInt(value, out intValue);
            if (IsWindows && Is64) return Native64.JsNumberToInt(value, out intValue);
            if (IsWindows && IsArm) return NativeArm.JsNumberToInt(value, out intValue);
            if (IsLinux && Is64) return Native64Linux.JsNumberToInt(value, out intValue);
            if (IsMac && Is64) return Native64Mac.JsNumberToInt(value, out intValue);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsNumberToInt(value, out intValue)
                : Native64.JsNumberToInt(value, out intValue);
#endif
        }

        public static JsErrorCode JsSetIndexedPropertiesToExternalData(JsValue obj,
            IntPtr data,
            JavaScriptTypedArrayType arrayType,
            uint elementLength)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetIndexedPropertiesToExternalData(obj, data, arrayType, elementLength);
            if (IsWindows && Is64) return Native64.JsSetIndexedPropertiesToExternalData(obj, data, arrayType, elementLength);
            if (IsWindows && IsArm) return NativeArm.JsSetIndexedPropertiesToExternalData(obj, data, arrayType, elementLength);
            if (IsLinux && Is64) return Native64Linux.JsSetIndexedPropertiesToExternalData(obj, data, arrayType, elementLength);
            if (IsMac && Is64) return Native64Mac.JsSetIndexedPropertiesToExternalData(obj, data, arrayType, elementLength);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetIndexedPropertiesToExternalData(obj, data, arrayType, elementLength)
                : Native64.JsSetIndexedPropertiesToExternalData(obj, data, arrayType, elementLength);
#endif
        }

        public static JsErrorCode JsGetIndexedPropertiesExternalData(JsValue obj,
            IntPtr data,
            out JavaScriptTypedArrayType arrayType,
            out uint elementLength)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetIndexedPropertiesExternalData(obj, data, out arrayType, out elementLength);
            if (IsWindows && Is64) return Native64.JsGetIndexedPropertiesExternalData(obj, data, out arrayType, out elementLength);
            if (IsWindows && IsArm) return NativeArm.JsGetIndexedPropertiesExternalData(obj, data, out arrayType, out elementLength);
            if (IsLinux && Is64) return Native64Linux.JsGetIndexedPropertiesExternalData(obj, data, out arrayType, out elementLength);
            if (IsMac && Is64) return Native64Mac.JsGetIndexedPropertiesExternalData(obj, data, out arrayType, out elementLength);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetIndexedPropertiesExternalData(obj, data, out arrayType, out elementLength)
                : Native64.JsGetIndexedPropertiesExternalData(obj, data, out arrayType, out elementLength);
#endif
        }

        public static JsErrorCode JsHasIndexedPropertiesExternalData(JsValue obj, out bool value)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsHasIndexedPropertiesExternalData(obj, out value);
            if (IsWindows && Is64) return Native64.JsHasIndexedPropertiesExternalData(obj, out value);
            if (IsWindows && IsArm) return NativeArm.JsHasIndexedPropertiesExternalData(obj, out value);
            if (IsLinux && Is64) return Native64Linux.JsHasIndexedPropertiesExternalData(obj, out value);
            if (IsMac && Is64) return Native64Mac.JsHasIndexedPropertiesExternalData(obj, out value);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsHasIndexedPropertiesExternalData(obj, out value)
                : Native64.JsHasIndexedPropertiesExternalData(obj, out value);
#endif
        }

        public static JsErrorCode JsInstanceOf(JsValue obj, JsValue constructor, out bool result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsInstanceOf(obj, constructor, out result);
            if (IsWindows && Is64) return Native64.JsInstanceOf(obj, constructor, out result);
            if (IsWindows && IsArm) return NativeArm.JsInstanceOf(obj, constructor, out result);
            if (IsLinux && Is64) return Native64Linux.JsInstanceOf(obj, constructor, out result);
            if (IsMac && Is64) return Native64Mac.JsInstanceOf(obj, constructor, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsInstanceOf(obj, constructor, out result)
                : Native64.JsInstanceOf(obj, constructor, out result);
#endif
        }

        public static JsErrorCode JsCreateExternalArrayBuffer(IntPtr data,
            uint byteLength,
            JsObjectFinalizeCallback finalizeCallback,
            IntPtr callbackState,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsCreateExternalArrayBuffer(data, byteLength, finalizeCallback, callbackState, out result);
            if (IsWindows && Is64) return Native64.JsCreateExternalArrayBuffer(data, byteLength, finalizeCallback, callbackState, out result);
            if (IsWindows && IsArm) return NativeArm.JsCreateExternalArrayBuffer(data, byteLength, finalizeCallback, callbackState, out result);
            if (IsLinux && Is64) return Native64Linux.JsCreateExternalArrayBuffer(data, byteLength, finalizeCallback, callbackState, out result);
            if (IsMac && Is64) return Native64Mac.JsCreateExternalArrayBuffer(data, byteLength, finalizeCallback, callbackState, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsCreateExternalArrayBuffer(data, byteLength, finalizeCallback, callbackState, out result)
                : Native64.JsCreateExternalArrayBuffer(data, byteLength, finalizeCallback, callbackState, out result);
#endif
        }

        public static JsErrorCode JsGetTypedArrayInfo(JsValue typedArray,
            out JavaScriptTypedArrayType arrayType,
            out JsValue arrayBuffer,
            out uint byteOffset,
            out uint byteLength)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetTypedArrayInfo(typedArray, out arrayType, out arrayBuffer, out byteOffset, out byteLength);
            if (IsWindows && Is64) return Native64.JsGetTypedArrayInfo(typedArray, out arrayType, out arrayBuffer, out byteOffset, out byteLength);
            if (IsWindows && IsArm) return NativeArm.JsGetTypedArrayInfo(typedArray, out arrayType, out arrayBuffer, out byteOffset, out byteLength);
            if (IsLinux && Is64) return Native64Linux.JsGetTypedArrayInfo(typedArray, out arrayType, out arrayBuffer, out byteOffset, out byteLength);
            if (IsMac && Is64) return Native64Mac.JsGetTypedArrayInfo(typedArray, out arrayType, out arrayBuffer, out byteOffset, out byteLength);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetTypedArrayInfo(typedArray, out arrayType, out arrayBuffer, out byteOffset,
                    out byteLength)
                : Native64.JsGetTypedArrayInfo(typedArray, out arrayType, out arrayBuffer, out byteOffset,
                    out byteLength);
#endif
        }

        public static JsErrorCode JsGetContextOfObject(JsValue obj, out JsContext context)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetContextOfObject(obj, out context);
            if (IsWindows && Is64) return Native64.JsGetContextOfObject(obj, out context);
            if (IsWindows && IsArm) return NativeArm.JsGetContextOfObject(obj, out context);
            if (IsLinux && Is64) return Native64Linux.JsGetContextOfObject(obj, out context);
            if (IsMac && Is64) return Native64Mac.JsGetContextOfObject(obj, out context);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetContextOfObject(obj, out context)
                : Native64.JsGetContextOfObject(obj, out context);
#endif
        }

        public static JsErrorCode JsGetContextData(JsContext context, out IntPtr data)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsGetContextData(context, out data);
            if (IsWindows && Is64) return Native64.JsGetContextData(context, out data);
            if (IsWindows && IsArm) return NativeArm.JsGetContextData(context, out data);
            if (IsLinux && Is64) return Native64Linux.JsGetContextData(context, out data);
            if (IsMac && Is64) return Native64Mac.JsGetContextData(context, out data);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsGetContextData(context, out data)
                : Native64.JsGetContextData(context, out data);
#endif
        }

        public static JsErrorCode JsSetContextData(JsContext context, IntPtr data)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetContextData(context, data);
            if (IsWindows && Is64) return Native64.JsSetContextData(context, data);
            if (IsWindows && IsArm) return NativeArm.JsSetContextData(context, data);
            if (IsLinux && Is64) return Native64Linux.JsSetContextData(context, data);
            if (IsMac && Is64) return Native64Mac.JsSetContextData(context, data);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetContextData(context, data)
                : Native64.JsSetContextData(context, data);
#endif
        }

        public static JsErrorCode JsParseSerializedScriptWithCallback(
            JavaScriptSerializedScriptLoadSourceCallback scriptLoadCallback,
            JavaScriptSerializedScriptUnloadCallback scriptUnloadCallback,
            byte[] buffer,
            JsSourceContext sourceContext,
            string sourceUrl,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsParseSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
            if (IsWindows && Is64) return Native64.JsParseSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
            if (IsWindows && IsArm) return NativeArm.JsParseSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
            if (IsLinux && Is64) return Native64Linux.JsParseSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
            if (IsMac && Is64) return Native64Mac.JsParseSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsParseSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer,
                    sourceContext, sourceUrl, out result)
                : Native64.JsParseSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer,
                    sourceContext, sourceUrl, out result);
#endif
        }

        public static JsErrorCode JsRunSerializedScriptWithCallback(
            JavaScriptSerializedScriptLoadSourceCallback scriptLoadCallback,
            JavaScriptSerializedScriptUnloadCallback scriptUnloadCallback,
            byte[] buffer,
            JsSourceContext sourceContext,
            string sourceUrl,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsRunSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
            if (IsWindows && Is64) return Native64.JsRunSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
            if (IsWindows && IsArm) return NativeArm.JsRunSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
            if (IsLinux && Is64) return Native64Linux.JsRunSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
            if (IsMac && Is64) return Native64Mac.JsRunSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsRunSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer,
                    sourceContext, sourceUrl, out result)
                : Native64.JsRunSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer,
                    sourceContext, sourceUrl, out result);
#endif
        }

        public static JsErrorCode JsInitializeModuleRecord(
            JsModuleRecord referencingModule,
            JsValue normalizedSpecifier,
            out JsModuleRecord moduleRecord)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsInitializeModuleRecord(referencingModule, normalizedSpecifier, out moduleRecord);
            if (IsWindows && Is64) return Native64.JsInitializeModuleRecord(referencingModule, normalizedSpecifier, out moduleRecord);
            if (IsWindows && IsArm) return NativeArm.JsInitializeModuleRecord(referencingModule, normalizedSpecifier, out moduleRecord);
            if (IsLinux && Is64) return Native64Linux.JsInitializeModuleRecord(referencingModule, normalizedSpecifier, out moduleRecord);
            if (IsMac && Is64) return Native64Mac.JsInitializeModuleRecord(referencingModule, normalizedSpecifier, out moduleRecord);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsInitializeModuleRecord(referencingModule, normalizedSpecifier, out moduleRecord)
                : Native64.JsInitializeModuleRecord(referencingModule, normalizedSpecifier, out moduleRecord);
#endif
        }

        public static JsErrorCode JsSetModuleHostInfo(JsModuleRecord module, JsFetchImportedModuleCallBack callback)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleCallback, callback);
            if (IsWindows && Is64) return Native64.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleCallback, callback);
            if (IsWindows && IsArm) return NativeArm.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleCallback, callback);
            if (IsLinux && Is64) return Native64Linux.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleCallback, callback);
            if (IsMac && Is64) return Native64Mac.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleCallback, callback);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleCallback, callback)
                : Native64.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleCallback, callback);
#endif
        }

        public static JsErrorCode JsSetModuleHostInfo(JsModuleRecord module, JsNotifyModuleReadyCallback callback)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetModuleHostInfo(module, JsModuleHostInfoKind.NotifyModuleReadyCallback, callback);
            if (IsWindows && Is64) return Native64.JsSetModuleHostInfo(module, JsModuleHostInfoKind.NotifyModuleReadyCallback, callback);
            if (IsWindows && IsArm) return NativeArm.JsSetModuleHostInfo(module, JsModuleHostInfoKind.NotifyModuleReadyCallback, callback);
            if (IsLinux && Is64) return Native64Linux.JsSetModuleHostInfo(module, JsModuleHostInfoKind.NotifyModuleReadyCallback, callback);
            if (IsMac && Is64) return Native64Mac.JsSetModuleHostInfo(module, JsModuleHostInfoKind.NotifyModuleReadyCallback, callback);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetModuleHostInfo(module, JsModuleHostInfoKind.NotifyModuleReadyCallback, callback)
                : Native64.JsSetModuleHostInfo(module, JsModuleHostInfoKind.NotifyModuleReadyCallback, callback);
#endif
        }

        public static JsErrorCode JsSetModuleHostInfo(JsModuleRecord module,
            JsFetchImportedModuleFromScriptCallback callback)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, callback);
            if (IsWindows && Is64) return Native64.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, callback);
            if (IsWindows && IsArm) return NativeArm.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, callback);
            if (IsLinux && Is64) return Native64Linux.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, callback);
            if (IsMac && Is64) return Native64Mac.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, callback);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback,
                    callback)
                : Native64.JsSetModuleHostInfo(module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback,
                    callback);
#endif
        }

        public static JsErrorCode JsParseModuleSource(
            JsModuleRecord requestModule,
            JsSourceContext sourceContext,
            byte[] script,
            uint scriptLength,
            JsParseModuleSourceFlags sourceFlag,
            out JsValue exception)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsParseModuleSource(requestModule, sourceContext, script, scriptLength, sourceFlag, out exception);
            if (IsWindows && Is64) return Native64.JsParseModuleSource(requestModule, sourceContext, script, scriptLength, sourceFlag, out exception);
            if (IsWindows && IsArm) return NativeArm.JsParseModuleSource(requestModule, sourceContext, script, scriptLength, sourceFlag, out exception);
            if (IsLinux && Is64) return Native64Linux.JsParseModuleSource(requestModule, sourceContext, script, scriptLength, sourceFlag, out exception);
            if (IsMac && Is64) return Native64Mac.JsParseModuleSource(requestModule, sourceContext, script, scriptLength, sourceFlag, out exception);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsParseModuleSource(requestModule, sourceContext, script, scriptLength, sourceFlag,
                    out exception)
                : Native64.JsParseModuleSource(requestModule, sourceContext, script, scriptLength, sourceFlag,
                    out exception);
#endif
        }

        public static JsErrorCode JsModuleEvaluation(
            JsModuleRecord requestModule,
            out JsValue result)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsModuleEvaluation(requestModule, out result);
            if (IsWindows && Is64) return Native64.JsModuleEvaluation(requestModule, out result);
            if (IsWindows && IsArm) return NativeArm.JsModuleEvaluation(requestModule, out result);
            if (IsLinux && Is64) return Native64Linux.JsModuleEvaluation(requestModule, out result);
            if (IsMac && Is64) return Native64Mac.JsModuleEvaluation(requestModule, out result);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsModuleEvaluation(requestModule, out result)
                : Native64.JsModuleEvaluation(requestModule, out result);
#endif
        }

        public static JsErrorCode JsDiagStartDebugging(
            JsRuntime runtime,
            JsDiagDebugEventCallback debugEventCallback,
            IntPtr callbackState)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDiagStartDebugging(runtime, debugEventCallback, callbackState);
            if (IsWindows && Is64) return Native64.JsDiagStartDebugging(runtime, debugEventCallback, callbackState);
            if (IsWindows && IsArm) return NativeArm.JsDiagStartDebugging(runtime, debugEventCallback, callbackState);
            if (IsLinux && Is64) return Native64Linux.JsDiagStartDebugging(runtime, debugEventCallback, callbackState);
            if (IsMac && Is64) return Native64Mac.JsDiagStartDebugging(runtime, debugEventCallback, callbackState);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDiagStartDebugging(runtime, debugEventCallback, callbackState)
                : Native64.JsDiagStartDebugging(runtime, debugEventCallback, callbackState);
#endif
        }

        public static JsErrorCode JsDiagStopDebugging(JsRuntime runtime, out IntPtr callbackState)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDiagStopDebugging(runtime, out callbackState);
            if (IsWindows && Is64) return Native64.JsDiagStopDebugging(runtime, out callbackState);
            if (IsWindows && IsArm) return NativeArm.JsDiagStopDebugging(runtime, out callbackState);
            if (IsLinux && Is64) return Native64Linux.JsDiagStopDebugging(runtime, out callbackState);
            if (IsMac && Is64) return Native64Mac.JsDiagStopDebugging(runtime, out callbackState);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDiagStopDebugging(runtime, out callbackState)
                : Native64.JsDiagStopDebugging(runtime, out callbackState);
#endif
        }

        public static JsErrorCode SetBreakpoint(uint scriptId, uint lineNumber, uint column, out JsValue breakpoint)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDiagSetBreakpoint(scriptId, lineNumber, column, out breakpoint);
            if (IsWindows && Is64) return Native64.JsDiagSetBreakpoint(scriptId, lineNumber, column, out breakpoint);
            if (IsWindows && IsArm) return NativeArm.JsDiagSetBreakpoint(scriptId, lineNumber, column, out breakpoint);
            if (IsLinux && Is64) return Native64Linux.JsDiagSetBreakpoint(scriptId, lineNumber, column, out breakpoint);
            if (IsMac && Is64) return Native64Mac.JsDiagSetBreakpoint(scriptId, lineNumber, column, out breakpoint);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDiagSetBreakpoint(scriptId, lineNumber, column, out breakpoint)
                : Native64.JsDiagSetBreakpoint(scriptId, lineNumber, column, out breakpoint);
#endif
        }

        public static JsErrorCode JsDiagRequestAsyncBreak(JsRuntime jsRuntime)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDiagRequestAsyncBreak(jsRuntime);
            if (IsWindows && Is64) return Native64.JsDiagRequestAsyncBreak(jsRuntime);
            if (IsWindows && IsArm) return NativeArm.JsDiagRequestAsyncBreak(jsRuntime);
            if (IsLinux && Is64) return Native64Linux.JsDiagRequestAsyncBreak(jsRuntime);
            if (IsMac && Is64) return Native64Mac.JsDiagRequestAsyncBreak(jsRuntime);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDiagRequestAsyncBreak(jsRuntime)
                : Native64.JsDiagRequestAsyncBreak(jsRuntime);
#endif
        }

        public static JsErrorCode JsDiagGetBreakpoints(out JsValue breakpoints)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDiagGetBreakpoints(out breakpoints);
            if (IsWindows && Is64) return Native64.JsDiagGetBreakpoints(out breakpoints);
            if (IsWindows && IsArm) return NativeArm.JsDiagGetBreakpoints(out breakpoints);
            if (IsLinux && Is64) return Native64Linux.JsDiagGetBreakpoints(out breakpoints);
            if (IsMac && Is64) return Native64Mac.JsDiagGetBreakpoints(out breakpoints);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDiagGetBreakpoints(out breakpoints)
                : Native64.JsDiagGetBreakpoints(out breakpoints);
#endif
        }

        public static JsErrorCode JsDiagRemoveBreakpoint(uint breakpointId)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDiagRemoveBreakpoint(breakpointId);
            if (IsWindows && Is64) return Native64.JsDiagRemoveBreakpoint(breakpointId);
            if (IsWindows && IsArm) return NativeArm.JsDiagRemoveBreakpoint(breakpointId);
            if (IsLinux && Is64) return Native64Linux.JsDiagRemoveBreakpoint(breakpointId);
            if (IsMac && Is64) return Native64Mac.JsDiagRemoveBreakpoint(breakpointId);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDiagRemoveBreakpoint(breakpointId)
                : Native64.JsDiagRemoveBreakpoint(breakpointId);
#endif
        }

        public static JsErrorCode JsDiagGetScripts(out JsValue scripts)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDiagGetScripts(out scripts);
            if (IsWindows && Is64) return Native64.JsDiagGetScripts(out scripts);
            if (IsWindows && IsArm) return NativeArm.JsDiagGetScripts(out scripts);
            if (IsLinux && Is64) return Native64Linux.JsDiagGetScripts(out scripts);
            if (IsMac && Is64) return Native64Mac.JsDiagGetScripts(out scripts);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDiagGetScripts(out scripts)
                : Native64.JsDiagGetScripts(out scripts);
#endif
        }

        public static JsErrorCode JsDiagEvaluate(JsValue expression, uint stackFrameIndex,
            JsParseScriptAttributes parseAttributes, bool forceSetValueProp, out JsValue eval)
        {
#if NETSTANDARD
            if (IsWindows && Is32) return Native32.JsDiagEvaluate(expression, stackFrameIndex, parseAttributes, forceSetValueProp, out eval);
            if (IsWindows && Is64) return Native64.JsDiagEvaluate(expression, stackFrameIndex, parseAttributes, forceSetValueProp, out eval);
            if (IsWindows && IsArm) return NativeArm.JsDiagEvaluate(expression, stackFrameIndex, parseAttributes, forceSetValueProp, out eval);
            if (IsLinux && Is64) return Native64Linux.JsDiagEvaluate(expression, stackFrameIndex, parseAttributes, forceSetValueProp, out eval);
            if (IsMac && Is64) return Native64Mac.JsDiagEvaluate(expression, stackFrameIndex, parseAttributes, forceSetValueProp, out eval);
            throw new NotSupportedException("The current operation system is not supported.");
#else
            return Is32
                ? Native32.JsDiagEvaluate(expression, stackFrameIndex, parseAttributes, forceSetValueProp, out eval)
                : Native64.JsDiagEvaluate(expression, stackFrameIndex, parseAttributes, forceSetValueProp, out eval);
#endif
        }
    }
}