#region

using System.Numerics;
using Raylib_cs;

#endregion

namespace RayGame;

public class MeshRenderer : IRenderer
{
    private Mesh mesh;
    public GameObject Container { get; set; }

    public void Start()
    {
    }

    public void Update()
    {
        var Vertices = Container.Transform.ApplyTransform(mesh.GetVertexArray());
        if (Vertices.Length > 0) RenderMesh(Vertices, Color.Black);
    }

    public Mesh SetMesh(Mesh mesh)
    {
        this.mesh = mesh;
        return mesh;
    }

    public Mesh GetMesh()
    {
        return mesh;
    }

    public static void RenderMesh(Vector2[] Vertices, Color color)
    {
        Raylib.DrawLineV(Vertices[^1], Vertices[0], color);
        for (var i = 0; i < Vertices.Length - 1; i++) Raylib.DrawLineV(Vertices[i], Vertices[i + 1], color);
    }
}