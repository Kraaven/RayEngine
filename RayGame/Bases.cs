#region

using System.Numerics;

#endregion

namespace RayGame;


/// <summary>
/// Interface for game components.
/// </summary>
/// <remarks>
/// Implement this interface to create custom game components.
/// </remarks>
public interface IGameComponent
{
    
    /// <summary>
    /// The container GameObject for this component.
    /// </summary>
    public GameObject Container { get; set; }

    /// <summary>
    /// Called when the component is Added.
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    /// Called every frame to update the component.
    /// </summary>
    public void Update()
    {
    }
}

/// <summary>
/// Interface for renderers.
/// </summary>
/// <remarks>
/// Implement this interface to create custom renderers.
/// </remarks>
public interface IRenderer
{
    /// <summary>
    /// The container GameObject for this renderer.
    /// </summary>
    public GameObject Container { set; get; }

    /// <summary>
    /// Called when the renderer is Added.
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    /// Called every frame to update the renderer.
    /// </summary>
    public void Update()
    {
    }
}

/// <summary>
/// Represents a transformation in 2D space.
/// </summary>
/// <remarks>
/// Contains position, rotation, and scale information, Contains useful functions to use the Transform.
/// </remarks>
public class Transform
{
    /// <summary>
    /// The position of the transform as a <see cref="Vector2"/>.
    /// </summary>
    public Vector2 Position;
    /// <summary>
    /// The rotation of the transform in radians.
    /// </summary>
    private float Rotation;
    /// <summary>
    /// The Scale of the transform as a Float.
    /// </summary>
    public float Scale;

    /// <summary>
    /// Initializes a new instance of the <see cref="Transform"/> class with specified position, rotation, and scale.
    /// </summary>
    /// <param name="pos">The position of the transform.</param>
    /// <param name="ang">The rotation of the transform in degrees.</param>
    /// <param name="sc">The scale of the transform.</param>
    public Transform(Vector2 pos, float ang, float sc)
    {
        Position = pos;
        SetRotation(ang);
        Scale = sc;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Transform"/> class with default values.
    /// </summary>
    /// <remarks>
    /// Position being (0,0). Rotation being 0. Scale being 1.
    /// </remarks>>
    public Transform()
    {
        Position = new Vector2(0, 0);
        Rotation = 0;
        Scale = 1;
    }

    /// <summary>
    /// Rotates the transform by the specified angle.
    /// </summary>
    /// <param name="Angle">The angle in degrees.</param>
    /// <remarks>
    /// Takes an Angle in degrees, and stores them internally as radians.
    /// </remarks>>
    public void Rotate(float Angle)
    {
        Rotation += MathF.PI * (Angle / 180);
    }

    /// <summary>
    /// Translates the transform by the specified offset.
    /// </summary>
    /// <param name="Offset">The offset as a tuple(<see cref="float"/>, <see cref="float"/>)</param>
    public void Translate((float, float) Offset)
    {
        Position.X += Offset.Item1;
        Position.Y += Offset.Item2;
    }

    /// <summary>
    /// Translates the transform by the specified offset.
    /// </summary>
    /// <param name="Offset">The offset as a <see cref="Vector2"/>.</param>
    public void Translate(Vector2 Offset)
    {
        Position += Offset;
    }

    /// <summary>
    /// Sets the rotation of the transform to the specified angle.
    /// </summary>
    /// <param name="Angle">The angle in degrees.</param>
    /// <remarks>
    /// Takes an Angle in degrees, and stores them internally as radians.
    /// </remarks>>
    public void SetRotation(float Angle)
    {
        Rotation = MathF.PI * (Angle / 180);
    }

    /// <summary>
    /// Gets the rotation of the transform in radians.
    /// </summary>
    /// <returns>The rotation in returned as Degrees.</returns>
    /// <returns>
    /// <see cref="float"/>
    /// </returns>>
    public float GetRotation()
    {
        return Rotation *(180/MathF.PI);
    }

    /// <summary>
    /// Applies the transform to an array of vertices.
    /// </summary>
    /// <param name="VertexArray">The array of vertices to transform.</param>
    /// <returns>The transformed array of vertices.</returns>
    /// <remarks>
    /// Applies that transforms onto the Vertices of a <see cref="Mesh"/>'s Vertex Array. Primarily used internally in the Engine.
    /// </remarks>>
    public Vector2[] ApplyTransform(Vector2[] VertexArray)
    {
        for (var i = 0; i < VertexArray.Length; i++)
        {
            var x = MathF.Cos(Rotation) * VertexArray[i].X - MathF.Sin(Rotation) * VertexArray[i].Y;
            var y = MathF.Sin(Rotation) * VertexArray[i].X + MathF.Cos(Rotation) * VertexArray[i].Y;
            var result = new Vector2(x, y);
            result *= Scale;
            result.X += Position.X;
            result.Y += Position.Y;

            VertexArray[i] = result;
        }

        return VertexArray;
    }
}