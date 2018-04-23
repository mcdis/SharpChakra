using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SharpChakra;
using SharpChakra.Extensions;
using SharpChakra.Json;

namespace Sample.Debugging
{
   class Program
   {
      static void Main()
      {
         using (var jsrt = JsRuntime.Create())
         {
            jsrt.StartDebugging((_event, _data, _state) =>
            {
               Console.WriteLine($"debugger>\r\n {_event}, {_data.ToJToken().ToString(Formatting.Indented)}");
               if (_event == JsDiagDebugEvent.JsDiagDebugEventSourceCompile)
               {
                  var scriptId = _data.GetProperty("scriptId").ToInt32();
                  jsrt.SetBreakpoint((uint)scriptId,5,0,out var breakpoint);
               }

               if (_event == JsDiagDebugEvent.JsDiagDebugEventBreakpoint)
               {
                  Console.WriteLine("debugger> Breakpoint! Enter to continue... (write 'break' to stop the script)");
                  Console.ReadLine();
               }
            });
            using (jsrt.CreateContext().Scope())
            {
               var fn = new JsNativeFunctionBuilder();
               var globalObject = JsValue.GlobalObject; // Get JS Global Object

               globalObject // Register Global Functions
                  .SetProperty("loginfo", // loginfo
                     fn.New(_x => Console.WriteLine(BuildMsg(_x.Arguments, _x.ArgumentCount).ToString())),
                     true);

               foreach (var js in Directory.EnumerateFiles("js", "*.js")) // Execute all scripts inside js folder
               {
                  Console.WriteLine($"Executing '{js}' ... ------------------->");
                  JsContext.RunScript(File.ReadAllText(js));
                  Console.WriteLine($"<------------------- '{js}' executed");
               }
            }

            //jsrt.StopDebugging();
         }

         Console.WriteLine("Finished... Press enter to exit...");
         Console.ReadLine();
      }
      private static StringBuilder BuildMsg(JsValue[] _arguments, ushort _count)
      {
         var build = new StringBuilder();
         build.Append("js> ");
         for (uint index = 1;
            index < _count;
            index++)
         {
            if (index > 1)
               build.Append(" ");

            build.Append(_arguments[index].ConvertToString().ToString());
         }
         return build;
      }
   }
}