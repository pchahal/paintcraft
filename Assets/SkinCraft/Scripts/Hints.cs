using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour
{
    float lerpTime = 1.5f;
    float currentLerpTime = 5f;

    float moveDistance = 200f;

    Vector3 startPos;
    Vector3 endPos;
    public Vector3 axis;
    Image hint;

    bool hasSwiped;
    Text swipeText;
    Settings settings;
    public void Start()
    {

        startPos = transform.position;
        endPos = transform.position + axis * moveDistance;

        swipeText = GameObject.Find("SwipeText").GetComponent<Text>();
        hint = GetComponent<Image>();

        settings = Resources.Load("Settings") as Settings;
        hasSwiped = settings.HasSwiped;
        hasSwiped = false;
        if (!hasSwiped)
        {

            InvokeRepeating("ShowSwipeHint", 5, lerpTime);
        }
    }

    private void ShowSwipeHint()
    {
        currentLerpTime = 0;
        hint.enabled = true; ;

        swipeText.enabled = true;
        hasSwiped = settings.HasSwiped;
        if (hasSwiped)
            HideSwipeHint();
    }

    public void HideSwipeHint()
    {
        CancelInvoke();
        hint.enabled = false;

        swipeText.enabled = false;
        transform.gameObject.SetActive(false);
    }

    protected void Update()
    {
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //lerp!
        float perc = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(startPos, endPos, perc);
    }
}