#region

using System.Numerics;

#endregion

namespace RayGame.Demo;

public class Manager : IGameComponent
{
    private static Manager instance;
    
    //Declaring a Bird
    private GameObject BIRD;
    
    //Declaring a reference to pipes that passes
    private GameObject passed;
    
    //Declaring a List of Pipes
    public List<GameObject> PipeInstances = new();
    
    //Declaring a boolean to determine if the game is running or not
    public bool Running;
    
    // Declaring a boolean flag for the timer
    private bool spawnFlag;

    //Creating a Singleton out of the Manager Class
    public static Manager Instance
    {
        get
        {
            if (instance == null) instance = new Manager();

            return instance;
        }
    }

    //The Container of the Manager
    public GameObject Container { get; set; }


    //Manager Start function, 1st function to be called in game
    public void Start()
    {
        //Creating the BIRD GameObject
        BIRD = Engine.CreateGameObject("bird");
        
        //Adding the Bird Component to the BIRD GameObject
        BIRD.AddComponent<Bird>();

        //Setting the game state running to true
        Running = true;
    }

    // The Function that is being run per frame
    public void Update()
    {
        // Cross-Checking the time to create a constant looped timer
        if (Engine.GAMETIME % 650 < 20)
        {
            if (!spawnFlag)
            {
                // Spawning a Pipe
                SpawnObject();
                spawnFlag = true;
                
                // printing the number of GameObjects in the Scene
                Console.WriteLine($"Objects in Scene: {Engine.GetGameObjectCount()}");
            }
        }
        else
        {
            spawnFlag = false;
        }


        // Checking Each pipe
        foreach (var pipeInstance in PipeInstances)
        {
            // If a pipe is out of the screen (left), make the pass variable refer it
            pipeInstance.Transform.Translate((-5f, 0f));
            if (pipeInstance.Transform.Position.X < -50) passed = pipeInstance;
        }

        //Remove the object referred, ie, the passes Pipe
        PipeInstances.Remove(passed);
        Engine.DeleteGameObject(passed);

        // Running a Loop again, with all the pipes on the screen
        foreach (var pipe in PipeInstances)
            // If the Bird Collides with any of the pipes, set running to false
            if (BIRD.IsColliding(pipe))
                Running = false;

        // If running is false, restart the game
        if (!Running) RestartGame();
    }

    // Spawn a Pipe
    public void SpawnObject()
    {
        // Declaring the Meshes for the top of the pipes
        var TopLip = new Mesh(new[] { (-40f, 15f), (40f, 15f), (40f, -15f), (-40f, -15f) });
        var TopBody = new Mesh(new[] { (-25f, -15f), (25f, -15f), (25f, -450f), (-25f, -450f) });


        // Create the Pipe
        var Pipes = Engine.CreateGameObject("Entity",
            new Transform(new Vector2(800, Engine.random.Next(60, 300)), 180, 1));
        // Add the meshes as renderers, use the same meshes as colliders
        Pipes.AddRenderer<MeshRenderer>().SetMesh(TopLip).ShiftMesh((0f, -150));
        Pipes.AddRenderer<MeshRenderer>().SetMesh(TopBody).ShiftMesh((0f, -150));
        Pipes.Colliders.Add(TopLip);
        Pipes.Colliders.Add(TopBody);

        //create 2 more meshes for the bottom pipes, and use them for the renderers and colliders
        var BottomLip = new Mesh(TopLip.GetVertexArray());
        var BottomBody = new Mesh(TopBody.GetVertexArray());
        var M3 = Pipes.AddRenderer<MeshRenderer>().SetMesh(new Mesh(BottomLip.GetVertexArray())).ShiftMesh((0f, 150f))
            .RotateMesh(180);
        var M4 = Pipes.AddRenderer<MeshRenderer>().SetMesh(new Mesh(BottomBody.GetVertexArray())).ShiftMesh((0f, 150f))
            .RotateMesh(180);
        Pipes.Colliders.Add(M3);
        Pipes.Colliders.Add(M4);

        // Add the new Pipe to the List of Pipes
        PipeInstances.Add(Pipes);
    }

    // Restart the Game
    public void RestartGame()
    {
        //Deleting all the Pipes
        foreach (var pipe in PipeInstances) Engine.DeleteGameObject(pipe);

        // Clear the list of references
        PipeInstances.Clear();

        //Delete the bird
        Engine.DeleteGameObject(BIRD);
        BIRD = null;

        //Execute the Start Function
        Start();
        Running = true;
    }
}