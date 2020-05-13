namespace Engine.GamePlay
{
    using System;

    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class PlayerAction
    {
        public event Action<MoveDirection> Move;
        public event Action Jump;
        public event Action Run;
        public event Action Duck;
        public event Action OpenInventory;
        public event Action Select;
        public event Action ToggleFightMode;
        public event Action Pause;

        public event Action Interact;

        //private readonly KeyInput _keyInput;
        //private readonly MouseInput _mouseInput;

        //public PlayerAction(InputManager inputManager)
        //{
        //    _keyInput = Configuration.Settings.KeyInput;
        //    _mouseInput = Configuration.Settings.MouseInput;

        //    inputManager.Keyboard.KeyDown += OnKeyDown;
        //    inputManager.Keyboard.KeyUp += OnKeyUp;
        //    inputManager.Mouse.MouseDown += OnMouseDown;
        //}

        //private void OnKeyDown(IKeyboard keyboard, Key key, int arg)
        //{
        //    if (key == _keyInput.Up) Move?.Invoke(MoveDirection.Up);
        //    if (key == _keyInput.Down) Move?.Invoke(MoveDirection.Down);
        //    if (key == _keyInput.Left) Move?.Invoke(MoveDirection.Left);
        //    if (key == _keyInput.Right) Move?.Invoke(MoveDirection.Right);
        //    if (key == _keyInput.Jump) Jump?.Invoke();
        //    if (key == _keyInput.Run) Run?.Invoke();
        //    if (key == _keyInput.Duck) Duck?.Invoke();
        //    if (key == _keyInput.OpenInventory) OpenInventory?.Invoke();
        //    if (key == _keyInput.Select) Select?.Invoke();
        //    if (key == _keyInput.ToggleFightMode) ToggleFightMode?.Invoke();
        //    if (key == _keyInput.Pause) Pause?.Invoke();
        //}

        //private void OnKeyUp(IKeyboard keyboard, Key key, int arg)
        //{
        //    if (key == _keyInput.OpenInventory) OpenInventory?.Invoke();
        //    if (key == _keyInput.Select) Select?.Invoke();
        //    if (key == _keyInput.ToggleFightMode) ToggleFightMode?.Invoke();
        //    if (key == _keyInput.Pause) Pause?.Invoke();
        //}


        //private void OnMouseDown(IMouse mouse, MouseButton button)
        //{
        //    if (button == _mouseInput.Interact) Interact?.Invoke();
        //}

    }
}