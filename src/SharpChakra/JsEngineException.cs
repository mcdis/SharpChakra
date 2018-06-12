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
        /// <param name="code">The error code returned.</param>
        public JsEngineException(JsErrorCode code) :
            this(code, "A fatal exception has occurred in a JavaScript runtime")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsEngineException"/> class. 
        /// </summary>
        /// <param name="code">The error code returned.</param>
        /// <param name="message">The error _message.</param>
        public JsEngineException(JsErrorCode code, string message) :
            base(code, message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JsEngineException"/> class.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        private JsEngineException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}