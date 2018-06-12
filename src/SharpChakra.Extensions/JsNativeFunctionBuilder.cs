using System;
using System.Collections.Generic;

namespace SharpChakra.Extensions
{
   public class JsNativeFunctionBuilder
   {
      private readonly List<JsNativeFunction> _pRefs;
      public JsNativeFunctionBuilder()
      {
         _pRefs = new List<JsNativeFunction>();
      }
      public JsValue New(JsNativeFunction handler)
      {
         _pRefs.Add(handler);
         return JsValue.CreateFunction(handler);
      }
      public JsValue New(Action x) => New((callee, isConstructCall, arguments, argumentCount, callbackData) =>
      {
         x();
         return JsValue.Undefined;
      });
      public JsValue New(Func<JsValue> x) => New((callee, isConstructCall, arguments, argumentCount, callbackData) => x());
      public JsValue New(Action<JsNativeFunctionArgs> handler)
         => New((callee, isConstructCall, arguments, argumentCount, callbackData) =>
         {
            handler(new JsNativeFunctionArgs
            {
               Callee = callee,
               IsConstructCall = isConstructCall,
               Arguments = arguments,
               ArgumentCount = argumentCount,
               CallbackData = callbackData
            });
            return JsValue.Undefined;
         });
      public JsValue New(Func<JsNativeFunctionArgs, JsValue> handler)
         => New((callee, isConstructCall, arguments, argumentCount, callbackData)
            => handler(new JsNativeFunctionArgs
            {
               Callee = callee,
               IsConstructCall = isConstructCall,
               Arguments = arguments,
               ArgumentCount = argumentCount,
               CallbackData = callbackData
            }));
   }
}