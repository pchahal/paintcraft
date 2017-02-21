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


    public void Start()
    {

        startPos = transform.position;
        endPos = transform.position + axis * moveDistance;


        hint = GetComponent<Image>();

        if (PlayerPrefs.GetInt("HasSwiped") == 0)
        {

            InvokeRepeating("ShowSwipeHint", 5, lerpTime);
        }
    }

    private void ShowSwipeHint()
    {
        currentLerpTime = 0;
        hint.enabled = true; ;



        if (PlayerPrefs.GetInt("HasSwiped") == 1)
            HideSwipeHint();
    }

    public void HideSwipeHint()
    {
        CancelInvoke();
        hint.enabled = false;

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