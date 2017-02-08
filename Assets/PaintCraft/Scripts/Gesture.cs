using UnityEngine;


public class Gesture : MonoBehaviour
{
    // public float speed = 0.1F;
    private Vector3 mouseDown;
    public Steve steve;
    Painter painter;



    void Start()
    {
        painter = Camera.main.GetComponent<Painter>();

    }

    void OnMouseDrag()
    {
        painter.enabled = false;
        Vector2 rotation = Input.mousePosition - mouseDown;
        if (Vector2.Distance(Input.mousePosition, mouseDown) > Mathf.Abs(5))
            steve.RotateBodyPart(rotation);
        OnMouseDown();

        PlayerPrefs.SetInt("HasSwiped", 1);
    }


    void OnMouseDown()
    {
        mouseDown = Input.mousePosition;
    }

    void OnMouseUp()
    {
        painter.enabled = true;
    }


}