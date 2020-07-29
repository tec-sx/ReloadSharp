using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using Reload.Editor.Factories;
using Silk.NET.Core.Contexts;
using SpaceVIL;
using SpaceVIL.Common;

namespace Reload.Editor
{
    public class MainWindow : ActiveWindow
    {
        private const string _name = "Reload Editor";
        private const string _version = "0.01";
        
        internal ListBox ItemList = new ListBox();
        internal TextArea ItemText = new TextArea();
        internal ButtonCore BtnGenerate;
        internal ButtonCore BtnSave;
        internal SpinItem NumberCount;

        public override void InitWindow()
        {
            string windowTitle = $"{_name} - v.{_version}";
            
            int displayWidth = DisplayService.GetDisplayWidth();
            int displayHeight = DisplayService.GetDisplayHeight();
            SetParameters(nameof(MainWindow), windowTitle, displayWidth, displayHeight, true);
            SetMinSize((int)(displayWidth * 0.5), (int)(displayHeight * 0.5));
            SetBackground(32, 34, 37);
            
            IsMaximized = true;
            IsCentered = true;
            IsTransparent = true;

            OpenGlLayer openGlLayer = new OpenGlLayer();
            
            MainWindow openGlWindow =  new MainWindow();
            openGlWindow.EventOnStart += () => openGlLayer.Initialize();
            
            WindowManager.AddWindow(openGlWindow);
            RenderService.SetGLLayerViewport(openGlWindow, openGlLayer);

            openGlWindow.Show();

            EventOnStart += () =>
            {
                HorizontalStack toolbar = ItemFactory.GetToolbar();
                ItemFactory.TopMargin = toolbar.GetHeight();
                
                VerticalStack layout = ItemFactory.GetStandardLayout();
                VerticalSplitArea verticalSplit = ItemFactory.CreateVerticalSplitArea();

                AddItems(toolbar, layout);
                layout.AddItems(verticalSplit);

                // BtnGenerate = ItemFactory.GetToolbarButton();
                // BtnSave = ItemFactory.GetToolbarButton();
                // NumberCount = ItemFactory.GetSpinItem();
                // ItemText.SetStyle(StyleFactory.GetTextAreaStyle());
                //
                // AddItems(layout);
                // layout.AddItems(toolbar, splitArea);
                // splitArea.AssignLeftItem(ItemList);
                // splitArea.AssignRightItem(ItemText);
                // toolbar.AddItems(BtnGenerate, BtnSave, ItemFactory.GetVerticalDivider(), NumberCount);
            };
            
        }
    }
}