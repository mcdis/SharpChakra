using System;
using System.Threading.Tasks;

namespace SharpChakra.Extensions
{
    public static class JsContextExtensions
    {
        public static Task SetGlobalAsync(this JsContext context, string name, object value)
        {
            return context.RequestScopeAsync(session => session.Global.SetProperty(name, JsValue.FromObject(value)));
        }

        public static Task DeclareFunctionAsync(this JsContext context, string name, Action action)
        {
            return context.RequestScopeAsync(session => session.Global.SetProperty(name, JsValue.FromDelegate(action)));
        }

        public static Task DeclareFunctionAsync(this JsContext context, string name, Func<object> func)
        {
            return context.RequestScopeAsync(session => session.Global.SetProperty(name, JsValue.FromDelegate(() => JsValue.FromObject(func()))));
        }

        public static Task<JsValue> GetGlobalAsync(this JsContext context, string name)
        {
            return context.RequestScopeAsync(session => session.Global.GetProperty(name));
        }

        public static Task GetGlobalAsync<T>(this JsContext context, string name)
        {
            return context.RequestScopeAsync(session => session.Global.GetProperty(name).ToObject<T>());
        }

        public static Task<JsValue> EvalAsync(this JsContext context, string script, JsSourceContext sourceContext, string sourceName)
        {
            return context.RequestScopeAsync(session => session.RunScript(script, sourceContext, sourceName));
        }

        public static Task<JsValue> EvalAsync(this JsContext context, string script, byte[] buffer, JsSourceContext sourceContext, string sourceName)
        {
            return context.RequestScopeAsync(session => session.RunScript(script, buffer, sourceContext, sourceName));
        }

        public static Task<JsValue> EvalAsync(this JsContext context, string script)
        {
            return EvalAsync(context, script, JsSourceContext.None, string.Empty);
        }

        public static Task<JsValue> EvalAsync(this JsContext context, string script, byte[] buffer)
        {
            return EvalAsync(context, script, buffer, JsSourceContext.None, string.Empty);
        }

        public static Task<T> EvalAsync<T>(this JsContext context, string script, JsSourceContext sourceContext, string sourceName)
        {
            return context.RequestScopeAsync(session => session.RunScript(script, sourceContext, sourceName).ToObject<T>());
        }

        public static Task<T> EvalAsync<T>(this JsContext context, string script, byte[] buffer, JsSourceContext sourceContext, string sourceName)
        {
            return context.RequestScopeAsync(session => session.RunScript(script, buffer, sourceContext, sourceName).ToObject<T>());
        }

        public static Task<T> EvalAsync<T>(this JsContext context, string script)
        {
            return EvalAsync<T>(context, script, JsSourceContext.None, string.Empty);
        }

        public static Task<T> EvalAsync<T>(this JsContext context, string script, byte[] buffer)
        {
            return EvalAsync<T>(context, script, buffer, JsSourceContext.None, string.Empty);
        }
    }
}
