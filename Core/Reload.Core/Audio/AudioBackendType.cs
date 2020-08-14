using System.ComponentModel;

namespace Reload.Core.Audio
{
    public enum AudioBackendType
    {
        [Description("No audio")]
        None = 0,

        [Description("OpenAL")]
        OpenAL,

        [Description("XAudio")]
        XAudio,

        [Description("Custom audio backend")]
        Custom
    }
}
