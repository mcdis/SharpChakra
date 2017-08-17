using System;

namespace SharpChakra
{
   /// <summary>
    ///     An exception returned from the Chakra engine.
    /// </summary>
    public class JavaScriptException : Exception
    {
        /// <summary>
        /// The error code.
        /// </summary>
        private readonly JavaScriptErrorCode p_code;

        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptException"/> class. 
        /// </summary>
        /// <param name="_code">The error code returned.</param>
        public JavaScriptException(JavaScriptErrorCode _code) :
            this(_code, "A fatal exception has occurred in a JavaScript runtime")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptException"/> class. 
        /// </summary>
        /// <param name="_code">The error code returned.</param>
        /// <param name="message">The error message.</param>
        public JavaScriptException(JavaScriptErrorCode _code, string message) :
            base(message)
        {
            this.p_code = _code;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="JavaScriptException"/> class. 
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        protected JavaScriptException(string message, Exception _innerException) :
            base(message, _innerException)
        {
            if (message != null)
            {
                p_code = (JavaScriptErrorCode) base.HResult;
            }
        }

        /*
        /// <summary>
        ///     Serializes the exception information.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("code", (uint)code);
        }
        */
        /// <summary>
        ///     Gets the error code.
        /// </summary>
        public JavaScriptErrorCode ErrorCode
        {
            get { return p_code; }
        }
    }
}