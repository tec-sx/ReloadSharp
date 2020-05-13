namespace Reload.Input.Source
{
    using Reload.Core.Collections;
    using Reload.Input.Events;
    using Reload.Input.Events.Enums;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class Keyboard : ITextInputDevice, IKeyboardDevice, IDisposable
    {
        private readonly IKeyboard silkKeyboard;
        private readonly List<TextInputEvent> textEvents;
        private readonly HashSet<Key> pressedKeys;
        private readonly HashSet<Key> releasedKeys;
        private readonly HashSet<Key> downKeys;

        protected readonly List<KeyEvent> Events;

        public Dictionary<Key, int> KeyRepeats { get; }

        public IReadOnlySet<Key> PressedKeys { get; }

        public IReadOnlySet<Key> ReleasedKeys { get; }

        public IReadOnlySet<Key> DownKeys { get; }

        public int Priority { get; set; }

        public string Name { get; }

        public int Index { get; }

        public bool IsConnected { get; }

        public Keyboard(IKeyboard keyboard)
        {
            silkKeyboard = keyboard;

            Name = keyboard.Name;
            Index = keyboard.Index;
            IsConnected = keyboard.IsConnected;

            textEvents = new List<TextInputEvent>();
            pressedKeys = new HashSet<Key>();
            releasedKeys = new HashSet<Key>();
            downKeys = new HashSet<Key>();

            Events = new List<KeyEvent>();
            KeyRepeats = new Dictionary<Key, int>();
            PressedKeys = new ReadOnlySet<Key>(pressedKeys);
            ReleasedKeys = new ReadOnlySet<Key>(releasedKeys);
            DownKeys = new ReadOnlySet<Key>(downKeys);

            silkKeyboard.KeyDown += HandleKeyDown;
            silkKeyboard.KeyUp += HandleKeyUp;
        }

        public void Dispose()
        {
            silkKeyboard.KeyDown -= HandleKeyDown;
            silkKeyboard.KeyUp -= HandleKeyUp;
            silkKeyboard.KeyChar -= HandleTextInput;
        }

        /// <inheritdoc>
        public bool IsKeyPressed(Key key) => pressedKeys.Contains(key);

        public bool IsKeyReleased(Key key) => releasedKeys.Contains(key);

        public bool IsKeyDown(Key key) => downKeys.Contains(key);

        public virtual void Update(List<InputEvent> inputEvents)
        {
            pressedKeys.Clear();
            releasedKeys.Clear();

            // Fire events
            for (int i = 0; i < Events.Count; i++)
            {
                var keyEvent = Events[i];

                inputEvents.Add(keyEvent);

                if (keyEvent != null)
                {
                    if (keyEvent.IsDown)
                    {
                        pressedKeys.Add(keyEvent.Key);
                    }
                    else
                    {
                        releasedKeys.Add(keyEvent.Key);
                    }
                }
            }

            Events.Clear();
        }

        public void HandleKeyDown(IKeyboard keyboard, Key key, int args)
        {
            // Increment repeat count on subsequent down events
            if (KeyRepeats.TryGetValue(key, out int repeatCount))
            {
                KeyRepeats[key] = ++repeatCount;
            }
            else
            {
                KeyRepeats.Add(key, repeatCount);
                downKeys.Add(key);
            }

            var keyEvent = InputEventPool<KeyEvent>.GetOrCreate(silkKeyboard);
            keyEvent.IsDown = true;
            keyEvent.Key = key;
            keyEvent.RepeatCount = repeatCount;
            Events.Add(keyEvent);
        }

        public void HandleKeyUp(IKeyboard keyboard, Key key, int args)
        {
            // Prevent duplicate up events
            if (!KeyRepeats.ContainsKey(key))
            {
                return;
            }

            KeyRepeats.Remove(key);
            downKeys.Remove(key);
            var keyEvent = InputEventPool<KeyEvent>.GetOrCreate(silkKeyboard);
            keyEvent.IsDown = false;
            keyEvent.Key = key;
            keyEvent.RepeatCount = 0;
            Events.Add(keyEvent);
        }

        public void EnabledTextInput()
        {
            silkKeyboard.KeyDown -= HandleKeyDown;
            silkKeyboard.KeyUp -= HandleKeyUp;
            silkKeyboard.KeyChar += HandleTextInput;
        }

        public void DisableTextInput()
        {
            silkKeyboard.KeyChar -= HandleTextInput;
            silkKeyboard.KeyDown += HandleKeyDown;
            silkKeyboard.KeyUp += HandleKeyUp;
        }

        private void HandleTextInput(IKeyboard keyboard, char character)
        {
            var textInputEvent = InputEventPool<TextInputEvent>.GetOrCreate(silkKeyboard);
            textInputEvent.Text += character;
            textInputEvent.Type = TextInputEventType.Input;
            textEvents.Add(textInputEvent);
        }
    }
}
