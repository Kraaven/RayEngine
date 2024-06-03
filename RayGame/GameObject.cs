#region

using Raylib_cs;

#endregion

namespace RayGame;

/// <summary>
/// Represents a game object in the Scene.
/// </summary>
/// <remarks>
/// The GameObjects are the building blocks of your scene. They contain a few fields that allow for adding logic and properties to make them functional.
/// </remarks>>
public class GameObject
{
    
    private List<IGameComponent> ObjectComponents = new();
    private List<IRenderer> ObjectRenderers = new();
    /// <summary>
    /// The List of Colliders attached to the GameObject. They are represented as a List of <see cref="Mesh"/>'s. You can add Colliders directly.
    /// </summary>
    public List<Mesh> Colliders = new();
    /// <summary>
    /// This is a property of the GameObject that determines what is to be rendered. If *false*, all the <see cref="IRenderer"/>'s Update Method will be fired per frame.
    /// If *true*, then instead of the <see cref="IRenderer"/>'s, the Colliders attached to your GameObject will be rendered. This is for debugging purposes.
    /// </summary>
    public bool DEBUGCOLIDERS = false;
    /// <summary>
    /// Think of this field as a unique ID for each GameObject.
    /// </summary>
    private readonly Guid ID = Guid.NewGuid();
    /// <summary>
    /// This is the Name Associated with the GameObject
    /// </summary>
    public string Name;
    /// <summary>
    /// This is the <see cref="Transform"/> attached to the GameObject.
    /// </summary>
    public Transform Transform = new();

    // private List<Collider> ObjectColliders;

    /// <summary>
    /// The function that is called when the GameObject is Initiated. Not used in most cases. **Primarily for internal use**
    /// </summary>
    public void StartActions()
    {
        foreach (var gameComponent in ObjectComponents) gameComponent.Start();
    }

    /// <summary>
    /// This is the function that calls to update the state of the GameObject per frame.**Primarily for internal use**
    /// </summary>
    public void UpdateActions()
    {
        foreach (var gameComponent in ObjectComponents) gameComponent.Update();

        if (DEBUGCOLIDERS)
            foreach (var collider in Colliders)
                MeshRenderer.RenderMesh(Transform.ApplyTransform(collider.GetVertexArray()), Color.Green);
        else
            foreach (var renderer in ObjectRenderers)
                renderer.Update();
    }

    /// <summary>
    /// Checks if this game object is colliding with the specified target game object.
    /// </summary>
    /// <param name="Target">The target game object to check for collisions with.</param>
    /// <returns>True if a collision is detected, otherwise false.</returns>
    public bool IsColliding(GameObject Target)
    {
        if (Colliders.Count != 0)
        {
            foreach (var mesh in Colliders)
            foreach (var targetCollider in Target.Colliders)
                if (CollisionDetection.CheckCollision(Transform.ApplyTransform(mesh.GetVertexArray()),
                        Target.Transform.ApplyTransform(targetCollider.GetVertexArray())))
                    return true;

            return false;
        }

        return false;
    }

    /// <summary>
    /// Sets the transform of this game object to the specified new transform.
    /// </summary>
    /// <param name="newTransform">The new transform to set.</param>
    public void SetTransform(Transform newTransform)
    {
        Transform = newTransform;
    }

    #region Component Management

    /// <summary>
    /// Adds a component of the specified type to this game object.
    /// </summary>
    /// <typeparam name="T">The type of component to add.</typeparam>
    /// <returns>The added component.</returns>
    public T AddComponent<T>() where T : IGameComponent, new()
    {
        if (ObjectComponents.Any(item => item is T)) return GetComponent<T>();

        var gameComponent = new T();
        ObjectComponents.Add(gameComponent);
        gameComponent.Container = this;
        ObjectComponents.Last().Start();
        return gameComponent;
    }

    
    /// /// <summary>
    /// Gets the first component of the specified type attached to this game object.
    /// </summary>
    /// <typeparam name="T">The type of component to get.</typeparam>
    /// <returns>The component if found, otherwise null.</returns>
    public T GetComponent<T>()
    {
        return ObjectComponents.OfType<T>().FirstOrDefault();
    }

    /// <summary>
    /// Deletes the first component of the specified type from this game object.
    /// </summary>
    /// <typeparam name="T">The type of component to delete.</typeparam>
    public void DeleteComponent<T>() where T : IGameComponent
    {
        ObjectComponents.Remove(GetComponent<T>());
    }

    /// <summary>
    /// Deletes all components from this game object.
    /// </summary>
    public void DeleteObjectComponents()
    {
        ObjectComponents.Clear();
    }

    /// <summary>
    /// Gets a list of the names of all components attached to this game object.
    /// </summary>
    /// <param name="Print">If true, prints the names to the console.</param>
    /// <returns>A list of component names.</returns>
    public List<string> GetComponentNameList(bool Print)
    {
        List<string> Components = new();
        foreach (var gameComponent in ObjectComponents) Components.Add(gameComponent.GetType().ToString());

        if (Print) Console.WriteLine(string.Join(", ", Components.ToArray()));

        return Components;
    }

