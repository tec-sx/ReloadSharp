namespace Core.Config
{
    using Models;

    public interface IConfiguration
    {
        void SaveSettings();
        ApplicationSettings GetSettings();
    }
}