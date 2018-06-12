using System;
using System.Runtime.InteropServices;
using SharpChakra.Parts;

namespace SharpChakra
{
   /// <summary>
   /// Native interfaces.
   /// </summary>
   public static class Native
{
#if NETSTANDARD
       private static readonly bool Is32 = RuntimeInformation.OSArchitecture == Architecture.X86;
       private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
       private static readonly bool IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
       private static readonly bool IsMac = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
#else
       private static readonly bool Is32 = IntPtr.Size == 4;
#endif

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
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateRuntime(_attributes, _threadService, out _runtime);
         if (IsWindows && !Is32) return Native64.JsCreateRuntime(_attributes, _threadService, out _runtime);
         if (IsLinux && !Is32) return Native64Linux.JsCreateRuntime(_attributes, _threadService, out _runtime);
         if (IsMac && !Is32) return Native64Mac.JsCreateRuntime(_attributes, _threadService, out _runtime);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateRuntime(_attributes, _threadService, out _runtime)
            : Native64.JsCreateRuntime(_attributes, _threadService, out _runtime);
#endif
      }

      public static JsErrorCode JsCollectGarbage(JsRuntime _handle)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCollectGarbage(_handle);
         if (IsWindows && !Is32) return Native64.JsCollectGarbage(_handle);
         if (IsLinux && !Is32) return Native64Linux.JsCollectGarbage(_handle);
         if (IsMac && !Is32) return Native64Mac.JsCollectGarbage(_handle);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCollectGarbage(_handle)
            : Native64.JsCollectGarbage(_handle);
#endif
      }

      public static JsErrorCode JsDisposeRuntime(JsRuntime _handle)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDisposeRuntime(_handle);
         if (IsWindows && !Is32) return Native64.JsDisposeRuntime(_handle);
         if (IsLinux && !Is32) return Native64Linux.JsDisposeRuntime(_handle);
         if (IsMac && !Is32) return Native64Mac.JsDisposeRuntime(_handle);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDisposeRuntime(_handle)
            : Native64.JsDisposeRuntime(_handle);
#endif
      }

      public static JsErrorCode JsGetRuntimeMemoryUsage(JsRuntime _runtime, out UIntPtr _memoryUsage)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage);
         if (IsWindows && !Is32) return Native64.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage);
         if (IsLinux && !Is32) return Native64Linux.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage);
         if (IsMac && !Is32) return Native64Mac.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage)
            : Native64.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage);
#endif
      }

      public static JsErrorCode JsGetRuntimeMemoryLimit(JsRuntime _runtime, out UIntPtr _memoryLimit)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit);
         if (IsWindows && !Is32) return Native64.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit);
         if (IsLinux && !Is32) return Native64Linux.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit);
         if (IsMac && !Is32) return Native64Mac.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit)
            : Native64.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit);
#endif
      }

      public static JsErrorCode JsSetRuntimeMemoryLimit(JsRuntime _runtime, UIntPtr _memoryLimit)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit);
         if (IsWindows && !Is32) return Native64.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit);
         if (IsLinux && !Is32) return Native64Linux.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit);
         if (IsMac && !Is32) return Native64Mac.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit)
            : Native64.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit);
#endif
      }

      public static JsErrorCode JsSetRuntimeMemoryAllocationCallback(JsRuntime _runtime,
         IntPtr _callbackState,
         JsMemoryAllocationCallback _allocationCallback)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetRuntimeMemoryAllocationCallback(_runtime, _callbackState, _allocationCallback);
         if (IsWindows && !Is32) return Native64.JsSetRuntimeMemoryAllocationCallback(_runtime, _callbackState, _allocationCallback);
         if (IsLinux && !Is32) return Native64Linux.JsSetRuntimeMemoryAllocationCallback(_runtime, _callbackState, _allocationCallback);
         if (IsMac && !Is32) return Native64Mac.JsSetRuntimeMemoryAllocationCallback(_runtime, _callbackState, _allocationCallback);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetRuntimeMemoryAllocationCallback(_runtime, _callbackState, _allocationCallback)
            : Native64.JsSetRuntimeMemoryAllocationCallback(_runtime, _callbackState, _allocationCallback);
#endif
      }

      public static JsErrorCode JsSetRuntimeBeforeCollectCallback(JsRuntime _runtime,
         IntPtr _callbackState,
         JsBeforeCollectCallback _beforeCollectCallback)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetRuntimeBeforeCollectCallback(_runtime, _callbackState, _beforeCollectCallback);
         if (IsWindows && !Is32) return Native64.JsSetRuntimeBeforeCollectCallback(_runtime, _callbackState, _beforeCollectCallback);
         if (IsLinux && !Is32) return Native64Linux.JsSetRuntimeBeforeCollectCallback(_runtime, _callbackState, _beforeCollectCallback);
         if (IsMac && !Is32) return Native64Mac.JsSetRuntimeBeforeCollectCallback(_runtime, _callbackState, _beforeCollectCallback);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetRuntimeBeforeCollectCallback(_runtime, _callbackState, _beforeCollectCallback)
            : Native64.JsSetRuntimeBeforeCollectCallback(_runtime, _callbackState, _beforeCollectCallback);
#endif
      }

      public static JsErrorCode JsContextAddRef(JsContext _reference, out uint _count)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsContextAddRef(_reference, out _count);
         if (IsWindows && !Is32) return Native64.JsContextAddRef(_reference, out _count);
         if (IsLinux && !Is32) return Native64Linux.JsContextAddRef(_reference, out _count);
         if (IsMac && !Is32) return Native64Mac.JsContextAddRef(_reference, out _count);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsContextAddRef(_reference, out _count)
            : Native64.JsContextAddRef(_reference, out _count);
#endif
      }

      public static JsErrorCode JsAddRef(JsValue _reference, out uint _count)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsAddRef(_reference, out _count);
         if (IsWindows && !Is32) return Native64.JsAddRef(_reference, out _count);
         if (IsLinux && !Is32) return Native64Linux.JsAddRef(_reference, out _count);
         if (IsMac && !Is32) return Native64Mac.JsAddRef(_reference, out _count);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsAddRef(_reference, out _count)
            : Native64.JsAddRef(_reference, out _count);
#endif
      }

      public static JsErrorCode JsContextRelease(JsContext _reference, out uint _count)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsContextRelease(_reference, out _count);
         if (IsWindows && !Is32) return Native64.JsContextRelease(_reference, out _count);
         if (IsLinux && !Is32) return Native64Linux.JsContextRelease(_reference, out _count);
         if (IsMac && !Is32) return Native64Mac.JsContextRelease(_reference, out _count);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsContextRelease(_reference, out _count)
            : Native64.JsContextRelease(_reference, out _count);
#endif
      }

      public static JsErrorCode JsRelease(JsValue _reference, out uint _count)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsRelease(_reference, out _count);
         if (IsWindows && !Is32) return Native64.JsRelease(_reference, out _count);
         if (IsLinux && !Is32) return Native64Linux.JsRelease(_reference, out _count);
         if (IsMac && !Is32) return Native64Mac.JsRelease(_reference, out _count);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsRelease(_reference, out _count)
            : Native64.JsRelease(_reference, out _count);
#endif
      }

      public static JsErrorCode JsCreateContext(JsRuntime _runtime, out JsContext _newContext)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateContext(_runtime, out _newContext);
         if (IsWindows && !Is32) return Native64.JsCreateContext(_runtime, out _newContext);
         if (IsLinux && !Is32) return Native64Linux.JsCreateContext(_runtime, out _newContext);
         if (IsMac && !Is32) return Native64Mac.JsCreateContext(_runtime, out _newContext);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateContext(_runtime, out _newContext)
            : Native64.JsCreateContext(_runtime, out _newContext);
#endif
      }

      public static JsErrorCode JsGetCurrentContext(out JsContext _currentContext)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetCurrentContext(out _currentContext);
         if (IsWindows && !Is32) return Native64.JsGetCurrentContext(out _currentContext);
         if (IsLinux && !Is32) return Native64Linux.JsGetCurrentContext(out _currentContext);
         if (IsMac && !Is32) return Native64Mac.JsGetCurrentContext(out _currentContext);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetCurrentContext(out _currentContext)
            : Native64.JsGetCurrentContext(out _currentContext);
#endif
      }

      public static JsErrorCode JsSetCurrentContext(JsContext _context)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetCurrentContext(_context);
         if (IsWindows && !Is32) return Native64.JsSetCurrentContext(_context);
         if (IsLinux && !Is32) return Native64Linux.JsSetCurrentContext(_context);
         if (IsMac && !Is32) return Native64Mac.JsSetCurrentContext(_context);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetCurrentContext(_context)
            : Native64.JsSetCurrentContext(_context);
