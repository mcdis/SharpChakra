using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpChakra;
using SharpChakra.Json;

namespace Sample.Json
{
   class Program
   {
      static void Main()
      {
         using (var runtime = JsRuntime.Create())
         using (new JsContext.Scope(runtime.CreateContext()))
         {
            // Register Global Function
            JsValue
               .GlobalObject
               .SetProperty(JsPropertyId.FromString("dump"), // function name
               JsValue.CreateFunction((_callee, _call, _arguments, _count, _data) =>
                  {
                     Console.WriteLine("-- dump --");
                     Console.WriteLine(_arguments[1].ToJToken().ToString(Formatting.Indented));
                     return JObject.Parse("{status:'ok',error:-1}").ToJsValue();
                  },
                  IntPtr.Zero),
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
