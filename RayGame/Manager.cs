using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace RayGame;

public class Manager : IGameComponent
{
    private static Manager instance;

    public static Manager Instance
    {
        get { if (instance == null) { instance = new Manager(); } return instance; }
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
        if (obj.IsColliding(obj2))
        {
            Engine.DeleteGameObject(obj);
        }
    }

    // void SpawnObject()
    // {
    //     var obj =Engine.CreateGameObject("Entity");
    //     obj.AddComponent<B2>();
    // }
}