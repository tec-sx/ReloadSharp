namespace Reload.Input.Source
{
    using Reload.Core;

    public interface IDeviceControllable
    {
        public void RegisterCommand<TControl>(TControl control, Command command);
    }
}
