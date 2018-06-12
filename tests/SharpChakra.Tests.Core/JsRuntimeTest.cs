using System;
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
            using (runtime.CreateContext().Scope())
            {
                var called = false;
                var fn = new JsNativeFunctionBuilder();
                JsValue.GetGlobalObject()
                    .SetProperty("run",
                        fn.New(() => called = true),
                        true);

                JsContext.RunScript("run();");

                Assert.True(called, "the run function was not callded.");
            }
        }
    }
}
