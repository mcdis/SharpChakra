using System;

namespace SharpChakra
{
   /// <summary>
    ///     An exception that occurred in the workings of the JavaScript engine itself.
    /// </summary>
    public sealed class JsEngineException : JsException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="JsEngineException"/> class. 
        /// </summary>
        /// <param name="_code">The error code returned.</param>
        public JsEngineException(JsErrorCode _code) :
            this(_code, "A fatal exception has occurred in a JavaScript runtime")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsEngineException"/> class. 
        /// </summary>
        /// <param name="_code">The error code returned.</param>
        /// <param name="_message">The error _message.</param>
        public JsEngineException(JsErrorCode _code, string _message) :
            base(_code, _message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsEngineException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        private JsEngineException(string message, Exception _innerException) :
            base(message, _innerException)
        {
        }
    }
}