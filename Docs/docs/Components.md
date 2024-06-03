# Logic
Think of Components as scripts that you attach to a GameObject to add logic to what it does. There are no restrictions about how many components a GameObject has, or how many GameObjects have the same component.

However, always keep in mind, that a GameObject can only have 1 type of component at a time, so write your logic accordingly. It is not possible to add the same component to a GameObject multiple times.

## The Component Script
- Lets start talking about components. 
1. There can be only 1 of them on a Gameobject
2. They must implement the "IGameComponent" Interface
3. They Must have a Container variable
4. It must have a Start() and Update() method.

```csharp
using RayGame;
namespace [YOUR_NAMESPACE];

public class Main : IGameComponent
{
    public GameObject Container { get; set; }

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }
}
```

- I have created a Main class to be my component.
- This is what an Empty Component script would look like.
- Notice how it Implements IGameComponent
- If your IDE supports it, then just by typing the name of the variable or method, A suggestion will show up to autocomplete the entire sentence.
- The Container variable is the reference you will use to reffer to your GameObject that component to attached to.
- With that said, lets add some simple code:
```csharp
using RayGame;
namespace [YOUR_NAMESPACE];

public class Main : IGameComponent
{
    public GameObject Container { get; set; }

    public void Start()
    {
        Console.WriteLine("Hello Component!");
    }

    public void Update()
    {
        
    }
}
```
- And with that, we finally have our Component Script done. Lets put this all together!

## Running the Engine
Now we have both an Entry Script, and a Component to use, lets finally have something we can see.
- In our Entry script, lets now use our "Main" Component in the INIT function:
```csharp
using RayGame;
using [YOUR_NAMESPACE];;

public class Entry
{
    public static void Main()
    {
        
        Console.WriteLine("Hello World");
        Engine.INIT<Main>();
    }
}

```
- And with this, your window has opened up and your cosole should be showing:
    > Hello World
    > 
    > Hello Component!