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
         var builder = new JsNativeFunctionBuilder();
         using (var runtime = JsRuntime.Create(JsRuntimeAttributes.EnableExperimentalFeatures, JsRuntimeVersion.VersionEdge))
         using (runtime.CreateContext().Scope())
         {
            var jsCtx = JsValue.CreateObject();
            JsValue.GlobalObject
               .SetProperty("ctx", jsCtx, true)
               .SetProperty("proxy", JsProxy.New(new TestProxy(), builder), true);
            JsContext.RunScript(@"
               ctx['a'] = 4;
               ctx['b'] = 'hello world';
               ctx['c'] = [1,2,3,4,5];
               ctx['d'] = proxy.Name;
               ctx['e'] = proxy.Name;
               ctx['f'] = proxy.Name;
               ctx['g'] = proxy.Name;
            ");
            Console.WriteLine("-- result --");
            Console.WriteLine($"added properties: {string.Join(",", jsCtx.EnumeratePropertyNames())}");
            Console.WriteLine($"ctx['c']: {string.Join(",", jsCtx.GetProperty("c").EnumerateArrayValues().Select(_ => _.ToInt32()))}");
            Console.WriteLine($"ctx['d']: {jsCtx.GetProperty("d").ConvertToString().ToString()}");
            Console.WriteLine($"ctx['e']: {jsCtx.GetProperty("e").ConvertToString().ToString()}");
            Console.WriteLine($"ctx['f']: {jsCtx.GetProperty("f").ConvertToString().ToString()}");
            Console.WriteLine($"ctx['g']: {jsCtx.GetProperty("g").ConvertToString().ToString()}");
         }
         Console.WriteLine("Finished... Press enter to exit...");
         Console.ReadLine();
      }
   }
}