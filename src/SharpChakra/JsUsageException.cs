using System;

namespace SharpChakra
{
    public sealed class JsUsageException : JsException
    {
        public JsUsageException(JsErrorCode code) :
            this(code, "A fatal exception has occurred in a JavaScript runtime")
        {
        }

        public JsUsageException(JsErrorCode code, string message) :
            base(code, message)
        {
        }

        private JsUsageException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}