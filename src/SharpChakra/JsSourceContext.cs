using System;

namespace SharpChakra
{
    public struct JsSourceContext : IEquatable<JsSourceContext>
    {
        private readonly IntPtr _pContext;

        private JsSourceContext(IntPtr context)
        {
            _pContext = context;
        }

        public static JsSourceContext None => new JsSourceContext(new IntPtr(-1));

        public static bool operator ==(JsSourceContext left, JsSourceContext right) => left.Equals(right);

        public static bool operator !=(JsSourceContext left, JsSourceContext right) => !left.Equals(right);

        public static JsSourceContext operator -(JsSourceContext context, int offset) =>
            FromIntPtr(context._pContext - offset);

        public static JsSourceContext Subtract(JsSourceContext left, int right) => left - right;

        public static JsSourceContext operator --(JsSourceContext context) => FromIntPtr(context._pContext - 1);

        public static JsSourceContext Decrement(JsSourceContext left) => --left;

        public static JsSourceContext operator +(JsSourceContext context, int offset) =>
            FromIntPtr(context._pContext + offset);

        public static JsSourceContext Add(JsSourceContext left, int right) => left + right;

        public static JsSourceContext operator ++(JsSourceContext context) => FromIntPtr(context._pContext + 1);

        public static JsSourceContext Increment(JsSourceContext left) => ++left;

        public static JsSourceContext FromIntPtr(IntPtr cookie) => new JsSourceContext(cookie);

        public bool Equals(JsSourceContext other) => _pContext == other._pContext;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is JsSourceContext && Equals((JsSourceContext) obj);
        }

        public override int GetHashCode() => _pContext.ToInt32();
    }
}