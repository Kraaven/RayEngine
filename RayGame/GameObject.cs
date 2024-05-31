using System.ComponentModel;
using System.Numerics;
using Raylib_cs;

namespace RayGame;

public class GameObject
{
    public string Name;
    public Transform Transform = new Transform();
    private Guid ID = Guid.NewGuid();
    
    private List<IGameComponent> ObjectComponents = new List<IGameComponent>();
    private List<IRenderer> ObjectRenderers = new List<IRenderer>();
    public List<Mesh> Colliders = new List<Mesh>(); 
    
    // private List<Collider> ObjectColliders;
    
    public void StartActions()
    {
        foreach (var gameComponent in ObjectComponents)
        {
            gameComponent.Start();
        }
    }
    public void UpdateActions()
    {
        foreach (var gameComponent in ObjectComponents)
        {
            gameComponent.Update();
        }

        foreach (var renderer in ObjectRenderers)
        {
            renderer.Update();
        }

        // foreach (var collider in Colliders)
        // {
        //     MeshRenderer.RenderMesh(Transform.ApplyTransform(collider.GetVertexArray()), Color.Green);
        // }
    }

    public bool IsColliding(GameObject Target)
    {
        
        if (Colliders.Count != 0)
        {
            foreach (var mesh in Colliders)
            {
                foreach (var targetCollider in Target.Colliders)
                {
                    if (CollisionDetection.CheckCollision(Transform.ApplyTransform(mesh.GetVertexArray()), Target.Transform.ApplyTransform(targetCollider.GetVertexArray())))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        else
        {
            return false;
        }
    }
    public void SetTransform(Transform newTransform)
    {
        Transform = newTransform;
    }

    #region Component Management
    public T AddComponent<T>() where T : IGameComponent, new()
    {
        if (ObjectComponents.Any(item => item is T))
        {
            return GetComponent<T>();
        }
        else
        {
            T gameComponent = new T();
            ObjectComponents.Add(gameComponent);
            gameComponent.Container = this;
            ObjectComponents.Last().Start();
            return gameComponent;
        }

    }
    public T GetComponent<T>()
    {
        return ObjectComponents.OfType<T>().FirstOrDefault();
    }
    public void DeleteComponent<T>() where T : IGameComponent
    {
        ObjectComponents.Remove(GetComponent<T>());
    }
    public void DeleteObjectComponents()
    {
        ObjectComponents.Clear();
    }
    public List<string> GetComponentNameList(bool Print)
    {
        List<string> Components = new();
        foreach (var gameComponent in ObjectComponents)
        {
            Components.Add(gameComponent.GetType().ToString());
        }

        if (Print)
        {
            Console.WriteLine(string.Join(", ", Components.ToArray()));
        }

        return Components;
    }
    public void ShiftComponent<T>(int offset) where T : IGameComponent
    {
        int currentIndex = ObjectComponents.FindIndex(component => component is T);
        if (currentIndex == -1)
        {
            throw new ArgumentException("Component of the specified type not found in the list.");
        }

        int newIndex = (currentIndex + offset) % ObjectComponents.Count;
        if (newIndex < 0)
        {
            newIndex += ObjectComponents.Count;
        }

        T component = (T)ObjectComponents[currentIndex];
        ObjectComponents.RemoveAt(currentIndex);
        ObjectComponents.Insert(newIndex, component);
    }
    public bool HasComponent<T>() where T : IGameComponent
    {
        foreach (var gameComponent in ObjectComponents)
        {
            if (gameComponent is T)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region RendererManagement
    public T AddRenderer<T>() where T : IRenderer, new()
    {
        T RenderComponent = new T();
        ObjectRenderers.Add(RenderComponent);
        RenderComponent.Container = this;
        ObjectRenderers.Last().Start();
        return RenderComponent;
    }
    public void DeleteRenderer<T>(int Index) where T : class, IRenderer
    {
        T renderer = GetRenderer<T>(Index);
        if (renderer != null)
        {
            ObjectRenderers.Remove(renderer);
        }
        else
        {
            Console.WriteLine($"Renderer of type {typeof(T).Name} at index {Index} not found.");
        }
    }
    public T GetRenderer<T>(int Index) where T : class, IRenderer
    {
        foreach (var renderer in ObjectRenderers)
        {
            if (renderer is T Specific)
            {
                if (Index == 0)
                {
                    return Specific;
                }
                else
                {
                    Index--;
                }
            }
        }

        return null as T;
    }
    public List<string> GetRendererNameList(bool Print)
    {
        List<string> Renderers = new();
        foreach (var renderer in ObjectRenderers)
        {
            Renderers.Add(renderer.GetType().ToString());
        }

        if (Print)
        {
            Console.WriteLine(string.Join(", ", Renderers.ToArray()));
        }

        return Renderers;
    }
    public void DeleteAllRenderers()
    {
        ObjectRenderers.Clear();
    }
    public bool HasRenderer<T>() where T : IRenderer
    {
        foreach (var renderer in ObjectRenderers)
        {
            if (renderer is T)
            {
                return true;
            }
        }

        return false;
    }
    public void ShiftRenderer<T>(int offset) where T : IRenderer
    {
        int currentIndex = ObjectRenderers.FindIndex(renderer => renderer is T);
        if (currentIndex == -1)
        {
            throw new ArgumentException("Renderer of the specified type not found in the list.");
        }

        int newIndex = (currentIndex + offset) % ObjectRenderers.Count;
        if (newIndex < 0)
        {
            newIndex += ObjectRenderers.Count;
        }

        T renderer = (T)ObjectRenderers[currentIndex];
        ObjectRenderers.RemoveAt(currentIndex);
        ObjectRenderers.Insert(newIndex, renderer);
    }
    #endregion
}
