using System;

namespace SharpChakra
{

   public struct JsContext
   {
      private readonly IntPtr _pReference;

      internal JsContext(IntPtr reference)
      {
         _pReference = reference;
      }

      public static JsContext Invalid => new JsContext(IntPtr.Zero);
      public static JsContext Current
      {
         get
         {
            Native.ThrowIfError(Native.JsGetCurrentContext(out var reference));
            return reference;
         }

         set => Native.ThrowIfError(Native.JsSetCurrentContext(value));
      }
      public static bool HasException
      {
         get
         {
            bool hasException;
            Native.ThrowIfError(Native.JsHasException(out hasException));
            return hasException;
         }
      }

      public JsRuntime Runtime
      {
         get
         {
            JsRuntime handle;
            Native.ThrowIfError(Native.JsGetRuntime(this, out handle));
            return handle;
         }
      }

      public bool IsValid => _pReference != IntPtr.Zero;
      public static uint Idle()
      {
         uint ticks;
         Native.ThrowIfError(Native.JsIdle(out ticks));
         return ticks;
      }
      public static JsValue ParseScript(string script, JsSourceContext sourceContext, string sourceName)
      {
         JsValue result;
         Native.ThrowIfError(Native.JsParseScript(script, sourceContext, sourceName, out result));
         return result;
      }
      public static JsValue ParseScript(string script, byte[] buffer, JsSourceContext sourceContext, string sourceName)
      {
         Native.ThrowIfError(Native.JsParseSerializedScript(script, buffer, sourceContext, sourceName, out var result));
         return result;
      }

      public static JsValue ParseScript(string script) => ParseScript(script, JsSourceContext.None, string.Empty);
      public static JsValue ParseScript(string script, byte[] buffer) => ParseScript(script, buffer, JsSourceContext.None, string.Empty);
      public static JsValue RunScript(string script, JsSourceContext sourceContext, string sourceName)
      {
         Native.ThrowIfError(Native.JsRunScript(script, sourceContext, sourceName, out var result));
         return result;
      }

      public static JsValue RunScript(string script, byte[] buffer, JsSourceContext sourceContext, string sourceName)
      {
         Native.ThrowIfError(Native.JsRunSerializedScript(script, buffer, sourceContext, sourceName, out var result));
         return result;
      }

      public static JsValue RunScript(string script) => RunScript(script, JsSourceContext.None, string.Empty);
      public static JsValue RunScript(string script, byte[] buffer) => RunScript(script, buffer, JsSourceContext.None, string.Empty);
      public static ulong SerializeScript(string script, byte[] buffer)
      {
         var bufferSize = (ulong)buffer.Length;
         Native.ThrowIfError(Native.JsSerializeScript(script, buffer, ref bufferSize));
         return bufferSize;
      }

      public static JsValue GetAndClearException()
      {
         JsValue reference;
         Native.ThrowIfError(Native.JsGetAndClearException(out reference));
         return reference;
      }

      public static void SetException(JsValue exception) => Native.ThrowIfError(Native.JsSetException(exception));
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

      public struct Scope : IDisposable
      {
         private readonly JsContext _pPreviousContext;
         private bool _pDisposed;
         public Scope(JsContext context)
         {
            _pDisposed = false;
            _pPreviousContext = Current;
            Current = context;
         }

         public void Dispose()
         {
            if (_pDisposed)
               return;

            Current = _pPreviousContext;
            _pDisposed = true;
         }
      }
   }
}
