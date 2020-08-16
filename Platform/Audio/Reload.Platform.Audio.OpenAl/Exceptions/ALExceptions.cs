using System;

namespace Reload.Platform.Audio.OpenAl.Exceptions
{
    public class OpenAlInvalidNameException : ArgumentException
    {
        public OpenAlInvalidNameException()
            : base("A bad name (ID) was passed to an OpenAL function.")
        { }
    }

    public class OpenAlInvalidEnumException : ArgumentException
    {
        public OpenAlInvalidEnumException()
            : base("An invalid enum value was passed to an OpenAL function.")
        { }
    }

    public class OpenAlInvalidValueException : ArgumentException
    {
        public OpenAlInvalidValueException()
            : base("An invalid value was passed to an OpenAL function.")
        { }
    }

    public class OpenAlInvalidOperationException : InvalidOperationException
    {
        public OpenAlInvalidOperationException()
            : base("The requested operation is not valid.")
        { }
    }

    public class OpenAlOutOfMemoryException : OutOfMemoryException
    {
        public OpenAlOutOfMemoryException()
            : base("The requested operation resulted in OpenAL running out of memory.")
        { }
    }

    public class OpenAlUnknownException : Exception
    {
        public OpenAlUnknownException()
            : base("Unknown Al Error.")
        { }
    }
}
