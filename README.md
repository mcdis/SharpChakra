# SharpChakra
Slim managed any cpu .net wrapper of Microsoft.ChakraCore
Simple auto call proxy to x86/x64 dllimport method based on Microsoft.ChakraCore C# Host Sample

# How to use
Install SharpChakra [nuget](https://www.nuget.org/packages/SharpChakra)

# Example

```csharp
using (var runtime = JavaScriptRuntime.Create())
using (new JavaScriptContext.Scope(runtime.CreateContext()))
{
   JavaScriptValue.GlobalObject
      .SetProperty(JavaScriptPropertyId.FromString("run"), // Register run function
         JavaScriptValue.CreateFunction((_callee, _call, _arguments, _count, _data) =>
            {
               Console.WriteLine("hello from script!"); // Output
               return JavaScriptValue.Invalid;
            },
            IntPtr.Zero),
         true);

   JavaScriptContext.RunScript("run();", JavaScriptSourceContext.FromIntPtr(IntPtr.Zero), "");
}
```
Output: 'hello from script!'

# Goals
- You can use exists js code
- You can extend your C# app by use script
- You can control script execution (you can terminate dead loop and limit memory consumption, tnx MS)
- You can build your own script system (game,config system and etc.)
- Speed, Chakra is very fast 
