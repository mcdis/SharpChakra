namespace SharpChakra
{
    public delegate JsErrorCode JsFetchImportedModuleCallBack(JsModuleRecord referencingModule, JsValue specifier,
        out JsModuleRecord dependentModuleRecord);
}