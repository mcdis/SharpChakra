using System.Threading;

namespace Sample.Extensions
{
   class TestProxy
   {
      private int p_callCount;
      public string Name => $"TestProxy Object [{p_callCount++}]";
   }
}