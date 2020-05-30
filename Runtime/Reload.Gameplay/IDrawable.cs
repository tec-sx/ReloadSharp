namespace Reload.Game
{
    using System;

    /// <summary>
    /// An interface for a drawable game component that is called by the <see cref="GameBase"/> class.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Occurs when the <see cref="DrawOrder"/> property changes.
        /// </summary>
        event Action DrawOrderChanged;

        /// <summary>
        /// Occurs when the <see cref="Visible"/> property changes.
        /// </summary>
        event Action VisibleChanged;

        /// <summary>
        /// Starts the drawing of a frame. This method is followed by calls to Draw and EndDraw.
        /// </summary>
        /// <returns><c>true</c> if Draw should occur, <c>false</c> otherwise</returns>
        bool BeginDraw();

        /// <summary>
        /// Draws this instance.
        /// </summary>
        /// <param name="gameTime">The current timing.</param>
        void Draw(double gameTime);

        /// <summary>
        /// Ends the drawing of a frame. This method is preceeded by calls to Draw and BeginDraw.
        /// </summary>
        void EndDraw();

        /// <summary>
        /// Gets a value indicating whether the <see cref="Draw"/> method should be called by <see cref="GameBase.Draw"/>.
        /// </summary>
        /// <value><c>true</c> if this drawable component is visible; otherwise, <c>false</c>.</value>
        bool Visible { get; }

        /// <summary>
        /// Gets the draw order relative to other objects. <see cref="IDrawable"/> objects with a lower value are drawn first.
        /// </summary>
        /// <value>The draw order.</value>
        int DrawOrder { get; }
    }
}
