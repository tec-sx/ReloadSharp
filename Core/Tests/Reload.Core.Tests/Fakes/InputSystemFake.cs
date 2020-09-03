using Reload.Core.Input;

namespace Reload.Core.Tests.Fakes
{
    internal class InputSystemFake : IInputSystem
    {
        public InputSourceType Source => InputSourceType.None;

        public void StartUp()
        { }

        public void ShutDown()
        { }
    }
}
