using System.Numerics;

namespace RayGame;

public interface IGameComponent
{
    public GameObject Container { get; set;}

    public void Start()
    {
    }

    public void Update()
    {
    }
}
public interface IRenderer
{
    public GameObject Container { set; get; }
    public void Start() { }
    public void Update() { }
}
public class Transform
{
    public Vector2 Position;
    private float Rotation;
    public float Scale;

    public Transform(Vector2 pos, float ang, float sc)
    {
        Position = pos;
        SetRotation(ang);
        Scale = sc;
    }

    public Transform()
    {
        Position = new Vector2(0, 0);
        Rotation = 0;
        Scale = 1;
    }

    public void Rotate(float Angle)
    {
        Rotation += MathF.PI * (Angle / 180);
    }

    public void Translate((float, float) Offset)
    {
        Position.X += Offset.Item1;
        Position.Y += Offset.Item2;
    }

    public void Translate(Vector2 Offset)
    {
        Position += Offset;
    }

    public void SetRotation(float Angle)
    {
        Rotation = MathF.PI * (Angle / 180);
    }
    public float GetRotation()
    {
        return Rotation;
    }
    public Vector2[] ApplyTransform(Vector2[] VertexArray)
    {
        for (int i = 0; i < VertexArray.Length; i++)
        {
            var x = MathF.Cos(Rotation) * VertexArray[i].X - MathF.Sin(Rotation) * VertexArray[i].Y;
            var y = MathF.Sin(Rotation) * VertexArray[i].X + MathF.Cos(Rotation) * VertexArray[i].Y;
            Vector2 result = new Vector2(x, y);
            result *= Scale;
            result.X += Position.X;
            result.Y += Position.Y;

            VertexArray[i] = result;
        }

        return VertexArray;
    }
}