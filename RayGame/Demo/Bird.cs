#region

using System.Numerics;
using Raylib_cs;

#endregion

namespace RayGame.Demo;

// The Bird Component
public class Bird : IGameComponent
{
    // declaring variables for the input, and gravity
    private const float gravity = 0.5f;
    private const float jumpStrength = -10f;
    private Vector2 BirdPosition;
    private Vector2 birdVelocity = new(0, 0);
    public GameObject Container { get; set; }

    public void Start()
    {
        // customizing the values
        Container.Transform.Position = new Vector2(200, 200);
        Container.Transform.Scale = 2;
        BirdPosition = Container.Transform.Position;
        
        // Creating a triangle for the mesh
        Container.AddRenderer<MeshRenderer>().SetMesh(new Mesh(new[]
        {
            new Vector2(-17f, 0f),
            new Vector2(-30f, 5f),
            new Vector2(-30f, -5f)
        }));

        //Adding the Mesh to the Collider as well
        Container.Colliders.Add(Container.GetRenderer<MeshRenderer>(0).GetMesh());
    }

    // update the birds position to simulate gravity
    public void Update()
    {
        birdVelocity.Y += gravity;

        // Apply a "Force" when the Space Key is pressed
        if (Raylib.IsKeyPressed(KeyboardKey.Space)) birdVelocity.Y = jumpStrength;

        BirdPosition.Y += birdVelocity.Y;

        //If the bird hits the ground, restart the game via the boolean flag
        if (BirdPosition.Y > 440) Engine.FindObjectOfType<Manager>().GetComponent<Manager>().Running = false;

        Container.Transform.Position = BirdPosition;
    }
}