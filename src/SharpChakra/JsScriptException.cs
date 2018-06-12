using System;

namespace SharpChakra
{
   public sealed class JsScriptException : JsException
   {
      private readonly JsValue _pError;
      public JsScriptException(JsErrorCode code, JsValue error) :
          this(code, error, "JavaScript Exception")
      {
      }
      public JsScriptException(JsErrorCode code, JsValue error, string message) :
          base(code, message)
      {
         _pError = error;
      }

      private JsScriptException(string message, Exception innerException) :
          base(message, innerException)
      {
      }
      public JsValue Error => _pError;
   }
}