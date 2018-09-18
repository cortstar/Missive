
using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework.Constraints;

namespace Missive_CSharp.tests
{
    using NUnit.Framework;

    [TestFixture]
    public class Tests
    {
        [TestCase(0)]
        [TestCase(1)]
        public void Single_Listener_Receives_Correct_Messages(int sent_messages)
        {
            var listener = new StubListener();
            var aggregator = new MissiveAggregator();
            
            aggregator.Register(listener);

            for (var i = 0; i < sent_messages; i++)
            {
                aggregator.Publish(new StubMissive());
            }
            
            Assert.AreEqual(listener.messagesRecieved, sent_messages);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        public void Single_Listener_Does_Not_Receive_Incorrect_Messages(int sent_messages)
        {
            var listener = new StubListener();
            var aggregator = new MissiveAggregator();
            
            aggregator.Register(listener);
            
            for (var i = 0; i < sent_messages; i++)
            {
                aggregator.Publish(new OtherStubMissive());
            }
            
            Assert.AreEqual(listener.messagesRecieved, 0);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        public void Multiple_Listeners_Receive_Correct_Messages(int sent_messages)
        {
            var listener = new StubListener();
            var other_listener = new OtherStubListener();
            var aggregator = new MissiveAggregator();
            
            aggregator.Register(listener);
            aggregator.Register(other_listener);

            for (var i = 0; i < sent_messages; i++)
            {
                aggregator.Publish(new StubMissive());
            }
            
            Assert.AreEqual(listener.messagesRecieved, sent_messages);
        }
        
        [TestCase(0)]
        [TestCase(1)]
        public void Multiple_Listeners_Do_Not_Receive_Incorrect_Messages(int sent_messages)
        {
            var listener = new StubListener();
            var other_listener = new OtherStubListener();
            var aggregator = new MissiveAggregator();
            
            aggregator.Register(listener);
            aggregator.Register(other_listener);

            for (var i = 0; i < sent_messages; i++)
            {
                aggregator.Publish(new StubMissive());
            }
            
            Assert.AreEqual(other_listener.messagesRecieved, 0);
        }

        [Test]
        public void Global_Listener_Receives_All_Missives()
        {
            var listener = new StubGlobalListener();
            var aggregator = new MissiveAggregator();
            
            aggregator.RegisterGlobalListener(listener);
            
            aggregator.Publish(new StubMissive());
            aggregator.Publish(new OtherStubMissive());
            
            Assert.AreEqual(listener.messagesRecieved, 2);
        }

        [Test]
        public void Listener_Can_Unsubscribe()
        {
            var listener = new StubListener();
            var aggregator = new MissiveAggregator();
            
            aggregator.Register(listener);
            aggregator.Publish(new StubMissive());
            Assert.AreEqual(listener.messagesRecieved, 1);
            
            aggregator.Unregister(listener);
            aggregator.Publish(new StubMissive());
            Assert.AreEqual(listener.messagesRecieved, 1);
        }
    }
    }
    
    