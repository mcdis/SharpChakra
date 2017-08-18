using System;

namespace SharpChakra
{

   public struct JsContext
   {
      private readonly IntPtr p_reference;

      internal JsContext(IntPtr _reference)
      {
         p_reference = _reference;
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

      public bool IsValid => p_reference != IntPtr.Zero;
      public static uint Idle()
      {
         uint ticks;
         Native.ThrowIfError(Native.JsIdle(out ticks));
         return ticks;
      }
      public static JsValue ParseScript(string _script, JsSourceContext _sourceContext, string _sourceName)
      {
         JsValue result;
         Native.ThrowIfError(Native.JsParseScript(_script, _sourceContext, _sourceName, out result));
         return result;
      }
      public static JsValue ParseScript(string _script, byte[] _buffer, JsSourceContext _sourceContext, string _sourceName)
      {
         Native.ThrowIfError(Native.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceName, out var result));
         return result;
      }

      public static JsValue ParseScript(string _script) => ParseScript(_script, JsSourceContext.None, string.Empty);
      public static JsValue ParseScript(string _script, byte[] _buffer) => ParseScript(_script, _buffer, JsSourceContext.None, string.Empty);
      public static JsValue RunScript(string _script, JsSourceContext _sourceContext, string _sourceName)
      {
         Native.ThrowIfError(Native.JsRunScript(_script, _sourceContext, _sourceName, out var result));
         return result;
      }

      public static JsValue RunScript(string _script, byte[] _buffer, JsSourceContext _sourceContext, string _sourceName)
      {
         Native.ThrowIfError(Native.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceName, out var result));
         return result;
      }

      public static JsValue RunScript(string _script) => RunScript(_script, JsSourceContext.None, string.Empty);
      public static JsValue RunScript(string _script, byte[] _buffer) => RunScript(_script, _buffer, JsSourceContext.None, string.Empty);
      public static ulong SerializeScript(string _script, byte[] _buffer)
      {
         var bufferSize = (ulong)_buffer.Length;
         Native.ThrowIfError(Native.JsSerializeScript(_script, _buffer, ref bufferSize));
         return bufferSize;
      }

      public static JsValue GetAndClearException()
      {
         JsValue reference;
         Native.ThrowIfError(Native.JsGetAndClearException(out reference));
         return reference;
      }

      public static void SetException(JsValue _exception) => Native.ThrowIfError(Native.JsSetException(_exception));
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
         private readonly JsContext p_previousContext;
         private bool p_disposed;
         public Scope(JsContext _context)
         {
            p_disposed = false;
            p_previousContext = Current;
            Current = _context;
         }

         public void Dispose()
         {
            if (p_disposed)
               return;

            Current = p_previousContext;
            p_disposed = true;
         }
      }
   }
}
