using System;
using System.IO;
using System.Threading.Tasks;
using SharpChakra;

namespace Sample.Modules
{
    internal class Program
    {
        private static async Task Main()
        {
            using (var jsrt = JsRuntime.Create(JsRuntimeAttributes.EnableExperimentalFeatures,
                JsRuntimeVersion.VersionEdge))
            {
                await jsrt.CreateContext().RequestScopeAsync(context =>
                {
                    // Create the global method 'echo'.
                    context.Global.SetProperty("echo",
                        JsValue.FromDelegate(x => Console.WriteLine(x.Arguments[1].ToString())));

                    // Create the root Module
                    var rootModule = context.CreateModule(JsModuleRecord.Root, JsValue.FromString(""));

                    // Callback when a module is requested.
                    JsErrorCode OnFetch(JsModuleRecord reference, JsValue specifier, out JsModuleRecord record)
                    {
                        var name = specifier.ToString();
                        Console.WriteLine($"> importing '{name}'...");

                        // Create the module.
                        record = context.CreateModule(reference, specifier);

                        // Parse the module.
                        switch (name)
                        {
                            case "foo.js":
                                record.Parse(@"
                                import { data } from 'bar.js';

                                export let test = () => data;
                            ");
                                break;
                            case "bar.js":
                                record.Parse("export let data = 'Hello World!';");
                                break;
                            default:
                                throw new FileNotFoundException();
                        }

                        return JsErrorCode.NoError;
                    }

                    // Set the callback.
                    rootModule.SetHostInfo(OnFetch);

                    // Set the root module.
                    rootModule.Parse(@"
                        import { test } from 'foo.js';
                        echo(test());
                    ");

                    rootModule.Eval();
                });
            }

            Console.WriteLine("Finished... Press enter to exit...");
            Console.ReadLine();
        }
    }
}