using UnityEngine;

[CreateAssetMenuAttribute]
public class Settings : ScriptableObject
{
    public Color[] swatchColors = new Color[5];

    public string CurrentSkinPath;
    public bool HasSwiped;
    private bool hasPurchasedIAP;

    public bool HasPurchasedIAP
    {
        get { return hasPurchasedIAP; }
        set
        {
            hasPurchasedIAP = value;
            UnityEditor.EditorUtility.SetDirty(this);
        }

    }
}