#endif
      }

      public static JsErrorCode JsGetRuntime(JsContext _context, out JsRuntime _runtime)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetRuntime(_context, out _runtime);
         if (IsWindows && !Is32) return Native64.JsGetRuntime(_context, out _runtime);
         if (IsLinux && !Is32) return Native64Linux.JsGetRuntime(_context, out _runtime);
         if (IsMac && !Is32) return Native64Mac.JsGetRuntime(_context, out _runtime);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetRuntime(_context, out _runtime)
            : Native64.JsGetRuntime(_context, out _runtime);
#endif
      }

      public static JsErrorCode JsIdle(out uint _nextIdleTick)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsIdle(out _nextIdleTick);
         if (IsWindows && !Is32) return Native64.JsIdle(out _nextIdleTick);
         if (IsLinux && !Is32) return Native64Linux.JsIdle(out _nextIdleTick);
         if (IsMac && !Is32) return Native64Mac.JsIdle(out _nextIdleTick);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsIdle(out _nextIdleTick)
            : Native64.JsIdle(out _nextIdleTick);
#endif
      }

      public static JsErrorCode JsParseScript(string _script,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsParseScript(_script, _sourceContext, _sourceUrl, out _result);
         if (IsWindows && !Is32) return Native64.JsParseScript(_script, _sourceContext, _sourceUrl, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsParseScript(_script, _sourceContext, _sourceUrl, out _result);
         if (IsMac && !Is32) return Native64Mac.JsParseScript(_script, _sourceContext, _sourceUrl, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsParseScript(_script, _sourceContext, _sourceUrl, out _result)
            : Native64.JsParseScript(_script, _sourceContext, _sourceUrl, out _result);
#endif
      }

      public static JsErrorCode JsRunScript(string _script,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsRunScript(_script, _sourceContext, _sourceUrl, out _result);
         if (IsWindows && !Is32) return Native64.JsRunScript(_script, _sourceContext, _sourceUrl, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsRunScript(_script, _sourceContext, _sourceUrl, out _result);
         if (IsMac && !Is32) return Native64Mac.JsRunScript(_script, _sourceContext, _sourceUrl, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsRunScript(_script, _sourceContext, _sourceUrl, out _result)
            : Native64.JsRunScript(_script, _sourceContext, _sourceUrl, out _result);
#endif
      }

      public static JsErrorCode JsSerializeScript(string _script, byte[] _buffer, ref ulong _bufferSize)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSerializeScript(_script, _buffer, ref _bufferSize);
         if (IsWindows && !Is32) return Native64.JsSerializeScript(_script, _buffer, ref _bufferSize);
         if (IsLinux && !Is32) return Native64Linux.JsSerializeScript(_script, _buffer, ref _bufferSize);
         if (IsMac && !Is32) return Native64Mac.JsSerializeScript(_script, _buffer, ref _bufferSize);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSerializeScript(_script, _buffer, ref _bufferSize)
            : Native64.JsSerializeScript(_script, _buffer, ref _bufferSize);
#endif
      }

      public static JsErrorCode JsParseSerializedScript(string _script,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsWindows && !Is32) return Native64.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsMac && !Is32) return Native64Mac.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result)
            : Native64.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
#endif
      }

      public static JsErrorCode JsRunSerializedScript(string _script,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsWindows && !Is32) return Native64.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsMac && !Is32) return Native64Mac.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result)
            : Native64.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);
#endif
      }

      public static JsErrorCode JsGetPropertyIdFromName(string _name, out JsPropertyId _propertyId)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetPropertyIdFromName(_name, out _propertyId);
         if (IsWindows && !Is32) return Native64.JsGetPropertyIdFromName(_name, out _propertyId);
         if (IsLinux && !Is32) return Native64Linux.JsGetPropertyIdFromName(_name, out _propertyId);
         if (IsMac && !Is32) return Native64Mac.JsGetPropertyIdFromName(_name, out _propertyId);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetPropertyIdFromName(_name, out _propertyId)
            : Native64.JsGetPropertyIdFromName(_name, out _propertyId);
#endif
      }

      public static JsErrorCode JsGetPropertyNameFromId(JsPropertyId _propertyId, out string _name)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetPropertyNameFromId(_propertyId, out _name);
         if (IsWindows && !Is32) return Native64.JsGetPropertyNameFromId(_propertyId, out _name);
         if (IsLinux && !Is32) return Native64Linux.JsGetPropertyNameFromId(_propertyId, out _name);
         if (IsMac && !Is32) return Native64Mac.JsGetPropertyNameFromId(_propertyId, out _name);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetPropertyNameFromId(_propertyId, out _name)
            : Native64.JsGetPropertyNameFromId(_propertyId, out _name);
