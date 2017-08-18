namespace SharpChakra
{
   public delegate JsErrorCode JsFetchImportedModuleFromScriptCallback(JsSourceContext _referencingSourceContext,
      JsValue _specifier,
      out JsModuleRecord _dependentModuleRecord);
}
