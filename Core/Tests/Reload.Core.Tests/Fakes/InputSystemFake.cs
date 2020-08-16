using Reload.Core.Input;

namespace Reload.Core.Tests.Fakes
{
    internal class InputSystemFake : IInputSystem
    {
        public InputSourceType Source { get; init; } = InputSourceType.None;

        public void Initialize()
        { }

        public void ShutDown()
        { }
    }
}
