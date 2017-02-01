using UnityEngine;
using UnityEngine.UI;

public class Modal : MonoBehaviour
{

    void ToggleButtons(bool flag)
    {
        Painter painter = Camera.main.GetComponent<Painter>();
        if (painter != null)
            Camera.main.GetComponent<Painter>().enabled = flag;
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
        foreach (var button in buttons)
        {
            button.GetComponent<Button>().enabled = flag;
        }

        GameObject.Find("Gestures").GetComponent<BoxCollider>().enabled = flag;
    }

    void OnEnable()
    {
        ToggleButtons(false);
    }

    void OnDisable()
    {
        ToggleButtons(true);

    }


}
