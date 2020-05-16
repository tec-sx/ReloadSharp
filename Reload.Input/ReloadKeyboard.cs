namespace Reload.Input.Source
{
    using Reload.Core;
    using Reload.Core.Collections;
    using Silk.NET.Input.Common;
    using System;
    using System.Collections.Generic;

    public class ReloadKeyboard
    {
        public event Action<Command> FireKeyCommand;

        private Dictionary<Key, Command> keyCommands;
        private List<Chord> chordCommands;

        private int pressedKeysCount;
        private HashSet<Key> keysUsedInChords;

        private IKeyboard keyboardBase;

        public ReloadKeyboard(IKeyboard keyboard)
        {
            keyCommands = new Dictionary<Key, Command>();
            chordCommands = new List<Chord>();

            keyboardBase = keyboard;
            keyboardBase.KeyDown += HandleKeyDown;
            keyboardBase.KeyUp += HandleKeyUp;
        }

        private void HandleKeyDown(IKeyboard keyboard, Key key, int arg)
        {
            if (pressedKeysCount++ == 1 && keyCommands.TryGetValue(key, out var command))
            {
                FireKeyCommand(command);
            }

            for (var i = 0; i < chordCommands.Count; i ++)
            {
                var chord = chordCommands[i];

                if (chord.Keys.Contains(key))
                {
                    chord.KeyMatchCount++;
                }

                if (chord.Keys.Count == chord.KeyMatchCount)
                {
                }
            }
        }

        private void HandleKeyUp(IKeyboard keyboard, Key key, int arg)
        {
            if (pressedKeysCount-- == 1)
            {
                return;
            }

            for (var i = 0; i < chordCommands.Count; i++)
            {
                var chord = chordCommands[i];

                if (chord.Keys.Contains(key))
                {
                    chord.KeyMatchCount--;
                }
            }
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

        public void IsKeyPressed(Key key) => keyboardBase.IsKeyPressed(key);

        public void RegisterKeyPress(Key key, Command command) => keyCommands.Add(key, command);
        public void RegisterChord(Chord chord)
        {
            chordCommands.Add(chord);

            for (var i = 0; i < chord.Keys.Count; i++)
            {
                var key = chord.Keys[i];

                if (!keysUsedInChords.Contains(key))
                {
                    keysUsedInChords.Add(key);
                }
            }
        }
    }
}
