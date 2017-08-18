using System;

namespace SharpChakra
{
   public struct JsSourceContext : IEquatable<JsSourceContext>
   {
      private readonly IntPtr p_context;

      private JsSourceContext(IntPtr _context)
      {
         p_context = _context;
      }

      public static JsSourceContext None => new JsSourceContext(new IntPtr(-1));
      public static bool operator ==(JsSourceContext _left, JsSourceContext _right) => _left.Equals(_right);
      public static bool operator !=(JsSourceContext _left, JsSourceContext _right) => !_left.Equals(_right);
      public static JsSourceContext operator -(JsSourceContext _context, int _offset) => FromIntPtr(_context.p_context - _offset);
      public static JsSourceContext Subtract(JsSourceContext _left, int _right) => _left - _right;
      public static JsSourceContext operator --(JsSourceContext _context) => FromIntPtr(_context.p_context - 1);
      public static JsSourceContext Decrement(JsSourceContext _left) => --_left;
      public static JsSourceContext operator +(JsSourceContext _context, int _offset) => FromIntPtr(_context.p_context + _offset);
      public static JsSourceContext Add(JsSourceContext _left, int _right) => _left + _right;
      public static JsSourceContext operator ++(JsSourceContext _context) => FromIntPtr(_context.p_context + 1);
      public static JsSourceContext Increment(JsSourceContext _left) => ++_left;
      public static JsSourceContext FromIntPtr(IntPtr _cookie) => new JsSourceContext(_cookie);
      public bool Equals(JsSourceContext _other) => p_context == _other.p_context;
      public override bool Equals(object _obj)
      {
         if (ReferenceEquals(null, _obj))
            return false;

         return _obj is JsSourceContext && Equals((JsSourceContext)_obj);
      }

      public override int GetHashCode() => p_context.ToInt32();
   }
}