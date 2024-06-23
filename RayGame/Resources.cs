using Raylib_cs;

namespace RayGame;

public class Resources
{
    
}

public class Sprite
{
    public Texture2D Image;

    public Sprite(string TextureName)
    {
        try
        {
            var I = Raylib.LoadImage($"/Resources/{TextureName}");
            Image = Raylib.LoadTextureFromImage(I);
            Raylib.UnloadImage(I);
        }
        catch (Exception e)
        {
            Console.WriteLine("Image does not Exist");
            throw;
        }
    }
    
    
}