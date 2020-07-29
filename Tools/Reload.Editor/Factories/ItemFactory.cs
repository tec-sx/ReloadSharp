using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace Reload.Editor.Factories
{
    internal static class ItemFactory
    {
        private static int _topMargin;
        internal static int TopMargin
        {
            get => _topMargin;
            set => _topMargin = value > 0 ? value : 0;
        }

        internal static ButtonCore GetToolbarButton()
        {
            ButtonCore btn = new ButtonCore();

            btn.SetBackground(55, 55, 55);
            btn.SetHeightPolicy(SizePolicy.Expand);
            btn.SetWidth(30);
            btn.SetPadding(5, 5, 5, 5);
            btn.AddItemState(
                ItemStateType.Hovered,
                new ItemState(Color.FromArgb(30, 255, 255, 255)));

            return btn;
        }

        internal static ImageItem GetToolbarIcon(Bitmap bitmap)
        {
            ImageItem image = new ImageItem(bitmap, false);

            image.KeepAspectRatio(true);

            return image;
        }

        internal static SpinItem GetSpinItem()
        {
            SpinItem item = new SpinItem();

            item.SetParameters(15, 3, 1000, 1);
            item.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            item.SetSize(80, 26);
            item.SetAlignment(ItemAlignment.VCenter, ItemAlignment.Left);
            item.SetMargin(5, 0, 0, 0);
            item.SetBackground(80, 80, 80);
            item.SetForeground(Color.White);
            item.SetBorder(new Border(Color.Gray, new CornerRadius(), 1));
            item.SetPadding(1, 1, 1, 1);

            return item;
        }

        internal static SpaceVIL.Rectangle GetVerticalDivider()
        {
            SpaceVIL.Rectangle item = new SpaceVIL.Rectangle();

            item.SetBackground(120, 120, 120);
            item.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Expand);
            item.SetWidth(1);
            item.SetMargin(5, 6, 5, 6);

            return item;
        }

        internal static HorizontalStack GetToolbar()
        {
            HorizontalStack toolbar = new HorizontalStack();

            toolbar.SetHeightPolicy(SizePolicy.Fixed);
            toolbar.SetHeight(20);
            toolbar.SetBackground(55, 55, 55);
            toolbar.SetPadding(10, 0, 0, 0);
            toolbar.SetSpacing(5);

            return toolbar;
        }

        internal static VerticalSplitArea CreateVerticalSplitArea()
        {
            VerticalSplitArea verticalSplit = new VerticalSplitArea();
            verticalSplit.SetSplitPosition(300);
            verticalSplit.SetSplitThickness(4);
            
            return verticalSplit;
        }
        
        internal static VerticalStack GetStandardLayout()
        {
            VerticalStack layout = new VerticalStack();

            layout.SetMargin(0, _topMargin, 0, 0);
            layout.SetBackground(30, 30, 30);

            return layout;
        }

        internal static VerticalStack CreateRightAside(int width)
        {
            VerticalStack aside = new VerticalStack();

            aside.SetMargin(DisplayService.GetDisplayWidth() - width, _topMargin, 0, 0);
            aside.SetBackground(30, 30, 30);

            return aside;
        }
    }
}