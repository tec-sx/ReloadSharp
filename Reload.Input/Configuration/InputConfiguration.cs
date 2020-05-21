namespace Reload.Input.Configuration
{
    using Silk.NET.Input.Common;

    public struct InputConfiguration
    {
        public int KeyboardId;
        public Key Up;
        public Key Down;
        public Key Left;
        public Key Right;
        public Key Run;
        public Key Duck;
        public Key Jump;
        public Key OpenInventory;
        public Key ToggleFightMode;
        public Key Select;
        public Key Pause;

        public int MouseId;
        public MouseButton Interact;
    }
}
