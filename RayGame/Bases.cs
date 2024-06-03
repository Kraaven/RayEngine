#region

using System.Numerics;

#endregion

namespace RayGame;


/// <summary>
/// Interface for game components.
/// </summary>
/// <remarks>
/// Implement this interface to create custom game components. Each <see cref="GameObject"/> has a List of Components that it runs by itself.
/// Hence, any class that implements <see cref="IGameComponent"/>, will be eligible to perform as a script attached to the Object.
/// By This, It is possible to create *Prefabs* buy creating 1 class component that adds all the required components and modifications.
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
/// This is interface is to be inherited by all Renderers, If you wish to make a custom renderer, then u can,
/// but i would recommend sticking to the provided renderers. **Primarily for internal use**
/// </remarks>>
public interface IRenderer
{
    /// <summary>
    /// The container GameObject for this renderer.
    /// Any Actions that want to be performed to it, is does through this reference. 
    /// </summary>
    public GameObject Container { set; get; }

    /// <summary>
    /// The Function Called when the Renderer is Added to an Object.
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    /// Called every frame to update the renderer. Primarily holds code to render the content.
    /// </summary>
    public void Update()
    {
    }
}

/// <summary>
/// The class that holds all the transformation data for an object 
/// </summary>
/// <remarks>
/// A Class that is used to represent the Position, Rotation and Scale of an object.
/// This class super-imposes its properties onto vertices per frame, which means that any object containing this class can enact global transformations within that <see cref="GameObject"/>.
/// By default, any transformations does on the object should be done through its Transform.
/// </remarks>>
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
    /// Position being (0,0). Rotation being 0. Scale being 1.
    /// </summary>
    public Transform()
    {
        Position = new Vector2(0, 0);
        Rotation = 0;
        Scale = 1;
    }

    /// <summary>
    /// Rotates the transform by the specified angle.
    /// Takes an Angle in degrees, and stores them internally as radians.
    /// </summary>
    /// <param name="Angle">The angle in degrees.</param>
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
    /// Takes an Angle in degrees, and stores them internally as radians.
    /// </summary>
    /// <param name="Angle">The angle in degrees.</param>
    public void SetRotation(float Angle)
    {
        Rotation = MathF.PI * (Angle / 180);
    }

    /// <summary>
    /// Gets the rotation of the transform.
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
    /// Applies that transforms onto the Vertices of a <see cref="Mesh"/>'s Vertex Array. Primarily used internally in the Engine.
    /// </summary>
    /// <param name="VertexArray">The array of vertices to transform.</param>
    /// <returns>The transformed array of vertices.</returns>
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