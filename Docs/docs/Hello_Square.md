# A GameObject
- Now that we have all this setup, we can completely forget about our Entry Script.
- We not only consider our Main Class as the point where the Engine Starts.
- Keeping that in Mind, let me walk you through making a simple square, so that you understand how to write logic.

# Creating a GameObject

- Creating a GameObject is pretty simple.
- A lot of your required functions can be found in the Engine Class. So we use that to create a GameObject.
```csharp
var square = Engine.CreateGameObject("My Square");
```
- Here we use the Engine add GameObject to the scene. Most functions that involve GameObjects in the scene will be done through the Engine Class. We dont absolutely need to assign the GameObject to a variable, but if you want to, you can

- After that, we can now set the position of the Object.
```csharp
square.Transform.Position = new Vector2(400, 240);
```
- The GameObject has a transform, which we manipulate the Position property to assign a new position.
- In Raylib, the top left is (0,0), hence, (400,240) would be the middle of the window.
- Despite assigning a position though, you still cannot see anything when you run the project. Thats because tou have not assigned a Renderer to the GameObject.
- Now, GameObjects have a LOT of methods and some properties you can mess around with, but we shall not be covering all of them right now.

## Renderer

- So, the answer seems simple right? Just add a renderer to the Object, and you can see it. But no, there a just a few more steps before you get there.
- Lets start with adding a Renderer.
```csharp
square.AddRenderer<MeshRenderer>();
```
- And that how we add a Mesh Renderer to the GameObject.
```csharp
GAMEOBJECT.AddRenderer<T>();
GAMEOBJECT.AddComponent<T>();
```
- These are the methods that can add Components and Renderers to your GameObject, allowing you to create more complex activity in your scene.
- Now that we have a Renderer, we need to tell it what to render.

### Meshes
- A Mesh is a class that stores vertices of a shape.
- It contains some methods that let you then manipulate the shape it contains.
- All the points of the Mesh are completely local, so you dont need to break your head trying to figure out where you should place each point. (i mean, you still do, but not that much).
```csharp
Mesh sqr = new Mesh(new[] {(10f, 10f), (-10f, 10f), (-10f, -10f), (-10f, -10f)});
```
- I have constructed a Mesh, that takes in an Array of either tuple(float, float), or an array of Vector2. 
- The Choice of types are up to you.

## Putting it all Together
Finally, now that we have eveything setup, lets take a look at the final code:

```csharp
using System.Numerics;
using RayGame;
namespace [YOUR_NAMESPACE];

public class Main : IGameComponent
{
    public GameObject Container { get; set; }

    public void Start()
    {
        Console.WriteLine("Hello Component!");

        var square =Engine.CreateGameObject("My Square");
        square.Transform.Position = new Vector2(400, 240);
        
        Mesh sqr = new Mesh(new[] {(10f, 10f), (-10f, 10f), (-10f, -10f), (10f, -10f)});
        
        MeshRenderer Renderer = square.AddRenderer<MeshRenderer>();
        Renderer.SetMesh(sqr);

    }

    public void Update()
    {
        
    }
}
```
- And Voilà! Your code now generates a square.

## Adding some Features
- Now that we have a square, it seems a bit too small, so lets make the square bigger
- Lets also make the square rotate a little per frame.
```csharp
square.Transform.Scale = 4.5f;
square.Transform.Rotate(2);
```
- I have moved the variable as a global one, to be used in other functions.
- I have called rotate in update, and changed the scale in start.

```csharp
using System.Numerics;
using RayGame;
namespace [YOUR_NAMESPACE];

public class Main : IGameComponent
{
    public GameObject Container { get; set; }
    private GameObject square;

    public void Start()
    {
        Console.WriteLine("Hello Component!");

        square =Engine.CreateGameObject("My Square");
        square.Transform.Position = new Vector2(400, 240);
        
        Mesh sqr = new Mesh(new[] { (10f, 10f), (-10f, 10f), (-10f, -10f), (10f, -10f) });
        
        MeshRenderer Renderer = square.AddRenderer<MeshRenderer>();
        Renderer.SetMesh(sqr);

        square.Transform.Scale = 4.5f;

    }

    public void Update()
    {
        square.Transform.Rotate(2);
    }
}
```