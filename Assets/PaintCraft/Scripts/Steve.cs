using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BodyPart
{

    HEAD,
    HAT,
    BODY,
    JACKET,
    LEFTARM,
    LEFTSLEEVE,
    RIGHTARM,
    RIGHTSLEEVE,
    LEFTLEG,
    LEFTPANT,
    RIGHTLEG,
    RIGHTPANT,
    NONE,
}

static class SteveDict
{
    public static Dictionary<string, BodyPart> bodyPartsDict = new Dictionary<string, BodyPart>()
    {
        { "Head",            BodyPart.HEAD        },
        {
            "Hat",
            BodyPart.HAT
        },
        { "Body",           BodyPart.BODY         },
        {
            "Jacket",
            BodyPart.JACKET
        },
        { "Left Arm",        BodyPart.LEFTARM     },
        {
            "Left Sleeve",
            BodyPart.LEFTSLEEVE
        },
        { "Right Arm",       BodyPart.RIGHTARM    },
        {
            "Right Sleeve",
            BodyPart.RIGHTSLEEVE
        },
        { "Left Leg",        BodyPart.LEFTLEG     },
        {
            "Left Pant",
            BodyPart.LEFTPANT
        },
        { "Right Leg",       BodyPart.RIGHTLEG    },
        {
            "Right Pant",
            BodyPart.RIGHTPANT
        },
        { "Quad",            BodyPart.BODY        }
    };
}


public class Steve : MonoBehaviour
{

    private Transform head;
    private Transform body;
    private Transform rightArm;
    private Transform leftArm;
    private Transform leftLeg;
    private Transform rightLeg;
    public Material mat;
    private Transform currentBodyPart;


    Texture2D tex;

    Color[] defaultColors;
    string filePath;
    [HideInInspector]
    public int bodyClothes;


    public int GetBodyClothes()
    {
        return bodyClothes;
    }

    public void SetBodyClothes(int value)
    {
        bodyClothes = value;

        UVEditor[] uvEditors = gameObject.GetComponentsInChildren<UVEditor>();
        foreach (UVEditor uv in uvEditors)
        {
            uv.ChangeUVs(bodyClothes);
        }
    }

    public bool isEditMode { get; set; }

    void Start()
    {
   
      
        Debug.Log("steve.cs path=" + PlayerPrefs.GetString("CurrentSkinPath"));
        Texture2D defaultTexture = Resources.Load("DefaultSkin") as Texture2D;
        defaultColors = defaultTexture.GetPixels();
        for (int i = 0; i < defaultColors.Length; i++)
        {
            defaultColors [i].a = 0;
        }

        filePath = PlayerPrefs.GetString("CurrentSkinPath");
        if (filePath != "")
        {
            tex = FileManager.Instance.GetTextureFromPNG(filePath);
            mat.SetTexture("_MainTex", tex);
        } else
        {
            int skinCount = FileManager.Instance.GetAllFiles(Application.persistentDataPath, "*.png").Length + 1;
            filePath = Application.persistentDataPath + "/" + "Skin" + skinCount + ".png";
            tex = new Texture2D(defaultTexture.width, defaultTexture.height, TextureFormat.RGBAHalf, false);
            tex.filterMode = FilterMode.Point;
            tex.SetPixels(defaultColors);
            tex.Apply();

            mat.SetTexture("_MainTex", tex);
        }
        head = transform.FindChild("Head");
        body = transform.FindChild("Body");
        rightArm = transform.FindChild("Right Arm");
        leftArm = transform.FindChild("Left Arm");
        rightLeg = transform.FindChild("Right Leg");
        leftLeg = transform.FindChild("Left Leg");
    }

