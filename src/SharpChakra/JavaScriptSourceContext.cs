using System;

namespace SharpChakra
{
   /// <summary>
    ///     A cookie that identifies a script for debugging purposes.
    /// </summary>
    public struct JavaScriptSourceContext : IEquatable<JavaScriptSourceContext>
    {
        /// <summary>
        /// The context.
        /// </summary>
        private readonly IntPtr p_context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptSourceContext"/> struct.
        /// </summary>
        /// <param name="_context">The context.</param>
        private JavaScriptSourceContext(IntPtr _context)
        {
            p_context = _context;
        }

        /// <summary>
        ///     Gets an empty source context.
        /// </summary>
        public static JavaScriptSourceContext None => new JavaScriptSourceContext(new IntPtr(-1));
       /// <summary>
        ///     The equality operator for source contexts.
        /// </summary>
        /// <param name="_left">The first source context to compare.</param>
        /// <param name="_right">The second source context to compare.</param>
        /// <returns>Whether the two source contexts are the same.</returns>
        public static bool operator ==(JavaScriptSourceContext _left, JavaScriptSourceContext _right)
        {
            return _left.Equals(_right);
        }

        /// <summary>
        ///     The inequality operator for source contexts.
        /// </summary>
        /// <param name="_left">The first source context to compare.</param>
        /// <param name="_right">The second source context to compare.</param>
        /// <returns>Whether the two source contexts are not the same.</returns>
        public static bool operator !=(JavaScriptSourceContext _left, JavaScriptSourceContext _right)
        {
            return !_left.Equals(_right);
        }

        /// <summary>
        ///     Subtracts an offset from the value of the source context.
        /// </summary>
        /// <param name="_context">The source context to subtract the offset from.</param>
        /// <param name="_offset">The offset to subtract.</param>
        /// <returns>A new source context that reflects the subtraction of the offset from the context.</returns>
        public static JavaScriptSourceContext operator -(JavaScriptSourceContext _context, int _offset)
        {
            return FromIntPtr(_context.p_context - _offset);
        }

        /// <summary>
        ///     Subtracts an offset from the value of the source context.
        /// </summary>
        /// <param name="_left">The source context to subtract the offset from.</param>
        /// <param name="_right">The offset to subtract.</param>
        /// <returns>A new source context that reflects the subtraction of the offset from the context.</returns>
        public static JavaScriptSourceContext Subtract(JavaScriptSourceContext _left, int _right)
        {
            return _left - _right;
        }

        /// <summary>
        ///     Decrements the value of the source context.
        /// </summary>
        /// <param name="_context">The source context to decrement.</param>
        /// <returns>A new source context that reflects the decrementing of the context.</returns>
        public static JavaScriptSourceContext operator --(JavaScriptSourceContext _context)
        {
            return FromIntPtr(_context.p_context - 1);
        }

        /// <summary>
        ///     Decrements the value of the source context.
        /// </summary>
        /// <param name="_left">The source context to decrement.</param>
        /// <returns>A new source context that reflects the decrementing of the context.</returns>
        public static JavaScriptSourceContext Decrement(JavaScriptSourceContext _left)
        {
            return --_left;
        }

        /// <summary>
        ///     Adds an offset from the value of the source context.
        /// </summary>
        /// <param name="_context">The source context to add the offset to.</param>
        /// <param name="_offset">The offset to add.</param>
        /// <returns>A new source context that reflects the addition of the offset to the context.</returns>
        public static JavaScriptSourceContext operator +(JavaScriptSourceContext _context, int _offset)
        {
            return FromIntPtr(_context.p_context + _offset);
        }

        /// <summary>
        ///     Adds an offset from the value of the source context.
        /// </summary>
        /// <param name="_left">The source context to add the offset to.</param>
        /// <param name="_right">The offset to add.</param>
        /// <returns>A new source context that reflects the addition of the offset to the context.</returns>
        public static JavaScriptSourceContext Add(JavaScriptSourceContext _left, int _right)
        {
            return _left + _right;
        }

        /// <summary>
        ///     Increments the value of the source context.
        /// </summary>
        /// <param name="_context">The source context to increment.</param>
        /// <returns>A new source context that reflects the incrementing of the context.</returns>
        public static JavaScriptSourceContext operator ++(JavaScriptSourceContext _context)
        {
            return FromIntPtr(_context.p_context + 1);
        }

        /// <summary>
        ///     Increments the value of the source context.
        /// </summary>
        /// <param name="_left">The source context to increment.</param>
        /// <returns>A new source context that reflects the incrementing of the context.</returns>
        public static JavaScriptSourceContext Increment(JavaScriptSourceContext _left)
        {
            return ++_left;
        }

        /// <summary>
        ///     Creates a new source context. 
        /// </summary>
        /// <param name="_cookie">
        ///     The cookie for the source context.
        /// </param>
        /// <returns>The new source context.</returns>
        public static JavaScriptSourceContext FromIntPtr(IntPtr _cookie)
        {
            return new JavaScriptSourceContext(_cookie);
        }

        /// <summary>
        ///     Checks for equality between source contexts.
        /// </summary>
        /// <param name="_other">The other source context to compare.</param>
        /// <returns>Whether the two source contexts are the same.</returns>
        public bool Equals(JavaScriptSourceContext _other)
        {
            return p_context == _other.p_context;
        }

        /// <summary>
        ///     Checks for equality between source contexts.
        /// </summary>
        /// <param name="_obj">The other source context to compare.</param>
        /// <returns>Whether the two source contexts are the same.</returns>
        public override bool Equals(object _obj)
        {
            if (ReferenceEquals(null, _obj))
            {
                return false;
            }

            return _obj is JavaScriptSourceContext && Equals((JavaScriptSourceContext)_obj);
        }

        /// <summary>
        ///     The hash code.
        /// </summary>
        /// <returns>The hash code of the source context.</returns>
        public override int GetHashCode()
        {
            return p_context.ToInt32();
        }
    }
}