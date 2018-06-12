using System;
using System.Collections.Generic;
using System.Text;
using SharpChakra.Extensions;
using Xunit;
using Xunit.Sdk;

namespace SharpChakra.Tests.Core
{
    public class ProxyTest
    {
        private class TestClass
        {
            public bool IsCalled { get; set; }

            public void Call()
            {
                IsCalled = true;
            }
        }

        [Fact]
        public void TestJsProxy()
        {
            using (var runtime = JsRuntime.Create())
            {
                var context = runtime.CreateContext();

                var instance = new TestClass();
                var proxy = context.CreateProxy(instance);

                context.Global.SetProperty("item", proxy);

                context.RunScript("item.Call();");
                Assert.True(instance.IsCalled);

                context.RunScript("item.IsCalled = false;");
                Assert.False(instance.IsCalled);
            }
        }
    }
}
