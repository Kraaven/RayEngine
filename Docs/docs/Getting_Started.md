# Hello World!

## Note:
Now that we finally have our project setup, lets begin writing some code!.
Now, we would need 2 scripts to start with. A Entry script, and a Component to start everything else.
Create a new folder if you want, organising files will be really helpful in the long run.

## The Entry script
- When using this engine, we need some entry point to tell the Engine to start, thats what this script is for.
- So lets make a script called Entry.cs
- When using the engine, make sure to include:
```csharp
using RayGame;
```
- This is the Engine code that we have imported to start making something.
- After that, lets create a very standard C# script:
```csharp
using RayGame;
using [YOUR_NAMESPACE];;

public class Entry
{
    public static void Main()
    {
        
        Console.WriteLine("Hello World");
        //Note, the file does not HAVE to called Entry.
    }
}
```
- Lovely, we now have a script that we can use as an entry, from here we can initiate the Engine to start doing what we want it to do.
- Via the Engine Class, we Run an INIT function to attach a Component of choice into a prebuilt GameObject that we then use to manipulate the Engine to do what we want.
    - Although that may seem complicated, dont worry, it will be explained.
```csharp
Engine.INIT<T>();
```
- As you can see, the INIT function requires some class to replace its Template field. That class is going to be our Custom Component.
- In the next section, we will talk about Components, and finally get something to show!