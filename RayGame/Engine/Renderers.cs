using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Raylib_cs;

namespace RayGame;

public class MeshRenderer : IRenderer
{
    public GameObject Container { get; set; }
    private Mesh mesh;

    public Mesh SetMesh(Mesh mesh)
    {
        this.mesh = mesh;
        return mesh;
    }

    public Mesh GetMesh()
    {
        return mesh;
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        var Vertices = Container.Transform.ApplyTransform(mesh.GetVertexArray());
        if (Vertices.Length > 0)
        {
            RenderMesh(Vertices, Color.Black);
        }
    }

    public static void RenderMesh(Vector2[] Vertices, Color color)
    {
        
        Raylib.DrawLineV(Vertices[^1],Vertices[0],color); 
        for (int i = 0; i < Vertices.Length-1; i++)
        {
            Raylib.DrawLineV(Vertices[i],Vertices[i+1],color); 
        } 
        
    }

}