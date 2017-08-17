using System;
using SharpChakra.Parts;

namespace SharpChakra
{
   /// <summary>
   /// Native interfaces.
   /// </summary>
   public static class Native
   {
      private static readonly bool Is32 = IntPtr.Size == 4;

      /// <summary>
      /// Throws if a native method returns an error code.
      /// </summary>
      /// <param name="_error">The error.</param>
      public static void ThrowIfError(JavaScriptErrorCode _error)
      {
         if (Is32)
            Native32.ThrowIfError(_error);
         else
            Native64.ThrowIfError(_error);
      }
      internal static JavaScriptErrorCode JsCreateRuntime(JavaScriptRuntimeAttributes _attributes,
         JavaScriptThreadServiceCallback _threadService,
         out JavaScriptRuntime _runtime)
         => Is32 ? Native32.JsCreateRuntime(_attributes, _threadService, out _runtime) : Native64.JsCreateRuntime(_attributes, _threadService, out _runtime);
      public static JavaScriptErrorCode JsCollectGarbage(JavaScriptRuntime _handle)
         => Is32? Native32.JsCollectGarbage(_handle):Native64.JsCollectGarbage(_handle);
      public static JavaScriptErrorCode JsDisposeRuntime(JavaScriptRuntime _handle)
         => Is32 ? Native32.JsDisposeRuntime(_handle) : Native64.JsDisposeRuntime(_handle);
      public static JavaScriptErrorCode JsGetRuntimeMemoryUsage(JavaScriptRuntime _runtime, out UIntPtr _memoryUsage)
         => Is32 ? Native32.JsGetRuntimeMemoryUsage(_runtime,out _memoryUsage) : Native64.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage);
      public static JavaScriptErrorCode JsGetRuntimeMemoryLimit(JavaScriptRuntime _runtime, out UIntPtr _memoryLimit)
         => Is32 ? Native32.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit) : Native64.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit);
      public static JavaScriptErrorCode JsSetRuntimeMemoryLimit(JavaScriptRuntime _runtime, UIntPtr _memoryLimit)
         => Is32 ? Native32.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit) : Native64.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit);
      public static JavaScriptErrorCode JsSetRuntimeMemoryAllocationCallback(JavaScriptRuntime _runtime,
         IntPtr _callbackState,
         JavaScriptMemoryAllocationCallback _allocationCallback)
         => Is32 ? Native32.JsSetRuntimeMemoryAllocationCallback(_runtime, _callbackState, _allocationCallback) : Native64.JsSetRuntimeMemoryAllocationCallback(_runtime, _callbackState, _allocationCallback);
      public static JavaScriptErrorCode JsSetRuntimeBeforeCollectCallback(JavaScriptRuntime _runtime,
         IntPtr _callbackState,
         JavaScriptBeforeCollectCallback _beforeCollectCallback)
         => Is32 ? Native32.JsSetRuntimeBeforeCollectCallback(_runtime, _callbackState, _beforeCollectCallback) : Native64.JsSetRuntimeBeforeCollectCallback(_runtime, _callbackState, _beforeCollectCallback);
      public static JavaScriptErrorCode JsContextAddRef(JavaScriptContext _reference, out uint _count)
         => Is32 ? Native32.JsContextAddRef(_reference, out _count) : Native64.JsContextAddRef(_reference, out _count);
      public static JavaScriptErrorCode JsAddRef(JavaScriptValue _reference, out uint _count)
         => Is32 ? Native32.JsAddRef(_reference, out _count) : Native64.JsAddRef(_reference, out _count);
      public static JavaScriptErrorCode JsContextRelease(JavaScriptContext _reference, out uint _count)
         => Is32 ? Native32.JsContextRelease(_reference, out _count) : Native64.JsContextRelease(_reference, out _count);
      public static JavaScriptErrorCode JsRelease(JavaScriptValue _reference, out uint _count)
         => Is32 ? Native32.JsRelease(_reference, out _count) : Native64.JsRelease(_reference, out _count);
      public static JavaScriptErrorCode JsCreateContext(JavaScriptRuntime _runtime, out JavaScriptContext _newContext)
         => Is32 ? Native32.JsCreateContext(_runtime, out _newContext) : Native64.JsCreateContext(_runtime, out _newContext);
      public static JavaScriptErrorCode JsGetCurrentContext(out JavaScriptContext _currentContext)
         => Is32 ? Native32.JsGetCurrentContext(out _currentContext) : Native64.JsGetCurrentContext(out _currentContext);
      public static JavaScriptErrorCode JsSetCurrentContext(JavaScriptContext _context)
         => Is32 ? Native32.JsSetCurrentContext(_context) : Native64.JsSetCurrentContext(_context);
      public static JavaScriptErrorCode JsGetRuntime(JavaScriptContext _context, out JavaScriptRuntime _runtime)
         => Is32 ? Native32.JsGetRuntime(_context,out _runtime) : Native64.JsGetRuntime(_context,out _runtime);
      public static JavaScriptErrorCode JsIdle(out uint _nextIdleTick)
         => Is32 ? Native32.JsIdle(out _nextIdleTick) : Native64.JsIdle(out _nextIdleTick);
      public static JavaScriptErrorCode JsParseScript(string _script,
         JavaScriptSourceContext _sourceContext,
         string _sourceUrl,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsParseScript(_script, _sourceContext, _sourceUrl,out _result) : Native64.JsParseScript(_script, _sourceContext, _sourceUrl, out _result);
      public static JavaScriptErrorCode JsRunScript(string _script,
         JavaScriptSourceContext _sourceContext,
         string _sourceUrl,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsRunScript(_script, _sourceContext, _sourceUrl, out _result) : Native64.JsRunScript(_script, _sourceContext, _sourceUrl, out _result);
      public static JavaScriptErrorCode JsSerializeScript(string _script, byte[] _buffer, ref ulong _bufferSize)
         => Is32 ? Native32.JsSerializeScript(_script, _buffer, ref _bufferSize) : Native64.JsSerializeScript(_script, _buffer, ref _bufferSize);
      public static JavaScriptErrorCode JsParseSerializedScript(string _script,
         byte[] _buffer,
         JavaScriptSourceContext _sourceContext,
         string _sourceUrl,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl,out _result) : Native64.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
      public static JavaScriptErrorCode JsRunSerializedScript(string _script,
         byte[] _buffer,
         JavaScriptSourceContext _sourceContext,
         string _sourceUrl,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result) : Native64.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
      public static JavaScriptErrorCode JsGetPropertyIdFromName(string _name, out JavaScriptPropertyId _propertyId)
         => Is32 ? Native32.JsGetPropertyIdFromName(_name, out _propertyId) : Native64.JsGetPropertyIdFromName(_name, out _propertyId);
      public static JavaScriptErrorCode JsGetPropertyNameFromId(JavaScriptPropertyId _propertyId, out string _name)
         => Is32 ? Native32.JsGetPropertyNameFromId(_propertyId, out _name) : Native64.JsGetPropertyNameFromId(_propertyId, out _name);
      public static JavaScriptErrorCode JsGetUndefinedValue(out JavaScriptValue _undefinedValue)
         => Is32 ? Native32.JsGetUndefinedValue(out _undefinedValue) : Native64.JsGetUndefinedValue(out _undefinedValue);
      public static JavaScriptErrorCode JsGetNullValue(out JavaScriptValue _nullValue)
         => Is32 ? Native32.JsGetNullValue(out _nullValue) : Native64.JsGetNullValue(out _nullValue);
      public static JavaScriptErrorCode JsGetTrueValue(out JavaScriptValue _trueValue)
         => Is32 ? Native32.JsGetTrueValue(out _trueValue) : Native64.JsGetTrueValue(out _trueValue);
      public static JavaScriptErrorCode JsGetFalseValue(out JavaScriptValue _falseValue)
         => Is32 ? Native32.JsGetFalseValue(out _falseValue) : Native64.JsGetFalseValue(out _falseValue);
      public static JavaScriptErrorCode JsBoolToBoolean(bool _value, out JavaScriptValue _booleanValue)
         => Is32 ? Native32.JsBoolToBoolean(_value,out _booleanValue) : Native64.JsBoolToBoolean(_value, out _booleanValue);
      public static JavaScriptErrorCode JsBooleanToBool(JavaScriptValue _booleanValue, out bool _boolValue)
         => Is32 ? Native32.JsBooleanToBool(_booleanValue, out _boolValue) : Native64.JsBooleanToBool(_booleanValue, out _boolValue);
      public static JavaScriptErrorCode JsConvertValueToBoolean(JavaScriptValue _value, out JavaScriptValue _booleanValue)
         => Is32 ? Native32.JsConvertValueToBoolean(_value, out _booleanValue) : Native64.JsConvertValueToBoolean(_value, out _booleanValue);
      public static JavaScriptErrorCode JsGetValueType(JavaScriptValue _value, out JavaScriptValueType _type)
         => Is32 ? Native32.JsGetValueType(_value, out _type) : Native64.JsGetValueType(_value, out _type);
      public static JavaScriptErrorCode JsDoubleToNumber(double _doubleValue, out JavaScriptValue _value)
         => Is32 ? Native32.JsDoubleToNumber(_doubleValue, out _value) : Native64.JsDoubleToNumber(_doubleValue, out _value);
      public static JavaScriptErrorCode JsIntToNumber(int _intValue, out JavaScriptValue _value)
         => Is32 ? Native32.JsDoubleToNumber(_intValue, out _value) : Native64.JsDoubleToNumber(_intValue, out _value);
      public static JavaScriptErrorCode JsNumberToDouble(JavaScriptValue _value, out double _doubleValue)
         => Is32 ? Native32.JsNumberToDouble(_value, out _doubleValue) : Native64.JsNumberToDouble(_value, out _doubleValue);
      public static JavaScriptErrorCode JsConvertValueToNumber(JavaScriptValue _value, out JavaScriptValue _numberValue)
         => Is32 ? Native32.JsConvertValueToNumber(_value, out _numberValue) : Native64.JsConvertValueToNumber(_value, out _numberValue);
      public static JavaScriptErrorCode JsGetStringLength(JavaScriptValue _sringValue, out int _length)
         => Is32 ? Native32.JsGetStringLength(_sringValue, out _length) : Native64.JsGetStringLength(_sringValue, out _length);
      public static JavaScriptErrorCode JsPointerToString(string _value, UIntPtr _stringLength, out JavaScriptValue _stringValue)
         => Is32 ? Native32.JsPointerToString(_value, _stringLength, out _stringValue) : Native64.JsPointerToString(_value, _stringLength, out _stringValue);
      public static JavaScriptErrorCode JsStringToPointer(JavaScriptValue _value, out IntPtr _stringValue, out UIntPtr _stringLength)
         => Is32 ? Native32.JsStringToPointer(_value, out _stringValue, out _stringLength) : Native64.JsStringToPointer(_value, out _stringValue, out _stringLength);
      public static JavaScriptErrorCode JsConvertValueToString(JavaScriptValue _value, out JavaScriptValue _stringValue)
         => Is32 ? Native32.JsConvertValueToString(_value, out _stringValue) : Native64.JsConvertValueToString(_value, out _stringValue);
      public static JavaScriptErrorCode JsGetGlobalObject(out JavaScriptValue _globalObject)
         => Is32 ? Native32.JsGetGlobalObject(out _globalObject) : Native64.JsGetGlobalObject(out _globalObject);
      public static JavaScriptErrorCode JsCreateObject(out JavaScriptValue _obj)
         => Is32 ? Native32.JsCreateObject(out _obj) : Native64.JsCreateObject(out _obj);
      public static JavaScriptErrorCode
         JsCreateExternalObject(IntPtr _data, JavaScriptObjectFinalizeCallback _finalizeCallback, out JavaScriptValue _obj)
         => Is32 ? Native32.JsCreateExternalObject(_data, _finalizeCallback,out _obj) : Native64.JsCreateExternalObject(_data, _finalizeCallback, out _obj);
      public static JavaScriptErrorCode JsConvertValueToObject(JavaScriptValue _value, out JavaScriptValue _obj)
         => Is32 ? Native32.JsConvertValueToObject(_value, out _obj) : Native64.JsConvertValueToObject(_value, out _obj);
      public static JavaScriptErrorCode JsGetPrototype(JavaScriptValue _obj, out JavaScriptValue _prototypeObject)
         => Is32 ? Native32.JsGetPrototype(_obj, out _prototypeObject) : Native64.JsGetPrototype(_obj, out _prototypeObject);
      public static JavaScriptErrorCode JsSetPrototype(JavaScriptValue _obj, JavaScriptValue _prototypeObject)
         => Is32 ? Native32.JsSetPrototype(_obj, _prototypeObject) : Native64.JsSetPrototype(_obj, _prototypeObject);
      public static JavaScriptErrorCode JsGetExtensionAllowed(JavaScriptValue _obj, out bool _value)
         => Is32 ? Native32.JsGetExtensionAllowed(_obj, out _value) : Native64.JsGetExtensionAllowed(_obj, out _value);
      public static JavaScriptErrorCode JsPreventExtension(JavaScriptValue _obj)
         => Is32 ? Native32.JsPreventExtension(_obj) : Native64.JsPreventExtension(_obj);
      public static JavaScriptErrorCode JsGetProperty(JavaScriptValue _obj, JavaScriptPropertyId _propertyId, out JavaScriptValue _value)
         => Is32 ? Native32.JsGetProperty(_obj, _propertyId,out _value) : Native64.JsGetProperty(_obj, _propertyId, out _value);
      public static JavaScriptErrorCode JsGetOwnPropertyDescriptor(JavaScriptValue _obj,
         JavaScriptPropertyId _propertyId,
         out JavaScriptValue _propertyDescriptor)
         => Is32 ? Native32.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor) : Native64.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor);
      public static JavaScriptErrorCode JsGetOwnPropertyNames(JavaScriptValue _obj, out JavaScriptValue _propertyNames)
         => Is32 ? Native32.JsGetOwnPropertyNames(_obj, out _propertyNames) : Native64.JsGetOwnPropertyNames(_obj, out _propertyNames);
      public static JavaScriptErrorCode
         JsSetProperty(JavaScriptValue _obj, JavaScriptPropertyId _propertyId, JavaScriptValue _value, bool _useStrictRules)
         => Is32 ? Native32.JsSetProperty(_obj, _propertyId, _value, _useStrictRules) : Native64.JsSetProperty(_obj, _propertyId, _value, _useStrictRules);
      public static JavaScriptErrorCode JsHasProperty(JavaScriptValue _obj, JavaScriptPropertyId _propertyId, out bool _hasProperty)
         => Is32 ? Native32.JsHasProperty(_obj, _propertyId, out _hasProperty) : Native64.JsHasProperty(_obj, _propertyId, out _hasProperty);
      public static JavaScriptErrorCode JsDeleteProperty(JavaScriptValue _obj,
         JavaScriptPropertyId _propertyId,
         bool _useStrictRules,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result) : Native64.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result);
      public static JavaScriptErrorCode JsDefineProperty(JavaScriptValue _obj,
         JavaScriptPropertyId _propertyId,
         JavaScriptValue _propertyDescriptor,
         out bool _result)
         => Is32 ? Native32.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result) : Native64.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result);
      public static JavaScriptErrorCode JsHasIndexedProperty(JavaScriptValue _obj, JavaScriptValue _index, out bool _result)
         => Is32 ? Native32.JsHasIndexedProperty(_obj, _index,out _result) : Native64.JsHasIndexedProperty(_obj, _index, out _result);
      public static JavaScriptErrorCode JsGetIndexedProperty(JavaScriptValue _obj, JavaScriptValue _index, out JavaScriptValue _result)
         => Is32 ? Native32.JsGetIndexedProperty(_obj, _index, out _result) : Native64.JsGetIndexedProperty(_obj, _index, out _result);
      public static JavaScriptErrorCode JsSetIndexedProperty(JavaScriptValue _obj, JavaScriptValue _index, JavaScriptValue _value)
         => Is32 ? Native32.JsSetIndexedProperty(_obj, _index, _value) : Native64.JsSetIndexedProperty(_obj, _index, _value);
      public static JavaScriptErrorCode JsDeleteIndexedProperty(JavaScriptValue _obj, JavaScriptValue _index)
         => Is32 ? Native32.JsDeleteIndexedProperty(_obj, _index) : Native64.JsDeleteIndexedProperty(_obj, _index);
      public static JavaScriptErrorCode JsEquals(JavaScriptValue _obj1, JavaScriptValue _obj2, out bool _result)
         => Is32 ? Native32.JsEquals(_obj1, _obj2,out _result) : Native64.JsEquals(_obj1, _obj2, out _result);
      public static JavaScriptErrorCode JsStrictEquals(JavaScriptValue _obj1, JavaScriptValue _obj2, out bool _result)
         => Is32 ? Native32.JsStrictEquals(_obj1, _obj2, out _result) : Native64.JsEquals(_obj1, _obj2, out _result);
      public static JavaScriptErrorCode JsHasExternalData(JavaScriptValue _obj, out bool _value)
         => Is32 ? Native32.JsHasExternalData(_obj, out _value) : Native64.JsHasExternalData(_obj, out _value);
      public static JavaScriptErrorCode JsGetExternalData(JavaScriptValue _obj, out IntPtr _externalData)
         => Is32 ? Native32.JsGetExternalData(_obj, out _externalData) : Native64.JsGetExternalData(_obj, out _externalData);
      public static JavaScriptErrorCode JsSetExternalData(JavaScriptValue _obj, IntPtr _externalData)
         => Is32 ? Native32.JsSetExternalData(_obj, _externalData) : Native64.JsSetExternalData(_obj, _externalData);
      public static JavaScriptErrorCode JsCreateArray(uint _length, out JavaScriptValue _result)
         => Is32 ? Native32.JsCreateArray(_length, out _result) : Native64.JsCreateArray(_length, out _result);
      public static JavaScriptErrorCode JsCallFunction(JavaScriptValue _function,
         JavaScriptValue[] _arguments,
         ushort _argumentCount,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsCallFunction(_function,_arguments, _argumentCount,out _result) : Native64.JsCallFunction(_function,_arguments, _argumentCount, out _result);
      public static JavaScriptErrorCode JsConstructObject(JavaScriptValue _function,
         JavaScriptValue[] _arguments,
         ushort _argumentCount,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsConstructObject(_function, _arguments, _argumentCount, out _result) : Native64.JsConstructObject(_function, _arguments, _argumentCount, out _result);
      public static JavaScriptErrorCode JsCreateFunction(JavaScriptNativeFunction _nativeFunction, IntPtr _externalData, out JavaScriptValue _function)
         => Is32 ? Native32.JsCreateFunction(_nativeFunction, _externalData, out _function) : Native64.JsCreateFunction(_nativeFunction, _externalData, out _function);
      public static JavaScriptErrorCode JsCreateError(JavaScriptValue _message, out JavaScriptValue _error)
         => Is32 ? Native32.JsCreateError(_message, out _error) : Native64.JsCreateError(_message, out _error);
      public static JavaScriptErrorCode JsCreateRangeError(JavaScriptValue _message, out JavaScriptValue _error)
         => Is32 ? Native32.JsCreateRangeError(_message, out _error) : Native64.JsCreateRangeError(_message, out _error);
      public static JavaScriptErrorCode JsCreateReferenceError(JavaScriptValue _message, out JavaScriptValue _error)
         => Is32 ? Native32.JsCreateReferenceError(_message, out _error) : Native64.JsCreateReferenceError(_message, out _error);
      public static JavaScriptErrorCode JsCreateSyntaxError(JavaScriptValue _message, out JavaScriptValue _error)
         => Is32 ? Native32.JsCreateSyntaxError(_message, out _error) : Native64.JsCreateSyntaxError(_message, out _error);
      public static JavaScriptErrorCode JsCreateTypeError(JavaScriptValue _message, out JavaScriptValue _error)
         => Is32 ? Native32.JsCreateTypeError(_message, out _error) : Native64.JsCreateTypeError(_message, out _error);
      public static JavaScriptErrorCode JsCreateUriError(JavaScriptValue _message, out JavaScriptValue _error)
         => Is32 ? Native32.JsCreateURIError(_message, out _error) : Native64.JsCreateURIError(_message, out _error);
      public static JavaScriptErrorCode JsHasException(out bool _hasException)
         => Is32 ? Native32.JsHasException(out _hasException) : Native64.JsHasException(out _hasException);
      public static JavaScriptErrorCode JsGetAndClearException(out JavaScriptValue _exception)
         => Is32 ? Native32.JsGetAndClearException(out _exception) : Native64.JsGetAndClearException(out _exception);
      public static JavaScriptErrorCode JsSetException(JavaScriptValue _exception)
         => Is32 ? Native32.JsSetException(_exception) : Native64.JsSetException(_exception);
      public static JavaScriptErrorCode JsDisableRuntimeExecution(JavaScriptRuntime _runtime)
         => Is32 ? Native32.JsDisableRuntimeExecution(_runtime) : Native64.JsDisableRuntimeExecution(_runtime);
      public static JavaScriptErrorCode JsEnableRuntimeExecution(JavaScriptRuntime _runtime)
         => Is32 ? Native32.JsEnableRuntimeExecution(_runtime) : Native64.JsEnableRuntimeExecution(_runtime);
      public static JavaScriptErrorCode JsIsRuntimeExecutionDisabled(JavaScriptRuntime _runtime, out bool _isDisabled)
         => Is32 ? Native32.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled) : Native64.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled);
      public static JavaScriptErrorCode JsSetObjectBeforeCollectCallback(JavaScriptValue _reference,
         IntPtr _callbackState,
         JavaScriptObjectBeforeCollectCallback _beforeCollectCallback)
         => Is32 ? Native32.JsSetObjectBeforeCollectCallback(_reference, _callbackState, _beforeCollectCallback) : Native64.JsSetObjectBeforeCollectCallback(_reference, _callbackState, _beforeCollectCallback);
      public static JavaScriptErrorCode JsCreateNamedFunction(JavaScriptValue _name,
         JavaScriptNativeFunction _nativeFunction,
         IntPtr _callbackState,
         out JavaScriptValue _function)
         => Is32 ? Native32.JsCreateNamedFunction(_name, _nativeFunction, _callbackState,out _function) : Native64.JsCreateNamedFunction(_name, _nativeFunction, _callbackState, out _function);
      public static JavaScriptErrorCode JsSetPromiseContinuationCallback(JavaScriptPromiseContinuationCallback _promiseContinuationCallback,
         IntPtr _callbackState)
         => Is32 ? Native32.JsSetPromiseContinuationCallback(_promiseContinuationCallback, _callbackState) : Native64.JsSetPromiseContinuationCallback(_promiseContinuationCallback, _callbackState);
      public static JavaScriptErrorCode JsCreateArrayBuffer(uint _byteLength, out JavaScriptValue _result)
         => Is32 ? Native32.JsCreateArrayBuffer(_byteLength, out _result) : Native64.JsCreateArrayBuffer(_byteLength, out _result);
      public static JavaScriptErrorCode JsCreateTypedArray(JavaScriptTypedArrayType _arrayType,
         JavaScriptValue _arrayBuffer,
         uint _byteOffset,
         uint _elementLength,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result) : Native64.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result);
      public static JavaScriptErrorCode JsCreateDataView(JavaScriptValue _arrayBuffer,
         uint _byteOffset,
         uint _byteOffsetLength,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength,out _result) : Native64.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result);
      public static JavaScriptErrorCode JsGetArrayBufferStorage(JavaScriptValue _arrayBuffer, out IntPtr _buffer, out uint _bufferLength)
         => Is32 ? Native32.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength) : Native64.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength);
      public static JavaScriptErrorCode JsGetTypedArrayStorage(JavaScriptValue _typedArray,
         out IntPtr _buffer,
         out uint _bufferLength,
         out JavaScriptTypedArrayType _arrayType,
         out int _elementSize)
         => Is32 ? Native32.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength,out _arrayType,out _elementSize) : Native64.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize);
      public static JavaScriptErrorCode JsGetDataViewStorage(JavaScriptValue _dataView, out IntPtr _buffer, out uint _bufferLength)
         => Is32 ? Native32.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength) : Native64.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength);
      public static JavaScriptErrorCode JsGetPropertyIdType(JavaScriptPropertyId _propertyId, out JavaScriptPropertyIdType _propertyIdType)
         => Is32 ? Native32.JsGetPropertyIdType(_propertyId, out _propertyIdType) : Native64.JsGetPropertyIdType(_propertyId, out _propertyIdType);
      public static JavaScriptErrorCode JsCreateSymbol(JavaScriptValue _description, out JavaScriptValue _symbol)
         => Is32 ? Native32.JsCreateSymbol(_description, out _symbol) : Native64.JsCreateSymbol(_description, out _symbol);
      public static JavaScriptErrorCode JsGetSymbolFromPropertyId(JavaScriptPropertyId _propertyId, out JavaScriptValue _symbol)
         => Is32 ? Native32.JsGetSymbolFromPropertyId(_propertyId, out _symbol) : Native64.JsGetSymbolFromPropertyId(_propertyId, out _symbol);
      public static JavaScriptErrorCode JsGetPropertyIdFromSymbol(JavaScriptValue _symbol, out JavaScriptPropertyId _propertyId)
         => Is32 ? Native32.JsGetPropertyIdFromSymbol(_symbol, out _propertyId) : Native64.JsGetPropertyIdFromSymbol(_symbol, out _propertyId);
      public static JavaScriptErrorCode JsGetOwnPropertySymbols(JavaScriptValue _obj, out JavaScriptValue _propertySymbols)
         => Is32 ? Native32.JsGetOwnPropertySymbols(_obj, out _propertySymbols) : Native64.JsGetOwnPropertySymbols(_obj, out _propertySymbols);
      public static JavaScriptErrorCode JsNumberToInt(JavaScriptValue _value, out int _intValue)
         => Is32 ? Native32.JsNumberToInt(_value, out _intValue) : Native64.JsNumberToInt(_value, out _intValue);
      public static JavaScriptErrorCode JsSetIndexedPropertiesToExternalData(JavaScriptValue _obj,
         IntPtr _data,
         JavaScriptTypedArrayType _arrayType,
         uint _elementLength)
         => Is32 ? Native32.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength) : Native64.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength);
      public static JavaScriptErrorCode JsGetIndexedPropertiesExternalData(JavaScriptValue _obj,
         IntPtr _data,
         out JavaScriptTypedArrayType _arrayType,
         out uint _elementLength)
         => Is32 ? Native32.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength) : Native64.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength);
      public static JavaScriptErrorCode JsHasIndexedPropertiesExternalData(JavaScriptValue _obj, out bool _value)
         => Is32 ? Native32.JsHasIndexedPropertiesExternalData(_obj, out _value) : Native64.JsHasIndexedPropertiesExternalData(_obj, out _value);
      public static JavaScriptErrorCode JsInstanceOf(JavaScriptValue _obj, JavaScriptValue _constructor, out bool _result)
         => Is32 ? Native32.JsInstanceOf(_obj, _constructor,out _result) : Native64.JsInstanceOf(_obj, _constructor, out _result);
      public static JavaScriptErrorCode JsCreateExternalArrayBuffer(IntPtr _data,
         uint _byteLength,
         JavaScriptObjectFinalizeCallback _finalizeCallback,
         IntPtr _callbackState,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeCallback, _callbackState, out _result) : Native64.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeCallback, _callbackState, out _result);
      public static JavaScriptErrorCode JsGetTypedArrayInfo(JavaScriptValue _typedArray,
         out JavaScriptTypedArrayType _arrayType,
         out JavaScriptValue _arrayBuffer,
         out uint _byteOffset,
         out uint _byteLength)
         => Is32 ? Native32.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength) : Native64.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength);
      public static JavaScriptErrorCode JsGetContextOfObject(JavaScriptValue _obj, out JavaScriptContext _context)
         => Is32 ? Native32.JsGetContextOfObject(_obj, out _context) : Native64.JsGetContextOfObject(_obj, out _context);
      public static JavaScriptErrorCode JsGetContextData(JavaScriptContext _context, out IntPtr _data)
         => Is32 ? Native32.JsGetContextData(_context, out _data) : Native64.JsGetContextData(_context, out _data);
      public static JavaScriptErrorCode JsSetContextData(JavaScriptContext _context, IntPtr _data)
         => Is32 ? Native32.JsSetContextData(_context, _data) : Native64.JsSetContextData(_context, _data);
      public static JavaScriptErrorCode JsParseSerializedScriptWithCallback(JavaScriptSerializedScriptLoadSourceCallback _scriptLoadCallback,
         JavaScriptSerializedScriptUnloadCallback _scriptUnloadCallback,
         byte[] _buffer,
         JavaScriptSourceContext _sourceContext,
         string _sourceUrl,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsParseSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl,out _result) : Native64.JsParseSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
      public static JavaScriptErrorCode JsRunSerializedScriptWithCallback(JavaScriptSerializedScriptLoadSourceCallback _scriptLoadCallback,
         JavaScriptSerializedScriptUnloadCallback _scriptUnloadCallback,
         byte[] _buffer,
         JavaScriptSourceContext _sourceContext,
         string _sourceUrl,
         out JavaScriptValue _result)
         => Is32 ? Native32.JsRunSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result) : Native64.JsRunSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
   }
}