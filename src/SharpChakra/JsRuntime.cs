using System;
using System.Collections.Concurrent;

namespace SharpChakra
{
    public struct JsRuntime : IDisposable
    {
        private static readonly ConcurrentDictionary<IntPtr, JsRuntimeInfo> RuntimeInfo = new ConcurrentDictionary<IntPtr, JsRuntimeInfo>();

        internal IntPtr Handle;

        private JsRuntime(IntPtr zero)
        {
            Handle = zero;
        }

        public static JsRuntime Invalid => new JsRuntime(IntPtr.Zero);

        public bool IsValid => Handle != IntPtr.Zero;

        public ulong MemoryUsage
        {
            get
            {
                Native.ThrowIfError(Native.JsGetRuntimeMemoryUsage(this, out var memoryUsage));
                return memoryUsage.ToUInt64();
            }
        }

        public ulong MemoryLimit
        {
            get
            {
                Native.ThrowIfError(Native.JsGetRuntimeMemoryLimit(this, out var memoryLimit));
                return memoryLimit.ToUInt64();
            }

            set => Native.ThrowIfError(Native.JsSetRuntimeMemoryLimit(this, new UIntPtr(value)));
        }

        public bool Disabled
        {
            get
            {
                Native.ThrowIfError(Native.JsIsRuntimeExecutionDisabled(this, out var isDisabled));
                return isDisabled;
            }

            set => Native.ThrowIfError(value
                ? Native.JsDisableRuntimeExecution(this)
                : Native.JsEnableRuntimeExecution(this));
        }
        
        public static JsRuntime Create(JsRuntimeAttributes attributes, JsRuntimeVersion version, JavaScriptThreadServiceCallback threadServiceCallback)
        {
            Native.ThrowIfError(Native.JsCreateRuntime(attributes, threadServiceCallback, out var handle));
            var info = RuntimeInfo.GetOrAdd(handle.Handle, ptr => new JsRuntimeInfo());
            info.References++;
            return handle;
        }

        public static JsRuntime Create(JsRuntimeAttributes attributes, JsRuntimeVersion version) => Create(attributes, version, null);

        public static JsRuntime Create() => Create(JsRuntimeAttributes.None, JsRuntimeVersion.Version11, null);

        public void Dispose()
        {
            if (JsContext.Current.IsValid && Equals(JsContext.Current.Runtime, this))
            {
                Native.JsSetCurrentContext(Native.DefaultContext);
            }

            if (IsValid)
            {
                var info = RuntimeInfo.GetOrAdd(Handle, ptr => new JsRuntimeInfo());
                if (--info.References == 0)
                {
                    Native.ThrowIfError(Native.JsDisposeRuntime(this));
                }
            }

            Handle = IntPtr.Zero;
        }

        public void Gc() => Native.ThrowIfError(Native.JsCollectGarbage(this));

        public void SetMemoryAllocationCallback(IntPtr callbackState, JsMemoryAllocationCallback allocationCallback)
            => Native.ThrowIfError(
                Native.JsSetRuntimeMemoryAllocationCallback(this, callbackState, allocationCallback));

        public void SetBeforeCollectCallback(IntPtr callbackState, JsBeforeCollectCallback beforeCollectCallback)
            => Native.ThrowIfError(
                Native.JsSetRuntimeBeforeCollectCallback(this, callbackState, beforeCollectCallback));

        public void StartDebugging(JsDiagDebugEventCallback debugEventCallback)
            => StartDebugging(debugEventCallback, IntPtr.Zero);

        public void StopDebugging() => StopDebugging(out var state);

        public void StopDebugging(out IntPtr callbackState)
            => Native.ThrowIfError(Native.JsDiagStopDebugging(this, out callbackState));

        public void StartDebugging(JsDiagDebugEventCallback debugEventCallback, IntPtr callbackState)
            => Native.ThrowIfError(Native.JsDiagStartDebugging(this, debugEventCallback, callbackState));

        public void SetBreakpoint(uint scriptId, uint lineNumber, uint column, out JsValue breakpoint)
            => Native.ThrowIfError(Native.SetBreakpoint(scriptId, lineNumber, column, out breakpoint));

        public void RequestAsyncBreak()
            => Native.ThrowIfError(Native.JsDiagRequestAsyncBreak(this));

        public void RemoveBreakpoint(uint breakpointId)
            => Native.ThrowIfError(Native.JsDiagRemoveBreakpoint(breakpointId));

        public JsValue GetScripts()
        {
            Native.ThrowIfError(Native.JsDiagGetScripts(out var scripts));
            return scripts;
        }

        public JsContext CreateContext()
        {
            Native.ThrowIfError(Native.JsCreateContext(this, out var reference));
            return reference;
        }

        public JsValue GetBreakpoints()
        {
            Native.ThrowIfError(Native.JsDiagGetBreakpoints(out var breakpoints));
            return breakpoints;
        }

        public JsValue DiagEvaluate(JsValue expression, uint stackFrameIndex,
            JsParseScriptAttributes parseAttributes, bool forceSetValueProp)
        {
            Native.ThrowIfError(Native.JsDiagEvaluate(expression, stackFrameIndex, parseAttributes, forceSetValueProp,
                out var eval));
            return eval;
        }

        private struct JsRuntimeInfo
        {
            public int References { get; set; }
        }
    }
}