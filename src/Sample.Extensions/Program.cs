using System;
using System.Linq;
using SharpChakra;
using SharpChakra.Extensions;

namespace Sample.Extensions
{
   class Program
   {
      static void Main()
      {
         using (var runtime = JsRuntime.Create())
         using (new JsContext.Scope(runtime.CreateContext()))
         {
            var jsCtx = JsValue.CreateObject();
            JsValue.GlobalObject.SetProperty("ctx", jsCtx, true);

            JsContext.RunScript(@"
               ctx['a'] = 4;
               ctx['b'] = 'hello world';
               ctx['c'] = [1,2,3,4,5];
            ");
            Console.WriteLine("-- result --");
            Console.WriteLine($"added properties: {string.Join(",", jsCtx.EnumeratePropertyNames())}");
            Console.WriteLine($"ctx['c']: {string.Join(",", jsCtx.GetProperty("c").EnumerateArrayValues().Select(_ => _.ToInt32()))}");
         }
         Console.WriteLine("Finished... Press enter to exit...");
         Console.ReadLine();
      }
   }
}