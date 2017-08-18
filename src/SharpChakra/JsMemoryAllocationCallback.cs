using System;

namespace SharpChakra
{
   public delegate bool JsMemoryAllocationCallback(IntPtr _callbackState, JsMemoryEventType _allocationEvent, UIntPtr _allocationSize);
}
