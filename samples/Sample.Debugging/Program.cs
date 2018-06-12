using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SharpChakra;
using SharpChakra.Extensions;
using SharpChakra.Json;

namespace Sample.Debugging
{
    internal class Program
    {
        private static void Main()
        {
            using (var jsrt = JsRuntime.Create())
            {
                var context = jsrt.CreateContext();

                jsrt.StartDebugging((_event, data, state) =>
                {
                    Console.WriteLine($"debugger>\r\n {_event}, {data.ToJToken().ToString(Formatting.Indented)}");
                    if (_event == JsDiagDebugEvent.JsDiagDebugEventSourceCompile)
                    {
                        var scriptId = data.GetProperty("scriptId").ToInt32();
                        jsrt.SetBreakpoint((uint) scriptId, 5, 0, out var breakpoint);
                    }

                    if (_event == JsDiagDebugEvent.JsDiagDebugEventBreakpoint)
                    {
                        Console.WriteLine(
                            "debugger> Breakpoint! Enter to continue... (write 'break' to stop the script)");
                        var res = jsrt.DiagEvaluate(context.CreateString("debugVar"), 0,
                            JsParseScriptAttributes.JsParseScriptAttributeNone, false);
                        Console.WriteLine(
                            $"debugger> dump 'debugVar' info:\r\n {res.ToJToken().ToString(Formatting.Indented)}");
                        Console.ReadLine();
                    }
                });

                context.Global // Register Global Functions
                    .SetProperty("loginfo", // loginfo
                        context.CreateFunction(x => Console.WriteLine(BuildMsg(x.Arguments, x.ArgumentCount).ToString())));

                foreach (var js in Directory.EnumerateFiles("js", "*.js")) // Execute all scripts inside js folder
                {
                    Console.WriteLine($"Executing '{js}' ... ------------------->");
                    context.RunScript(File.ReadAllText(js));
                    Console.WriteLine($"<------------------- '{js}' executed");
                }

                //jsrt.StopDebugging();
            }

            Console.WriteLine("Finished... Press enter to exit...");
            Console.ReadLine();
        }

        private static StringBuilder BuildMsg(JsValue[] arguments, ushort count)
        {
            var build = new StringBuilder();
            build.Append("js> ");
            for (uint index = 1;
                index < count;
                index++)
            {
                if (index > 1)
                    build.Append(" ");

                build.Append(arguments[index].ConvertToString().ToString());
            }

            return build;
        }
    }
}