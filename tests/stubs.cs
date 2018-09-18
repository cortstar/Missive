namespace Missive_CSharp.tests
{
    class StubMissive : Missive
    {
        public override string ToString()
        {
            return string.Empty;
        }
    }

    class OtherStubMissive : Missive
    {
        public override string ToString()
        {
            return string.Empty;
        }
    }

    class StubListener : IMissiveListener<StubMissive>
    {
        public int messagesRecieved { get; private set; }
        public void HandleMissive(StubMissive missive)
        {
            messagesRecieved++;
        }
    }
    
    class OtherStubListener : IMissiveListener<OtherStubMissive>
    {
        public int messagesRecieved { get; private set; }
        public void HandleMissive(OtherStubMissive missive)
        {
            messagesRecieved++;
        }
    }

    class StubGlobalListener : IMissiveListener<Missive>
    {
        public int messagesRecieved { get; private set; }
        public void HandleMissive(Missive missive)
        {
            messagesRecieved++;
        }
    }
    
}