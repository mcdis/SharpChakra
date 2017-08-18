using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
   public static class JTokenExtensions
   {
      public static JsValue ToJavaScriptValue(this JToken _this)
         => JTokenToJsValueConverter.Convert(_this);
   }
}