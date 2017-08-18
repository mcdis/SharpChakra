using System;
using System.Runtime.InteropServices;

namespace SharpChakra
{
   public delegate JsValue JsNativeFunction(JsValue _callee, [MarshalAs(UnmanagedType.U1)] bool _isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] _arguments, ushort _argumentCount, IntPtr _callbackData);
}
