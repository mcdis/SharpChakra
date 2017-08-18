using System;

namespace SharpChakra
{
   /// <summary>
    ///     A script exception.
    /// </summary>
    public sealed class JavaScriptScriptException : JavaScriptException
    {
        /// <summary>
        /// The error.
        /// </summary>
        private readonly JavaScriptValue p_error;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptScriptException"/> class. 
        /// </summary>
        /// <param name="_code">The error code returned.</param>
        /// <param name="_error">The JavaScript error object.</param>
        public JavaScriptScriptException(JavaScriptErrorCode _code, JavaScriptValue _error) :
            this(_code, _error, "JavaScript Exception")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptScriptException"/> class. 
        /// </summary>
        /// <param name="_code">The error code returned.</param>
        /// <param name="_error">The JavaScript error object.</param>
        /// <param name="message">The error message.</param>
        public JavaScriptScriptException(JavaScriptErrorCode _code, JavaScriptValue _error, string message) :
            base(_code, message)
        {
            p_error = _error;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptScriptException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        private JavaScriptScriptException(string message, Exception _innerException) :
            base(message, _innerException)
        {
        }

        /// <summary>
        ///     Gets a JavaScript object representing the script error.
        /// </summary>
        public JavaScriptValue Error => p_error;
    }
}