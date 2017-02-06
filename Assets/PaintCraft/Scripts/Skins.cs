
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skins : MonoBehaviour
{
    private string[] textures;
    public GameObject buttonPrefab;
    public GameObject tapNewSkin;

    void Start()
    {


        PlayerPrefs.SetString("CurrentSkinPath", "");
        textures = FileManager.Instance.GetAllFiles(Application.persistentDataPath, "*.png");
        Point point = GetGalleryPosition(textures.Length);
        Rect rect = transform.GetComponent<RectTransform>().rect;
        int numRows = (1 + textures.Length) / 3;
        int height = numRows * 125 + numRows * 25;// Mathf.Abs(point.y);
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, height);
        if (textures.Length > 0)
            tapNewSkin.SetActive(false);

        int i = 1;
        foreach (var tex in textures)
        {
            GameObject background = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
            Point backposition = GetGalleryPosition(i);
            background.transform.SetParent(transform, false);
            background.GetComponent<RectTransform>().localPosition = new Vector3(backposition.x, backposition.y, background.transform.position.z);
            Destroy(background.GetComponent<Button>());


            GameObject button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
            Point position = GetGalleryPosition(i);
            button.transform.SetParent(transform, false);
            button.GetComponent<RectTransform>().localPosition = new Vector3(position.x, position.y, button.transform.position.z);
            button.name = tex;
            button.GetComponent<Image>().sprite = FileManager.Instance.GetSpriteFromPNG(tex, new Rect(8, 48, 8, 8));
            button.GetComponent<Button>().onClick.AddListener(delegate
            {
                OnClickSkinButton(tex);
            });
            i++;

        }
    }

    private Point GetGalleryPosition(int index)
    {
        int paddingSize = 25;
        int imageSize = 125;
        int imagesPerRow = 3;
        int row = (index / imagesPerRow);
        int col = (index % imagesPerRow);
        int y = (row * imageSize + row * paddingSize) * -1;
        int x = col * imageSize + col * paddingSize;
        Point point = new Point(x, y);
        return point;
    }


    public void OnClickSkinButton(string filePath)
    {
        Debug.Log("skins.cs onclickbutton=" + filePath);
        PlayerPrefs.SetString("CurrentSkinPath", filePath);
        SceneManager.LoadScene(1);
    }
}
