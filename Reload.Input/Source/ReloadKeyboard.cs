namespace Reload.Input.Source
{
    using Reload.Core.Collections;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class ReloadKeyboard
    {
        public Dictionary<Key, Action> keyActionPairs = new Dictionary<Key, Action>();

        private const int initialBufferSize = 8;
        private IKeyboard keyboardBase;

        public FastList<Key> PressedKeys;
        public FastList<Key> ReleasedKeys;
        public FastList<Key> DownKeys;
        public Dictionary<Key, int> RepeadKeys;

        public ReloadKeyboard(IKeyboard keyboard)
        {
            keyboardBase = keyboard;
            keyboardBase.KeyDown += HandleKeyDown;
            keyboardBase.KeyUp += HandleKeyUp;

            PressedKeys = new FastList<Key>(initialBufferSize);
            ReleasedKeys = new FastList<Key>(initialBufferSize);
            DownKeys = new FastList<Key>(initialBufferSize);
            RepeadKeys = new Dictionary<Key, int>();
        }

        private void HandleKeyDown(IKeyboard keyboard, Key key, int arg)
        {
            PressedKeys.Add(key);
        }

        private void HandleKeyUp(IKeyboard keyboard, Key key, int arg)
        {
            if (PressedKeys.Count == 1)
            {
                keyActionPairs.TryGetValue(key, out Action action);
                action?.Invoke();
            }

            PressedKeys.Remove(key);
        }

        private void HandleTextInput(IKeyboard keyboard, char character)
        {

        }

        public void EnabledTextInput()
        {
            keyboardBase.KeyDown -= HandleKeyDown;
            keyboardBase.KeyUp -= HandleKeyUp;
            keyboardBase.KeyChar += HandleTextInput;
        }

        public void DisableTextInput()
        {
            keyboardBase.KeyChar -= HandleTextInput;
            keyboardBase.KeyDown += HandleKeyDown;
            keyboardBase.KeyUp += HandleKeyUp;
        }
    }
}
