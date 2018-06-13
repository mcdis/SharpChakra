using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpChakra;
using SharpChakra.Json;

namespace Sample.Json
{
    internal class Program
    {
        private static async Task Main()
        {
            using (var jsrt = JsRuntime.Create())
            {
                await jsrt.CreateContext().RequestScopeAsync(context =>
                {
                    context.Global // Register Global Function
                        .SetProperty("dump", // dump
                            JsValue.FromDelegate(x =>
                            {
                                Console.WriteLine("-- dump --");
                                Console.WriteLine(x.Arguments[1].ToJToken(context).ToString(Formatting.Indented));
                                return JObject.Parse("{status:'ok',error:-1}").ToJsValue(context);
                            }));

                    Console.WriteLine("-- executing --");
                    var res = context.RunScript("dump({id:4,name:'chakra'});");

                    Console.WriteLine("-- result --");
                    Console.WriteLine(res.ToJToken(context).ToString(Formatting.Indented));
                });
            }

            Console.WriteLine("Finished... Press enter to exit...");
            Console.ReadLine();
        }
    }
}