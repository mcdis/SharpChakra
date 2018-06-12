namespace SharpChakra
{
   public delegate JsErrorCode JsFetchImportedModuleFromScriptCallback(JsSourceContext referencingSourceContext,
      JsValue specifier,
      out JsModuleRecord dependentModuleRecord);
}
