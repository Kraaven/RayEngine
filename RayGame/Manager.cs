using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace RayGame;

public class Manager : IGameComponent
{
    public GameObject Container { get; set; }
    private bool spawnFlag;

    public void Start()
    {
        
        // var b1 = Engine.CreateGameObject("Box1", new Vector2(250,200));
        // b1.AddVertices(new []{(50f,50f),(-50f,50f),(-50f,-50f),(50f,-50f)});
        // b1.Scale = 0.1f;
        // b1.Rotation = 45;
        //
        // var b2 = Engine.CreateGameObject("Box2", new Vector2(350,250));
        // b2.AddVertices(new []{(50f,50f),(-50f,50f),(-50f,-50f),(50f,-50f)});
        // //b2.Translate((-100,-45));
        // b2.Rotation = 75;
        // b2.Scale = 0.5f;
        // b2.AddComponent<B2>();
        
        // Console.WriteLine(Engine.GameObjectList.Count);
        
        
    }

    public void Update()
    {
        // Console.WriteLine((int)Engine.GameClock.Elapsed.TotalMilliseconds );
        if ((int)Engine.GameClock.Elapsed.TotalSeconds % 1.5f == 0)
        {
            if (!spawnFlag)
            {
                SpawnObject();
                spawnFlag = true;
                Console.WriteLine($"Objects in Scene: {Engine.GameObjectList.Count}");
            }
        }
        else
        {
            spawnFlag = false;
        }
    }

    void SpawnObject()
    {
        var obj =Engine.CreateGameObject("Entity");
        obj.AddComponent<B2>();
    }
}