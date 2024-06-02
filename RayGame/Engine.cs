#region

using System.Numerics;
using Raylib_cs;

#endregion

namespace RayGame;

public static class Engine
{
    public static List<GameObject> GameObjectList = new();
    public static Random random = new();

    private static int updateCount;
    private static double TIME { set; get; }
    public static long GAMETIME { private set; get; }

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

    public static GameObject CreateGameObject(string name)
    {
        GameObjectList.Add(new GameObject());
        GameObjectList.Last().Name = name;
        return GameObjectList.Last();
    }

    public static GameObject CreateGameObject(string name, Transform transform)
    {
        GameObjectList.Add(new GameObject());
        GameObjectList.Last().Name = name;
        GameObjectList.Last().Transform = transform;
        return GameObjectList.Last();
    }

    public static GameObject CreateGameObject(string name, Vector2 Position, float Angle, float Scale)
    {
        GameObjectList.Add(new GameObject());
        GameObjectList.Last().Name = name;
        GameObjectList.Last().Transform = new Transform(Position, Angle, Scale);
        return GameObjectList.Last();
    }

    public static void DeleteGameObject(GameObject Gobj)
    {
        if (GameObjectList.Contains(Gobj))
        {
            Gobj.DeleteObjectComponents();
            GameObjectList.Remove(Gobj);
        }
    }

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

    public static GameObject FindObjectByName(string name)
    {
        return GameObjectList.FirstOrDefault(obj => obj.Name == name);
    }


    public static void EnableColliderRendering()
    {
        foreach (var gameObject in GameObjectList) gameObject.DEBUGCOLIDERS = true;
    }

    public static void DisableColliderRendering()
    {
        foreach (var gameObject in GameObjectList) gameObject.DEBUGCOLIDERS = false;
    }
}