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
      public static void ThrowIfError(JsErrorCode _error)
      {
         if (Is32)
            Native32.ThrowIfError(_error);
         else
            Native64.ThrowIfError(_error);
      }
      internal static JsErrorCode JsCreateRuntime(JsRuntimeAttributes _attributes,
         JavaScriptThreadServiceCallback _threadService,
         out JsRuntime _runtime)
         => Is32 ? Native32.JsCreateRuntime(_attributes, _threadService, out _runtime) : Native64.JsCreateRuntime(_attributes, _threadService, out _runtime);
      public static JsErrorCode JsCollectGarbage(JsRuntime _handle)
         => Is32 ? Native32.JsCollectGarbage(_handle) : Native64.JsCollectGarbage(_handle);
      public static JsErrorCode JsDisposeRuntime(JsRuntime _handle)
         => Is32 ? Native32.JsDisposeRuntime(_handle) : Native64.JsDisposeRuntime(_handle);
      public static JsErrorCode JsGetRuntimeMemoryUsage(JsRuntime _runtime, out UIntPtr _memoryUsage)
         => Is32 ? Native32.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage) : Native64.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage);
      public static JsErrorCode JsGetRuntimeMemoryLimit(JsRuntime _runtime, out UIntPtr _memoryLimit)
         => Is32 ? Native32.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit) : Native64.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit);
      public static JsErrorCode JsSetRuntimeMemoryLimit(JsRuntime _runtime, UIntPtr _memoryLimit)
         => Is32 ? Native32.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit) : Native64.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit);
      public static JsErrorCode JsSetRuntimeMemoryAllocationCallback(JsRuntime _runtime,
         IntPtr _callbackState,
         JsMemoryAllocationCallback _allocationCallback)
         => Is32
            ? Native32.JsSetRuntimeMemoryAllocationCallback(_runtime, _callbackState, _allocationCallback)
            : Native64.JsSetRuntimeMemoryAllocationCallback(_runtime, _callbackState, _allocationCallback);
      public static JsErrorCode JsSetRuntimeBeforeCollectCallback(JsRuntime _runtime,
         IntPtr _callbackState,
         JsBeforeCollectCallback _beforeCollectCallback)
         => Is32
            ? Native32.JsSetRuntimeBeforeCollectCallback(_runtime, _callbackState, _beforeCollectCallback)
            : Native64.JsSetRuntimeBeforeCollectCallback(_runtime, _callbackState, _beforeCollectCallback);
      public static JsErrorCode JsContextAddRef(JsContext _reference, out uint _count)
         => Is32 ? Native32.JsContextAddRef(_reference, out _count) : Native64.JsContextAddRef(_reference, out _count);
      public static JsErrorCode JsAddRef(JsValue _reference, out uint _count)
         => Is32 ? Native32.JsAddRef(_reference, out _count) : Native64.JsAddRef(_reference, out _count);
      public static JsErrorCode JsContextRelease(JsContext _reference, out uint _count)
         => Is32 ? Native32.JsContextRelease(_reference, out _count) : Native64.JsContextRelease(_reference, out _count);
      public static JsErrorCode JsRelease(JsValue _reference, out uint _count)
         => Is32 ? Native32.JsRelease(_reference, out _count) : Native64.JsRelease(_reference, out _count);
      public static JsErrorCode JsCreateContext(JsRuntime _runtime, out JsContext _newContext)
         => Is32 ? Native32.JsCreateContext(_runtime, out _newContext) : Native64.JsCreateContext(_runtime, out _newContext);
      public static JsErrorCode JsGetCurrentContext(out JsContext _currentContext)
         => Is32 ? Native32.JsGetCurrentContext(out _currentContext) : Native64.JsGetCurrentContext(out _currentContext);
      public static JsErrorCode JsSetCurrentContext(JsContext _context)
         => Is32 ? Native32.JsSetCurrentContext(_context) : Native64.JsSetCurrentContext(_context);
      public static JsErrorCode JsGetRuntime(JsContext _context, out JsRuntime _runtime)
         => Is32 ? Native32.JsGetRuntime(_context, out _runtime) : Native64.JsGetRuntime(_context, out _runtime);
      public static JsErrorCode JsIdle(out uint _nextIdleTick)
         => Is32 ? Native32.JsIdle(out _nextIdleTick) : Native64.JsIdle(out _nextIdleTick);
      public static JsErrorCode JsParseScript(string _script,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
         => Is32
            ? Native32.JsParseScript(_script, _sourceContext, _sourceUrl, out _result)
            : Native64.JsParseScript(_script, _sourceContext, _sourceUrl, out _result);
      public static JsErrorCode JsRunScript(string _script,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
         => Is32
            ? Native32.JsRunScript(_script, _sourceContext, _sourceUrl, out _result)
            : Native64.JsRunScript(_script, _sourceContext, _sourceUrl, out _result);
      public static JsErrorCode JsSerializeScript(string _script, byte[] _buffer, ref ulong _bufferSize)
         => Is32 ? Native32.JsSerializeScript(_script, _buffer, ref _bufferSize) : Native64.JsSerializeScript(_script, _buffer, ref _bufferSize);
      public static JsErrorCode JsParseSerializedScript(string _script,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
         => Is32
            ? Native32.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result)
            : Native64.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
      public static JsErrorCode JsRunSerializedScript(string _script,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
         => Is32
            ? Native32.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result)
            : Native64.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
      public static JsErrorCode JsGetPropertyIdFromName(string _name, out JsPropertyId _propertyId)
         => Is32 ? Native32.JsGetPropertyIdFromName(_name, out _propertyId) : Native64.JsGetPropertyIdFromName(_name, out _propertyId);
      public static JsErrorCode JsGetPropertyNameFromId(JsPropertyId _propertyId, out string _name)
         => Is32 ? Native32.JsGetPropertyNameFromId(_propertyId, out _name) : Native64.JsGetPropertyNameFromId(_propertyId, out _name);
      public static JsErrorCode JsGetUndefinedValue(out JsValue _undefinedValue)
         => Is32 ? Native32.JsGetUndefinedValue(out _undefinedValue) : Native64.JsGetUndefinedValue(out _undefinedValue);
      public static JsErrorCode JsGetNullValue(out JsValue _nullValue)
         => Is32 ? Native32.JsGetNullValue(out _nullValue) : Native64.JsGetNullValue(out _nullValue);
      public static JsErrorCode JsGetTrueValue(out JsValue _trueValue)
         => Is32 ? Native32.JsGetTrueValue(out _trueValue) : Native64.JsGetTrueValue(out _trueValue);
      public static JsErrorCode JsGetFalseValue(out JsValue _falseValue)
         => Is32 ? Native32.JsGetFalseValue(out _falseValue) : Native64.JsGetFalseValue(out _falseValue);
      public static JsErrorCode JsBoolToBoolean(bool _value, out JsValue _booleanValue)
         => Is32 ? Native32.JsBoolToBoolean(_value, out _booleanValue) : Native64.JsBoolToBoolean(_value, out _booleanValue);
      public static JsErrorCode JsBooleanToBool(JsValue _booleanValue, out bool _boolValue)
         => Is32 ? Native32.JsBooleanToBool(_booleanValue, out _boolValue) : Native64.JsBooleanToBool(_booleanValue, out _boolValue);
      public static JsErrorCode JsConvertValueToBoolean(JsValue _value, out JsValue _booleanValue)
         => Is32 ? Native32.JsConvertValueToBoolean(_value, out _booleanValue) : Native64.JsConvertValueToBoolean(_value, out _booleanValue);
      public static JsErrorCode JsGetValueType(JsValue _value, out JsValueType _type)
         => Is32 ? Native32.JsGetValueType(_value, out _type) : Native64.JsGetValueType(_value, out _type);
      public static JsErrorCode JsDoubleToNumber(double _doubleValue, out JsValue _value)
         => Is32 ? Native32.JsDoubleToNumber(_doubleValue, out _value) : Native64.JsDoubleToNumber(_doubleValue, out _value);
      public static JsErrorCode JsIntToNumber(int _intValue, out JsValue _value)
         => Is32 ? Native32.JsDoubleToNumber(_intValue, out _value) : Native64.JsDoubleToNumber(_intValue, out _value);
      public static JsErrorCode JsNumberToDouble(JsValue _value, out double _doubleValue)
         => Is32 ? Native32.JsNumberToDouble(_value, out _doubleValue) : Native64.JsNumberToDouble(_value, out _doubleValue);
      public static JsErrorCode JsConvertValueToNumber(JsValue _value, out JsValue _numberValue)
         => Is32 ? Native32.JsConvertValueToNumber(_value, out _numberValue) : Native64.JsConvertValueToNumber(_value, out _numberValue);
      public static JsErrorCode JsGetStringLength(JsValue _sringValue, out int _length)
         => Is32 ? Native32.JsGetStringLength(_sringValue, out _length) : Native64.JsGetStringLength(_sringValue, out _length);
      public static JsErrorCode JsPointerToString(string _value, UIntPtr _stringLength, out JsValue _stringValue)
         => Is32 ? Native32.JsPointerToString(_value, _stringLength, out _stringValue) : Native64.JsPointerToString(_value, _stringLength, out _stringValue);
      public static JsErrorCode JsStringToPointer(JsValue _value, out IntPtr _stringValue, out UIntPtr _stringLength)
         => Is32
            ? Native32.JsStringToPointer(_value, out _stringValue, out _stringLength)
            : Native64.JsStringToPointer(_value, out _stringValue, out _stringLength);
      public static JsErrorCode JsConvertValueToString(JsValue _value, out JsValue _stringValue)
         => Is32 ? Native32.JsConvertValueToString(_value, out _stringValue) : Native64.JsConvertValueToString(_value, out _stringValue);
      public static JsErrorCode JsGetGlobalObject(out JsValue _globalObject)
         => Is32 ? Native32.JsGetGlobalObject(out _globalObject) : Native64.JsGetGlobalObject(out _globalObject);
      public static JsErrorCode JsCreateObject(out JsValue _obj)
         => Is32 ? Native32.JsCreateObject(out _obj) : Native64.JsCreateObject(out _obj);
      public static JsErrorCode
         JsCreateExternalObject(IntPtr _data, JsObjectFinalizeCallback _finalizeCallback, out JsValue _obj)
         => Is32 ? Native32.JsCreateExternalObject(_data, _finalizeCallback, out _obj) : Native64.JsCreateExternalObject(_data, _finalizeCallback, out _obj);
      public static JsErrorCode JsConvertValueToObject(JsValue _value, out JsValue _obj)
         => Is32 ? Native32.JsConvertValueToObject(_value, out _obj) : Native64.JsConvertValueToObject(_value, out _obj);
      public static JsErrorCode JsGetPrototype(JsValue _obj, out JsValue _prototypeObject)
         => Is32 ? Native32.JsGetPrototype(_obj, out _prototypeObject) : Native64.JsGetPrototype(_obj, out _prototypeObject);
      public static JsErrorCode JsSetPrototype(JsValue _obj, JsValue _prototypeObject)
         => Is32 ? Native32.JsSetPrototype(_obj, _prototypeObject) : Native64.JsSetPrototype(_obj, _prototypeObject);
      public static JsErrorCode JsGetExtensionAllowed(JsValue _obj, out bool _value)
         => Is32 ? Native32.JsGetExtensionAllowed(_obj, out _value) : Native64.JsGetExtensionAllowed(_obj, out _value);
      public static JsErrorCode JsPreventExtension(JsValue _obj)
         => Is32 ? Native32.JsPreventExtension(_obj) : Native64.JsPreventExtension(_obj);
      public static JsErrorCode JsGetProperty(JsValue _obj, JsPropertyId _propertyId, out JsValue _value)
         => Is32 ? Native32.JsGetProperty(_obj, _propertyId, out _value) : Native64.JsGetProperty(_obj, _propertyId, out _value);
      public static JsErrorCode JsGetOwnPropertyDescriptor(JsValue _obj,
         JsPropertyId _propertyId,
         out JsValue _propertyDescriptor)
         => Is32
            ? Native32.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor)
            : Native64.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor);
      public static JsErrorCode JsGetOwnPropertyNames(JsValue _obj, out JsValue _propertyNames)
         => Is32 ? Native32.JsGetOwnPropertyNames(_obj, out _propertyNames) : Native64.JsGetOwnPropertyNames(_obj, out _propertyNames);
      public static JsErrorCode
         JsSetProperty(JsValue _obj, JsPropertyId _propertyId, JsValue _value, bool _useStrictRules)
         => Is32 ? Native32.JsSetProperty(_obj, _propertyId, _value, _useStrictRules) : Native64.JsSetProperty(_obj, _propertyId, _value, _useStrictRules);
      public static JsErrorCode JsHasProperty(JsValue _obj, JsPropertyId _propertyId, out bool _hasProperty)
         => Is32 ? Native32.JsHasProperty(_obj, _propertyId, out _hasProperty) : Native64.JsHasProperty(_obj, _propertyId, out _hasProperty);
      public static JsErrorCode JsDeleteProperty(JsValue _obj,
         JsPropertyId _propertyId,
         bool _useStrictRules,
         out JsValue _result)
         => Is32
            ? Native32.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result)
            : Native64.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result);
      public static JsErrorCode JsDefineProperty(JsValue _obj,
         JsPropertyId _propertyId,
         JsValue _propertyDescriptor,
         out bool _result)
         => Is32
            ? Native32.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result)
            : Native64.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result);
      public static JsErrorCode JsHasIndexedProperty(JsValue _obj, JsValue _index, out bool _result)
         => Is32 ? Native32.JsHasIndexedProperty(_obj, _index, out _result) : Native64.JsHasIndexedProperty(_obj, _index, out _result);
      public static JsErrorCode JsGetIndexedProperty(JsValue _obj, JsValue _index, out JsValue _result)
         => Is32 ? Native32.JsGetIndexedProperty(_obj, _index, out _result) : Native64.JsGetIndexedProperty(_obj, _index, out _result);
      public static JsErrorCode JsSetIndexedProperty(JsValue _obj, JsValue _index, JsValue _value)
         => Is32 ? Native32.JsSetIndexedProperty(_obj, _index, _value) : Native64.JsSetIndexedProperty(_obj, _index, _value);
      public static JsErrorCode JsDeleteIndexedProperty(JsValue _obj, JsValue _index)
         => Is32 ? Native32.JsDeleteIndexedProperty(_obj, _index) : Native64.JsDeleteIndexedProperty(_obj, _index);
      public static JsErrorCode JsEquals(JsValue _obj1, JsValue _obj2, out bool _result)
         => Is32 ? Native32.JsEquals(_obj1, _obj2, out _result) : Native64.JsEquals(_obj1, _obj2, out _result);
      public static JsErrorCode JsStrictEquals(JsValue _obj1, JsValue _obj2, out bool _result)
         => Is32 ? Native32.JsStrictEquals(_obj1, _obj2, out _result) : Native64.JsEquals(_obj1, _obj2, out _result);
      public static JsErrorCode JsHasExternalData(JsValue _obj, out bool _value)
         => Is32 ? Native32.JsHasExternalData(_obj, out _value) : Native64.JsHasExternalData(_obj, out _value);
      public static JsErrorCode JsGetExternalData(JsValue _obj, out IntPtr _externalData)
         => Is32 ? Native32.JsGetExternalData(_obj, out _externalData) : Native64.JsGetExternalData(_obj, out _externalData);
      public static JsErrorCode JsSetExternalData(JsValue _obj, IntPtr _externalData)
         => Is32 ? Native32.JsSetExternalData(_obj, _externalData) : Native64.JsSetExternalData(_obj, _externalData);
      public static JsErrorCode JsCreateArray(uint _length, out JsValue _result)
         => Is32 ? Native32.JsCreateArray(_length, out _result) : Native64.JsCreateArray(_length, out _result);
      public static JsErrorCode JsCallFunction(JsValue _function,
         JsValue[] _arguments,
         ushort _argumentCount,
         out JsValue _result)
         => Is32
            ? Native32.JsCallFunction(_function, _arguments, _argumentCount, out _result)
            : Native64.JsCallFunction(_function, _arguments, _argumentCount, out _result);
      public static JsErrorCode JsConstructObject(JsValue _function,
         JsValue[] _arguments,
         ushort _argumentCount,
         out JsValue _result)
         => Is32
            ? Native32.JsConstructObject(_function, _arguments, _argumentCount, out _result)
            : Native64.JsConstructObject(_function, _arguments, _argumentCount, out _result);
      public static JsErrorCode JsCreateFunction(JsNativeFunction _nativeFunction, IntPtr _externalData, out JsValue _function)
         => Is32
            ? Native32.JsCreateFunction(_nativeFunction, _externalData, out _function)
            : Native64.JsCreateFunction(_nativeFunction, _externalData, out _function);
      public static JsErrorCode JsCreateError(JsValue _message, out JsValue _error)
         => Is32 ? Native32.JsCreateError(_message, out _error) : Native64.JsCreateError(_message, out _error);
      public static JsErrorCode JsCreateRangeError(JsValue _message, out JsValue _error)
         => Is32 ? Native32.JsCreateRangeError(_message, out _error) : Native64.JsCreateRangeError(_message, out _error);
      public static JsErrorCode JsCreateReferenceError(JsValue _message, out JsValue _error)
         => Is32 ? Native32.JsCreateReferenceError(_message, out _error) : Native64.JsCreateReferenceError(_message, out _error);
      public static JsErrorCode JsCreateSyntaxError(JsValue _message, out JsValue _error)
         => Is32 ? Native32.JsCreateSyntaxError(_message, out _error) : Native64.JsCreateSyntaxError(_message, out _error);
      public static JsErrorCode JsCreateTypeError(JsValue _message, out JsValue _error)
         => Is32 ? Native32.JsCreateTypeError(_message, out _error) : Native64.JsCreateTypeError(_message, out _error);
      public static JsErrorCode JsCreateUriError(JsValue _message, out JsValue _error)
         => Is32 ? Native32.JsCreateURIError(_message, out _error) : Native64.JsCreateURIError(_message, out _error);
      public static JsErrorCode JsHasException(out bool _hasException)
         => Is32 ? Native32.JsHasException(out _hasException) : Native64.JsHasException(out _hasException);
      public static JsErrorCode JsGetAndClearException(out JsValue _exception)
         => Is32 ? Native32.JsGetAndClearException(out _exception) : Native64.JsGetAndClearException(out _exception);
      public static JsErrorCode JsSetException(JsValue _exception)
         => Is32 ? Native32.JsSetException(_exception) : Native64.JsSetException(_exception);
      public static JsErrorCode JsDisableRuntimeExecution(JsRuntime _runtime)
         => Is32 ? Native32.JsDisableRuntimeExecution(_runtime) : Native64.JsDisableRuntimeExecution(_runtime);
      public static JsErrorCode JsEnableRuntimeExecution(JsRuntime _runtime)
         => Is32 ? Native32.JsEnableRuntimeExecution(_runtime) : Native64.JsEnableRuntimeExecution(_runtime);
      public static JsErrorCode JsIsRuntimeExecutionDisabled(JsRuntime _runtime, out bool _isDisabled)
         => Is32 ? Native32.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled) : Native64.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled);
      public static JsErrorCode JsSetObjectBeforeCollectCallback(JsValue _reference,
         IntPtr _callbackState,
         JsObjectBeforeCollectCallback _beforeCollectCallback)
         => Is32
            ? Native32.JsSetObjectBeforeCollectCallback(_reference, _callbackState, _beforeCollectCallback)
            : Native64.JsSetObjectBeforeCollectCallback(_reference, _callbackState, _beforeCollectCallback);
      public static JsErrorCode JsCreateNamedFunction(JsValue _name,
         JsNativeFunction _nativeFunction,
         IntPtr _callbackState,
         out JsValue _function)
         => Is32
            ? Native32.JsCreateNamedFunction(_name, _nativeFunction, _callbackState, out _function)
            : Native64.JsCreateNamedFunction(_name, _nativeFunction, _callbackState, out _function);
      public static JsErrorCode JsSetPromiseContinuationCallback(JsPromiseContinuationCallback _promiseContinuationCallback,
         IntPtr _callbackState)
         => Is32
            ? Native32.JsSetPromiseContinuationCallback(_promiseContinuationCallback, _callbackState)
            : Native64.JsSetPromiseContinuationCallback(_promiseContinuationCallback, _callbackState);
      public static JsErrorCode JsCreateArrayBuffer(uint _byteLength, out JsValue _result)
         => Is32 ? Native32.JsCreateArrayBuffer(_byteLength, out _result) : Native64.JsCreateArrayBuffer(_byteLength, out _result);
      public static JsErrorCode JsCreateTypedArray(JavaScriptTypedArrayType _arrayType,
         JsValue _arrayBuffer,
         uint _byteOffset,
         uint _elementLength,
         out JsValue _result)
         => Is32
            ? Native32.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result)
            : Native64.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result);
      public static JsErrorCode JsCreateDataView(JsValue _arrayBuffer,
         uint _byteOffset,
         uint _byteOffsetLength,
         out JsValue _result)
         => Is32
            ? Native32.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result)
            : Native64.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result);
      public static JsErrorCode JsGetArrayBufferStorage(JsValue _arrayBuffer, out IntPtr _buffer, out uint _bufferLength)
         => Is32
            ? Native32.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength)
            : Native64.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength);
      public static JsErrorCode JsGetTypedArrayStorage(JsValue _typedArray,
         out IntPtr _buffer,
         out uint _bufferLength,
         out JavaScriptTypedArrayType _arrayType,
         out int _elementSize)
         => Is32
            ? Native32.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize)
            : Native64.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize);
      public static JsErrorCode JsGetDataViewStorage(JsValue _dataView, out IntPtr _buffer, out uint _bufferLength)
         => Is32
            ? Native32.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength)
            : Native64.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength);
      public static JsErrorCode JsGetPropertyIdType(JsPropertyId _propertyId, out JsPropertyIdType _propertyIdType)
         => Is32 ? Native32.JsGetPropertyIdType(_propertyId, out _propertyIdType) : Native64.JsGetPropertyIdType(_propertyId, out _propertyIdType);
      public static JsErrorCode JsCreateSymbol(JsValue _description, out JsValue _symbol)
         => Is32 ? Native32.JsCreateSymbol(_description, out _symbol) : Native64.JsCreateSymbol(_description, out _symbol);
      public static JsErrorCode JsGetSymbolFromPropertyId(JsPropertyId _propertyId, out JsValue _symbol)
         => Is32 ? Native32.JsGetSymbolFromPropertyId(_propertyId, out _symbol) : Native64.JsGetSymbolFromPropertyId(_propertyId, out _symbol);
      public static JsErrorCode JsGetPropertyIdFromSymbol(JsValue _symbol, out JsPropertyId _propertyId)
         => Is32 ? Native32.JsGetPropertyIdFromSymbol(_symbol, out _propertyId) : Native64.JsGetPropertyIdFromSymbol(_symbol, out _propertyId);
      public static JsErrorCode JsGetOwnPropertySymbols(JsValue _obj, out JsValue _propertySymbols)
         => Is32 ? Native32.JsGetOwnPropertySymbols(_obj, out _propertySymbols) : Native64.JsGetOwnPropertySymbols(_obj, out _propertySymbols);
      public static JsErrorCode JsNumberToInt(JsValue _value, out int _intValue)
         => Is32 ? Native32.JsNumberToInt(_value, out _intValue) : Native64.JsNumberToInt(_value, out _intValue);
      public static JsErrorCode JsSetIndexedPropertiesToExternalData(JsValue _obj,
         IntPtr _data,
         JavaScriptTypedArrayType _arrayType,
         uint _elementLength)
         => Is32
            ? Native32.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength)
            : Native64.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength);
      public static JsErrorCode JsGetIndexedPropertiesExternalData(JsValue _obj,
         IntPtr _data,
         out JavaScriptTypedArrayType _arrayType,
         out uint _elementLength)
         => Is32
            ? Native32.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength)
            : Native64.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength);
      public static JsErrorCode JsHasIndexedPropertiesExternalData(JsValue _obj, out bool _value)
         => Is32 ? Native32.JsHasIndexedPropertiesExternalData(_obj, out _value) : Native64.JsHasIndexedPropertiesExternalData(_obj, out _value);
      public static JsErrorCode JsInstanceOf(JsValue _obj, JsValue _constructor, out bool _result)
         => Is32 ? Native32.JsInstanceOf(_obj, _constructor, out _result) : Native64.JsInstanceOf(_obj, _constructor, out _result);
      public static JsErrorCode JsCreateExternalArrayBuffer(IntPtr _data,
         uint _byteLength,
         JsObjectFinalizeCallback _finalizeCallback,
         IntPtr _callbackState,
         out JsValue _result)
         => Is32
            ? Native32.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeCallback, _callbackState, out _result)
            : Native64.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeCallback, _callbackState, out _result);
      public static JsErrorCode JsGetTypedArrayInfo(JsValue _typedArray,
         out JavaScriptTypedArrayType _arrayType,
         out JsValue _arrayBuffer,
         out uint _byteOffset,
         out uint _byteLength)
         => Is32
            ? Native32.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength)
            : Native64.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength);
      public static JsErrorCode JsGetContextOfObject(JsValue _obj, out JsContext _context)
         => Is32 ? Native32.JsGetContextOfObject(_obj, out _context) : Native64.JsGetContextOfObject(_obj, out _context);
      public static JsErrorCode JsGetContextData(JsContext _context, out IntPtr _data)
         => Is32 ? Native32.JsGetContextData(_context, out _data) : Native64.JsGetContextData(_context, out _data);
      public static JsErrorCode JsSetContextData(JsContext _context, IntPtr _data)
         => Is32 ? Native32.JsSetContextData(_context, _data) : Native64.JsSetContextData(_context, _data);
      public static JsErrorCode JsParseSerializedScriptWithCallback(JavaScriptSerializedScriptLoadSourceCallback _scriptLoadCallback,
         JavaScriptSerializedScriptUnloadCallback _scriptUnloadCallback,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
         => Is32
            ? Native32.JsParseSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result)
            : Native64.JsParseSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
      public static JsErrorCode JsRunSerializedScriptWithCallback(JavaScriptSerializedScriptLoadSourceCallback _scriptLoadCallback,
         JavaScriptSerializedScriptUnloadCallback _scriptUnloadCallback,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
         => Is32
            ? Native32.JsRunSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result)
            : Native64.JsRunSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
      public static JsErrorCode JsInitializeModuleRecord(
         JsModuleRecord _referencingModule,
         JsValue _normalizedSpecifier,
         out JsModuleRecord _moduleRecord)
         => Is32
            ? Native32.JsInitializeModuleRecord(_referencingModule, _normalizedSpecifier, out _moduleRecord)
            : Native64.JsInitializeModuleRecord(_referencingModule, _normalizedSpecifier, out _moduleRecord);
      public static JsErrorCode JsSetModuleHostInfo(JsModuleRecord _module, JsFetchImportedModuleCallBack _callback)
         => Is32
            ? Native32.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleCallback, _callback)
            : Native64.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleCallback, _callback);
      public static JsErrorCode JsSetModuleHostInfo(JsModuleRecord _module, JsNotifyModuleReadyCallback _callback)
         => Is32
            ? Native32.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.NotifyModuleReadyCallback, _callback)
            : Native64.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.NotifyModuleReadyCallback, _callback);
      public static JsErrorCode JsSetModuleHostInfo(JsModuleRecord _module, JsFetchImportedModuleFromScriptCallback _callback)
         => Is32
            ? Native32.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, _callback)
            : Native64.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, _callback);
      public static JsErrorCode JsParseModuleSource(
         JsModuleRecord _requestModule,
         JsSourceContext _sourceContext,
         byte[] _script,
         uint _scriptLength,
         JsParseModuleSourceFlags _sourceFlag,
         out JsValue _exception)
         => Is32
            ? Native32.JsParseModuleSource(_requestModule, _sourceContext, _script, _scriptLength, _sourceFlag, out _exception)
            : Native64.JsParseModuleSource(_requestModule, _sourceContext, _script, _scriptLength, _sourceFlag, out _exception);
      public static JsErrorCode JsModuleEvaluation(
         JsModuleRecord _requestModule,
         out JsValue _result)
         => Is32 ? Native32.JsModuleEvaluation(_requestModule, out _result) : Native64.JsModuleEvaluation(_requestModule, out _result);

      public static JsErrorCode JsDiagStartDebugging(
         JsRuntime _runtime,
         JsDiagDebugEventCallback _debugEventCallback,
         IntPtr _callbackState)
         => Is32 ? Native32.JsDiagStartDebugging(_runtime, _debugEventCallback, _callbackState) : Native64.JsDiagStartDebugging(_runtime, _debugEventCallback, _callbackState);

      public static JsErrorCode JsDiagStopDebugging(JsRuntime _runtime, out IntPtr _callbackState)
         => Is32 ? Native32.JsDiagStopDebugging(_runtime, out _callbackState) : Native64.JsDiagStopDebugging(_runtime, out _callbackState);

      public static JsErrorCode SetBreakpoint(uint _scriptId, uint _lineNumber, uint _column, out JsValue _breakpoint)
         => Is32 ? Native32.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint) : Native64.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint);

      public static JsErrorCode JsDiagRequestAsyncBreak(JsRuntime _jsRuntime)
         => Is32 ? Native32.JsDiagRequestAsyncBreak(_jsRuntime) : Native64.JsDiagRequestAsyncBreak(_jsRuntime);

      public static JsErrorCode JsDiagGetBreakpoints(out JsValue _breakpoints)
         => Is32 ? Native32.JsDiagGetBreakpoints(out _breakpoints) : Native64.JsDiagGetBreakpoints(out _breakpoints);

      public static JsErrorCode JsDiagRemoveBreakpoint(uint _breakpointId)
         => Is32 ? Native32.JsDiagRemoveBreakpoint(_breakpointId) : Native64.JsDiagRemoveBreakpoint(_breakpointId);
   }
}