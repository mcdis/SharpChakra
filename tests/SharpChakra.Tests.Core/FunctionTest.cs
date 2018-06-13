using System.Threading.Tasks;
using SharpChakra.Extensions;
using Xunit;

namespace SharpChakra.Tests.Core
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestFunction()
        {
            using (var runtime = JsRuntime.Create())
            {
                var called = false;
                var context = runtime.CreateContext();

                await context.DeclareFunctionAsync("run", () => called = true);
                await context.EvalAsync("run()");

                Assert.True(called, "the run function was not callded.");
            }
        }

        [Fact]
        public async Task TestFunctionReturn()
        {
            const string expected = "foobar";

            using (var runtime = JsRuntime.Create())
            {
                var context = runtime.CreateContext();

                await context.DeclareFunctionAsync("run", () => expected);
                var result = await context.EvalAsync<string>("run()");

                Assert.Equal(expected, result);
            }
        }
    }
}
