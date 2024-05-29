using System;
using System.Diagnostics;
using System.Numerics;
using RayGame;
using Raylib_cs;
class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World");
        Raylib.InitWindow(800,480,"Hello World");
        GameObject test = new GameObject(new Vector2(200, 200),0);
        test.AddVertices(new []{(50f,50f),(-50f,50f),(-50f,-50f),(50f,-50f)});
        Stopwatch Clock = new Stopwatch();
        Clock.Start();
        
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            test.RenderObject();
            test.Rotate(0.05f);
            Raylib.EndDrawing();
        }
        
        Raylib.CloseWindow();
    }

}