#endif
      }

      public static JsErrorCode JsGetUndefinedValue(out JsValue _undefinedValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetUndefinedValue(out _undefinedValue);
         if (IsWindows && !Is32) return Native64.JsGetUndefinedValue(out _undefinedValue);
         if (IsLinux && !Is32) return Native64Linux.JsGetUndefinedValue(out _undefinedValue);
         if (IsMac && !Is32) return Native64Mac.JsGetUndefinedValue(out _undefinedValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetUndefinedValue(out _undefinedValue)
            : Native64.JsGetUndefinedValue(out _undefinedValue);
#endif
      }

      public static JsErrorCode JsGetNullValue(out JsValue _nullValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetNullValue(out _nullValue);
         if (IsWindows && !Is32) return Native64.JsGetNullValue(out _nullValue);
         if (IsLinux && !Is32) return Native64Linux.JsGetNullValue(out _nullValue);
         if (IsMac && !Is32) return Native64Mac.JsGetNullValue(out _nullValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetNullValue(out _nullValue)
            : Native64.JsGetNullValue(out _nullValue);
#endif
      }

      public static JsErrorCode JsGetTrueValue(out JsValue _trueValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetTrueValue(out _trueValue);
         if (IsWindows && !Is32) return Native64.JsGetTrueValue(out _trueValue);
         if (IsLinux && !Is32) return Native64Linux.JsGetTrueValue(out _trueValue);
         if (IsMac && !Is32) return Native64Mac.JsGetTrueValue(out _trueValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetTrueValue(out _trueValue)
            : Native64.JsGetTrueValue(out _trueValue);
#endif
      }

      public static JsErrorCode JsGetFalseValue(out JsValue _falseValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetFalseValue(out _falseValue);
         if (IsWindows && !Is32) return Native64.JsGetFalseValue(out _falseValue);
         if (IsLinux && !Is32) return Native64Linux.JsGetFalseValue(out _falseValue);
         if (IsMac && !Is32) return Native64Mac.JsGetFalseValue(out _falseValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetFalseValue(out _falseValue)
            : Native64.JsGetFalseValue(out _falseValue);
#endif
      }

      public static JsErrorCode JsBoolToBoolean(bool _value, out JsValue _booleanValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsBoolToBoolean(_value, out _booleanValue);
         if (IsWindows && !Is32) return Native64.JsBoolToBoolean(_value, out _booleanValue);
         if (IsLinux && !Is32) return Native64Linux.JsBoolToBoolean(_value, out _booleanValue);
         if (IsMac && !Is32) return Native64Mac.JsBoolToBoolean(_value, out _booleanValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsBoolToBoolean(_value, out _booleanValue)
            : Native64.JsBoolToBoolean(_value, out _booleanValue);
#endif
      }

      public static JsErrorCode JsBooleanToBool(JsValue _booleanValue, out bool _boolValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsBooleanToBool(_booleanValue, out _boolValue);
         if (IsWindows && !Is32) return Native64.JsBooleanToBool(_booleanValue, out _boolValue);
         if (IsLinux && !Is32) return Native64Linux.JsBooleanToBool(_booleanValue, out _boolValue);
         if (IsMac && !Is32) return Native64Mac.JsBooleanToBool(_booleanValue, out _boolValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsBooleanToBool(_booleanValue, out _boolValue)
            : Native64.JsBooleanToBool(_booleanValue, out _boolValue);
#endif
      }

      public static JsErrorCode JsConvertValueToBoolean(JsValue _value, out JsValue _booleanValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsConvertValueToBoolean(_value, out _booleanValue);
         if (IsWindows && !Is32) return Native64.JsConvertValueToBoolean(_value, out _booleanValue);
         if (IsLinux && !Is32) return Native64Linux.JsConvertValueToBoolean(_value, out _booleanValue);
         if (IsMac && !Is32) return Native64Mac.JsConvertValueToBoolean(_value, out _booleanValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsConvertValueToBoolean(_value, out _booleanValue)
            : Native64.JsConvertValueToBoolean(_value, out _booleanValue);
#endif
      }

      public static JsErrorCode JsGetValueType(JsValue _value, out JsValueType _type)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetValueType(_value, out _type);
         if (IsWindows && !Is32) return Native64.JsGetValueType(_value, out _type);
         if (IsLinux && !Is32) return Native64Linux.JsGetValueType(_value, out _type);
         if (IsMac && !Is32) return Native64Mac.JsGetValueType(_value, out _type);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetValueType(_value, out _type)
            : Native64.JsGetValueType(_value, out _type);
#endif
      }

      public static JsErrorCode JsDoubleToNumber(double _doubleValue, out JsValue _value)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDoubleToNumber(_doubleValue, out _value);
         if (IsWindows && !Is32) return Native64.JsDoubleToNumber(_doubleValue, out _value);
         if (IsLinux && !Is32) return Native64Linux.JsDoubleToNumber(_doubleValue, out _value);
         if (IsMac && !Is32) return Native64Mac.JsDoubleToNumber(_doubleValue, out _value);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDoubleToNumber(_doubleValue, out _value)
            : Native64.JsDoubleToNumber(_doubleValue, out _value);
#endif
      }

      public static JsErrorCode JsIntToNumber(int _intValue, out JsValue _value)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDoubleToNumber(_intValue, out _value);
         if (IsWindows && !Is32) return Native64.JsDoubleToNumber(_intValue, out _value);
         if (IsLinux && !Is32) return Native64Linux.JsDoubleToNumber(_intValue, out _value);
         if (IsMac && !Is32) return Native64Mac.JsDoubleToNumber(_intValue, out _value);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDoubleToNumber(_intValue, out _value)
            : Native64.JsDoubleToNumber(_intValue, out _value);
#endif
      }

      public static JsErrorCode JsNumberToDouble(JsValue _value, out double _doubleValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsNumberToDouble(_value, out _doubleValue);
         if (IsWindows && !Is32) return Native64.JsNumberToDouble(_value, out _doubleValue);
         if (IsLinux && !Is32) return Native64Linux.JsNumberToDouble(_value, out _doubleValue);
         if (IsMac && !Is32) return Native64Mac.JsNumberToDouble(_value, out _doubleValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsNumberToDouble(_value, out _doubleValue)
            : Native64.JsNumberToDouble(_value, out _doubleValue);
#endif
      }

      public static JsErrorCode JsConvertValueToNumber(JsValue _value, out JsValue _numberValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsConvertValueToNumber(_value, out _numberValue);
         if (IsWindows && !Is32) return Native64.JsConvertValueToNumber(_value, out _numberValue);
         if (IsLinux && !Is32) return Native64Linux.JsConvertValueToNumber(_value, out _numberValue);
         if (IsMac && !Is32) return Native64Mac.JsConvertValueToNumber(_value, out _numberValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsConvertValueToNumber(_value, out _numberValue)
            : Native64.JsConvertValueToNumber(_value, out _numberValue);
#endif
      }

      public static JsErrorCode JsGetStringLength(JsValue _sringValue, out int _length)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetStringLength(_sringValue, out _length);
         if (IsWindows && !Is32) return Native64.JsGetStringLength(_sringValue, out _length);
         if (IsLinux && !Is32) return Native64Linux.JsGetStringLength(_sringValue, out _length);
         if (IsMac && !Is32) return Native64Mac.JsGetStringLength(_sringValue, out _length);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetStringLength(_sringValue, out _length)
            : Native64.JsGetStringLength(_sringValue, out _length);
#endif
      }

      public static JsErrorCode JsPointerToString(string _value, UIntPtr _stringLength, out JsValue _stringValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsPointerToString(_value, _stringLength, out _stringValue);
         if (IsWindows && !Is32) return Native64.JsPointerToString(_value, _stringLength, out _stringValue);
         if (IsLinux && !Is32) return Native64Linux.JsPointerToString(_value, _stringLength, out _stringValue);
         if (IsMac && !Is32) return Native64Mac.JsPointerToString(_value, _stringLength, out _stringValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsPointerToString(_value, _stringLength, out _stringValue)
            : Native64.JsPointerToString(_value, _stringLength, out _stringValue);
#endif
      }

      public static JsErrorCode JsStringToPointer(JsValue _value, out IntPtr _stringValue, out UIntPtr _stringLength)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsStringToPointer(_value, out _stringValue, out _stringLength);
         if (IsWindows && !Is32) return Native64.JsStringToPointer(_value, out _stringValue, out _stringLength);
         if (IsLinux && !Is32) return Native64Linux.JsStringToPointer(_value, out _stringValue, out _stringLength);
         if (IsMac && !Is32) return Native64Mac.JsStringToPointer(_value, out _stringValue, out _stringLength);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsStringToPointer(_value, out _stringValue, out _stringLength)
            : Native64.JsStringToPointer(_value, out _stringValue, out _stringLength);
#endif
      }

      public static JsErrorCode JsConvertValueToString(JsValue _value, out JsValue _stringValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsConvertValueToString(_value, out _stringValue);
         if (IsWindows && !Is32) return Native64.JsConvertValueToString(_value, out _stringValue);
         if (IsLinux && !Is32) return Native64Linux.JsConvertValueToString(_value, out _stringValue);
         if (IsMac && !Is32) return Native64Mac.JsConvertValueToString(_value, out _stringValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsConvertValueToString(_value, out _stringValue)
            : Native64.JsConvertValueToString(_value, out _stringValue);
#endif
      }

      public static JsErrorCode JsGetGlobalObject(out JsValue _globalObject)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetGlobalObject(out _globalObject);
         if (IsWindows && !Is32) return Native64.JsGetGlobalObject(out _globalObject);
         if (IsLinux && !Is32) return Native64Linux.JsGetGlobalObject(out _globalObject);
         if (IsMac && !Is32) return Native64Mac.JsGetGlobalObject(out _globalObject);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetGlobalObject(out _globalObject)
            : Native64.JsGetGlobalObject(out _globalObject);
#endif
      }

      public static JsErrorCode JsCreateObject(out JsValue _obj)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateObject(out _obj);
         if (IsWindows && !Is32) return Native64.JsCreateObject(out _obj);
         if (IsLinux && !Is32) return Native64Linux.JsCreateObject(out _obj);
         if (IsMac && !Is32) return Native64Mac.JsCreateObject(out _obj);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateObject(out _obj)
            : Native64.JsCreateObject(out _obj);
#endif
      }

      public static JsErrorCode
         JsCreateExternalObject(IntPtr _data, JsObjectFinalizeCallback _finalizeCallback, out JsValue _obj)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateExternalObject(_data, _finalizeCallback, out _obj);
         if (IsWindows && !Is32) return Native64.JsCreateExternalObject(_data, _finalizeCallback, out _obj);
         if (IsLinux && !Is32) return Native64Linux.JsCreateExternalObject(_data, _finalizeCallback, out _obj);
         if (IsMac && !Is32) return Native64Mac.JsCreateExternalObject(_data, _finalizeCallback, out _obj);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateExternalObject(_data, _finalizeCallback, out _obj)
            : Native64.JsCreateExternalObject(_data, _finalizeCallback, out _obj);
#endif
      }

      public static JsErrorCode JsConvertValueToObject(JsValue _value, out JsValue _obj)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsConvertValueToObject(_value, out _obj);
         if (IsWindows && !Is32) return Native64.JsConvertValueToObject(_value, out _obj);
         if (IsLinux && !Is32) return Native64Linux.JsConvertValueToObject(_value, out _obj);
         if (IsMac && !Is32) return Native64Mac.JsConvertValueToObject(_value, out _obj);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsConvertValueToObject(_value, out _obj)
            : Native64.JsConvertValueToObject(_value, out _obj);
#endif
      }

      public static JsErrorCode JsGetPrototype(JsValue _obj, out JsValue _prototypeObject)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetPrototype(_obj, out _prototypeObject);
         if (IsWindows && !Is32) return Native64.JsGetPrototype(_obj, out _prototypeObject);
         if (IsLinux && !Is32) return Native64Linux.JsGetPrototype(_obj, out _prototypeObject);
         if (IsMac && !Is32) return Native64Mac.JsGetPrototype(_obj, out _prototypeObject);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetPrototype(_obj, out _prototypeObject)
            : Native64.JsGetPrototype(_obj, out _prototypeObject);
#endif
      }

      public static JsErrorCode JsSetPrototype(JsValue _obj, JsValue _prototypeObject)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetPrototype(_obj, _prototypeObject);
         if (IsWindows && !Is32) return Native64.JsSetPrototype(_obj, _prototypeObject);
         if (IsLinux && !Is32) return Native64Linux.JsSetPrototype(_obj, _prototypeObject);
         if (IsMac && !Is32) return Native64Mac.JsSetPrototype(_obj, _prototypeObject);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetPrototype(_obj, _prototypeObject)
            : Native64.JsSetPrototype(_obj, _prototypeObject);
#endif
      }

      public static JsErrorCode JsGetExtensionAllowed(JsValue _obj, out bool _value)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetExtensionAllowed(_obj, out _value);
         if (IsWindows && !Is32) return Native64.JsGetExtensionAllowed(_obj, out _value);
         if (IsLinux && !Is32) return Native64Linux.JsGetExtensionAllowed(_obj, out _value);
         if (IsMac && !Is32) return Native64Mac.JsGetExtensionAllowed(_obj, out _value);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetExtensionAllowed(_obj, out _value)
            : Native64.JsGetExtensionAllowed(_obj, out _value);
