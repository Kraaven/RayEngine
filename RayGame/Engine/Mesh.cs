using System.Numerics;

namespace RayGame;

public class Mesh
{
    private List<Vector2> Vertices = new();

    public Mesh((float, float)[] vertexArray)
    {
        AddVertices(vertexArray);
    }
    public Mesh(Vector2[] vertexArray)
    {
        AddVertices(vertexArray);
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
    
    public Mesh ShiftMesh(Vector2 Offset)
    {
        for (int i = 0; i < Vertices.Count; i++)
        {
            Vertices[i] += Offset;
        }

        return this;
    }
    public Mesh ShiftMesh((float,float) Offset)
    {
        for (int i = 0; i < Vertices.Count; i++)
        {
            Vertices[i] += new Vector2(Offset.Item1, Offset.Item2);

        }

        return this;
    }
    public Mesh RotateMesh(float Angle)
    {
        var Rotation = MathF.PI * (Angle / 180);

        for (int i = 0; i < Vertices.Count; i++)
        {
            var x = MathF.Cos(Rotation) * Vertices[i].X - MathF.Sin(Rotation) * Vertices[i].Y;
            var y = MathF.Sin(Rotation) * Vertices[i].X + MathF.Cos(Rotation) * Vertices[i].Y;

            Vertices[i] = new Vector2(x, y);
        }

        return this;
    }
    public Mesh ScaleMesh(float Scale)
    {
        for (int i = 0; i < Vertices.Count; i++)
        {
            Vertices[i] *= Scale;

        }

        return this;
    }

    public Vector2[] GetVertexArray()
    {
        return Vertices.ToArray();
    }

    public void DeleteVertex(int Index)
    {
        Vertices.RemoveAt(Index);
    }

    public void DeleteVertex((float, float) point)
    {
        var V = new Vector2(point.Item1, point.Item2);
        if (Vertices.Contains(V))
        {
            Vertices.Remove(V);
        }
    }

    public void InsertVertex(int Index, Vector2 Vertex)
    {
        Vertices.Insert(Index, Vertex);
    }


}