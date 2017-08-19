namespace SharpChakra.Extensions
{
   public static class JsValueExtensions
   {
      public static JsValue SetProperty(this JsValue _this, string _propertyId, JsValue _value, bool _useStrictRules)
      {
         _this.SetProperty(JsPropertyId.FromString(_propertyId), _value, _useStrictRules);
         return _this;
      }
      public static JsValue GetProperty(this JsValue _this, string _propertyId)
         => _this.GetProperty(JsPropertyId.FromString(_propertyId));
      public static bool HasProperty(this JsValue _this, string _propertyId)
         => _this.HasProperty(JsPropertyId.FromString(_propertyId));
   }
}