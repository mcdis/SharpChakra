using System;

namespace SharpChakra
{
   public sealed class JsScriptException : JsException
   {
      private readonly JsValue p_error;
      public JsScriptException(JsErrorCode _code, JsValue _error) :
          this(_code, _error, "JavaScript Exception")
      {
      }
      public JsScriptException(JsErrorCode _code, JsValue _error, string message) :
          base(_code, message)
      {
         p_error = _error;
      }

      private JsScriptException(string message, Exception _innerException) :
          base(message, _innerException)
      {
      }
      public JsValue Error => p_error;
   }
}