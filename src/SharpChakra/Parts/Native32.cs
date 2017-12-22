using System;
using System.Runtime.InteropServices;

namespace SharpChakra.Parts
{
   /// <summary>
   ///     Native interfaces.
   /// </summary>
   static class Native32
   {
      const string DllName = @"x86\ChakraCore.dll";
      /// <summary>
      /// Throws if a native method returns an error code.
      /// </summary>
      /// <param name="_error">The error.</param>
      internal static void ThrowIfError(JsErrorCode _error)
      {
         if (_error != JsErrorCode.NoError)
         {
            switch (_error)
            {
               case JsErrorCode.InvalidArgument:   
                  throw new JsUsageException(_error, "Invalid argument.");

               case JsErrorCode.NullArgument:
                  throw new JsUsageException(_error, "Null argument.");

               case JsErrorCode.NoCurrentContext:
                  throw new JsUsageException(_error, "No current context.");

               case JsErrorCode.InExceptionState:
                  throw new JsUsageException(_error, "Runtime is in exception state.");

               case JsErrorCode.NotImplemented:
                  throw new JsUsageException(_error, "Method is not implemented.");

               case JsErrorCode.WrongThread:
                  throw new JsUsageException(_error, "Runtime is active on another thread.");

               case JsErrorCode.RuntimeInUse:
                  throw new JsUsageException(_error, "Runtime is in use.");

               case JsErrorCode.BadSerializedScript:
                  throw new JsUsageException(_error, "Bad serialized script.");

               case JsErrorCode.InDisabledState:
                  throw new JsUsageException(_error, "Runtime is disabled.");

               case JsErrorCode.CannotDisableExecution:
                  throw new JsUsageException(_error, "Cannot disable execution.");

               case JsErrorCode.AlreadyDebuggingContext:
                  throw new JsUsageException(_error, "Context is already in debug mode.");

               case JsErrorCode.HeapEnumInProgress:
                  throw new JsUsageException(_error, "Heap enumeration is in progress.");

               case JsErrorCode.ArgumentNotObject:
                  throw new JsUsageException(_error, "Argument is not an object.");

               case JsErrorCode.InProfileCallback:
                  throw new JsUsageException(_error, "In a profile callback.");

               case JsErrorCode.InThreadServiceCallback:
                  throw new JsUsageException(_error, "In a thread service callback.");

               case JsErrorCode.CannotSerializeDebugScript:
                  throw new JsUsageException(_error, "Cannot serialize a debug script.");

               case JsErrorCode.AlreadyProfilingContext:
                  throw new JsUsageException(_error, "Already profiling this context.");

               case JsErrorCode.IdleNotEnabled:
                  throw new JsUsageException(_error, "Idle is not enabled.");

               case JsErrorCode.OutOfMemory:
                  throw new JsEngineException(_error, "Out of memory.");

               case JsErrorCode.ScriptException:
                  {
                     var innerError = JsGetAndClearException(out var errorObject);

                     if (innerError != JsErrorCode.NoError)
                     {
                        throw new JsFatalException(innerError);
                     }

                     throw new JsScriptException(_error, errorObject, "Script threw an exception.");
                  }

               case JsErrorCode.ScriptCompile:
                  {
                     var innerError = JsGetAndClearException(out var errorObject);

                     if (innerError != JsErrorCode.NoError)
                     {
                        throw new JsFatalException(innerError);
                     }

                     throw new JsScriptException(_error, errorObject, "Compile error.");
                  }

               case JsErrorCode.ScriptTerminated:
                  throw new JsScriptException(_error, JsValue.Invalid, "Script was terminated.");

               case JsErrorCode.ScriptEvalDisabled:
                  throw new JsScriptException(_error, JsValue.Invalid, "Eval of strings is disabled in this runtime.");

               case JsErrorCode.Fatal:
                  throw new JsFatalException(_error);

               default:
                  throw new JsFatalException(_error);
            }
         }
      }
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateRuntime(JsRuntimeAttributes _attributes,
         JavaScriptThreadServiceCallback _threadService,
         out JsRuntime _runtime);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCollectGarbage(JsRuntime _handle);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsDisposeRuntime(JsRuntime _handle);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetRuntimeMemoryUsage(JsRuntime _runtime, out UIntPtr _memoryUsage);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetRuntimeMemoryLimit(JsRuntime _runtime, out UIntPtr _memoryLimit);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetRuntimeMemoryLimit(JsRuntime _runtime, UIntPtr _memoryLimit);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetRuntimeMemoryAllocationCallback(JsRuntime _runtime,
         IntPtr _callbackState,
         JsMemoryAllocationCallback _allocationCallback);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetRuntimeBeforeCollectCallback(JsRuntime _runtime,
         IntPtr _callbackState,
         JsBeforeCollectCallback _beforeCollectCallback);
      [DllImport(DllName, EntryPoint = "JsAddRef")]
      internal static extern JsErrorCode JsContextAddRef(JsContext _reference, out uint _count);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsAddRef(JsValue _reference, out uint _count);
      [DllImport(DllName, EntryPoint = "JsRelease")]
      internal static extern JsErrorCode JsContextRelease(JsContext _reference, out uint _count);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsRelease(JsValue _reference, out uint _count);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateContext(JsRuntime _runtime, out JsContext _newContext);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetCurrentContext(out JsContext _currentContext);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetCurrentContext(JsContext _context);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetRuntime(JsContext _context, out JsRuntime _runtime);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsIdle(out uint _nextIdleTick);
      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JsErrorCode JsParseScript(string _script,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result);
      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JsErrorCode JsRunScript(string _script,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result);
      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JsErrorCode JsSerializeScript(string _script, byte[] _buffer, ref ulong _bufferSize);
      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JsErrorCode JsParseSerializedScript(string _script,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result);
      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JsErrorCode JsRunSerializedScript(string _script,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result);
      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JsErrorCode JsGetPropertyIdFromName(string _name, out JsPropertyId _propertyId);
      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JsErrorCode JsGetPropertyNameFromId(JsPropertyId _propertyId, out string _name);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetUndefinedValue(out JsValue _undefinedValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetNullValue(out JsValue _nullValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetTrueValue(out JsValue _trueValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetFalseValue(out JsValue _falseValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsBoolToBoolean(bool _value, out JsValue _booleanValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsBooleanToBool(JsValue _booleanValue, out bool _boolValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsConvertValueToBoolean(JsValue _value, out JsValue _booleanValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetValueType(JsValue _value, out JsValueType _type);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsDoubleToNumber(double _doubleValue, out JsValue _value);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsIntToNumber(int _intValue, out JsValue _value);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsNumberToDouble(JsValue _value, out double _doubleValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsConvertValueToNumber(JsValue _value, out JsValue _numberValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetStringLength(JsValue _sringValue, out int _length);
      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JsErrorCode JsPointerToString(string _value, UIntPtr _stringLength, out JsValue _stringValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsStringToPointer(JsValue _value, out IntPtr _stringValue, out UIntPtr _stringLength);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsConvertValueToString(JsValue _value, out JsValue _stringValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetGlobalObject(out JsValue _globalObject);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateObject(out JsValue _obj);
      [DllImport(DllName)]
      internal static extern JsErrorCode
         JsCreateExternalObject(IntPtr _data, JsObjectFinalizeCallback _finalizeCallback, out JsValue _obj);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsConvertValueToObject(JsValue _value, out JsValue _obj);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetPrototype(JsValue _obj, out JsValue _prototypeObject);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetPrototype(JsValue _obj, JsValue _prototypeObject);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetExtensionAllowed(JsValue _obj, out bool _value);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsPreventExtension(JsValue _obj);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetProperty(JsValue _obj, JsPropertyId _propertyId, out JsValue _value);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetOwnPropertyDescriptor(JsValue _obj,
         JsPropertyId _propertyId,
         out JsValue _propertyDescriptor);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetOwnPropertyNames(JsValue _obj, out JsValue _propertyNames);
      [DllImport(DllName)]
      internal static extern JsErrorCode
         JsSetProperty(JsValue _obj, JsPropertyId _propertyId, JsValue _value, bool _useStrictRules);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsHasProperty(JsValue _obj, JsPropertyId _propertyId, out bool _hasProperty);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsDeleteProperty(JsValue _obj,
         JsPropertyId _propertyId,
         bool _useStrictRules,
         out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsDefineProperty(JsValue _obj,
         JsPropertyId _propertyId,
         JsValue _propertyDescriptor,
         out bool _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsHasIndexedProperty(JsValue _obj, JsValue _index, out bool _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetIndexedProperty(JsValue _obj, JsValue _index, out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetIndexedProperty(JsValue _obj, JsValue _index, JsValue _value);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsDeleteIndexedProperty(JsValue _obj, JsValue _index);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsEquals(JsValue _obj1, JsValue _obj2, out bool _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsStrictEquals(JsValue _obj1, JsValue _obj2, out bool _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsHasExternalData(JsValue _obj, out bool _value);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetExternalData(JsValue _obj, out IntPtr _externalData);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetExternalData(JsValue _obj, IntPtr _externalData);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateArray(uint _length, out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCallFunction(JsValue _function,
         JsValue[] _arguments,
         ushort _argumentCount,
         out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsConstructObject(JsValue _function,
         JsValue[] _arguments,
         ushort _argumentCount,
         out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateFunction(JsNativeFunction _nativeFunction, IntPtr _externalData, out JsValue _function);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateError(JsValue _message, out JsValue _error);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateRangeError(JsValue _message, out JsValue _error);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateReferenceError(JsValue _message, out JsValue _error);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateSyntaxError(JsValue _message, out JsValue _error);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateTypeError(JsValue _message, out JsValue _error);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateURIError(JsValue _message, out JsValue _error);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsHasException(out bool _hasException);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetAndClearException(out JsValue _exception);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetException(JsValue _exception);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsDisableRuntimeExecution(JsRuntime _runtime);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsEnableRuntimeExecution(JsRuntime _runtime);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsIsRuntimeExecutionDisabled(JsRuntime _runtime, out bool _isDisabled);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetObjectBeforeCollectCallback(JsValue _reference,
         IntPtr _callbackState,
         JsObjectBeforeCollectCallback _beforeCollectCallback);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateNamedFunction(JsValue _name,
         JsNativeFunction _nativeFunction,
         IntPtr _callbackState,
         out JsValue _function);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetPromiseContinuationCallback(JsPromiseContinuationCallback _promiseContinuationCallback,
         IntPtr _callbackState);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateArrayBuffer(uint _byteLength, out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateTypedArray(JavaScriptTypedArrayType _arrayType,
         JsValue _arrayBuffer,
         uint _byteOffset,
         uint _elementLength,
         out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateDataView(JsValue _arrayBuffer,
         uint _byteOffset,
         uint _byteOffsetLength,
         out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetArrayBufferStorage(JsValue _arrayBuffer, out IntPtr _buffer, out uint _bufferLength);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetTypedArrayStorage(JsValue _typedArray,
         out IntPtr _buffer,
         out uint _bufferLength,
         out JavaScriptTypedArrayType _arrayType,
         out int _elementSize);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetDataViewStorage(JsValue _dataView, out IntPtr _buffer, out uint _bufferLength);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetPropertyIdType(JsPropertyId _propertyId, out JsPropertyIdType _propertyIdType);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateSymbol(JsValue _description, out JsValue _symbol);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetSymbolFromPropertyId(JsPropertyId _propertyId, out JsValue _symbol);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetPropertyIdFromSymbol(JsValue _symbol, out JsPropertyId _propertyId);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetOwnPropertySymbols(JsValue _obj, out JsValue _propertySymbols);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsNumberToInt(JsValue _value, out int _intValue);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetIndexedPropertiesToExternalData(JsValue _obj,
         IntPtr _data,
         JavaScriptTypedArrayType _arrayType,
         uint _elementLength);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetIndexedPropertiesExternalData(JsValue _obj,
         IntPtr _data,
         out JavaScriptTypedArrayType _arrayType,
         out uint _elementLength);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsHasIndexedPropertiesExternalData(JsValue _obj, out bool _value);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsInstanceOf(JsValue _obj, JsValue _constructor, out bool _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsCreateExternalArrayBuffer(IntPtr _data,
         uint _byteLength,
         JsObjectFinalizeCallback _finalizeCallback,
         IntPtr _callbackState,
         out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetTypedArrayInfo(JsValue _typedArray,
         out JavaScriptTypedArrayType _arrayType,
         out JsValue _arrayBuffer,
         out uint _byteOffset,
         out uint _byteLength);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetContextOfObject(JsValue _obj, out JsContext _context);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsGetContextData(JsContext _context, out IntPtr _data);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsSetContextData(JsContext _context, IntPtr _data);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsParseSerializedScriptWithCallback(JavaScriptSerializedScriptLoadSourceCallback _scriptLoadCallback,
         JavaScriptSerializedScriptUnloadCallback _scriptUnloadCallback,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsRunSerializedScriptWithCallback(JavaScriptSerializedScriptLoadSourceCallback _scriptLoadCallback,
         JavaScriptSerializedScriptUnloadCallback _scriptUnloadCallback,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsInitializeModuleRecord(
         JsModuleRecord _referencingModule,
         JsValue _normalizedSpecifier,
         out JsModuleRecord _moduleRecord);

      [DllImport(DllName, EntryPoint = "JsSetModuleHostInfo")]
      internal static extern JsErrorCode JsSetModuleHostInfo(JsModuleRecord _requestModule,
         JsModuleHostInfoKind _moduleHostInfo,
         JsFetchImportedModuleCallBack _callback);
      [DllImport(DllName, EntryPoint = "JsSetModuleHostInfo")]
      internal static extern JsErrorCode JsSetModuleHostInfo(JsModuleRecord _requestModule,
         JsModuleHostInfoKind _moduleHostInfo,
         JsNotifyModuleReadyCallback _callback);
      [DllImport(DllName, EntryPoint = "JsSetModuleHostInfo")]
      internal static extern JsErrorCode JsSetModuleHostInfo(JsModuleRecord _requestModule,
         JsModuleHostInfoKind _moduleHostInfo,
         JsFetchImportedModuleFromScriptCallback _callback);


      [DllImport(DllName)]
      internal static extern JsErrorCode JsParseModuleSource(
         JsModuleRecord _requestModule,
         JsSourceContext _sourceContext,
         byte[] _script,
         uint _scriptLength,
         JsParseModuleSourceFlags _sourceFlag,
         out JsValue _exception);
      [DllImport(DllName)]
      internal static extern JsErrorCode JsModuleEvaluation(
         JsModuleRecord _requestModule,
         out JsValue _result);
   }
}