using UnityEngine;

[CreateAssetMenuAttribute]
public class Settings : ScriptableObject
{
    public Color[] swatchColors = new Color[5];

    public string CurrentSkinPath;
    public int SkinCount;
    public bool HasSwiped;
    public bool HasPurchasedIAP;

}