using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
    public static class JTokenExtensions
    {
        public static JsValue ToJsValue(this JToken _this, JsContext context)
            => new JTokenToJsValueConverter(context).Convert(_this);
    }
}