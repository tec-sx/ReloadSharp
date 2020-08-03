using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;
using System.Diagnostics;
using System.Drawing;

namespace Reload.Editor.UiElements
{
    /// <summary>
    /// The <seealso cref="MainWindow"/> right aside element.
    /// </summary>
    public class RightAside : ResizableItem
    {
        private VerticalStack _layout;

        private int _padding;

        /// <summary>
        /// Initializes a new instance of the <see cref="RightAside"/> class.
        /// </summary>
        public RightAside()
        {
            _padding = 5;

            IsXFloating = false;
            IsYFloating = false;
            IsXResizable = true;
            IsYResizable = false;

            SetBackground(35, 30, 30);
            SetAlignment(ItemAlignment.Right);
            SetPadding(_padding, _padding, _padding, _padding);

            _layout = new VerticalStack();

            var slider = new HorizontalSlider();
            
            _layout.AddItem(slider);

            base.AddItem(_layout);
        }

        /// <summary>
        /// Sets the parent element of the <see cref="RightAside"/>.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public new void SetParent(Prototype parent)
        {
            parent.AddItem(this);

            SetSize(200, parent.GetHeight());
            SetY(parent.GetY());
            
            _layout.SetSize(GetWidth() - _padding * 2, GetHeight() - _padding * 2);
            _layout.SetBackground(50, 50, 50);
            _layout.UpdateLayout();
        }

        /// <summary>
        /// Adds new item to the layout.
        /// </summary>
        /// <param name="item">The item.</param>
        public new void AddItem(IBaseItem item)
        {
            _layout.AddItem(item);
        }

        /// <summary>
        /// Adds new items to the layout.
        /// </summary>
        /// <param name="items">The items.</param>
        public new void AddItems(params IBaseItem[] items)
        {
            _layout.AddItems(items);
        }
    }
}
