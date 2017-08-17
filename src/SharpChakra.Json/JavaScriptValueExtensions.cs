using Newtonsoft.Json.Linq;

namespace SharpChakra.Json
{
   public static class JavaScriptValueExtensions
   {
      public static JToken ToJToken(this JavaScriptValue _this)
         => JavaScriptValueToJTokenConverter.Convert(_this);
   }
}