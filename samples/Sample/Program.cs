using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SharpChakra;
using SharpChakra.Extensions;

namespace Sample
{
    internal class Program
    {
        private static async Task Main()
        {
            using (var jsrt = JsRuntime.Create())
            {
                var context = jsrt.CreateContext();

                await context.SetGlobalAsync("console", new JsConsole());

                // Execute all scripts inside the js folder
                foreach (var js in Directory.EnumerateFiles("js", "*.js"))
                {
                    Console.WriteLine($"> Executing '{js}'");

                    await context.EvalAsync(File.ReadAllText(js));

                    var name = await context.GetGlobalAsync("sayHelloWorld");

                    await name.CallFunctionAsync(null);
                }

                Console.ReadLine();
            }
        }

        public class JsConsole
        {
            public void Info(JsFunctionArgs args)
            {
                Console.WriteLine(BuildMsg(args.Arguments, args.ArgumentCount).ToString());
            }

            public void Error(JsFunctionArgs args)
            {
                var msg = BuildMsg(args.Arguments, args.ArgumentCount).ToString(); // Build msg
                var color = Console.ForegroundColor; // Save color
                Console.ForegroundColor = ConsoleColor.DarkRed; // Change color to red
                Console.WriteLine(msg); // Output
                Console.ForegroundColor = color; // Restore color
            }

            private static StringBuilder BuildMsg(JsValue[] arguments, ushort count)
            {
                var build = new StringBuilder();
                for (uint index = 1; index < count; index++)
                {
                    if (index > 1)
                        build.Append(" ");

                    build.Append(arguments[index].ConvertToString().ToString());
                }

                return build;
            }
        }
    }
}