using System;
using System.Runtime.InteropServices;

namespace SharpChakra.Parts
{
   /// <summary>
   ///     Native interfaces.
   /// </summary>
   static class Native64
   {
      const string DllName = @"x64\ChakraCore.dll";
      /// <summary>
      /// Throws if a native method returns an error code.
      /// </summary>
      /// <param name="_error">The error.</param>
      internal static void ThrowIfError(JavaScriptErrorCode _error)
      {
         if (_error != JavaScriptErrorCode.NoError)
         {
            switch (_error)
            {
               case JavaScriptErrorCode.InvalidArgument:
                  throw new JavaScriptUsageException(_error, "Invalid argument.");

               case JavaScriptErrorCode.NullArgument:
                  throw new JavaScriptUsageException(_error, "Null argument.");

               case JavaScriptErrorCode.NoCurrentContext:
                  throw new JavaScriptUsageException(_error, "No current context.");

               case JavaScriptErrorCode.InExceptionState:
                  throw new JavaScriptUsageException(_error, "Runtime is in exception state.");

               case JavaScriptErrorCode.NotImplemented:
                  throw new JavaScriptUsageException(_error, "Method is not implemented.");

               case JavaScriptErrorCode.WrongThread:
                  throw new JavaScriptUsageException(_error, "Runtime is active on another thread.");

               case JavaScriptErrorCode.RuntimeInUse:
                  throw new JavaScriptUsageException(_error, "Runtime is in use.");

               case JavaScriptErrorCode.BadSerializedScript:
                  throw new JavaScriptUsageException(_error, "Bad serialized script.");

               case JavaScriptErrorCode.InDisabledState:
                  throw new JavaScriptUsageException(_error, "Runtime is disabled.");

               case JavaScriptErrorCode.CannotDisableExecution:
                  throw new JavaScriptUsageException(_error, "Cannot disable execution.");

               case JavaScriptErrorCode.AlreadyDebuggingContext:
                  throw new JavaScriptUsageException(_error, "Context is already in debug mode.");

               case JavaScriptErrorCode.HeapEnumInProgress:
                  throw new JavaScriptUsageException(_error, "Heap enumeration is in progress.");

               case JavaScriptErrorCode.ArgumentNotObject:
                  throw new JavaScriptUsageException(_error, "Argument is not an object.");

               case JavaScriptErrorCode.InProfileCallback:
                  throw new JavaScriptUsageException(_error, "In a profile callback.");

               case JavaScriptErrorCode.InThreadServiceCallback:
                  throw new JavaScriptUsageException(_error, "In a thread service callback.");

               case JavaScriptErrorCode.CannotSerializeDebugScript:
                  throw new JavaScriptUsageException(_error, "Cannot serialize a debug script.");

               case JavaScriptErrorCode.AlreadyProfilingContext:
                  throw new JavaScriptUsageException(_error, "Already profiling this context.");

               case JavaScriptErrorCode.IdleNotEnabled:
                  throw new JavaScriptUsageException(_error, "Idle is not enabled.");

               case JavaScriptErrorCode.OutOfMemory:
                  throw new JavaScriptEngineException(_error, "Out of memory.");

               case JavaScriptErrorCode.ScriptException:
                  {
                     JavaScriptValue errorObject;
                     var innerError = JsGetAndClearException(out errorObject);

                     if (innerError != JavaScriptErrorCode.NoError)
                     {
                        throw new JavaScriptFatalException(innerError);
                     }

                     throw new JavaScriptScriptException(_error, errorObject, "Script threw an exception.");
                  }

               case JavaScriptErrorCode.ScriptCompile:
                  {
                     JavaScriptValue errorObject;
                     var innerError = JsGetAndClearException(out errorObject);

                     if (innerError != JavaScriptErrorCode.NoError)
                     {
                        throw new JavaScriptFatalException(innerError);
                     }

                     throw new JavaScriptScriptException(_error, errorObject, "Compile error.");
                  }

               case JavaScriptErrorCode.ScriptTerminated:
                  throw new JavaScriptScriptException(_error, JavaScriptValue.Invalid, "Script was terminated.");

               case JavaScriptErrorCode.ScriptEvalDisabled:
                  throw new JavaScriptScriptException(_error, JavaScriptValue.Invalid, "Eval of strings is disabled in this runtime.");

               case JavaScriptErrorCode.Fatal:
                  throw new JavaScriptFatalException(_error);

               default:
                  throw new JavaScriptFatalException(_error);
            }
         }
      }

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateRuntime(JavaScriptRuntimeAttributes _attributes, JavaScriptThreadServiceCallback _threadService, out JavaScriptRuntime _runtime);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCollectGarbage(JavaScriptRuntime _handle);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsDisposeRuntime(JavaScriptRuntime _handle);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetRuntimeMemoryUsage(JavaScriptRuntime _runtime, out UIntPtr _memoryUsage);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetRuntimeMemoryLimit(JavaScriptRuntime _runtime, out UIntPtr _memoryLimit);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetRuntimeMemoryLimit(JavaScriptRuntime _runtime, UIntPtr _memoryLimit);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetRuntimeMemoryAllocationCallback(JavaScriptRuntime _runtime, IntPtr _callbackState, JavaScriptMemoryAllocationCallback _allocationCallback);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetRuntimeBeforeCollectCallback(JavaScriptRuntime _runtime, IntPtr _callbackState, JavaScriptBeforeCollectCallback _beforeCollectCallback);

