using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
   public static class JsValueExtensions
   {
      public static JToken ToJToken(this JsValue _this)
         => JavaScriptValueToJTokenConverter.Convert(_this);
   }
}