using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpChakra.Extensions
{
    public static class JsFunctionsExtensions
    {
        public static JsValue CreateFunction(this JsContext context, Action x)
        {
            return context.CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) =>
            {
                x();

                return context.CreateUndefined();
            });
        }

        public static JsValue CreateFunction(this JsContext context, Func<JsValue> func)
        {
            return context.CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) => func());
        }

        public static JsValue CreateFunction(this JsContext context, Action<JsNativeFunctionArgs> handler)
        {
            return context.CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) =>
            {
                var args = new JsNativeFunctionArgs
                {
                    Callee = callee,
                    IsConstructCall = isConstructCall,
                    Arguments = arguments,
                    ArgumentCount = argumentCount,
                    CallbackData = callbackData
                };

                handler(args);

                return context.CreateUndefined();
            });
        }

        public static JsValue CreateFunction(this JsContext context, Func<JsNativeFunctionArgs, JsValue> handler)
        {
            return context.CreateFunction((callee, isConstructCall, arguments, argumentCount, callbackData) =>
            {
                var args = new JsNativeFunctionArgs
                {
                    Callee = callee,
                    IsConstructCall = isConstructCall,
                    Arguments = arguments,
                    ArgumentCount = argumentCount,
                    CallbackData = callbackData
                };

                return handler(args);
            });
        }
    }
}
