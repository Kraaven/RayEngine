using System.ComponentModel;
using System.Numerics;
using Raylib_cs;

namespace RayGame;

public class GameObject
{
    public string Name;
    public Vector2 Position;
    private float Rotation;
    public List<Vector2> Vertices;
    public float Scale;
    private Guid ID = Guid.NewGuid();
    private List<IGameComponent> GameComponents = new List<IGameComponent>();
    // private List<Collider> ObjectColliders;

    void InitValues()
    {
        Position = new Vector2();
        Rotation = 0;
        Scale = 1;
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
    public void AddVertices((float, float)[] vertexArray)
    {
        foreach (var V in vertexArray)
        {
            Vertices.Add(new Vector2(V.Item1, V.Item2));
        }
    }
    public void AddVertices(Vector2 vertex)
    {
        Vertices.Add(vertex);
    }
    public void AddVertices((float, float) vertex)
    {
        Vertices.Add(new Vector2(vertex.Item1, vertex.Item2));
    }
    public void Translate(Vector2 offset)
    {
        Position += offset;
    }
    public void Translate((float, float) offset)
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

        var RenderVertices = ProcessVertices(Vertices);
        
        if (RenderVertices.Count > 0)
        {
            Raylib.DrawLineV(RenderVertices[^1], RenderVertices[0], Color.Black);
            for (int i = 0; i < RenderVertices.Count - 1; i++)
            {
                Raylib.DrawLineV(RenderVertices[i], RenderVertices[i + 1], Color.Black);
            }
        }
    }
    private Vector2 ProcessPoint(Vector2 Vertex)
    {
        var x = MathF.Cos(Rotation) * Vertex.X - MathF.Sin(Rotation) * Vertex.Y;
        var y = MathF.Sin(Rotation) * Vertex.X + MathF.Cos(Rotation) * Vertex.Y;

        Vector2 result = new Vector2(x, y);
        result *= Scale;
        result.X += Position.X;
        result.Y += Position.Y;

        return result;
    }

    public List<Vector2> ProcessVertices(List<Vector2> LocalVertices)
    {
        var ProcessedVertices = new List<Vector2>();
        foreach (var localVertex in LocalVertices)
        {
            ProcessedVertices.Add(ProcessPoint(localVertex));
        }

        return ProcessedVertices;
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
    public T AddComponent<T>() where T : IGameComponent, new()
    {
        if (GameComponents.Any(item => item is T))
        {
            return GetComponent<T>();
        }
        else
        {
            T gameComponent = new T();
            GameComponents.Add(gameComponent);
            gameComponent.Container = this;
            GameComponents.Last().Start();
            return gameComponent;
        }

    }
    public float GetRotation()
    {
        return Rotation * (180 / MathF.PI);
    }
    public void SetRotation(float angle)
    {
        Rotation = MathF.PI * (angle / 180);
    }
    public T GetComponent<T>()
    {
        return GameComponents.OfType<T>().FirstOrDefault();
    }
    public void DeleteComponent<T>() where T : IGameComponent
    {
        GameComponents.Remove(GetComponent<T>());
    }
    public void DeleteObjectComponents()
    {
        GameComponents.Clear();
    }
    public List<string> GetComponentNameList(bool Print)
    {
        List<string> Components = new();
        foreach (var gameComponent in GameComponents)
        {
            Components.Add(gameComponent.GetType().ToString());
        }

        if (Print)
        {
            Console.WriteLine(string.Join(", ", Components.ToArray()));
        }

        return Components;
    }
    public void ShiftComponent<T>(int offset) where T : IGameComponent
    {
        int currentIndex = GameComponents.FindIndex(component => component is T);
        if (currentIndex == -1)
        {
            throw new ArgumentException("Component of the specified type not found in the list.");
        }

        int newIndex = (currentIndex + offset) % GameComponents.Count;
        if (newIndex < 0)
        {
            newIndex += GameComponents.Count;
        }

        T component = (T)GameComponents[currentIndex];
        GameComponents.RemoveAt(currentIndex);
        GameComponents.Insert(newIndex, component);
    }
    public bool HasComponent<T>() where T : IGameComponent
    {
        foreach (var gameComponent in GameComponents)
        {
            if (gameComponent is T)
            {
                return true;
            }
        }

        return false;
    }
}
