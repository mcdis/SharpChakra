using System;

namespace SharpChakra
{
   /// <summary>
    ///     A background work item callback.
    /// </summary>
    /// <remarks>
    ///     This is passed to the host's thread service (if provided) to allow the host to 
    ///     invoke the work item callback on the background thread of its choice.
    /// </remarks>
    /// <param name="_callbackData">Data argument passed to the thread service.</param>
    public delegate void JsBackgroundWorkItemCallback(IntPtr _callbackData);
}
