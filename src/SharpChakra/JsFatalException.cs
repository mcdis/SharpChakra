using System;

namespace SharpChakra
{
    public sealed class JsFatalException : JsException
    {
        public JsFatalException(JsErrorCode _code) :
            this(_code, "A fatal exception has occurred in a JavaScript runtime")
        {
        }
        public JsFatalException(JsErrorCode _code, string _message) :
            base(_code, _message)
        {
        }

        private JsFatalException(string _message, Exception _innerException) :
            base(_message, _innerException)
        {
        }
    }
}