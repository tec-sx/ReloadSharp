using Reload.Core.Audio;
using Reload.Platform.Audio.OpenAl.Exceptions;
using Silk.NET.OpenAL;
using System.Numerics;

namespace Reload.Platform.Audio.OpenAl
{
    /// <summary>
    /// The OpenAL audio backend.
    /// </summary>
    public class OpenAlBackend : IAudioBackend
    {
        /// <inheritdoc/>
        public AudioBackendType Type => AudioBackendType.OpenAL;

        /// <inheritdoc/>
        public void Initialize()
        {
        }

        /// <inheritdoc/>
        public void ShutDown()
        {
        }

        private static readonly AL api = AL.GetApi();

        private static void CheckForErrors()
        {
            switch (api.GetError())
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

        public bool IsExtensionPresent(string ext)
        {
            return api.IsExtensionPresent(ext);
        }

        #region Generators

        public uint GenerateBuffer()
        {
            var buffer = api.GenBuffer();
            CheckForErrors();

            return buffer;
        }

        public void DeleteBuffer(uint buffer)
        {
            api.DeleteBuffer(buffer);
            CheckForErrors();
        }

        public void BufferData<T>(uint buffer, BufferFormat bufferFormat, T[] data, int sampleRate)
            where T : unmanaged
        {
            api.BufferData(buffer, bufferFormat, data, sampleRate);
            CheckForErrors();
        }

        public uint GenerateSource()
        {
            var source = api.GenSource();
            CheckForErrors();

            return source;
        }

        public void DeleteSource(uint source)
        {
            api.DeleteSource(source);
            CheckForErrors();
        }

        #endregion

        #region Source setup

        public void SetDistanceModel(DistanceModel model)
        {
            api.DistanceModel(model);
            CheckForErrors();
        }

        #endregion

        #region Get source properties

        public int GetSourceProperty(uint source, GetSourceInteger param)
        {
            api.GetSourceProperty(source, param, out int value);
            CheckForErrors();

            return value;
        }

        public float GetSourceProperty(uint source, SourceFloat param)
        {
            api.GetSourceProperty(source, param, out float value);
            CheckForErrors();

            return value;
        }

        public bool GetSourceProperty(uint source, SourceBoolean param)
        {
            api.GetSourceProperty(source, param, out bool value);
            CheckForErrors();

            return value;
        }

        public Vector3 GetSourceProperty(uint source, SourceVector3 param)
        {
            api.GetSourceProperty(source, param, out Vector3 value);
            CheckForErrors();

            return value;
        }

        #endregion

        #region Set source properties

        public static void SetSourceProperty(uint source, SourceInteger param, int value)
        {
            api.SetSourceProperty(source, param, value);
            CheckForErrors();
        }

        public static void SetSourceProperty(uint source, SourceFloat param, float value)
        {
            api.SetSourceProperty(source, param, value);
            CheckForErrors();
        }

        public static void SetSourceProperty(uint source, SourceBoolean param, bool value)
        {
            api.SetSourceProperty(source, param, value);
            CheckForErrors();
        }

        public static void SetSourceProperty(uint source, SourceVector3 param, Vector3 value)
        {
            api.SetSourceProperty(source, param, value);
            CheckForErrors();
        }

        #endregion

        #region Get listener properties

        public static int GetListenerProperty(ListenerInteger param)
        {
            api.GetListenerProperty(param, out int value);
            CheckForErrors();

            return value;
        }

        public static float GetListenerProperty(ListenerFloat param)
        {
            api.GetListenerProperty(param, out float value);
            CheckForErrors();

            return value;
        }

        public static Vector3 GetListenerProperty(ListenerVector3 param)
        {
            api.GetListenerProperty(param, out Vector3 value);
            CheckForErrors();

            return value;
        }

        #endregion

        #region Set listner properties

        public static void SetListenerProperty(ListenerInteger param, int value)
        {
            api.SetListenerProperty(param, value);
            CheckForErrors();
        }

        public static void ListenerProperty(ListenerFloat param, float value)
        {
            api.SetListenerProperty(param, value);
            CheckForErrors();
        }

        public static void SetListenerProperty(ListenerVector3 param, Vector3 value)
        {
            api.SetListenerProperty(param, value);
            CheckForErrors();
        }

        #endregion

        public static void SourceQueueBuffers(uint source, uint[] buffers)
        {
            api.SourceQueueBuffers(source, buffers);
            CheckForErrors();
        }

        public static void SourceUnqueueBuffers(uint source, uint[] buffers)
        {
            api.SourceUnqueueBuffers(source, buffers);
            CheckForErrors();
        }

        public static void SourcePlay(uint source)
        {
            api.SourcePlay(source);
            CheckForErrors();
        }

        public static void SourceStop(uint source)
        {
            api.SourceStop(source);
            CheckForErrors();
        }
    }
}
