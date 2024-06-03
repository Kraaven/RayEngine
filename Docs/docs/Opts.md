# Optimisations and Efficiency

- Along with what i have shown you, there are other features like collision, that you need to find and learn by yourself.
- You can do that by checking out the API section next to the Docs that you are reading.
- In said API, all Classes and methods have a </> next to it, clicking it will take you to the source code for said Class/ Method.
- The Plethora of methods available should let you do a variety of things, and i am open to adding more if requested.
- That being said, I have made a very basic version of Flappy bird, which you can check out by doing
```csharp
Engine.INIT<RayGame.Demo.Manager>();
```

## New Code
- After refining the code  made earlier, this is what it looks like now:
```csharp
using System.Numerics;
using RayGame;
namespace TestEngine;

public class Main : IGameComponent
{
    public GameObject Container { get; set; }
    private GameObject square;

    public void Start()
    {
        square = Engine.CreateGameObject("My Square",
            new Transform(
                new Vector2(400, 240),
                0,
                4.5f));
        
        square.AddRenderer<MeshRenderer>().SetMesh(
            new Mesh(new[]
            {
                (10f, 10f),
                (-10f, 10f),
                (-10f, -10f),
                (10f, -10f)
            }));
        
        // Code has been expanded with \n so that its more readable.
        
    }

    public void Update()
    {
        square.Transform.Rotate(2);
    }
}
```
- With the Help of different constructor, chaining, and declaring a variable inside a constructor, we have made the same program with less clutter.
- *technically, I could chain the AddRenderer<>() to the CreateGameObject(), but I decided not to*.

- And that marks the END of this tutorial, from here, you can read the docs and try making your own stuff!