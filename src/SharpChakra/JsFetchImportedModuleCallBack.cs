namespace SharpChakra
{
   public delegate JsErrorCode JsFetchImportedModuleCallBack(JsModuleRecord _referencingModule, JsValue _specifier,out JsModuleRecord _dependentModuleRecord);
}
