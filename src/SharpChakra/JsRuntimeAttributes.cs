using System;

namespace SharpChakra
{
    [Flags]
    public enum JsRuntimeAttributes
    {
        None = 0x00000000,
        DisableBackgroundWork = 0x00000001,
        AllowScriptInterrupt = 0x00000002,
        EnableIdleProcessing = 0x00000004,
        DisableNativeCodeGeneration = 0x00000008,
        DisableEval = 0x00000010,
        EnableExperimentalFeatures = 0x00000020,
        DispatchSetExceptionsToDebugger = 0x00000040
    }
}