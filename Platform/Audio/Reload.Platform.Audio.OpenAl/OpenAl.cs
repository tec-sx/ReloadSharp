using Reload.Core.Audio;
using Reload.Platform.Audio.OpenAl.Exceptions;
using Silk.NET.OpenAL;
using System.Numerics;

namespace Reload.Platform.Audio.OpenAl
{
    /// <summary>
    /// The OpenAL audio backend.
    /// </summary>
    public class OpenAl : AudioAPI
    {
        private static AL _api;

        private bool _disposed;

        public AudioAPIType Type => AudioAPIType.OpenAL;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenAl"/> class.
        /// </summary>
        public OpenAl()
        { }

        /// <summary>
        /// Checks the for OpenAL error codes and throw corresponding exceptions.
        /// </summary>
        private static void CheckForErrors()
        {
            switch (_api.GetError())
            {
                case AudioError.NoError: break;
                case AudioError.InvalidValue: throw new OpenAlInvalidValueException();
                case AudioError.InvalidName: throw new OpenAlInvalidNameException();
                case AudioError.InvalidEnum: throw new OpenAlInvalidEnumException();
                case AudioError.InvalidOperation: throw new OpenAlInvalidOperationException();
                case AudioError.OutOfMemory: throw new OpenAlOutOfMemoryException();
                default: throw new OpenAlUnknownException();
            };
        }

        /// <summary>
        /// Checks whether an extension is present on the current api.
        /// </summary>
        /// <param name="extension">The extension to be checked.</param>
        /// <returns>A bool.</returns>
        public static bool IsExtensionPresent(string extension)
        {
            return _api.IsExtensionPresent(extension);
        }

        #region Generators

        /// <summary>
        /// Generates audio buffer.
        /// </summary>
        /// <returns>An audio buffer handle as uint.</returns>
        public static uint GenerateBuffer()
        {
            var buffer = _api.GenBuffer();
            CheckForErrors();

            return buffer;
        }

        /// <summary>
        /// Deletes the passed audio buffer.
        /// </summary>
        /// <param name="buffer">The audio buffer handle.</param>
        public static void DeleteBuffer(uint buffer)
        {
            _api.DeleteBuffer(buffer);
            CheckForErrors();
        }

        /// <summary>
        /// Sets the data to the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer to set the data to.</param>
        /// <param name="bufferFormat">The buffer format.</param>
        /// <param name="data">The data to be set.</param>
        /// <param name="sampleRate">The sample rate of the data.</param>
        public static void BufferData<T>(uint buffer, BufferFormat bufferFormat, T[] data, int sampleRate)
            where T : unmanaged
        {
            _api.BufferData(buffer, bufferFormat, data, sampleRate);
            CheckForErrors();
        }

        /// <summary>
        /// Generates an audio source.
        /// </summary>
        /// <returns>An audio source handle as an uint.</returns>
        public static uint GenerateSource()
        {
            var source = _api.GenSource();
            CheckForErrors();

            return source;
        }

        /// <summary>
        /// Deletes the source.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void DeleteSource(uint source)
        {
            _api.DeleteSource(source);
            CheckForErrors();
        }

        #endregion

        #region Source setup

        public static void SetDistanceModel(DistanceModel model)
        {
            _api.DistanceModel(model);
            CheckForErrors();
        }

        #endregion

        #region Get source properties

        public static int GetSourceProperty(uint source, GetSourceInteger param)
        {
            _api.GetSourceProperty(source, param, out int value);
            CheckForErrors();

            return value;
        }

        public  static float GetSourceProperty(uint source, SourceFloat param)
        {
            _api.GetSourceProperty(source, param, out float value);
            CheckForErrors();

            return value;
        }

        public static bool GetSourceProperty(uint source, SourceBoolean param)
        {
            _api.GetSourceProperty(source, param, out bool value);
            CheckForErrors();

            return value;
        }

        public static Vector3 GetSourceProperty(uint source, SourceVector3 param)
        {
            _api.GetSourceProperty(source, param, out Vector3 value);
            CheckForErrors();

            return value;
        }

        #endregion

        #region Set source properties

        public static void SetSourceProperty(uint source, SourceInteger param, int value)
        {
            _api.SetSourceProperty(source, param, value);
            CheckForErrors();
        }

        public static void SetSourceProperty(uint source, SourceFloat param, float value)
        {
            _api.SetSourceProperty(source, param, value);
            CheckForErrors();
        }

        public static void SetSourceProperty(uint source, SourceBoolean param, bool value)
        {
            _api.SetSourceProperty(source, param, value);
            CheckForErrors();
        }

        public static void SetSourceProperty(uint source, SourceVector3 param, Vector3 value)
        {
            _api.SetSourceProperty(source, param, value);
            CheckForErrors();
        }

        #endregion

        #region Get listener properties

        public static int GetListenerProperty(ListenerInteger param)
        {
            _api.GetListenerProperty(param, out int value);
            CheckForErrors();

            return value;
        }

        public static float GetListenerProperty(ListenerFloat param)
        {
            _api.GetListenerProperty(param, out float value);
            CheckForErrors();

            return value;
        }

        public static Vector3 GetListenerProperty(ListenerVector3 param)
        {
            _api.GetListenerProperty(param, out Vector3 value);
            CheckForErrors();

            return value;
        }

        #endregion

        #region Set listner properties

        public static void SetListenerProperty(ListenerInteger param, int value)
        {
            _api.SetListenerProperty(param, value);
            CheckForErrors();
        }

        public static void ListenerProperty(ListenerFloat param, float value)
        {
            _api.SetListenerProperty(param, value);
            CheckForErrors();
        }

        public static void SetListenerProperty(ListenerVector3 param, Vector3 value)
        {
            _api.SetListenerProperty(param, value);
            CheckForErrors();
        }

        #endregion

        public static void SourceQueueBuffers(uint source, uint[] buffers)
        {
            _api.SourceQueueBuffers(source, buffers);
            CheckForErrors();
        }

        public static void SourceUnqueueBuffers(uint source, uint[] buffers)
        {
            _api.SourceUnqueueBuffers(source, buffers);
            CheckForErrors();
        }

        public static void SourcePlay(uint source)
        {
            _api.SourcePlay(source);
            CheckForErrors();
        }

        public static void SourceStop(uint source)
        {
            _api.SourceStop(source);
            CheckForErrors();
        }

        /// <inheritdoc/>
        public override void Configure()
        {
            _api = AL.GetApi();
        }

        /// <inheritdoc/>
        public override void StartUp()
        {}

        /// <inheritdoc/>
        public override void ShutDown()
        { }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _api.Dispose();
            }

            _disposed = true;
        }
    }
}
