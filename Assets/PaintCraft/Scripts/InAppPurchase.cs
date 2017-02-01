using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppPurchase : MonoBehaviour
{
    public GameObject SavedToGallery;

    public void OnBuy()
    {
        Debug.Log("Buy PRODUCT");
    }

    public void OnPurchasedSuccessfully()
    {
        SavedToGallery.SetActive(true);
        Debug.Log("purchase success");
    }

}