      [DllImport(DllName, EntryPoint = "JsAddRef")]
      internal static extern JavaScriptErrorCode JsContextAddRef(JavaScriptContext _reference, out uint _count);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsAddRef(JavaScriptValue _reference, out uint _count);

      [DllImport(DllName, EntryPoint = "JsRelease")]
      internal static extern JavaScriptErrorCode JsContextRelease(JavaScriptContext _reference, out uint _count);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsRelease(JavaScriptValue _reference, out uint _count);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateContext(JavaScriptRuntime _runtime, out JavaScriptContext _newContext);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetCurrentContext(out JavaScriptContext _currentContext);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetCurrentContext(JavaScriptContext _context);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetRuntime(JavaScriptContext _context, out JavaScriptRuntime _runtime);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsIdle(out uint _nextIdleTick);

      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JavaScriptErrorCode JsParseScript(string _script, JavaScriptSourceContext _sourceContext, string _sourceUrl, out JavaScriptValue _result);

      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JavaScriptErrorCode JsRunScript(string _script, JavaScriptSourceContext _sourceContext, string _sourceUrl, out JavaScriptValue _result);

      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JavaScriptErrorCode JsSerializeScript(string _script, byte[] _buffer, ref ulong _bufferSize);

      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JavaScriptErrorCode JsParseSerializedScript(string _script, byte[] _buffer, JavaScriptSourceContext _sourceContext, string _sourceUrl, out JavaScriptValue _result);

      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JavaScriptErrorCode JsRunSerializedScript(string _script, byte[] _buffer, JavaScriptSourceContext _sourceContext, string _sourceUrl, out JavaScriptValue _result);

      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JavaScriptErrorCode JsGetPropertyIdFromName(string _name, out JavaScriptPropertyId _propertyId);

      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JavaScriptErrorCode JsGetPropertyNameFromId(JavaScriptPropertyId _propertyId, out string _name);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetUndefinedValue(out JavaScriptValue _undefinedValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetNullValue(out JavaScriptValue _nullValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetTrueValue(out JavaScriptValue _trueValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetFalseValue(out JavaScriptValue _falseValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsBoolToBoolean(bool _value, out JavaScriptValue _booleanValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsBooleanToBool(JavaScriptValue _booleanValue, out bool _boolValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsConvertValueToBoolean(JavaScriptValue _value, out JavaScriptValue _booleanValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetValueType(JavaScriptValue _value, out JavaScriptValueType _type);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsDoubleToNumber(double _doubleValue, out JavaScriptValue _value);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsIntToNumber(int _intValue, out JavaScriptValue _value);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsNumberToDouble(JavaScriptValue _value, out double _doubleValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsConvertValueToNumber(JavaScriptValue _value, out JavaScriptValue _numberValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetStringLength(JavaScriptValue _sringValue, out int _length);

      [DllImport(DllName, CharSet = CharSet.Unicode)]
      internal static extern JavaScriptErrorCode JsPointerToString(string _value, UIntPtr _stringLength, out JavaScriptValue _stringValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsStringToPointer(JavaScriptValue _value, out IntPtr _stringValue, out UIntPtr _stringLength);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsConvertValueToString(JavaScriptValue _value, out JavaScriptValue _stringValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetGlobalObject(out JavaScriptValue _globalObject);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateObject(out JavaScriptValue _obj);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateExternalObject(IntPtr _data, JavaScriptObjectFinalizeCallback _finalizeCallback, out JavaScriptValue _obj);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsConvertValueToObject(JavaScriptValue _value, out JavaScriptValue _obj);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetPrototype(JavaScriptValue _obj, out JavaScriptValue _prototypeObject);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetPrototype(JavaScriptValue _obj, JavaScriptValue _prototypeObject);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetExtensionAllowed(JavaScriptValue _obj, out bool _value);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsPreventExtension(JavaScriptValue _obj);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetProperty(JavaScriptValue _obj, JavaScriptPropertyId _propertyId, out JavaScriptValue _value);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetOwnPropertyDescriptor(JavaScriptValue _obj, JavaScriptPropertyId _propertyId, out JavaScriptValue _propertyDescriptor);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetOwnPropertyNames(JavaScriptValue _obj, out JavaScriptValue _propertyNames);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetProperty(JavaScriptValue _obj, JavaScriptPropertyId _propertyId, JavaScriptValue _value, bool _useStrictRules);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsHasProperty(JavaScriptValue _obj, JavaScriptPropertyId _propertyId, out bool _hasProperty);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsDeleteProperty(JavaScriptValue _obj, JavaScriptPropertyId _propertyId, bool _useStrictRules, out JavaScriptValue _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsDefineProperty(JavaScriptValue _obj, JavaScriptPropertyId _propertyId, JavaScriptValue _propertyDescriptor, out bool _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsHasIndexedProperty(JavaScriptValue _obj, JavaScriptValue _index, out bool _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetIndexedProperty(JavaScriptValue _obj, JavaScriptValue _index, out JavaScriptValue _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetIndexedProperty(JavaScriptValue _obj, JavaScriptValue _index, JavaScriptValue _value);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsDeleteIndexedProperty(JavaScriptValue _obj, JavaScriptValue _index);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsEquals(JavaScriptValue _obj1, JavaScriptValue _obj2, out bool _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsStrictEquals(JavaScriptValue _obj1, JavaScriptValue _obj2, out bool _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsHasExternalData(JavaScriptValue _obj, out bool _value);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetExternalData(JavaScriptValue _obj, out IntPtr _externalData);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetExternalData(JavaScriptValue _obj, IntPtr _externalData);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateArray(uint _length, out JavaScriptValue _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCallFunction(JavaScriptValue _function, JavaScriptValue[] _arguments, ushort _argumentCount, out JavaScriptValue _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsConstructObject(JavaScriptValue _function, JavaScriptValue[] _arguments, ushort _argumentCount, out JavaScriptValue _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateFunction(JavaScriptNativeFunction _nativeFunction, IntPtr _externalData, out JavaScriptValue _function);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateError(JavaScriptValue _message, out JavaScriptValue _error);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateRangeError(JavaScriptValue _message, out JavaScriptValue _error);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateReferenceError(JavaScriptValue _message, out JavaScriptValue _error);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateSyntaxError(JavaScriptValue _message, out JavaScriptValue _error);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateTypeError(JavaScriptValue _message, out JavaScriptValue _error);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateURIError(JavaScriptValue _message, out JavaScriptValue _error);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsHasException(out bool _hasException);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetAndClearException(out JavaScriptValue _exception);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetException(JavaScriptValue _exception);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsDisableRuntimeExecution(JavaScriptRuntime _runtime);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsEnableRuntimeExecution(JavaScriptRuntime _runtime);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsIsRuntimeExecutionDisabled(JavaScriptRuntime _runtime, out bool _isDisabled);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetObjectBeforeCollectCallback(JavaScriptValue _reference, IntPtr _callbackState, JavaScriptObjectBeforeCollectCallback _beforeCollectCallback);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateNamedFunction(JavaScriptValue _name, JavaScriptNativeFunction _nativeFunction, IntPtr _callbackState, out JavaScriptValue _function);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetPromiseContinuationCallback(JavaScriptPromiseContinuationCallback _promiseContinuationCallback, IntPtr _callbackState);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateArrayBuffer(uint _byteLength, out JavaScriptValue _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateTypedArray(JavaScriptTypedArrayType _arrayType, JavaScriptValue _arrayBuffer, uint _byteOffset,
          uint _elementLength, out JavaScriptValue _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateDataView(JavaScriptValue _arrayBuffer, uint _byteOffset, uint _byteOffsetLength, out JavaScriptValue _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetArrayBufferStorage(JavaScriptValue _arrayBuffer, out IntPtr _buffer, out uint _bufferLength);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetTypedArrayStorage(JavaScriptValue _typedArray, out IntPtr _buffer, out uint _bufferLength, out JavaScriptTypedArrayType _arrayType, out int _elementSize);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetDataViewStorage(JavaScriptValue _dataView, out IntPtr _buffer, out uint _bufferLength);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetPropertyIdType(JavaScriptPropertyId _propertyId, out JavaScriptPropertyIdType _propertyIdType);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateSymbol(JavaScriptValue _description, out JavaScriptValue _symbol);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetSymbolFromPropertyId(JavaScriptPropertyId _propertyId, out JavaScriptValue _symbol);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetPropertyIdFromSymbol(JavaScriptValue _symbol, out JavaScriptPropertyId _propertyId);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetOwnPropertySymbols(JavaScriptValue _obj, out JavaScriptValue _propertySymbols);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsNumberToInt(JavaScriptValue _value, out int _intValue);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetIndexedPropertiesToExternalData(JavaScriptValue _obj, IntPtr _data, JavaScriptTypedArrayType _arrayType, uint _elementLength);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetIndexedPropertiesExternalData(JavaScriptValue _obj, IntPtr _data, out JavaScriptTypedArrayType _arrayType, out uint _elementLength);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsHasIndexedPropertiesExternalData(JavaScriptValue _obj, out bool _value);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsInstanceOf(JavaScriptValue _obj, JavaScriptValue _constructor, out bool _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsCreateExternalArrayBuffer(IntPtr _data, uint _byteLength, JavaScriptObjectFinalizeCallback _finalizeCallback, IntPtr _callbackState, out JavaScriptValue _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetTypedArrayInfo(JavaScriptValue _typedArray, out JavaScriptTypedArrayType _arrayType, out JavaScriptValue _arrayBuffer, out uint _byteOffset, out uint _byteLength);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetContextOfObject(JavaScriptValue _obj, out JavaScriptContext _context);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsGetContextData(JavaScriptContext _context, out IntPtr _data);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsSetContextData(JavaScriptContext _context, IntPtr _data);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsParseSerializedScriptWithCallback(JavaScriptSerializedScriptLoadSourceCallback _scriptLoadCallback,
          JavaScriptSerializedScriptUnloadCallback _scriptUnloadCallback, byte[] _buffer, JavaScriptSourceContext _sourceContext, string _sourceUrl, out JavaScriptValue _result);

      [DllImport(DllName)]
      internal static extern JavaScriptErrorCode JsRunSerializedScriptWithCallback(JavaScriptSerializedScriptLoadSourceCallback _scriptLoadCallback,
          JavaScriptSerializedScriptUnloadCallback _scriptUnloadCallback, byte[] _buffer, JavaScriptSourceContext _sourceContext, string _sourceUrl, out JavaScriptValue _result);
   }
}
