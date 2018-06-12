using System;
using System.IO;
using System.Text;
using SharpChakra;
using SharpChakra.Extensions;

namespace Sample
{
   class Program
   {
      static void Main()
      {
         using (var jsrt = JsRuntime.Create())
         using (jsrt.CreateContext().Scope())
         {
            var fn = new JsNativeFunctionBuilder();            
            var globalObject = JsValue.GetGlobalObject(); // Get JS Global Object

            globalObject // Register Global Functions
               .SetProperty("loginfo", // loginfo
                  fn.New(x => Console.WriteLine(BuildMsg(x.Arguments, x.ArgumentCount).ToString())),
                  true)
               .SetProperty("logerror", // logerror
                  fn.New(x =>
                  {
                     var msg = BuildMsg(x.Arguments, x.ArgumentCount).ToString(); // Build msg
                     var color = Console.ForegroundColor; // Save color
                     Console.ForegroundColor = ConsoleColor.DarkRed; // Change color to red
                     Console.WriteLine(msg); // Output
                     Console.ForegroundColor = color; // Restore color
                  }),
                  true);
            
            foreach (var js in Directory.EnumerateFiles("js", "*.js")) // Execute all scripts inside js folder
            {
               Console.WriteLine($"Executing '{js}' ... ------------------->");
               JsContext.RunScript(File.ReadAllText(js));
               Console.WriteLine($"<------------------- '{js}' executed");
            }
         }
         Console.WriteLine("Finished... Press enter to exit...");
         Console.ReadLine();
      }
      private static StringBuilder BuildMsg(JsValue[] arguments, ushort count)
      {
         var build = new StringBuilder();
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