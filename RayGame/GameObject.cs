using System.Numerics;
using Raylib_cs;

namespace RayGame;

public class GameObject
{
    public Vector2 Position;
    public float Rotation;
    public List<Vector2> Vertices;
    public float scale;

    void InitValues()
    {
        Position = new Vector2();
        Rotation = 0;
        scale = 1;
        Vertices = new List<Vector2>();
    }

    public GameObject()
    {
        InitValues();
    }

    public GameObject(Vector2 pos, float angle)
    {
        InitValues();
        Position = pos;
        Rotation = angle;
    }

    public void AddVertices(Vector2[] vertexArray)
    {
        foreach (var V in vertexArray)
        {
            Vertices.Add(V);
        }
    }
    
    public void AddVertices((float,float)[] vertexArray)
    {
        foreach (var V in vertexArray)
        {
            Vertices.Add(new Vector2(V.Item1,V.Item2));
        }
    }
    public void AddVertices(Vector2 vertex)
    {
        Vertices.Add(vertex);
    }
    
    public void AddVertices((float,float) vertex)
    {
        Vertices.Add(new Vector2(vertex.Item1,vertex.Item2));
    }

    public GameObject(float x, float y)
    {
        InitValues();
        Position = new Vector2(x, y);
    }

    public void Translate(Vector2 offset)
    {
        Position += offset;
    }

    public void Translate((float,float) offset)
    {
        Position.X += offset.Item1;
        Position.Y += offset.Item2;
    }

    public void Rotate(float angle)
    {
        Rotation += MathF.PI * (angle / 180);

    }

    public void RenderObject()
    {
        
        Raylib.DrawLineV(ProcessPoint(Vertices[^1]),ProcessPoint(Vertices[0]),Color.Black);
        for (int i = 0; i < Vertices.Count-1; i++)
        {
            Raylib.DrawLineV(ProcessPoint(Vertices[i]),ProcessPoint(Vertices[i+1]),Color.Black); 
        }
    }

    Vector2 ProcessPoint(Vector2 Vertex)
    {
        var x = MathF.Cos(Rotation) * Vertex.X - MathF.Sin(Rotation) * Vertex.Y;
        var y = MathF.Sin(Rotation) * Vertex.X + MathF.Cos(Rotation) * Vertex.Y;
        
        Vector2 result = new Vector2(x, y);
        result *= scale;
        result.X += Position.X;
        result.Y += Position.Y;
        
        return result;
    }
}