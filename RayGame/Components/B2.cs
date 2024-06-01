﻿using System.ComponentModel;
using System.Numerics;

namespace RayGame;

public class B2 : IGameComponent
{
    public GameObject Container { get; set; }

    private float RotationSpeed;
    
    public void Start()
    {
        Container.AddRenderer<MeshRenderer>().SetMesh(new Mesh(new []{(50f,50f),(-50f,50f),(-50f,-50f),(50f,-50f)}));
        Container.Transform = new Transform(new Vector2(Engine.random.Next() % 800, Engine.random.Next() % 500),
            Engine.random.Next() % 360, 0.5f);
        Container.Colliders.Add(new Mesh(new []{(50f,50f),(-50f,50f),(-50f,-50f),(50f,-50f)}));
        
        RotationSpeed = Engine.random.Next() % 5f;
        RotationSpeed += 1.2f;
        RotationSpeed = (int)RotationSpeed;
        
        
    }


    

    public void Update()
    {
        Container.Transform.Translate(MoveTowardsTarget(Container.Transform.Position,(400,240),1.5f));

        Container.Transform.Rotate(RotationSpeed);
        
    }

    private static Vector2 MoveTowardsTarget(Vector2 position, (float, float) target, float speed)
    {
        Vector2 direction = Vector2.Subtract(new Vector2(target.Item1,target.Item2), position);
        Vector2 normalizedDirection = Vector2.Normalize(direction);
        Vector2 translation = Vector2.Multiply(normalizedDirection, speed);
        return translation;
    }
}