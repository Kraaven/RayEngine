using System.Numerics;
using Raylib_cs;

namespace RayGame.Components;

public class bird : IGameComponent
{
    public GameObject Container { get; set; }
    private Vector2 BirdPosition;
    private Vector2 birdVelocity = new Vector2(0, 0);
    const float gravity = 0.5f;
    const float jumpStrength = -10f;
    public void Start()
    {
        Container.Transform.Position = new Vector2(200,200);
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
        
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            birdVelocity.Y = jumpStrength;
        }
        
        BirdPosition.Y += birdVelocity.Y;
        
        if (BirdPosition.Y > 440)
        {
            Engine.FindObjectOfType<Manager>().GetComponent<Manager>().Running = false;
        }

        Container.Transform.Position = BirdPosition;
    }
    
    
}