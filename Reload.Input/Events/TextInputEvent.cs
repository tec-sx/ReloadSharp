﻿namespace Reload.Input.Events
{
    using Reload.Input.Events.Enums;

    public class TextInputEvent : InputEvent
    {
        /// <summary>
        /// The text that was entered
        /// </summary>
        public string Text;

        /// <summary>
        /// The type of text input event
        /// </summary>
        public TextInputEventType Type;

        /// <summary>
        /// Start of the current composition being edited
        /// </summary>
        public int CompositionStart;

        /// <summary>
        /// Length of the current part of the composition being edited
        /// </summary>
        public int CompositionLength;

        public override string ToString()
        {
            return $"{nameof(Text)}: {Text}, {nameof(Type)}: {Type}, {nameof(CompositionStart)}: {CompositionStart}, {nameof(CompositionLength)}: {CompositionLength}";
        }
    }
}
