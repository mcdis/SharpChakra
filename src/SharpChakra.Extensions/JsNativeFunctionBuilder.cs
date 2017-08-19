using System;
using System.Collections.Generic;

namespace SharpChakra.Extensions
{
   public class JsNativeFunctionBuilder
   {
      private readonly List<JsNativeFunction> p_refs;
      public JsNativeFunctionBuilder()
      {
         p_refs = new List<JsNativeFunction>();
      }
      public JsValue New(JsNativeFunction _handler)
      {
         p_refs.Add(_handler);
         return JsValue.CreateFunction(_handler);
      }
      public JsValue New(Action<JsNativeFunctionArgs> _handler)
         => New((_callee, _isConstructCall, _arguments, _argumentCount, _callbackData) =>
         {
            _handler(new JsNativeFunctionArgs
            {
               Callee = _callee,
               IsConstructCall = _isConstructCall,
               Arguments = _arguments,
               ArgumentCount = _argumentCount,
               CallbackData = _callbackData
            });
            return JsValue.Undefined;
         });
      public JsValue New(Func<JsNativeFunctionArgs, JsValue> _handler)
         => New((_callee, _isConstructCall, _arguments, _argumentCount, _callbackData)
            => _handler(new JsNativeFunctionArgs
            {
               Callee = _callee,
               IsConstructCall = _isConstructCall,
               Arguments = _arguments,
               ArgumentCount = _argumentCount,
               CallbackData = _callbackData
            }));
   }
}