    public void ShowBodyPart(BodyPart bodyPart)
    {
        GameObject.Find("Part").GetComponent<Text>().text = GetCurrentPartName(bodyPart);
        ShowAll(false);
        if (bodyPart == BodyPart.HEAD || bodyPart == BodyPart.HAT)
        {
            currentBodyPart = transform.FindChild("Head");
            currentBodyPart.gameObject.SetActive(true);
        } else if (bodyPart == BodyPart.BODY || bodyPart == BodyPart.JACKET)
        {
            currentBodyPart = transform.FindChild("Body");
            currentBodyPart.gameObject.SetActive(true);
        } else if (bodyPart == BodyPart.RIGHTARM || bodyPart == BodyPart.RIGHTSLEEVE)
        {
            currentBodyPart = transform.FindChild("Right Arm");
            currentBodyPart.gameObject.SetActive(true);
        } else if (bodyPart == BodyPart.LEFTARM || bodyPart == BodyPart.LEFTSLEEVE)
        {
            currentBodyPart = transform.FindChild("Left Arm");
            currentBodyPart.gameObject.SetActive(true);
        } else if (bodyPart == BodyPart.RIGHTLEG || bodyPart == BodyPart.RIGHTPANT)
        {
            currentBodyPart = transform.FindChild("Right Leg");
            currentBodyPart.gameObject.SetActive(true);
        } else if (bodyPart == BodyPart.LEFTLEG || bodyPart == BodyPart.LEFTPANT)
        {
            currentBodyPart = transform.FindChild("Left Leg");
            currentBodyPart.gameObject.SetActive(true);
        } else
        {
            isEditMode = false;
            currentBodyPart = null;
            ResetRotations();
            ShowAll(true);
        }
    }

    private void ShowAll(bool flag)
    {
        foreach (Transform go in transform)
        {
            go.gameObject.SetActive(flag);
        }
    }


    private void ResetRotations()
    {
        head.rotation = Quaternion.identity;
        body.rotation = Quaternion.identity;
        rightArm.rotation = Quaternion.identity;
        leftArm.rotation = Quaternion.identity;
        rightLeg.rotation = Quaternion.identity;
        leftLeg.rotation = Quaternion.identity;
    }

    public void RotateBodyPart(Vector2 rot)
    {
        if (currentBodyPart != null)
        {
            if (Mathf.Abs(rot.x) > Mathf.Abs(rot.y))
                currentBodyPart.RotateAround(currentBodyPart.position, Vector3.up, Time.deltaTime * 250 * Mathf.Sign(rot.x));
            else
                currentBodyPart.RotateAround(currentBodyPart.position, Vector3.right, Time.deltaTime * 250 * Mathf.Sign(rot.y));
        }
    }

    public Color GetDefaultPixel(Vector2 pixelUV)
    {
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        int x = (int)pixelUV.x;
        int y = (int)pixelUV.y;

        Color currentColor = defaultColors [x * tex.width + y];
        currentColor.a = 0;
        return currentColor;
    }

    public void SaveSkin()
    {
        FileManager.Instance.SaveTextureToFile(tex, filePath);
    }

    public Texture2D GetCurrentSkinTexture()
    {
        return tex;
    }

    public string GetCurrentPartName(BodyPart part)
    {
        if (bodyClothes == 0)
        {
            if (part == BodyPart.HEAD)
                return "Head";
            else if (part == BodyPart.BODY)
                return "Body";
            else if (part == BodyPart.LEFTARM)
                return "Left Arm";
            else if (part == BodyPart.LEFTLEG)
                return "Left Leg";
            else if (part == BodyPart.RIGHTARM)
                return "Right Arm";
            else if (part == BodyPart.RIGHTLEG)
                return "Right Leg";
            else
                return "";
        } else
        {
            if (part == BodyPart.HEAD)
                return "Hat";
            else if (part == BodyPart.BODY)
                return "Jacket";
            else if (part == BodyPart.LEFTARM)
                return "Left Sleeve";
            else if (part == BodyPart.LEFTLEG)
                return "Left Pant";
            else if (part == BodyPart.RIGHTARM)
                return "Right Sleeve";
            else if (part == BodyPart.RIGHTLEG)
                return "Right Pant";
            else
                return "";
        }
    }

    public BodyPart GetCurrentBodyPart(BodyPart part)
    {
        if (bodyClothes == 0)
        {
            return part;
        } else
        {
            if (part == BodyPart.HEAD)
                return BodyPart.HAT;
            else if (part == BodyPart.BODY)
                return BodyPart.JACKET;
            else if (part == BodyPart.LEFTARM)
                return BodyPart.LEFTSLEEVE;
            else if (part == BodyPart.LEFTLEG)
                return BodyPart.LEFTPANT;
            else if (part == BodyPart.RIGHTARM)
                return BodyPart.RIGHTARM;
            else if (part == BodyPart.RIGHTLEG)
                return BodyPart.RIGHTPANT;
            else
                return BodyPart.NONE;
        }

    }


}
