using System;
using System.Text;

namespace SharpChakra
{
   public struct JsModuleRecord : IEquatable<JsModuleRecord>
   {
      private readonly IntPtr _pPtr;
      internal JsModuleRecord(IntPtr ptr)
      {
         _pPtr = ptr;
      }

      public static JsModuleRecord Root => new JsModuleRecord(IntPtr.Zero);
      public static JsModuleRecord Invalid => new JsModuleRecord(new IntPtr(-1));
      public bool Equals(JsModuleRecord other) => _pPtr.Equals(other._pPtr);
      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         return obj is JsModuleRecord && Equals((JsModuleRecord)obj);
      }
      public override int GetHashCode() => _pPtr.GetHashCode();
      public static bool operator ==(JsModuleRecord left, JsModuleRecord right) => left.Equals(right);
      public static bool operator !=(JsModuleRecord left, JsModuleRecord right) => !(left == right);

      public static JsModuleRecord Create(JsModuleRecord reference, JsValue specifier)
      {
         Native.ThrowIfError(Native.JsInitializeModuleRecord(reference, specifier, out var module));
         return module;
      }
      public JsValue Parse(string src)
         => Parse(src, JsSourceContext.None);
      public JsValue Parse(string src, JsSourceContext ctx)
      {
         var data = Encoding.UTF8.GetBytes(src);
         Native.ThrowIfError(Native.JsParseModuleSource(this,
            ctx,
            data,
            (uint)data.Length,
            JsParseModuleSourceFlags.DataIsUtf8,
            out var exception));
         return exception;
      }
      public JsValue Eval()
      {
         Native.ThrowIfError(Native.JsModuleEvaluation(this,out var res));
         return res;
      }
      public void SetHostInfo(JsFetchImportedModuleCallBack fetch) => Native.ThrowIfError(Native.JsSetModuleHostInfo(this, fetch));
      public void SetHostInfo(JsNotifyModuleReadyCallback ready) => Native.ThrowIfError(Native.JsSetModuleHostInfo(this, ready));
      public void SetHostInfo(JsFetchImportedModuleFromScriptCallback fetch) => Native.ThrowIfError(Native.JsSetModuleHostInfo(this, fetch));
   }
}
