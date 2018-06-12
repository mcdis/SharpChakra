using System;
using System.Collections.Generic;
using System.Text;
using SharpChakra.Extensions;
using Xunit;

namespace SharpChakra.Tests.Core
{
    public class FunctionTest
    {
        [Fact]
        public void TestFunction()
        {
            using (var runtime = JsRuntime.Create())
            {
                var context = runtime.CreateContext();
                var called = false;

                context.Global.SetProperty("run", context.CreateFunction(() => called = true));
                context.RunScript("run()");

                Assert.True(called, "the run function was not callded.");
            }
        }

        [Fact]
        public void TestFunctionWithReturn()
        {
            using (var runtime = JsRuntime.Create())
            {
                var context = runtime.CreateContext();

                context.Global.SetProperty("run", context.CreateFunction(() => context.CreateTrue()));
                var result = context.RunScript("run()");

                Assert.True(result.ToBoolean(), "the run function should return true.");
            }
        }

        [Fact]
        public void TestFunctionFromMethod()
        {
            using (var runtime = JsRuntime.Create())
            {
                var context = runtime.CreateContext();

                context.Global.SetProperty("getFoo", context.CreateFunctionFromMethod(this, nameof(ReturnFoo)));
                context.Global.SetProperty("getBar", context.CreateFunctionFromMethod<FunctionTest>(nameof(ReturnBar)));

                Assert.Equal("foo", context.RunScript("getFoo()").ToString());
                Assert.Equal("bar", context.RunScript("getBar()").ToString());
            }
        }

        public string ReturnFoo()
        {
            return "foo";
        }

        public static string ReturnBar()
        {
            return "bar";
        }
    }
}
