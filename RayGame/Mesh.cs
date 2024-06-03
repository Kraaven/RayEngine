#region

using System.Numerics;

#endregion

namespace RayGame;

/// <summary>
/// Represents a 2D mesh consisting of a collection of vertices.
/// </summary>
/// <remarks>
/// A Mesh is a collection of Vertices that is used to display/render anything. It is always a closed loop. The Mesh also have functions that can edit it, however, be careful, as all modifications as *permanent*.
/// It is Attached to a <see cref="MeshRenderer"/> to be viewed at the <see cref="GameObject"/>'s Position. The vertices themselves are always represented in local space.
/// </remarks>
public class Mesh
{
    private List<Vector2> Vertices = new();

    
    /// <summary>
    /// Initializes a new instance of the <see cref="Mesh"/> class with an array of vertices specified as tuples.
    /// </summary>
    /// <param name="vertexArray">An array of tuples representing the vertices of the mesh.</param>
    public Mesh((float, float)[] vertexArray)
    {
        AddVertices(vertexArray);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Mesh"/> class with an array of vertices specified as <see cref="Vector2"/>.
    /// </summary>
    /// <param name="vertexArray">An array of <see cref="Vector2"/> representing the vertices of the mesh.</param>
    public Mesh(Vector2[] vertexArray)
    {
        AddVertices(vertexArray);
    }

    /// <summary>
    /// Adds an array of vertices to the mesh.
    /// </summary>
    /// <param name="vertexArray">An array of <see cref="Vector2"/> to add to the mesh.</param>
    public void AddVertices(Vector2[] vertexArray)
    {
        foreach (var V in vertexArray) Vertices.Add(V);
    }

    /// <summary>
    /// Adds an array of vertices to the mesh specified as tuples.
    /// </summary>
    /// <param name="vertexArray">An array of tuples representing the vertices to add to the mesh.</param>
    public void AddVertices((float, float)[] vertexArray)
    {
        foreach (var V in vertexArray) Vertices.Add(new Vector2(V.Item1, V.Item2));
    }

    /// <summary>
    /// Adds a single vertex to the mesh.
    /// </summary>
    /// <param name="vertex">The vertex to add to the mesh.</param>
    public void AddVertex(Vector2 vertex)
    {
        Vertices.Add(vertex);
    }

    /// <summary>
    /// Adds a single vertex to the mesh specified as a tuple.
    /// </summary>
    /// <param name="vertex">The vertex to add to the mesh.</param>
    public void AddVertex((float, float) vertex)
    {
        Vertices.Add(new Vector2(vertex.Item1, vertex.Item2));
    }

    /// <summary>
    /// Shifts the entire mesh by the specified offset in Position.
    /// </summary>
    /// <param name="Offset">The offset by which to shift the mesh.</param>
    /// <returns>The shifted mesh.</returns>
    public Mesh ShiftMesh(Vector2 Offset)
    {
        for (var i = 0; i < Vertices.Count; i++) Vertices[i] += Offset;

        return this;
    }

    /// <summary>
    /// Shifts the entire mesh by the specified offset in Position.
    /// </summary>
    /// <param name="Offset">The offset specified as a tuple by which to shift the mesh.</param>
    /// <returns>The shifted mesh.</returns>
    public Mesh ShiftMesh((float, float) Offset)
    {
        for (var i = 0; i < Vertices.Count; i++) Vertices[i] += new Vector2(Offset.Item1, Offset.Item2);

        return this;
    }

    /// <summary>
    /// Rotates the entire mesh by the specified angle.
    /// </summary>
    /// <param name="Angle">The angle in degrees by which to rotate the mesh.</param>
    /// <returns>The rotated mesh.</returns>
    public Mesh RotateMesh(float Angle)
    {
        var Rotation = MathF.PI * (Angle / 180);

        for (var i = 0; i < Vertices.Count; i++)
        {
            var x = MathF.Cos(Rotation) * Vertices[i].X - MathF.Sin(Rotation) * Vertices[i].Y;
            var y = MathF.Sin(Rotation) * Vertices[i].X + MathF.Cos(Rotation) * Vertices[i].Y;

            Vertices[i] = new Vector2(x, y);
        }

        return this;
    }

    /// <summary>
    /// Scales the entire mesh by the specified scale factor.
    /// </summary>
    /// <param name="Scale">The scale factor by which to scale the mesh.</param>
    /// <returns>The scaled mesh.</returns>
    public Mesh ScaleMesh(float Scale)
    {
        for (var i = 0; i < Vertices.Count; i++) Vertices[i] *= Scale;

        return this;
    }

    /// <summary>
    /// Gets the vertices of the mesh as an array.
    /// </summary>
    /// <returns>An array of <see cref="Vector2"/> representing the vertices of the mesh.</returns>
    public Vector2[] GetVertexArray()
    {
        return Vertices.ToArray();
    }

    /// <summary>
    /// Deletes a vertex at the specified index.
    /// </summary>
    /// <param name="Index">The index of the vertex to delete.</param>
    public void DeleteVertex(int Index)
    {
        Vertices.RemoveAt(Index);
    }

    /// <summary>
    /// Deletes a Vertex from the last position.
    /// </summary>
    public void DeleteLastVertex()
    {
        Vertices.RemoveAt(Vertices.Count-1);
    }

    /// <summary>
    /// Deletes a vertex that matches the specified point.
    /// </summary>
    /// <param name="point">The point representing the vertex to delete.</param>
    public void DeleteVertex((float, float) point)
    {
        var V = new Vector2(point.Item1, point.Item2);
        if (Vertices.Contains(V)) Vertices.Remove(V);
    }

    /// <summary>
    /// Inserts a vertex at the specified index.
    /// </summary>
    /// <param name="Index">The index at which to insert the vertex.</param>
    /// <param name="Vertex">The vertex to insert.</param>
    public void InsertVertex(int Index, Vector2 Vertex)
    {
        Vertices.Insert(Index, Vertex);
    }
}