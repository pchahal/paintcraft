using UnityEngine;
using UnityEngine.UI;

public class ColorSwatch : MonoBehaviour
{
    public Image currentSwatch;
    public Settings settings;
    public ColorPicker colorPicker;
    public Image[] swatches;

    void Start()
    {
        settings = Resources.Load("Settings") as Settings;
        for (int i = 0; i < swatches.Length; i++)
        {
            swatches[i].color = settings.swatchColors[i];
        }
    }


    public void OnClickSwatch(Button swatchButton)
    {
        colorPicker.gameObject.SetActive(true);
        currentSwatch = swatchButton.GetComponent<Image>();

        foreach (Transform t in transform)
        {
            t.localScale = new Vector3(.6f, .6f, .6f);
        }

        currentSwatch.transform.localScale = new Vector3(.8f, .8f, .8f);

        //LOAD hue and colorpointer 
        colorPicker.CurrentColor = currentSwatch.color;
    }


    public void OnChangeSwatchColor(Color color)
    {
        currentSwatch.color = color;
        settings.swatchColors[GetSwatchNumber()] = currentSwatch.color;
    }

    private int GetSwatchNumber()
    {
        string swatchName = currentSwatch.gameObject.name;
        if (swatchName == "swatch1") return 0;
        if (swatchName == "swatch1") return 1;
        if (swatchName == "swatch1") return 2;
        if (swatchName == "swatch1") return 3;
        else return 4;
    }
}
