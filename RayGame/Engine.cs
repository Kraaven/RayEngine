using System;
using System.Diagnostics;
using System.Numerics;
using RayGame;
using Raylib_cs;
static class Engine
{
    public static List<GameObject> GameObjectList = new List<GameObject>();
    public static void Main()
    {
        Raylib.InitWindow(800,480,"Hello World");
        // var obj = Program.CreateGameObject("Test", new Vector2(250,200));
        // obj.AddVertices(new []{(50f,50f),(-50f,50f),(-50f,-50f),(50f,-50f)});
        // GameObject test = new GameObject(new Vector2(200, 200),0);
        // test.AddVertices(new []{(50f,50f),(-50f,50f),(-50f,-50f),(50f,-50f)}); 

        var M = CreateGameObject("Manager");
        M.AddComponent<Manager>();
        M.StartActions();
        
        Stopwatch Clock = new Stopwatch();
        Clock.Start();

        foreach (var gameObject in GameObjectList)
        {
            if (gameObject.Name != "Manager")
            {
                gameObject.StartActions();
            }
        }
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            // test.RenderObject();
            // test.Rotate(0.05f);
            foreach (var OBJECT in GameObjectList)
            {
                OBJECT.UpdateActions();
            }
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
    
    public static GameObject CreateGameObject(string name, Vector2 Position)
    {
        GameObjectList.Add(new GameObject());
        GameObjectList.Last().Name = name;
        GameObjectList.Last().Position = Position;
        return GameObjectList.Last();
    }

    public static GameObject CreateGameObject(string name, Vector2 Position, float Angle)
    {
        GameObjectList.Add(new GameObject());
        GameObjectList.Last().Name = name;
        GameObjectList.Last().Position = Position;
        GameObjectList.Last().Rotation = Angle;
        return GameObjectList.Last();
    }

}