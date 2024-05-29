using System;
using System.Numerics;
using RayGame;
using Raylib_cs;
class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World");
        Raylib.InitWindow(800,480,"Hello World");
        GameObject test = new GameObject();
        test.Position = new Vector2(200, 200);
        
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);
            
            Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
            test.RenderObject();
            Raylib.EndDrawing();
        }
        
        Raylib.CloseWindow();
    }

}