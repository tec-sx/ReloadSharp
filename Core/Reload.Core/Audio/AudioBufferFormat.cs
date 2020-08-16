using System.ComponentModel;

namespace Reload.Core.Audio
{
    public enum BufferFormat
    {
        [Description("1 Channel, 8 bits per sample.")]  
        Mono8 = 4352,
        
        [Description("1 Channel, 16 bits per sample.")]
        Mono16 = 4353,

        [Description("2 Channels, 8 bits per sample each.")]
        Stereo8 = 4354,
        
        [Description("2 Channels, 16 bits per sample each.")] 
        Stereo16 = 4355
    }
}
