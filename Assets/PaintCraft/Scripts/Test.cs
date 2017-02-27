using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{

    Color[] defaultColors;
    string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/skin.png";


    }

    public void OnButtonSave()
    {

        defaultColors = new Color[64 * 64];
        for (int i = 0; i < defaultColors.Length; i++)
        {
            if (i % 2 == 0)
                defaultColors[i] = Color.red;
            else
                defaultColors[i] = Color.grey;
        }

        Texture2D tex = new Texture2D(64, 64);
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.SetPixels(defaultColors);
        tex.Apply();


        FileManager.Instance.SaveTextureToFile(tex, filePath);

    }




    public void OnButtonLoad()
    {

        Sprite sprite = FileManager.Instance.GetSpriteFromPNG(filePath, new Rect(8, 48, 8, 8));
        GetComponent<Image>().sprite = sprite;
    }

    public void OnButtonLoadAllFiles()
    {
        string[] textures = FileManager.Instance.GetAllFiles(Application.persistentDataPath, "*.png");
        Sprite sprite = FileManager.Instance.GetSpriteFromPNG(textures[0], new Rect(8, 48, 8, 8));
        GetComponent<Image>().sprite = sprite;
    }

    public void OnButtonSkins()
    {
        SceneManager.LoadScene("skins");

    }
}
