using System;
using System.Runtime.InteropServices;

namespace SharpChakra
{
   public delegate JsValue JsNativeFunction(JsValue callee, [MarshalAs(UnmanagedType.U1)] bool isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] arguments, ushort argumentCount, IntPtr callbackData);
}
