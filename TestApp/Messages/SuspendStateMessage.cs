using System;
using Windows.ApplicationModel;

namespace TestApp.Messages
{
    public class SuspendStateMessage
    {
        public SuspendStateMessage(SuspendingOperation operation)
        {
            Operation = operation;
        }

        public SuspendingOperation Operation { get; }
    }
}
