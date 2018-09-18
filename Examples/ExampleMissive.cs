using System.Runtime.Remoting.Messaging;

namespace Missive_CSharp.Examples
{
    public class ExampleMissive : Missive
    {
        public readonly string Message;
        
        public ExampleMissive(string message)
        {
            this.Message = message;
        }

        public override string ToString()
        {
            return Message;
        }
    }
}