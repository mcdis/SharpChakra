using System;
using SharpChakra;
using SharpChakra.Extensions;

namespace Sample.Modules
{
   class Program
   {
      static void Main()
      {
         using (var jsrt = JsRuntime.Create(JsRuntimeAttributes.EnableExperimentalFeatures, JsRuntimeVersion.VersionEdge))
         using (jsrt.CreateContext().Scope())
         {
            var fn = new JsNativeFunctionBuilder();

            JsValue // Register Global Function
               .GetGlobalObject()
               .SetProperty("echo", // echo
                  fn.New(x => Console.WriteLine(x.Arguments[1].ToString())),
                  true);

            var rootModule =
               JsModuleRecord.Create(JsModuleRecord.Root, JsValue.FromString("")); // Declare Root Module               
            var fooModule = JsModuleRecord.Invalid;

            JsErrorCode OnFetch(JsModuleRecord module, JsValue specifier,
               out JsModuleRecord record) // fetch callback
            {
               Console.WriteLine($"importing '{specifier.ToString()}'...");

               fooModule = JsModuleRecord.Create(module, specifier); // Create Foo Module

               Console.WriteLine($"imported '{specifier.ToString()}'...");
               record = fooModule;
               return JsErrorCode.NoError;
            }

            rootModule.SetHostInfo(OnFetch);
            var rootSrc =
               @"
                  import {test} from 'foo.js';
                  echo(test());
               ";
            rootModule.Parse(rootSrc);
            fooModule.Parse("export let test = function(){return 'hello';}");

            rootModule.Eval();
         }
         Console.WriteLine("Finished... Press enter to exit...");
         Console.ReadLine();
      }
   }
}