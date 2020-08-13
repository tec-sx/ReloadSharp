using Reload.Configuration;
using Reload.Rendering.Model;
using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Gltf = SharpGLTF.Schema2;
using Reload3D = Reload.Core.Math3D.Vertices;

namespace Reload.AssetSystem.Loaders
{
    public static class MeshLoader
    {
        public static Rendering.Model.Mesh LoadFromFile(string fileName)
        {
            string fullPath = Path.Combine(ContentPaths.Models, fileName);

            ModelRoot modelRoot = Gltf::ModelRoot.Load(fullPath);

            var indicesList = new List<Reload3D::Index>();
            var vertices = new List<Reload3D::Vertex>();
            var subMeshes = new List<SubMesh>();

            uint vertexCount = 0;
            uint indexCount = 0;

            foreach (Gltf::Mesh mesh in modelRoot.LogicalMeshes)
            {
                foreach (MeshPrimitive primitive in mesh.Primitives)
                {
                    var indices = primitive.GetIndices().ToList();
                    // ?
                    MeshPrimitive accessors = primitive.WithIndicesAutomatic(PrimitiveType.TRIANGLES);

                    foreach (var (name, accessor) in accessors.VertexAccessors)
                    {
                        Console.WriteLine(name + ": " + accessor.Count);
                        switch (accessor.Dimensions)
                        {
                            case DimensionType.SCALAR:
                                break;
                            case DimensionType.VEC2:
                                {
                                    var list = accessor.AsVector2Array();
                                    for (var i = 0; i < list.Count; i++)
                                    {
                                        var vector2 = list[i];

                                        if (vertices.Count < i)
                                        {
                                            vertices.Add(new Vertex());
                                        }

                                        if (name == "TEXCOORD_0")
                                        {
                                            vertices[i].TexCoord = new Vector2F(vector2.X, vector2.Y);
                                            var c = primitive.Material.Channels.First().Parameter;

                                            //vertexes[i].Color = new Vector3F(c.X, c.Y, c.Z);
                                        }
                                    }

                                    break;
                                }
                            case DimensionType.VEC3:
                                {
                                    var list = accessor.AsVector3Array();
                                    for (var i = 0; i < list.Count; i++)
                                    {
                                        var vector3 = list[i];

                                        if (vertices.Count <= i)
                                        {
                                            vertices.Add(new Vertex());
                                        }

                                        if (name == "POSITION")
                                        {
                                            vertices[i].Position = new Vector3F(vector3.X, vector3.Y, vector3.Z);
                                        }

                                        if (name == "NORMAL")
                                        {
                                            vertices[i].Normal = new Vector3F(vector3.X, vector3.Y, vector3.Z);
                                        }

                                        if (name == "TANGENT")
                                        {
                                            vertices[i].Normal = new Vector3F(vector3.X, vector3.Y, vector3.Z);
                                        }
                                    }
                                }
                                break;
                            case DimensionType.VEC4:
                                {
                                    var list = accessor.AsVector4Array();
                                    for (var i = 0; i < list.Count; i++)
                                    {
                                        var vector4 = list[i];

                                        if (vertices.Count <= i)
                                        {
                                            vertices.Add(new Vertex());
                                        }


                                        if (name == "JOINTS_0")
                                        {
                                            var jts = new List<int>();

                                            foreach (var skin in model.LogicalSkins)
                                            {
                                                foreach (var f in skin.GetType().GetRuntimeFields())
                                                {
                                                    if (f.Name == "_joints")
                                                    {
                                                        jts = (List<int>)f.GetValue(skin);

                                                        /*File.WriteAllLines("lol.txt",
                                                            jts.ConvertAll<string>((x) => x.ToString())
                                                                .ToArray());*/
                                                        break;
                                                    }
                                                }
                                            }

                                            var c = Color.FromArgb(
                                                int.Parse(ColourValues[jts[(int)vector4.X] + 1],
                                                    System.Globalization.NumberStyles.HexNumber));

                                            /*c = Color.FromArgb(
                                               int.Parse(
                                                   ColourValues[(int) vector4.X],
                                                   System.Globalization.NumberStyles.HexNumber));*/
                                            var j = model.LogicalNodes[(int)vector4.X];


                                            // if (JointsNames.IndexOf("Head") == (int) vector4.X)
                                            {
                                                vertices[i].Color = new Vector3F(
                                                    c.R / 255f,
                                                    c.G / 255f,
                                                    c.B / 255f
                                                );
                                            }

                                            /*
                                            Console.WriteLine(
                                                $"[Loader] Mapping: {model.LogicalNodes[(int) vector4.X].Name} to {JointsNames.IndexOf(model.LogicalNodes[(int) vector4.X].Name)}");
                                                */

                                            vertices[i].JointIDS = new Vector3I(
                                                jts[(int)vector4.X],
                                                jts[(int)vector4.Y],
                                                jts[(int)vector4.Z]
                                            );
                                        }

                                        if (name == "WEIGHTS_0")
                                        {
                                            vertices[i].JointWeights = new Vector3F(vector4.X, vector4.Y, vector4.Z);
                                            //vertexes[i].JointWeights = new Vector3F(0, vector4.Y,  0);

                                            /*vertexes[i].Color.X *= vector4.X;
                                            vertexes[i].Color.Y *= vector4.Y;
                                            vertexes[i].Color.Z *= vector4.Z;*/

                                            /*if (vertexes[i].JointIDS.X == 0 && vector4.X > 0)
                                                Console.WriteLine("index zero id: " + JointsNames[vertexes[i].JointIDS.X]);
                                            if (vertexes[i].JointIDS.Y == 0 && vector4.X > 0)
                                                Console.WriteLine("index zero id: " + JointsNames[vertexes[i].JointIDS.Y]);
                                            if (vertexes[i].JointIDS.Z == 0 && vector4.X > 0)
                                                Console.WriteLine("index zero id: " + JointsNames[vertexes[i].JointIDS.Z]);*/
                                        }
                                    }
                                }
                                break;
                            case DimensionType.MAT2:
                                break;
                            case DimensionType.MAT3:
                                break;
                            case DimensionType.MAT4:
                                break;
                        }
                    }
                }
            }

            //subMeshes.Add(new SubMesh
            //{
            //    BaseVertex = vertexCount,
            //    BaseIndex = indexCount,
            //    MaterialIndex = mesh.Primitives.M
            //});
        }

            return null;
        }
    }
}
