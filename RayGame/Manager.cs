using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace RayGame;

public class Manager : IGameComponent
{
    private static Manager instance;
    public static Manager Instance
    {
        get
        {
            // If instance is null, create a new instance
            if (instance == null)
            {
                instance = new Manager();
            }
            return instance;
        }
    }
    public GameObject Container { get; set; }
    private bool spawnFlag;

    private GameObject obj;
    private GameObject obj2;

    public void Start()
    {
        
        obj =Engine.CreateGameObject("Entity");
        obj.AddComponent<B2>();
        
        obj2 = Engine.CreateGameObject("Entity2");
        obj2.AddComponent<B2>();
        
        
    }
    
    public void Update()
    {
        // if ((int)Engine.GameClock.Elapsed.TotalSeconds % 1.5f == 0)
        // {
        //     if (!spawnFlag)
        //     {
        //         SpawnObject();
        //         spawnFlag = true;
        //     }
        // }
        // else
        // {
        //     spawnFlag = false;
        // }

        if (Collider.CheckCollision(obj.ProcessVertices(obj.Vertices).ToArray(), obj2.ProcessVertices(obj2.Vertices).ToArray()))
        {
            Console.WriteLine("Objects Collided");
        }
    }

    // void SpawnObject()
    // {
    //     var obj =Engine.CreateGameObject("Entity");
    //     obj.AddComponent<B2>();
    // }
}