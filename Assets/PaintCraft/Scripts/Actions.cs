
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ActionType
{
    BRUSH,
    ERASER,
    COLORPICKER,
    FILL,
}

public class Actions : MonoBehaviour
{
    public static Color currentColor;
    Menu menu;
    private Action<Texture2D, Vector2, Vector3, BodyPart> currentAction;
    Steve steve;

    void Start()
    {
        menu = GameObject.Find("EventSystem").GetComponent<Menu>();
        steve = GameObject.Find("StevePaintable").GetComponent<Steve>();
        currentAction = BrushAction;
        currentColor = Color.red;
    }

    public void SetAction(int actionNum)
    {
        ActionType type = (ActionType)actionNum;
        switch (type)
        {
            case ActionType.BRUSH: currentAction = BrushAction; break;
            case ActionType.COLORPICKER: currentAction = ColorPickerAction; break;
            case ActionType.ERASER: currentAction = EraserAction; break;
            case ActionType.FILL: currentAction = FillAction; break;
            default: break;
        }
    }

    public void SetCurrentColor(Color color)
    {
        currentColor = color;
        GameObject.Find("ColorSwatches").GetComponent<ColorSwatch>().OnChangeSwatchColor(currentColor);
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }

    public void DoAction(Texture2D tex, Vector2 pixelUV, Vector3 normal, BodyPart bodyPart)
    {
        if (steve.isEditMode)
            currentAction(tex, pixelUV, normal, bodyPart);
        Camera.main.GetComponent<SkinCamera>().ZoomIn(bodyPart);
        menu.EditSkin();
        steve.ShowBodyPart(bodyPart);
    }

    private static void BrushAction(Texture2D tex, Vector2 pixelUV, Vector3 normal, BodyPart bodyPart)
    {
        SetPixelColor(tex, pixelUV, currentColor);
        tex.Apply();
    }

    private void EraserAction(Texture2D tex, Vector2 pixelUV, Vector3 normal, BodyPart bodyPart)
    {
        Color defaultColor = steve.GetDefaultPixel(pixelUV);
        SetPixelColor(tex, pixelUV, defaultColor);
        tex.Apply();
    }

    private static void ColorPickerAction(Texture2D tex, Vector2 pixelUV, Vector3 normal, BodyPart bodyPart)
    {
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        Color newColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
        if (newColor.a > .9f)
        {
            currentColor = newColor;
            GameObject.Find("ColorSwatches").GetComponent<ColorSwatch>().OnChangeSwatchColor(currentColor);
        }
    }

    static private List<Rect> GetTextureRects(BodyPart bodyPart)
    {
        List<Rect> rects = null;

        if (bodyPart == BodyPart.HEAD)
            rects = new List<Rect>() { { new Rect(0, 48, 8, 8) }, { new Rect(8, 48, 8, 8) }, { new Rect(16, 48, 8, 8) }, { new Rect(24, 48, 8, 8) }, { new Rect(8, 56, 8, 8) }, { new Rect(16, 56, 8, 8) } };
        else if (bodyPart == BodyPart.HAT)
            rects = new List<Rect>() { { new Rect(32, 48, 8, 8) }, { new Rect(40, 48, 8, 8) }, { new Rect(48, 48, 8, 8) }, { new Rect(56, 48, 8, 8) }, { new Rect(40, 56, 8, 8) }, { new Rect(48, 56, 8, 8) } };

        else if (bodyPart == BodyPart.BODY)
            rects = new List<Rect>() { { new Rect(16, 32, 4, 12) }, { new Rect(20, 32, 8, 12) }, { new Rect(28, 32, 4, 12) }, { new Rect(32, 32, 8, 12) }, { new Rect(20, 44, 8, 4) }, { new Rect(24, 44, 8, 4) } };
        else if (bodyPart == BodyPart.RIGHTARM)
            rects = new List<Rect>() { { new Rect(40, 32, 4, 12) }, { new Rect(44, 32, 4, 12) }, { new Rect(48, 32, 4, 12) }, { new Rect(52, 32, 4, 12) }, { new Rect(44, 44, 4, 4) }, { new Rect(48, 44, 4, 4) } };
        else if (bodyPart == BodyPart.LEFTARM)
            rects = new List<Rect>() { { new Rect(32, 0, 4, 12) }, { new Rect(36, 0, 4, 12) }, { new Rect(40, 0, 4, 12) }, { new Rect(44, 0, 4, 12) }, { new Rect(36, 12, 4, 4) }, { new Rect(40, 12, 4, 4) } };
        else if (bodyPart == BodyPart.RIGHTLEG)
            rects = new List<Rect>() { { new Rect(0, 32, 4, 12) }, { new Rect(4, 32, 4, 12) }, { new Rect(8, 32, 4, 12) }, { new Rect(12, 32, 4, 12) }, { new Rect(4, 44, 4, 4) }, { new Rect(8, 44, 4, 4) } };
        else if (bodyPart == BodyPart.LEFTLEG)
            rects = new List<Rect>() { { new Rect(16, 0, 4, 12) }, { new Rect(20, 0, 4, 12) }, { new Rect(24, 0, 4, 12) }, { new Rect(28, 0, 4, 12) }, { new Rect(20, 12, 4, 4) }, { new Rect(24, 12, 4, 4) } };
        return rects;
    }


    private static void FillAction(Texture2D tex, Vector2 pixelUV, Vector3 normal, BodyPart bodyPart)
    {
        List<Rect> rects = null;
        rects = GetTextureRects(bodyPart);

        Rect rect = new Rect();
        if (Vector3.Dot(normal, Vector3.left) > .9f)
            rect = rects[0];
        else if (Vector3.Dot(normal, Vector3.forward * -1) > .9f)
            rect = rects[1];
        else if (Vector3.Dot(normal, Vector3.right) > .9f)
            rect = rects[2];
        else if (Vector3.Dot(normal, Vector3.back * -1) > .9f)
            rect = rects[3];
        else if (Vector3.Dot(normal, Vector3.up) > .9f)
            rect = rects[4];
        else if (Vector3.Dot(normal, Vector3.down) > .9f)
            rect = rects[5];

        for (int i = (int)rect.x; i < rect.x + rect.width; i++)
            for (int j = (int)rect.y; j < rect.y + rect.height; j++)
            {
                tex.SetPixel(i, j, currentColor);
                tex.Apply();
            }
    }

    static void SetPixelColor(Texture2D tex, Vector2 pixelUV, Color color)
    {
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
        tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, color);
    }
}