#endif
      }

      public static JsErrorCode JsPreventExtension(JsValue _obj)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsPreventExtension(_obj);
         if (IsWindows && !Is32) return Native64.JsPreventExtension(_obj);
         if (IsLinux && !Is32) return Native64Linux.JsPreventExtension(_obj);
         if (IsMac && !Is32) return Native64Mac.JsPreventExtension(_obj);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsPreventExtension(_obj)
            : Native64.JsPreventExtension(_obj);
#endif
      }

      public static JsErrorCode JsGetProperty(JsValue _obj, JsPropertyId _propertyId, out JsValue _value)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetProperty(_obj, _propertyId, out _value);
         if (IsWindows && !Is32) return Native64.JsGetProperty(_obj, _propertyId, out _value);
         if (IsLinux && !Is32) return Native64Linux.JsGetProperty(_obj, _propertyId, out _value);
         if (IsMac && !Is32) return Native64Mac.JsGetProperty(_obj, _propertyId, out _value);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetProperty(_obj, _propertyId, out _value)
            : Native64.JsGetProperty(_obj, _propertyId, out _value);
#endif
      }

      public static JsErrorCode JsGetOwnPropertyDescriptor(JsValue _obj,
         JsPropertyId _propertyId,
         out JsValue _propertyDescriptor)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor);
         if (IsWindows && !Is32) return Native64.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor);
         if (IsLinux && !Is32) return Native64Linux.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor);
         if (IsMac && !Is32) return Native64Mac.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor)
            : Native64.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor);
#endif
      }

      public static JsErrorCode JsGetOwnPropertyNames(JsValue _obj, out JsValue _propertyNames)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetOwnPropertyNames(_obj, out _propertyNames);
         if (IsWindows && !Is32) return Native64.JsGetOwnPropertyNames(_obj, out _propertyNames);
         if (IsLinux && !Is32) return Native64Linux.JsGetOwnPropertyNames(_obj, out _propertyNames);
         if (IsMac && !Is32) return Native64Mac.JsGetOwnPropertyNames(_obj, out _propertyNames);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetOwnPropertyNames(_obj, out _propertyNames)
            : Native64.JsGetOwnPropertyNames(_obj, out _propertyNames);
#endif
      }

      public static JsErrorCode
         JsSetProperty(JsValue _obj, JsPropertyId _propertyId, JsValue _value, bool _useStrictRules)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetProperty(_obj, _propertyId, _value, _useStrictRules);
         if (IsWindows && !Is32) return Native64.JsSetProperty(_obj, _propertyId, _value, _useStrictRules);
         if (IsLinux && !Is32) return Native64Linux.JsSetProperty(_obj, _propertyId, _value, _useStrictRules);
         if (IsMac && !Is32) return Native64Mac.JsSetProperty(_obj, _propertyId, _value, _useStrictRules);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetProperty(_obj, _propertyId, _value, _useStrictRules)
            : Native64.JsSetProperty(_obj, _propertyId, _value, _useStrictRules);
#endif
      }

      public static JsErrorCode JsHasProperty(JsValue _obj, JsPropertyId _propertyId, out bool _hasProperty)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsHasProperty(_obj, _propertyId, out _hasProperty);
         if (IsWindows && !Is32) return Native64.JsHasProperty(_obj, _propertyId, out _hasProperty);
         if (IsLinux && !Is32) return Native64Linux.JsHasProperty(_obj, _propertyId, out _hasProperty);
         if (IsMac && !Is32) return Native64Mac.JsHasProperty(_obj, _propertyId, out _hasProperty);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsHasProperty(_obj, _propertyId, out _hasProperty)
            : Native64.JsHasProperty(_obj, _propertyId, out _hasProperty);
#endif
      }

      public static JsErrorCode JsDeleteProperty(JsValue _obj,
         JsPropertyId _propertyId,
         bool _useStrictRules,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result);
         if (IsWindows && !Is32) return Native64.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result);
         if (IsMac && !Is32) return Native64Mac.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result)
            : Native64.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result);
#endif
      }

      public static JsErrorCode JsDefineProperty(JsValue _obj,
         JsPropertyId _propertyId,
         JsValue _propertyDescriptor,
         out bool _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result);
         if (IsWindows && !Is32) return Native64.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result);
         if (IsMac && !Is32) return Native64Mac.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result)
            : Native64.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result);
