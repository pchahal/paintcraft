struct Point
{
    public int x, y;
    public Point(int px, int py)
    {
        x = px;
        y = py;

    }
}



/*
IEnumerator LerpColor()
 {
     float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
     float increment = smoothness/duration; //The amount of change to apply.
     while(progress < 1)
     {
        currentColor = Color.Lerp(currentColor, Color.red, progress);
        progress += increment;
        material.color = currentColor;

        yield return new WaitForSeconds(smoothness);
     }
     currentColor = Color.white;
     material.color = Color.white;
     Menu.Instance.EditSkin();
     yield return true;
 }*/
