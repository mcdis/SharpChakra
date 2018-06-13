using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
    public static class JTokenExtensions
    {
        public static JsValue ToJsValue(this JToken _this, JsContext context)
            => new JTokenToJsValueConverter(context).Visit(_this);

        public static JToken ToJToken(this JsValue _this, JsContext context)
            => new JsValueToJTokenConverter(context).Visit(_this);
    }
}