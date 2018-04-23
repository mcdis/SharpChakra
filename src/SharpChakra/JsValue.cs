using System;
using System.Runtime.InteropServices;

namespace SharpChakra
{
   public struct JsValue
   {
      private readonly IntPtr p_reference;
      private JsValue(IntPtr _reference)
      {
         p_reference = _reference;
      }
      public static JsValue Invalid => new JsValue(IntPtr.Zero);
      public static JsValue Undefined
      {
         get
         {
            Native.ThrowIfError(Native.JsGetUndefinedValue(out var value));
            return value;
         }
      }
      public static JsValue Null
      {
         get
         {
            Native.ThrowIfError(Native.JsGetNullValue(out var value));
            return value;
         }
      }
      public static JsValue True
      {
         get
         {
            Native.ThrowIfError(Native.JsGetTrueValue(out var value));
            return value;
         }
      }
      public static JsValue False
      {
         get
         {
            Native.ThrowIfError(Native.JsGetFalseValue(out var value));
            return value;
         }
      }
      public static JsValue GetGlobalObject()
      {
         Native.ThrowIfError(Native.JsGetGlobalObject(out var value));
         return value;
      }
      public bool IsValid => p_reference != IntPtr.Zero;
      public JsValueType ValueType
      {
         get
         {
            Native.ThrowIfError(Native.JsGetValueType(this, out var type));
            return type;
         }
      }
      public int StringLength
      {
         get
         {
            Native.ThrowIfError(Native.JsGetStringLength(this, out var length));
            return length;
         }
      }
      public JsValue Prototype
      {
         get
         {
            Native.ThrowIfError(Native.JsGetPrototype(this, out var prototypeReference));
            return prototypeReference;
         }
         set => Native.ThrowIfError(Native.JsSetPrototype(this, value));
      }
      public bool IsExtensionAllowed
      {
         get
         {
            Native.ThrowIfError(Native.JsGetExtensionAllowed(this, out var allowed));
            return allowed;
         }
      }
      public bool HasExternalData
      {
         get
         {
            Native.ThrowIfError(Native.JsHasExternalData(this, out var hasExternalData));
            return hasExternalData;
         }
      }
      public IntPtr ExternalData
      {
         get
         {
            Native.ThrowIfError(Native.JsGetExternalData(this, out var data));
            return data;
         }
         set => Native.ThrowIfError(Native.JsSetExternalData(this, value));
      }
      public static JsValue FromBoolean(bool _value)
      {
         Native.ThrowIfError(Native.JsBoolToBoolean(_value, out var reference));
         return reference;
      }
      public static JsValue FromDouble(double _value)
      {
         Native.ThrowIfError(Native.JsDoubleToNumber(_value, out var reference));
         return reference;
      }
      public static JsValue FromInt32(int _value)
      {
         Native.ThrowIfError(Native.JsIntToNumber(_value, out var reference));
         return reference;
      }
      public static JsValue FromString(string _value)
      {
         Native.ThrowIfError(Native.JsPointerToString(_value, new UIntPtr((uint) _value.Length), out var reference));
         return reference;
      }
      public static JsValue CreateObject()
      {
         Native.ThrowIfError(Native.JsCreateObject(out var reference));
         return reference;
      }
      public static JsValue CreateExternalObject(IntPtr _data, JsObjectFinalizeCallback _finalizer)
      {
         Native.ThrowIfError(Native.JsCreateExternalObject(_data, _finalizer, out var reference));
         return reference;
      }
      public static JsValue CreateFunction(JsNativeFunction _function)
      {
         Native.ThrowIfError(Native.JsCreateFunction(_function, IntPtr.Zero, out var reference));
         return reference;
      }
      public static JsValue CreateFunction(JsNativeFunction _function, IntPtr _callbackData)
      {
         Native.ThrowIfError(Native.JsCreateFunction(_function, _callbackData, out var reference));
         return reference;
      }
      public static JsValue CreateArray(uint _length)
      {
         Native.ThrowIfError(Native.JsCreateArray(_length, out var reference));
         return reference;
      }
      public static JsValue CreateError(JsValue _message)
      {
         Native.ThrowIfError(Native.JsCreateError(_message, out var reference));
         return reference;
      }
      public static JsValue CreateRangeError(JsValue _message)
      {
         Native.ThrowIfError(Native.JsCreateRangeError(_message, out var reference));
         return reference;
      }
      public static JsValue CreateReferenceError(JsValue _message)
      {
         Native.ThrowIfError(Native.JsCreateReferenceError(_message, out var reference));
         return reference;
      }
      public static JsValue CreateSyntaxError(JsValue _message)
      {
         Native.ThrowIfError(Native.JsCreateSyntaxError(_message, out var reference));
         return reference;
      }
      public static JsValue CreateTypeError(JsValue _message)
      {
         Native.ThrowIfError(Native.JsCreateTypeError(_message, out var reference));
         return reference;
      }
      public static JsValue CreateUriError(JsValue _message)
      {
         Native.ThrowIfError(Native.JsCreateUriError(_message, out var reference));
         return reference;
      }
      public uint AddRef()
      {
         Native.ThrowIfError(Native.JsAddRef(this, out var count));
         return count;
      }
      public uint Release()
      {
         Native.ThrowIfError(Native.JsRelease(this, out var count));
         return count;
      }
      public bool ToBoolean()
      {
         Native.ThrowIfError(Native.JsBooleanToBool(this, out var value));
         return value;
      }
      public double ToDouble()
      {
         Native.ThrowIfError(Native.JsNumberToDouble(this, out var value));
         return value;
      }
      public int ToInt32()
      {
         Native.ThrowIfError(Native.JsNumberToInt(this, out var value));
         return value;
      }
      public new string ToString()
      {
         Native.ThrowIfError(Native.JsStringToPointer(this, out var buffer, out var length));
         return Marshal.PtrToStringUni(buffer, (int) length);
      }
      public JsValue ConvertToBoolean()
      {
         Native.ThrowIfError(Native.JsConvertValueToBoolean(this, out var booleanReference));
         return booleanReference;
      }
      public JsValue ConvertToNumber()
      {
         Native.ThrowIfError(Native.JsConvertValueToNumber(this, out var numberReference));
         return numberReference;
      }
      public JsValue ConvertToString()
      {
         Native.ThrowIfError(Native.JsConvertValueToString(this, out var stringReference));
         return stringReference;
      }
      public JsValue ConvertToObject()
      {
         Native.ThrowIfError(Native.JsConvertValueToObject(this, out var objectReference));
         return objectReference;
      }
      public void PreventExtension()
      {
         Native.ThrowIfError(Native.JsPreventExtension(this));
      }
      public JsValue GetOwnPropertyDescriptor(JsPropertyId _propertyId)
      {
         Native.ThrowIfError(Native.JsGetOwnPropertyDescriptor(this, _propertyId, out var descriptorReference));
         return descriptorReference;
      }
      public JsValue GetOwnPropertyNames()
      {
         Native.ThrowIfError(Native.JsGetOwnPropertyNames(this, out var propertyNamesReference));
         return propertyNamesReference;
      }
      public bool HasProperty(JsPropertyId _propertyId)
      {
         Native.ThrowIfError(Native.JsHasProperty(this, _propertyId, out var hasProperty));
         return hasProperty;
      }
      public JsValue GetProperty(JsPropertyId _id)
      {
         Native.ThrowIfError(Native.JsGetProperty(this, _id, out var propertyReference));
         return propertyReference;
      }
      public void SetProperty(JsPropertyId _id, JsValue _value, bool _useStrictRules) =>
         Native.ThrowIfError(Native.JsSetProperty(this, _id, _value, _useStrictRules));
      public JsValue DeleteProperty(JsPropertyId _propertyId, bool _useStrictRules)
      {
         Native.ThrowIfError(Native.JsDeleteProperty(this, _propertyId, _useStrictRules, out var returnReference));
         return returnReference;
      }
      public bool DefineProperty(JsPropertyId _propertyId, JsValue _propertyDescriptor)
      {
         Native.ThrowIfError(Native.JsDefineProperty(this, _propertyId, _propertyDescriptor, out var result));
         return result;
      }
      public bool HasIndexedProperty(JsValue _index)
      {
         Native.ThrowIfError(Native.JsHasIndexedProperty(this, _index, out var hasProperty));
         return hasProperty;
      }
      public JsValue GetIndexedProperty(JsValue _index)
      {
         Native.ThrowIfError(Native.JsGetIndexedProperty(this, _index, out var propertyReference));
         return propertyReference;
      }
      public void SetIndexedProperty(JsValue _index, JsValue _value) => Native.ThrowIfError(Native.JsSetIndexedProperty(this, _index, _value));
      public void DeleteIndexedProperty(JsValue _index)
      {
         Native.ThrowIfError(Native.JsDeleteIndexedProperty(this, _index));
      }
      public bool Equals(JsValue _other)
      {
         Native.ThrowIfError(Native.JsEquals(this, _other, out var equals));
         return equals;
      }
      public bool StrictEquals(JsValue _other)
      {
         Native.ThrowIfError(Native.JsStrictEquals(this, _other, out var equals));
         return equals;
      }
      public JsValue CallFunction(params JsValue[] _arguments)
      {
         if (_arguments.Length > ushort.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(_arguments));

         Native.ThrowIfError(Native.JsCallFunction(this, _arguments, (ushort) _arguments.Length, out var returnReference));
         return returnReference;
      }
      public JsValue ConstructObject(params JsValue[] _arguments)
      {
         if (_arguments.Length > ushort.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(_arguments));

         Native.ThrowIfError(Native.JsConstructObject(this, _arguments, (ushort) _arguments.Length, out var returnReference));
         return returnReference;
      }
   }
}