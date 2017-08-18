using System;
using System.Text;
using SharpChakra;

namespace Sample.Modules
{
   class Program
   {
      static void Main(string[] args)
      {
         try
         {
            using (var runtime = JsRuntime.Create(JsRuntimeAttributes.EnableExperimentalFeatures,JsRuntimeVersion.VersionEdge))
            using (var ctx = new JsContext.Scope(runtime.CreateContext()))
            {
               // Register Global Function
               JsValue
                  .GlobalObject.SetProperty(JsPropertyId.FromString("echo"), // function name
                  JsValue.CreateFunction((_callee, _call, _arguments, _count, _data) =>
                     {
                        Console.WriteLine(_arguments[1].ToString());
                        return JsValue.Undefined;
                     },
                     IntPtr.Zero),
                  true);

               // Declare Root Module               
               var rootModule = JsModuleRecord.Create(JsModuleRecord.Root,JsValue.FromString(""));
               var fooModule = JsModuleRecord.Invalid;
               JsErrorCode onFetch(JsModuleRecord _module, JsValue _specifier, out JsModuleRecord _record)
               {
                  Console.WriteLine($"importing '{_specifier.ToString()}'...");

                  // Create Foo Module
                  fooModule = JsModuleRecord.Create(_module, _specifier);                  

                  Console.WriteLine($"imported '{_specifier.ToString()}'...");
                  _record = fooModule;
                  return JsErrorCode.NoError;
               }               
               JsErrorCode onReady(JsModuleRecord _referencingmodule, ref JsValue _exception)
               {
                  int a = 0;
                  return JsErrorCode.NoError;
               }

               rootModule.SetHostInfo(onFetch);
               rootModule.SetHostInfo(onReady);
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
         catch (Exception e)
         {
            int a = 0;
         }
      }
   }
}
