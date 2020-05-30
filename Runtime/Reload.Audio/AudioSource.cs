namespace Reload.Audio
{
    using Backend;
    using Codec;
    using Silk.NET.OpenAL;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Numerics;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class AudioSource : IDisposable
    {
        private readonly uint _source;
        private readonly Decoder _decoder;
        private readonly BufferChain _bufferChain;
        private readonly Stopwatch _timer;
        private byte[] _data;

        public float Gain
        {
            get => ALNative.GetSourceProperty(_source, SourceFloat.Gain);
            set
            {
                var normalValue = value > 1.0f ? 1.0f : value <= 0 ? 0.001f : value;
                ALNative.SetSourceProperty(_source, SourceFloat.Gain, normalValue);
            }
        }

        public bool IsPlaying =>
            (SourceState)ALNative.GetSourceProperty(_source, GetSourceInteger.SourceState) == SourceState.Playing;

        public bool Looping
        {
            get => ALNative.GetSourceProperty(_source, SourceBoolean.Looping);
            set => ALNative.SetSourceProperty(_source, SourceBoolean.Looping, value);
        }

        public float Pitch
        {
            get => ALNative.GetSourceProperty(_source, SourceFloat.Pitch);
            set => ALNative.SetSourceProperty(_source, SourceFloat.Pitch, value);
        }

        public Vector3 Position
        {
            get => ALNative.GetSourceProperty(_source, SourceVector3.Position);
            set => ALNative.SetSourceProperty(_source, SourceVector3.Position, value);
        }

        public Vector3 Direction
        {
            get => ALNative.GetSourceProperty(_source, SourceVector3.Direction);
            set => ALNative.SetSourceProperty(_source, SourceVector3.Direction, value);
        }

        public Vector3 Velocity
        {
            get => ALNative.GetSourceProperty(_source, SourceVector3.Velocity);
            set => ALNative.SetSourceProperty(_source, SourceVector3.Velocity, value);
        }

        public TimeSpan Duration => _decoder.Duration;
        public TimeSpan Elapsed => _timer.Elapsed;

        public AudioSource(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null!");
            }

            _source = ALNative.GenerateSource();
            _bufferChain = new BufferChain(_source);
            _decoder = DecoderFactory.CreateDecoder(stream);

            _data = _decoder.ReadSamples(TimeSpan.FromSeconds(1));
            _bufferChain.QueueData(_data, _decoder.Format);

            _timer = new Stopwatch();
        }

        public void Dispose()
        {
            _bufferChain.Dispose();
            ALNative.DeleteSource(_source);
        }

        public void Play(bool loop)
        {
            Looping = loop;
            ALNative.SourcePlay(_source);
            _timer.Start();

            Task.Run(() =>
            {
                while (IsPlaying)
                {
                    if (_bufferChain.BuffersQueued < 3 && !_decoder.IsFinished)
                    {
                        _data = _decoder.ReadSamples(TimeSpan.FromSeconds(1));
                        _bufferChain.QueueData(_data, _decoder.Format);
                    }

                    Thread.Sleep(100);
                }

                _timer.Stop();
            });
        }

        public void Stop()
        {
            ALNative.SourceStop(_source);
            _timer?.Stop();
        }
    }
}
