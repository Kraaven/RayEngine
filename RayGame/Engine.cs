#region

using System.Numerics;
using Raylib_cs;

#endregion

namespace RayGame;


/// <summary>
/// The main engine class that initializes and runs the game.
/// </summary>
public static class Engine
{
    private static List<GameObject> GameObjectList = new();
    
    /// <summary>
    /// The Random Number Generator initiated with the Engine.
    /// </summary>
    public static Random random = new();

    private static int updateCount;
    private static double TIME { set; get; }
    
    /// <summary>
    /// The value associated with the amount of milliseconds passed.
    /// </summary>
    public static long GAMETIME { private set; get; }

    /// <summary>
    /// Initializes the game engine with a specified game component. That Custom Component is the entry point into using the Engine.
    /// </summary>
    /// /// <typeparam name="T">The type of game component to initialize.</typeparam>
    public static void INIT<T>() where T : IGameComponent, new()
    {
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(800, 480, "Hello World");

        var M = CreateGameObject("Manager");
        M.AddComponent<T>();

        foreach (var gameObject in GameObjectList)
            if (!gameObject.HasComponent<T>())
                gameObject.StartActions();

        while (!Raylib.WindowShouldClose())
        {
            double deltaTime = Raylib.GetFrameTime();
            TIME += deltaTime;

            if (TIME >= 0.01f)
            {
                updateCount++;
                TIME -= 0.01f;
                GAMETIME = (long)(TIME * 1000);
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            foreach (var OBJECT in GameObjectList.ToArray()) OBJECT.UpdateActions();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    
    /// <summary>
    /// Creates a new game object with the specified name.
    /// </summary>
    /// <param name="name">The name of the game object.</param>
    /// <returns>The created game object.</returns>
    public static GameObject CreateGameObject(string name)
    {
        GameObjectList.Add(new GameObject());
        GameObjectList.Last().Name = name;
        return GameObjectList.Last();
    }

    /// <summary>
    /// Creates a new game object with the specified name and <see cref="Transform"/>>.
    /// </summary>
    /// <param name="name">The name of the game object.</param>
    /// <param name="transform">The transform of the game object.</param>
    /// <returns>The created game object.</returns>
    public static GameObject CreateGameObject(string name, Transform transform)
    {
        GameObjectList.Add(new GameObject());
        GameObjectList.Last().Name = name;
        GameObjectList.Last().Transform = transform;
        return GameObjectList.Last();
    }

    /// <summary>
    /// Creates a new game object with the specified name, position, angle, and scale.
    /// </summary>
    /// <param name="name">The name of the game object.</param>
    /// <param name="Position">The position of the game object.</param>
    /// <param name="Angle">The angle of the game object.</param>
    /// <param name="Scale">The scale of the game object.</param>
    /// <returns>The created game object.</returns>
    public static GameObject CreateGameObject(string name, Vector2 Position, float Angle, float Scale)
    {
        GameObjectList.Add(new GameObject());
        GameObjectList.Last().Name = name;
        GameObjectList.Last().Transform = new Transform(Position, Angle, Scale);
        return GameObjectList.Last();
    }

    /// <summary>
    /// Deletes the specified game object.
    /// </summary>
    /// <param name="Gobj">The game object to delete.</param>
    public static void DeleteGameObject(GameObject Gobj)
    {
        if (GameObjectList.Contains(Gobj))
        {
            Gobj.DeleteObjectComponents();
            GameObjectList.Remove(Gobj);
        }
    }

    /// <summary>
    /// Finds the first game object that has a component of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of component to look for.</typeparam>
    /// <returns>The game object with the specified component, or null if not found.</returns>
    public static GameObject FindObjectOfType<T>()
    {
        var Type = typeof(T).ToString();
        Console.WriteLine(Type);
        foreach (var gameObject in GameObjectList)
        {
            var types = gameObject.GetComponentNameList(false);
            if (types.Contains(Type)) return gameObject;
        }

        return null;
    }

    /// <summary>
    /// Finds a game object by its name.
    /// </summary>
    /// <param name="name">The name of the game object.</param>
    /// <returns>The game object with the specified name, or null if not found.</returns>
    public static GameObject FindObjectByName(string name)
    {
        return GameObjectList.FirstOrDefault(obj => obj.Name == name);
    }


    /// <summary>
    /// Enables rendering of colliders for all game objects.
    /// Look at <see cref="GameObject"/> for more detail.
    /// </summary>
    public static void EnableColliderRendering()
    {
        foreach (var gameObject in GameObjectList) gameObject.DEBUGCOLIDERS = true;
    }

    /// <summary>
    /// Disables rendering of colliders for all game objects.
    /// Look at <see cref="GameObject"/> for more detail.
    /// </summary>
    public static void DisableColliderRendering()
    {
        foreach (var gameObject in GameObjectList) gameObject.DEBUGCOLIDERS = false;
    }

    /// <summary>
    /// Gets the count of game objects currently in the engine.
    /// </summary>
    /// <returns>The number of game objects.</returns>
    public static int GetGameObjectCount()
    {
        return GameObjectList.Count;
    }
}