#endif
      }

      public static JsErrorCode JsHasIndexedProperty(JsValue _obj, JsValue _index, out bool _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsHasIndexedProperty(_obj, _index, out _result);
         if (IsWindows && !Is32) return Native64.JsHasIndexedProperty(_obj, _index, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsHasIndexedProperty(_obj, _index, out _result);
         if (IsMac && !Is32) return Native64Mac.JsHasIndexedProperty(_obj, _index, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsHasIndexedProperty(_obj, _index, out _result)
            : Native64.JsHasIndexedProperty(_obj, _index, out _result);
#endif
      }

      public static JsErrorCode JsGetIndexedProperty(JsValue _obj, JsValue _index, out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetIndexedProperty(_obj, _index, out _result);
         if (IsWindows && !Is32) return Native64.JsGetIndexedProperty(_obj, _index, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsGetIndexedProperty(_obj, _index, out _result);
         if (IsMac && !Is32) return Native64Mac.JsGetIndexedProperty(_obj, _index, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetIndexedProperty(_obj, _index, out _result)
            : Native64.JsGetIndexedProperty(_obj, _index, out _result);
#endif
      }

      public static JsErrorCode JsSetIndexedProperty(JsValue _obj, JsValue _index, JsValue _value)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetIndexedProperty(_obj, _index, _value);
         if (IsWindows && !Is32) return Native64.JsSetIndexedProperty(_obj, _index, _value);
         if (IsLinux && !Is32) return Native64Linux.JsSetIndexedProperty(_obj, _index, _value);
         if (IsMac && !Is32) return Native64Mac.JsSetIndexedProperty(_obj, _index, _value);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetIndexedProperty(_obj, _index, _value)
            : Native64.JsSetIndexedProperty(_obj, _index, _value);
#endif
      }

      public static JsErrorCode JsDeleteIndexedProperty(JsValue _obj, JsValue _index)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDeleteIndexedProperty(_obj, _index);
         if (IsWindows && !Is32) return Native64.JsDeleteIndexedProperty(_obj, _index);
         if (IsLinux && !Is32) return Native64Linux.JsDeleteIndexedProperty(_obj, _index);
         if (IsMac && !Is32) return Native64Mac.JsDeleteIndexedProperty(_obj, _index);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDeleteIndexedProperty(_obj, _index)
            : Native64.JsDeleteIndexedProperty(_obj, _index);
#endif
      }

      public static JsErrorCode JsEquals(JsValue _obj1, JsValue _obj2, out bool _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsEquals(_obj1, _obj2, out _result);
         if (IsWindows && !Is32) return Native64.JsEquals(_obj1, _obj2, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsEquals(_obj1, _obj2, out _result);
         if (IsMac && !Is32) return Native64Mac.JsEquals(_obj1, _obj2, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsEquals(_obj1, _obj2, out _result)
            : Native64.JsEquals(_obj1, _obj2, out _result);
#endif
      }

      public static JsErrorCode JsStrictEquals(JsValue _obj1, JsValue _obj2, out bool _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsStrictEquals(_obj1, _obj2, out _result);
         if (IsWindows && !Is32) return Native64.JsStrictEquals(_obj1, _obj2, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsStrictEquals(_obj1, _obj2, out _result);
         if (IsMac && !Is32) return Native64Mac.JsStrictEquals(_obj1, _obj2, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsStrictEquals(_obj1, _obj2, out _result)
            : Native64.JsStrictEquals(_obj1, _obj2, out _result);
#endif
      }

      public static JsErrorCode JsHasExternalData(JsValue _obj, out bool _value)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsHasExternalData(_obj, out _value);
         if (IsWindows && !Is32) return Native64.JsHasExternalData(_obj, out _value);
         if (IsLinux && !Is32) return Native64Linux.JsHasExternalData(_obj, out _value);
         if (IsMac && !Is32) return Native64Mac.JsHasExternalData(_obj, out _value);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsHasExternalData(_obj, out _value)
            : Native64.JsHasExternalData(_obj, out _value);
#endif
      }

      public static JsErrorCode JsGetExternalData(JsValue _obj, out IntPtr _externalData)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetExternalData(_obj, out _externalData);
         if (IsWindows && !Is32) return Native64.JsGetExternalData(_obj, out _externalData);
         if (IsLinux && !Is32) return Native64Linux.JsGetExternalData(_obj, out _externalData);
         if (IsMac && !Is32) return Native64Mac.JsGetExternalData(_obj, out _externalData);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetExternalData(_obj, out _externalData)
            : Native64.JsGetExternalData(_obj, out _externalData);
#endif
      }

      public static JsErrorCode JsSetExternalData(JsValue _obj, IntPtr _externalData)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetExternalData(_obj, _externalData);
         if (IsWindows && !Is32) return Native64.JsSetExternalData(_obj, _externalData);
         if (IsLinux && !Is32) return Native64Linux.JsSetExternalData(_obj, _externalData);
         if (IsMac && !Is32) return Native64Mac.JsSetExternalData(_obj, _externalData);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetExternalData(_obj, _externalData)
            : Native64.JsSetExternalData(_obj, _externalData);
#endif
      }

      public static JsErrorCode JsCreateArray(uint _length, out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateArray(_length, out _result);
         if (IsWindows && !Is32) return Native64.JsCreateArray(_length, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsCreateArray(_length, out _result);
         if (IsMac && !Is32) return Native64Mac.JsCreateArray(_length, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateArray(_length, out _result)
            : Native64.JsCreateArray(_length, out _result);
#endif
      }

      public static JsErrorCode JsCallFunction(JsValue _function,
         JsValue[] _arguments,
         ushort _argumentCount,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCallFunction(_function, _arguments, _argumentCount, out _result);
         if (IsWindows && !Is32) return Native64.JsCallFunction(_function, _arguments, _argumentCount, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsCallFunction(_function, _arguments, _argumentCount, out _result);
         if (IsMac && !Is32) return Native64Mac.JsCallFunction(_function, _arguments, _argumentCount, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCallFunction(_function, _arguments, _argumentCount, out _result)
            : Native64.JsCallFunction(_function, _arguments, _argumentCount, out _result);
#endif
      }

      public static JsErrorCode JsConstructObject(JsValue _function,
         JsValue[] _arguments,
         ushort _argumentCount,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsConstructObject(_function, _arguments, _argumentCount, out _result);
         if (IsWindows && !Is32) return Native64.JsConstructObject(_function, _arguments, _argumentCount, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsConstructObject(_function, _arguments, _argumentCount, out _result);
         if (IsMac && !Is32) return Native64Mac.JsConstructObject(_function, _arguments, _argumentCount, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsConstructObject(_function, _arguments, _argumentCount, out _result)
            : Native64.JsConstructObject(_function, _arguments, _argumentCount, out _result);
#endif
      }

      public static JsErrorCode JsCreateFunction(JsNativeFunction _nativeFunction, IntPtr _externalData,
         out JsValue _function)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateFunction(_nativeFunction, _externalData, out _function);
         if (IsWindows && !Is32) return Native64.JsCreateFunction(_nativeFunction, _externalData, out _function);
         if (IsLinux && !Is32) return Native64Linux.JsCreateFunction(_nativeFunction, _externalData, out _function);
         if (IsMac && !Is32) return Native64Mac.JsCreateFunction(_nativeFunction, _externalData, out _function);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateFunction(_nativeFunction, _externalData, out _function)
            : Native64.JsCreateFunction(_nativeFunction, _externalData, out _function);
#endif
      }

      public static JsErrorCode JsCreateError(JsValue _message, out JsValue _error)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateError(_message, out _error);
         if (IsWindows && !Is32) return Native64.JsCreateError(_message, out _error);
         if (IsLinux && !Is32) return Native64Linux.JsCreateError(_message, out _error);
         if (IsMac && !Is32) return Native64Mac.JsCreateError(_message, out _error);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateError(_message, out _error)
            : Native64.JsCreateError(_message, out _error);
#endif
      }

      public static JsErrorCode JsCreateRangeError(JsValue _message, out JsValue _error)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateRangeError(_message, out _error);
         if (IsWindows && !Is32) return Native64.JsCreateRangeError(_message, out _error);
         if (IsLinux && !Is32) return Native64Linux.JsCreateRangeError(_message, out _error);
         if (IsMac && !Is32) return Native64Mac.JsCreateRangeError(_message, out _error);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateRangeError(_message, out _error)
            : Native64.JsCreateRangeError(_message, out _error);
#endif
      }

      public static JsErrorCode JsCreateReferenceError(JsValue _message, out JsValue _error)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateReferenceError(_message, out _error);
         if (IsWindows && !Is32) return Native64.JsCreateReferenceError(_message, out _error);
         if (IsLinux && !Is32) return Native64Linux.JsCreateReferenceError(_message, out _error);
         if (IsMac && !Is32) return Native64Mac.JsCreateReferenceError(_message, out _error);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateReferenceError(_message, out _error)
            : Native64.JsCreateReferenceError(_message, out _error);
#endif
      }

      public static JsErrorCode JsCreateSyntaxError(JsValue _message, out JsValue _error)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateSyntaxError(_message, out _error);
         if (IsWindows && !Is32) return Native64.JsCreateSyntaxError(_message, out _error);
         if (IsLinux && !Is32) return Native64Linux.JsCreateSyntaxError(_message, out _error);
         if (IsMac && !Is32) return Native64Mac.JsCreateSyntaxError(_message, out _error);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateSyntaxError(_message, out _error)
            : Native64.JsCreateSyntaxError(_message, out _error);
#endif
      }

      public static JsErrorCode JsCreateTypeError(JsValue _message, out JsValue _error)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateTypeError(_message, out _error);
         if (IsWindows && !Is32) return Native64.JsCreateTypeError(_message, out _error);
         if (IsLinux && !Is32) return Native64Linux.JsCreateTypeError(_message, out _error);
         if (IsMac && !Is32) return Native64Mac.JsCreateTypeError(_message, out _error);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateTypeError(_message, out _error)
            : Native64.JsCreateTypeError(_message, out _error);
#endif
      }

      public static JsErrorCode JsCreateUriError(JsValue _message, out JsValue _error)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateURIError(_message, out _error);
         if (IsWindows && !Is32) return Native64.JsCreateURIError(_message, out _error);
         if (IsLinux && !Is32) return Native64Linux.JsCreateURIError(_message, out _error);
         if (IsMac && !Is32) return Native64Mac.JsCreateURIError(_message, out _error);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateURIError(_message, out _error)
            : Native64.JsCreateURIError(_message, out _error);
#endif
      }

      public static JsErrorCode JsHasException(out bool _hasException)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsHasException(out _hasException);
         if (IsWindows && !Is32) return Native64.JsHasException(out _hasException);
         if (IsLinux && !Is32) return Native64Linux.JsHasException(out _hasException);
         if (IsMac && !Is32) return Native64Mac.JsHasException(out _hasException);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsHasException(out _hasException)
            : Native64.JsHasException(out _hasException);
#endif
      }

      public static JsErrorCode JsGetAndClearException(out JsValue _exception)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetAndClearException(out _exception);
         if (IsWindows && !Is32) return Native64.JsGetAndClearException(out _exception);
         if (IsLinux && !Is32) return Native64Linux.JsGetAndClearException(out _exception);
         if (IsMac && !Is32) return Native64Mac.JsGetAndClearException(out _exception);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetAndClearException(out _exception)
            : Native64.JsGetAndClearException(out _exception);
#endif
      }

      public static JsErrorCode JsSetException(JsValue _exception)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetException(_exception);
         if (IsWindows && !Is32) return Native64.JsSetException(_exception);
         if (IsLinux && !Is32) return Native64Linux.JsSetException(_exception);
         if (IsMac && !Is32) return Native64Mac.JsSetException(_exception);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetException(_exception)
            : Native64.JsSetException(_exception);
#endif
      }

      public static JsErrorCode JsDisableRuntimeExecution(JsRuntime _runtime)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDisableRuntimeExecution(_runtime);
         if (IsWindows && !Is32) return Native64.JsDisableRuntimeExecution(_runtime);
         if (IsLinux && !Is32) return Native64Linux.JsDisableRuntimeExecution(_runtime);
         if (IsMac && !Is32) return Native64Mac.JsDisableRuntimeExecution(_runtime);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDisableRuntimeExecution(_runtime)
            : Native64.JsDisableRuntimeExecution(_runtime);
