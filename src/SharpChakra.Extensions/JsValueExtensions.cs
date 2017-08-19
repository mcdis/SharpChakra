namespace SharpChakra.Extensions
{
   public static class JsValueExtensions
   {
      public static JsValue SetProperty(this JsValue _this, string _propertyId, JsValue _value, bool _useStrictRules)
      {
         _this.SetProperty(JsPropertyId.FromString(_propertyId), _value, _useStrictRules);
         return _this;
      }
   }
}