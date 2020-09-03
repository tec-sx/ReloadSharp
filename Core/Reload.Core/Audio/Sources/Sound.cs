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
using System.Numerics;

namespace Reload.Core.Audio
{
    public class Sound
    {
        private readonly AudioSource _source;

        public float Gain
        {
            get => _source.Gain;
            set => _source.Gain = value;
        }

        public float Pitch
        {
            get => _source.Pitch;
            set => _source.Pitch = value;
        }

        public bool Looping => _source.Looping;

        public Vector3 Position
        {
            get => _source.Position;
            set => _source.Position = value;
        }
        public Vector3 Direction
        {
            get => _source.Direction;
            set => _source.Direction = value;
        }

        public Vector3 Velocity
        {
            get => _source.Velocity;
            set => _source.Velocity = value;
        }

        public Sound(AudioSource source)
        {
            _source = source;
        }

        public void Play(bool loop = false) => _source.Play(loop);
        public void Stop() => _source.Stop();
    }
}