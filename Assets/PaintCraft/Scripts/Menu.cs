using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class Menu : MonoBehaviour
{

    public GameObject toolButton;
    public GameObject bodyClothesButton;
    public GameObject backButton;
    public GameObject colorSwatches;
    public Transform stevePaintable;
    public Transform steveRigable;
    public GameObject deleteButton;
    public GameObject playButton;
    public GameObject shareButton;
    public GameObject swipeHintUp;
    public GameObject swipeHintRight;
    public GameObject inappPurchase;
    public GameObject savedToGallery;
    private Settings settings;

    void Start()
    {
        settings = Resources.Load("Settings") as Settings;
    }

    void OnEnable()
    {
        NativeToolkit.OnImageSaved += ImageSaved;
    }

    void OnDisable()
    {
        NativeToolkit.OnImageSaved -= ImageSaved;
    }

    public void EditSkin()
    {
        bodyClothesButton.SetActive(false);
        toolButton.SetActive(true);
        backButton.GetComponentInChildren<Text>().text = "BODY";
        GameObject.Find("TapBodyPart").GetComponent<Text>().enabled = false;
        GameObject.Find("Part").GetComponent<Text>().enabled = true;
        colorSwatches.SetActive(true);
        playButton.SetActive(false);
        deleteButton.SetActive(false);
        shareButton.SetActive(false);
        swipeHintUp.SetActive(true);
        swipeHintRight.SetActive(true);
    }

    public void OnBackButton()
    {
        bodyClothesButton.SetActive(true);
        toolButton.SetActive(false);

        Text text = backButton.GetComponentInChildren<Text>();
        if (text.text == "BODY")
        {
            backButton.GetComponentInChildren<Text>().text = "SKINS";
            Camera.main.GetComponent<SkinCamera>().ZoomIn(BodyPart.NONE);
            stevePaintable.GetComponent<Steve>().ShowBodyPart(BodyPart.NONE);
            GameObject.Find("TapBodyPart").GetComponent<Text>().enabled = true;
            GameObject.Find("Part").GetComponent<Text>().enabled = false;
            colorSwatches.SetActive(false);
            stevePaintable.GetComponent<Steve>().SaveSkin();
        } else if (text.text == "SKINS")
        {
            SceneManager.LoadScene(0);
        }

        playButton.SetActive(true);
        deleteButton.SetActive(true);
        shareButton.SetActive(true);
        stevePaintable.gameObject.SetActive(true);
        steveRigable.gameObject.SetActive(false);
        swipeHintUp.SetActive(true);
        swipeHintRight.SetActive(true);

    }

    public void OnPlayButton()
    {
        stevePaintable.gameObject.SetActive(false);
        steveRigable.gameObject.SetActive(true);
        steveRigable.GetComponent<Animator>().SetBool("Walking", true);

        bodyClothesButton.SetActive(false);
        backButton.GetComponentInChildren<Text>().text = "BODY";
        colorSwatches.SetActive(false);
        playButton.SetActive(false);
        deleteButton.SetActive(false);
        shareButton.SetActive(false);


    }

    public void OnShareButton()
    {
        string fileName = PlayerPrefs.GetString("CurrentSkinPath");
        fileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
        Debug.Log("haspurchasedIAP=" + settings.HasPurchasedIAP);
        if (settings.HasPurchasedIAP)
        {
            NativeToolkit.SaveImage(stevePaintable.GetComponent<Steve>().GetCurrentSkinTexture(), fileName, "png");
        } else
        {
            inappPurchase.SetActive(true);
        }
    }

    void ImageSaved(string filename)
    {
        savedToGallery.SetActive(true);
        savedToGallery.transform.Find("BodySaved").gameObject.SetActive(true);
        savedToGallery.transform.Find("BodyFailed").gameObject.SetActive(false);
    }



    public void OnDeleteButton()
    {
        string path = PlayerPrefs.GetString("CurrentSkinPath");
        if (path != "")
            File.Delete(path);
        SceneManager.LoadScene(0);
    }



}


