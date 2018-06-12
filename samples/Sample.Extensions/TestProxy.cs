using System;
using System.Threading;

namespace Sample.Extensions
{
   struct ComplexType
   {
      public int X { get; set; }
      public int Y { get; set; }
   }
   class TestProxy
   {
      private int p_callCount;
      public string Name => $"TestProxy Object [{p_callCount++}]";
      public void EchoHello() => Console.WriteLine("Hello");
      public void Echo(string _s) => Console.WriteLine(_s);
      public ComplexType ConstComplex { get; } = new ComplexType { X = 1, Y = 5 };
      public void EchoComplex(ComplexType _x) => Console.WriteLine($"X:{_x.X}, Y:{_x.Y}");
      public string TestArgs(int _a, string _s, double _d)
      {
         var t = $"int:{_a}, string: {_s}, double: {_d}";
         Console.WriteLine(t);
         return t;
      }
   }
}