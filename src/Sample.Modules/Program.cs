using System;
using SharpChakra;
using SharpChakra.Extensions;

namespace Sample.Modules
{
   class Program
   {
      static void Main()
      {
         using (var runtime = JsRuntime.Create(JsRuntimeAttributes.EnableExperimentalFeatures, JsRuntimeVersion.VersionEdge))
         using (new JsContext.Scope(runtime.CreateContext()))
         {
            var fn = new JsNativeFunctionBuilder();

            JsValue // Register Global Function
               .GlobalObject
               .SetProperty("echo", // echo
                  fn.New(_x => Console.WriteLine(_x.Arguments[1].ToString())),
                  true);

            var rootModule = JsModuleRecord.Create(JsModuleRecord.Root, JsValue.FromString("")); // Declare Root Module               
            var fooModule = JsModuleRecord.Invalid;

            JsErrorCode onFetch(JsModuleRecord _module, JsValue _specifier, out JsModuleRecord _record) // fetch callback
            {
               Console.WriteLine($"importing '{_specifier.ToString()}'...");

               fooModule = JsModuleRecord.Create(_module, _specifier); // Create Foo Module

               Console.WriteLine($"imported '{_specifier.ToString()}'...");
               _record = fooModule;
               return JsErrorCode.NoError;
            }

            rootModule.SetHostInfo(onFetch);
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