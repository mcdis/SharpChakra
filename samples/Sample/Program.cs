using System;
using System.IO;
using System.Text;
using SharpChakra;
using SharpChakra.Extensions;

namespace Sample
{
    internal class Program
    {
        private static void Main()
        {
            using (var jsrt = JsRuntime.Create())
            {
                var context = jsrt.CreateContext();

                context.Global // Register Global Functions
                    .SetProperty("loginfo", // loginfo
                        context.CreateFunction(x => Console.WriteLine(BuildMsg(x.Arguments, x.ArgumentCount).ToString())))
                    .SetProperty("logerror", // logerror
                        context.CreateFunction(x =>
                        {
                            var msg = BuildMsg(x.Arguments, x.ArgumentCount).ToString(); // Build msg
                            var color = Console.ForegroundColor; // Save color
                            Console.ForegroundColor = ConsoleColor.DarkRed; // Change color to red
                            Console.WriteLine(msg); // Output
                            Console.ForegroundColor = color; // Restore color
                        }));

                foreach (var js in Directory.EnumerateFiles("js", "*.js")) // Execute all scripts inside js folder
                {
                    Console.WriteLine($"Executing '{js}' ... ------------------->");
                    context.RunScript(File.ReadAllText(js));
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