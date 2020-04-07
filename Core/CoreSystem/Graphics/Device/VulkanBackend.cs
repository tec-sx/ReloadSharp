namespace Core.CoreSystem.Graphics.Device
{
    using System;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;
    using Config;
    using Silk.NET.Vulkan;
    using Silk.NET.Core.Native;
    using Silk.NET.GLFW;
    using Silk.NET.Vulkan.Extensions.EXT;
    using Silk.NET.Vulkan.Extensions.KHR;
    using Silk.NET.Windowing.Common;

    using Image = Silk.NET.Vulkan.Image;
    using System.Linq;

    public struct SwapChainSupportDetails
    {
        public SurfaceCapabilitiesKHR Capabilities { get; set; }
        public SurfaceFormatKHR[] Formats { get; set; }
        public PresentModeKHR[] PresentModes { get; set; }
    }

    public struct QueueFamilyIndices
    {
        public uint? GraphicsFamily { get; set; }
        public uint? PresentFamily { get; set; }

        public bool IsComplete()
        {
            return GraphicsFamily.HasValue && PresentFamily.HasValue;
        }
    }

    internal sealed class VulkanBackend : GraphicsBackendBase<Vk>
    {
#if DEBUG
        public const bool EnableValidationLayers = true;
# else
        public const bool EnableValidationLayers = false;
#endif
        public const int MaxFramesInFlight = 8;

        public Vk Api { get; } = Vk.GetApi();

        private IVulkanWindow _window;

        private Instance _instance;
        private KhrSurface _vkSurface;
        private KhrSwapchain _vkSwapchain;
        private ExtDebugUtils _debugUtils;
        private string[] _validationLayers = { "VK_LAYER_KHRONOS_validation" };
        private string[] _deviceExtensions = { "VK_KHR_swapchain" };

        private DebugUtilsMessengerEXT _debugMessenger;
        private SurfaceKHR _surface;

        private PhysicalDevice _physicalDevice;
        private Device _device;

        private Queue _graphicsQueue;
        private Queue _presentQueue;

        private SwapchainKHR _swapchain;
        private Image[] _swapchainImages;
        private Format _swapchainImageFormat;
        private Extent2D _swapchainExtent;
        private ImageView[] _swapchainImageViews;
        private Framebuffer[] _swapchainFramebuffers;

        private RenderPass _renderPass;
        private PipelineLayout _pipelineLayout;
        private Pipeline _graphicsPipeline;

        private CommandPool _commandPool;
        private CommandBuffer[] _commandBuffers;

        private Semaphore[] _imageAvailableSemaphores;
        private Semaphore[] _renderFinishedSemaphores;
        private Fence[] _inFlightFences;
        private Fence[] _imagesInFlight;
        private uint _currentFrame;

        public override void Initialize(IWindow window)
        {
            _window = window as IVulkanWindow;

            CreateInstance();
            SetupDebugMessenger();
            CreateSurface();
            PickPhysicalDevice();
            CreateLogicalDevice();
            CreateSwapChain();
            CreateImageViews();
            CreateRenderPass();
            CreateGraphicsPipeline();
            CreateFramebuffers();
            CreateCommandPool();
            CreateCommandBuffers();
            CreateSyncObjects();

            _window.Render += DrawFrame;
        }

        public void WaitForIdle()
        {
            Api.DeviceWaitIdle(_device);
        }


        public override unsafe void Dispose()
        {
            for (var i = 0; i < MaxFramesInFlight; i++)
            {
                Api.DestroySemaphore(_device, _renderFinishedSemaphores[i], (AllocationCallbacks*)null);
                Api.DestroySemaphore(_device, _imageAvailableSemaphores[i], (AllocationCallbacks*)null);
                Api.DestroyFence(_device, _inFlightFences[i], (AllocationCallbacks*)null);
            }

            Api.DestroyCommandPool(_device, _commandPool, (AllocationCallbacks*)null);

            foreach (var framebuffer in _swapchainFramebuffers)
            {
                Api.DestroyFramebuffer(_device, framebuffer, (AllocationCallbacks*)null);
            }

            Api.DestroyPipeline(_device, _graphicsPipeline, (AllocationCallbacks*)null);
            Api.DestroyPipelineLayout(_device, _pipelineLayout, (AllocationCallbacks*)null);
            Api.DestroyRenderPass(_device, _renderPass, (AllocationCallbacks*)null);

            foreach (var imageView in _swapchainImageViews)
            {
                Api.DestroyImageView(_device, imageView, (AllocationCallbacks*)null);
            }

            _vkSwapchain.DestroySwapchain(_device, _swapchain, (AllocationCallbacks*)null);
            Api.DestroyDevice(_device, (AllocationCallbacks*)null);

            if (EnableValidationLayers)
            {
                _debugUtils.DestroyDebugUtilsMessenger(_instance, _debugMessenger, (AllocationCallbacks*)null);
            }

            _vkSurface.DestroySurface(_instance, _surface, (AllocationCallbacks*)null);
            Api.DestroyInstance(_instance, (AllocationCallbacks*)null);
        }

        #region Instance

        public unsafe void CreateInstance()
        {
            if (EnableValidationLayers && !CheckValidationLayerSupport())
            {
                throw new NotSupportedException("Validation layers requested, but not available!");
            }

            var appInfo = new Silk.NET.Vulkan.ApplicationInfo
            {
                SType = StructureType.ApplicationInfo,
                PApplicationName = (byte*)Marshal.StringToHGlobalAnsi(_window.Title),
                ApplicationVersion = new Version32(1, 0, 0),
                PEngineName = (byte*)Marshal.StringToHGlobalAnsi(Configuration.Settings.Info.ProgramName),
                EngineVersion = new Version32(1, 0, 0),
                ApiVersion = Vk.Version11
            };

            var createInfo = new InstanceCreateInfo
            {
                SType = StructureType.InstanceCreateInfo,
                PApplicationInfo = &appInfo
            };

            var extensions = (_window).GetRequiredExtensions(out var extCount);

            createInfo.EnabledExtensionCount = extCount;
            createInfo.PpEnabledExtensionNames = (byte**)extensions;


            if (EnableValidationLayers)
            {
                createInfo.EnabledLayerCount = (uint)_validationLayers.Length;
                createInfo.PpEnabledLayerNames = (byte**)SilkMarshal.MarshalStringArrayToPtr(_validationLayers);
            }
            else
            {
                createInfo.EnabledLayerCount = 0;
                createInfo.PNext = null;
            }

            fixed (Instance* instance = &_instance)
            {
                if (Api.CreateInstance(&createInfo, null, instance) != Result.Success)
                {
                    throw new Exception("Failed to create instance!");
                }
            }

            Api.CurrentInstance = _instance;

            if (!Api.TryGetExtension(out _vkSurface))
            {
                throw new NotSupportedException("KHR_surface extension not found.");
            }

            if (!Api.TryGetExtension(out _vkSwapchain))
            {
                throw new NotSupportedException("KHR_swapchain extension not found.");
            }

            Marshal.FreeHGlobal((IntPtr)appInfo.PApplicationName);
            Marshal.FreeHGlobal((IntPtr)appInfo.PEngineName);

            if (EnableValidationLayers)
            {
                SilkMarshal.FreeStringArrayPtr((IntPtr)createInfo.PpEnabledLayerNames, _validationLayers.Length);
            }
        }

        private unsafe bool CheckValidationLayerSupport()
        {
            uint layerCount = 0;
            Api.EnumerateInstanceLayerProperties(&layerCount, (LayerProperties*)0);

            var availableLayers = new LayerProperties[layerCount];
            fixed (LayerProperties* availableLayersPtr = availableLayers)
                Api.EnumerateInstanceLayerProperties(&layerCount, availableLayersPtr);

            foreach (var layerName in _validationLayers)
            {
                var layerFound = false;

                foreach (var layerProperties in availableLayers)
                {
                    if (layerName == Marshal.PtrToStringAnsi((IntPtr)layerProperties.LayerName))
                    {
                        layerFound = true;
                        break;
                    }
                }

                if (!layerFound)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Debug

        private unsafe void SetupDebugMessenger()
        {
            if (!EnableValidationLayers) return;
            if (!Api.TryGetExtension(out _debugUtils)) return;

            var createInfo = new DebugUtilsMessengerCreateInfoEXT();

            createInfo.SType = StructureType.DebugUtilsMessengerCreateInfoExt;
            createInfo.MessageSeverity = DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityVerboseBitExt |
                                         DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityWarningBitExt |
                                         DebugUtilsMessageSeverityFlagsEXT.DebugUtilsMessageSeverityErrorBitExt;
            createInfo.MessageType = DebugUtilsMessageTypeFlagsEXT.DebugUtilsMessageTypeGeneralBitExt |
                                     DebugUtilsMessageTypeFlagsEXT.DebugUtilsMessageTypePerformanceBitExt |
                                     DebugUtilsMessageTypeFlagsEXT.DebugUtilsMessageTypeValidationBitExt;
            createInfo.PfnUserCallback = FuncPtr.Of<DebugUtilsMessengerCallbackFunctionEXT>(DebugCallback);

            fixed (DebugUtilsMessengerEXT* debugMessenger = &_debugMessenger)
            {
                if (_debugUtils.CreateDebugUtilsMessenger
                        (_instance, &createInfo, null, debugMessenger) != Result.Success)
                {
                    throw new Exception("Failed to create debug messenger.");
                }
            }
        }

        private unsafe uint DebugCallback
        (
            DebugReportFlagsEXT flags,
            DebugReportObjectTypeEXT objecttype,
            ulong o,
            UIntPtr location,
            int messagecode,
            char* playerprefix,
            char* pmessage,
            void* puserdata
        )
        {
            Console.Write("[" + flags.ToString().Replace("DebugReport", string.Empty) + "] ");
            Console.Write(Marshal.PtrToStringAnsi((IntPtr)playerprefix) + "/");
            Console.Write(objecttype.ToString().Replace("DebugReportObjectType", string.Empty)[..^3]);
            Console.WriteLine(": " + Marshal.PtrToStringAnsi((IntPtr)pmessage));
            return Vk.False;
        }

        #endregion

        private unsafe void CreateSurface()
        {
            _surface = _window.CreateSurface<AllocationCallbacks>(_instance.ToHandle(), null).ToSurface();
        }


        #region Physical Device
        private unsafe void PickPhysicalDevice()
        {
            var deviceCount = 0u;
            Api.EnumeratePhysicalDevices(_instance, &deviceCount, (PhysicalDevice*)null);

            if (deviceCount == 0)
            {
                throw new NotSupportedException("Failed to find GPUs with Vulkan support.");
            }

            var devices = stackalloc PhysicalDevice[(int)deviceCount];
            Api.EnumeratePhysicalDevices(_instance, &deviceCount, devices);

            for (var i = 0; i < deviceCount; i++)
            {
                var device = devices[i];
                if (IsDeviceSuitable(device))
                {
                    _physicalDevice = device;
                    return;
                }
            }

            throw new Exception("No suitable device.");
        }

        private bool IsDeviceSuitable(PhysicalDevice device)
        {
            var indices = FindQueueFamilies(device);

            var extensionsSupported = CheckDeviceExtensionSupport(device);

            var swapChainAdequate = false;
            if (extensionsSupported)
            {
                var swapChainSupport = QuerySwapChainSupport(device);
                swapChainAdequate = swapChainSupport.Formats.Length != 0 && swapChainSupport.PresentModes.Length != 0;
            }

            return indices.IsComplete() && extensionsSupported && swapChainAdequate;
        }

        private unsafe SwapChainSupportDetails QuerySwapChainSupport(PhysicalDevice device)
        {
            var details = new SwapChainSupportDetails();
            _vkSurface.GetPhysicalDeviceSurfaceCapabilities(device, _surface, out var surfaceCapabilities);
            details.Capabilities = surfaceCapabilities;

            var formatCount = 0u;
            _vkSurface.GetPhysicalDeviceSurfaceFormats(device, _surface, &formatCount, (SurfaceFormatKHR*)null);

            if (formatCount != 0)
            {
                details.Formats = new SurfaceFormatKHR[formatCount];
                var formats = stackalloc SurfaceFormatKHR[(int)formatCount];
                _vkSurface.GetPhysicalDeviceSurfaceFormats(device, _surface, &formatCount, formats);

                for (var i = 0; i < formatCount; i++)
                {
                    details.Formats[i] = formats[i];
                }
            }

            var presentModeCount = 0u;
            _vkSurface.GetPhysicalDeviceSurfacePresentModes(device, _surface, &presentModeCount, (PresentModeKHR*)null);

            if (presentModeCount != 0)
            {
                details.PresentModes = new PresentModeKHR[presentModeCount];
                var modes = stackalloc PresentModeKHR[(int)presentModeCount];
                _vkSurface.GetPhysicalDeviceSurfacePresentModes(device, _surface, &presentModeCount, modes);

                for (var i = 0; i < formatCount; i++)
                {
                    details.PresentModes[i] = modes[i];
                }
            }

            return details;
        }

        private unsafe QueueFamilyIndices FindQueueFamilies(PhysicalDevice device)
        {
            var indices = new QueueFamilyIndices();

            uint queryFamilyCount = 0;
            Api.GetPhysicalDeviceQueueFamilyProperties(device, &queryFamilyCount, (QueueFamilyProperties*)null);

            var queueFamilies = stackalloc QueueFamilyProperties[(int)queryFamilyCount];

            Api.GetPhysicalDeviceQueueFamilyProperties(device, &queryFamilyCount, queueFamilies);
            for (var i = 0u; i < queryFamilyCount; i++)
            {
                var queueFamily = queueFamilies[i];
                // note: HasFlag is slow on .NET Core 2.1 and below.
                // if you're targeting these versions, use ((queueFamily.QueueFlags & QueueFlags.QueueGraphicsBit) != 0)
                if (queueFamily.QueueFlags.HasFlag(QueueFlags.QueueGraphicsBit))
                {
                    indices.GraphicsFamily = i;
                }

                _vkSurface.GetPhysicalDeviceSurfaceSupport(device, i, _surface, out var presentSupport);

                if (presentSupport == Vk.True)
                {
                    indices.PresentFamily = i;
                }

                if (indices.IsComplete())
                {
                    break;
                }
            }

            return indices;
        }

        private unsafe bool CheckDeviceExtensionSupport(PhysicalDevice device)
        {
            uint extensionCount;
            Api.EnumerateDeviceExtensionProperties(device, (byte*)null, &extensionCount, (ExtensionProperties*)null);

            var availableExtensions = stackalloc ExtensionProperties[(int)extensionCount];
            Api.EnumerateDeviceExtensionProperties(device, (byte*)null, &extensionCount, availableExtensions);

            var requiredExtensions = new List<string>();
            requiredExtensions.AddRange(_deviceExtensions);

            for (var i = 0u; i < extensionCount; i++)
            {
                requiredExtensions.Remove(Marshal.PtrToStringAnsi((IntPtr)availableExtensions[i].ExtensionName));
            }

            return requiredExtensions.Count == 0;
        }

        #endregion

        #region Logical Device

        private unsafe void CreateLogicalDevice()
        {
            var indices = FindQueueFamilies(_physicalDevice);
            var uniqueQueueFamilies = new[] { indices.GraphicsFamily.Value, indices.PresentFamily.Value };
            var queueCreateInfos = stackalloc DeviceQueueCreateInfo[uniqueQueueFamilies.Length];

            var queuePriority = 1f;
            for (var i = 0; i < uniqueQueueFamilies.Length; i++)
            {
                var queueCreateInfo = new DeviceQueueCreateInfo
                {
                    SType = StructureType.DeviceQueueCreateInfo,
                    QueueFamilyIndex = uniqueQueueFamilies[i],
                    QueueCount = 1,
                    PQueuePriorities = &queuePriority
                };
                queueCreateInfos[i] = queueCreateInfo;
            }

            var deviceFeatures = new PhysicalDeviceFeatures();

            var createInfo = new DeviceCreateInfo();
            createInfo.SType = StructureType.DeviceCreateInfo;
            createInfo.QueueCreateInfoCount = (uint)uniqueQueueFamilies.Length;
            createInfo.PQueueCreateInfos = queueCreateInfos;
            createInfo.PEnabledFeatures = &deviceFeatures;
            createInfo.EnabledExtensionCount = (uint)_deviceExtensions.Length;

            var enabledExtensionNames = SilkMarshal.MarshalStringArrayToPtr(_deviceExtensions);
            createInfo.PpEnabledExtensionNames = (byte**)enabledExtensionNames;

            if (EnableValidationLayers)
            {
                createInfo.EnabledLayerCount = (uint)_validationLayers.Length;
                createInfo.PpEnabledLayerNames = (byte**)SilkMarshal.MarshalStringArrayToPtr(_validationLayers);
            }
            else
            {
                createInfo.EnabledLayerCount = 0;
            }

            fixed (Device* device = &_device)
            {
                if (Api.CreateDevice(_physicalDevice, &createInfo, null, device) != Result.Success)
                {
                    throw new Exception("Failed to create logical device.");
                }
            }

            fixed (Queue* graphicsQueue = &_graphicsQueue)
            {
                Api.GetDeviceQueue(_device, indices.GraphicsFamily.Value, 0, graphicsQueue);
            }

            fixed (Queue* presentQueue = &_presentQueue)
            {
                Api.GetDeviceQueue(_device, indices.PresentFamily.Value, 0, presentQueue);
            }

            Api.CurrentDevice = _device;
        }

        #endregion

        #region SwapChain

        private unsafe void CreateSwapChain()
        {
            var swapChainSupport = QuerySwapChainSupport(_physicalDevice);

            var surfaceFormat = ChooseSwapSurfaceFormat(swapChainSupport.Formats);
            var presentMode = ChooseSwapPresentMode(swapChainSupport.PresentModes);
            var extent = ChooseSwapExtent(swapChainSupport.Capabilities);

            var imageCount = swapChainSupport.Capabilities.MinImageCount + 1;
            if (swapChainSupport.Capabilities.MaxImageCount > 0 &&
                imageCount > swapChainSupport.Capabilities.MaxImageCount)
            {
                imageCount = swapChainSupport.Capabilities.MaxImageCount;
            }

            var createInfo = new SwapchainCreateInfoKHR
            {
                SType = StructureType.SwapchainCreateInfoKhr,
                Surface = _surface,
                MinImageCount = imageCount,
                ImageFormat = surfaceFormat.Format,
                ImageColorSpace = surfaceFormat.ColorSpace,
                ImageExtent = extent,
                ImageArrayLayers = 1,
                ImageUsage = ImageUsageFlags.ImageUsageColorAttachmentBit
            };

            var indices = FindQueueFamilies(_physicalDevice);
            uint[] queueFamilyIndices = { indices.GraphicsFamily.Value, indices.PresentFamily.Value };

            fixed (uint* qfiPtr = queueFamilyIndices)
            {
                if (indices.GraphicsFamily != indices.PresentFamily)
                {
                    createInfo.ImageSharingMode = SharingMode.Concurrent;
                    createInfo.QueueFamilyIndexCount = 2;
                    createInfo.PQueueFamilyIndices = qfiPtr;
                }
                else
                {
                    createInfo.ImageSharingMode = SharingMode.Exclusive;
                }

                createInfo.PreTransform = swapChainSupport.Capabilities.CurrentTransform;
                createInfo.CompositeAlpha = CompositeAlphaFlagsKHR.CompositeAlphaOpaqueBitKhr;
                createInfo.PresentMode = presentMode;
                createInfo.Clipped = Vk.True;

                createInfo.OldSwapchain = default;

                fixed (SwapchainKHR* swapchain = &_swapchain)
                {
                    if (_vkSwapchain.CreateSwapchain(_device, &createInfo, null, swapchain) != Result.Success)
                    {
                        throw new Exception("failed to create swap chain!");
                    }
                }
            }

            _vkSwapchain.GetSwapchainImages(_device, _swapchain, &imageCount, (Image*)null);
            _swapchainImages = new Image[imageCount];
            fixed (Image* swapchainImage = _swapchainImages)
            {
                _vkSwapchain.GetSwapchainImages(_device, _swapchain, &imageCount, swapchainImage);
            }

            _swapchainImageFormat = surfaceFormat.Format;
            _swapchainExtent = extent;
        }

        private Extent2D ChooseSwapExtent(SurfaceCapabilitiesKHR capabilities)
        {
            if (capabilities.CurrentExtent.Width != uint.MaxValue)
            {
                return capabilities.CurrentExtent;
            }

            var actualExtent = new Extent2D
            { Height = (uint)_window.Size.Height, Width = (uint)_window.Size.Width };
            actualExtent.Width = new[]
            {
                capabilities.MinImageExtent.Width,
                new[] {capabilities.MaxImageExtent.Width, actualExtent.Width}.Min()
            }.Max();
            actualExtent.Height = new[]
            {
                capabilities.MinImageExtent.Height,
                new[] {capabilities.MaxImageExtent.Height, actualExtent.Height}.Min()
            }.Max();

            return actualExtent;
        }

        private PresentModeKHR ChooseSwapPresentMode(PresentModeKHR[] presentModes)
        {
            foreach (var availablePresentMode in presentModes)
            {
                if (availablePresentMode == PresentModeKHR.PresentModeMailboxKhr)
                {
                    return availablePresentMode;
                }
            }

            return PresentModeKHR.PresentModeFifoKhr;
        }

        private SurfaceFormatKHR ChooseSwapSurfaceFormat(SurfaceFormatKHR[] formats)
        {
            foreach (var format in formats)
            {
                if (format.Format == Format.B8G8R8A8Unorm)
                {
                    return format;
                }
            }

            return formats[0];
        }

        private unsafe void CreateImageViews()
        {
            _swapchainImageViews = new ImageView[_swapchainImages.Length];

            for (var i = 0; i < _swapchainImages.Length; i++)
            {
                var createInfo = new ImageViewCreateInfo
                {
                    SType = StructureType.ImageViewCreateInfo,
                    Image = _swapchainImages[i],
                    ViewType = ImageViewType.ImageViewType2D,
                    Format = _swapchainImageFormat,
                    Components =
                    {
                        R = ComponentSwizzle.Identity,
                        G = ComponentSwizzle.Identity,
                        B = ComponentSwizzle.Identity,
                        A = ComponentSwizzle.Identity
                    },
                    SubresourceRange =
                    {
                        AspectMask = ImageAspectFlags.ImageAspectColorBit,
                        BaseMipLevel = 0,
                        LevelCount = 1,
                        BaseArrayLayer = 0,
                        LayerCount = 1
                    }
                };

                ImageView imageView = default;
                if (Api.CreateImageView(_device, &createInfo, null, &imageView) != Result.Success)
                {
                    throw new Exception("failed to create image views!");
                }

                _swapchainImageViews[i] = imageView;
            }
        }

        private unsafe void CreateRenderPass()
        {
            var colorAttachment = new AttachmentDescription
            {
                Format = _swapchainImageFormat,
                Samples = SampleCountFlags.SampleCount1Bit,
                LoadOp = AttachmentLoadOp.Clear,
                StoreOp = AttachmentStoreOp.Store,
                StencilLoadOp = AttachmentLoadOp.DontCare,
                StencilStoreOp = AttachmentStoreOp.DontCare,
                InitialLayout = ImageLayout.Undefined,
                FinalLayout = ImageLayout.PresentSrcKhr
            };

            var colorAttachmentRef = new AttachmentReference
            {
                Attachment = 0,
                Layout = ImageLayout.ColorAttachmentOptimal
            };

            var subpass = new SubpassDescription
            {
                PipelineBindPoint = PipelineBindPoint.Graphics,
                ColorAttachmentCount = 1,
                PColorAttachments = &colorAttachmentRef
            };

            var dependency = new SubpassDependency
            {
                SrcSubpass = Vk.SubpassExternal,
                DstSubpass = 0,
                SrcStageMask = PipelineStageFlags.PipelineStageColorAttachmentOutputBit,
                SrcAccessMask = 0,
                DstStageMask = PipelineStageFlags.PipelineStageColorAttachmentOutputBit,
                DstAccessMask = AccessFlags.AccessColorAttachmentReadBit | AccessFlags.AccessColorAttachmentWriteBit
            };

            var renderPassInfo = new RenderPassCreateInfo
            {
                SType = StructureType.RenderPassCreateInfo,
                AttachmentCount = 1,
                PAttachments = &colorAttachment,
                SubpassCount = 1,
                PSubpasses = &subpass,
                DependencyCount = 1,
                PDependencies = &dependency
            };

            fixed (RenderPass* renderPass = &_renderPass)
            {
                if (Api.CreateRenderPass(_device, &renderPassInfo, null, renderPass) != Result.Success)
                {
                    throw new Exception("failed to create render pass!");
                }
            }
        }

        #endregion

        #region Shaders

        private unsafe void CreateGraphicsPipeline()
        {
            var vertShaderCode = Program.LoadEmbeddedResourceBytes("VulkanTriangle.shader.vert.spv");
            var fragShaderCode = Program.LoadEmbeddedResourceBytes("VulkanTriangle.shader.frag.spv");

            var vertShaderModule = CreateShaderModule(vertShaderCode);
            var fragShaderModule = CreateShaderModule(fragShaderCode);

            var vertShaderStageInfo = new PipelineShaderStageCreateInfo
            {
                SType = StructureType.PipelineShaderStageCreateInfo,
                Stage = ShaderStageFlags.ShaderStageVertexBit,
                Module = vertShaderModule,
                PName = (byte*)SilkMarshal.MarshalStringToPtr("main")
            };

            var fragShaderStageInfo = new PipelineShaderStageCreateInfo
            {
                SType = StructureType.PipelineShaderStageCreateInfo,
                Stage = ShaderStageFlags.ShaderStageFragmentBit,
                Module = fragShaderModule,
                PName = (byte*)SilkMarshal.MarshalStringToPtr("main")
            };

            var shaderStages = stackalloc PipelineShaderStageCreateInfo[2];
            shaderStages[0] = vertShaderStageInfo;
            shaderStages[1] = fragShaderStageInfo;

            var vertexInputInfo = new PipelineVertexInputStateCreateInfo
            {
                SType = StructureType.PipelineVertexInputStateCreateInfo,
                VertexBindingDescriptionCount = 0,
                VertexAttributeDescriptionCount = 0
            };

            var inputAssembly = new PipelineInputAssemblyStateCreateInfo
            {
                SType = StructureType.PipelineInputAssemblyStateCreateInfo,
                Topology = PrimitiveTopology.TriangleList,
                PrimitiveRestartEnable = Vk.False
            };

            var viewport = new Viewport
            {
                X = 0.0f,
                Y = 0.0f,
                Width = _swapchainExtent.Width,
                Height = _swapchainExtent.Height,
                MinDepth = 0.0f,
                MaxDepth = 1.0f
            };

            var scissor = new Rect2D { Offset = default, Extent = _swapchainExtent };

            var viewportState = new PipelineViewportStateCreateInfo
            {
                SType = StructureType.PipelineViewportStateCreateInfo,
                ViewportCount = 1,
                PViewports = &viewport,
                ScissorCount = 1,
                PScissors = &scissor
            };

            var rasterizer = new PipelineRasterizationStateCreateInfo
            {
                SType = StructureType.PipelineRasterizationStateCreateInfo,
                DepthClampEnable = Vk.False,
                RasterizerDiscardEnable = Vk.False,
                PolygonMode = PolygonMode.Fill,
                LineWidth = 1.0f,
                CullMode = CullModeFlags.CullModeBackBit,
                FrontFace = FrontFace.Clockwise,
                DepthBiasEnable = Vk.False
            };

            var multisampling = new PipelineMultisampleStateCreateInfo
            {
                SType = StructureType.PipelineMultisampleStateCreateInfo,
                SampleShadingEnable = Vk.False,
                RasterizationSamples = SampleCountFlags.SampleCount1Bit
            };

            var colorBlendAttachment = new PipelineColorBlendAttachmentState
            {
                ColorWriteMask = ColorComponentFlags.ColorComponentRBit |
                                 ColorComponentFlags.ColorComponentGBit |
                                 ColorComponentFlags.ColorComponentBBit |
                                 ColorComponentFlags.ColorComponentABit,
                BlendEnable = Vk.False
            };

            var colorBlending = new PipelineColorBlendStateCreateInfo
            {
                SType = StructureType.PipelineColorBlendStateCreateInfo,
                LogicOpEnable = Vk.False,
                LogicOp = LogicOp.Copy,
                AttachmentCount = 1,
                PAttachments = &colorBlendAttachment
            };

            colorBlending.BlendConstants[0] = 0.0f;
            colorBlending.BlendConstants[1] = 0.0f;
            colorBlending.BlendConstants[2] = 0.0f;
            colorBlending.BlendConstants[3] = 0.0f;

            var pipelineLayoutInfo = new PipelineLayoutCreateInfo
            {
                SType = StructureType.PipelineLayoutCreateInfo,
                SetLayoutCount = 0,
                PushConstantRangeCount = 0
            };

            fixed (PipelineLayout* pipelineLayout = &_pipelineLayout)
            {
                if (Api.CreatePipelineLayout(_device, &pipelineLayoutInfo, null, pipelineLayout) != Result.Success)
                {
                    throw new Exception("failed to create pipeline layout!");
                }
            }

            var pipelineInfo = new GraphicsPipelineCreateInfo
            {
                SType = StructureType.GraphicsPipelineCreateInfo,
                StageCount = 2,
                PStages = shaderStages,
                PVertexInputState = &vertexInputInfo,
                PInputAssemblyState = &inputAssembly,
                PViewportState = &viewportState,
                PRasterizationState = &rasterizer,
                PMultisampleState = &multisampling,
                PColorBlendState = &colorBlending,
                Layout = _pipelineLayout,
                RenderPass = _renderPass,
                Subpass = 0,
                BasePipelineHandle = default
            };

            fixed (Pipeline* graphicsPipeline = &_graphicsPipeline)
            {
                if (Api.CreateGraphicsPipelines
                        (_device, default, 1, &pipelineInfo, null, graphicsPipeline) != Result.Success)
                {
                    throw new Exception("failed to create graphics pipeline!");
                }
            }

            Api.DestroyShaderModule(_device, fragShaderModule, (AllocationCallbacks*)null);
            Api.DestroyShaderModule(_device, vertShaderModule, (AllocationCallbacks*)null);
        }

        private unsafe ShaderModule CreateShaderModule(byte[] code)
        {
            var createInfo = new ShaderModuleCreateInfo
            {
                SType = StructureType.ShaderModuleCreateInfo,
                CodeSize = new UIntPtr((uint)code.Length)
            };
            fixed (byte* codePtr = code)
            {
                createInfo.PCode = (uint*)codePtr;
            }

            var shaderModule = new ShaderModule();
            if (Api.CreateShaderModule(_device, &createInfo, null, &shaderModule) != Result.Success)
            {
                throw new Exception("failed to create shader module!");
            }

            return shaderModule;
        }

        #endregion

        private unsafe void CreateFramebuffers()
        {
            _swapchainFramebuffers = new Framebuffer[_swapchainImageViews.Length];

            for (var i = 0; i < _swapchainImageViews.Length; i++)
            {
                var attachment = _swapchainImageViews[i];
                var framebufferInfo = new FramebufferCreateInfo
                {
                    SType = StructureType.FramebufferCreateInfo,
                    RenderPass = _renderPass,
                    AttachmentCount = 1,
                    PAttachments = &attachment,
                    Width = _swapchainExtent.Width,
                    Height = _swapchainExtent.Height,
                    Layers = 1
                };

                var framebuffer = new Framebuffer();
                if (Api.CreateFramebuffer(_device, &framebufferInfo, null, &framebuffer) != Result.Success)
                {
                    throw new Exception("failed to create framebuffer!");
                }

                _swapchainFramebuffers[i] = framebuffer;
            }
        }

        private unsafe void CreateCommandPool()
        {
            var queueFamilyIndices = FindQueueFamilies(_physicalDevice);

            var poolInfo = new CommandPoolCreateInfo
            {
                SType = StructureType.CommandPoolCreateInfo,
                QueueFamilyIndex = queueFamilyIndices.GraphicsFamily.Value
            };

            fixed (CommandPool* commandPool = &_commandPool)
            {
                if (Api.CreateCommandPool(_device, &poolInfo, null, commandPool) != Result.Success)
                {
                    throw new Exception("failed to create command pool!");
                }
            }
        }

        private unsafe void CreateCommandBuffers()
        {
            _commandBuffers = new CommandBuffer[_swapchainFramebuffers.Length];

            var allocInfo = new CommandBufferAllocateInfo
            {
                SType = StructureType.CommandBufferAllocateInfo,
                CommandPool = _commandPool,
                Level = CommandBufferLevel.Primary,
                CommandBufferCount = (uint)_commandBuffers.Length
            };

            fixed (CommandBuffer* commandBuffers = _commandBuffers)
            {
                if (Api.AllocateCommandBuffers(_device, &allocInfo, commandBuffers) != Result.Success)
                {
                    throw new Exception("failed to allocate command buffers!");
                }
            }

            for (var i = 0; i < _commandBuffers.Length; i++)
            {
                var beginInfo = new CommandBufferBeginInfo { SType = StructureType.CommandBufferBeginInfo };

                if (Api.BeginCommandBuffer(_commandBuffers[i], &beginInfo) != Result.Success)
                {
                    throw new Exception("failed to begin recording command buffer!");
                }

                var renderPassInfo = new RenderPassBeginInfo
                {
                    SType = StructureType.RenderPassBeginInfo,
                    RenderPass = _renderPass,
                    Framebuffer = _swapchainFramebuffers[i],
                    RenderArea = { Offset = new Offset2D { X = 0, Y = 0 }, Extent = _swapchainExtent }
                };

                var clearColor = new ClearValue
                { Color = new ClearColorValue { Float32_0 = 0, Float32_1 = 0, Float32_2 = 0, Float32_3 = 1 } };
                renderPassInfo.ClearValueCount = 1;
                renderPassInfo.PClearValues = &clearColor;

                Api.CmdBeginRenderPass(_commandBuffers[i], &renderPassInfo, SubpassContents.Inline);
                Api.CmdBindPipeline(_commandBuffers[i], PipelineBindPoint.Graphics, _graphicsPipeline);
                Api.CmdDraw(_commandBuffers[i], 3, 1, 0, 0);
                Api.CmdEndRenderPass(_commandBuffers[i]);

                if (Api.EndCommandBuffer(_commandBuffers[i]) != Result.Success)
                {
                    throw new Exception("failed to record command buffer!");
                }
            }
        }

        private unsafe void CreateSyncObjects()
        {
            _imageAvailableSemaphores = new Semaphore[MaxFramesInFlight];
            _renderFinishedSemaphores = new Semaphore[MaxFramesInFlight];
            _inFlightFences = new Fence[MaxFramesInFlight];
            _imagesInFlight = new Fence[MaxFramesInFlight];

            SemaphoreCreateInfo semaphoreInfo = new SemaphoreCreateInfo();
            semaphoreInfo.SType = StructureType.SemaphoreCreateInfo;

            FenceCreateInfo fenceInfo = new FenceCreateInfo();
            fenceInfo.SType = StructureType.FenceCreateInfo;
            fenceInfo.Flags = FenceCreateFlags.FenceCreateSignaledBit;

            for (var i = 0; i < MaxFramesInFlight; i++)
            {
                Semaphore imgAvSema, renderFinSema;
                Fence inFlightFence;
                if (Api.CreateSemaphore(_device, &semaphoreInfo, null, &imgAvSema) != Result.Success ||
                    Api.CreateSemaphore(_device, &semaphoreInfo, null, &renderFinSema) != Result.Success ||
                    Api.CreateFence(_device, &fenceInfo, null, &inFlightFence) != Result.Success)
                {
                    throw new Exception("failed to create synchronization objects for a frame!");
                }

                _imageAvailableSemaphores[i] = imgAvSema;
                _renderFinishedSemaphores[i] = renderFinSema;
                _inFlightFences[i] = inFlightFence;
            }
        }

        private unsafe void DrawFrame(double obj)
        {
            var fence = _inFlightFences[_currentFrame];
            Api.WaitForFences(_device, 1, ref fence, Vk.True, ulong.MaxValue);

            uint imageIndex;
            _vkSwapchain.AcquireNextImage
                (_device, _swapchain, ulong.MaxValue, _imageAvailableSemaphores[_currentFrame], default, &imageIndex);

            if (_imagesInFlight[imageIndex].Handle != 0)
            {
                Api.WaitForFences(_device, 1, ref _imagesInFlight[imageIndex], Vk.True, ulong.MaxValue);
            }

            _imagesInFlight[imageIndex] = _inFlightFences[_currentFrame];

            SubmitInfo submitInfo = new SubmitInfo { SType = StructureType.SubmitInfo };

            Semaphore[] waitSemaphores = { _imageAvailableSemaphores[_currentFrame] };
            PipelineStageFlags[] waitStages = { PipelineStageFlags.PipelineStageColorAttachmentOutputBit };
            submitInfo.WaitSemaphoreCount = 1;
            var signalSemaphore = _renderFinishedSemaphores[_currentFrame];
            fixed (Semaphore* waitSemaphoresPtr = waitSemaphores)
            {
                fixed (PipelineStageFlags* waitStagesPtr = waitStages)
                {
                    submitInfo.PWaitSemaphores = waitSemaphoresPtr;
                    submitInfo.PWaitDstStageMask = waitStagesPtr;

                    submitInfo.CommandBufferCount = 1;
                    var buffer = _commandBuffers[imageIndex];
                    submitInfo.PCommandBuffers = &buffer;

                    submitInfo.SignalSemaphoreCount = 1;
                    submitInfo.PSignalSemaphores = &signalSemaphore;

                    Api.ResetFences(_device, 1, &fence);

                    if (Api.QueueSubmit
                            (_graphicsQueue, 1, &submitInfo, _inFlightFences[_currentFrame]) != Result.Success)
                    {
                        throw new Exception("failed to submit draw command buffer!");
                    }
                }
            }

            fixed (SwapchainKHR* swapchain = &_swapchain)
            {
                PresentInfoKHR presentInfo = new PresentInfoKHR
                {
                    SType = StructureType.PresentInfoKhr,
                    WaitSemaphoreCount = 1,
                    PWaitSemaphores = &signalSemaphore,
                    SwapchainCount = 1,
                    PSwapchains = swapchain,
                    PImageIndices = &imageIndex
                };

                _vkSwapchain.QueuePresent(_presentQueue, &presentInfo);
            }

            _currentFrame = (_currentFrame + 1) % MaxFramesInFlight;
        }
    }
}