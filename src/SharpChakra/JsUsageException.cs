using System;

namespace SharpChakra
{
   public sealed class JsUsageException : JsException
   {
      public JsUsageException(JsErrorCode _code) :
          this(_code, "A fatal exception has occurred in a JavaScript runtime")
      {
      }
      public JsUsageException(JsErrorCode _code, string _message) :
          base(_code, _message)
      {
      }
      private JsUsageException(string _message, Exception _innerException) :
          base(_message, _innerException)
      {
      }
   }
}