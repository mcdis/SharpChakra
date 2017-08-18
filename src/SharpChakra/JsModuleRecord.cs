using System;
using System.Text;

namespace SharpChakra
{
   public struct JsModuleRecord : IEquatable<JsModuleRecord>
   {
      private readonly IntPtr p_ptr;
      internal JsModuleRecord(IntPtr _ptr)
      {
         p_ptr = _ptr;
      }

      public static JsModuleRecord Root => new JsModuleRecord(IntPtr.Zero);
      public static JsModuleRecord Invalid => new JsModuleRecord(new IntPtr(-1));
      public bool Equals(JsModuleRecord _other) => p_ptr.Equals(_other.p_ptr);
      public override bool Equals(object _obj)
      {
         if (ReferenceEquals(null, _obj)) return false;
         return _obj is JsModuleRecord && Equals((JsModuleRecord)_obj);
      }
      public override int GetHashCode() => p_ptr.GetHashCode();
      public static bool operator ==(JsModuleRecord _left, JsModuleRecord _right) => _left.Equals(_right);
      public static bool operator !=(JsModuleRecord _left, JsModuleRecord _right) => !(_left == _right);

      public static JsModuleRecord Create(JsModuleRecord _reference, JsValue _specifier)
      {
         Native.ThrowIfError(Native.JsInitializeModuleRecord(_reference, _specifier, out var module));
         return module;
      }
      public JsValue Parse(string _src)
         => Parse(_src, JsSourceContext.None);
      public JsValue Parse(string _src, JsSourceContext _ctx)
      {
         var data = Encoding.UTF8.GetBytes(_src);
         Native.ThrowIfError(Native.JsParseModuleSource(this,
            _ctx,
            data,
            (uint)data.Length,
            JsParseModuleSourceFlags.DataIsUTF8,
            out var exception));
         return exception;
      }
      public JsValue Eval()
      {
         Native.ThrowIfError(Native.JsModuleEvaluation(this,out var res));
         return res;
      }
      public void SetHostInfo(JsFetchImportedModuleCallBack _fetch) => Native.ThrowIfError(Native.JsSetModuleHostInfo(this, _fetch));
      public void SetHostInfo(JsNotifyModuleReadyCallback _ready) => Native.ThrowIfError(Native.JsSetModuleHostInfo(this, _ready));
      public void SetHostInfo(JsFetchImportedModuleFromScriptCallback _fetch) => Native.ThrowIfError(Native.JsSetModuleHostInfo(this, _fetch));
   }
}
