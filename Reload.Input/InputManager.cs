namespace Reload.Input
{
    using Reload.Input.Configuration;
    using Silk.NET.Input;
    using Silk.NET.Input.Common;
    using Reload.Game;
    using System.Collections.Generic;
    using Reload.Input.Events;
    using Reload.Input.Source;
    using System.Linq;
    using System;
    using Reload.Core.Collections;
    using Reload.Input.Events.EventArguments;
    using System.Numerics;
    using System.Drawing;

    public partial class InputManager : GameSystemBase
    {
        public event EventHandler<InputPreUpdateEventArgs> PreUpdateInput;
        public event EventHandler<DeviceChangedEventArgs> DeviceRemoved;
        public event EventHandler<DeviceChangedEventArgs> DeviceAdded;

        private IInputContext inputContext;

        private readonly List<InputEvent> events;

        private readonly Dictionary<Type, IInputEventRouter> eventRouters;

        public IReadOnlyList<InputEvent> Events => events;

        public bool HasPointer { get; private set; }

        public bool HasMouse { get; private set; }

        public bool HasKeyboard { get; private set; }

        public bool HasGameController { get; private set; }

        public IMouseDevice Mouse { get; private set; }

        public IKeyboardDevice Keyboard { get; private set; }

        public ITextInputDevice TextInput { get; private set; }

        public InputManager(IGame game) : base(game)
        {
            events = new List<InputEvent>();

            eventRouters = new Dictionary<Type, IInputEventRouter>();
        }

        public void Initialize(
            KeyboardConfiguration keyboardConfiguration,
            MouseConfiguration mouseConfiguration)
        {
            Game.Activated += OnApplicationResumed;
            Game.Deactivated += OnApplicationPaused;

            Game.Window.Load += () =>
            {
                inputContext = Game.Window.CreateInput();

                Keyboard = new Keyboard(inputContext.Keyboards[0]);
                Mouse = new Mouse(inputContext.Mice[0], Game);

                TextInput = Keyboard as ITextInputDevice;

                RegisterEventType<KeyEvent>();
                RegisterEventType<TextInputEvent>();
                RegisterEventType<MouseButtonEvent>();
                RegisterEventType<MouseWheelEvent>();
                RegisterEventType<PointerEvent>();
            };
        }

        public override void Destroy()
        {
            Game.Activated -= OnApplicationResumed;
            Game.Deactivated -= OnApplicationPaused;

            OnApplicationPaused(this, EventArgs.Empty);
        }

        /// <summary>
        /// Lock the mouse's position and hides it until the next call to <see cref="UnlockMousePosition"/>.
        /// </summary>
        /// <param name="forceCenter">If true will make sure that the mouse cursor position moves to the center of the client window</param>
        /// <remarks>This function has no effects on devices that does not have mouse</remarks>
        public void LockMousePosition(bool forceCenter = false)
        {
            // Lock primary mouse
            if (HasMouse)
            {
                Mouse.LockPosition(forceCenter);
            }
        }

        /// <summary>
        /// Unlock the mouse's position previously locked by calling <see cref="LockMousePosition"/> and restore the mouse visibility.
        /// </summary>
        /// <remarks>This function has no effects on devices that does not have mouse</remarks>
        public void UnlockMousePosition()
        {
            if (HasMouse)
            {
                Mouse.UnlockPosition();
            }
        }

        private void SetMousePosition(Vector2 normalizedPosition)
        {
            // Set mouse position for first mouse device
            if (HasMouse)
            {
                Mouse.SetPosition(normalizedPosition);
            }
        }

        public override void Update(double gameTime)
        {
            ResetGlobalInputState();

            // Recycle input event to reduce garbage generation
            for (int i = 0; i < events.Count; i++)
            {
                // The router takes care of putting the event back in its respective InputEventPool since it already has the type information
                eventRouters[events[i].GetType()].PoolEvent(events[i]);
            }

            events.Clear();

            // Notify PreUpdateInput
            PreUpdateInput?.Invoke(this, new InputPreUpdateEventArgs { GameTime = gameTime });

            // Send events to input listeners
            for (int i = 0; i < events.Count; i++)
            {
                IInputEventRouter router;
                if (!eventRouters.TryGetValue(events[i].GetType(), out router))
                {
                    throw new InvalidOperationException($"The event type {events[i].GetType()} was not registered with the input manager and cannot be processed");
                }

                router.RouteEvent(events[i]);
            }
        }

        private void OnApplicationPaused(object sender, EventArgs e)
        {
            // Pause sources
        }

        private void OnApplicationResumed(object sender, EventArgs e)
        {
            // Resume sources
        }

        /// <summary>
        /// Registers an object that listens for certain types of events using the specialized versions of <see cref="IInputEventListener&lt;"/>
        /// </summary>
        /// <param name="listener">The listener to register</param>
        public void AddListener(IInputEventListener listener)
        {
            foreach (var router in eventRouters)
            {
                router.Value.TryAddListener(listener);
            }
        }

        /// <summary>
        /// Removes a previously registered event listener
        /// </summary>
        /// <param name="listener">The listener to remove</param>
        public void RemoveListener(IInputEventListener listener)
        {
            foreach (var pair in eventRouters)
            {
                pair.Value.Listeners.Remove(listener);
            }
        }

        /// <summary>
        /// Registers an input event type to process
        /// </summary>
        /// <typeparam name="TEventType">The event type to process</typeparam>
        public void RegisterEventType<TEventType>() where TEventType : InputEvent, new()
        {
            var type = typeof(TEventType);
            eventRouters.Add(type, new InputEventRouter<TEventType>());
        }

        /// <summary>
        /// Inserts any registered event back into it's <see cref="InputEventPool&lt;"/>.
        /// </summary>
        /// <param name="inputEvent">The event to insert into it's event pool</param>
        public void PoolInputEvent(InputEvent inputEvent)
        {
            eventRouters[inputEvent.GetType()].PoolEvent(inputEvent);
        }

        private interface IInputEventRouter
        {
            HashSet<IInputEventListener> Listeners { get; }

            void PoolEvent(InputEvent evt);

            void RouteEvent(InputEvent evt);

            void TryAddListener(IInputEventListener listener);
        }

        private class InputEventRouter<TEventType> : IInputEventRouter where TEventType : InputEvent, new()
        {
            public HashSet<IInputEventListener> Listeners { get; } = new HashSet<IInputEventListener>(EqualityComparer<IInputEventListener>.Default);

            public void RouteEvent(InputEvent evt)
            {
                var listeners = Listeners.ToArray();
                foreach (var gesture in listeners)
                {
                    ((IInputEventListener<TEventType>)gesture).ProcessEvent((TEventType)evt);
                }
            }

            public void TryAddListener(IInputEventListener listener)
            {
                var specific = listener as IInputEventListener<TEventType>;
                if (specific != null)
                {
                    Listeners.Add(specific);
                }
            }

            public void PoolEvent(InputEvent evt)
            {
                InputEventPool<TEventType>.Enqueue((TEventType)evt);
            }

            /// <summary>
            /// Helper method to transform mouse and pointer event positions to sub rectangles
            /// </summary>
            /// <param name="fromSize">the size of the source rectangle</param>
            /// <param name="destinationRectangle">The destination viewport rectangle</param>
            /// <param name="screenCoordinates">The normalized screen coordinates</param>
            /// <returns></returns>
            //public static Vector2 TransformPosition(Size2F fromSize, RectangleF destinationRectangle, Vector2 screenCoordinates)
            //{
            //    return new Vector2((screenCoordinates.X * fromSize.Width - destinationRectangle.X) / destinationRectangle.Width,
            //        (screenCoordinates.Y * fromSize.Height - destinationRectangle.Y) / destinationRectangle.Height);
            //}
        }
    }
}
