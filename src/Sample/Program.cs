using System;
using System.IO;
using System.Text;
using SharpChakra;

namespace Sample
{
   class Program
   {
      static void Main()
      {
         using (var runtime = JsRuntime.Create())
         {
            var context = runtime.CreateContext();
            using (new JsContext.Scope(context))
            {
               // Get JS Global Object
               var globalObject = JsValue.GlobalObject;

               // Register Global Function
               globalObject.SetProperty(JsPropertyId.FromString("loginfo"), // function name
                  JsValue.CreateFunction((_callee, _call, _arguments, _count, _data) =>
                     {
                        var build = BuildLogRecord(_arguments, _count); // Build msg
                        Console.WriteLine(build.ToString()); // Output
                        return JsValue.Invalid;
                     },
                     IntPtr.Zero),
                  true);

               // logerror
               globalObject.SetProperty(JsPropertyId.FromString("logerror"), // function name 
                  JsValue.CreateFunction((_callee, _call, _arguments, _count, _data) =>
                     {
                        var build = BuildLogRecord(_arguments, _count); // Build msg
                        var color = Console.ForegroundColor; // Save color
                        Console.ForegroundColor = ConsoleColor.DarkRed; // Change color to red
                        Console.WriteLine(build.ToString()); // Output
                        Console.ForegroundColor = color; // Restore color
                        return JsValue.Invalid;
                     },
                     IntPtr.Zero),
                  true);

               // Execute all scripts inside js folder
               var currentSourceContext = JsSourceContext.FromIntPtr(IntPtr.Zero);
               foreach (var js in Directory.EnumerateFiles("js","*.js"))
               {
                  Console.WriteLine($"Executing '{js}' ... ------------------->");
                  try
                  {
                     var src = File.ReadAllText(js);
                     JsContext.RunScript(src, currentSourceContext++, js);
                  }
                  catch (JsScriptException e)
                  {
                     Console.WriteLine($"Can't process '{js}' script, {e}");
                  }
                  Console.WriteLine($"<------------------- '{js}' executed");
               }
            }
            Console.WriteLine("Finished... Press enter to exit...");
            Console.ReadLine();
         }
      }
      private static StringBuilder BuildLogRecord(JsValue[] _arguments, ushort _count)
      {
         var build = new StringBuilder();
         for (uint index = 1; index < _count; index++)
         {
            if (index > 1)
               build.Append(" ");

            build.Append(_arguments[index].ConvertToString().ToString());
         }

         return build;
      }
   }
}