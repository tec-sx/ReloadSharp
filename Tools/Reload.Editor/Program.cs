using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using Reload.Graphics;
using Silk.NET.Windowing.Common;
using SpaceVIL.Common;

namespace Reload.Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonService.InitSpaceVILComponents();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
