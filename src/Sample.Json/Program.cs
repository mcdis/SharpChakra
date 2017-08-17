using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpChakra;
using SharpChakra.Json;

namespace Sample.Json
{
   class Program
   {
      static void Main(string[] _args)
      {
         using (var runtime = JavaScriptRuntime.Create())
         using (new JavaScriptContext.Scope(runtime.CreateContext()))
         {
            // Register Global Function
            JavaScriptValue
               .GlobalObject
               .SetProperty(JavaScriptPropertyId.FromString("dump"), // function name
               JavaScriptValue.CreateFunction((_callee, _call, _arguments, _count, _data) =>
                  {
                     Console.WriteLine("-- dump --");
                     Console.WriteLine(_arguments[1].ToJToken().ToString(Formatting.Indented));
                     return JObject.Parse("{status:'ok',error:-1}").ToJavaScriptValue();
                  },
                  IntPtr.Zero),
               true);

            Console.WriteLine("-- executing --");
            var res = JavaScriptContext.RunScript("dump({id:4,name:'chakra'});", JavaScriptSourceContext.FromIntPtr(IntPtr.Zero), "");

            Console.WriteLine("-- result --");
            Console.WriteLine(res.ToJToken().ToString(Formatting.Indented));
         }
         Console.WriteLine("Finished... Press enter to exit...");
         Console.ReadLine();
      }
   }
}
