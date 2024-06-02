using System.Numerics;

namespace RayGame.Components;

public class Manager : IGameComponent
{
    private static Manager instance;

    public static Manager Instance
    {
        get { if (instance == null) { instance = new Manager(); } return instance; }
    }
    public GameObject Container { get; set; }
    private bool spawnFlag;
    public List<GameObject> PipeInstances = new List<GameObject>();
    private GameObject passed;
    private GameObject BIRD;
    public bool Running;
    

    public void Start()
    {
        BIRD = Engine.CreateGameObject("bird");
        BIRD.AddComponent<bird>();

        Running = true;
    }
    
    public void Update()
    {
        if (Engine.GAMETIME % 650 < 20)
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
    

    
        foreach (var pipeInstance in PipeInstances)
        {
            pipeInstance.Transform.Translate((-5f,0f));
            if (pipeInstance.Transform.Position.X < -50)
            {
                passed = pipeInstance;
            }
        }

        PipeInstances.Remove(passed);
        Engine.DeleteGameObject(passed);
        
        foreach (var pipe in PipeInstances)
        {
            if (BIRD.IsColliding(pipe))
            {
                Running = false;
            }
        }

        if (!Running)
        {
            RestartGame();
        }
            
        
    }

    public void SpawnObject()
    {
        Mesh TopLip = new Mesh(new []{(-40f, 15f), (40f, 15f), (40f, -15f), (-40f, -15f)});
        Mesh TopBody = new Mesh(new[] { (-25f, -15f), (25f, -15f), (25f, -450f), (-25f, -450f) });
        
        
        var Pipes = Engine.CreateGameObject("Entity", new Transform(new Vector2(800,Engine.random.Next(60,300)),180,1));
        Pipes.AddRenderer<MeshRenderer>().SetMesh(TopLip).ShiftMesh((0f,-150));
        Pipes.AddRenderer<MeshRenderer>().SetMesh(TopBody).ShiftMesh((0f,-150));
        Pipes.Colliders.Add(TopLip);
        Pipes.Colliders.Add(TopBody);

        Mesh BottomLip = new Mesh(TopLip.GetVertexArray());
        Mesh BottomBody = new Mesh(TopBody.GetVertexArray());
         var M3 = Pipes.AddRenderer<MeshRenderer>().SetMesh(new Mesh(BottomLip.GetVertexArray())).ShiftMesh((0f,150f)).RotateMesh(180);
         var M4 =Pipes.AddRenderer<MeshRenderer>().SetMesh(new Mesh(BottomBody.GetVertexArray())).ShiftMesh((0f,150f)).RotateMesh(180);
        Pipes.Colliders.Add(M3);
        Pipes.Colliders.Add(M4);
        
        PipeInstances.Add(Pipes);
    }

    public void RestartGame()
    {
        foreach (var pipe in PipeInstances)
        {
            Engine.DeleteGameObject(pipe);
        }
        PipeInstances.Clear();
            
        Engine.DeleteGameObject(BIRD);
        BIRD = null;
            
        Start();
        Running = true;
    }

}