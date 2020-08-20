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
        private static readonly AL api = AL.GetApi();

        public AudioAPIType Type => AudioAPIType.OpenAL;

        /// <summary>
        /// Checks the for OpenAL error codes and throw corresponding exceptions.
        /// </summary>
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

        /// <summary>
        /// Checks whether an extension is present on the current api.
        /// </summary>
        /// <param name="extension">The extension to be checked.</param>
        /// <returns>A bool.</returns>
        public static bool IsExtensionPresent(string extension)
        {
            return api.IsExtensionPresent(extension);
        }

        #region Generators

        /// <summary>
        /// Generates audio buffer.
        /// </summary>
        /// <returns>An audio buffer handle as uint.</returns>
        public static uint GenerateBuffer()
        {
            var buffer = api.GenBuffer();
            CheckForErrors();

            return buffer;
        }

        /// <summary>
        /// Deletes the passed audio buffer.
        /// </summary>
        /// <param name="buffer">The audio buffer handle.</param>
        public static void DeleteBuffer(uint buffer)
        {
            api.DeleteBuffer(buffer);
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
            api.BufferData(buffer, bufferFormat, data, sampleRate);
            CheckForErrors();
        }

        /// <summary>
        /// Generates an audio source.
        /// </summary>
        /// <returns>An audio source handle as an uint.</returns>
        public static uint GenerateSource()
        {
            var source = api.GenSource();
            CheckForErrors();

            return source;
        }

        /// <summary>
        /// Deletes the source.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void DeleteSource(uint source)
        {
            api.DeleteSource(source);
            CheckForErrors();
        }

        #endregion

        #region Source setup

        public static void SetDistanceModel(DistanceModel model)
        {
            api.DistanceModel(model);
            CheckForErrors();
        }

        #endregion

        #region Get source properties

        public static int GetSourceProperty(uint source, GetSourceInteger param)
        {
            api.GetSourceProperty(source, param, out int value);
            CheckForErrors();

            return value;
        }

        public  static float GetSourceProperty(uint source, SourceFloat param)
        {
            api.GetSourceProperty(source, param, out float value);
            CheckForErrors();

            return value;
        }

        public static bool GetSourceProperty(uint source, SourceBoolean param)
        {
            api.GetSourceProperty(source, param, out bool value);
            CheckForErrors();

            return value;
        }

        public static Vector3 GetSourceProperty(uint source, SourceVector3 param)
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

        public override void Initialize()
        {}

        public override void ShutDown()
        { }
    }
}
