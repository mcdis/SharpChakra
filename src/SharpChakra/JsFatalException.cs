using System;

namespace SharpChakra
{
    public sealed class JsFatalException : JsException
    {
        public JsFatalException(JsErrorCode code) :
            this(code, "A fatal exception has occurred in a JavaScript runtime")
        {
        }
        public JsFatalException(JsErrorCode code, string message) :
            base(code, message)
        {
        }

        private JsFatalException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}