    /// <summary>
    /// Shifts a component of the specified type by a given offset in the component list.
    /// </summary>
    /// <typeparam name="T">The type of component to shift.</typeparam>
    /// <param name="offset">The offset by which to shift the component.</param>
    public void ShiftComponent<T>(int offset) where T : IGameComponent
    {
        var currentIndex = ObjectComponents.FindIndex(component => component is T);
        if (currentIndex == -1) throw new ArgumentException("Component of the specified type not found in the list.");

        var newIndex = (currentIndex + offset) % ObjectComponents.Count;
        if (newIndex < 0) newIndex += ObjectComponents.Count;

        var component = (T)ObjectComponents[currentIndex];
        ObjectComponents.RemoveAt(currentIndex);
        ObjectComponents.Insert(newIndex, component);
    }

    /// <summary>
    /// Checks if this game object has a component of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of component to check for.</typeparam>
    /// <returns>True if the component is found, otherwise false.</returns>
    public bool HasComponent<T>() where T : IGameComponent
    {
        foreach (var gameComponent in ObjectComponents)
            if (gameComponent is T)
                return true;

        return false;
    }

    #endregion

    #region RendererManagement

    /// <summary>
    /// Adds a renderer of the specified type to this game object.
    /// </summary>
    /// <typeparam name="T">The type of renderer to add.</typeparam>
    /// <returns>The added renderer.</returns>
    public T AddRenderer<T>() where T : IRenderer, new()
    {
        var RenderComponent = new T();
        ObjectRenderers.Add(RenderComponent);
        RenderComponent.Container = this;
        ObjectRenderers.Last().Start();
        return RenderComponent;
    }

    /// <summary>
    /// Deletes a renderer of the specified type at the given index from this game object.
    /// </summary>
    /// <typeparam name="T">The type of renderer to delete.</typeparam>
    /// <param name="Index">The index of the renderer to delete.</param>
    public void DeleteRenderer<T>(int Index) where T : class, IRenderer
    {
        var renderer = GetRenderer<T>(Index);
        if (renderer != null)
            ObjectRenderers.Remove(renderer);
        else
            Console.WriteLine($"Renderer of type {typeof(T).Name} at index {Index} not found.");
    }

    /// <summary>
    /// Gets a renderer of the specified type at the given index from this game object. If the GameObject contains
    /// multiple types of renderers, then the index takes into account only the specified <see cref="T"/>
    /// </summary>
    /// <typeparam name="T">The type of renderer to get.</typeparam>
    /// <param name="Index">The index of the renderer to get.</param>
    /// <returns>The renderer if found, otherwise null.</returns>
    public T GetRenderer<T>(int Index) where T : class, IRenderer
    {
        foreach (var renderer in ObjectRenderers)
            if (renderer is T Specific)
            {
                if (Index == 0)
                    return Specific;
                Index--;
            }

        return null as T;
    }

    /// <summary>
    /// Gets a list of the names of all renderers attached to this game object.
    /// </summary>
    /// <param name="Print">If true, prints the names to the console.</param>
    /// <returns>A list of renderer names.</returns>
    public List<string> GetRendererNameList(bool Print)
    {
        List<string> Renderers = new();
        foreach (var renderer in ObjectRenderers) Renderers.Add(renderer.GetType().ToString());

        if (Print) Console.WriteLine(string.Join(", ", Renderers.ToArray()));

        return Renderers;
    }

    /// <summary>
    /// Deletes all renderers from this game object.
    /// </summary>
    public void DeleteAllRenderers()
    {
        ObjectRenderers.Clear();
    }

    /// <summary>
    /// Checks if this game object has a renderer of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of renderer to check for.</typeparam>
    /// <returns>True if the renderer is found, otherwise false.</returns>
    public bool HasRenderer<T>() where T : IRenderer
    {
        foreach (var renderer in ObjectRenderers)
            if (renderer is T)
                return true;

        return false;
    }

    /// <summary>
    /// Shifts a renderer of the specified type by a given offset in the renderer list.
    /// </summary>
    /// <typeparam name="T">The type of renderer to shift.</typeparam>
    /// <param name="offset">The offset by which to shift the renderer.</param>
    public void ShiftRenderer<T>(int offset) where T : IRenderer
    {
        var currentIndex = ObjectRenderers.FindIndex(renderer => renderer is T);
        if (currentIndex == -1) throw new ArgumentException("Renderer of the specified type not found in the list.");

        var newIndex = (currentIndex + offset) % ObjectRenderers.Count;
        if (newIndex < 0) newIndex += ObjectRenderers.Count;

        var renderer = (T)ObjectRenderers[currentIndex];
        ObjectRenderers.RemoveAt(currentIndex);
        ObjectRenderers.Insert(newIndex, renderer);
    }

    #endregion
}