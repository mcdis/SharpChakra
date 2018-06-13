using System;

namespace SharpChakra
{
    public delegate void JsDiagDebugEventCallback(JsDiagDebugEvent debugEvent, JsValue eventData, IntPtr callbackState);
}