# SharpChakra
Slim managed any cpu .net wrapper of Microsoft.ChakraCore
Simple auto call proxy to x86/x64 dllimport method based on Microsoft.ChakraCore C# Host Sample

# How to use
- Install SharpChakra [nuget](https://www.nuget.org/packages/SharpChakra)
- Install SharpChakra.Json [nuget](https://www.nuget.org/packages/SharpChakra.Json) (Json Interop)

# Example

```csharp
using (var runtime = JavaScriptRuntime.Create())
using (new JavaScriptContext.Scope(runtime.CreateContext()))
{
   JavaScriptValue.GlobalObject
      .SetProperty(JavaScriptPropertyId.FromString("run"), // Register run function
         JavaScriptValue.CreateFunction((_callee, _call, _arguments, _count, _data) =>
            {
               Console.WriteLine("hello from script!"); // Output
               return JavaScriptValue.Invalid;
            },
            IntPtr.Zero),
         true);

   JavaScriptContext.RunScript("run();");
}
```
Output: 'hello from script!'

# Newtonsoft.Json Interopability example
```csharp
using (var runtime = JavaScriptRuntime.Create())
using (new JavaScriptContext.Scope(runtime.CreateContext()))
{
   // Register Global Function
   JavaScriptValue
      .GlobalObject
      .SetProperty(JavaScriptPropertyId.FromString("dump"), // function name
      JavaScriptValue.CreateFunction((_callee, _call, _arguments, _count, _data) =>
         {
            Console.WriteLine("-- dump --");
            Console.WriteLine(_arguments[1].ToJToken().ToString(Formatting.Indented));
            return JObject.Parse("{status:'ok',error:-1}").ToJavaScriptValue();
         },
         IntPtr.Zero),
      true);

   Console.WriteLine("-- executing --");
   var res = JavaScriptContext.RunScript("dump({id:4,name:'chakra'});");

   Console.WriteLine("-- result --");
   Console.WriteLine(res.ToJToken().ToString(Formatting.Indented));
}
```
Output:
```
-- executing --
-- dump --
{
  "id": 4,
  "name": "chakra"
}
-- result --
{
  "status": "ok",
  "error": -1
}
```

# Api coverage

**JsrtCommonExports.inc**
Function | Status
---------| -------------
JsSetObjectBeforeCollectCallback | done
JsCreateRuntime | done
JsCollectGarbage | done
JsDisposeRuntime | done
JsAddRef | done
JsRelease | done
JsCreateContext | done
JsGetCurrentContext | done
JsSetCurrentContext | done
JsGetContextOfObject | done
JsRunScript | done
JsGetUndefinedValue | done
JsGetNullValue | done
JsGetTrueValue | done
JsGetFalseValue | done
JsBoolToBoolean | done
JsBooleanToBool | done
JsConvertValueToBoolean | done
JsGetValueType | done
JsDoubleToNumber | done
JsIntToNumber | done
JsNumberToDouble | done
JsNumberToInt | done
JsConvertValueToNumber | done
JsPointerToString | done
JsStringToPointer | done
JsConvertValueToString | done
JsGetGlobalObject | done
JsCreateObject | done
JsCreateExternalObject | done
JsConvertValueToObject | done
JsGetPrototype | done
JsSetPrototype | done
JsInstanceOf | done
JsGetExtensionAllowed | done
JsPreventExtension | done
JsGetProperty | done
JsGetOwnPropertyDescriptor | done
JsSetProperty | done
JsHasProperty | done
JsDeleteProperty | done
JsDefineProperty | done
JsCreateArray | done
JsCreateArrayBuffer | done
JsCreateExternalArrayBuffer | done
JsCreateTypedArray | done
JsCreateDataView | done
JsGetTypedArrayInfo | done 
JsGetArrayBufferStorage | done
JsGetTypedArrayStorage | done
JsGetDataViewStorage | done
JsHasIndexedProperty | done
JsGetIndexedProperty | done
JsSetIndexedProperty | done
JsDeleteIndexedProperty | done
JsHasIndexedPropertiesExternalData | done
JsGetIndexedPropertiesExternalData | done
JsSetIndexedPropertiesToExternalData | done
JsEquals | done
JsStrictEquals | done
JsHasExternalData | done
JsGetExternalData | done
JsSetExternalData | done
JsCallFunction | done
JsCreateFunction | done
JsCreateNamedFunction | done
JsCreateError | done
JsCreateRangeError | done
JsCreateReferenceError | done
JsCreateSyntaxError | done
JsCreateTypeError | done
JsCreateURIError | done
JsHasException | done
JsGetAndClearException | done
JsSetException | done
JsGetRuntimeMemoryUsage | done
JsGetRuntimeMemoryLimit | done
JsSetRuntimeMemoryLimit | done
JsSetRuntimeMemoryAllocationCallback | done
JsSetRuntimeBeforeCollectCallback | done
JsGetStringLength | done
JsDisableRuntimeExecution | done
JsEnableRuntimeExecution | done
JsIsRuntimeExecutionDisabled | done
JsSerializeScript | done
JsParseSerializedScript | done
JsRunSerializedScript | done
JsParseSerializedScriptWithCallback | done
JsRunSerializedScriptWithCallback | done
JsParseScript | done
JsParseScriptWithAttributes | done
JsConstructObject | done
JsGetPropertyIdFromName | done
JsGetPropertyNameFromId | done
JsGetPropertyIdType | done
JsGetOwnPropertyNames | done
JsGetPropertyIdFromSymbol | done
JsGetSymbolFromPropertyId | done
JsCreateSymbol | done
JsGetOwnPropertySymbols | done
JsGetRuntime | done
JsIdle | done
JsSetPromiseContinuationCallback | done

**ChakraCore.def**
Function | Status
---------| -------------
JsDiagEvaluate | -
JsDiagGetBreakOnException | -
JsDiagGetBreakpoints | -
JsDiagGetFunctionPosition | -
JsDiagGetProperties | -
JsDiagGetScripts | -
JsDiagGetSource | -
JsDiagGetStackProperties | -
JsDiagGetStackTrace | -
JsDiagGetObjectFromHandle | -
JsDiagRemoveBreakpoint | -
JsDiagRequestAsyncBreak | -
JsDiagSetBreakOnException | -
JsDiagSetBreakpoint | -
JsDiagSetStepType | -
JsDiagStartDebugging | -
JsDiagStopDebugging | -

JsTTDCreateRecordRuntime | -
JsTTDCreateReplayRuntime | -
JsTTDCreateContext | -
JsTTDNotifyContextDestroy | -
JsTTDStart | -
JsTTDStop | -

JsTTDPauseTimeTravelBeforeRuntimeOperation | -
JsTTDReStartTimeTravelAfterRuntimeOperation | -

JsTTDNotifyYield | -
JsTTDNotifyLongLivedReferenceAdd | -
JsTTDHostExit | -
JsTTDRawBufferCopySyncIndirect | -
JsTTDRawBufferModifySyncIndirect | -
JsTTDRawBufferAsyncModificationRegister | -
JsTTDRawBufferAsyncModifyComplete | -
JsTTDCheckAndAssertIfTTDRunning | -
JsTTDGetSnapTimeTopLevelEventMove | -
JsTTDGetSnapShotBoundInterval | -
JsTTDGetPreviousSnapshotInterval | -
JsTTDPreExecuteSnapShotInterval | -
JsTTDMoveToTopLevelEvent | -
JsTTDReplayExecution | -

JsInitializeModuleRecord | done
JsParseModuleSource | done
JsModuleEvaluation | done
JsSetModuleHostInfo | done
JsGetModuleHostInfo | -
JsInitializeJITServer | -

JsCreateSharedArrayBufferWithSharedContent | -
JsGetSharedArrayBufferContent | -
JsReleaseSharedArrayBufferContentHandle | -