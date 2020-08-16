using System.ComponentModel;

namespace Reload.Core.Audio.Buffers
{
    public enum AudioBufferFormat
    {
        [Description("1 Channel, 8 bits per sample.")]  
        Mono8,
        
        [Description("1 Channel, 16 bits per sample.")]
        Mono16,

        [Description("2 Channels, 8 bits per sample each.")]
        Stereo8,
        
        [Description("2 Channels, 16 bits per sample each.")] 
        Stereo16
    }
}
