using System;

namespace SharpChakra
{
    public class JsException : Exception
    {
        public JsException(JsErrorCode code) :
            this(code, "A fatal exception has occurred in a JavaScript runtime")
        {
        }

        public JsException(JsErrorCode code, string message) :
            base(message)
        {
            ErrorCode = code;
        }


        protected JsException(string message, Exception innerException) :
            base(message, innerException)
        {
            if (message != null)
            {
                ErrorCode = (JsErrorCode) HResult;
            }
        }


        public JsErrorCode ErrorCode { get; }
    }
}