#endif
      }

      public static JsErrorCode JsEnableRuntimeExecution(JsRuntime _runtime)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsEnableRuntimeExecution(_runtime);
         if (IsWindows && !Is32) return Native64.JsEnableRuntimeExecution(_runtime);
         if (IsLinux && !Is32) return Native64Linux.JsEnableRuntimeExecution(_runtime);
         if (IsMac && !Is32) return Native64Mac.JsEnableRuntimeExecution(_runtime);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsEnableRuntimeExecution(_runtime)
            : Native64.JsEnableRuntimeExecution(_runtime);
#endif
      }

      public static JsErrorCode JsIsRuntimeExecutionDisabled(JsRuntime _runtime, out bool _isDisabled)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled);
         if (IsWindows && !Is32) return Native64.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled);
         if (IsLinux && !Is32) return Native64Linux.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled);
         if (IsMac && !Is32) return Native64Mac.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled)
            : Native64.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled);
#endif
      }

      public static JsErrorCode JsSetObjectBeforeCollectCallback(JsValue _reference,
         IntPtr _callbackState,
         JsObjectBeforeCollectCallback _beforeCollectCallback)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetObjectBeforeCollectCallback(_reference, _callbackState, _beforeCollectCallback);
         if (IsWindows && !Is32) return Native64.JsSetObjectBeforeCollectCallback(_reference, _callbackState, _beforeCollectCallback);
         if (IsLinux && !Is32) return Native64Linux.JsSetObjectBeforeCollectCallback(_reference, _callbackState, _beforeCollectCallback);
         if (IsMac && !Is32) return Native64Mac.JsSetObjectBeforeCollectCallback(_reference, _callbackState, _beforeCollectCallback);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetObjectBeforeCollectCallback(_reference, _callbackState, _beforeCollectCallback)
            : Native64.JsSetObjectBeforeCollectCallback(_reference, _callbackState, _beforeCollectCallback);
#endif
      }

      public static JsErrorCode JsCreateNamedFunction(JsValue _name,
         JsNativeFunction _nativeFunction,
         IntPtr _callbackState,
         out JsValue _function)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateNamedFunction(_name, _nativeFunction, _callbackState, out _function);
         if (IsWindows && !Is32) return Native64.JsCreateNamedFunction(_name, _nativeFunction, _callbackState, out _function);
         if (IsLinux && !Is32) return Native64Linux.JsCreateNamedFunction(_name, _nativeFunction, _callbackState, out _function);
         if (IsMac && !Is32) return Native64Mac.JsCreateNamedFunction(_name, _nativeFunction, _callbackState, out _function);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateNamedFunction(_name, _nativeFunction, _callbackState, out _function)
            : Native64.JsCreateNamedFunction(_name, _nativeFunction, _callbackState, out _function);
#endif
      }

      public static JsErrorCode JsSetPromiseContinuationCallback(
         JsPromiseContinuationCallback _promiseContinuationCallback,
         IntPtr _callbackState)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetPromiseContinuationCallback(_promiseContinuationCallback, _callbackState);
         if (IsWindows && !Is32) return Native64.JsSetPromiseContinuationCallback(_promiseContinuationCallback, _callbackState);
         if (IsLinux && !Is32) return Native64Linux.JsSetPromiseContinuationCallback(_promiseContinuationCallback, _callbackState);
         if (IsMac && !Is32) return Native64Mac.JsSetPromiseContinuationCallback(_promiseContinuationCallback, _callbackState);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetPromiseContinuationCallback(_promiseContinuationCallback, _callbackState)
            : Native64.JsSetPromiseContinuationCallback(_promiseContinuationCallback, _callbackState);
#endif
      }

      public static JsErrorCode JsCreateArrayBuffer(uint _byteLength, out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateArrayBuffer(_byteLength, out _result);
         if (IsWindows && !Is32) return Native64.JsCreateArrayBuffer(_byteLength, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsCreateArrayBuffer(_byteLength, out _result);
         if (IsMac && !Is32) return Native64Mac.JsCreateArrayBuffer(_byteLength, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateArrayBuffer(_byteLength, out _result)
            : Native64.JsCreateArrayBuffer(_byteLength, out _result);
#endif
      }

      public static JsErrorCode JsCreateTypedArray(JavaScriptTypedArrayType _arrayType,
         JsValue _arrayBuffer,
         uint _byteOffset,
         uint _elementLength,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result);
         if (IsWindows && !Is32) return Native64.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result);
         if (IsMac && !Is32) return Native64Mac.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result)
            : Native64.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result);
#endif
      }

      public static JsErrorCode JsCreateDataView(JsValue _arrayBuffer,
         uint _byteOffset,
         uint _byteOffsetLength,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result);
         if (IsWindows && !Is32) return Native64.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result);
         if (IsMac && !Is32) return Native64Mac.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result)
            : Native64.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result);
#endif
      }

      public static JsErrorCode JsGetArrayBufferStorage(JsValue _arrayBuffer, out IntPtr _buffer,
         out uint _bufferLength)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength);
         if (IsWindows && !Is32) return Native64.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength);
         if (IsLinux && !Is32) return Native64Linux.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength);
         if (IsMac && !Is32) return Native64Mac.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength)
            : Native64.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength);
#endif
      }

      public static JsErrorCode JsGetTypedArrayStorage(JsValue _typedArray,
         out IntPtr _buffer,
         out uint _bufferLength,
         out JavaScriptTypedArrayType _arrayType,
         out int _elementSize)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize);
         if (IsWindows && !Is32) return Native64.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize);
         if (IsLinux && !Is32) return Native64Linux.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize);
         if (IsMac && !Is32) return Native64Mac.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize)
            : Native64.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize);
#endif
      }

      public static JsErrorCode JsGetDataViewStorage(JsValue _dataView, out IntPtr _buffer, out uint _bufferLength)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength);
         if (IsWindows && !Is32) return Native64.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength);
         if (IsLinux && !Is32) return Native64Linux.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength);
         if (IsMac && !Is32) return Native64Mac.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength)
            : Native64.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength);
#endif
      }

      public static JsErrorCode JsGetPropertyIdType(JsPropertyId _propertyId, out JsPropertyIdType _propertyIdType)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetPropertyIdType(_propertyId, out _propertyIdType);
         if (IsWindows && !Is32) return Native64.JsGetPropertyIdType(_propertyId, out _propertyIdType);
         if (IsLinux && !Is32) return Native64Linux.JsGetPropertyIdType(_propertyId, out _propertyIdType);
         if (IsMac && !Is32) return Native64Mac.JsGetPropertyIdType(_propertyId, out _propertyIdType);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetPropertyIdType(_propertyId, out _propertyIdType)
            : Native64.JsGetPropertyIdType(_propertyId, out _propertyIdType);
#endif
      }

      public static JsErrorCode JsCreateSymbol(JsValue _description, out JsValue _symbol)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateSymbol(_description, out _symbol);
         if (IsWindows && !Is32) return Native64.JsCreateSymbol(_description, out _symbol);
         if (IsLinux && !Is32) return Native64Linux.JsCreateSymbol(_description, out _symbol);
         if (IsMac && !Is32) return Native64Mac.JsCreateSymbol(_description, out _symbol);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateSymbol(_description, out _symbol)
            : Native64.JsCreateSymbol(_description, out _symbol);
#endif
      }

      public static JsErrorCode JsGetSymbolFromPropertyId(JsPropertyId _propertyId, out JsValue _symbol)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetSymbolFromPropertyId(_propertyId, out _symbol);
         if (IsWindows && !Is32) return Native64.JsGetSymbolFromPropertyId(_propertyId, out _symbol);
         if (IsLinux && !Is32) return Native64Linux.JsGetSymbolFromPropertyId(_propertyId, out _symbol);
         if (IsMac && !Is32) return Native64Mac.JsGetSymbolFromPropertyId(_propertyId, out _symbol);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetSymbolFromPropertyId(_propertyId, out _symbol)
            : Native64.JsGetSymbolFromPropertyId(_propertyId, out _symbol);
#endif
      }

      public static JsErrorCode JsGetPropertyIdFromSymbol(JsValue _symbol, out JsPropertyId _propertyId)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetPropertyIdFromSymbol(_symbol, out _propertyId);
         if (IsWindows && !Is32) return Native64.JsGetPropertyIdFromSymbol(_symbol, out _propertyId);
         if (IsLinux && !Is32) return Native64Linux.JsGetPropertyIdFromSymbol(_symbol, out _propertyId);
         if (IsMac && !Is32) return Native64Mac.JsGetPropertyIdFromSymbol(_symbol, out _propertyId);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetPropertyIdFromSymbol(_symbol, out _propertyId)
            : Native64.JsGetPropertyIdFromSymbol(_symbol, out _propertyId);
