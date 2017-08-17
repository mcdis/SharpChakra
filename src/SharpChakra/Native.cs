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
      /// <param name="error">The error.</param>
      public static void ThrowIfError(JavaScriptErrorCode error)
      {
         if (Is32)
            Native32.ThrowIfError(error);
         else
            Native64.ThrowIfError(error);
      }
      internal static JavaScriptErrorCode JsCreateRuntime(JavaScriptRuntimeAttributes attributes,
         JavaScriptThreadServiceCallback threadService,
         out JavaScriptRuntime runtime)
         => Is32 ? Native32.JsCreateRuntime(attributes, threadService, out runtime) : Native64.JsCreateRuntime(attributes, threadService, out runtime);
      public static JavaScriptErrorCode JsCollectGarbage(JavaScriptRuntime handle)
         => Is32? Native32.JsCollectGarbage(handle):Native64.JsCollectGarbage(handle);
      public static JavaScriptErrorCode JsDisposeRuntime(JavaScriptRuntime handle)
         => Is32 ? Native32.JsDisposeRuntime(handle) : Native64.JsDisposeRuntime(handle);
      public static JavaScriptErrorCode JsGetRuntimeMemoryUsage(JavaScriptRuntime runtime, out UIntPtr memoryUsage)
         => Is32 ? Native32.JsGetRuntimeMemoryUsage(runtime,out memoryUsage) : Native64.JsGetRuntimeMemoryUsage(runtime, out memoryUsage);
      public static JavaScriptErrorCode JsGetRuntimeMemoryLimit(JavaScriptRuntime runtime, out UIntPtr memoryLimit)
         => Is32 ? Native32.JsGetRuntimeMemoryLimit(runtime, out memoryLimit) : Native64.JsGetRuntimeMemoryLimit(runtime, out memoryLimit);
      public static JavaScriptErrorCode JsSetRuntimeMemoryLimit(JavaScriptRuntime runtime, UIntPtr memoryLimit)
         => Is32 ? Native32.JsSetRuntimeMemoryLimit(runtime, memoryLimit) : Native64.JsSetRuntimeMemoryLimit(runtime, memoryLimit);
      public static JavaScriptErrorCode JsSetRuntimeMemoryAllocationCallback(JavaScriptRuntime runtime,
         IntPtr callbackState,
         JavaScriptMemoryAllocationCallback allocationCallback)
         => Is32 ? Native32.JsSetRuntimeMemoryAllocationCallback(runtime, callbackState, allocationCallback) : Native64.JsSetRuntimeMemoryAllocationCallback(runtime, callbackState, allocationCallback);
      public static JavaScriptErrorCode JsSetRuntimeBeforeCollectCallback(JavaScriptRuntime runtime,
         IntPtr callbackState,
         JavaScriptBeforeCollectCallback beforeCollectCallback)
         => Is32 ? Native32.JsSetRuntimeBeforeCollectCallback(runtime, callbackState, beforeCollectCallback) : Native64.JsSetRuntimeBeforeCollectCallback(runtime, callbackState, beforeCollectCallback);
      public static JavaScriptErrorCode JsContextAddRef(JavaScriptContext reference, out uint count)
         => Is32 ? Native32.JsContextAddRef(reference, out count) : Native64.JsContextAddRef(reference, out count);
      public static JavaScriptErrorCode JsAddRef(JavaScriptValue reference, out uint count)
         => Is32 ? Native32.JsAddRef(reference, out count) : Native64.JsAddRef(reference, out count);
      public static JavaScriptErrorCode JsContextRelease(JavaScriptContext reference, out uint count)
         => Is32 ? Native32.JsContextRelease(reference, out count) : Native64.JsContextRelease(reference, out count);
      public static JavaScriptErrorCode JsRelease(JavaScriptValue reference, out uint count)
         => Is32 ? Native32.JsRelease(reference, out count) : Native64.JsRelease(reference, out count);
      public static JavaScriptErrorCode JsCreateContext(JavaScriptRuntime runtime, out JavaScriptContext newContext)
         => Is32 ? Native32.JsCreateContext(runtime, out newContext) : Native64.JsCreateContext(runtime, out newContext);
      public static JavaScriptErrorCode JsGetCurrentContext(out JavaScriptContext currentContext)
         => Is32 ? Native32.JsGetCurrentContext(out currentContext) : Native64.JsGetCurrentContext(out currentContext);
      public static JavaScriptErrorCode JsSetCurrentContext(JavaScriptContext context)
         => Is32 ? Native32.JsSetCurrentContext(context) : Native64.JsSetCurrentContext(context);
      public static JavaScriptErrorCode JsGetRuntime(JavaScriptContext context, out JavaScriptRuntime runtime)
         => Is32 ? Native32.JsGetRuntime(context,out runtime) : Native64.JsGetRuntime(context,out runtime);
      public static JavaScriptErrorCode JsIdle(out uint nextIdleTick)
         => Is32 ? Native32.JsIdle(out nextIdleTick) : Native64.JsIdle(out nextIdleTick);
      public static JavaScriptErrorCode JsParseScript(string script,
         JavaScriptSourceContext sourceContext,
         string sourceUrl,
         out JavaScriptValue result)
         => Is32 ? Native32.JsParseScript(script, sourceContext, sourceUrl,out result) : Native64.JsParseScript(script, sourceContext, sourceUrl, out result);
      public static JavaScriptErrorCode JsRunScript(string script,
         JavaScriptSourceContext sourceContext,
         string sourceUrl,
         out JavaScriptValue result)
         => Is32 ? Native32.JsRunScript(script, sourceContext, sourceUrl, out result) : Native64.JsRunScript(script, sourceContext, sourceUrl, out result);
      public static JavaScriptErrorCode JsSerializeScript(string script, byte[] buffer, ref ulong bufferSize)
         => Is32 ? Native32.JsSerializeScript(script, buffer, ref bufferSize) : Native64.JsSerializeScript(script, buffer, ref bufferSize);
      public static JavaScriptErrorCode JsParseSerializedScript(string script,
         byte[] buffer,
         JavaScriptSourceContext sourceContext,
         string sourceUrl,
         out JavaScriptValue result)
         => Is32 ? Native32.JsParseSerializedScript(script, buffer, sourceContext, sourceUrl,out result) : Native64.JsParseSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
      public static JavaScriptErrorCode JsRunSerializedScript(string script,
         byte[] buffer,
         JavaScriptSourceContext sourceContext,
         string sourceUrl,
         out JavaScriptValue result)
         => Is32 ? Native32.JsRunSerializedScript(script, buffer, sourceContext, sourceUrl, out result) : Native64.JsRunSerializedScript(script, buffer, sourceContext, sourceUrl, out result);
      public static JavaScriptErrorCode JsGetPropertyIdFromName(string name, out JavaScriptPropertyId propertyId)
         => Is32 ? Native32.JsGetPropertyIdFromName(name, out propertyId) : Native64.JsGetPropertyIdFromName(name, out propertyId);
      public static JavaScriptErrorCode JsGetPropertyNameFromId(JavaScriptPropertyId propertyId, out string name)
         => Is32 ? Native32.JsGetPropertyNameFromId(propertyId, out name) : Native64.JsGetPropertyNameFromId(propertyId, out name);
      public static JavaScriptErrorCode JsGetUndefinedValue(out JavaScriptValue undefinedValue)
         => Is32 ? Native32.JsGetUndefinedValue(out undefinedValue) : Native64.JsGetUndefinedValue(out undefinedValue);
      public static JavaScriptErrorCode JsGetNullValue(out JavaScriptValue nullValue)
         => Is32 ? Native32.JsGetNullValue(out nullValue) : Native64.JsGetNullValue(out nullValue);
      public static JavaScriptErrorCode JsGetTrueValue(out JavaScriptValue trueValue)
         => Is32 ? Native32.JsGetTrueValue(out trueValue) : Native64.JsGetTrueValue(out trueValue);
      public static JavaScriptErrorCode JsGetFalseValue(out JavaScriptValue falseValue)
         => Is32 ? Native32.JsGetFalseValue(out falseValue) : Native64.JsGetFalseValue(out falseValue);
      public static JavaScriptErrorCode JsBoolToBoolean(bool value, out JavaScriptValue booleanValue)
         => Is32 ? Native32.JsBoolToBoolean(value,out booleanValue) : Native64.JsBoolToBoolean(value, out booleanValue);
      public static JavaScriptErrorCode JsBooleanToBool(JavaScriptValue booleanValue, out bool boolValue)
         => Is32 ? Native32.JsBooleanToBool(booleanValue, out boolValue) : Native64.JsBooleanToBool(booleanValue, out boolValue);
      public static JavaScriptErrorCode JsConvertValueToBoolean(JavaScriptValue value, out JavaScriptValue booleanValue)
         => Is32 ? Native32.JsConvertValueToBoolean(value, out booleanValue) : Native64.JsConvertValueToBoolean(value, out booleanValue);
      public static JavaScriptErrorCode JsGetValueType(JavaScriptValue value, out JavaScriptValueType type)
         => Is32 ? Native32.JsGetValueType(value, out type) : Native64.JsGetValueType(value, out type);
      public static JavaScriptErrorCode JsDoubleToNumber(double doubleValue, out JavaScriptValue value)
         => Is32 ? Native32.JsDoubleToNumber(doubleValue, out value) : Native64.JsDoubleToNumber(doubleValue, out value);
      public static JavaScriptErrorCode JsIntToNumber(int intValue, out JavaScriptValue value)
         => Is32 ? Native32.JsDoubleToNumber(intValue, out value) : Native64.JsDoubleToNumber(intValue, out value);
      public static JavaScriptErrorCode JsNumberToDouble(JavaScriptValue value, out double doubleValue)
         => Is32 ? Native32.JsNumberToDouble(value, out doubleValue) : Native64.JsNumberToDouble(value, out doubleValue);
      public static JavaScriptErrorCode JsConvertValueToNumber(JavaScriptValue value, out JavaScriptValue numberValue)
         => Is32 ? Native32.JsConvertValueToNumber(value, out numberValue) : Native64.JsConvertValueToNumber(value, out numberValue);
      public static JavaScriptErrorCode JsGetStringLength(JavaScriptValue sringValue, out int length)
         => Is32 ? Native32.JsGetStringLength(sringValue, out length) : Native64.JsGetStringLength(sringValue, out length);
      public static JavaScriptErrorCode JsPointerToString(string value, UIntPtr stringLength, out JavaScriptValue stringValue)
         => Is32 ? Native32.JsPointerToString(value, stringLength, out stringValue) : Native64.JsPointerToString(value, stringLength, out stringValue);
      public static JavaScriptErrorCode JsStringToPointer(JavaScriptValue value, out IntPtr stringValue, out UIntPtr stringLength)
         => Is32 ? Native32.JsStringToPointer(value, out stringValue, out stringLength) : Native64.JsStringToPointer(value, out stringValue, out stringLength);
      public static JavaScriptErrorCode JsConvertValueToString(JavaScriptValue value, out JavaScriptValue stringValue)
         => Is32 ? Native32.JsConvertValueToString(value, out stringValue) : Native64.JsConvertValueToString(value, out stringValue);
      public static JavaScriptErrorCode JsGetGlobalObject(out JavaScriptValue globalObject)
         => Is32 ? Native32.JsGetGlobalObject(out globalObject) : Native64.JsGetGlobalObject(out globalObject);
      public static JavaScriptErrorCode JsCreateObject(out JavaScriptValue obj)
         => Is32 ? Native32.JsCreateObject(out obj) : Native64.JsCreateObject(out obj);
      public static JavaScriptErrorCode
         JsCreateExternalObject(IntPtr data, JavaScriptObjectFinalizeCallback finalizeCallback, out JavaScriptValue obj)
         => Is32 ? Native32.JsCreateExternalObject(data, finalizeCallback,out obj) : Native64.JsCreateExternalObject(data, finalizeCallback, out obj);
      public static JavaScriptErrorCode JsConvertValueToObject(JavaScriptValue value, out JavaScriptValue obj)
         => Is32 ? Native32.JsConvertValueToObject(value, out obj) : Native64.JsConvertValueToObject(value, out obj);
      public static JavaScriptErrorCode JsGetPrototype(JavaScriptValue obj, out JavaScriptValue prototypeObject)
         => Is32 ? Native32.JsGetPrototype(obj, out prototypeObject) : Native64.JsGetPrototype(obj, out prototypeObject);
      public static JavaScriptErrorCode JsSetPrototype(JavaScriptValue obj, JavaScriptValue prototypeObject)
         => Is32 ? Native32.JsSetPrototype(obj, prototypeObject) : Native64.JsSetPrototype(obj, prototypeObject);
      public static JavaScriptErrorCode JsGetExtensionAllowed(JavaScriptValue obj, out bool value)
         => Is32 ? Native32.JsGetExtensionAllowed(obj, out value) : Native64.JsGetExtensionAllowed(obj, out value);
      public static JavaScriptErrorCode JsPreventExtension(JavaScriptValue obj)
         => Is32 ? Native32.JsPreventExtension(obj) : Native64.JsPreventExtension(obj);
      public static JavaScriptErrorCode JsGetProperty(JavaScriptValue obj, JavaScriptPropertyId propertyId, out JavaScriptValue value)
         => Is32 ? Native32.JsGetProperty(obj, propertyId,out value) : Native64.JsGetProperty(obj, propertyId, out value);
      public static JavaScriptErrorCode JsGetOwnPropertyDescriptor(JavaScriptValue obj,
         JavaScriptPropertyId propertyId,
         out JavaScriptValue propertyDescriptor)
         => Is32 ? Native32.JsGetOwnPropertyDescriptor(obj, propertyId, out propertyDescriptor) : Native64.JsGetOwnPropertyDescriptor(obj, propertyId, out propertyDescriptor);
      public static JavaScriptErrorCode JsGetOwnPropertyNames(JavaScriptValue obj, out JavaScriptValue propertyNames)
         => Is32 ? Native32.JsGetOwnPropertyNames(obj, out propertyNames) : Native64.JsGetOwnPropertyNames(obj, out propertyNames);
      public static JavaScriptErrorCode
         JsSetProperty(JavaScriptValue obj, JavaScriptPropertyId propertyId, JavaScriptValue value, bool useStrictRules)
         => Is32 ? Native32.JsSetProperty(obj, propertyId, value, useStrictRules) : Native64.JsSetProperty(obj, propertyId, value, useStrictRules);
      public static JavaScriptErrorCode JsHasProperty(JavaScriptValue obj, JavaScriptPropertyId propertyId, out bool hasProperty)
         => Is32 ? Native32.JsHasProperty(obj, propertyId, out hasProperty) : Native64.JsHasProperty(obj, propertyId, out hasProperty);
      public static JavaScriptErrorCode JsDeleteProperty(JavaScriptValue obj,
         JavaScriptPropertyId propertyId,
         bool useStrictRules,
         out JavaScriptValue result)
         => Is32 ? Native32.JsDeleteProperty(obj, propertyId, useStrictRules, out result) : Native64.JsDeleteProperty(obj, propertyId, useStrictRules, out result);
      public static JavaScriptErrorCode JsDefineProperty(JavaScriptValue obj,
         JavaScriptPropertyId propertyId,
         JavaScriptValue propertyDescriptor,
         out bool result)
         => Is32 ? Native32.JsDefineProperty(obj, propertyId, propertyDescriptor, out result) : Native64.JsDefineProperty(obj, propertyId, propertyDescriptor, out result);
      public static JavaScriptErrorCode JsHasIndexedProperty(JavaScriptValue obj, JavaScriptValue index, out bool result)
         => Is32 ? Native32.JsHasIndexedProperty(obj, index,out result) : Native64.JsHasIndexedProperty(obj, index, out result);
      public static JavaScriptErrorCode JsGetIndexedProperty(JavaScriptValue obj, JavaScriptValue index, out JavaScriptValue result)
         => Is32 ? Native32.JsGetIndexedProperty(obj, index, out result) : Native64.JsGetIndexedProperty(obj, index, out result);
      public static JavaScriptErrorCode JsSetIndexedProperty(JavaScriptValue obj, JavaScriptValue index, JavaScriptValue value)
         => Is32 ? Native32.JsSetIndexedProperty(obj, index, value) : Native64.JsSetIndexedProperty(obj, index, value);
      public static JavaScriptErrorCode JsDeleteIndexedProperty(JavaScriptValue obj, JavaScriptValue index)
         => Is32 ? Native32.JsDeleteIndexedProperty(obj, index) : Native64.JsDeleteIndexedProperty(obj, index);
      public static JavaScriptErrorCode JsEquals(JavaScriptValue obj1, JavaScriptValue obj2, out bool result)
         => Is32 ? Native32.JsEquals(obj1, obj2,out result) : Native64.JsEquals(obj1, obj2, out result);
      public static JavaScriptErrorCode JsStrictEquals(JavaScriptValue obj1, JavaScriptValue obj2, out bool result)
         => Is32 ? Native32.JsStrictEquals(obj1, obj2, out result) : Native64.JsEquals(obj1, obj2, out result);
      public static JavaScriptErrorCode JsHasExternalData(JavaScriptValue obj, out bool value)
         => Is32 ? Native32.JsHasExternalData(obj, out value) : Native64.JsHasExternalData(obj, out value);
      public static JavaScriptErrorCode JsGetExternalData(JavaScriptValue obj, out IntPtr externalData)
         => Is32 ? Native32.JsGetExternalData(obj, out externalData) : Native64.JsGetExternalData(obj, out externalData);
      public static JavaScriptErrorCode JsSetExternalData(JavaScriptValue obj, IntPtr externalData)
         => Is32 ? Native32.JsSetExternalData(obj, externalData) : Native64.JsSetExternalData(obj, externalData);
      public static JavaScriptErrorCode JsCreateArray(uint length, out JavaScriptValue result)
         => Is32 ? Native32.JsCreateArray(length, out result) : Native64.JsCreateArray(length, out result);
      public static JavaScriptErrorCode JsCallFunction(JavaScriptValue function,
         JavaScriptValue[] arguments,
         ushort argumentCount,
         out JavaScriptValue result)
         => Is32 ? Native32.JsCallFunction(function,arguments, argumentCount,out result) : Native64.JsCallFunction(function,arguments, argumentCount, out result);
      public static JavaScriptErrorCode JsConstructObject(JavaScriptValue function,
         JavaScriptValue[] arguments,
         ushort argumentCount,
         out JavaScriptValue result)
         => Is32 ? Native32.JsConstructObject(function, arguments, argumentCount, out result) : Native64.JsConstructObject(function, arguments, argumentCount, out result);
      public static JavaScriptErrorCode JsCreateFunction(JavaScriptNativeFunction nativeFunction, IntPtr externalData, out JavaScriptValue function)
         => Is32 ? Native32.JsCreateFunction(nativeFunction, externalData, out function) : Native64.JsCreateFunction(nativeFunction, externalData, out function);
      public static JavaScriptErrorCode JsCreateError(JavaScriptValue message, out JavaScriptValue error)
         => Is32 ? Native32.JsCreateError(message, out error) : Native64.JsCreateError(message, out error);
      public static JavaScriptErrorCode JsCreateRangeError(JavaScriptValue message, out JavaScriptValue error)
         => Is32 ? Native32.JsCreateRangeError(message, out error) : Native64.JsCreateRangeError(message, out error);
      public static JavaScriptErrorCode JsCreateReferenceError(JavaScriptValue message, out JavaScriptValue error)
         => Is32 ? Native32.JsCreateReferenceError(message, out error) : Native64.JsCreateReferenceError(message, out error);
      public static JavaScriptErrorCode JsCreateSyntaxError(JavaScriptValue message, out JavaScriptValue error)
         => Is32 ? Native32.JsCreateSyntaxError(message, out error) : Native64.JsCreateSyntaxError(message, out error);
      public static JavaScriptErrorCode JsCreateTypeError(JavaScriptValue message, out JavaScriptValue error)
         => Is32 ? Native32.JsCreateTypeError(message, out error) : Native64.JsCreateTypeError(message, out error);
      public static JavaScriptErrorCode JsCreateURIError(JavaScriptValue message, out JavaScriptValue error)
         => Is32 ? Native32.JsCreateURIError(message, out error) : Native64.JsCreateURIError(message, out error);
      public static JavaScriptErrorCode JsHasException(out bool hasException)
         => Is32 ? Native32.JsHasException(out hasException) : Native64.JsHasException(out hasException);
      public static JavaScriptErrorCode JsGetAndClearException(out JavaScriptValue exception)
         => Is32 ? Native32.JsGetAndClearException(out exception) : Native64.JsGetAndClearException(out exception);
      public static JavaScriptErrorCode JsSetException(JavaScriptValue exception)
         => Is32 ? Native32.JsSetException(exception) : Native64.JsSetException(exception);
      public static JavaScriptErrorCode JsDisableRuntimeExecution(JavaScriptRuntime runtime)
         => Is32 ? Native32.JsDisableRuntimeExecution(runtime) : Native64.JsDisableRuntimeExecution(runtime);
      public static JavaScriptErrorCode JsEnableRuntimeExecution(JavaScriptRuntime runtime)
         => Is32 ? Native32.JsEnableRuntimeExecution(runtime) : Native64.JsEnableRuntimeExecution(runtime);
      public static JavaScriptErrorCode JsIsRuntimeExecutionDisabled(JavaScriptRuntime runtime, out bool isDisabled)
         => Is32 ? Native32.JsIsRuntimeExecutionDisabled(runtime, out isDisabled) : Native64.JsIsRuntimeExecutionDisabled(runtime, out isDisabled);
      public static JavaScriptErrorCode JsSetObjectBeforeCollectCallback(JavaScriptValue reference,
         IntPtr callbackState,
         JavaScriptObjectBeforeCollectCallback beforeCollectCallback)
         => Is32 ? Native32.JsSetObjectBeforeCollectCallback(reference, callbackState, beforeCollectCallback) : Native64.JsSetObjectBeforeCollectCallback(reference, callbackState, beforeCollectCallback);
      public static JavaScriptErrorCode JsCreateNamedFunction(JavaScriptValue name,
         JavaScriptNativeFunction nativeFunction,
         IntPtr callbackState,
         out JavaScriptValue function)
         => Is32 ? Native32.JsCreateNamedFunction(name, nativeFunction, callbackState,out function) : Native64.JsCreateNamedFunction(name, nativeFunction, callbackState, out function);
      public static JavaScriptErrorCode JsSetPromiseContinuationCallback(JavaScriptPromiseContinuationCallback promiseContinuationCallback,
         IntPtr callbackState)
         => Is32 ? Native32.JsSetPromiseContinuationCallback(promiseContinuationCallback, callbackState) : Native64.JsSetPromiseContinuationCallback(promiseContinuationCallback, callbackState);
      public static JavaScriptErrorCode JsCreateArrayBuffer(uint byteLength, out JavaScriptValue result)
         => Is32 ? Native32.JsCreateArrayBuffer(byteLength, out result) : Native64.JsCreateArrayBuffer(byteLength, out result);
      public static JavaScriptErrorCode JsCreateTypedArray(JavaScriptTypedArrayType arrayType,
         JavaScriptValue arrayBuffer,
         uint byteOffset,
         uint elementLength,
         out JavaScriptValue result)
         => Is32 ? Native32.JsCreateTypedArray(arrayType, arrayBuffer, byteOffset, elementLength, out result) : Native64.JsCreateTypedArray(arrayType, arrayBuffer, byteOffset, elementLength, out result);
      public static JavaScriptErrorCode JsCreateDataView(JavaScriptValue arrayBuffer,
         uint byteOffset,
         uint byteOffsetLength,
         out JavaScriptValue result)
         => Is32 ? Native32.JsCreateDataView(arrayBuffer, byteOffset, byteOffsetLength,out result) : Native64.JsCreateDataView(arrayBuffer, byteOffset, byteOffsetLength, out result);
      public static JavaScriptErrorCode JsGetArrayBufferStorage(JavaScriptValue arrayBuffer, out IntPtr buffer, out uint bufferLength)
         => Is32 ? Native32.JsGetArrayBufferStorage(arrayBuffer, out buffer, out bufferLength) : Native64.JsGetArrayBufferStorage(arrayBuffer, out buffer, out bufferLength);
      public static JavaScriptErrorCode JsGetTypedArrayStorage(JavaScriptValue typedArray,
         out IntPtr buffer,
         out uint bufferLength,
         out JavaScriptTypedArrayType arrayType,
         out int elementSize)
         => Is32 ? Native32.JsGetTypedArrayStorage(typedArray, out buffer, out bufferLength,out arrayType,out elementSize) : Native64.JsGetTypedArrayStorage(typedArray, out buffer, out bufferLength, out arrayType, out elementSize);
      public static JavaScriptErrorCode JsGetDataViewStorage(JavaScriptValue dataView, out IntPtr buffer, out uint bufferLength)
         => Is32 ? Native32.JsGetDataViewStorage(dataView, out buffer, out bufferLength) : Native64.JsGetDataViewStorage(dataView, out buffer, out bufferLength);
      public static JavaScriptErrorCode JsGetPropertyIdType(JavaScriptPropertyId propertyId, out JavaScriptPropertyIdType propertyIdType)
         => Is32 ? Native32.JsGetPropertyIdType(propertyId, out propertyIdType) : Native64.JsGetPropertyIdType(propertyId, out propertyIdType);
      public static JavaScriptErrorCode JsCreateSymbol(JavaScriptValue description, out JavaScriptValue symbol)
         => Is32 ? Native32.JsCreateSymbol(description, out symbol) : Native64.JsCreateSymbol(description, out symbol);
      public static JavaScriptErrorCode JsGetSymbolFromPropertyId(JavaScriptPropertyId propertyId, out JavaScriptValue symbol)
         => Is32 ? Native32.JsGetSymbolFromPropertyId(propertyId, out symbol) : Native64.JsGetSymbolFromPropertyId(propertyId, out symbol);
      public static JavaScriptErrorCode JsGetPropertyIdFromSymbol(JavaScriptValue symbol, out JavaScriptPropertyId propertyId)
         => Is32 ? Native32.JsGetPropertyIdFromSymbol(symbol, out propertyId) : Native64.JsGetPropertyIdFromSymbol(symbol, out propertyId);
      public static JavaScriptErrorCode JsGetOwnPropertySymbols(JavaScriptValue obj, out JavaScriptValue propertySymbols)
         => Is32 ? Native32.JsGetOwnPropertySymbols(obj, out propertySymbols) : Native64.JsGetOwnPropertySymbols(obj, out propertySymbols);
      public static JavaScriptErrorCode JsNumberToInt(JavaScriptValue value, out int intValue)
         => Is32 ? Native32.JsNumberToInt(value, out intValue) : Native64.JsNumberToInt(value, out intValue);
      public static JavaScriptErrorCode JsSetIndexedPropertiesToExternalData(JavaScriptValue obj,
         IntPtr data,
         JavaScriptTypedArrayType arrayType,
         uint elementLength)
         => Is32 ? Native32.JsSetIndexedPropertiesToExternalData(obj, data, arrayType, elementLength) : Native64.JsSetIndexedPropertiesToExternalData(obj, data, arrayType, elementLength);
      public static JavaScriptErrorCode JsGetIndexedPropertiesExternalData(JavaScriptValue obj,
         IntPtr data,
         out JavaScriptTypedArrayType arrayType,
         out uint elementLength)
         => Is32 ? Native32.JsGetIndexedPropertiesExternalData(obj, data, out arrayType, out elementLength) : Native64.JsGetIndexedPropertiesExternalData(obj, data, out arrayType, out elementLength);
      public static JavaScriptErrorCode JsHasIndexedPropertiesExternalData(JavaScriptValue obj, out bool value)
         => Is32 ? Native32.JsHasIndexedPropertiesExternalData(obj, out value) : Native64.JsHasIndexedPropertiesExternalData(obj, out value);
      public static JavaScriptErrorCode JsInstanceOf(JavaScriptValue obj, JavaScriptValue constructor, out bool result)
         => Is32 ? Native32.JsInstanceOf(obj, constructor,out result) : Native64.JsInstanceOf(obj, constructor, out result);
      public static JavaScriptErrorCode JsCreateExternalArrayBuffer(IntPtr data,
         uint byteLength,
         JavaScriptObjectFinalizeCallback finalizeCallback,
         IntPtr callbackState,
         out JavaScriptValue result)
         => Is32 ? Native32.JsCreateExternalArrayBuffer(data, byteLength, finalizeCallback, callbackState, out result) : Native64.JsCreateExternalArrayBuffer(data, byteLength, finalizeCallback, callbackState, out result);
      public static JavaScriptErrorCode JsGetTypedArrayInfo(JavaScriptValue typedArray,
         out JavaScriptTypedArrayType arrayType,
         out JavaScriptValue arrayBuffer,
         out uint byteOffset,
         out uint byteLength)
         => Is32 ? Native32.JsGetTypedArrayInfo(typedArray, out arrayType, out arrayBuffer, out byteOffset, out byteLength) : Native64.JsGetTypedArrayInfo(typedArray, out arrayType, out arrayBuffer, out byteOffset, out byteLength);
      public static JavaScriptErrorCode JsGetContextOfObject(JavaScriptValue obj, out JavaScriptContext context)
         => Is32 ? Native32.JsGetContextOfObject(obj, out context) : Native64.JsGetContextOfObject(obj, out context);
      public static JavaScriptErrorCode JsGetContextData(JavaScriptContext context, out IntPtr data)
         => Is32 ? Native32.JsGetContextData(context, out data) : Native64.JsGetContextData(context, out data);
      public static JavaScriptErrorCode JsSetContextData(JavaScriptContext context, IntPtr data)
         => Is32 ? Native32.JsSetContextData(context, data) : Native64.JsSetContextData(context, data);
      public static JavaScriptErrorCode JsParseSerializedScriptWithCallback(JavaScriptSerializedScriptLoadSourceCallback scriptLoadCallback,
         JavaScriptSerializedScriptUnloadCallback scriptUnloadCallback,
         byte[] buffer,
         JavaScriptSourceContext sourceContext,
         string sourceUrl,
         out JavaScriptValue result)
         => Is32 ? Native32.JsParseSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl,out result) : Native64.JsParseSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
      public static JavaScriptErrorCode JsRunSerializedScriptWithCallback(JavaScriptSerializedScriptLoadSourceCallback scriptLoadCallback,
         JavaScriptSerializedScriptUnloadCallback scriptUnloadCallback,
         byte[] buffer,
         JavaScriptSourceContext sourceContext,
         string sourceUrl,
         out JavaScriptValue result)
         => Is32 ? Native32.JsRunSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result) : Native64.JsRunSerializedScriptWithCallback(scriptLoadCallback, scriptUnloadCallback, buffer, sourceContext, sourceUrl, out result);
   }
}