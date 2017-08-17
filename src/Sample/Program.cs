using System;
using System.IO;
using System.Text;
using SharpChakra;

namespace Sample
{
   class Program
   {
      static void Main(string[] _args)
      {
         using (var runtime = JavaScriptRuntime.Create())
         {
            var context = runtime.CreateContext();
            using (new JavaScriptContext.Scope(context))
            {
               // Get JS Global Object
               var globalObject = JavaScriptValue.GlobalObject;

               // Register Global Function
               globalObject.SetProperty(JavaScriptPropertyId.FromString("loginfo"), // function name
                  JavaScriptValue.CreateFunction((_callee, _call, _arguments, _count, _data) =>
                     {
                        var build = BuildLogRecord(_arguments, _count); // Build msg
                        Console.WriteLine(build.ToString()); // Output
                        return JavaScriptValue.Invalid;
                     },
                     IntPtr.Zero),
                  true);

               // logerror
               globalObject.SetProperty(JavaScriptPropertyId.FromString("logerror"), // function name 
                  JavaScriptValue.CreateFunction((_callee, _call, _arguments, _count, _data) =>
                     {
                        var build = BuildLogRecord(_arguments, _count); // Build msg
                        var color = Console.ForegroundColor; // Save color
                        Console.ForegroundColor = ConsoleColor.DarkRed; // Change color to red
                        Console.WriteLine(build.ToString()); // Output
                        Console.ForegroundColor = color; // Restore color
                        return JavaScriptValue.Invalid;
                     },
                     IntPtr.Zero),
                  true);

               // Execute all scripts inside js folder
               var currentSourceContext = JavaScriptSourceContext.FromIntPtr(IntPtr.Zero);
               foreach (var js in Directory.EnumerateFiles("js","*.js"))
               {
                  Console.WriteLine($"Executing '{js}' ... ------------------->");
                  try
                  {
                     var src = File.ReadAllText(js);
                     JavaScriptContext.RunScript(src, currentSourceContext++, js);
                  }
                  catch (JavaScriptScriptException e)
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
      private static StringBuilder BuildLogRecord(JavaScriptValue[] _arguments, ushort _count)
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