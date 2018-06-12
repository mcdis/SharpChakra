using System;
using System.Linq;
using System.Threading.Tasks;
using SharpChakra.Extensions;
using Xunit;

namespace SharpChakra.Tests.Core
{
    public class JsRuntimeTest
    {
        [Fact]
        public void TestFunction()
        {
            using (var runtime = JsRuntime.Create())
            {
                var context = runtime.CreateContext();
                var called = false;

                context.Global.SetProperty("run", context.CreateFunction(() => called = true), true);
                context.RunScript("run();");


                Assert.True(called, "the run function was not callded.");
            }
        }

        [Fact]
        public void TestContextSwitching()
        {
            using (var runtime = JsRuntime.Create())
            {
                var context1 = runtime.CreateContext();
                var context2 = runtime.CreateContext();
                var called = false;

                // Get the first global and create the function.
                var global1 = context1.Global;
                var func = context1.CreateFunction(() => called = true);

                // Ensure that context2 is selected.
                context2.EnsureCurrent();

                // Modify the global from context1 without switching.
                global1.SetProperty("run", func, true);

                // Run script at context1.
                context1.RunScript("run();");

                Assert.True(called, "the run function was not callded.");
            }
        }

        [Fact]
        public void TestJsValueContext()
        {
            using (var runtime = JsRuntime.Create())
            {
                var context = runtime.CreateContext();

                // Create a new string.
                var value = context.CreateString("test");
                Assert.Equal(context, value.Context);
            }
        }
    }
}