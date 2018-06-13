using System;

namespace Sample.Extensions
{
    internal struct ComplexType
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    internal class TestProxy
    {
        private int _pCallCount;
        public string Name => $"TestProxy Object [{_pCallCount++}]";
        public void EchoHello() => Console.WriteLine("Hello");
        public void Echo(string s) => Console.WriteLine(s);
        public ComplexType ConstComplex { get; } = new ComplexType {X = 1, Y = 5};
        public void EchoComplex(ComplexType x) => Console.WriteLine($"X:{x.X}, Y:{x.Y}");

        public string TestArgs(int a, string s, double d)
        {
            var t = $"int:{a}, string: {s}, double: {d}";
            Console.WriteLine(t);
            return t;
        }
    }
}