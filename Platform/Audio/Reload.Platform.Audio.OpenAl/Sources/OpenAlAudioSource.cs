using Reload.Core.Audio;
using Reload.Core.Audio.Buffers;
using Reload.Platform.Audio.OpenAl.Buffers;
using Reload.Platform.Audio.OpenAl.Codec;
using Silk.NET.OpenAL;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Reload.Platform.Audio.OpenAl
{
    /// <summary>
    /// An OpenAl implementation of the audio source.
    /// </summary>
    public class OpenAlAudioSource : AudioSource
    {
        private readonly uint _source;
        
        private readonly Decoder _decoder;
        
        private readonly BufferChain _bufferChain;
        
        private readonly Stopwatch _timer;
        
        private byte[] _data;

        private bool _isDisposed;

        /// <inheritdoc/>
        public override float Gain
        {
            get => OpenAl.GetSourceProperty(_source, SourceFloat.Gain);
            set
            {
                var normalValue = value > 1.0f ? 1.0f : value <= 0 ? 0.001f : value;
                OpenAl.SetSourceProperty(_source, SourceFloat.Gain, normalValue);
            }
        }

        /// <inheritdoc/>
        public override bool IsPlaying =>
            (SourceState)OpenAl.GetSourceProperty(_source, GetSourceInteger.SourceState) == SourceState.Playing;

        /// <inheritdoc/>
        public override bool Looping
        {
            get => OpenAl.GetSourceProperty(_source, SourceBoolean.Looping);
            set => OpenAl.SetSourceProperty(_source, SourceBoolean.Looping, value);
        }

        /// <inheritdoc/>
        public override float Pitch
        {
            get => OpenAl.GetSourceProperty(_source, SourceFloat.Pitch);
            set => OpenAl.SetSourceProperty(_source, SourceFloat.Pitch, value);
        }

        /// <inheritdoc/>
        public override Vector3 Position
        {
            get => OpenAl.GetSourceProperty(_source, SourceVector3.Position);
            set => OpenAl.SetSourceProperty(_source, SourceVector3.Position, value);
        }

        /// <inheritdoc/>
        public override Vector3 Direction
        {
            get => OpenAl.GetSourceProperty(_source, SourceVector3.Direction);
            set => OpenAl.SetSourceProperty(_source, SourceVector3.Direction, value);
        }

        /// <inheritdoc/>
        public override Vector3 Velocity
        {
            get => OpenAl.GetSourceProperty(_source, SourceVector3.Velocity);
            set => OpenAl.SetSourceProperty(_source, SourceVector3.Velocity, value);
        }

        /// <inheritdoc/>
        public override TimeSpan Duration => _decoder.Duration;

        /// <inheritdoc/>
        public override TimeSpan Elapsed => _timer.Elapsed;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenAlAudioSource"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public OpenAlAudioSource(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("Stream cannot be null!");
            }

            _source = OpenAl.GenerateSource();
            _bufferChain = new OpenAlBufferChain(_source);
            _decoder = DecoderFactory.CreateDecoder(stream);

            _data = _decoder.ReadSamples(TimeSpan.FromSeconds(1));
            _bufferChain.QueueData(_data, _decoder.Format);

            _timer = new Stopwatch();
        }

        /// <inheritdoc/>
        public override void Play(bool loop)
        {
            Looping = loop;
            OpenAl.SourcePlay(_source);
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

        /// <inheritdoc/>
        public override void Stop()
        {
            OpenAl.SourceStop(_source);
            _timer?.Stop();
        }


        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _bufferChain.Dispose();
            }

            OpenAl.DeleteSource(_source);

            _isDisposed = true;
        }

    }
}
