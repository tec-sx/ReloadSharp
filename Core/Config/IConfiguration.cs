namespace Core.Config
{
    public interface IConfiguration
    {
        ApplicationSettings Settings { get; }
        ContentPath ContentPath { get; }
        void SaveSettings();
    }
}