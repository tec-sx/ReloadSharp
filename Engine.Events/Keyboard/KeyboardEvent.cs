namespace Engine.Events.Keyboard
{
    using Silk.NET.Input.Common;
    using System;
    using System.Runtime.CompilerServices;

    [Flags]
    public enum MoveDirection
    {
        None = 0,
        Up = 1,
        Left = 2,
        Right = 4,
        Down = 8,
        UpLeft = Up | Left,
        UpRight = Up | Right,
        DownLeft = Down | Left,
        DownRight = Down | Right,
    }

    [Flags]
    public enum MoveStatus
    {
        Idle,
        Walk,
        Run,
        Duck
    }

    public class KeyboardEvent
    {
        public event Action<MoveDirection, MoveStatus> Move;
        public event Action Jump;
        public event Action OpenInventory;
        public event Action Select;
        public event Action ToggleFightMode;
        public event Action Pause;

        private KeyboardConfiguration keyInput;

        private bool upIsHeld;
        private bool downIsHeld;
        private bool rightIsHeld;
        private bool leftIsHeld;
        private bool runIsHeld;
        private bool duckIsHeld;

        public KeyboardEvent(IKeyboard keyboard)
        {
            keyboard.KeyDown += OnKeyDown;
            keyboard.KeyUp += OnKeyUp;
        }

        public void MapInput(KeyboardConfiguration configuration)
        {
            keyInput = configuration;
        }

        private void OnKeyDown(IKeyboard keyboard, Key key, int arg)
        {
            if (key == keyInput.Up) upIsHeld = true;
            else if (key == keyInput.Down) downIsHeld = true;
            else if (key == keyInput.Left) leftIsHeld = true;
            else if (key == keyInput.Right) rightIsHeld = true;
            else if (key == keyInput.Run) runIsHeld = true;
            else if (key == keyInput.Duck) duckIsHeld = true;

            HandleMovement(key);

            if (key == keyInput.Jump) Jump?.Invoke();
        }

        private void OnKeyUp(IKeyboard keyboard, Key key, int arg)
        {
            if (key == keyInput.Up) upIsHeld = false;
            else if (key == keyInput.Down) downIsHeld = false;
            else if (key == keyInput.Run) runIsHeld = false;
            else if (key == keyInput.Duck) duckIsHeld = false;
            else if (key == keyInput.Left) leftIsHeld = false;
            else if (key == keyInput.Right) rightIsHeld = false;

            HandleMovement(key);

            if (key == keyInput.OpenInventory) OpenInventory?.Invoke();
            if (key == keyInput.Select) Select?.Invoke();
            if (key == keyInput.ToggleFightMode) ToggleFightMode?.Invoke();
            if (key == keyInput.Pause) Pause?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void HandleMovement(Key key)
        {
            // Set moving status
            var moveDirection = MoveDirection.None;
            var moveStatus = MoveStatus.Idle;

            if (upIsHeld && !(rightIsHeld && leftIsHeld && downIsHeld)) moveDirection = MoveDirection.Up;
            else if (upIsHeld && rightIsHeld && !(leftIsHeld && downIsHeld)) moveDirection = MoveDirection.UpRight;
            else if (upIsHeld && leftIsHeld && !(rightIsHeld && downIsHeld)) moveDirection = MoveDirection.UpLeft;
            else if (downIsHeld && !(upIsHeld && rightIsHeld && leftIsHeld)) moveDirection = MoveDirection.Down;
            else if (downIsHeld && rightIsHeld && !(upIsHeld && leftIsHeld)) moveDirection = MoveDirection.DownRight;
            else if (downIsHeld && leftIsHeld && !(upIsHeld && rightIsHeld)) moveDirection = MoveDirection.DownLeft;

            if (moveDirection != MoveDirection.None) moveStatus = MoveStatus.Walk;
            if (runIsHeld && !duckIsHeld) moveStatus = MoveStatus.Run;
            else if (duckIsHeld && !runIsHeld) moveStatus = MoveStatus.Duck;
            //else if (runIsHeld && duckIsHeld) moveStatus = MoveStatus.LowRun;

            Move?.Invoke(moveDirection, moveStatus);
        }

        public void HandleMovementKeyUp()
        {

        }
    }
}
