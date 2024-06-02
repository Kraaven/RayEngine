#region

using System.Numerics;
using Raylib_cs;

#endregion

namespace RayGame.Demo;

public class bird : IGameComponent
{
    private const float gravity = 0.5f;
    private const float jumpStrength = -10f;
    private Vector2 BirdPosition;
    private Vector2 birdVelocity = new(0, 0);
    public GameObject Container { get; set; }

    public void Start()
    {
        Container.Transform.Position = new Vector2(200, 200);
        Container.Transform.Scale = 2;
        BirdPosition = Container.Transform.Position;
        Container.AddRenderer<MeshRenderer>().SetMesh(new Mesh(new[]
        {
            new Vector2(-17f, 0f),
            new Vector2(-30f, 5f),
            new Vector2(-30f, -5f)
        }));

        Container.Colliders.Add(Container.GetRenderer<MeshRenderer>(0).GetMesh());
    }

    public void Update()
    {
        birdVelocity.Y += gravity;

        if (Raylib.IsKeyPressed(KeyboardKey.Space)) birdVelocity.Y = jumpStrength;

        BirdPosition.Y += birdVelocity.Y;

        if (BirdPosition.Y > 440) Engine.FindObjectOfType<Manager>().GetComponent<Manager>().Running = false;

        Container.Transform.Position = BirdPosition;
    }
}