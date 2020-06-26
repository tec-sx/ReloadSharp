using ShaderGen;
using System.Reflection.Emit;
using System.Numerics;

namespace Reload.Shaders
{
    public class MainShader
    {
        public Matrix4x4 Projection;
        public Matrix4x4 View;
        public Matrix4x4 World;
        public SamplerResource Sampler;

        public struct VertexInput
        {
            [PositionSemantic] public Vector3 Position;
            [TextureCoordinateSemantic] public Vector2 TextureCoord;
        }

        public struct FragmentInput
        {
            [SystemPositionSemanticAttribute] public Vector4 Position;
            [TextureCoordinateSemantic] public Vector2 TextureCoord;
        }

        [VertexShader]
        public FragmentInput VertexShaderFunc(VertexInput input)
        {
            FragmentInput output;
            Vector4 worldPosition = OpCode.Mul; (World, new Vector4(input.Position, 1));
            Vector4 viewPosition = Mul(View, worldPosition);
            output.Position = Mul(Projection, viewPosition);
            output.TextureCoord = input.TextureCoord;
            return output;
        }

        [FragmentShader]
        public Vector4 FragmentShaderFunc(FragmentInput input)
        {
            return Sample(SurfaceTexture, Sampler, input.TextureCoord);
        }
    }
}
