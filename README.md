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
From ChakraRtInterface.h

Function | Status
---------| -------------
JsCreateRuntime | done
JsCreateContext | done
JsSetObjectBeforeCollectCallback | done
JsSetRuntimeMemoryLimit | done
JsSetCurrentContext | done
JsGetCurrentContext | done
JsDisposeRuntime | done
JsCreateObject | done
JsCreateExternalObject | done
JsCreateFunction | done
JsCreateNamedFunction | done
JsSetProperty | done
JsGetGlobalObject | done
JsGetUndefinedValue | done
JsGetNullValue | done
JsGetTrueValue | done
JsGetFalseValue | done
JsConvertValueToString | done
JsConvertValueToNumber | done
JsConvertValueToBoolean | done
JsBooleanToBool | done
JsGetProperty | done
JsHasProperty | done
JsCallFunction | done
JsNumberToDouble | done
JsNumberToInt | done
JsDoubleToNumber | done
JsGetExternalData | done
JsSetExternalData | done
JsCreateArray | done
JsCreateArrayBuffer | done
JsCreateSharedArrayBufferWithSharedContent | -
JsGetSharedArrayBufferContent | -
JsReleaseSharedArrayBufferContentHandle | -
JsGetArrayBufferStorage | done
JsCreateError | done
JsHasException | done
JsSetException | done
JsGetAndClearException | done
JsGetRuntime | done
JsRelease | done
JsAddRef | done
JsGetValueType | done
JsSetIndexedProperty | done
JsSetPromiseContinuationCallback | done
JsGetContextOfObject | done
JsDiagStartDebugging | -
JsDiagStopDebugging | -
JsDiagGetSource | -
JsDiagSetBreakpoint | -
JsDiagGetStackTrace | -
JsDiagRequestAsyncBreak | -
JsDiagGetBreakpoints | -
JsDiagRemoveBreakpoint | -
JsDiagSetBreakOnException | -
JsDiagGetBreakOnException | -
JsDiagSetStepType | -
JsDiagGetScripts | -
JsDiagGetFunctionPosition | -
JsDiagGetStackProperties | -
JsDiagGetProperties | -
JsDiagGetObjectFromHandle | -
JsDiagEvaluate | -
JsParseModuleSource | -
JsModuleEvaluation | -
JsInitializeModuleRecord | -
JsSetModuleHostInfo | -
JsGetModuleHostInfo | -
JsTTDCreateRecordRuntime | -
JsTTDCreateReplayRuntime | -
JsTTDCreateContext | -
JsTTDNotifyContextDestroy | -
JsTTDStart | -
JsTTDStop | -
JsTTDNotifyYield | -
JsTTDHostExit | -
JsTTDGetSnapTimeTopLevelEventMove | -
JsTTDMoveToTopLevelEvent | -
JsTTDReplayExecution | -
JsRun | -
JsParse |- 
JsSerialize | -
JsRunSerialized | -
JsGetStringLength | done
JsCopyString | -
JsCreateString | - 
JsCreateStringUtf16 | - 
JsCreatePropertyId | - 
JsCreateExternalArrayBuffer | done
