using System;

namespace SharpChakra
{
    public delegate void JsPromiseContinuationCallback(JsValue task, IntPtr callbackState);
}