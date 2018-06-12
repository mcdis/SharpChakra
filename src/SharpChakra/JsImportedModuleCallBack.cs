namespace SharpChakra
{
    public delegate void JsImportedModuleCallBack(JsModuleRecord referencingModule, JsValue specifier,
        out JsModuleRecord dependentModuleRecord);
}