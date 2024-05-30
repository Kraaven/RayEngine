using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace RayGame;

public class Manager : IGameComponent
{
    public GameObject Container { get; set; }
    private bool spawnFlag;

    public void Start()
    {

    }

    public void Update()
    {
        if ((int)Engine.GameClock.Elapsed.TotalSeconds % 1.5f == 0)
        {
            if (!spawnFlag)
            {
                SpawnObject();
                spawnFlag = true;
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