#endif
      }

      public static JsErrorCode JsGetOwnPropertySymbols(JsValue _obj, out JsValue _propertySymbols)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetOwnPropertySymbols(_obj, out _propertySymbols);
         if (IsWindows && !Is32) return Native64.JsGetOwnPropertySymbols(_obj, out _propertySymbols);
         if (IsLinux && !Is32) return Native64Linux.JsGetOwnPropertySymbols(_obj, out _propertySymbols);
         if (IsMac && !Is32) return Native64Mac.JsGetOwnPropertySymbols(_obj, out _propertySymbols);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetOwnPropertySymbols(_obj, out _propertySymbols)
            : Native64.JsGetOwnPropertySymbols(_obj, out _propertySymbols);
#endif
      }

      public static JsErrorCode JsNumberToInt(JsValue _value, out int _intValue)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsNumberToInt(_value, out _intValue);
         if (IsWindows && !Is32) return Native64.JsNumberToInt(_value, out _intValue);
         if (IsLinux && !Is32) return Native64Linux.JsNumberToInt(_value, out _intValue);
         if (IsMac && !Is32) return Native64Mac.JsNumberToInt(_value, out _intValue);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsNumberToInt(_value, out _intValue)
            : Native64.JsNumberToInt(_value, out _intValue);
#endif
      }

      public static JsErrorCode JsSetIndexedPropertiesToExternalData(JsValue _obj,
         IntPtr _data,
         JavaScriptTypedArrayType _arrayType,
         uint _elementLength)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength);
         if (IsWindows && !Is32) return Native64.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength);
         if (IsLinux && !Is32) return Native64Linux.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength);
         if (IsMac && !Is32) return Native64Mac.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength)
            : Native64.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength);
#endif
      }

      public static JsErrorCode JsGetIndexedPropertiesExternalData(JsValue _obj,
         IntPtr _data,
         out JavaScriptTypedArrayType _arrayType,
         out uint _elementLength)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength);
         if (IsWindows && !Is32) return Native64.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength);
         if (IsLinux && !Is32) return Native64Linux.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength);
         if (IsMac && !Is32) return Native64Mac.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength)
            : Native64.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength);
#endif
      }

      public static JsErrorCode JsHasIndexedPropertiesExternalData(JsValue _obj, out bool _value)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsHasIndexedPropertiesExternalData(_obj, out _value);
         if (IsWindows && !Is32) return Native64.JsHasIndexedPropertiesExternalData(_obj, out _value);
         if (IsLinux && !Is32) return Native64Linux.JsHasIndexedPropertiesExternalData(_obj, out _value);
         if (IsMac && !Is32) return Native64Mac.JsHasIndexedPropertiesExternalData(_obj, out _value);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsHasIndexedPropertiesExternalData(_obj, out _value)
            : Native64.JsHasIndexedPropertiesExternalData(_obj, out _value);
#endif
      }

      public static JsErrorCode JsInstanceOf(JsValue _obj, JsValue _constructor, out bool _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsInstanceOf(_obj, _constructor, out _result);
         if (IsWindows && !Is32) return Native64.JsInstanceOf(_obj, _constructor, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsInstanceOf(_obj, _constructor, out _result);
         if (IsMac && !Is32) return Native64Mac.JsInstanceOf(_obj, _constructor, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsInstanceOf(_obj, _constructor, out _result)
            : Native64.JsInstanceOf(_obj, _constructor, out _result);
#endif
      }

      public static JsErrorCode JsCreateExternalArrayBuffer(IntPtr _data,
         uint _byteLength,
         JsObjectFinalizeCallback _finalizeCallback,
         IntPtr _callbackState,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeCallback, _callbackState, out _result);
         if (IsWindows && !Is32) return Native64.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeCallback, _callbackState, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeCallback, _callbackState, out _result);
         if (IsMac && !Is32) return Native64Mac.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeCallback, _callbackState, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeCallback, _callbackState, out _result)
            : Native64.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeCallback, _callbackState, out _result);
#endif
      }

      public static JsErrorCode JsGetTypedArrayInfo(JsValue _typedArray,
         out JavaScriptTypedArrayType _arrayType,
         out JsValue _arrayBuffer,
         out uint _byteOffset,
         out uint _byteLength)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength);
         if (IsWindows && !Is32) return Native64.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength);
         if (IsLinux && !Is32) return Native64Linux.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength);
         if (IsMac && !Is32) return Native64Mac.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength)
            : Native64.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength);
#endif
      }

      public static JsErrorCode JsGetContextOfObject(JsValue _obj, out JsContext _context)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetContextOfObject(_obj, out _context);
         if (IsWindows && !Is32) return Native64.JsGetContextOfObject(_obj, out _context);
         if (IsLinux && !Is32) return Native64Linux.JsGetContextOfObject(_obj, out _context);
         if (IsMac && !Is32) return Native64Mac.JsGetContextOfObject(_obj, out _context);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetContextOfObject(_obj, out _context)
            : Native64.JsGetContextOfObject(_obj, out _context);
#endif
      }

      public static JsErrorCode JsGetContextData(JsContext _context, out IntPtr _data)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsGetContextData(_context, out _data);
         if (IsWindows && !Is32) return Native64.JsGetContextData(_context, out _data);
         if (IsLinux && !Is32) return Native64Linux.JsGetContextData(_context, out _data);
         if (IsMac && !Is32) return Native64Mac.JsGetContextData(_context, out _data);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsGetContextData(_context, out _data)
            : Native64.JsGetContextData(_context, out _data);
#endif
      }

      public static JsErrorCode JsSetContextData(JsContext _context, IntPtr _data)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetContextData(_context, _data);
         if (IsWindows && !Is32) return Native64.JsSetContextData(_context, _data);
         if (IsLinux && !Is32) return Native64Linux.JsSetContextData(_context, _data);
         if (IsMac && !Is32) return Native64Mac.JsSetContextData(_context, _data);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetContextData(_context, _data)
            : Native64.JsSetContextData(_context, _data);
#endif
      }

      public static JsErrorCode JsParseSerializedScriptWithCallback(
         JavaScriptSerializedScriptLoadSourceCallback _scriptLoadCallback,
         JavaScriptSerializedScriptUnloadCallback _scriptUnloadCallback,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsParseSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsWindows && !Is32) return Native64.JsParseSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsParseSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsMac && !Is32) return Native64Mac.JsParseSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsParseSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result)
            : Native64.JsParseSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
#endif
      }

      public static JsErrorCode JsRunSerializedScriptWithCallback(
         JavaScriptSerializedScriptLoadSourceCallback _scriptLoadCallback,
         JavaScriptSerializedScriptUnloadCallback _scriptUnloadCallback,
         byte[] _buffer,
         JsSourceContext _sourceContext,
         string _sourceUrl,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsRunSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsWindows && !Is32) return Native64.JsRunSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsRunSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
         if (IsMac && !Is32) return Native64Mac.JsRunSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsRunSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result)
            : Native64.JsRunSerializedScriptWithCallback(_scriptLoadCallback, _scriptUnloadCallback, _buffer, _sourceContext, _sourceUrl, out _result);
#endif
      }

      public static JsErrorCode JsInitializeModuleRecord(
         JsModuleRecord _referencingModule,
         JsValue _normalizedSpecifier,
         out JsModuleRecord _moduleRecord)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsInitializeModuleRecord(_referencingModule, _normalizedSpecifier, out _moduleRecord);
         if (IsWindows && !Is32) return Native64.JsInitializeModuleRecord(_referencingModule, _normalizedSpecifier, out _moduleRecord);
         if (IsLinux && !Is32) return Native64Linux.JsInitializeModuleRecord(_referencingModule, _normalizedSpecifier, out _moduleRecord);
         if (IsMac && !Is32) return Native64Mac.JsInitializeModuleRecord(_referencingModule, _normalizedSpecifier, out _moduleRecord);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsInitializeModuleRecord(_referencingModule, _normalizedSpecifier, out _moduleRecord)
            : Native64.JsInitializeModuleRecord(_referencingModule, _normalizedSpecifier, out _moduleRecord);
#endif
      }

      public static JsErrorCode JsSetModuleHostInfo(JsModuleRecord _module, JsFetchImportedModuleCallBack _callback)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleCallback, _callback);
         if (IsWindows && !Is32) return Native64.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleCallback, _callback);
         if (IsLinux && !Is32) return Native64Linux.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleCallback, _callback);
         if (IsMac && !Is32) return Native64Mac.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleCallback, _callback);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleCallback, _callback)
            : Native64.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleCallback, _callback);
