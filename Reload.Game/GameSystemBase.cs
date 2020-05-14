namespace Reload.Game
{
    using System;

    public abstract class GameSystemBase : IGameSystemBase, IUpdateable, IDrawable, IContentable
    {
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        /// <summary>
        /// Gets the <see cref="Game"/> associated with this <see cref="GameSystemBase"/>.
        /// </summary>
        /// <value>The game.</value>
        /// <remarks>This value can be null</remarks>
        public GameBase Game { get; }

        private bool enabled;
        public bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    EnabledChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private int updateOrder;
        public int UpdateOrder
        {
            get => updateOrder;
            set
            {
                if (updateOrder != value)
                {
                    updateOrder = value;
                    UpdateOrderChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private bool visible;
        public bool Visible
        {
            get => visible;
            set
            {
                if (visible != value)
                {
                    visible = value;
                    VisibleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }


        private int drawOrder;
        public int DrawOrder
        {
            get => drawOrder;
            set
            {
                if (drawOrder != value)
                {
                    drawOrder = value;
                    DrawOrderChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public GameSystemBase(IGame game)
        {
            Game = game as GameBase;
        }

        public virtual bool BeginDraw()
        {
            return true;
        }

        public virtual void Draw(double gameTime)
        { }

        public virtual void EndDraw()
        { }

        public virtual void Initialize()
        { }

        void IContentable.LoadContent()
        {
            LoadContent();
        }

        void IContentable.UnloadContent()
        {
            UnloadContent();
        }

        public virtual void LoadContent()
        { }

        public virtual void UnloadContent()
        { }

        public virtual void Update(double gameTime)
        { }

        public virtual void Destroy()
        { }
    }
}
