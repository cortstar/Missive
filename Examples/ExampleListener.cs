using System;

namespace Missive_CSharp.Examples
{
    /// <summary>
    /// An example Missive listener that hears ExampleMissive
    /// </summary>
    public class ExampleListener : IMissiveListener<ExampleMissive>
    {
        private int _received_count = 0;
        
        public void HandleMissive(ExampleMissive missive)
        {
            _received_count++;
            Console.WriteLine("Example listener received a message. Count {0} received in total.", _received_count);
        }
    }
}