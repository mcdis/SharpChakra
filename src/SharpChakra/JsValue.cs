using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace SharpChakra
{
    public struct JsValue
    {
        internal readonly IntPtr Reference;

        private JsValue(IntPtr reference)
        {
            Reference = reference;
        }

        public static JsValue Invalid => new JsValue(IntPtr.Zero);

        public JsContext Context
        {
            get
            {
                Native.ThrowIfError(Native.JsGetContextOfObject(this, out var context));
                return context;
            }
        }

        public bool IsValid => Reference != IntPtr.Zero;

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

        public JsValue GetOwnPropertyDescriptor(JsPropertyId propertyId)
        {
            Native.ThrowIfError(Native.JsGetOwnPropertyDescriptor(this, propertyId, out var descriptorReference));
            return descriptorReference;
        }

        public JsValue GetOwnPropertyNames()
        {
            Native.ThrowIfError(Native.JsGetOwnPropertyNames(this, out var propertyNamesReference));
            return propertyNamesReference;
        }

        public bool HasProperty(JsPropertyId propertyId)
        {
            Native.ThrowIfError(Native.JsHasProperty(this, propertyId, out var hasProperty));
            return hasProperty;
        }

        public JsValue GetProperty(JsPropertyId id)
        {
            Native.ThrowIfError(Native.JsGetProperty(this, id, out var propertyReference));
            return propertyReference;
        }

        public void SetProperty(JsPropertyId id, JsValue value, bool useStrictRules) =>
            Native.ThrowIfError(Native.JsSetProperty(this, id, value, useStrictRules));

        public JsValue DeleteProperty(JsPropertyId propertyId, bool useStrictRules)
        {
            Native.ThrowIfError(Native.JsDeleteProperty(this, propertyId, useStrictRules, out var returnReference));
            return returnReference;
        }

        public bool DefineProperty(JsPropertyId propertyId, JsValue propertyDescriptor)
        {
            Native.ThrowIfError(Native.JsDefineProperty(this, propertyId, propertyDescriptor, out var result));
            return result;
        }

        public bool HasIndexedProperty(JsValue index)
        {
            Native.ThrowIfError(Native.JsHasIndexedProperty(this, index, out var hasProperty));
            return hasProperty;
        }

        public JsValue GetIndexedProperty(JsValue index)
        {
            Native.ThrowIfError(Native.JsGetIndexedProperty(this, index, out var propertyReference));
            return propertyReference;
        }

        public void SetIndexedProperty(JsValue index, JsValue value) =>
            Native.ThrowIfError(Native.JsSetIndexedProperty(this, index, value));

        public void DeleteIndexedProperty(JsValue index)
        {
            Native.ThrowIfError(Native.JsDeleteIndexedProperty(this, index));
        }

        public bool Equals(JsValue other)
        {
            Native.ThrowIfError(Native.JsEquals(this, other, out var equals));
            return equals;
        }

        public bool StrictEquals(JsValue other)
        {
            Native.ThrowIfError(Native.JsStrictEquals(this, other, out var equals));
            return equals;
        }

        public JsValue CallFunction(params JsValue[] arguments)
        {
            if (arguments.Length > ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(arguments));

            Native.ThrowIfError(Native.JsCallFunction(this, arguments, (ushort) arguments.Length, out var returnReference));
            return returnReference;
        }

        public JsValue ConstructObject(params JsValue[] arguments)
        {
            if (arguments.Length > ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(arguments));

            Native.ThrowIfError(Native.JsConstructObject(this, arguments, (ushort) arguments.Length,
                out var returnReference));
            return returnReference;
        }
    }
}