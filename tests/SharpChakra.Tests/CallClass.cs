namespace SharpChakra.Tests.Core
{
    public class CallClass
    {
        public bool IsCalled { get; set; }

        public void Call()
        {
            IsCalled = true;
        }
    }
}
