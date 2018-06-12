using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpChakra;
using SharpChakra.Extensions;
using SharpChakra.Json;

namespace Sample.Json
{
    internal class Program
    {
        private static void Main()
        {
            using (var jsrt = JsRuntime.Create())
            {
                var context = jsrt.CreateContext();

                context.Global // Register Global Function
                    .SetProperty("dump", // dump
                        context.CreateFunction(x =>
                        {
                            Console.WriteLine("-- dump --");
                            Console.WriteLine(x.Arguments[1].ToJToken().ToString(Formatting.Indented));
                            return JObject.Parse("{status:'ok',error:-1}").ToJsValue(context);
                        }),
                        true);

                Console.WriteLine("-- executing --");
                var res = context.RunScript("dump({id:4,name:'chakra'});");

                Console.WriteLine("-- result --");
                Console.WriteLine(res.ToJToken().ToString(Formatting.Indented));
            }

            Console.WriteLine("Finished... Press enter to exit...");
            Console.ReadLine();
        }
    }
}