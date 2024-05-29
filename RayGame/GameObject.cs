using System.Numerics;
using Raylib_cs;

namespace RayGame;

public class GameObject
{
    public string Name;
    public Vector2 Position;
    public float Rotation;
    public List<Vector2> Vertices;
    public float Scale;
    private List<IGameComponent> GameComponents = new List<IGameComponent>();

    void InitValues()
    {
        Position = new Vector2();
        Rotation = 0;
        Scale = 1;
        Vertices = new List<Vector2>();
    }

    public GameObject()
    {
        InitValues();
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
        if (Vertices.Count > 0)
        {
            Raylib.DrawLineV(ProcessPoint(Vertices[^1]),ProcessPoint(Vertices[0]),Color.Black);
            for (int i = 0; i < Vertices.Count-1; i++)
            {
                Raylib.DrawLineV(ProcessPoint(Vertices[i]),ProcessPoint(Vertices[i+1]),Color.Black); 
            } 
        }
    }
    Vector2 ProcessPoint(Vector2 Vertex)
    {
        var x = MathF.Cos(Rotation) * Vertex.X - MathF.Sin(Rotation) * Vertex.Y;
        var y = MathF.Sin(Rotation) * Vertex.X + MathF.Cos(Rotation) * Vertex.Y;
        
        Vector2 result = new Vector2(x, y);
        result *= Scale;
        result.X += Position.X;
        result.Y += Position.Y;
        
        return result;
    }

    public void StartActions()
    {
        foreach (var gameComponent in GameComponents)
        {
            gameComponent.Start();
        }
    }

    public void UpdateActions()
    {
        foreach (var gameComponent in GameComponents)
        {
            gameComponent.Update();
        }
        RenderObject();
    }

    public IGameComponent AddComponent<T>() where T : IGameComponent
    {
        GameComponents.Add((T)Activator.CreateInstance(typeof(T)));
        GameComponents.Last().Container = this;
        return GameComponents.Last();
    }
}