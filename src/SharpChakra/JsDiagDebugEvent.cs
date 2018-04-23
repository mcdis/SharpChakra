namespace SharpChakra
{
   public enum JsDiagDebugEvent
   {
      JsDiagDebugEventSourceCompile = 0,
      JsDiagDebugEventCompileError = 1,
      JsDiagDebugEventBreakpoint = 2,
      JsDiagDebugEventStepComplete = 3,
      JsDiagDebugEventDebuggerStatement = 4,
      JsDiagDebugEventAsyncBreak = 5,
      JsDiagDebugEventRuntimeException = 6
   }
}