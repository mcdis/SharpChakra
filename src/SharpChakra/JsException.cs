using System;

namespace SharpChakra
{
   public class JsException : Exception
   {
      private readonly JsErrorCode p_code;
      public JsException(JsErrorCode _code) :
          this(_code, "A fatal exception has occurred in a JavaScript runtime")
      {
      }

      public JsException(JsErrorCode _code, string _message) :
            base(_message)
      {
         p_code = _code;
      }


      protected JsException(string message, Exception _innerException) :
               base(message, _innerException)
      {
         if (message != null)
         {
            p_code = (JsErrorCode)HResult;
         }
      }


      public JsErrorCode ErrorCode => p_code;
   }
}