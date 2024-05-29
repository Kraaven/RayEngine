namespace RayGame;

public class B2 : IGameComponent
{
    public GameObject Container { get; set; }
    public void Start()
    {
        
    }

    public void Update()
    {
        Container.Rotation += 0.1f;
    }
}