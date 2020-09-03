#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
namespace Reload.Audio.Exceptions
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
