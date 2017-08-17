using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
   public static class JTokenExtensions
   {
      public static JavaScriptValue ToJavaScriptValue(this JToken _this)
         => JTokenToJavaScriptValueConverter.Convert(_this);
   }
}