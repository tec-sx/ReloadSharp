namespace Core.Config
{
    using Models;

    public interface IConfiguration
    {
        ApplicationSettings Settings { get; }
        ContentPath ContentPath { get; }
        void SaveSettings();
    }
}