using System;

namespace SharpChakra
{
   public class JsException : Exception
   {
      private readonly JsErrorCode _pCode;
      public JsException(JsErrorCode code) :
          this(code, "A fatal exception has occurred in a JavaScript runtime")
      {
      }

      public JsException(JsErrorCode code, string message) :
            base(message)
      {
         _pCode = code;
      }


      protected JsException(string message, Exception innerException) :
               base(message, innerException)
      {
         if (message != null)
         {
            _pCode = (JsErrorCode)HResult;
         }
      }


      public JsErrorCode ErrorCode => _pCode;
   }
}