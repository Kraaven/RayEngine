using System.Numerics;
using Raylib_cs;

namespace RayGame;

public class GameObject
{
    public Vector2 Position;
    public float Rotation;

    void InitValues()
    {
        Position = new Vector2();
        Rotation = new float();
    }

    public GameObject()
    {
        InitValues();
    }

    public void RenderObject()
    {
        Raylib.DrawLineV(Position,Position+(Vector2.One*25),Color.Black);
    }
}