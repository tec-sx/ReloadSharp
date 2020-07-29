﻿namespace Reload.Rendering
{
    using System;
    using System.Numerics;

    [Flags]
    public enum MaterialFlag
    {
        None = 0,
        DepthTest = 1 << 1,
        Blend = 1 << 2
    }

    public abstract class Material
    {
        private MaterialFlag _materialFlags;
        public MaterialFlag MaterialFlags
        {
            get => _materialFlags;
            set => _materialFlags |= value;
        }

        public Material(ShaderProgram shader)
        {

        }

        public void Bind()
        {

        }

        public void Set<T>(string name, T value)
        {

        }

        public class MaterialInstance
        {

        }
    }
}