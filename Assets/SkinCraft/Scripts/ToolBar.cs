using UnityEngine;
using UnityEngine.UI;

public class ToolBar : MonoBehaviour
{

    Color pressedColor;
    Color normalColor;

    void Start()
    {
        foreach (var button in transform.GetComponentsInChildren<Button>())
        {
            normalColor = button.colors.normalColor;
            pressedColor = button.colors.pressedColor;
        }
    }

    public void SelectButton(Button sender)
    {
        Button[] buttons = transform.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].name == sender.gameObject.name)
                buttons[i].GetComponent<Image>().color = pressedColor;
            else
                buttons[i].GetComponent<Image>().color = normalColor;
        }
    }
}
