using System;

namespace Core.CoreSystem.Graphics
{
    using Config;
    using Logging.Exceptions;
    using System.Runtime.InteropServices;
    using Silk.NET.Windowing;
    using Silk.NET.Windowing.Common;
    using Silk.NET.Vulkan;
    using System.Drawing;

    public static class WindowFactory
    {
        private Instance
        public static IWindow CreateWindow()
        {
            var windowOptions = CreateWindowOptionsFromSettings();

            if (Configuration.Settings.Display.UseVulkan)
            {
                if (TryCreateVulkanWindow(windowOptions, out var vulkanWindow))
                {
                    return vulkanWindow;
                }

                GraphicsBackendNotSupported.Warning("Vulkan is not supported, fallback to OpenGl.");
                
                Configuration.Settings.Display.UseVulkan = false;
                Configuration.SaveSettings();
            }
            
            if(TryCreateOpenGlWindow(windowOptions, out var openGlWindow))
            {
                return openGlWindow;
            }
            
            throw GraphicsBackendNotSupported.Exception("Open GL is not supported.");
        }

        private static WindowOptions CreateWindowOptionsFromSettings()
        {
            var info = Configuration.Settings.Info;
            var display = Configuration.Settings.Display;
            
            var options = display.UseVulkan ? WindowOptions.DefaultVulkan : WindowOptions.Default;
            
            options.Title = $"{info.ProgramName} - v.{info.ProgramVersion}";
            options.Size = new Size(display.Width, display.Height);
            options.WindowState = display.IsFullScreen ? WindowState.Fullscreen : WindowState.Normal;
            options.UpdatesPerSecond = display.TargetFps;
            options.FramesPerSecond = display.TargetFps;
            options.VSync = display.VSync ? VSyncMode.On : VSyncMode.Adaptive;
            options.RunningSlowTolerance = 5;

            return options;
        }

        private static unsafe bool TryCreateVulkanWindow(WindowOptions options, out IWindow window)
        {
            window = Window.Create(options) as IVulkanWindow;
            
            if (window is null || !window.IsVulkanSupported)
            {
                return false;
            }
            
            window.Initialize();

            var vk = Vk.GetApi();
            var appInfo = new Silk.NET.Vulkan.ApplicationInfo
            {
                SType = StructureType.ApplicationInfo,
                PApplicationName = (byte*) Marshal.StringToHGlobalAnsi(options.Title),
                ApplicationVersion = new Version32(1, 0, 0),
                PEngineName = (byte*) Marshal.StringToHGlobalAnsi(Configuration.Settings.Info.ProgramName),
                EngineVersion = new Version32(1, 0, 0),
                ApiVersion = Vk.Version11
            };
            
            var createInfo = new InstanceCreateInfo
            {
                SType = StructureType.InstanceCreateInfo,
                PApplicationInfo = &appInfo
            };
            
            var extensions = ((IVulkanWindow) window).GetRequiredExtensions(out var extCount);
            
            createInfo.EnabledExtensionCount = extCount;
            createInfo.PpEnabledExtensionNames = (byte**) extensions;
            createInfo.EnabledLayerCount = 0;
            createInfo.PNext = null;
            
            fixed (Instance* instance = &_instance)
            {
                if (vk.CreateInstance(&createInfo, null, instance) != Result.Success)
                {
                    throw new Exception("Failed to create instance!");
                }
            }
            
            vk.CurrentInstance = _instance;
            
            if (!vk.TryGetExtension(out _vkSurface))
            {
                throw new NotSupportedException("KHR_surface extension not found.");
            }

            if (!vk.TryGetExtension(out _vkSwapchain))
            {
                throw new NotSupportedException("KHR_swapchain extension not found.");
            }
            
            Marshal.FreeHGlobal((IntPtr) appInfo.PApplicationName);
            Marshal.FreeHGlobal((IntPtr) appInfo.PEngineName);
            
            return true;
        }

        private static bool TryCreateOpenGlWindow(WindowOptions options, out IWindow window)
        {
            window = Window.Create(options);

            if (window is null)
            {
                return false;
            }
            
            window.Initialize();
            return true;
        }
    }
}