namespace SharpChakra.Extensions
{
   public static class JsContextExtensions
   {
      public static JsContext.Scope Scope(this JsContext _this)
         => new JsContext.Scope(_this);
   }
}