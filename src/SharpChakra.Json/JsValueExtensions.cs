using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
    public static class JsValueExtensions
    {
        public static JToken ToJToken(this JsValue _this)
            => JsValueToJTokenConverter.Convert(_this);
    }
}