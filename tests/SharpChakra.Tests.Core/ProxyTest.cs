using System.Threading.Tasks;
using SharpChakra.Extensions;
using Xunit;

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
        public async Task TestJsProxy()
        {
            using (var runtime = JsRuntime.Create())
            {
                var context = runtime.CreateContext();
                var instance = new TestClass();

                await context.SetGlobalAsync("item", instance);

                await context.EvalAsync("item.call()");
                Assert.True(instance.IsCalled);

                await context.EvalAsync("item.isCalled = false");
                Assert.False(instance.IsCalled);
            }
        }
    }
}
