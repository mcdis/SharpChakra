using System;

namespace SharpChakra
{
   /// <summary>
    ///     A property identifier.
    /// </summary>
    /// <remarks>
    ///     Property identifiers are used to refer to properties of JavaScript objects instead of using
    ///     strings.
    /// </remarks>
    public struct JavaScriptPropertyId : IEquatable<JavaScriptPropertyId>
    {
        /// <summary>
        /// The id.
        /// </summary>
        private readonly IntPtr p_id;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptPropertyId"/> struct. 
        /// </summary>
        /// <param name="_id">The ID.</param>
        internal JavaScriptPropertyId(IntPtr _id)
        {
            this.p_id = _id;
        }

        /// <summary>
        ///     Gets an invalid ID.
        /// </summary>
        public static JavaScriptPropertyId Invalid
        {
            get { return new JavaScriptPropertyId(IntPtr.Zero); }
        }

        /// <summary>
        ///     Gets the name associated with the property ID.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///     Requires an active script context.
        ///     </para>
        /// </remarks>
        public string Name
        {
            get
            {
                string name;
                Native.ThrowIfError(Native.JsGetPropertyNameFromId(this, out name));
                return name;
            }
        }

        /// <summary>
        ///     Gets the property ID associated with the name. 
        /// </summary>
        /// <remarks>
        ///     <para>
        ///     Property IDs are specific to a context and cannot be used across contexts.
        ///     </para>
        ///     <para>
        ///     Requires an active script context.
        ///     </para>
        /// </remarks>
        /// <param name="_name">
        ///     The name of the property ID to get or create. The name may consist of only digits.
        /// </param>
        /// <returns>The property ID in this runtime for the given name.</returns>
        public static JavaScriptPropertyId FromString(string _name)
        {
            JavaScriptPropertyId id;
            Native.ThrowIfError(Native.JsGetPropertyIdFromName(_name, out id));
            return id;
        }

        /// <summary>
        ///     The equality operator for property IDs.
        /// </summary>
        /// <param name="_left">The first property ID to compare.</param>
        /// <param name="_right">The second property ID to compare.</param>
        /// <returns>Whether the two property IDs are the same.</returns>
        public static bool operator ==(JavaScriptPropertyId _left, JavaScriptPropertyId _right)
        {
            return _left.Equals(_right);
        }

        /// <summary>
        ///     The inequality operator for property IDs.
        /// </summary>
        /// <param name="_left">The first property ID to compare.</param>
        /// <param name="_right">The second property ID to compare.</param>
        /// <returns>Whether the two property IDs are not the same.</returns>
        public static bool operator !=(JavaScriptPropertyId _left, JavaScriptPropertyId _right)
        {
            return !_left.Equals(_right);
        }

        /// <summary>
        ///     Checks for equality between property IDs.
        /// </summary>
        /// <param name="_other">The other property ID to compare.</param>
        /// <returns>Whether the two property IDs are the same.</returns>
        public bool Equals(JavaScriptPropertyId _other)
        {
            return p_id == _other.p_id;
        }

        /// <summary>
        ///     Checks for equality between property IDs.
        /// </summary>
        /// <param name="_obj">The other property ID to compare.</param>
        /// <returns>Whether the two property IDs are the same.</returns>
        public override bool Equals(object _obj)
        {
            if (ReferenceEquals(null, _obj))
            {
                return false;
            }

            return _obj is JavaScriptPropertyId && Equals((JavaScriptPropertyId)_obj);
        }

        /// <summary>
        ///     The hash code.
        /// </summary>
        /// <returns>The hash code of the property ID.</returns>
        public override int GetHashCode()
        {
            return p_id.ToInt32();
        }

        /// <summary>
        ///     Converts the property ID to a string.
        /// </summary>
        /// <returns>The name of the property ID.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}