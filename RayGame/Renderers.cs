﻿#region

using System.Numerics;
using Raylib_cs;

#endregion

namespace RayGame;

/// <summary>
/// A Renderer that renders a <see cref="Mesh"/> associated with a <see cref="GameObject"/>.
/// </summary>
public class MeshRenderer : IRenderer
{
    private Mesh mesh;
    
    /// <summary>
    /// The Container is the reference to the <see cref="GameObject"/> it is connected to.
    /// </summary>
    public GameObject Container { get; set; }

    
    /// <summary>
    /// Initializes the renderer. This method is called when the renderer is first added to a <see cref="GameObject"/>.
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    /// Updates the renderer. This method is called once per frame.
    /// </summary>
    public void Update()
    {
        var Vertices = Container.Transform.ApplyTransform(mesh.GetVertexArray());
        if (Vertices.Length > 0) RenderMesh(Vertices, Color.Black);
    }

    /// <summary>
    /// Sets the mesh to be rendered.
    /// </summary>
    /// <param name="mesh">The <see cref="Mesh"/> to set.</param>
    /// <returns>The <see cref="Mesh"/> that was set.</returns>
    public Mesh SetMesh(Mesh mesh)
    {
        this.mesh = mesh;
        return mesh;
    }

    /// <summary>
    /// Gets the mesh being rendered.
    /// </summary>
    /// <returns>The <see cref="Mesh"/> being rendered.</returns>
    public Mesh GetMesh()
    {
        return mesh;
    }

    /// <summary>
    /// Renders the specified vertices as lines with the given color. **Primarily for internal use**
    /// </summary>
    /// <param name="Vertices">An array of <see cref="Vector2"/> representing the vertices of the mesh.</param>
    /// <param name="color">The color to use for rendering the lines.</param>
    /// <exclude />
    public static void RenderMesh(Vector2[] Vertices, Color color)
    {
        Raylib.DrawLineV(Vertices[^1], Vertices[0], color);
        for (var i = 0; i < Vertices.Length - 1; i++) Raylib.DrawLineV(Vertices[i], Vertices[i + 1], color);
    }
}

public class SpriteRenderer : IRenderer
{
    /// <summary>
    /// The Container is the reference to the <see cref="GameObject"/> it is connected to.
    /// </summary>
    public GameObject Container { get; set; }

    private Texture2D sprite;

    private Rectangle source;

    private Rectangle destination;

    private Vector2 Origin;
    
    public Transform transform;
    // private Texture2D spritesheet;
    /// <summary>
    /// Initializes the renderer. This method is called when the renderer is first added to a <see cref="GameObject"/>.
    /// </summary>
    public void Start()
    {
        transform = new Transform();
        destination = new Rectangle();
    }

    /// <summary>
    /// Retrieves the current sprite texture.
    /// </summary>
    /// <returns>The current <see cref="Texture2D"/> sprite.</returns>
    public Texture2D GetSprite()
    {
        return sprite;
    }
    
    /// <summary>
    /// Sets the sprite texture.
    /// </summary>
    /// <param name="inputSprite">The new <see cref="Texture2D"/> sprite to set.</param>
    public void SetSprite(Texture2D ImputSprite)
    {
        sprite = ImputSprite;
        source = new Rectangle(0, 0, sprite.Width, sprite.Height);
        
    }

    /// <summary>
    /// Updates the renderer. This method is called once per frame.
    /// </summary>
    public void Update()
    {
        destination.Position = Container.Transform.Position + transform.Position;
        destination.Width = sprite.Width * (Container.Transform.Scale * transform.Scale);
        destination.Height = sprite.Height * (Container.Transform.Scale* transform.Scale);
        Origin = new Vector2(destination.Width / 2, destination.Height / 2);
        
        Raylib.DrawTexturePro(sprite,source,destination,Origin, Container.Transform.GetRotation() + transform.GetRotation(),Color.White);
        Console.WriteLine(destination);
    }
}