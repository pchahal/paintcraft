using UnityEngine;
using UnityEngine.UI;

public class ShareSkin : MonoBehaviour
{
    public Text console;
    public Texture2D texture;
    string imagePath = "";

    //=============================================================================
    // Button handlers
    //=============================================================================
    public void OnSaveImagePress()
    {
        NativeToolkit.SaveImage(texture, "MyImage", "png");
    }

    public void OnEmailSharePress()
    {
        NativeToolkit.SendEmail("Hello there", "<html><body><b>This is an email sent from my App!</b></body></html>", imagePath, "", "", "");
    }

    //=============================================================================
    // Callbacks
    //=============================================================================

    void ScreenshotSaved(string path)
    {
        console.text += "\n" + "Screenshot saved to: " + path;
    }

    void ImageSaved(string path)
    {
        console.text += "\n" + texture.name + " saved to: " + path;
    }






}