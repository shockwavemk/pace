using System;

// Remote object.
namespace PaceServer
{
    public class RemoteObject : MarshalByRefObject
    {
        private int callCount = 0;

        public int GetCount()
        {
            Console.WriteLine("GetCount was called.");
            callCount++;
            return (callCount);
        }

    }
}
