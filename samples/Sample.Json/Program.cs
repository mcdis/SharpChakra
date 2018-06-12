using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpChakra;
using SharpChakra.Extensions;
using SharpChakra.Json;

namespace Sample.Json
{
   class Program
   {
      static void Main()
      {
         using (var jsrt = JsRuntime.Create())
         using (jsrt.CreateContext().Scope())
         {
            var fn = new JsNativeFunctionBuilder();
            JsValue // Register Global Function
               .GetGlobalObject()
               .SetProperty("dump", // dump
               fn.New(x =>
                  {
                     Console.WriteLine("-- dump --");
                     Console.WriteLine(x.Arguments[1].ToJToken().ToString(Formatting.Indented));
                     return JObject.Parse("{status:'ok',error:-1}").ToJsValue();
                  }),
               true);

            Console.WriteLine("-- executing --");
            var res = JsContext.RunScript("dump({id:4,name:'chakra'});");

            Console.WriteLine("-- result --");
            Console.WriteLine(res.ToJToken().ToString(Formatting.Indented));
         }
         Console.WriteLine("Finished... Press enter to exit...");
         Console.ReadLine();
      }
   }
}
