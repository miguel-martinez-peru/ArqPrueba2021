using System;

namespace AuthZ.BackgroundTask.Exceptions
{
    public class BackgroundTaskException : Exception
    {
        public BackgroundTaskException()
        { }

        public BackgroundTaskException(string message)
            : base(message)
        { }

        public BackgroundTaskException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
