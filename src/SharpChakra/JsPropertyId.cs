using System;

namespace SharpChakra
{
   public struct JsPropertyId : IEquatable<JsPropertyId>
   {
      private readonly IntPtr p_id;

      internal JsPropertyId(IntPtr _id)
      {
         p_id = _id;
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

      public static JsPropertyId FromString(string _name)
      {
         Native.ThrowIfError(Native.JsGetPropertyIdFromName(_name, out var id));
         return id;
      }

      public static bool operator ==(JsPropertyId _left, JsPropertyId _right) => _left.Equals(_right);
      public static bool operator !=(JsPropertyId _left, JsPropertyId _right) => !_left.Equals(_right);
      public bool Equals(JsPropertyId _other) => p_id == _other.p_id;
      public override bool Equals(object _obj)
      {
         if (ReferenceEquals(null, _obj))
            return false;

         return _obj is JsPropertyId && Equals((JsPropertyId)_obj);
      }

      public override int GetHashCode() => p_id.ToInt32();
      public override string ToString() => Name;
   }
}