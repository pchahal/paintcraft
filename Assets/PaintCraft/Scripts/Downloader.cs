// Add this script to a GameObject. The Start() function fetches an
// image from the documentation site.  It is then applied as the
// texture on the GameObject.

using UnityEngine;
using System.Collections;

public class Downloader : MonoBehaviour
{



    IEnumerator Start()
    {
        string[] textures = FileManager.Instance.GetAllFiles(Application.persistentDataPath, "*.png");


        string url;
        url = "file://" + Application.persistentDataPath + "/skin.png";

        Texture2D tex;
        tex = new Texture2D(8, 8, TextureFormat.DXT1, false);
        WWW www = new WWW(url);
        yield return www;
        www.LoadImageIntoTexture(tex);
        GetComponent<Renderer>().material.mainTexture = tex;
    }
}