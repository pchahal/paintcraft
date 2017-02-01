
using UnityEngine;
using System.IO;

public class FileManager : Singleton<FileManager>
{
    private string baseURL;

    public Sprite GetSpriteFromPNG(string fullPathName, Rect rect)
    {
        string url = "file://" + fullPathName;
        WWW www = new WWW(url);

        Texture2D tex = new Texture2D(www.texture.width, www.texture.height);
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.SetPixels(www.texture.GetPixels());
        tex.Apply();

        Sprite sprite = Sprite.Create(tex, rect, new Vector2(0, 0));
        return sprite;

    }

    public Texture2D GetTextureFromPNG(string fullPathName)
    {
        string url = "file://" + fullPathName;
        WWW www = new WWW(url);

        Texture2D tex = new Texture2D(www.texture.width, www.texture.height);
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.SetPixels(www.texture.GetPixels());
        tex.Apply();
        return tex;

    }

    public void SaveTextureToFile(Texture2D tex, string fullPathName)
    {
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(fullPathName, bytes);

    }
    public string[] GetAllFiles(string path, string fileExtension)
    {
        string[] files = System.IO.Directory.GetFiles(path, fileExtension);
        return files;
    }

    public void DeleteFile(string path)
    {
        File.Delete(path);
    }

}
