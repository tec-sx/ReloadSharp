namespace Reload.Audio
{
    using System;

    public class AudioInitializationException : Exception
    {
        internal AudioInitializationException()
            : base("Initialization of the audio engine failed. This may be due to missing audio hardware or missing connected audio outputs.")
        { }
    }

    public class NoMicrophoneConnectedException : Exception
    {
        internal NoMicrophoneConnectedException()
            : base("No microphone is currently connected.")
        { }
    }

    public class AudioDeviceInvalidatedException : Exception
    {
        internal AudioDeviceInvalidatedException()
            : base("The audio device became unusable through being unplugged or some other event.")
        { }
    }

    public class LoadAudioFileException : Exception
    {
        internal LoadAudioFileException(string file)
            : base($"Error loading sound file: {file}.")
        { }
    }

}
