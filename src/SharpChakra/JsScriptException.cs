using System;

namespace SharpChakra
{
    public sealed class JsScriptException : JsException
    {
        public JsScriptException(JsErrorCode code, JsValue error) :
            this(code, error, "JavaScript Exception")
        {
        }

        public JsScriptException(JsErrorCode code, JsValue error, string message) :
            base(code, message)
        {
            Error = error;
        }

        private JsScriptException(string message, Exception innerException) :
            base(message, innerException)
        {
        }

        public JsValue Error { get; }
    }
}