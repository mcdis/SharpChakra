using System;

namespace SharpChakra
{
   public struct JsRuntime : IDisposable
   {
      private IntPtr p_handle;
      public bool IsValid => p_handle != IntPtr.Zero;
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

         set => Native.ThrowIfError(Native.JsSetRuntimeMemoryLimit(this,new UIntPtr(value)));
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

      public static JsRuntime Create(JsRuntimeAttributes _attributes, JsRuntimeVersion _version, JavaScriptThreadServiceCallback _threadServiceCallback)
      {
         Native.ThrowIfError(Native.JsCreateRuntime(_attributes, _threadServiceCallback, out var handle));
         return handle;
      }
      public static JsRuntime Create(JsRuntimeAttributes _attributes, JsRuntimeVersion _version) => Create(_attributes, _version, null);
      public static JsRuntime Create() => Create(JsRuntimeAttributes.None, JsRuntimeVersion.Version11, null);
      public void Dispose()
      {
         if (IsValid)
            Native.ThrowIfError(Native.JsDisposeRuntime(this));

         p_handle = IntPtr.Zero;
      }

      public void GC() => Native.ThrowIfError(Native.JsCollectGarbage(this));

      public void SetMemoryAllocationCallback(IntPtr _callbackState, JsMemoryAllocationCallback _allocationCallback)
         => Native.ThrowIfError(Native.JsSetRuntimeMemoryAllocationCallback(this, _callbackState, _allocationCallback));
      public void SetBeforeCollectCallback(IntPtr _callbackState, JsBeforeCollectCallback _beforeCollectCallback)
         => Native.ThrowIfError(Native.JsSetRuntimeBeforeCollectCallback(this, _callbackState, _beforeCollectCallback));

      public void StartDebugging(JsDiagDebugEventCallback _debugEventCallback)
         => StartDebugging(_debugEventCallback, IntPtr.Zero);

      public void StopDebugging() => StopDebugging(out var state);

      public void StopDebugging(out IntPtr _callbackState)
         => Native.ThrowIfError(Native.JsDiagStopDebugging(this, out _callbackState));
      public void StartDebugging(JsDiagDebugEventCallback _debugEventCallback, IntPtr _callbackState)
         => Native.ThrowIfError(Native.JsDiagStartDebugging(this, _debugEventCallback, _callbackState));

      public void SetBreakpoint(uint _scriptId, uint _lineNumber, uint _column, out JsValue _breakpoint)
         => Native.ThrowIfError(Native.SetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint));
      public void RequestAsyncBreak()
         => Native.ThrowIfError(Native.JsDiagRequestAsyncBreak(this));

      public void RemoveBreakpoint(uint _breakpointId)
         => Native.ThrowIfError(Native.JsDiagRemoveBreakpoint(_breakpointId));

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

      public JsValue DiagEvaluate(JsValue _expression, uint _stackFrameIndex,
         JsParseScriptAttributes _parseAttributes, bool _forceSetValueProp)
      {
         Native.ThrowIfError(Native.JsDiagEvaluate(_expression, _stackFrameIndex, _parseAttributes, _forceSetValueProp,out var eval));
         return eval;
      }
   }
}
