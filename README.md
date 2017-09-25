# SharpChakra
[![Licensed under the MIT License](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/mcdis/SharpChakra/blob/master/LICENSE)

Slim managed any cpu (x86/x64) .net wrapper of Microsoft.ChakraCore (https://github.com/Microsoft/ChakraCore)

# How to use
- Install SharpChakra [nuget](https://www.nuget.org/packages/SharpChakra)
- Install SharpChakra.Json [nuget](https://www.nuget.org/packages/SharpChakra.Json) (Json Interop)
- Install SharpChakra.Extensions [nuget](https://www.nuget.org/packages/SharpChakra.Extensions) (Extensions)


# Goals
- any cpu (auto proxy to x86/x64)
- slim (no extra logic, only Api)
- module API support (es6 import/export)
- Json interopability

# Chakra binaries
Microsoft.ChakraCore 1.7.2

# Example

```csharp
using (var runtime = JsRuntime.Create())
using (runtime.CreateContext().Scope())
{
  var fn = new JsNativeFunctionBuilder();
  JsValue.GlobalObject
    .SetProperty("run", // Register run function
      fn.New(() =>Console.WriteLine("hello from script!")),
      true);

  JsContext.RunScript("run();");
}
```
Output: 'hello from script!'

# Newtonsoft.Json Interopability example
```csharp
using (var runtime = JsRuntime.Create())
using (runtime.CreateContext().Scope())
{
  var fn = new JsNativeFunctionBuilder();   
  JsValue.GlobalObject
    .SetProperty("dump", // register dump function
    fn.New(_x =>
    {
      Console.WriteLine("-- dump --");
      Console.WriteLine(_x.Arguments[1].ToJToken().ToString(Formatting.Indented));
      return JObject.Parse("{status:'ok',error:-1}").ToJsValue();
    }),
    true);

  Console.WriteLine("-- executing --");
  var res = JsContext.RunScript("dump({id:4,name:'chakra'});");

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
# Module Api

N | host | Dir | chakracore.dll | Comment
--|------|-----|---------------|-----------
1.| Load top level script | | |
2.| | ---->|JsInitializeModuleRecord | for root module
3.| | ---->|JsParseModuleSource|parse root module
4.| FetchImportedModuleCallback | <---- | | optional for imported module
5.| |----->|JsInitializeModuleRecord| for nested module
6.| load additional script| | |
7.| |----->|  JsParseModuleSource| for nested module
8.| NotifyModuleReadyCallback |<-----| |
9.| |----->|  JsModuleEvaluation|       for root module

```csharp
using (var runtime = JsRuntime.Create(JsRuntimeAttributes.EnableExperimentalFeatures,JsRuntimeVersion.VersionEdge))
using (runtime.CreateContext().Scope())
{
  var fn = new JsNativeFunctionBuilder();   
  JsValue.GlobalObject
    .SetProperty("echo", // register echo func
    fn.New(_x =>Console.WriteLine(_x.Arguments[1].ToString())),true);

  var mainModule = JsModuleRecord.Create(JsModuleRecord.Root,JsValue.FromString("")); // 2. JsInitializeModuleRecord
  var fooModule = JsModuleRecord.Invalid;
  JsErrorCode onFetch(JsModuleRecord _module, JsValue _specifier, out JsModuleRecord _record) // 4. FetchImportedModuleCallback
  {
    fooModule = JsModuleRecord.Create(_module, _specifier); // 2. JsInitializeModuleRecord (foo.js)                   
    _record = fooModule;
    return JsErrorCode.NoError;
  }               

  mainModule.SetHostInfo(onFetch);
  var rootSrc = 
  @"
    import {test} from 'foo.js';
    echo(test());
  ";
  mainModule.Parse(rootSrc); // 3. JsParseModuleSource(root)
  fooModule.Parse("export let test = function(){return 'hello';}"); //3. JsParseModuleSource(foo.js)

  mainModule.Eval(); // 9. JsModuleEvaluation(main)->import {test}->test()->echo(test())->echo('hello')-> hello
}
```
Output: hello

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

# License
Code licensed under the MIT License.
