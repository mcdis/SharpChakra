namespace SharpChakra
{
   public delegate void JsImportedModuleCallBack(JsModuleRecord _referencingModule, JsValue _specifier, out JsModuleRecord _dependentModuleRecord);
}