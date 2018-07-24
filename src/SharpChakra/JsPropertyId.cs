using System;

namespace SharpChakra
{
    public struct JsPropertyId : IEquatable<JsPropertyId>
    {
        private readonly IntPtr _pId;

        internal JsPropertyId(IntPtr id)
        {
            _pId = id;
        }

        public static JsPropertyId Invalid => new JsPropertyId(IntPtr.Zero);

        public string Name
        {
            get
            {
                Native.ThrowIfError(Native.JsGetPropertyNameFromId(this, out var name));
                return name;
            }
        }

        public static JsPropertyId FromString(string name)
        {
            Native.ThrowIfError(Native.JsGetPropertyIdFromName(name, out var id));
            return id;
        }

        public static bool operator ==(JsPropertyId left, JsPropertyId right) => left.Equals(right);
        public static bool operator !=(JsPropertyId left, JsPropertyId right) => !left.Equals(right);
        public bool Equals(JsPropertyId other) => _pId == other._pId;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is JsPropertyId && Equals((JsPropertyId) obj);
        }

        public override int GetHashCode() => _pId.ToInt32();
        public override string ToString() => Name;

        public static implicit operator string(JsPropertyId value)
        {
            return value.Name;
        }

        public static implicit operator JsPropertyId(string value)
        {
            return FromString(value);
        }
    }
}