using System;

namespace SharpChakra
{
   public delegate bool JsMemoryAllocationCallback(IntPtr callbackState, JsMemoryEventType allocationEvent, UIntPtr allocationSize);
}
