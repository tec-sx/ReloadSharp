#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
using Reload.Core.Graphics.Rendering.Buffers;
using Reload.Core.Graphics.Rendering.Structures;
using System.Collections.Generic;
using System.Numerics;

namespace Reload.Core.Models
{
    public abstract class MeshBase
    {
        private List<BoneInfo> _bonesInfo;

        private VertexArray _vertexArray;

        private List<Vertex> _staticVertices;

        private List<AnimatedVertex> _animatedVertices;

        private List<Index> _indices;

        private Dictionary<string, uint> _boneMappings;

        private List<Matrix4x4> _boneTransforms;

        private bool _isAnimated;

        private float _animationTime;

        private float _worldTime;

        private float _timeMultiplier;

        private bool _animationPlaying;
    }
}