#endif
      }

      public static JsErrorCode JsSetModuleHostInfo(JsModuleRecord _module, JsNotifyModuleReadyCallback _callback)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.NotifyModuleReadyCallback, _callback);
         if (IsWindows && !Is32) return Native64.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.NotifyModuleReadyCallback, _callback);
         if (IsLinux && !Is32) return Native64Linux.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.NotifyModuleReadyCallback, _callback);
         if (IsMac && !Is32) return Native64Mac.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.NotifyModuleReadyCallback, _callback);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.NotifyModuleReadyCallback, _callback)
            : Native64.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.NotifyModuleReadyCallback, _callback);
#endif
      }

      public static JsErrorCode JsSetModuleHostInfo(JsModuleRecord _module,
         JsFetchImportedModuleFromScriptCallback _callback)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, _callback);
         if (IsWindows && !Is32) return Native64.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, _callback);
         if (IsLinux && !Is32) return Native64Linux.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, _callback);
         if (IsMac && !Is32) return Native64Mac.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, _callback);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, _callback)
            : Native64.JsSetModuleHostInfo(_module, JsModuleHostInfoKind.FetchImportedModuleFromScriptCallback, _callback);
#endif
      }

      public static JsErrorCode JsParseModuleSource(
         JsModuleRecord _requestModule,
         JsSourceContext _sourceContext,
         byte[] _script,
         uint _scriptLength,
         JsParseModuleSourceFlags _sourceFlag,
         out JsValue _exception)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsParseModuleSource(_requestModule, _sourceContext, _script, _scriptLength, _sourceFlag, out _exception);
         if (IsWindows && !Is32) return Native64.JsParseModuleSource(_requestModule, _sourceContext, _script, _scriptLength, _sourceFlag, out _exception);
         if (IsLinux && !Is32) return Native64Linux.JsParseModuleSource(_requestModule, _sourceContext, _script, _scriptLength, _sourceFlag, out _exception);
         if (IsMac && !Is32) return Native64Mac.JsParseModuleSource(_requestModule, _sourceContext, _script, _scriptLength, _sourceFlag, out _exception);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsParseModuleSource(_requestModule, _sourceContext, _script, _scriptLength, _sourceFlag, out _exception)
            : Native64.JsParseModuleSource(_requestModule, _sourceContext, _script, _scriptLength, _sourceFlag, out _exception);
#endif
      }

      public static JsErrorCode JsModuleEvaluation(
         JsModuleRecord _requestModule,
         out JsValue _result)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsModuleEvaluation(_requestModule, out _result);
         if (IsWindows && !Is32) return Native64.JsModuleEvaluation(_requestModule, out _result);
         if (IsLinux && !Is32) return Native64Linux.JsModuleEvaluation(_requestModule, out _result);
         if (IsMac && !Is32) return Native64Mac.JsModuleEvaluation(_requestModule, out _result);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsModuleEvaluation(_requestModule, out _result)
            : Native64.JsModuleEvaluation(_requestModule, out _result);
#endif
      }

      public static JsErrorCode JsDiagStartDebugging(
         JsRuntime _runtime,
         JsDiagDebugEventCallback _debugEventCallback,
         IntPtr _callbackState)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDiagStartDebugging(_runtime, _debugEventCallback, _callbackState);
         if (IsWindows && !Is32) return Native64.JsDiagStartDebugging(_runtime, _debugEventCallback, _callbackState);
         if (IsLinux && !Is32) return Native64Linux.JsDiagStartDebugging(_runtime, _debugEventCallback, _callbackState);
         if (IsMac && !Is32) return Native64Mac.JsDiagStartDebugging(_runtime, _debugEventCallback, _callbackState);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDiagStartDebugging(_runtime, _debugEventCallback, _callbackState)
            : Native64.JsDiagStartDebugging(_runtime, _debugEventCallback, _callbackState);
#endif
      }

      public static JsErrorCode JsDiagStopDebugging(JsRuntime _runtime, out IntPtr _callbackState)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDiagStopDebugging(_runtime, out _callbackState);
         if (IsWindows && !Is32) return Native64.JsDiagStopDebugging(_runtime, out _callbackState);
         if (IsLinux && !Is32) return Native64Linux.JsDiagStopDebugging(_runtime, out _callbackState);
         if (IsMac && !Is32) return Native64Mac.JsDiagStopDebugging(_runtime, out _callbackState);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDiagStopDebugging(_runtime, out _callbackState)
            : Native64.JsDiagStopDebugging(_runtime, out _callbackState);
#endif
      }

      public static JsErrorCode SetBreakpoint(uint _scriptId, uint _lineNumber, uint _column, out JsValue _breakpoint)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint);
         if (IsWindows && !Is32) return Native64.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint);
         if (IsLinux && !Is32) return Native64Linux.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint);
         if (IsMac && !Is32) return Native64Mac.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint)
            : Native64.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint);
#endif
      }

      public static JsErrorCode JsDiagRequestAsyncBreak(JsRuntime _jsRuntime)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDiagRequestAsyncBreak(_jsRuntime);
         if (IsWindows && !Is32) return Native64.JsDiagRequestAsyncBreak(_jsRuntime);
         if (IsLinux && !Is32) return Native64Linux.JsDiagRequestAsyncBreak(_jsRuntime);
         if (IsMac && !Is32) return Native64Mac.JsDiagRequestAsyncBreak(_jsRuntime);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDiagRequestAsyncBreak(_jsRuntime)
            : Native64.JsDiagRequestAsyncBreak(_jsRuntime);
#endif
      }

      public static JsErrorCode JsDiagGetBreakpoints(out JsValue _breakpoints)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDiagGetBreakpoints(out _breakpoints);
         if (IsWindows && !Is32) return Native64.JsDiagGetBreakpoints(out _breakpoints);
         if (IsLinux && !Is32) return Native64Linux.JsDiagGetBreakpoints(out _breakpoints);
         if (IsMac && !Is32) return Native64Mac.JsDiagGetBreakpoints(out _breakpoints);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDiagGetBreakpoints(out _breakpoints)
            : Native64.JsDiagGetBreakpoints(out _breakpoints);
#endif
      }

      public static JsErrorCode JsDiagRemoveBreakpoint(uint _breakpointId)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDiagRemoveBreakpoint(_breakpointId);
         if (IsWindows && !Is32) return Native64.JsDiagRemoveBreakpoint(_breakpointId);
         if (IsLinux && !Is32) return Native64Linux.JsDiagRemoveBreakpoint(_breakpointId);
         if (IsMac && !Is32) return Native64Mac.JsDiagRemoveBreakpoint(_breakpointId);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDiagRemoveBreakpoint(_breakpointId)
            : Native64.JsDiagRemoveBreakpoint(_breakpointId);
#endif
      }

      public static JsErrorCode JsDiagGetScripts(out JsValue _scripts)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDiagGetScripts(out _scripts);
         if (IsWindows && !Is32) return Native64.JsDiagGetScripts(out _scripts);
         if (IsLinux && !Is32) return Native64Linux.JsDiagGetScripts(out _scripts);
         if (IsMac && !Is32) return Native64Mac.JsDiagGetScripts(out _scripts);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDiagGetScripts(out _scripts)
            : Native64.JsDiagGetScripts(out _scripts);
#endif
      }

      public static JsErrorCode JsDiagEvaluate(JsValue _expression, uint _stackFrameIndex,
         JsParseScriptAttributes _parseAttributes, bool _forceSetValueProp, out JsValue _eval)
      {
#if NETSTANDARD
         if (IsWindows && Is32) return Native32.JsDiagEvaluate(_expression, _stackFrameIndex, _parseAttributes, _forceSetValueProp, out _eval);
         if (IsWindows && !Is32) return Native64.JsDiagEvaluate(_expression, _stackFrameIndex, _parseAttributes, _forceSetValueProp, out _eval);
         if (IsLinux && !Is32) return Native64Linux.JsDiagEvaluate(_expression, _stackFrameIndex, _parseAttributes, _forceSetValueProp, out _eval);
         if (IsMac && !Is32) return Native64Mac.JsDiagEvaluate(_expression, _stackFrameIndex, _parseAttributes, _forceSetValueProp, out _eval);
         throw new NotSupportedException("The current operation system is not supported.");
#else
         return Is32
            ? Native32.JsDiagEvaluate(_expression, _stackFrameIndex, _parseAttributes, _forceSetValueProp, out _eval)
            : Native64.JsDiagEvaluate(_expression, _stackFrameIndex, _parseAttributes, _forceSetValueProp, out _eval);
#endif
      }
   }
}