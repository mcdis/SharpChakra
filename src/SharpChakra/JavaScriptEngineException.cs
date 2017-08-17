using System;

namespace SharpChakra
{
   /// <summary>
    ///     An exception that occurred in the workings of the JavaScript engine itself.
    /// </summary>
    public sealed class JavaScriptEngineException : JavaScriptException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptEngineException"/> class. 
        /// </summary>
        /// <param name="_code">The error code returned.</param>
        public JavaScriptEngineException(JavaScriptErrorCode _code) :
            this(_code, "A fatal exception has occurred in a JavaScript runtime")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptEngineException"/> class. 
        /// </summary>
        /// <param name="_code">The error code returned.</param>
        /// <param name="message">The error message.</param>
        public JavaScriptEngineException(JavaScriptErrorCode _code, string message) :
            base(_code, message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptEngineException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        private JavaScriptEngineException(string message, Exception _innerException) :
            base(message, _innerException)
        {
        }
    }
}